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

                    Console.WriteLine("1) Bug/Defect");
                    Console.WriteLine("2) Enhancement");
                    Console.WriteLine("3) Task");
                    string ticketTypeChoice = Console.ReadLine();
                    logger.Info("User choice: {Choice}", ticketTypeChoice);

                    if (ticketTypeChoice == "1")
                    {
                        BugDefect bugDefect = new BugDefect();

                        Console.WriteLine("Enter Bug/Defect ticket summary");
                        bugDefect.summary = Console.ReadLine();

                        Console.WriteLine("Enter Bug/Defect ticket status");
                        bugDefect.status = Console.ReadLine();

                        Console.WriteLine("Enter Bug/Defect ticket priority");
                        bugDefect.priority = Console.ReadLine();

                        Console.WriteLine("Enter Bug/Defect ticket submitter");
                        bugDefect.submitter = Console.ReadLine();

                        Console.WriteLine("Enter Bug/Defect ticket asignee");
                        bugDefect.assigned = Console.ReadLine();

                        string input;
                        do
                        {
                            Console.WriteLine("Enter Bug/Defect ticket watcher (or done to quit)");
                            input = Console.ReadLine();
                            if (input != "done" && input.Length > 0)
                            { bugDefect.watching.Add(input); }
                        }
                        while (input != "done");

                        if (bugDefect.watching.Count == 0)
                        { bugDefect.watching.Add("(no watchers listed)"); }

                        Console.WriteLine("Enter Bug/Defect ticket severity");
                        bugDefect.severity = Console.ReadLine();

                        ticketFile.AddBugDefect(bugDefect);
                    }

                    if (ticketTypeChoice == "2")
                    {
                        Enhancement enhancement = new Enhancement();

                        Console.WriteLine("Enter Enhancement ticket summary");
                        enhancement.summary = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket status");
                        enhancement.status = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket priority");
                        enhancement.priority = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket submitter");
                        enhancement.submitter = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket asignee");
                        enhancement.assigned = Console.ReadLine();

                        string input;
                        do
                        {
                            Console.WriteLine("Enter Enhancement ticket watcher (or done to quit)");
                            input = Console.ReadLine();
                            if (input != "done" && input.Length > 0)
                            { enhancement.watching.Add(input); }
                        }
                        while (input != "done");

                        if (enhancement.watching.Count == 0)
                        { enhancement.watching.Add("(no watchers listed)"); }

                        Console.WriteLine("Enter Enhancement ticket software");
                        enhancement.software = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket cost");
                        enhancement.cost = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket reason");
                        enhancement.reason = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket estimate");
                        enhancement.estimate = Console.ReadLine();

                        ticketFile.AddEnhancemant(enhancement);
                    }
                    if (ticketTypeChoice == "3")
                    {
                        Task task = new Task();

                        Console.WriteLine("Enter Task ticket summary");
                        task.summary = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket status");
                        task.status = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket priority");
                        task.priority = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket submitter");
                        task.submitter = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket asignee");
                        task.assigned = Console.ReadLine();

                        string input;
                        do
                        {
                            Console.WriteLine("Enter Task ticket watcher (or done to quit)");
                            input = Console.ReadLine();
                            if (input != "done" && input.Length > 0)
                            { task.watching.Add(input); }
                        }
                        while (input != "done");

                        if (task.watching.Count == 0)
                        { task.watching.Add("(no watchers listed)"); }

                        Console.WriteLine("Enter Task ticket project name");
                        task.projectName = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket due date");
                        task.dueDate = Console.ReadLine();

                        ticketFile.AddTask(task);
                    }
                } 

                else if (choice == "2")
                {
                    foreach(Ticket m in ticketFile.Tickets)
                    { Console.WriteLine(m.Display()); }
                }
            } while (choice == "1" || choice == "2");
            logger.Info("Program ended");
        }
    }
}