namespace ToDoList.Models.DTO
{
    public class UpdateTaskDTO
    {

        public int TaskId { get; set; }
        public StatusType TaskStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? AssignedTo { get; set; }
        public string DueTime { get; set; }

    }
}
