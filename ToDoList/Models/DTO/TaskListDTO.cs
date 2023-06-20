namespace ToDoList.Models.DTO
{
    public class TaskListDTO
    {
       
            public int TaskId { get; set; }
            public string TaskName { get; set; }
            public string TaskStatus { get; set; }
            public int? CreatedBy { get; set; }
            public int? AssignedTo { get; set; }
            public DateTime DueTime { get; set; }
       
    }
}
