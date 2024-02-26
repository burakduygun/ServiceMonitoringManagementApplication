using System;
using System.IO;

namespace Shared.Logging.Loggers
{
    public class FileLogger : AbstractLogger
    {
        private readonly string _path;
        private readonly string _serviceName;

        public FileLogger(string path, string serviceName)
        {
            _path = path;
            _serviceName = serviceName;
        }

        protected override void LogImpl(string message)
        {
            string path = _path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            string filepath = path + "\\Logs\\" + _serviceName + "ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
}
