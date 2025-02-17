using AutoMapper;
using Microsoft.Extensions.Logging;
using NLog;

namespace xSdk.Data.Converters.Mapper
{
    public sealed class SemVerToStringConverter : IValueConverter<SemVer, string>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public string Convert(SemVer sourceMember, ResolutionContext context)
        {
            string result = default;
            try
            {
                if (sourceMember != null)
                {
                    result = sourceMember.Version;
                }
            }
            catch (Exception ex)
            {
                logger.Error(
                    ex,
                    "Version could not converted. See further Log for further Details"
                );
                if (ex.InnerException != null)
                    logger.Info(ex.InnerException.Message);

                throw;
            }

            return result;
        }
    }
}
