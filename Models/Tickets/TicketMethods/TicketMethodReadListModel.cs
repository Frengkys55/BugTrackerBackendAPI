﻿using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public ICollection<ShortTicket> GetAllTicketList(string accesstoken, string connectionString)
        {
            Collection<ShortTicket> list = new Collection<ShortTicket>();

            Data.DbHelper.GenericRead<ShortTicket> dbRead = new Data.DbHelper.GenericRead<ShortTicket>();

            string query = "SELECT * FROM GetAllTicketsShort ('" + accesstoken.ToString() + "')";
            try
            {
                var result = dbRead.Read(query, connectionString);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
