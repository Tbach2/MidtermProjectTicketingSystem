using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketingSystem
{
    public class TicketFile
    {
        public string filePath { get; set; }
        public List<Ticket> Tickets { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public TicketFile(string ticketFilePath)
        {
            filePath = ticketFilePath;
            Tickets = new List<Ticket>();

            try
            {
                StreamReader sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    
                    string line = sr.ReadLine();
                    string[] ticketDetails = line.Split(',');
                    if (ticketDetails.Length == 8)
                    {
                        BugDefect bugDefect = new BugDefect();

                        bugDefect.ticketId = UInt64.Parse(ticketDetails[0]);
                        bugDefect.summary = ticketDetails[1];
                        bugDefect.status = ticketDetails[2];
                        bugDefect.priority = ticketDetails[3];
                        bugDefect.submitter = ticketDetails[4];
                        bugDefect.assigned = ticketDetails[5];
                        bugDefect.watching = ticketDetails[6].Split('|').ToList();
                        bugDefect.severity = ticketDetails[7];
                        Tickets.Add(bugDefect);
                    }
                    else if (ticketDetails.Length == 11)
                    {
                        Enhancement enhancement = new Enhancement();

                        enhancement.ticketId = UInt64.Parse(ticketDetails[0]);
                        enhancement.summary = ticketDetails[1];
                        enhancement.status = ticketDetails[2];
                        enhancement.priority = ticketDetails[3];
                        enhancement.submitter = ticketDetails[4];
                        enhancement.assigned = ticketDetails[5];
                        enhancement.watching = ticketDetails[6].Split('|').ToList();
                        enhancement.software = ticketDetails[7];
                        enhancement.cost = ticketDetails[8];
                        enhancement.reason = ticketDetails[9];
                        enhancement.estimate = ticketDetails[10];
                        Tickets.Add(enhancement);
                    }
                    else if (ticketDetails.Length == 9)
                    {
                        Task task = new Task();

                        task.ticketId = UInt64.Parse(ticketDetails[0]);
                        task.summary = ticketDetails[1];
                        task.status = ticketDetails[2];
                        task.priority = ticketDetails[3];
                        task.submitter = ticketDetails[4];
                        task.assigned = ticketDetails[5];
                        task.watching = ticketDetails[6].Split('|').ToList();
                        task.projectName = ticketDetails[7];
                        task.dueDate = ticketDetails[8];
                        Tickets.Add(task);
                    }
                }
                sr.Close();
                logger.Info("Tickets in file: {Count}", Tickets.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddBugDefect(BugDefect bugDefect)
        {
            try
            {
                bugDefect.ticketId = Tickets.Max(m => m.ticketId) + 1;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{bugDefect.ticketId},{bugDefect.summary},{bugDefect.status},{bugDefect.priority},{bugDefect.submitter},{bugDefect.assigned},{string.Join("|", bugDefect.watching)},{bugDefect.severity}");
                sw.Close();
                Tickets.Add(bugDefect);
                logger.Info("Ticket ID {Id} added", bugDefect.ticketId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddEnhancemant(Enhancement enhancement)
        {
            try
            {
                enhancement.ticketId = Tickets.Max(m => m.ticketId) + 1;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{enhancement.ticketId},{enhancement.summary},{enhancement.status},{enhancement.priority},{enhancement.submitter},{enhancement.assigned},{string.Join("|", enhancement.watching)},{enhancement.software},{enhancement.cost},{enhancement.reason},{enhancement.estimate}");
                sw.Close();
                Tickets.Add(enhancement);
                logger.Info("Ticket ID {Id} added", enhancement.ticketId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddTask(Task task)
        {
            try
            {
                task.ticketId = Tickets.Max(m => m.ticketId) + 1;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{task.ticketId},{task.summary},{task.status},{task.priority},{task.submitter},{task.assigned},{string.Join("|", task.watching)},{task.projectName},{task.dueDate}");
                sw.Close();
                Tickets.Add(task);
                logger.Info("Ticket ID {Id} added", task.ticketId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}