﻿using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace ToDos.Models
{
    public class ToDo
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "What to do")]
        [Required(AllowEmptyStrings = false)]
        public string WhatToDo { get; set; }
        [Display(Name = "When it was done")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
                       ApplyFormatInEditMode = true)]
        public DateTime? WhenItWasDone { get; set; }
        public string UserName { get; set; }
        private ICollection<ToDoFile> toDoFiles;
        public virtual ICollection<ToDoFile> ToDoFiles { get; set; }
    }
}