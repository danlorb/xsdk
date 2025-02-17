using System.Net.Http.Handlers;
using NLog;
using xSdk.Hosting;
using xSdk.Shared;

namespace xSdk.Extensions.Web
{
    public static class HttpClientBuilder
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static HttpClient CreateHttpClient() =>
            CreateHttpClientInternal(null, null, null, null);

        public static HttpClient CreateHttpClient(IProgress<double>? progress) =>
            CreateHttpClientInternal(null, null, null, progress);

        public static HttpClient CreateHttpClient(Action<HttpClientHandler> configure) =>
            CreateHttpClientInternal(null, configure, null, null);

        public static HttpClient CreateHttpClient(
            Action<HttpClientHandler> configure,
            IProgress<double>? progress
        ) => CreateHttpClientInternal(null, configure, null, progress);

        public static HttpClient CreateHttpClient(IDictionary<string, string>? additionalHeaders) =>
            CreateHttpClientInternal(null, null, additionalHeaders, null);

        public static HttpClient CreateHttpClient(
            IDictionary<string, string>? additionalHeaders,
            IProgress<double>? progress
        ) => CreateHttpClientInternal(null, null, additionalHeaders, progress);

        public static HttpClient CreateHttpClient(
            Action<HttpClientHandler> configure,
            IDictionary<string, string>? additionalHeaders
        ) => CreateHttpClientInternal(null, configure, additionalHeaders, null);

        public static HttpClient CreateHttpClient(
            Action<HttpClientHandler> configure,
            IDictionary<string, string>? additionalHeaders,
            IProgress<double>? progress
        ) => CreateHttpClientInternal(null, configure, additionalHeaders, progress);

        public static HttpClient CreateHttpClient(string? baseUrl) =>
            CreateHttpClient(baseUrl, null, null, null);

        public static HttpClient CreateHttpClient(string? baseUrl, IProgress<double>? progress) =>
            CreateHttpClient(baseUrl, null, null, progress);

        public static HttpClient CreateHttpClient(
            string? baseUrl,
            Action<HttpClientHandler> configure
        ) => CreateHttpClient(baseUrl, configure, null, null);

        public static HttpClient CreateHttpClient(
            string? baseUrl,
            Action<HttpClientHandler> configure,
            IProgress<double>? progress
        ) => CreateHttpClient(baseUrl, configure, null, progress);

        public static HttpClient CreateHttpClient(
            string? baseUrl,
            IDictionary<string, string>? additionalHeaders
        ) => CreateHttpClient(baseUrl, null, additionalHeaders, null);

        public static HttpClient CreateHttpClient(
            string? baseUrl,
            IDictionary<string, string>? additionalHeaders,
            IProgress<double>? progress
        ) => CreateHttpClient(baseUrl, null, additionalHeaders, null);

        public static HttpClient CreateHttpClient(
            string? baseUrl,
            Action<HttpClientHandler>? configure,
            IDictionary<string, string>? additionalHeaders
        ) => CreateHttpClient(baseUrl, configure, additionalHeaders, null);

        public static HttpClient CreateHttpClient(
            string? baseUrl,
            Action<HttpClientHandler>? configure,
            IDictionary<string, string>? additionalHeaders,
            IProgress<double>? progress
        )
        {
            if (!string.IsNullOrEmpty(baseUrl))
            {
                if (!baseUrl.StartsWith("http") && !baseUrl.StartsWith("https"))
                    throw new SdkException("Http-Scheme for BaseUrl is missing");

                if (!baseUrl.EndsWith("/"))
                    baseUrl += "/";

                return CreateHttpClientInternal(
                    new Uri(baseUrl),
                    configure,
                    additionalHeaders,
                    progress
                );
            }

            return CreateHttpClientInternal(null, configure, additionalHeaders, progress);
        }

        public static HttpClient CreateHttpClient(Uri? baseUrl) =>
            CreateHttpClientInternal(baseUrl, null, null, null);

        public static HttpClient CreateHttpClient(Uri? baseUrl, IProgress<double>? progress) =>
            CreateHttpClientInternal(baseUrl, null, null, progress);

        public static HttpClient CreateHttpClient(
            Uri? baseUrl,
            Action<HttpClientHandler>? configure
        ) => CreateHttpClientInternal(baseUrl, configure, null, null);

        public static HttpClient CreateHttpClient(
            Uri? baseUrl,
            Action<HttpClientHandler>? configure,
            IProgress<double>? progress
        ) => CreateHttpClientInternal(baseUrl, configure, null, progress);

        public static HttpClient CreateHttpClient(
            Uri? baseUrl,
            IDictionary<string, string>? additionalHeaders
        ) => CreateHttpClientInternal(baseUrl, null, additionalHeaders, null);

        public static HttpClient CreateHttpClient(
            Uri? baseUrl,
            IDictionary<string, string>? additionalHeaders,
            IProgress<double>? progress
        ) => CreateHttpClientInternal(baseUrl, null, additionalHeaders, progress);

        public static HttpClient CreateHttpClient(
            Uri? baseUrl,
            Action<HttpClientHandler>? configure,
            IDictionary<string, string>? additionalHeaders
        ) => CreateHttpClientInternal(baseUrl, configure, additionalHeaders, null);

        public static HttpClient CreateHttpClient(
            Uri? baseUrl,
            Action<HttpClientHandler>? configure,
            IDictionary<string, string>? additionalHeaders,
            IProgress<double>? progress
        ) => CreateHttpClientInternal(baseUrl, configure, additionalHeaders, progress);

        private static HttpClient CreateHttpClientInternal(
            Uri? baseUrl,
            Action<HttpClientHandler>? configure,
            IDictionary<string, string>? additionalHeaders,
            IProgress<double>? progress
        )
        {
            HttpMessageHandler usedHandler;

            var defaultHandler = new HttpClientHandler()
            {
                UseProxy = false,
                AllowAutoRedirect = true,
                ServerCertificateCustomValidationCallback =
                    CertificateHelper.ValidateServerCallbacks,
            };
            configure?.Invoke(defaultHandler);
            usedHandler = defaultHandler;

            if (progress != null)
            {
                var progressHandler = new ProgressMessageHandler(defaultHandler);
                progressHandler.HttpSendProgress += (_, args) =>
                    progress.Report(args.ProgressPercentage);
                progressHandler.HttpReceiveProgress += (_, args) =>
                    progress.Report(args.ProgressPercentage);
                usedHandler = progressHandler;
            }

            return BuildHttpClient(usedHandler, baseUrl, additionalHeaders);
        }

        private static HttpClient BuildHttpClient(
            HttpMessageHandler handler,
            Uri baseUrl,
            IDictionary<string, string> additionalHeaders
        )
        {
            var client = new HttpClient(handler, true);
            ConfigureHttpClient(client, baseUrl, additionalHeaders);
            return client;
        }

        private static void ConfigureHttpClient(
            HttpClient client,
            Uri? baseUrl,
            IDictionary<string, string>? additionalHeaders
        )
        {
            if (baseUrl != null)
                client.BaseAddress = baseUrl;

            string? userAgent = string.Empty;
            string? appPrefix = SlimHost.Instance.AppPrefix;
            string? appVersion = SlimHost.Instance.AppVersion;

            if (!string.IsNullOrEmpty(appPrefix) && !string.IsNullOrEmpty(appVersion))
            {
                userAgent = $"{appPrefix.ToUpper()} {appVersion}";
            }

            if (!string.IsNullOrEmpty(userAgent))
            {
                client.DefaultRequestHeaders.UserAgent.TryParseAdd(userAgent);
            }

            if (additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }
    }
}
