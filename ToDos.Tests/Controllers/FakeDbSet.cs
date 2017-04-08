﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ToDos.Models;

namespace ToDos.Tests.Controllers
{
    internal class FakeDbSet<T> : IDbSet<ToDo>
    {
        public List<ToDo> ToDos { get; set; }

        public FakeDbSet()
        {
            ToDos = new List<ToDo>();
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
                return Expression.Constant(ToDos.AsQueryable());
            }
        }

        public ObservableCollection<ToDo> Local
        {
            get
            {
                ObservableCollection<ToDo> observableToDos = new ObservableCollection<ToDo>();
                foreach(ToDo toDo in ToDos)
                {
                    observableToDos.Add(toDo);
                }

                return observableToDos;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return null;
            }
        }

        public ToDo Add(ToDo entity)
        {
            ToDos.Add(entity);
            return entity;
        }

        public ToDo Attach(ToDo entity)
        {
            throw new NotImplementedException();
        }

        public ToDo Create()
        {
            throw new NotImplementedException();
        }

        public ToDo Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ToDo> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public ToDo Remove(ToDo entity)
        {
            throw new NotImplementedException();
        }

        TDerivedEntity IDbSet<ToDo>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}