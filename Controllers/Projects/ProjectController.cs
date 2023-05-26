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
    public partial class ProjectController : ControllerBase
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

        

        
    }
}
