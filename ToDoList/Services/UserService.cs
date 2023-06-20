using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.EfCore;
using ToDoList.Models;
using ToDoList.Models.DTO;

namespace ToDoList.Services
{
    public class UserService : IUserService
    {
        private readonly ToDoContext _context;
        private readonly IExceptionLoggerService _exceptionLoggerService;

        public UserService(ToDoContext context, IExceptionLoggerService exceptionLoggerService)
        {
            _context = context;
            _exceptionLoggerService = exceptionLoggerService;   
        }

        public async Task<IActionResult> CreateUser(AddUserDTO userObj)
        {
                var user = new User();
                user.UserName = userObj.UserName;
                user.Email = userObj.Email;
                user.Password = Helper.EncryptString("4e9f5a0824554525bbf35490d8da48f2", userObj.Password);
                user.FKRoleId=userObj.FKRoleId;
                user.IsActive = true;
               
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return new OkResult();

        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IActionResult> UpdateUser(int id, UserDTO updatedUserObj)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                if (user == null)
                {
                    return new NotFoundResult();
                }

                user.UserName = updatedUserObj.UserName;
                user.Email = updatedUserObj.Email;

                _context.Users.Update(user);

                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            try
            {
                return await _context.Users.Where(x => x.UserId == id)
                    .Select(y => new UserDTO
                    {
                        UserId = y.UserId,
                        UserName = y.UserName,
                        Email = y.Email,
                        ErrorMessage = null,
                        IsError = false

                    }).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                await _exceptionLoggerService.ExpectionLogger("GetUserById", "UsersController", ex.Message);
                return new UserDTO { IsError = true, ErrorMessage = ex.Message };

            }
        }

    }
}
