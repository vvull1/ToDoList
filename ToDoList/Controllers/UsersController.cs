using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using ToDoList.Models;
using ToDoList.Models.DTO;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IExceptionLoggerService _exceptionLoggerService;
        private readonly ILogger<UsersController> _logger;


        public UsersController(IUserService userService, IExceptionLoggerService exceptionLoggerService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _exceptionLoggerService = exceptionLoggerService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(AddUserDTO userObj)
        {
            try
            {
                throw new Exception("While creating user");
                var result = await _userService.CreateUser(userObj);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
               // await _exceptionLoggerService.ExpectionLogger("CreatedUser", "UsersController", ex.Message); //here it's hitting middleware also
                return null;
            }

        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                //throw new Exception("Error getting getallusers");
                var result = await _userService.GetAllUsers();

                return result;
            }
            catch (Exception ex)
            {
                await _exceptionLoggerService.ExpectionLogger("GetAllUsers", "UsersController", ex.Message);  //here its hitting only service class
                return null;
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO updatedUserObj)
        {
            try
            {
             //   throw new Exception("Error while updating users");
                var result = await _userService.UpdateUser(id, updatedUserObj);
                return result;
            }
            catch (Exception ex)
            {
                await _exceptionLoggerService.ExpectionLogger("UpdateUser", "UsersController", ex.Message);   //here it's hitting both service and middleware
                return null;
            }

        }

        [HttpGet("UserBy/{id}")]
        public async Task<UserDTO> GetUserById(int id)
        {
            try
            {
                //throw new Exception("Error getting when get user by id");
                return await _userService.GetUserById(id);
            }
            catch (Exception ex)
            {
                await _exceptionLoggerService.ExpectionLogger("GetUserById", "UsersController", ex.Message);  //here it's hitting only service class
                return null;
            }

        }

    }

}

