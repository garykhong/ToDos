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
        public const string UserIsNotAllowedToViewThisToDo = "User is not allowed to view this to do.";

        public IOrderedQueryable<ToDo> GetSortedToDosByLoggedInUserName(string userName)
        {
            return ToDoDBContextFactory.Create().ToDos.
                          Where(toDo => toDo.UserName == userName).
                           OrderBy(toDo => toDo.WhenItWasDone).
                             ThenByDescending(toDo => toDo.OrderID);
        }

        public List<ToDo> GetToDosDueToday(string userName, DateTime todaysDate)
        {
            List<ToDo> toDosDueToday = new List<ToDo>();
            foreach(ToDoReminder toDoReminder in new ToDoReminderSelector().GetToDoRemindersDueToday(userName, todaysDate))
            {
                toDosDueToday.Add(toDoReminder.ToDo);
            }

            return toDosDueToday;
        }

        public ToDo GetToDoByLoggedInUserName(int? id)
        {
            string userName = new LoggedInUserFinder().GetUserName();
            return GetToDo(id, userName);
        }

        public ToDo GetToDo(int? id, string userName)
        {
            int searchableID = 0;

            if(id.HasValue && id > 0)
            {
                searchableID = (int)id;
            }
            else
            {
                return new ToDo();
            }

            ToDo toDo = ToDoDBContextFactory.Create().ToDos.
                           Include(allToDo => allToDo.ToDoFiles).
                            Where(toDoThatMatches => toDoThatMatches.ID == searchableID && 
                                    toDoThatMatches.UserName == userName).FirstOrDefault();
            

            if(toDo == null)
            {
                throw new Exception(UserIsNotAllowedToViewThisToDo);
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

        public ToDo GetNextToDoThatIsHigherInPriority(int toDoOrderID, string userName)
        {
            ToDo nextToDoThatIsHigherInPriority = ToDoDBContextFactory.Create().
                ToDos.OrderByDescending(toDo => toDo.OrderID).Where(toDo => toDo.OrderID < toDoOrderID && 
                                                           toDo.UserName == userName &&
                                                           toDo.WhenItWasDone == null).FirstOrDefault();
            return nextToDoThatIsHigherInPriority == null ? new ToDo() : nextToDoThatIsHigherInPriority;
        }

        public ToDo GetNextToDoThatIsLowerInPriority(int toDoOrderID, string userName)
        {
            ToDo nextToDoThatIsLowerInPriority = ToDoDBContextFactory.Create().
                ToDos.OrderBy(toDo => toDo.OrderID).Where(toDo => toDo.OrderID > toDoOrderID &&
                                                           toDo.UserName == userName &&
                                                           toDo.WhenItWasDone == null).FirstOrDefault();
            return nextToDoThatIsLowerInPriority == null ? new ToDo() : nextToDoThatIsLowerInPriority;
        }

        public int GetMaxPlusOneToDoOrderID(string userName)
        {
            return GetMaxToDoOrderID(userName) + 1;
        }

        private int GetMaxToDoOrderID(string userName)
        {
            List<ToDo> toDos = ToDoDBContextFactory.Create().ToDos.Where(toDo => toDo.UserName == userName).ToList();
            return toDos.Count() > 0 ? toDos.Max(td => td.OrderID) : 0;
        }

        public ToDo GetToDoToUseAsModel(ToDo toDo)
        {
            ToDo toDoToUseAsModel = toDo;
            if (string.IsNullOrEmpty(toDo.UserName))
                toDoToUseAsModel = GetToDoByLoggedInUserName(toDo.ID);
            return toDoToUseAsModel;
        }
    }
}
