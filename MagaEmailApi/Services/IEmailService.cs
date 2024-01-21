using MagaEmailApi.Models;

namespace MagaEmailApi.Services
{
    public interface IEmailService
    {
        Task<EmailResponse> SendAsync(UserDetails details);
    }
}
