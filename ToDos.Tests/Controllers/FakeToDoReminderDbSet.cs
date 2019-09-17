using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ToDos.Models;

namespace ToDos.Tests.Controllers
{
    internal class FakeToDoReminderDbSet<T> : IDbSet<ToDoReminder>
    {
        public List<ToDoReminder> ToDoReminders { get; set; }

        public FakeToDoReminderDbSet()
        {
            ToDoReminders = new List<ToDoReminder>();
        }

        public Type ElementType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Expression Expression
        {
            get
            {
                return Expression.Constant(ToDoReminders.AsQueryable());
            }
        }

        public ObservableCollection<ToDoReminder> Local
        {
            get
            {
                ObservableCollection<ToDoReminder> observableToDos = new ObservableCollection<ToDoReminder>();
                foreach (ToDoReminder toDoReminder in ToDoReminders)
                {
                    observableToDos.Add(toDoReminder);
                }

                return observableToDos;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return ToDoReminders.AsQueryable().Provider;
            }
        }

        public ToDoReminder Add(ToDoReminder entity)
        {
            ToDoReminders.Add(entity);
            return entity;
        }

        public ToDoReminder Attach(ToDoReminder entity)
        {
            throw new NotImplementedException();
        }

        public ToDoReminder Create()
        {
            throw new NotImplementedException();
        }

        public ToDoReminder Find(params object[] keyValues)
        {
            return ToDoReminders.Where(toDo => toDo.ID == Convert.ToInt16(keyValues[0])).FirstOrDefault();
        }

        public IEnumerator<ToDoReminder> GetEnumerator()
        {
            return ToDoReminders.GetEnumerator();
        }

        public ToDoReminder Remove(ToDoReminder entity)
        {
            ToDoReminders.Remove(entity);
            return entity;
        }

        TDerivedEntity IDbSet<ToDoReminder>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}