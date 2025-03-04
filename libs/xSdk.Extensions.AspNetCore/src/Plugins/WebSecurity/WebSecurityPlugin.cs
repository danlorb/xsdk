using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using xSdk.Extensions.Variable;
using xSdk.Hosting;

namespace xSdk.Plugins.WebSecurity
{
    public class WebSecurityPlugin : WebHostPluginBase
    {
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
            var securitySetup = SlimHost.Instance.VariableSystem.GetSetup<WebSecuritySetup>();

            if (securitySetup.IsCorsEnabled)
            {
                services.AddCors(cors =>
                    cors.AddDefaultPolicy(policy =>
                        policy
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            // .AllowAnyOrigin() // Dont activate, otherwise everybody can access the API
                            .WithOrigins(GetOrigins())
                            .WithExposedHeaders("Content-Disposition")
                    )
                );
            }

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });
        }

        public override void Configure(WebHostBuilderContext context, IApplicationBuilder app)
        {
            var securitySetup = SlimHost.Instance.VariableSystem.GetSetup<WebSecuritySetup>();
            var enableCors = securitySetup.IsCorsEnabled;

            PreBuild(app);

            var stage = SlimHost.Instance.VariableSystem.GetSetup<EnvironmentSetup>().Stage;
            if (stage == Stage.Development)
            {
                app.UseDeveloperExceptionPage();
            }

            Logger.Debug("Enable Cookie Policy");
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax, Secure = CookieSecurePolicy.Always });

            Build(app);

            app.UseStaticFiles();

            if (enableCors)
                app.UseCors();

            PostBuild(app);
        }

        private static void PreBuild(IApplicationBuilder app)
        {
            var fordwardedHeaderOptions = new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All };
            fordwardedHeaderOptions.KnownNetworks.Clear();
            fordwardedHeaderOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(fordwardedHeaderOptions);
        }

        private static void Build(IApplicationBuilder app)
        {
            app.UseHsts()
                .UseReferrerPolicy(_ =>
                {
                    _.NoReferrer();
                });
        }

        private void PostBuild(IApplicationBuilder app)
        {
            var origins = GetOrigins();

            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXContentTypeOptions();

            app.UseNoCacheHttpHeaders();
            app.UseXfo(options => options.Deny());
            app.UseCsp(options =>
            {
                options
                    .BlockAllMixedContent()
                    .BaseUris(_ =>
                    {
                        _.Self();
                        _.CustomSources = origins;
                    })
                    .ObjectSources(_ =>
                    {
                        _.None();
                    })
                    .Sandbox(_ =>
                    {
                        _.AllowForms().AllowSameOrigin().AllowScripts().AllowPopups().AllowModals();
                    })
                    .FrameSources(_ =>
                    {
                        _.Self();
                    })
                    .ConnectSources(_ =>
                    {
                        _.Enabled = true;
                        _.Self();
                        _.CustomSources = origins;
                    })
                    .ImageSources(_ =>
                    {
                        _.Enabled = true;
                        _.SelfSrc = true;
                        _.CustomSources = origins.Concat(new List<string> { "data:" });
                    })
                    .FontSources(_ =>
                    {
                        _.Enabled = true;
                        _.SelfSrc = true;
                        _.CustomSources = origins.Concat(new List<string> { "data:" });
                    })
                    .ScriptSources(_ =>
                    {
                        _.Enabled = true;
                        _.SelfSrc = true;
                        _.UnsafeInlineSrc = true;
                        _.UnsafeEvalSrc = true;
                        _.CustomSources = origins;
                    })
                    .StyleSources(_ =>
                    {
                        _.Enabled = true;
                        _.SelfSrc = true;
                        _.UnsafeInlineSrc = true;
                        _.CustomSources = origins;
                    })
                    .DefaultSources(_ =>
                    {
                        _.Enabled = true;
                        _.SelfSrc = true;
                        _.CustomSources = origins;
                    });
            });
        }

        private string[] GetOrigins()
        {
            var webSetup = SlimHost.Instance.VariableSystem.GetSetup<IWebHostSetup>();
            var securitySetup = SlimHost.Instance.VariableSystem.GetSetup<WebSecuritySetup>();

            IEnumerable<string> additionalOrigins = new List<string>();
            if (!string.IsNullOrEmpty(securitySetup.Origins))
            {
                var splittedOrigins = securitySetup.Origins
                    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                additionalOrigins = new List<string>(splittedOrigins);
            }

            IEnumerable<string> origins = new List<string>();
            if (additionalOrigins.Any())
                origins = origins.Concat(additionalOrigins);

            return origins.ToArray();
        }

        private static string CleanDomain(string domain)
        {
            return domain.Replace("http://", "").Replace("https://", "");
        }
    }
}
