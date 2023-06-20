using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.EfCore;
using ToDoList.Models.DTO;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ToDoContext _context;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(ToDoContext context, IAuthenticationService authenticationService)
        {
            _context = context;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("UserLogin")]
        public async Task<ActionResult> UserLogin([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var response = new LoginResponse();
                ////To authenticate user.
                //Check weather user is exist are not
                var userExist = await _context.Users.Include(x=>x.Role)
                    .Where(x => (x.Email.ToLower() == loginDTO.Email.ToLower()) && (x.IsActive))
                    .FirstOrDefaultAsync();

                if (userExist != null)
                {

                    var decryptPass = Helper.DecryptString("4e9f5a0824554525bbf35490d8da48f2", userExist.Password);
                    if (decryptPass != loginDTO.Password)
                    {
                        response.ErrorMessage += "Invaild Password";
                    }
                    else
                    {
                        TokenModel tokenModel = await _authenticationService.GenerateJWT(userExist.UserName, userExist.UserId,userExist.Role.RoleName);
                        response.ErrorMessage += null;
                        response.TokenModel = tokenModel;

                    }
                }
                else
                {
                    response.ErrorMessage += "User doesn't Exist";
                }

                return Ok(response);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
