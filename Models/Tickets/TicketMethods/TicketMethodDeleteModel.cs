using BugTrackerBackendAPI.Models.Misc;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public async Task<int> DeleteTicket(Guid ticketGuid, string accesstoken, string connectionString)
        {
            Data.DbHelper.DbWriter writer = new Data.DbHelper.DbWriter();

            List<KeyValuePair<string, dynamic>> parameters = new List<KeyValuePair<string, dynamic>>()
            {
                new KeyValuePair<string, dynamic>("Guid", ticketGuid.ToString()),
                new KeyValuePair<string, dynamic>("AccessToken", accesstoken)
            };


            try
            {
                //int result = await writer.WriteUsingProcedure(connectionString, "DeleteTicket", parameters);

                var result = await new Data.DbHelper.Procedure.Executor(connectionString).Execute<object, DbResultModel>("DeleteTicket", null, null, parameters);
                return result.ToList()[0].Result;
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
