namespace ToDos.PerformanceTests

open Microsoft.VisualStudio.TestTools.UnitTesting
open ToDos.Tests.Controllers

type ToDoReminderControllerPerformanceTester (toDoReminderControllerTest : ToDoReminderControllerTest, 
                                                toDoReminderControllerTestFunction, 
                                                maximumTimeToSpend, 
                                                numberOfTimesToCallFunction) =
    member this.toDoReminderControllerTestFunction = toDoReminderControllerTestFunction
    member this.maximumTimeToSpend = maximumTimeToSpend
    member this.numberOfTimesToCallFunction = numberOfTimesToCallFunction
    member this.CallFunctionRepeatedlyAndTestTimeSpent() =
        let stopWatch = System.Diagnostics.Stopwatch.StartNew()        
        toDoReminderControllerTest.SetupDependencies()
        for numberOfTimesCalled in 1..numberOfTimesToCallFunction do
            toDoReminderControllerTestFunction()
        stopWatch.Stop()
        Assert.IsTrue(stopWatch.Elapsed.TotalSeconds < float maximumTimeToSpend)

