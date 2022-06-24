using System;
using Microsoft.Extensions.Logging;

namespace sample.console.Services
{
    /// <summary>
    /// Class to abstract console output including mirroring to ILogger
    /// </summary>
    public class ConsoleOutput: IConsoleOutput
    {
        private readonly ILogger<ConsoleOutput> _logger;

        public ConsoleOutput(ILogger<ConsoleOutput> logger)
        {
            _logger = logger;
        }
        
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
            _logger.LogInformation("{Message}", message);
        }

        public void WriteError(string message)
        {
            Console.Error.WriteLine(message);
            _logger.LogError("{Message}", message);
        }
    }

    public interface IConsoleOutput
    {
        void WriteLine(string message);
        void WriteError(string message);
    }
}