using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Controllers.Projects
{
    public partial class ProjectController : ControllerBase
    {
        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string accesstoken, Guid id)
        {
            try
            {
                new Project().DeleteProject(id, accesstoken, _configuration.GetConnectionString("Default"));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
            return NoContent();
        }
    }
}
