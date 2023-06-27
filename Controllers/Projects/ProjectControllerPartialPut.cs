using Azure.Core;
using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Http.Results;

namespace BugTrackerBackendAPI.Controllers.Projects
{
    public partial class ProjectController : ControllerBase
    {
        /// <summary>
        /// Update existing information with the new information
        /// </summary>
        /// <param name="project">New information about the project to be updated</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProjectInformation([FromBody] Project project)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string address = $"{Request.Scheme}://{Request.Host}";

            var projectInner = new Project(_environment, address);

            try
            {
;               projectInner.Name = project.Name;
                projectInner.Guid = project.Guid;
                projectInner.IconUrl = project.IconUrl;
                projectInner.BackgroundImageUrl = project.BackgroundImageUrl;
                projectInner.accesstoken = project.accesstoken;
                projectInner.DateCreated = project.DateCreated;
                projectInner.DateModified = project.DateModified;
                projectInner.Description = project.Description;
                projectInner.ProjectStatus = project.ProjectStatus;
                projectInner.User = project.User;
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                string connectionString = _configuration.GetConnectionString("Default")!;
                await projectInner.UpdateProject(projectInner, connectionString);
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok();
            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }
        }

        /// <summary>
        /// Create new project for the user
        /// </summary>
        /// <param name="accesstoken">User access token</param>
        /// <param name="project">The information about the project to be created</param>
        /// <returns></returns> 
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public IActionResult CreateNewProject([Required][FromBody] Project project)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.Gone;
            string address = $"{Request.Scheme}://{Request.Host}";

            Project projectInner = new Project(_environment, address)
            {
                Id = project.Id,
                Name = project.Name,
                Guid = project.Guid,
                IconUrl = project.IconUrl,
                BackgroundImageUrl = project.BackgroundImageUrl,
                accesstoken = project.accesstoken,
                DateCreated = project.DateCreated,
                DateModified = project.DateModified,
                Description = project.Description,
                ProjectStatus = project.ProjectStatus,
                User = project.User
            };

            try
            {
                // Replace guid that was received from user
                projectInner.Guid = Guid.NewGuid();
                projectInner.CreateProject(projectInner, _configuration.GetConnectionString("Default")!);
                response.StatusCode = System.Net.HttpStatusCode.Created;
                response.ReasonPhrase = "Hei! It's created!";
            }
            catch (Exception err)
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = err.Message;
                HttpRequestMessage message = new HttpRequestMessage();
                message.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, err.Message);
                return BadRequest(response);
            }
            return new CreatedResult(nameof(CreateNewProject), response);
        }
    }
}
