using System;
using System.Text;

namespace Shared.Logging
{
    public abstract class AbstractLogger
    {
        private LogLevel _level;
        public void SetLogLevel(LogLevel level)
        {
            _level = level;
        }

        public LogLevel GetLogLevel()
        {
            return _level;
        }

        protected AbstractLogger()
        {
            _level = LogLevel.Error;
        }

        protected AbstractLogger(LogLevel level)
        {
            _level = level;
        }

        public void Log(string message, LogLevel level)
        {
            if (_level > level)
                return;

            var sb = new StringBuilder();
            sb.Append($"[{DateTime.Now}] ");

            switch (level)
            {
                case LogLevel.Trace:
                    sb.Append($"[Trace] ");
                    break;
                case LogLevel.Debug:
                    sb.Append($"[Debug] ");
                    break;
                case LogLevel.Info:
                    sb.Append($"[Info] ");
                    break;
                case LogLevel.Warn:
                    sb.Append($"[Warn] ");
                    break;
                case LogLevel.Error:
                    sb.Append($"[Error] ");
                    break;
                case LogLevel.Fatal:
                    sb.Append($"[Fatal] ");
                    break;
                case LogLevel.Off:
                    return;
                default:
                    return;
            }

            sb.Append(message);
            LogImpl(sb.ToString());
        }

        protected abstract void LogImpl(string message);

        public void Info(string message)
        {
            Log(message, LogLevel.Info);
        }

        public void Warn(string message)
        {
            Log(message, LogLevel.Warn);
        }

        public void Error(string message)
        {
            Log(message, LogLevel.Error);
        }
        public void Fatal(string message)
        {
            Log(message, LogLevel.Fatal);
        }
        public void Debug(string message)
        {
            Log(message, LogLevel.Debug);
        }
        public void Trace(string message)
        {
            Log(message, LogLevel.Trace);
        }

    }
}
