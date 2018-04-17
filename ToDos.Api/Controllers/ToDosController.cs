using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDos.Rules;
using ToDos.Models;

namespace ToDos.Api.Controllers
{
    public class ToDosController : ApiController
    {
        // GET api/todos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/todos/5
        public IOrderedQueryable<ToDo> Get(int id)
        {
            return new ToDoSelector().GetSortedToDosByLoggedInUserName();
        }

        // POST api/todos
        public void Post([FromBody]string value)
        {
        }

        // PUT api/todos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/todos/5
        public void Delete(int id)
        {
        }
    }
}
