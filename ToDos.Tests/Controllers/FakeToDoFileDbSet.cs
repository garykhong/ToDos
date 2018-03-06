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
    internal class FakeToDoFileDbSet<T> : IDbSet<ToDoFile>
    {
        public List<ToDoFile> ToDoFiles { get; set; }

        public FakeToDoFileDbSet()
        {
            ToDoFiles = new List<ToDoFile>();
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
                return Expression.Constant(ToDoFiles.AsQueryable());
            }
        }

        public ObservableCollection<ToDoFile> Local
        {
            get
            {
                ObservableCollection<ToDoFile> observableToDos = new ObservableCollection<ToDoFile>();
                foreach (ToDoFile toDoFile in ToDoFiles)
                {
                    observableToDos.Add(toDoFile);
                }

                return observableToDos;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return ToDoFiles.AsQueryable().Provider;
            }
        }

        public ToDoFile Add(ToDoFile entity)
        {
            ToDoFiles.Add(entity);
            return entity;
        }

        public ToDoFile Attach(ToDoFile entity)
        {
            throw new NotImplementedException();
        }

        public ToDoFile Create()
        {
            throw new NotImplementedException();
        }

        public ToDoFile Find(params object[] keyValues)
        {
            return ToDoFiles.Where(toDo => toDo.ID == Convert.ToInt16(keyValues[0])).FirstOrDefault();
        }

        public IEnumerator<ToDoFile> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public ToDoFile Remove(ToDoFile entity)
        {
            ToDoFiles.Remove(entity);
            return entity;
        }

        TDerivedEntity IDbSet<ToDoFile>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}