using System.Text.Json;

namespace ToDoList.Models.DTO
{
    public class GlobalErrorDTO
    {
        
       
            public int StatusCode { get; set; }
            public String Message { get; set; }

            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        
    }

}

