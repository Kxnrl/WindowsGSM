using System;
using System.Diagnostics;

namespace WindowsGSM.Functions
{
    class ServerTable
    {
        public string ID { get; set; }
        public string PID { get; set; }
        public string Game { get; set; }
        public string Icon { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string Port { get; set; }
        public string QueryPort { get; set; }
        public string Defaultmap { get; set; }
        public string Maxplayers { get; set; }
        public string Uptime
        {
            get
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(PID) && int.TryParse(PID, out var pid))
                    {
                        var time = Process.GetProcessById(pid).StartTime;
                        return ((DateTime.Now - time).TotalSeconds < 0)
                            ? DateTime.Now + " => " + time
                            : (DateTime.Now - time).ToString("d':'hh':'mm':'ss");
                    }
                }
                catch { }

                return "N/A";
            }
        }
        public string CrashMessage { get; set; }
        public string CrashReason { get; set; }
    }
}
