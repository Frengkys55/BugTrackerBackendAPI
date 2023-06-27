using BugTrackerBackendAPI.Models;
using BugTrackerBackendAPI.Models.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerBackendAPI.Controllers.Comments
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : Controller
    {
        IConfiguration _configuration;
        IWebHostEnvironment _environment;
        public CommentController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// Get list of comments in a ticket
        /// </summary>
        /// <param name="id">Your ticket Guid</param>
        /// <param name="accesstoken">Your given access token</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CommentMinimal>> GetComments([Required] [FromQuery] Guid id, [Required] [FromHeader] string accesstoken)
        {
            try
            {
                return await new Comment().GetCommentListAsync(id, accesstoken, _configuration.GetConnectionString("Default")!);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Add comment to a ticket
        /// </summary>
        /// <param name="id">Your ticket Guid</param>
        /// <param name="comment">Information about the comment you want to add</param>
        /// <param name="accesstoken">your given access token</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<HttpResponseMessage> AddComment([Required] [FromQuery] Guid id, [Required] [FromBody] Comment comment, [Required] [FromHeader] string accesstoken)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            string connectionString = _configuration.GetConnectionString("Default")!;
            try
            {
                var result = await new Comment().AddAsync(id, comment, accesstoken, connectionString);
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.Created;
            }
            catch (Exception err)
            {
                throw;
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                httpResponseMessage.ReasonPhrase = err.Message;
            }

            return httpResponseMessage;
        }

        /// <summary>
        /// Delete a comment from a ticket
        /// </summary>
        /// <param name="id">Your comment Guid</param>
        /// <param name="accesstoken">Your given accesstoken</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteCommentAsync([Required] [FromQuery] Guid id, [Required][FromHeader] string accesstoken)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

            try
            {
                var result = await new Comment().DeleteAsync(id, accesstoken, _configuration.GetConnectionString("Default")!);
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.Created;
            }
            catch (Exception err)
            {
                httpResponseMessage.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                httpResponseMessage.ReasonPhrase = err.Message;
            }

            return httpResponseMessage;
        }
    }
}
