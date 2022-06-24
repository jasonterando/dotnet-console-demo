using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading.Tasks;
using CodingSeb.ExpressionEvaluator;
using Microsoft.Extensions.Logging;
using sample.console.Models.Arguments;
using sample.console.Models.Output;

namespace sample.console.Services.Tasks
{
    [SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
    public class CalculateTaskFactory : ITaskFactory
    {
        private readonly ExpressionEvaluator _evaluator;
        private readonly IConsoleOutput _console;
        private readonly ILogger<CalculateTaskFactory> _logger;
        private readonly CalculateOptions _options;

        public CalculateTaskFactory(
            ExpressionEvaluator evaluator,
            IConsoleOutput console,
            ILogger<CalculateTaskFactory> logger,
            CalculateOptions options)
        {
            _evaluator = evaluator;
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
                var expression = string.Join(" ", _options.Expression);
                _logger.LogDebug("Evaluating {Evaluation}", expression);
                var start = DateTime.Now;
                var results = _evaluator.Evaluate(expression);
                var end = DateTime.Now;
                var output = CalculateOutput.FromResults(results);
                _console.WriteLine(_options.Format == OutputFormat.text
                    ? output.ToString()
                    : output.ToJson());
                _logger.LogDebug("Evaluated in {Elapsed} milliseconds",
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