using AutoMapper;
using NLog;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class StringToSemVerConverter : IValueConverter<string, SemVer>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public SemVer Convert(string sourceMember, ResolutionContext context)
        {
            SemVer result = default;

            try
            {
                if (!string.IsNullOrEmpty(sourceMember))
                {
                    result = new SemVer(sourceMember);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Version could not converted. See further Log for further Details");
                if (ex.InnerException != null)
                    logger.Info(ex.InnerException.Message);

                throw;
            }

            return result;
        }
    }
}
