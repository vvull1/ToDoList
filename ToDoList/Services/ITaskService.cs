using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Models.DTO;

namespace ToDoList.Services
{
    public interface ITaskService
    {
        public Task<IActionResult> CreateTask(AdminTaskDTO createTask);
        Task<List<TaskListDTO>> GetTaskListByUserId(int userId);
        Task<IActionResult> UpdateTask(UpdateTaskDTO taskDto);
        Task<List<TaskTable>> GetAllTasks(StatusType taskStatus, int taskid);
    }
}