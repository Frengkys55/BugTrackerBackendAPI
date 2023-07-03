using Microsoft.AspNetCore.Mvc;
using BugTrackerBackendAPI.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using System.Text;
using System.Collections.ObjectModel;
using Azure.Core;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Http.HttpResults;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTrackerBackendAPI.Controllers.Projects
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public partial class ProjectController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;


        /// <summary>
        /// Used to read configuration and application environment
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="_environment"></param>
        public ProjectController(IConfiguration configuration, IWebHostEnvironment _environment)
        {
            _configuration = configuration;
            this._environment = _environment;
        }

        /// <summary>
        /// Get all available project for specific user
        /// </summary>
        /// <param name="accesstoken">Logged in user access token information</param>
        /// <returns></returns>
        [HttpGet("GetProjects")]
        public async Task<IActionResult> GetAllProjects([FromHeader] string accesstoken)
        {
            // TODO: Implmenent access token
            HttpRequestMessage message = new HttpRequestMessage();
            try
            {
               var projects = await new Project().GetProjectsList(accesstoken, _configuration.GetConnectionString("Default")!);
                return Ok(projects);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [EnableCors("AllowAllOrigins")]
        [HttpGet("{id}")]
        public IActionResult GetProjectDetail([FromRoute] Guid id, [FromHeader] string accesstoken)
        {
            // TODO: implement access token

            Project project = new Project(_environment, "");

            try
            {
                string connectionString = _configuration.GetConnectionString("Default")!;
                return Ok(project.GetProject(id, accesstoken, connectionString));
            }
            catch (Exception err)
            {
                if (err.Message.Contains("Not Found"))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(err);

                }
            }
        }
    }
}
