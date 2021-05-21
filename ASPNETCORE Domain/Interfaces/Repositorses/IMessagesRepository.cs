using System.Collections.Generic;

namespace ASPNET_CORE_Domain
{
    public interface IMessagesRepository
    {
        bool Add(MessageEntity message);
        bool Delate(MessageEntity message);
        List<MessageEntity> GetAll();
    }
}