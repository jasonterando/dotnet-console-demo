using System.Collections.Generic;
using CommandLine;

namespace sample.console.Models.Arguments
{
    /// <summary>
    /// Command line parameters for calculating
    /// </summary>
    [Verb("calc", true, HelpText="Evaluate a mathematical expression")]
    public class CalculateOptions
    {
        [Option('f', "format", Default = OutputFormat.text)]
        public OutputFormat Format { get; set; } = default!;
        
        [Value(0, MetaName = "Expression", Required = true,
            HelpText = @"Mathematical expression")]
        public IEnumerable<string> Expression { get; set; } = default!;
    }
}