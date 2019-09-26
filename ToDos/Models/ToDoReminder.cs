using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using ToDos.Rules;
using System.Linq;

namespace ToDos.Models
{
    public class ToDoReminder
    {
        [Key]
        public int ID { get; set; }
        public int ToDoID { get; set; }
        [Display(Name = "First Reminder Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                       ApplyFormatInEditMode = true)]        
        public DateTime FirstReminderDate { get; set; }
        [Display(Name = "Frequency Type")]
        [Required]
        public string FrequencyType { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        private ToDo toDo = new ToDo();
        [ForeignKey("ToDoID")]
        public virtual ToDo ToDo
        {
            get
            {
                if (toDo.ID == 0 && ToDoID > 0)
                    return new ToDoSelector().GetToDoByLoggedInUserName(ToDoID);

                return toDo;
            }
            set
            {
                toDo = value;
            }
        }
    }
}