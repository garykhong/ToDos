using System.ComponentModel.DataAnnotations;
using System;

namespace ToDos.Models
{
    public class ToDo
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "What to do") ]
        public string WhatToDo { get; set; }
        [Display(Name = "When it was done")]
        [DataType(DataType.DateTime)]
        public DateTime? WhenItWasDone { get; set; }
    }
}