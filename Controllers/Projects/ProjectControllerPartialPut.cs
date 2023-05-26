﻿using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        public IActionResult CreateNewProject([Required][FromBody] Project project)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.Gone;

            try
            {
                // Replace guid that was received from user
                project.Guid = Guid.NewGuid();
                project.CreateProject(project, _configuration.GetConnectionString("Default"));
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
