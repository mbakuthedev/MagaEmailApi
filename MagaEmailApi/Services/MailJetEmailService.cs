using MagaEmailApi.Models;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client;

namespace MagaEmailApi.Services
{
    public class MailJetEmailService : IEmailService
    {
        #region Private Members

        /// <summary>
        /// The scoped instance of the <see cref="MailjetClient"/> service
        /// </summary>
        private readonly MailjetClient _mailjetClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MailJetEmailService(MailjetClient mailjetClient)
        {
            _mailjetClient = mailjetClient;
        }

        #endregion

        /// <summary>
        /// Sends email based on provided details
        /// </summary>
        /// <param name="emailDetails">The provided email details</param>
        /// <returns></returns>
        public async Task<EmailResponse> SendAsync(EmailDetails emailDetails)
        {
            // Initialize Cc...
            var cC = new List<SendContact> { };

            // Initialize Cc...
            var bCc = new List<SendContact> { };

            // If we have cc...
            if (emailDetails.Cc.Count > 0)
            {
                // For each contact...
                foreach (var contact in emailDetails.Cc)
                {
                    // Add to cc collection
                    cC.Add(new SendContact(contact));
                }
            }

            // If we have bcc...
            if (emailDetails.Bcc.Count > 0)
            {
                // For each contact...
                foreach (var contact in emailDetails.Bcc)
                {
                    // Add to cc collection
                    bCc.Add(new SendContact(contact));
                }
            }

            // Construct and build the email
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(emailDetails.SenderEmail, emailDetails.SenderName))
                .WithSubject(emailDetails.Subject)
                .WithHtmlPart(emailDetails.Content)
                .WithTo(new SendContact(emailDetails.ReciepientEmail))
                .WithCc(cC)
                .WithBcc(bCc)
                .Build();

            // Invoke API to send email
            var response = await _mailjetClient.SendTransactionalEmailAsync(email);

            // Return the email response
            return new EmailResponse
            {

            };
        }
    }
}
}
