using System.Threading.Tasks;

namespace Mpower.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
