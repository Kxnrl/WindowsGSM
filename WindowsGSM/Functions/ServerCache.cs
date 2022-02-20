using System;
using System.IO;

namespace WindowsGSM.Functions
{
    static class ServerCache
    {
        public static int GetPID(string serverId)
        {
            string cacheFile = GetPIDPath(serverId);
            if (File.Exists(cacheFile))
            {
                string text = File.ReadAllText(cacheFile).Trim();
                if (int.TryParse(text, out int pid))
                {
                    return pid;
                }
            }

            return -1;
        }

        public static void SavePID(string serverId, int pid)
        {
            File.WriteAllText(GetPIDPath(serverId), pid.ToString());
        }

        private static string GetPIDPath(string serverId)
        {
            string cachePath = ServerPath.GetServersCache(serverId);
            Directory.CreateDirectory(cachePath);
            return Path.Combine(cachePath, "pid");
        }

        public static string GetProcessName(string serverId)
        {
            string cacheFile = GetProcessNamePath(serverId);
            return File.ReadAllText(cacheFile).Trim();
        }

        public static void SaveProcessName(string serverId, string pName)
        {
            File.WriteAllText(GetProcessNamePath(serverId), pName);
        }

        public static DateTime GetUptime(string serverId)
        {
            var cacheFile = GetUptimePath(serverId);
            if (!File.Exists(cacheFile))
            {
                return DateTime.Now;
            }
            var text = File.ReadAllText(cacheFile);
            if (!string.IsNullOrEmpty(text) && DateTime.TryParse(text.Trim(), out var time))
            {
                return time;
            }
            return DateTime.Now;
        }

        public static void SaveUptime(string serverId, DateTime time)
        {
            File.WriteAllText(GetUptimePath(serverId), time.ToString());
        }

        private static string GetProcessNamePath(string serverId)
        {
            string cachePath = ServerPath.GetServersCache(serverId);
            Directory.CreateDirectory(cachePath);
            return Path.Combine(cachePath, "processName");
        }

        private static string GetUptimePath(string serverId)
        {
            var cachePath = ServerPath.GetServersCache(serverId);
            Directory.CreateDirectory(cachePath);
            return Path.Combine(cachePath, "Uptime");
        }

        public static IntPtr GetWindowsIntPtr(string serverId)
        {
            string cacheFile = GetWindowsIntPtrPath(serverId);
            if (File.Exists(cacheFile))
            {
                string text = File.ReadAllText(cacheFile).Trim();
                if (long.TryParse(text, out long windows))
                {
                    return (IntPtr)windows;
                }
            }

            return (IntPtr)0;
        }

        public static void SaveWindowsIntPtr(string serverId, IntPtr windows)
        {
            File.WriteAllText(GetWindowsIntPtrPath(serverId), windows.ToString());
        }

        private static string GetWindowsIntPtrPath(string serverId)
        {
            string cachePath = ServerPath.GetServersCache(serverId);
            Directory.CreateDirectory(cachePath);
            return Path.Combine(cachePath, "windowsIntPtr");
        }
    }
}
