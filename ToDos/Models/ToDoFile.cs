using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDos.Models
{
    public class ToDoFile
    {
        [Key]
        public int ID { get; set; }
        public int ToDoID { get; set; }
        [ForeignKey("ToDoID")]
        public virtual ToDo ToDo{get; set;}
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; } 
    }    
}