namespace ToDoList.Models.DTO
{
    public class AdminCreateTaskDTO
    {
        public AdminCreateTaskDTO()
        {
            Tasks= new List<TaskDTO>();    
        }

        public List<TaskDTO> Tasks { get; set; }
    }

    public class TaskDTO
    {
        public string TaskName { get; set; }
        public StatusType TaskStatus { get; set; }
        public int? CreatedBy { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime DueTime { get; set; }
    }

    public class CreateTaskDTO
    {
        public CreateTaskDTO()
        {
            Tasks = new List<UserTaskDTO>();
        }

        public List<UserTaskDTO> Tasks { get; set; }
    }

    public class UserTaskDTO 
    {
        public string TaskName { get; set; }
        public DateTime DueTime { get; set; }
        public StatusType TaskStatus { get; set; }
    }
}
