namespace ToDos.PerformanceTests

open Microsoft.VisualStudio.TestTools.UnitTesting


[<TestClass>]
type ToDoReminderControllerPerformanceTest () =
    let toDoReminderControllerTest = ToDos.Tests.Controllers.ToDoReminderControllerTest()

    [<TestMethod>]
    member this.OneThousandDailyRemindersReminderDateOneMonthAgo_ExecutesUnderThreeSeconds () =        
        let toDoReminderControllerPerformanceTester = ToDoReminderControllerPerformanceTester(toDoReminderControllerTest, 
                                                                                                toDoReminderControllerTest.DueDateForFirstToDo_MovesFirstToDoToLast, 
                                                                                                3, 
                                                                                                1000)
        toDoReminderControllerPerformanceTester.CallFunctionRepeatedlyAndTestTimeSpent()


    [<TestMethod>]
    member this.OneThousandMoveDueToDosToLastPriorityWithNull_DoesNothing_ExecutesUnderTwoSeconds() =
        let toDoReminderControllerPerformanceTester = ToDoReminderControllerPerformanceTester(toDoReminderControllerTest, 
                                                                                              toDoReminderControllerTest.MoveDueToDosToLastPriorityWithNull_DoesNothing, 
                                                                                              2, 
                                                                                              1000)
        toDoReminderControllerPerformanceTester.CallFunctionRepeatedlyAndTestTimeSpent()


    [<TestMethod>]
    member this.OneThousandMoveDueToDosToLastPriorityWithEmptyString_DoesNothing_ExecutesUnderTwoSeconds() =                
        let toDoReminderControllerPerformanceTester = ToDoReminderControllerPerformanceTester(toDoReminderControllerTest, 
                                                          toDoReminderControllerTest.MoveDueToDosToLastPriorityWithEmptyString_DoesNothing, 
                                                          2, 
                                                          1000)
        toDoReminderControllerPerformanceTester.CallFunctionRepeatedlyAndTestTimeSpent()
        




        