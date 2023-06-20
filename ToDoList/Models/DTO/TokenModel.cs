using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.DTO
{
    public class TokenModel
    {
       
       
            public string Token { get; set; }
            public DateTime DateExpires { get; set; }
    }

    public class LoginDTO
    {
            [Required]
            public string Email { get; set; }
            [Required]
            public string Password { get; set; }
    }

     public class LoginResponse
     {
            public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrWhiteSpace(ErrorMessage);
            public string ErrorMessage { get; set; }
            public TokenModel TokenModel { get; set; }
     }
    
}

