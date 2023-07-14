using BugTrackerBackendAPI.Models.Misc;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public async void UpdateTicket(Ticket ticket, string accesstoken, string connectionString)
        {
            Data.DbHelper.GenericWrite<Ticket> genericWrite = new Data.DbHelper.GenericWrite<Ticket>();

            List<string> ignoreProperty = new List<string>()
            {
                nameof(ticket.Id),
                nameof(ticket.DateCreated),
                nameof(ticket.DateModified),
                nameof(ticket.Project),
                nameof(ticket.DateSolved)
            };

            List<KeyValuePair<string, string>> additionalParameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("AccessToken", accesstoken)
            };
            try
            {
                var result = await new Data.DbHelper.DbWriter().WriteUsingProcedureGeneric(connectionString, "EditTicket", ticket, ignoreProperty, additionalParameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
