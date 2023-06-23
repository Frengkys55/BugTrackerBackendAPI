using BugTrackerBackendAPI.Data.DbHelper.Procedure;
using BugTrackerBackendAPI.Models.Misc;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public async Task<bool> MarkSolved(Guid ticketGuid, string accesstoken, string connectionString)
        {
            Executor executor = new Executor(connectionString); 

            Data.DbHelper.GenericWrite<Ticket> genericWrite = new Data.DbHelper.GenericWrite<Ticket>();

            List<KeyValuePair<string, dynamic>> additionalParameters = new List<KeyValuePair<string, dynamic>>()
            {
                new KeyValuePair<string, dynamic>("Guid", ticketGuid.ToString()),
                new KeyValuePair<string, dynamic>("AccessToken", accesstoken)
            };

            try
            {
                string query = "MarkTicketSolved";
                var result = await executor.Execute<DBNull, DbResultModel>(query, null, null, additionalParameters);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
