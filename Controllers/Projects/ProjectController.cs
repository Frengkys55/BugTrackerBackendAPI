using Microsoft.AspNetCore.Mvc;
using BugTrackerBackendAPI.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using System.Text;
using System.Collections.ObjectModel;
using Azure.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTrackerBackendAPI.Controllers.Projects
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        /// <summary>
        /// Get all available project for specific user
        /// </summary>
        /// <param name="accesstoken">Logged in user access token information</param>
        /// <returns></returns>
        // GET: api/<ProjectController>
        [HttpGet("GetProjects")]
        public IEnumerable<ShortProjectInfo> GetAllProjects([FromHeader] string accesstoken)
        {
            // TODO: Implmenent access token

            Guid userGuid = Guid.NewGuid();

            try
            {
                return new Project().GetProjectsList(userGuid);
            }
            catch (Exception err)
            {
                throw;
            }
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public Project GetProjectDetail([FromHeader] string accesstoken, [Required] Guid id)
        {
            // TODO: implement access token

            Project project = new Project();

            try
            {
                return project.GetProject(id);
            }
            catch (Exception err)
            {
                throw;
            }
        }

        /// <summary>
        /// Update existing information with the new information
        /// </summary>
        /// <param name="project">New information about the project to be updated</param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage UpdateProjectInformation([FromHeader] string accesstoken, [FromBody] Project project)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.Gone;

            try
            {
                project.UpdateProject(project);
            }
            catch (Exception err)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = err.Message;
            }
            return response;
        }

        /// <summary>
        /// Create new project for the user
        /// </summary>
        /// <param name="accesstoken">User access token</param>
        /// <param name="project">The information about the project to be created</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public HttpResponseMessage CreateNewProject([FromHeader] string accesstoken, [Required] [FromBody] Project project)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.Gone;

            try
            {
                project.CreateProject(project, Guid.NewGuid());
                response.StatusCode = System.Net.HttpStatusCode.Created;
            }
            catch (Exception err)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = err.Message;
            }
            return response;
        }

        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public HttpResponseMessage Delete([FromHeader] string accesstoken , Guid id)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.Gone;

            try
            {
                new Project().DeleteProject(id);
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
