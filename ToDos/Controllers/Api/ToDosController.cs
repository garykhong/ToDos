using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDos.Controllers.Attributes;
using ToDos.Models;
using ToDos.Rules;

namespace ToDos.Controllers.Api
{
    [RequireHttpsForRemoteRequest]
    [Authorize]
    public class ToDosController : ApiController
    {
        // GET: api/ToDos/5
        public IHttpActionResult Get(int id)
        {
            return new HttpApiController(this).
                 CallGetFunction<ToDo>(() => new ToDoSelector().GetToDoByLoggedInUserName(id));

        }

        // GET: api/ToDos
        public IHttpActionResult Get()
        {
            return new HttpApiController(this).
                CallGetFunction<List<ToDo>>(() => new ToDoSelector().
                                                       GetSortedToDosByLoggedInUserName(new LoggedInUserFinder().GetUserName()).
                                                        ToList());
        }

        // POST: api/ToDos
        public IHttpActionResult Post([FromBody]ToDo toDo)
        {
            return new HttpApiController(this).
                          CallPostAction<ToDo>(() => new ToDoInserter().
                                                             SaveToDoWithLoggedInUserName(toDo), toDo);            
        }

        // PUT: api/ToDos/5
        public IHttpActionResult Put(int id, [FromBody]ToDo toDo)
        {
            return new HttpApiController(this).
                            CallPutAction<ToDo>(() => new ToDoUpdater().
                                                           UpdateToDoWithResetToDoDBContext(toDo), toDo);            
        }

        // DELETE: api/ToDos/5
        public IHttpActionResult Delete(int id)
        {
            return new HttpApiController(this).
                          CallDeleteAction(() => new ToDoDeletor().DeleteToDo(id));            
        }
    }
}
