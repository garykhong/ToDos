using System;
using ToDos.Models;
using ToDos.Rules;

namespace ToDos.Tests.Controllers
{
    public class FakeToDoDBContextSelector
    {
        public const string GaryAtAbcEmailAddress = "gary@abc.com";

        internal FakeToDoDBContext GetFakeToDoDBContextAfterSettingToDoDBContext()
        {
            FakeToDoDBContext fakeToDoDBContext = new FakeToDoDBContext();
            fakeToDoDBContext.ToDos.Add(GetFirstToDo());
            fakeToDoDBContext.ToDos.Add(new ToDo
            {
                ID = 2,
                WhatToDo = "Cook Rice",
                OrderID = 2,
                UserName = GaryAtAbcEmailAddress
            });
            fakeToDoDBContext.ToDos.Add(new ToDo
            {
                ID = 3,
                WhatToDo = "Buy baby groot",
                WhenItWasDone = new DateTime(2012, 10, 12),
                OrderID = 3,
                UserName = GaryAtAbcEmailAddress
            });
            fakeToDoDBContext.ToDos.Add(new ToDo
            {
                ID = 4,
                WhatToDo = "Clean room",
                WhenItWasDone = new DateTime(2013, 10, 12),
                OrderID = 4,
                UserName = GaryAtAbcEmailAddress
            });

            fakeToDoDBContext.ToDoReminders.Add(new ToDoReminder
            {
                ToDoID = 1,
                ID = 1,
                FirstReminderDate = new DateTime(2019, 7, 30),
                FrequencyType = FrequencyType.Yearly.ToString(),
                IsActive = true,
                ToDo = GetFirstToDo()
            });

            fakeToDoDBContext.ToDoReminders.Add(new ToDoReminder
            {
                ToDoID = 1,
                ID = 2,
                FirstReminderDate = new DateTime(2019, 9, 26, 10, 30, 15),
                FrequencyType = FrequencyType.Yearly.ToString(),
                IsActive = true,
                ToDo = GetFirstToDo()
            });

            ToDoDBContextFactory.SetToDoDBContext(fakeToDoDBContext);

            return fakeToDoDBContext;
        }

        private static ToDo GetFirstToDo()
        {
            return new ToDo
            {
                ID = 1,
                WhatToDo = "Buy Groceries",
                OrderID = 1,
                UserName = GaryAtAbcEmailAddress
            };
        }
    }
}
