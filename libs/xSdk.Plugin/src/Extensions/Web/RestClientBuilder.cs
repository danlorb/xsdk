using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using NLog;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.Json;
using xSdk.Data;
using xSdk.Shared;

namespace xSdk.Extensions.Web
{
    public static class RestClientBuilder
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static IRestClient CreateRestClient(string baseUrl) =>
            CreateRestClient(baseUrl, default, default, default);

        public static IRestClient CreateRestClient(string baseUrl, IProgress<double> progress) =>
            CreateRestClient(baseUrl, default, default, progress);

        public static IRestClient CreateRestClient(
            string baseUrl,
            Action<RestClientOptions>? configure
        ) => CreateRestClient(baseUrl, default, configure, default);

        public static IRestClient CreateRestClient(
            string baseUrl,
            Action<RestClientOptions>? configure,
            IProgress<double> progress
        ) => CreateRestClient(baseUrl, default, configure, progress);

        public static IRestClient CreateRestClient(string baseUrl, IAuthenticator? authenticator) =>
            CreateRestClient(baseUrl, authenticator, default, default);

        public static IRestClient CreateRestClient(
            string baseUrl,
            IAuthenticator? authenticator,
            IProgress<double> progress
        ) => CreateRestClient(baseUrl, authenticator, default, progress);

        public static IRestClient CreateRestClient(
            string baseUrl,
            IAuthenticator? authenticator,
            Action<RestClientOptions>? configure
        ) => CreateRestClient(baseUrl, authenticator, configure, default);

        public static IRestClient CreateRestClient(
            string baseUrl,
            IAuthenticator? authenticator,
            Action<RestClientOptions>? configure,
            IProgress<double>? progress
        ) => CreateRestClientInternal(new Uri(baseUrl), authenticator, configure, progress);

        public static IRestClient CreateRestClient(Uri baseUrl) =>
            CreateRestClientInternal(baseUrl, default, default, default);

        public static IRestClient CreateRestClient(Uri baseUrl, IProgress<double> progress) =>
            CreateRestClientInternal(baseUrl, default, default, progress);

        public static IRestClient CreateRestClient(
            Uri baseUrl,
            Action<RestClientOptions> configure
        ) => CreateRestClientInternal(baseUrl, default, configure, default);

        public static IRestClient CreateRestClient(
            Uri baseUrl,
            Action<RestClientOptions> configure,
            IProgress<double> progress
        ) => CreateRestClientInternal(baseUrl, default, configure, progress);

        public static IRestClient CreateRestClient(Uri baseUrl, IAuthenticator? authenticator) =>
            CreateRestClientInternal(baseUrl, authenticator, default, default);

        public static IRestClient CreateRestClient(
            Uri baseUrl,
            IAuthenticator? authenticator,
            IProgress<double> progress
        ) => CreateRestClientInternal(baseUrl, authenticator, default, progress);

        public static IRestClient CreateRestClient(
            Uri baseUrl,
            IAuthenticator? authenticator,
            Action<RestClientOptions> configure
        ) => CreateRestClientInternal(baseUrl, authenticator, configure, default);

        private static IRestClient CreateRestClient(
            Uri baseUrl,
            IAuthenticator? authenticator,
            Action<RestClientOptions> configure,
            IProgress<double> progress
        ) => CreateRestClientInternal(baseUrl, authenticator, configure, progress);

        private static IRestClient CreateRestClientInternal(
            Uri baseUrl,
            IAuthenticator? authenticator,
            Action<RestClientOptions>? configure,
            IProgress<double>? progress
        )
        {
            logger.Trace("Create rest api client");

            var options = new RestClientOptions(baseUrl);

            // Server Cert Validation
            options.RemoteCertificateValidationCallback = CertificateHelper.ValidateServerCallbacks;
            if (authenticator != null)
            {
                options.Authenticator = authenticator;
            }

            configure?.Invoke(options);

            var httpClient = HttpClientBuilder.CreateHttpClient(baseUrl, progress);

            return new RestClient(
                httpClient,
                options,
                configureSerialization: s => s.UseSystemTextJson(JsonHelper.GetSerializerOptions())
            );
        }
    }
}
