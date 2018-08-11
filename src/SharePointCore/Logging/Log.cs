using SharePointCore.UI;
using System;
using System.Threading;
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


        #region Error
        /// <summary>
        /// Logs error message and source
        /// </summary>
        /// <param name="source">Error source</param>
        /// <param name="message">Error message</param>
        /// <param name="args">Arguments object</param>
        public static void Error(Exception ex, Page page)
        {
            InitializeLogger();
            _logger.Error(ex, page);

        }

        #endregion



    }
}