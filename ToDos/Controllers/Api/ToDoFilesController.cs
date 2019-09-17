using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ToDos.Controllers.Attributes;
using ToDos.Models;
using ToDos.Rules;

namespace ToDos.Controllers.Api
{
    [RequireHttpsForRemoteRequest]
    [Authorize]
    public class ToDoFilesController : ApiController
    {
        [Route("api/ToDos/{toDoID}/ToDoFiles/{toDoFileID}")]
        public IHttpActionResult Get(int toDoID, int toDoFileID)
        {
            return new HttpApiController(this).
                      CallGetFunction(() => new ToDoFileSelector().
                                                  GetToDoFile(toDoID, toDoFileID, 
                                                     new LoggedInUserFinder().GetUserName()));
        }

        public IHttpActionResult Post(int toDoID)
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;

            if (files != null && files.Count > 0)
            {
                HttpPostedFileBase postedFile = new HttpPostedFileWrapper(files[0]);
                ToDo toDoToUpdate = new ToDoSelector().GetToDoByLoggedInUserName(toDoID);
                ToDoFile toDoFileToInsert = new ToDoFileSelector().GetToDoFile(postedFile, toDoID);
                return new HttpApiController(this).
                               CallPostAction<ToDoFile>(() => new ToDoFileInserter().
                                                                   InsertFile(toDoToUpdate, toDoFileToInsert), 
                                                                               toDoFileToInsert);
            }

            return BadRequest();
        }

        public IHttpActionResult Delete(int toDoFileID)
        {
            return new HttpApiController(this).
                          CallDeleteAction(() => new ToDoFileDeletor().
                                                        DeleteToDoFileByLoggedInUserName(toDoFileID));            
        }
    }
}
