using System.Threading.Tasks;

namespace sample.console.Services.Tasks
{
    /// <summary>
    /// Interface for launching an executable task
    /// </summary>
    public interface ITaskFactory
    {
        /// <summary>
        /// Execute a task, return the exit code (0 = success)
        /// </summary>
        /// <returns></returns>
        public Task<int> Launch();
    }
}