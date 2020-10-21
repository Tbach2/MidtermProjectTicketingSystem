using System;
using System.Collections.Generic;

namespace TicketingSystem
{
    public class Ticket
    {
        public UInt64 ticketId { get; set; }
        public string summary { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string submitter { get; set; }
        public string assigned { get; set; }
        public List<string> watching { get; set; }
    
        public Ticket()
        {
            watching = new List<string>();
        }

        public string Display()
        {
            return $"{ticketId},{summary},{status},{priority},{submitter},{assigned},{string.Join("|", watching)}";
        }
    }
}