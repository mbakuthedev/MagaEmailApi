using MagaEmailApi.Models;

namespace MagaEmailApi.Services
{
        /// <summary>
        /// The transaction based notifications throughout the application
        /// </summary>
        public static class TransactionNotifications
        {
            /// <summary>
            /// The DI instance of the <see cref="IEmailService"/>
            /// </summary>
            public static IEmailService EmailService => Framework.Service<IEmailService>();

            /// <summary>
            /// The DI instance of the <see cref="IConfiguration"/>
            /// </summary>
            public static IConfiguration Configuration => Framework.Service<IConfiguration>();

            /// <summary>
            /// The DI instance of the <see cref="ILogger"/>
            /// </summary>
            public static ILogger Logger => Framework.Service<ILogger>();

            public static async Task SendComplaintByMail(Microsoft.AspNetCore.Mvc.Controller controller, UserDetails details)
            {
                try
                {
                    // Process the html content
                    var htmlContent = await RenderViewAsync("~/Views/Email/UserDetailsTemplate.cshtml", reservationResult, controller);

                    // Set the email credentials
                    var emailCredentials = new EmailDetails
                    {
                        SenderEmail = Configuration["Email:Address"],
                        SenderName = Configuration["Email:Title"],
                        ReciepientEmail = Configuration["Email:Reciepient"],
                        Bcc = new List<string> { Configuration["Email:Address"] },
                        Subject = $"New Message",
                        Content = htmlContent
                    };

                    // Send the email
                    await EmailService.SendAsync(details);
                }
                catch (Exception ex)
                {
                    // Log the error
                    Logger.LogError($"Failed to Send Email: {ex.Message}");
                }
            }

       
        }
