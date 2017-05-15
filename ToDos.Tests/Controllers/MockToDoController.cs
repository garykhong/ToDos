using ToDos.Controllers;

namespace ToDos.Tests.Controllers
{
    internal class MockToDoController : ToDoController
    {
        protected override void ResetToDoDBContext()
        {

        }
    }
}