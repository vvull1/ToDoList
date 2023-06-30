using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    [Table("Messaging")]
    public class Messaging
    {
        public Messaging()
        {

        }

        [Key]
        public int MessageId { get; set; }
       public string Subject { get; set; }
        public int? ParentID { get; set; }
        public bool IsParent { get; set; } = false; 

        [Required]
        public string? Content { get; set; }
        [ForeignKey("FKSenderId")]
        public User User { get; set; }
        public int FKSenderId { get; set; }

        public int ReceiverId { get; set; }


        public DateTime SentTime { get; set; }

    }

}
