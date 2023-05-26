using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Controllers.Projects
{
    public partial class ProjectController : ControllerBase
    {
        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete([FromHeader] string accesstoken, Guid id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.Gone;

            try
            {
                new Project().DeleteProject(id, accesstoken);
            }
            catch (Exception err)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = err.Message;
            }
            return response;
        }
    }
}
