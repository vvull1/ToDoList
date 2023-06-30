using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models.DTO;
using ToDoList.Models;
using ToDoList.EfCore;
using ToDoList.Models.Utility;
using System.Globalization;

namespace ToDoList.Services
{
    public class UserTaskService:IUserTaskService
    {
        private readonly ToDoContext _context;
        private readonly ILogger<UserTaskService> _logger;
        private readonly CliamService _claimService;

        public UserTaskService(ToDoContext context, ILogger<UserTaskService> logger, CliamService claimService)
        {
            _context = context;
            _logger = logger;
            _claimService = claimService;
        }
        public async Task<IActionResult> CreateTask(UserTaskDTO createTask)
        {
            try
            {
                var userId = _claimService.GetCurrentUserId(); // Get the current user's ID

                var taskList = new TaskTable();

                    taskList.TaskName = createTask.TaskName;
                    taskList.DueDateTime = DateTime.ParseExact(createTask.DueTime,"MM-dd-yyyy",CultureInfo.InvariantCulture);
                    taskList.Status = createTask.TaskStatus;
                    taskList.FKCreatedByUserId = userId;
                    taskList.AssignedToUserId = userId;

                    _context.Tasks.Add(taskList);
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
