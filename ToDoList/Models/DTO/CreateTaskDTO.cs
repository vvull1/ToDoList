namespace ToDoList.Models.DTO
{
    //public class AdminCreateTaskDTO
    //{
    //    public AdminCreateTaskDTO()
    //    {
    //        Tasks= new List<TaskDTO>();    
    //    }

    //    public List<TaskDTO> Tasks { get; set; }
    //}

    public class AdminTaskDTO
    {
        public string TaskName { get; set; }
        public StatusType TaskStatus { get; set; }
        public int? AssignedTo { get; set; }
        public string DueTime { get; set; }
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
        public string DueTime { get; set; }
        public StatusType TaskStatus { get; set; }
    }
}
