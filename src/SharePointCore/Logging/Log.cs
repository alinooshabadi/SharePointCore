using System;
using System.Web.UI;

namespace SharePointCore.Logging
{
    /// <summary>
    /// Logging class
    /// </summary>
    public static class Log
    {
        [ThreadStatic]
        private static ILogger _logger;

        /// <summary>
        /// Gets or sets ILogger object
        /// </summary>
        public static ILogger Logger
        {
            get { return _logger; }
            set { _logger = value; }
        }

        private static void InitializeLogger()
        {
            if (_logger == null)
            {
                _logger = new LoggerCore();
            }
        }

        public static void Error(Exception ex)
        {
            InitializeLogger();
            _logger.Error(ex);
        }

        public static void Error(Exception ex, Page page)
        {
            InitializeLogger();
            _logger.Error(ex, page);
        }

        public static void Warning(string message)
        {
            InitializeLogger();
            _logger.Warning(message);
        }

        public static void Warning(string message, Page page)
        {
            InitializeLogger();
            _logger.Warning(message, page);
        }

        public static void Info(string message, Page page)
        {
            InitializeLogger();
            _logger.Info(message, page);
        }
    }

    public enum EitModules
    {

    }
}