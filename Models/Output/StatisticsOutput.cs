using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using MathNet.Numerics.Statistics;

namespace sample.console.Models.Output
{
    public class StatisticsOutput: IOutput
    {
        public static StatisticsOutput FromDescriptiveStatistics(DescriptiveStatistics statistics) =>
            new()
            {
                Count = statistics.Count,
                Mean = statistics.Mean,
                Variance = statistics.Variance,
                StandardDeviation = statistics.StandardDeviation,
                Skewness = double.IsNaN(statistics.Skewness) ? null : statistics.Skewness,
                Kurtosis = double.IsNaN(statistics.Kurtosis) ? null : statistics.Kurtosis,
                Minimum = statistics.Minimum,
                Maximum = statistics.Maximum
            };
        
        /// <summary>Gets the size of the sample.</summary>
        /// <value>The size of the sample.</value>
        [DataMember(Order = 1)]
        [JsonInclude]
        public long Count { get; init; }

        /// <summary>Gets the sample mean.</summary>
        /// <value>The sample mean.</value>
        [DataMember(Order = 2)]
        [JsonInclude]
        public double Mean { get; init; }

        /// <summary>
        /// Gets the unbiased population variance estimator (on a dataset of size N will use an N-1 normalizer).
        /// </summary>
        /// <value>The sample variance.</value>
        [DataMember(Order = 3)]
        [JsonInclude]
        public double Variance { get; init; }

        /// <summary>
        /// Gets the unbiased population standard deviation (on a dataset of size N will use an N-1 normalizer).
        /// </summary>
        /// <value>The sample standard deviation.</value>
        [DataMember(Order = 4)]
        [JsonInclude]
        public double StandardDeviation { get; init; }

        /// <summary>Gets the sample skewness.</summary>
        /// <value>The sample skewness.</value>
        /// <remarks>Returns zero if <see cref="P:MathNet.Numerics.Statistics.DescriptiveStatistics.Count" /> is less than three. </remarks>
        [DataMember(Order = 5)]
        [JsonInclude]
        public double? Skewness { get; init; }

        /// <summary>Gets the sample kurtosis.</summary>
        /// <value>The sample kurtosis.</value>
        /// <remarks>Returns zero if <see cref="P:MathNet.Numerics.Statistics.DescriptiveStatistics.Count" /> is less than four. </remarks>
        [DataMember(Order = 6)]
        [JsonInclude]
        public double? Kurtosis { get; init; }

        /// <summary>Gets the maximum sample value.</summary>
        /// <value>The maximum sample value.</value>
        [DataMember(Order = 7)]
        [JsonInclude]
        public double Maximum { get; init; }

        /// <summary>Gets the minimum sample value.</summary>
        /// <value>The minimum sample value.</value>
        [DataMember(Order = 8)]
        [JsonInclude]
        public double Minimum { get; init; }

        /// <summary>
        /// Output as plain text
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            $"Count: {Count}, Mean: {Mean}, Variance: {Variance}, StdDev: {StandardDeviation}, Skewness: {(Skewness == null ? "N/A" : Skewness.ToString())}, Kurtosis: {(Kurtosis == null ? "N/A" : Kurtosis.ToString())}, Maximum: {Maximum}: Minimum: {Minimum}";

        /// <summary>
        /// Output as JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson() => JsonSerializer.Serialize(this);
    }
}
