using System.ComponentModel.DataAnnotations;
using System;

namespace ToDos.Models
{
    public class ToDo
    {
        [Key]
        public int ID { get; set; }
        public string WhatToDo { get; set; }
        public DateTime? WhenItWasDone { get; set; }
    }
}