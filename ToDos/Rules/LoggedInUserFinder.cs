using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ToDos.Rules
{
    public class LoggedInUserFinder
    {
        public string GetLoggedInUserName()
        {
            string userName = HttpContext.Current == null ? null : HttpContext.Current.User.Identity.Name;
            return userName;
        }
    }
}
