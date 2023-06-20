using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Models.DTO;

namespace ToDoList.Services
{
    public interface IUserService
    {
        
        public Task<IActionResult> CreateUser(AddUserDTO userObj);
        Task<List<User>> GetAllUsers();
        Task<IActionResult> UpdateUser(int id, UserDTO updatedUserObj);
        public Task<UserDTO> GetUserById(int id);
    }
}
