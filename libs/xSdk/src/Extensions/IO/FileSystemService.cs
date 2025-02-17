using System.Runtime.InteropServices;
using NLog;
using xSdk.Extensions.Variable;
using xSdk.Hosting;
using xSdk.Security;
using Zio;
using Zio.FileSystems;

namespace xSdk.Extensions.IO
{
    internal class FileSystemService : IFileSystemService
    {
        private readonly EnvironmentSetup envSetup;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public FileSystemService(IVariableService variableService)
        {
            envSetup = variableService.GetSetup<EnvironmentSetup>();
        }

        internal FileSystemService() { }

        public IFileSystemResult Local =>
            RequestFileSystemAsync(FileSystemContext.Local).GetAwaiter().GetResult();

        public IFileSystemResult User =>
            RequestFileSystemAsync(FileSystemContext.User).GetAwaiter().GetResult();

        public IFileSystemResult Machine =>
            RequestFileSystemAsync(FileSystemContext.Machine).GetAwaiter().GetResult();

        public Task<IFileSystemResult> RequestFileSystemAsync(
            FileSystemContext context = FileSystemContext.None,
            CancellationToken token = default
        )
        {
            var rootFolders = CreateRootFolders();

            logger.Info("Request filesystem for context '{0}'", context);

            InternalFileSystemResult result = new()
            {
                App = new PhysicalFileSystem(),
                Data = new PhysicalFileSystem(),
            };

            IFileSystem rootFileSystem = new PhysicalFileSystem();
            if (context == FileSystemContext.Machine)
            {
                if (rootFileSystem.DirectoryExists(rootFolders.Machine))
                {
                    result.App = new SubFileSystem(rootFileSystem, rootFolders.Machine);
                }

                if (rootFileSystem.DirectoryExists(rootFolders.MachineData))
                {
                    result.Data = new SubFileSystem(rootFileSystem, rootFolders.MachineData);
                }
            }
            else if (context == FileSystemContext.User)
            {
                result.App = new SubFileSystem(rootFileSystem, rootFolders.User);
                result.Data = new SubFileSystem(rootFileSystem, rootFolders.UserData);
            }
            else if (context == FileSystemContext.Local)
            {
                result.App = new SubFileSystem(rootFileSystem, rootFolders.Local);
                if (rootFileSystem.DirectoryExists(rootFolders.LocalData))
                {
                    result.Data = new SubFileSystem(rootFileSystem, rootFolders.LocalData);
                }
            }

            return Task.FromResult<IFileSystemResult>(result);
        }

        private RootFolders CreateRootFolders()
        {
            logger.Trace("Determine root folders");

            RootFolders result;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                result = new RootFolders
                {
                    Machine = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                    User = Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData
                    ),

                    MachineData = Environment.GetFolderPath(
                        Environment.SpecialFolder.CommonApplicationData
                    ),
                    UserData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                };
            }
            else
            {
                var home = Environment.GetEnvironmentVariable("HOME");
                if (string.IsNullOrEmpty(home))
                {
                    home = "/home/" + Environment.UserName;
                }

                result = new RootFolders
                {
                    Machine = UPath.Combine("/usr", "local"),
                    User = UPath.Combine(home, ".local", "share"),

                    MachineData = UPath.Combine("/usr", "local", "share"),
                    UserData = UPath.Combine(home, ".config"),
                };
            }

            result.Local = FileSystemHelper.GetExecutingFolder();
            result.LocalData = FileSystemHelper.GetExecutingFolder();

            var companyName = SlimHost.Instance.AppCompany;
            var appName = SlimHost.Instance.AppName;
            if (envSetup != null)
            {
                var contentRoot = envSetup.ContentRoot;

                if (!string.IsNullOrEmpty(contentRoot))
                {
                    result.Local = contentRoot;
                    result.LocalData = contentRoot;
                }

                companyName = envSetup.AppCompany;
                appName = envSetup.AppName;
            }

            var specificPath = (UPath)$"{companyName}/{appName}".ToLower();
            result.Machine = CreateFolder(
                result.Machine / specificPath,
                SecurityContext.IsSuperUser()
            );
            result.User = CreateFolder(result.User / specificPath);
            result.Local = CreateFolder(result.Local);

            result.MachineData = CreateFolder(result.MachineData / specificPath);
            result.UserData = CreateFolder(result.UserData / specificPath);
            result.LocalData = CreateFolder(result.LocalData);

            // Fallback, if path does not exists
            result.MachineData = ValidatePaths(result.MachineData, result.UserData);

            return result;
        }

        private UPath CreateFolder(UPath path, bool shouldCreate = true)
        {
            logger.Trace("Create folder if not exists");

            var fs = new PhysicalFileSystem();
            var realPath = fs.ConvertPathFromInternal(path.FullName);

            if (!fs.DirectoryExists(realPath))
            {
                try
                {
                    if (shouldCreate)
                    {
                        logger.Trace("Creating folder '{0}'", realPath);
                        fs.CreateDirectory(realPath);
                    }
                }
                catch
                {
                    logger.Trace("Folder '{0}' could not created", realPath);
                }
            }
            return realPath;
        }

        private UPath ValidatePaths(UPath path, UPath fallback)
        {
            var pfs = new PhysicalFileSystem();
            var realPath = pfs.GetFullPath(path);
            var realFallbackPath = pfs.GetFullPath(fallback);

            if (Directory.Exists(realPath))
            {
                logger.Debug("Path '{0}' exists", realPath);
                return path;
            }
            else if (Directory.Exists(realFallbackPath))
            {
                logger.Warn(
                    "Path '{0}' does not exist. Use fallback path '{1}'",
                    realPath,
                    realFallbackPath
                );
                return fallback;
            }
            else
            {
                throw new SdkException(
                    string.Format(
                        "Real path '{0}' and fallback path '{1}' does not exist or could not created",
                        realPath,
                        realFallbackPath
                    )
                );
            }
        }
    }
}
