using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public void UpdateTicket(Ticket ticket, string accesstoken, string connectionString)
        {
            Data.DbHelper.GenericWrite<Ticket> genericWrite = new Data.DbHelper.GenericWrite<Ticket>();

            List<string> ignoreProperty = new List<string>()
            {
                "Id",
                "DateCreated",
                "DateModified",
                "Project"
            };

            List<KeyValuePair<string, string>> additionalParameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("AccessToken", accesstoken)
            };
            try
            {
                genericWrite.WriteUsingProcedure(connectionString, "EditTicket", ticket, ignoreProperty, additionalParameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
