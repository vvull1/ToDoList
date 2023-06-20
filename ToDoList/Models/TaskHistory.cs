using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    [Table("TaskHistory")]
    public class TaskHistory
    {

        public TaskHistory()
        {
           
        }

        
            [Key]
            public int TaskHistoryId { get; set; }

            [ForeignKey("FKTaskAssignedByUserId")]
            public User? User { get; set; }
            public int? FKTaskAssignedByUserId { get; set; }
            public int? TaskAssignedToUserId { get; set; }
            public DateTime AssignedDateTime { get; set; }


            [ForeignKey("TaskTable")]
            public int FKTaskId { get; set; }
            public TaskTable? TaskTable { get; set; }

            

            
        


    }
}
