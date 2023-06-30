using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ToDoList.EfCore;
using ToDoList.Models;
using ToDoList.Models.DTO;

namespace ToDoList.Services
{
    public class MessageService : IMessageService
    {
        private readonly ToDoContext _context;
        private readonly ILogger<IMessageService> _logger;
        public MessageService(ToDoContext context,ILogger<IMessageService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SendMessage([FromBody]SendMessageModel request)
        {
            var sender = await _context.Users.FindAsync(request.SenderId);
            var receiver = await _context.Users.FindAsync(request.ReceiverId);

            if (sender == null || receiver == null)
            {
                return;
            }
            
            var msgfromto = await _context.Messagings.Where(x=> x.FKSenderId== request.SenderId && x.ReceiverId == request.ReceiverId).OrderBy(x=>x.SentTime).ToListAsync();
            var msgtofrom = await _context.Messagings.Where(x=> x.FKSenderId== request.ReceiverId && x.ReceiverId == request.SenderId).OrderBy(x => x.SentTime).ToListAsync();

            if ((msgfromto ==null && msgtofrom ==null) || (msgfromto.Count == 0 && msgtofrom.Count == 0)) 
            {

                var message1 = new Messaging
                {
                    Content = request.Content,
                    FKSenderId = sender.UserId,
                    ReceiverId = receiver.UserId,
                    Subject= request.Subject,
                    SentTime = DateTime.UtcNow,
                    IsParent = true

                };
                _context.Messagings.Add(message1);

            }
            else
            {
                var final = new List<Messaging>();
                final.AddRange(msgfromto); //addRange contains elements from both the list
                final.AddRange(msgtofrom);
                //final = (List<Messaging>)final.OrderBy(x => x.SentTime);
                var parentid = final.Where(x => x.IsParent).FirstOrDefault().MessageId;
                //It retrieves the parent message Id
                var message2 = new Messaging
                {
                    Content = final[final.Count-1].Content + " ~ " +request.Content,
                    FKSenderId = sender.UserId,
                    ReceiverId = receiver.UserId,
                    SentTime = DateTime.UtcNow,
                    IsParent = false,
                    ParentID= parentid,
                    Subject = request.Subject,  
                    
                };
                _context.Messagings.Add(message2);

            }

            await _context.SaveChangesAsync();

            return;
        }
        public async Task<List<Messaging>> GetMessages(int userId)
        {
            try
            {
                _logger.LogInformation("GetMessages successfully.");                
                return await _context.Messagings.Where(x => x.FKSenderId == userId || x.ReceiverId == userId).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting messages.");
                return new List<Messaging>(); 
                

            }
        }

        public async Task<List<Messaging>> GetLastMessage(int userId)
        {
            return await _context.Messagings
                .Where(x => x.FKSenderId == userId || x.ReceiverId == userId)
                .OrderByDescending(x => x.SentTime)
                .ToListAsync();
        }

    }
}
