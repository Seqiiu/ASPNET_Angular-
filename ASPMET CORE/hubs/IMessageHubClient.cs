using System.Threading.Tasks;

namespace ASPMET_CORE.hubs
{
    public interface IMessageHubClient
    {
        Task NewMessage();
    }
}
