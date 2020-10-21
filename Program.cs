using System;
using NLog.Web;
using System.IO;

namespace TicketingSystem
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string ticketFilePath = Directory.GetCurrentDirectory() + "\\tickets.csv";

            logger.Info("Program started");

            TicketFile ticketFile = new TicketFile(ticketFilePath);

            string choice = "";
            do
            {
                Console.WriteLine("1) Add Ticket");
                Console.WriteLine("2) Display All Tickets");
                Console.WriteLine("Enter to quit");

                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);

                if (choice == "1")
                {
                    Ticket ticket = new Ticket();

                    Console.WriteLine("Enter ticket summary");
                    ticket.summary = Console.ReadLine();

                    Console.WriteLine("Enter ticket status");
                    ticket.status = Console.ReadLine();

                    Console.WriteLine("Enter ticket priority");
                    ticket.priority = Console.ReadLine();

                    Console.WriteLine("Enter ticket submitter");
                    ticket.submitter = Console.ReadLine();

                    Console.WriteLine("Enter ticket asignee");
                    ticket.assigned = Console.ReadLine();

                    string input;
                    do
                    {
                        Console.WriteLine("Enter watcher (or done to quit)");
                        input = Console.ReadLine();
                        if (input != "done" && input.Length > 0)
                        {
                            ticket.watching.Add(input);
                        }
                    }
                    while (input != "done");

                    if (ticket.watching.Count == 0)
                    {
                        ticket.watching.Add("(no watchers listed)");
                    }
                    ticketFile.AddTicket(ticket);


                } 
                else if (choice == "2")
                {
                    foreach(Ticket m in ticketFile.Tickets)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
            } while (choice == "1" || choice == "2");

            logger.Info("Program ended");
        }
    }
}