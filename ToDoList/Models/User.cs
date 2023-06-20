using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using ToDoList.EfCore;

namespace ToDoList.Models
{
    [Table("User")]
    public class User
    {

        public User()
        {
            Message = new List<Messaging>();
            TaskHistory = new List<TaskHistory>();
            ActivityLogger = new List<ActivityLogger>();
            TaskTable = new List<TaskTable>();    
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("Role")]
        public int? FKRoleId { get; set; }
        public Role? Role { get; set; }

        [InverseProperty("User")]
        public IList<Messaging>? Message { get; set; }

        [InverseProperty("User")]
        public IList<TaskHistory>? TaskHistory { get; set; }
        [InverseProperty("User")]
        public IList<ActivityLogger>? ActivityLogger { get; set; }

        [InverseProperty("User")]
        public IList<TaskTable>? TaskTable { get; set; }
    }
}


