using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDos.Models;
using ToDos.Rules;

namespace ToDos.Controllers.Api
{
    public class ToDosController : ApiController
    {
        // GET: api/ToDos/5
        public IHttpActionResult Get(int id, string userName)
        {
            return Ok(new ToDoSelector().GetToDo(id, userName));
        }

        // GET: api/ToDos?userName='jeff'
        public IHttpActionResult Get(string userName)
        {
            return Ok(new ToDoSelector().GetSortedToDosByLoggedInUserName(userName).ToList());
        }

        // POST: api/ToDos
        public void Post([FromBody]ToDo toDo)
        {
            new ToDoInserter().SaveToDoWithLoggedInUserName(toDo);
        }

        // PUT: api/ToDos/5
        public void Put(int id, [FromBody]ToDo toDo)
        {
            new ToDoDBContextResetter().ResetToDoDBContext();
            new ToDoUpdater().UpdateToDo(toDo);
        }

        // DELETE: api/ToDos/5
        public void Delete(int id)
        {
            new ToDoDeletor().DeleteToDo(id);
        }
    }
}
