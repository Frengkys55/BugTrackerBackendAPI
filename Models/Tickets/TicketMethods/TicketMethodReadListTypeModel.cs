using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public async Task<ICollection<Models.Tickets.TypeModel>> GetAllTicketTypes(string accesstoken, string connectionString)
        {
            Data.DbHelper.GenericRead< Models.Tickets.TypeModel> dbRead = new Data.DbHelper.GenericRead<Models.Tickets.TypeModel> ();

            string query = "SELECT * FROM GetAllTicketTypes ('" + accesstoken.ToString() + "')";
            try
            {
                return await dbRead.Read(query, connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
