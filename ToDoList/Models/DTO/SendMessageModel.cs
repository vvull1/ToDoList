namespace ToDoList.Models.DTO
{
    public class SendMessageModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }

}
