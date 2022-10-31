using System.Threading;
using System.Threading.Tasks;

namespace ConvertVoiceToTextBot.Services
{
    public interface IFileHandler
    {
        Task Download(string fileId, CancellationToken ct);
        string Process(string param);
    }
}
