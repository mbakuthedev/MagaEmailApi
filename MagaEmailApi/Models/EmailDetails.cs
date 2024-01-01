namespace MagaEmailApi.Models
{
    public class EmailDetails
    {
        /// <summary>
        /// The name of the sender
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// The email of the sender
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// The name of the receiver
        /// </summary>
        public string ReciepientName { get; set; }

        /// <summary>
        /// The email of the receiver
        /// </summary>
        public string ReciepientEmail { get; set; }

        /// <summary>
        /// The collection of cc reciepent
        /// </summary>
        public List<string> Cc { get; set; } = new List<string> { };

        /// <summary>
        /// The collection of bcc reciepent
        /// </summary>
        public List<string> Bcc { get; set; } = new List<string> { };

        /// <summary>
        /// The email subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The email body content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Indicates if the contents is a HTML email
        /// </summary>
        public bool IsHTML { get; set; }
    }
}
