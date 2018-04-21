using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoSelector
    {
        public IOrderedQueryable<ToDo> GetSortedToDosByLoggedInUserName(string userName)
        {
            return ToDoDBContextFactory.Create().ToDos.
                          Where(toDo => toDo.UserName == userName).
                           OrderBy(toDo => toDo.WhenItWasDone).
                             ThenByDescending(toDo => toDo.ID);
        }

        public ToDo GetToDoByLoggedInUserName(int? id)
        {
            string userName = new LoggedInUserFinder().GetLoggedInUserName();
            return GetToDo(id, userName);
        }

        public ToDo GetToDo(int? id, string userName)
        {
            int searchableID = 0;

            if(id != null)
            {
                searchableID = (int)id;
            }

            ToDo toDo = ToDoDBContextFactory.Create().ToDos.
                           Include(allToDo => allToDo.ToDoFiles).
                            Where(toDoThatMatches => toDoThatMatches.ID == searchableID && 
                                    toDoThatMatches.UserName == userName).FirstOrDefault();
            

            if(toDo == null)
            {
                throw new Exception("User is not allowed to view this to do.");
            }

            if (toDo.ToDoFiles == null)
            {
                toDo.ToDoFiles = new List<ToDoFile>();
            }

            return toDo;            
        }

        public IQueryable<ToDo> GetToDosThatIsLikeWhatToDo(string whatToDoContainsThis, string userName)
        {
            var toDosFound = ToDoDBContextFactory.Create().
                ToDos.Where(toDo => toDo.WhatToDo.ToLower().
                               Contains(whatToDoContainsThis.ToLower()) && toDo.UserName == userName
                           );

            return toDosFound;
        }
    }
}
