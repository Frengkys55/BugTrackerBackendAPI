using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTrackerBackendAPI.Controllers.Tickets
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        /// <summary>
        /// Get list of available tickets from a specified project
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        [HttpGet("GetTickets")]
        public IEnumerable<Ticket> GetTickets([FromHeader] string accesstoken, Guid projectGuid)
        {
            // TODO: Implement accesstoken

            Guid userGuid = Guid.NewGuid();

            try
            {
                return new Ticket().GetTicketList(userGuid);
            }
            catch (Exception err)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public string Get([Required] Guid id)
        {
            return "value";
        }

        // POST api/<TicketController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
