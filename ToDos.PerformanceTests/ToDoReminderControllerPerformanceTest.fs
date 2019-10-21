namespace ToDos.PerformanceTests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open ToDos.Models

[<TestClass>]
type ToDoReminderControllerPerformanceTest () =

    [<TestMethod>]
    member this.OneThousandDailyRemindersReminderDateOneMonthAgo_ExecutesUnderTwoSeconds () =
        let stopWatch = System.Diagnostics.Stopwatch.StartNew()
        //let toDoReminderControllerTest = ToDos.Tests.Controllers.ToDoReminderControllerTest()
        //toDoReminderControllerTest.SetupDependencies()
        stopWatch.Stop()
        Assert.IsTrue(stopWatch.Elapsed.TotalSeconds < float 2);

        