using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        /// <summary>
        /// Get detailed ticket information
        /// </summary>
        /// <param name="accesstoken">Your given access token</param>
        /// <param name="ticketGuid">Your ticket guid to use</param>
        /// <param name="connectionString">Database connection string/param>
        /// <returns></returns>
        public async Task<ICollection<Ticket>> GetLongestUnsolvedTickets(string accesstoken, string connectionString)
        {
            Data.DbHelper.GenericRead<Ticket> helper = new Data.DbHelper.GenericRead<Ticket>();
            string query = "SELECT * FROM GetLongestUnsolvedTickets (5, '" + accesstoken + "')";
            try
            {
                Ticket ticket = new Ticket();
                return await helper.Read(query, connectionString, null);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
