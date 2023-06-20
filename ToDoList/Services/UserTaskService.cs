using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models.DTO;
using ToDoList.Models;
using ToDoList.EfCore;
using ToDoList.Models.Utility;

namespace ToDoList.Services
{
    public class UserTaskService:IUserTaskService
    {
        private readonly ToDoContext _context;
       
        private readonly ILogger<UserTaskService> _logger;

        public UserTaskService(ToDoContext context, ILogger<UserTaskService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> CreateTask(CreateTaskDTO createTask)
        {
            try
            {
                foreach (var item in createTask.Tasks)
                {
                    var taskList = new TaskTable();

                    taskList.TaskName = item.TaskName;
                    taskList.DueDateTime = item.DueTime;
                    taskList.Status =item.TaskStatus;
                    taskList.FKCreatedByUserId = 3;
                    taskList.AssignedToUserId = 3;

                    _context.Tasks.Add(taskList);
                }
                //throw new Exception("Custom exception message");

                await _context.SaveChangesAsync();
                _logger.LogInformation("Task created successfully.");
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a task.");
                return new StatusCodeResult(500);
            }

        }

    }
}
