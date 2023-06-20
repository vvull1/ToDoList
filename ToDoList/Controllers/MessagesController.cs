using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using ToDoList.Models;
using ToDoList.Models.DTO;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageSender;
        private readonly IExceptionLoggerService _exceptionLoggerService;

        public MessagesController(IMessageService messageSender, IExceptionLoggerService exceptionLoggerService)
        {
            _messageSender = messageSender;
            _exceptionLoggerService = exceptionLoggerService;
        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<string> SendMessage([FromBody] SendMessageModel request)
        {
            try
            {
                var res = await _messageSender.SendMessage(request);
                return res;
            }
            catch(Exception ex)
            {
                return await _exceptionLoggerService.ExpectionLogger("SendMessage", "MessagesController", ex.Message);
            }
           
        }

        [HttpGet]
        [Route("GetMessages")]
        public async Task<List<Messaging>> GetMessages(int userId)
        {
            try
            {
                return await _messageSender.GetMessages(userId);
            }
            catch (Exception ex)
            {
               await _exceptionLoggerService.ExpectionLogger("GetMessages", "MessagesController", ex.Message);
                return null;
            }

        }

        [HttpGet]
        [Route("GetLastMessage")]
        public async Task<List<Messaging>> GetLastMessage(int userId)
        {
            try
            {
                return await _messageSender.GetLastMessage(userId);
            }
            catch(Exception ex)
            {
                await _exceptionLoggerService.ExpectionLogger("GetLastMessage", "MessagesController", ex.Message);
                return null;
            }
        }       

    }
}
