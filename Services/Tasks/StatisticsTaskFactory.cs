using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;
using Microsoft.Extensions.Logging;
using sample.console.Models.Arguments;
using sample.console.Models.Output;

namespace sample.console.Services.Tasks
{
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
    public class StatisticsTaskFactory : ITaskFactory
    {
        private readonly IConsoleOutput _console;
        private readonly ILogger<StatisticsTaskFactory> _logger;
        private readonly StatisticsOptions _options;

        public StatisticsTaskFactory(
            IConsoleOutput console,
            ILogger<StatisticsTaskFactory> logger,
            StatisticsOptions options)
        {
            _console = console;
            _logger = logger;
            _options = options;
        }

        /// <summary>
        /// Outputs a feed to the specified file and format  
        /// </summary>
        public Task<int> Launch() => Task.Run(() =>
        {
            try
            {
                _logger.LogDebug("Analyzing {Values}", string.Join(",", _options.Values));
                var start = DateTime.Now;
                var stats = new DescriptiveStatistics(_options.Values.Select(Convert.ToDouble));
                var end = DateTime.Now;
                var results = StatisticsOutput.FromDescriptiveStatistics(stats);
                var output = _options.Format == OutputFormat.text
                    ? string.Join(", ", results.GetType().GetProperties().Select(p => $"{p.Name}={p.GetValue(results)}"))
                    : JsonSerializer.Serialize(results);
                _console.WriteLine(output);
                _logger.LogDebug("Analyzed in {Elapsed} milliseconds", 
                    end.Subtract(start).TotalMilliseconds);
                return 0;
            }
            catch (Exception ex)
            {
                _console.WriteError($"Unable to calculate: {ex.Message}");
                return -1;
            }
        });
    }
}