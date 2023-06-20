using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Models.DTO;

namespace ToDoList.Services
{
    public class AuthenticationService:IAuthenticationService
    {
        public AuthenticationService() { }

        //To generate Token
        public async Task<TokenModel> GenerateJWT(string Username, int UserId,string role)
        {
            SymmetricSecurityKey scretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4e9f5a0824554525bbf35490d8da48f2"));
            SigningCredentials signingCredentials = new SigningCredentials(scretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.PrimarySid, UserId.ToString()),
            };

            DateTime tokenExpiresIn = DateTime.UtcNow.AddDays(1);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claim,
                expires: tokenExpiresIn,
                signingCredentials: signingCredentials);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            await Task.CompletedTask;
            return (new TokenModel { Token = tokenString, DateExpires = tokenExpiresIn });
        }

    }
}
