using healthcare_visuzlier25.Config;
using healthcare_visuzlier25.Crypto;
using healthcare_visuzlier25.Middleware;
using healthcare_visuzlier25.Session;
using healthcare_visuzlier25.Storage;

namespace healthcare_visuzlier25.Registration
{
    public static class Startup
    {
        public static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var config = hostContext.Configuration;
            services.AddConfigCustom<SessionCryptoConfig>(config, "SessionCryptoConfig")
                .AddConfigCustom<BlobConfig>(config, "BlobConfig");

            services.AddScoped<ISessionService, SessionService>();
            services.AddSingleton<ICryptoProvider, InMemoryCrypto>();
            services.AddScoped<IFileService, AzureBlobFileService>();
            services.AddAuthentication(options =>
            {
                //options.DefaultScheme
                //    = new Middleware.SessionValidatorSchemeOptions();
            })
          .AddScheme<SessionValidatorSchemeOptions, SessionValidator>(Constants.SessionAuth, op => { });

        }
        public static void ConfigureAppConfiguration(HostBuilderContext hostContext, IConfigurationBuilder configurationBuilder)
        {
            // Adding extra configuration files is done here
            if (File.Exists(Path.Combine(hostContext.HostingEnvironment.ContentRootPath, "secrets.json")))
            {
                configurationBuilder.SetBasePath(hostContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("secrets.json");
            }
        }

        public static IServiceCollection AddConfigCustom<T>(this IServiceCollection services, IConfiguration config, string sectionName)
            where T : class
        {
            services.AddOptions<T>()
                   .Bind(config.GetSection(sectionName), binderOptions =>
                   {
                       binderOptions.ErrorOnUnknownConfiguration = true;
                   })
               .ValidateDataAnnotations()
               .ValidateOnStart();
            return services;
        }
    }
}
