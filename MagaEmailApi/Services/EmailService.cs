using MagaEmailApi.Models;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace MagaEmailApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailJetSettings _mailjetSettings;

        public EmailService(IOptions<MailJetSettings> settings)
        {
            _mailjetSettings = settings.Value;
        }

        public async Task<EmailResponse> SendAsync(UserDetails details)
        {
            // FIX, And work on endpoints
                var client = new MailjetClient(_mailjetSettings.ApiKey, _mailjetSettings.ApiSecret)
                {
                    
                    Version = ApiVersion.V3_1,
                };

                var request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }
                .Property(Send.Messages, new JArray {
            new JObject {
                {"From", new JObject {
                    {"Email", _mailjetSettings.SenderEmail},
                    {"Name", "Your Sender Name"}
                }},
                {"To", new JArray {
                    new JObject {
                        {"Email", _mailjetSettings.ReciepientEmail},
                        {"Name", "superuser"}
                    }
                }},
                {"Subject", "New Contact"},
                {"TextPart", $"Hello, Contact details:\n\nName: {details.Name}, \nEmail: {details.Email}, \nSubject: {details.Subject}, \nMessage: {details.Message}"}
            }
                });

                await client.PostAsync(request);

            return new EmailResponse
            {

            };
            }
        }
    }

