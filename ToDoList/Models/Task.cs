using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using ToDoList.Models.DTO;

namespace ToDoList.Models
{
    [Table("Task")]
    public class TaskTable
    {
        #region Constants
        public TaskTable()
        {
            TaskHistorys = new List<TaskHistory>();
        }
        #endregion

        #region Properties
        [Key]
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        
        public DateTime DueDateTime { get; set; }
        public StatusType? Status { get; set; }
        public int? AssignedToUserId { get; set; }
        #endregion

        #region Associations
        [ForeignKey("FKCreatedByUserId")]
        public User User { get; set; }
        public int? FKCreatedByUserId { get; set; }     
        [InverseProperty("TaskTable")]
        public List<TaskHistory> TaskHistorys { get; set; }
        #endregion
    }

}

