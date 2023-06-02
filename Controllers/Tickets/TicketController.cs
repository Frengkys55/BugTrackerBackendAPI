using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTrackerBackendAPI.Controllers.Tickets
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        IConfiguration _configuration;
        public TicketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Get list of available tickets from a specified project
        /// </summary>
        /// <param name="accesstoken"></param>
        /// <returns></returns>
        [HttpGet("GetAllTickets")]
        public async Task<IEnumerable<ShortTicket>> GetAllTickets([FromHeader] string accesstoken)
        {
            string connectionString = _configuration.GetConnectionString("Default");
            try
            {
                return await new Ticket().GetAllTicketList(accesstoken, connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get tickets for specified project
        /// </summary>
        /// <param name="accesstoken">Your given access token</param>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpGet("{project}")]
        public async Task<IEnumerable<ShortTicket>> GetProjectTickets([FromHeader] string accesstoken, Guid project)
        {
            string connectionString = _configuration.GetConnectionString("Default");
            try
            {
                return await new Ticket().GetProjectTickets(project, accesstoken, connectionString);
            }
            catch (Exception)
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
        public async Task<Ticket> GetTicket([Required] [FromHeader] string accesstoken, [Required] [FromQuery] Guid id)
        {
            string connectionString = _configuration.GetConnectionString("Default");

            try
            {
                return await new Ticket().GetTicketDetail(accesstoken, id, connectionString);
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
            string connectionString = _configuration.GetConnectionString("Default");

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            try
            {
                new Ticket().CreateTicket(ticket, projectGuid, connectionString, accesstoken);
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
                new Ticket().UpdateTicket(ticket, accesstoken, _configuration.GetConnectionString("Default"));
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
            string connectionString = _configuration.GetConnectionString("Default");

            HttpResponseMessage httpsResponseMessage = new HttpResponseMessage();
            try
            {
                new Ticket().DeleteTicket(id, accesstoken, connectionString);
                httpsResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
            }
            catch (Exception err)
            {
                httpsResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                httpsResponseMessage.ReasonPhrase = err.Message;
            }

            return httpsResponseMessage;
        }

        /// <summary>
        /// Get available ticket types
        /// </summary>
        /// <param name="accesstoken">Your given access token</param>
        /// <returns></returns>
        [HttpGet("Types")]
        public async Task<IEnumerable<Models.Tickets.TypeModel>> GetAllTicketTypes([FromHeader] string accesstoken)
        {
            string connectionString = _configuration.GetConnectionString("Default");
            try
            {
                return await new Ticket().GetAllTicketTypes(accesstoken, connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get awailable ticket severity categories
        /// </summary>
        /// <param name="accesstoken">Your given access token</param>
        /// <returns></returns>
        [HttpGet("Severities")]
        public async Task<IEnumerable<Models.Tickets.SeverityModel>> GetAllTicketSeverities([FromHeader] string accesstoken)
        {
            string connectionString = _configuration.GetConnectionString("Default");
            try
            {
                return await new Ticket().GetAllTicketSeverities(accesstoken, connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Return a ticket with the highest severity
        /// </summary>
        /// <param name="accesstoken">Your given access token</param>
        /// <returns></returns>
        [HttpGet("GetHighestSeverityTickets")]
        public async Task<IEnumerable<ShortTicket>> GetHighestSeverityTickets([FromHeader] string accesstoken)
        {
            string connectionString = _configuration.GetConnectionString("Default");
            try
            {
                return await new Ticket().GetHighestSeverityTicketList(accesstoken, connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get the longest unsolved ticket
        /// </summary>
        /// <param name="accesstoken">Your given access token</param>
        /// <returns></returns>
        [HttpGet("LongestUnsolved")]
        public async Task<Ticket> GetLongestUnsolvedTicket([FromHeader] string accesstoken)
        {
            string connectionString = _configuration.GetConnectionString("Default");
            try
            {
                return await new Ticket().GetLongestUnsolvedTicket(accesstoken, connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("LongestUnsolvedTickets")]
        public async Task<IEnumerable<Ticket>> GetLongestUnsolvedTickets([FromHeader] string accesstoken)
        {
            string connectionString = _configuration.GetConnectionString("Default");
            try
            {
                return await new Ticket().GetLongestUnsolvedTickets(accesstoken, connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
