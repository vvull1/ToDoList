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

        public async Task<string> SendMessage([FromBody]SendMessageModel request)
        {
            var sender = await _context.Users.FindAsync(request.SenderId);
            var receiver = await _context.Users.FindAsync(request.ReceiverId);

            if (sender == null || receiver == null)
            {
                return "Sender or Receiver Not Found";
            }
            

            var message = new Messaging 
            { 
                Content  = request.Content,
                FKSenderId= sender.UserId, 
                ReceiverId= receiver.UserId,
                SendAt = DateTime.UtcNow,
                MsgUniqueId = Guid.NewGuid(),   
            };

            _context.Messagings.Add(message);   
            await _context.SaveChangesAsync();

            return message.MsgUniqueId.ToString();
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
                .OrderByDescending(x => x.SendAt)
                .ToListAsync();
        }

    }
}
