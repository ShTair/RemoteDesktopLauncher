using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RemoteDesktopLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            var docp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var defn = Path.Combine(docp, "Default.rdp");

            File.Delete(defn);
            File.Copy(args[0], defn, true);

            var pi = new ProcessStartInfo(@"mstsc.exe")
            {
                WorkingDirectory = @"C:\Windows\System32",
            };

            var p = Process.Start(pi);
            p.WaitForExit();
            var pp = Process.GetProcessesByName("mstsc").OrderByDescending(t => t.StartTime).FirstOrDefault();
            pp.WaitForExit();

            File.Copy(defn, args[0], true);
        }
    }
}
