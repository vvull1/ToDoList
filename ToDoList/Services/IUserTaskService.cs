using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.DTO;

namespace ToDoList.Services
{
    public interface IUserTaskService
    {
        public Task<IActionResult> CreateTask(UserTaskDTO createTask);
    }
}
