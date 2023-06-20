using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    [Table("Logger")]
    public class LoggerTable
    {
        [Key]
        public int LoggerId { get; set; }
        public string? Exception { get; set; }
        public string? Service { get; set; }
        public string? Controller { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
