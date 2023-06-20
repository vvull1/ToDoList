using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
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

        public TaskService(ToDoContext context, ILogger<TaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> CreateTask(AdminCreateTaskDTO createTask)
        {
            try
            {
                foreach (var item in createTask.Tasks)
                {
                    var taskList = new TaskTable
                    {
                        TaskName = item.TaskName,
                        DueDateTime = item.DueTime,
                        Status = item.TaskStatus,
                        FKCreatedByUserId = item.CreatedBy,
                        AssignedToUserId = item.AssignedTo,
                    };

                    var taskHistory = new TaskHistory
                    {
                        FKTaskAssignedByUserId = item.CreatedBy,
                        TaskAssignedToUserId = item.AssignedTo,
                        AssignedDateTime = item.DueTime,
                        FKTaskId = taskList.TaskId,

                    };

                    taskList.TaskHistorys.Add(taskHistory); 
                    await _context.AddAsync(taskList);
                }

                
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
            var updateTask = await _context.Tasks.Where(x => x.TaskId == taskDto.TaskId).FirstOrDefaultAsync();
            if (updateTask != null)
            {
                updateTask.DueDateTime = taskDto.DueTime;
                updateTask.Status = taskDto.TaskStatus;
                updateTask.AssignedToUserId = taskDto.AssignedTo;

                _context.Tasks.Update(updateTask);

            }
            var taskHistory = new TaskHistory
            {
                FKTaskAssignedByUserId = taskDto.CreatedBy,
                TaskAssignedToUserId = taskDto.AssignedTo,
                AssignedDateTime = taskDto.DueTime,
                FKTaskId = taskDto.TaskId,
            };

            updateTask.TaskHistorys.Add(taskHistory);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async Task<List<TaskTable>> GetAllTasks(StatusType taskStatus,int taskid)
        {
            return await _context.Tasks.WhereIf(taskStatus != null && taskStatus !=0, x=>x.Status == taskStatus)
                .WhereIf(taskid != null && taskid !=0, x=>x.TaskId == taskid).OrderByDescending(x=>x.DueDateTime).ToListAsync();   
        }

    }
}
               