using Mailjet.Client;
using System.Net.Mail;
using System.Net;
using MagaEmailApi.Services;

namespace MagaEmailApi.Extensions
{
    public static class EmailExtension
    {
        /// <summary>
        /// Adds the MailJet email service to DI container
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="apiKey">MailJet API key</param>
        /// <param name="apiSecret">MailJet API secret</param>
        /// <returns>The <see cref="IServiceCollection"/> for further chaining</returns>
        /// TODO:Add the mailjet apikey and secret
        public static IServiceCollection AddMailJetEmailService(this IServiceCollection services, string apiKey, string apiSecret)
        {
            // Add the MailJet client to DI
            services.AddTransient(provider => new MailjetClient(apiKey, apiSecret));

            // Add the MailJet email service to DI
          //  services.AddTransient<IEmailService, MailJetEmailService>();

            // Return services for further chaining
            return services;
        }
        public static IServiceCollection ConfigureMailJet(this IServiceCollection services,  IConfiguration configuration)
        {
            services.Configure<MailJetSettings>(configuration.GetSection("MailJtSettings"));

            return services;
        }
    }
}

