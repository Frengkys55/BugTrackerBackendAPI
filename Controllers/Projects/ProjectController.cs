using Microsoft.AspNetCore.Mvc;
using BugTrackerBackendAPI.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;
using System.Text;
using System.Collections.ObjectModel;
using Azure.Core;
using System.Web.Http.Results;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTrackerBackendAPI.Controllers.Projects
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public partial class ProjectController : ControllerBase
    {
        private IConfiguration _configuration;

        public ProjectController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Get all available project for specific user
        /// </summary>
        /// <param name="accesstoken">Logged in user access token information</param>
        /// <returns></returns>
        [HttpGet("GetProjects")]
        public IActionResult GetAllProjects([FromHeader] string accesstoken)
        {
            // TODO: Implmenent access token
            HttpRequestMessage message = new HttpRequestMessage();
            try
            {
                List<ShortProjectInfo> projects = new Project().GetProjectsList(accesstoken, _configuration.GetConnectionString("Default")).ToList();
                return Ok(projects);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public IActionResult GetProjectDetail([FromHeader] string accesstoken, [Required] Guid id)
        {
            // TODO: implement access token

            Project project = new Project();

            try
            {
                string connectionString = _configuration.GetConnectionString("Default");
                return Ok(project.GetProject(id, accesstoken, connectionString));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
