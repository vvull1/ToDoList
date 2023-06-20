using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.DTO;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly IUserTaskService _userCreateTaskService;
        private readonly IExceptionLoggerService _exceptionLoggerService;

        public UserTaskController(IUserTaskService userCreateTaskService, IExceptionLoggerService exceptionLoggerService)
        {
            _userCreateTaskService = userCreateTaskService;
            _exceptionLoggerService = exceptionLoggerService;
        }

        [HttpPost]
        [Route("UserCreateTask")]
        public async Task<IActionResult> CreateTask(CreateTaskDTO createTask)
        {
            var result = await _userCreateTaskService.CreateTask(createTask);
            return result;
        }
    }
}
