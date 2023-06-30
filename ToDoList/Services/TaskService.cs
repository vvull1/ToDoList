using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Globalization;
using ToDoList.EfCore;
using ToDoList.Models;
using ToDoList.Models.DTO;
using ToDoList.Models.Utility;

namespace ToDoList.Services
{
    public class TaskService : ITaskService
    {
        private readonly ToDoContext _context;
        private readonly ILogger<TaskService> _logger;
        private readonly CliamService _cliamService;

        public TaskService(ToDoContext context, ILogger<TaskService> logger, CliamService cliamService)
        {
            _context = context;
            _logger = logger;
            _cliamService = cliamService;
        }

        public async Task<IActionResult> CreateTask(AdminTaskDTO createTask)
        {
            var loggedINUserID = _cliamService.GetCurrentUserId();
            var currentRole = _cliamService.GetCurrentRole();

            if (currentRole == "Admin")
            {
                var taskList = new TaskTable
                {
                    TaskName = createTask.TaskName,
                    DueDateTime = DateTime.ParseExact(createTask.DueTime, "MM-dd-yyyy", CultureInfo.InvariantCulture),
                    Status = createTask.TaskStatus,
                    FKCreatedByUserId = loggedINUserID,
                    AssignedToUserId = createTask.AssignedTo,
                };

                var taskHistory = new TaskHistory
                {
                    FKTaskAssignedByUserId = loggedINUserID,
                    TaskAssignedToUserId = createTask.AssignedTo,
                    AssignedDateTime = DateTime.ParseExact(createTask.DueTime, "MM-dd-yyyy", CultureInfo.InvariantCulture),
                    FKTaskId = taskList.TaskId,
                };

                taskList.TaskHistorys.Add(taskHistory);
                await _context.AddAsync(taskList);
            }
            else
            {
                var usertaskList = new TaskTable();

                usertaskList.TaskName = createTask.TaskName;
                usertaskList.DueDateTime = DateTime.ParseExact(createTask.DueTime, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                usertaskList.Status = createTask.TaskStatus;
                usertaskList.FKCreatedByUserId = loggedINUserID;
                usertaskList.AssignedToUserId = loggedINUserID;

                var taskHistory = new TaskHistory
                {
                    FKTaskAssignedByUserId = loggedINUserID,
                    TaskAssignedToUserId = createTask.AssignedTo,
                    AssignedDateTime = DateTime.ParseExact(createTask.DueTime, "MM-dd-yyyy", CultureInfo.InvariantCulture),
                    FKTaskId = usertaskList.TaskId,
                };

                usertaskList.TaskHistorys.Add(taskHistory);
                _context.Tasks.Add(usertaskList);
            }

            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<List<TaskListDTO>> GetTaskListByUserId(int userId)
        {
            return await _context.Tasks.Where(x => x.AssignedToUserId == userId)
                .Select(y => new TaskListDTO
                {
                    TaskName = y.TaskName,
                    DueTime = y.DueDateTime,
                    AssignedTo = y.AssignedToUserId,
                    TaskStatus = y.Status.ToString(),
                    CreatedBy = y.FKCreatedByUserId,
                    TaskId = y.TaskId
                }).ToListAsync();
        }
        public async Task<IActionResult> UpdateTask(UpdateTaskDTO taskDto)
        {
            var loggedINUserID = _cliamService.GetCurrentUserId();
            var updateTask = await _context.Tasks.Where(x => x.TaskId == taskDto.TaskId).FirstOrDefaultAsync();
            var userData = await _context.Users.Include(x => x.Role).Where(x => x.UserId == updateTask.FKCreatedByUserId).FirstOrDefaultAsync();

            if (updateTask != null && userData.Role.RoleName == "Admin")
            {
                updateTask.DueDateTime = DateTime.ParseExact(taskDto.DueTime, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                updateTask.Status = taskDto.TaskStatus;
                updateTask.AssignedToUserId = taskDto.AssignedTo;

                _context.Tasks.Update(updateTask);
            }
            else if(updateTask != null && userData.Role.RoleName == "user")
            {
                updateTask.DueDateTime = DateTime.ParseExact(taskDto.DueTime, "MM-dd-yyyy", CultureInfo.InvariantCulture);
                updateTask.Status = taskDto.TaskStatus;

                _context.Tasks.Update(updateTask);
            }
            var taskHistory = new TaskHistory
            {
                FKTaskAssignedByUserId = loggedINUserID,
                TaskAssignedToUserId = taskDto.AssignedTo,
                AssignedDateTime = DateTime.ParseExact(taskDto.DueTime, "MM-dd-yyyy", CultureInfo.InvariantCulture),
                FKTaskId = taskDto.TaskId,
            };

            updateTask.TaskHistorys.Add(taskHistory);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<List<TaskTable>> GetAllTasks(StatusType taskStatus, int taskid)
        {
            return await _context.Tasks.WhereIf(taskStatus != null && taskStatus != 0, x => x.Status == taskStatus)
                .WhereIf(taskid != null && taskid != 0, x => x.TaskId == taskid).OrderByDescending(x => x.DueDateTime).ToListAsync();
        }

    }
}
