using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    [Table("ActivityLogger")]
    public class ActivityLogger
    {

        public ActivityLogger()
        {
        }


        [Key]
        public int ActivityLoggerId { get; set; }

        public string? ActivityPerformed { get; set; }

        public DateTime CreatedDateTime { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }


    }
}
