namespace ToDoList.Models.DTO
{
    public class AddUserDTO
    {       
            public string UserName { get; set; }
            public string Email { get; set; }

            public string Password { get; set; }

            public int FKRoleId { get; set; }
        

    }
}
