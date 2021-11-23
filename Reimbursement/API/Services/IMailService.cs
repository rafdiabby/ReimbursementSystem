using API.ViewModels;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestVM mailRequest);
    }
}
