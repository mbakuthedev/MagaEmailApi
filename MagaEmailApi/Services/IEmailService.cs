namespace MagaEmailApi.Services
{
    public interface IEmailService
    {
        Task<EmailResponse> SendAsync(EmailDetails emailDetails)
    }
}
