using ToDoList.Models.DTO;

namespace ToDoList.Services
{
    public interface IAuthenticationService
    {
        Task<TokenModel> GenerateJWT(string Username, int UserId, string role);
    }
}
