using ToDos.Rules;

namespace ToDos.Tests.Controllers
{
    public class FakeLoggedInUserFinder : UserFinder
    {
        public string GetUserName()
        {
            return FakeToDoDBContextSelector.GaryAtAbcEmailAddress;
        }
    }
}
