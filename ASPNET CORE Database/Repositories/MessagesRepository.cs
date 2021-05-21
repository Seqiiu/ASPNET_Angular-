using ASPNET_CORE_Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPNET_CORE_Database
{
    public class MessagesRepository : IMessagesRepository
    {
        private readonly ApplicationDbContext _DbContext;

        private DbSet<MessageEntity> Messages { get; set; }

        public MessagesRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            Messages = dbContext.Messages;
        }

        //public IEnumerable<MessageEntity> GetAll()
        //{
        //    return Messages;
        //}

        public List<MessageEntity> GetAll()
        {
            return Messages.ToList();
        }

        public bool Add(MessageEntity message)
        {
            Messages.Add(message);

            return _DbContext.SaveChanges() > 0;
        }

        public bool Delate(MessageEntity message)
        {
            Messages.Remove(message);
            return _DbContext.SaveChanges() > 0;
        }
    }
}
