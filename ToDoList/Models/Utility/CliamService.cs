using System.Security.Claims;

namespace ToDoList.Models.Utility
{
    public class CliamService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;

        public CliamService(IHttpContextAccessor httpContextAccessor) { _httpContextAccessor = httpContextAccessor; }

        public int GetCurrentUserId()
        {
            var res = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x=>x.Type == ClaimTypes.PrimarySid)?.Value ?? string.Empty;
            int.TryParse(res, out int userId);
            return userId;
        }

        public string GetCurrentRole()=> _httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x=>x.Type == ClaimTypes.Role)?.Value ?? string.Empty;
    }
}
