using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ToDos.Models;
using ToDos.Rules;

namespace ToDos.Controllers.Api
{    
    public class ToDoFilesController : ApiController
    {
        [Route("api/ToDos/{toDoID}/{userName}/ToDoFiles/{toDoFileID}")]
        public IHttpActionResult Get(int toDoID, int toDoFileID, string userName)
        {
            return new HttpApiController(this).
                      CallGetFunction(() => new ToDoFileSelector().
                                                  GetToDoFile(toDoID, toDoFileID, userName));
        }

        [Route("api/ToDoFiles/{toDoID}/{userName}")]
        public IHttpActionResult Post(int toDoID, string userName)
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;

            if (files != null && files.Count > 0)
            {
                HttpPostedFileBase postedFile = new HttpPostedFileWrapper(files[0]);
                ToDo toDoToUpdate = new ToDoSelector().GetToDo(toDoID, userName);
                ToDoFile toDoFileToInsert = new ToDoFileSelector().GetToDoFile(postedFile, toDoID);
                return new HttpApiController(this).
                               CallPostAction<ToDoFile>(() => new ToDoFileInserter().
                                                                   InsertFile(toDoToUpdate, toDoFileToInsert), 
                                                                               toDoFileToInsert);
            }

            return BadRequest();
        }

        public IHttpActionResult Delete(int toDoFileID, int toDoID, string userName)
        {
            return new HttpApiController(this).
                          CallDeleteAction(() => new ToDoFileDeletor().
                                                        DeleteToDoFile(toDoFileID, toDoID, userName));            
        }
    }
}
