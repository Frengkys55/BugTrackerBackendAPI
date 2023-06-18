using BugTrackerBackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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

        [HttpPost("Login")]
        public async Task<HttpResponseMessage> Login([FromBody] UserShort user)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

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
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                httpResponseMessage.Content = new StringContent(result);
                return httpResponseMessage;
            }
            catch (Exception err)
            {
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                httpResponseMessage.Content = new StringContent(err.Message);
            }
        }

        [HttpGet("Logout")]
        public async Task<HttpResponseMessage> Logout([FromHeader] string accesstoken)
        {
            throw new NotImplementedException();
        }
    }
}
