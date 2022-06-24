using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace sample.console.Models.Output
{
    public class CalculateOutput: IOutput
    {
        [DataMember(Order = 1)]
        [JsonInclude]
        public object? Value { get; init; }

        public static CalculateOutput FromResults(object? results) =>
            new()
            {
                Value = results
            };        
        
        /// <summary>
        /// Output as plain text
        /// </summary>
        /// <returns></returns>
        public new string ToString() => Value?.ToString() ?? "N/A";

        /// <summary>
        /// Output as JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson() => JsonSerializer.Serialize(this);

    }
}