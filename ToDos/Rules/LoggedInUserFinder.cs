using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ToDos.Rules
{
    public class LoggedInUserFinder : UserFinder
    {
        public string GetUserName()
        {
            string userName = HttpContext.Current == null ? null : HttpContext.Current.User.Identity.Name;
            return userName;
        }
    }

    public interface UserFinder
    {
        string GetUserName();
    }
}
