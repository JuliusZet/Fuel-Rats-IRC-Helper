/*
 *   Log.cs
 *   Fuel Rats IRC Helper
 *
 *   Created by Julius Zitzmann on 2020-05-08.
 *   Copyright © 2021 Julius Zitzmann. All rights reserved.
 *
 *   Feature requests, bug reports or questions?
 *   info@julius-zitzmann.de
 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */


using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Fuel_Rats_IRC_Helper
{
    public static class Log
    {
        private static readonly string _Filepath = "debug.log";
        private static StreamWriter _StreamWriter = new StreamWriter(_Filepath, true);

        public static void Append(string logEntry)
        {
            _StreamWriter.WriteLine(logEntry);
            _StreamWriter.Flush();
        }

        public static void Show()
        {
            if (File.Exists("debug.log"))
            {
                try
                {
                    Process.Start("debug.log");
                }
                catch
                {
                    try
                    {
                        ProcessStartInfo info = new ProcessStartInfo()
                        {
                            UseShellExecute = true,
                            FileName = Path.GetFullPath("debug.log")
                        };

                        Process.Start(info);
                    }
                    catch
                    {
                        Process.Start("notepad", Path.GetFullPath("debug.log"));
                    }
                }
            }

            else
            {
                MessageBox.Show("The debug log does not exist yet. Please try again after (re-)connecting to the IRC.", "Error");
            }
        }
    }
}
