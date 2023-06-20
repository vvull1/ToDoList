namespace ToDoList.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? ErrorMessage { get; set; }
        public bool? IsError { get; set; }

    }
    
}
