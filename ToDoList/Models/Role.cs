using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    [Table("Role")]
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }

        [Key]
        public int RoleId { get; set; }

        [Required]
        public string? RoleName { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        [InverseProperty("Role")]
        public List<User> Users { get; set; }

    }
}
