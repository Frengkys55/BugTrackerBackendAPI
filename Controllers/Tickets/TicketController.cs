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

        /// <summary>
        /// Get detail of a ticket from database
        /// </summary>
        /// <param name="accesstoken">User access token</param>
        /// <param name="id">Guid of the ticket</param>
        /// <returns></returns>
        [HttpGet]
        public Ticket GetTicket([Required] [FromHeader] string accesstoken, [Required] [FromQuery] Guid id)
        {
            // TODO: implement access token

            try
            {
                return new Ticket().GetTicketDetail(id);
            }
            catch (Exception err)
            {
                throw;
            }
        }

        /// <summary>
        /// Create new ticket for a project to database
        /// </summary>
        /// <param name="accesstoken">User access token</param>
        /// <param name="ticket">Ticket information to be added</param>
        /// <param name="projectGuid">Guid of the project to associate</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CreateTicket([Required] [FromHeader] string accesstoken, [FromBody] Ticket ticket, [FromQuery] Guid projectGuid)
        {
            // TODO: Implement acces token method

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            try
            {
                new Ticket().CreateTicket(ticket, projectGuid);
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.Created;
            }
            catch (Exception err)
            {
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                httpResponseMessage.ReasonPhrase = err.Message;
            }
            return httpResponseMessage;
        }

        /// <summary>
        /// Update the informaton of a ticket from the database with a new ticket information
        /// </summary>
        /// <param name="accesstoken">User access token</param>
        /// <param name="ticket">New ticket information</param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage UpdateTicket([FromHeader] string accesstoken, [FromBody] Ticket ticket)
        {
            // TODO: Implement acces token method

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            try
            {
                new Ticket().UpdateTicket(ticket);
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception err)
            {
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                httpResponseMessage.ReasonPhrase = err.Message;
            }

            return httpResponseMessage;
        }

        /// <summary>
        /// Delete a ticket from database
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage Delete([FromHeader] string accesstoken, [FromQuery] Guid id)
        {
            HttpResponseMessage httpsResponseMessage = new HttpResponseMessage();
            try
            {
                new Ticket().DeleteTicket(id);
                httpsResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception err)
            {
                httpsResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                httpsResponseMessage.ReasonPhrase = err.Message;
            }

            return httpsResponseMessage;
        }
    }
}
