using System;
using System.Collections.Generic;
using System.Linq;
using ToDos.Models;

namespace ToDos.Rules
{
    internal class ToDoReminderSelector
    {
        private const string unknownToDoReminderFrequencyType = "Unknown to do reminder frequency type";

        public ToDoReminderSelector()
        {
        }

        public List<ToDoReminder> GetToDoRemindersDueToday(string userName, DateTime todaysDate)
        {
            List<ToDoReminder> toDoReminders = new List<ToDoReminder>();
            foreach (ToDoReminder toDoReminder in GetToReminders(userName))
            {
                if (IsToDoReminderDueToday(toDoReminder, todaysDate))
                {
                    toDoReminders.Add(toDoReminder);
                }
            }

            return toDoReminders;
        }

        private bool IsToDoReminderDueToday(ToDoReminder toDoReminder, DateTime todaysDate)
        {
            DateTime toDoReminderDueDate = toDoReminder.FirstReminderDate;
            while (toDoReminderDueDate < todaysDate)
            {
                toDoReminderDueDate = GetNextReminderDueDate(toDoReminderDueDate, toDoReminder.FrequencyType);
            }

            return toDoReminderDueDate.Date == todaysDate.Date;
        }

        private DateTime GetNextReminderDueDate(DateTime toDoReminderDueDate, string frequencyType)
        {
            DateTime nextReminderDueDate = toDoReminderDueDate;
            switch (frequencyType)
            {
                case nameof(FrequencyType.Daily):
                    nextReminderDueDate = nextReminderDueDate.AddDays(1);
                    break;
                case nameof(FrequencyType.Weekly):
                    nextReminderDueDate = nextReminderDueDate.AddDays(7);
                    break;
                case nameof(FrequencyType.Monthly):
                    nextReminderDueDate = nextReminderDueDate.AddMonths(1);
                    break;
                case nameof(FrequencyType.Yearly):
                    nextReminderDueDate = nextReminderDueDate.AddYears(1);
                    break;
                default:
                    throw new ArgumentException(unknownToDoReminderFrequencyType);
            }

            return nextReminderDueDate;
        }

        private List<ToDoReminder> GetToReminders(string userName)
        {
            List<ToDoReminder> toDoReminders = new List<ToDoReminder>();

            foreach(ToDoReminder toDoReminder in ToDoDBContextFactory.Create().ToDoReminders)
            {
                if(toDoReminder.ToDo != null && toDoReminder.ToDo.UserName == userName)
                {
                    toDoReminders.Add(toDoReminder);
                }
            }
            return toDoReminders;
        }

        public ToDoReminder GetToDoReminder(int toDoReminderID)
        {
            return ToDoDBContextFactory.Create().ToDoReminders.Where(toDoReminder => toDoReminder.ID == toDoReminderID).FirstOrDefault();
        }
    }
}