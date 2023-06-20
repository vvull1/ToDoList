using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

    }
}
