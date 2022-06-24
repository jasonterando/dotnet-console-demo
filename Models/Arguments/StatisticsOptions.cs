using System.Collections.Generic;
using CommandLine;

namespace sample.console.Models.Arguments
{
    /// <summary>
    /// Command line parameters for calculating
    /// </summary>
    [Verb("stats", false, HelpText="Calculate statistics for a list of values")]
    public class StatisticsOptions
    {
        [Option('f', "format", Default = OutputFormat.text)]
        public OutputFormat Format { get; set; } = default!;
        
        [Value(0, MetaName = "Expression", Required = true,
            HelpText = @"Mathematical expression")]
        public IEnumerable<decimal> Values { get; set; } = default!;
    }
}