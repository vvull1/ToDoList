using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Models.DTO;

namespace ToDoList.Services
{
    public interface IMessageService
    {
        public Task SendMessage([FromBody] SendMessageModel request);
        Task<List<Messaging>> GetMessages(int userId);
        Task<List<Messaging>> GetLastMessage(int userId);




    }
}
