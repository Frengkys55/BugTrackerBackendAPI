﻿using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public async Task<ICollection<ShortTicket>> GetAllTicketList(string accesstoken, string connectionString)
        {
            Collection<ShortTicket> list = new Collection<ShortTicket>();

            Data.DbHelper.GenericRead<ShortTicket> dbRead = new Data.DbHelper.GenericRead<ShortTicket>();

            string query = "SELECT * FROM GetAllTicketsShort ('" + accesstoken.ToString() + "')";
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
