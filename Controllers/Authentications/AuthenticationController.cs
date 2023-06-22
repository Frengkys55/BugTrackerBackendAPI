using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Results;

namespace BugTrackerBackendAPI.Controllers.Authentications
{
    [Route("/api/Authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Created to get configuration information
        /// </summary>
        /// <param name="configuration"></param>
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="user">User information</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost("Register")]
        public async Task<HttpResponseMessage> AddUser([FromBody] User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            string connectionString = _configuration.GetConnectionString("Default");
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            try
            {
                var result = await new User().AddUserMinimal(user, connectionString);

                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.Created;
                return httpResponseMessage;
            }
            catch (Exception err)
            {
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.BadRequest;
                httpResponseMessage.ReasonPhrase = err.Message;
                return httpResponseMessage;
            }
        }

        /// <summary>
        /// Log user in
        /// </summary>
        /// <param name="user">User authentication information</param>
        /// <returns>Return user's access token</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserShort user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                throw new Exception(nameof(user.Username));
            }

            string connectionString = _configuration.GetConnectionString("Default");

            try
            {
                var result = await new Models.Authentications.Authentication().Login(user, connectionString);
                return Content(result);
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }
        }

        /// <summary>
        /// Log user off
        /// </summary>
        /// <param name="accesstoken">User's token information</param>
        /// <returns></returns>
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout([Required] [FromHeader] string accesstoken)
        {
            if (accesstoken == null)
                return BadRequest(new ArgumentNullException(nameof(accesstoken)));

            string? connectionString = _configuration.GetConnectionString("Default");

            try
            {
                var result = await new Models.Authentications.Authentication().Logout(accesstoken, connectionString);
                return Ok("Success");
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);   
            }
        }

        /// <summary>
        /// Request password reset when user forgot their password. For now it will only ask for username and email.
        /// Future request will also require user to solve their security question.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("RequestReset")]
        public async Task<IActionResult> RequestPasswordReset()
        {
            throw new NotImplementedException();
        }

        #region Demo user

        [HttpGet("RegisterGuest")]
        public async Task<IActionResult> RegisterGuestUser()
        {
            string connectionString = _configuration.GetConnectionString("Default");
            try
            {
                return Content(await new User().AddUserGuestMinimal(connectionString));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Demo user
    }
}
