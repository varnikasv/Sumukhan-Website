using System.Threading.Tasks;

namespace Sumukha.Data
{
    public interface IEmailService
    {
        Task<bool> SendNotificationAsync(string Name, string Email, string Subject, string Message);
    }
}
