using System;
using System.Threading.Tasks;
using CodingSeb.ExpressionEvaluator;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sample.console.Models.Arguments;
using sample.console.Services;
using sample.console.Services.Tasks;
using Serilog;

namespace sample.console
{
    internal class Program
    {
        /// <summary>
        /// Main routine
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Exit code</returns>
        public static async Task<int> Main(string[] args)
        {
            try
            {
                var host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((context, services) =>
                    {
                        // Configure Serilog
                        Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(context.Configuration)
                            .CreateLogger();

                        // Set up our console output class
                        services.AddSingleton<IConsoleOutput, ConsoleOutput>();

                        // Based upon verb/options, create services, including the task
                        var parserResult = Parser.Default.ParseArguments<CalculateOptions, StatisticsOptions>(args);
                        parserResult
                            .WithParsed<CalculateOptions>(options =>
                            {
                                services.AddSingleton<ExpressionEvaluator>();
                                services.AddSingleton(options);
                                services.AddSingleton<ITaskFactory, CalculateTaskFactory>();
                            })
                            .WithParsed<StatisticsOptions>(options =>
                            {
                                services.AddSingleton(options);
                                services.AddSingleton<ITaskFactory, StatisticsTaskFactory>();
                            });
                    })
                    .UseSerilog()
                    .Build();

                // If a task was set up to run (i.e. valid command line params) then run it
                // and return the results
                var task = host.Services.GetService<ITaskFactory>();
                return task == null
                    ? -1 // This can happen on --help or invalid arguments
                    : await task.Launch();
            }
            catch (Exception ex)
            {
                // Note that this should only occur if something went wrong with building Host
                await Console.Error.WriteLineAsync(ex.Message);
                return -1;
            }
        }
    }
}