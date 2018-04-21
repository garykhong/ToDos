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
            ToDo toDoThatIsSaved = new ToDoSelector().GetToDo(toDoID, userName);
            return Ok(new ToDoFileSelector().GetToDoFile(toDoThatIsSaved, toDoFileID));
        }

        [Route("api/ToDoFiles/{toDoID}/{userName}")]
        public void Post(int toDoID, string userName)
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;

            if(files != null && files.Count > 0)
            {
                HttpPostedFileBase postedFile = new HttpPostedFileWrapper(files[0]);
                new ToDoFileInserter().InsertFile(postedFile, toDoID, userName);
            }            
        }

        public void Delete(int toDoFileID, int toDoID, string userName)
        {
            new ToDoFileDeletor().DeleteToDoFile(toDoFileID, toDoID, userName);
        }
    }
}
