using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ToDos.Controllers.Api
{
    public class HttpApiController : ApiController
    {
        public HttpApiController(ApiController apiControllerThatReceivedRequest)
        {
            this.ApiControllerThatReceivedRequest = apiControllerThatReceivedRequest;
            this.RequestContext = apiControllerThatReceivedRequest.RequestContext;
            this.Request = this.ApiControllerThatReceivedRequest.Request;
            this.Configuration = this.ApiControllerThatReceivedRequest.Configuration;
        }

        public ApiController ApiControllerThatReceivedRequest { get; private set; }

        public IHttpActionResult CallGetFunction<T>(Func<T> functionToCall)
        {
            T result;

            try
            {
                result = functionToCall();

                if(result == null)
                {
                    return NotFound();
                }

                if(result is IEnumerable)
                {
                    if(((ICollection)result).Count == 0)
                    {
                        return NotFound();
                    }
                }

                return Ok(result);
            }
            catch(Exception)
            {                
                return InternalServerError();
            }            
        }

        public IHttpActionResult CallPostAction<T>(Action methodToCall, T dataToSave)
        {          
            try
            {
                methodToCall();
                // after the method is called this will automatically populate the id
                // on dataToSave.
                return Created(GetInsertedDataObjectUri<T>(dataToSave), dataToSave);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        private int GetIDFromDataToSaveObject<T>(T dataToSave)
        {
            Type t = dataToSave.GetType();
            PropertyInfo prop = t.GetProperty("ID");
            int id = Convert.ToInt32(prop.GetValue(dataToSave));
            return id;
        }

        private string GetInsertedDataObjectUri<T>(T dataToSave)
        {
            return Request.RequestUri + "/" + GetIDFromDataToSaveObject<T>(dataToSave).ToString();
        }

        public IHttpActionResult CallDeleteAction(Action methodToCall)
        {
            try
            {
                methodToCall();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        public IHttpActionResult CallPutAction<T>(Action methodToCall, T dataToUpdate)
        {
            try
            {
                methodToCall();
                return Ok(dataToUpdate);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
