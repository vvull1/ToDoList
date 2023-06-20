using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Models.DTO;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IExceptionLoggerService _exceptionLoggerService;

        public TasksController(ITaskService taskService, IExceptionLoggerService exceptionLoggerService)
        {
            _taskService = taskService;
            _exceptionLoggerService = exceptionLoggerService;
        }

        [HttpPost]
        [Route("AdminCreateTask")]
        public async Task<IActionResult> CreateTask(AdminCreateTaskDTO createTask)
        {
            try
            {
                var result = await _taskService.CreateTask(createTask);
                return result;
            }
            catch(Exception ex)
            {
                await _exceptionLoggerService.ExpectionLogger("AdminCreateTask", "TasksController", ex.Message);
                return null;
            }
            
        }

        [HttpGet]
        [Route("GetTaskListByUserId")]
        public async Task<List<TaskListDTO>> GetTaskListByUserId(int userId)
        {
            try
            {
                return await _taskService.GetTaskListByUserId(userId);
            }
            catch(Exception ex)
            {
                await _exceptionLoggerService.ExpectionLogger("GetTaskListByUserId", "TasksController", ex.Message);
                return null;
            }
        }

        [HttpGet]
        [Route("GetAllTasks")]
        public async Task<List<TaskTable>> GetAllTasks(StatusType taskStatus, int taskid)
        {
            try
            {
                return await _taskService.GetAllTasks(taskStatus, taskid);
            }
            catch(Exception ex)
            {
                await _exceptionLoggerService.ExpectionLogger("GetAllTasks", "TasksController", ex.Message);
                return null;
            }
        }

        [HttpPut]
        [Route("UpdateTask")]
        public async Task<IActionResult> UpdateTask(UpdateTaskDTO taskDto)
        {
            try
            {
                return await _taskService.UpdateTask(taskDto);
            }
            catch (Exception ex)
            {
                await _exceptionLoggerService.ExpectionLogger("UpdateTasks", "TasksController", ex.Message);
                return null;
            }
        }


    }
}
