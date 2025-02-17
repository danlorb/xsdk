using System.Reflection;
using xSdk.Hosting;
using Zio;
using Zio.FileSystems;

namespace xSdk.Extensions.IO
{
    public static class FileSystemHelper
    {
        public static string GetExecutingFolder()
        {
            var folder = Environment.CurrentDirectory;
            var assembly = Assembly.GetEntryAssembly();
            if (assembly != null)
            {
                folder = Path.GetDirectoryName(assembly.Location);
            }
            return folder;
        }

        public static string NormalizePath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (!path.StartsWith("/"))
                {
                    path = "/" + path;
                }
            }
            return path;
        }

        public static UPath GetFullPath(string path)
        {
            var fs = new PhysicalFileSystem();
            return fs.ConvertPathFromInternal(path);
        }

        public static string CreateSpecificDataFolder(FileSystemContext context, string folder)
        {
            var root = SlimHost.Instance.FileSystem.RequestFileSystem(context);
            return CreateSpecificDataFolder(root, folder);
        }

        public static string CreateSpecificDataFolder(IFileSystemResult fileSystem, string folder)
        {
            return CreateSpecificDataFolder(fileSystem.Data, folder);
        }

        public static string CreateSpecificDataFolder(IFileSystem fileSystem, string folder)
        {
            if (!fileSystem.DirectoryExists(folder))
            {
                fileSystem.CreateDirectory(folder);
            }

            return fileSystem.GetFullPath(folder);
        }

        public static string SearchGitRoot(string root)
        {
            if (string.IsNullOrEmpty(root) || new DirectoryInfo(root).Parent == null)
            {
                return System.Environment.CurrentDirectory;
            }

            if (IsGitRoot(root))
                return root;

            return SearchGitRoot(Path.Combine(root, ".."));
        }

        private static bool IsGitRoot(string root)
        {
            var current = Path.Combine(root, ".git");
            if (!string.IsNullOrEmpty(current) && Directory.Exists(current))
                return true;

            return false;
        }

        //public static bool IsDirectoryWritable(DirectoryInfo dir)
        //    => IsDirectoryWritable(dir.FullName);

        //public static bool IsDirectoryWritable(string dirPath)
        //{
        //    try
        //    {
        //        if (Directory.Exists(dirPath))
        //        {
        //            using (FileStream fs = File.Create(Path.Combine(dirPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
        //            { }
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public static bool IsDirectoryReadable(DirectoryInfo dir)
        //    => IsDirectoryReadable(dir.FullName);

        //public static bool IsDirectoryReadable(string dirPath)
        //{
        //    try
        //    {
        //        if (Directory.Exists(dirPath))
        //        {
        //            Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories);
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
