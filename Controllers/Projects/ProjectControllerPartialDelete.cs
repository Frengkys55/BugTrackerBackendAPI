using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Controllers.Projects
{
    public partial class ProjectController : ControllerBase
    {
        /// <summary>
        /// Delete a project from database
        /// </summary>
        /// <param name="accesstoken">Your access token</param>
        /// <param name="id">Project Guid</param>
        /// <returns></returns>
        [EnableCors("AllowAllOrigins")]
        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string accesstoken, Guid id)
        {
            try
            {
                new Project().DeleteProject(id, accesstoken, _configuration.GetConnectionString("Default")!);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
            return NoContent();
        }
    }
}
