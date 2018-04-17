using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDos.Models;

namespace ToDos.Rules
{
    public class ToDoDBContextResetter
    {
        public void ResetToDoDBContext()
        {
            ToDoDBContextFactory.SetToDoDBContext(new ToDoDBContext());
        }
    }
}
