using System.Threading.Tasks;

namespace AsyncExtensions
{
    /// <summary>
    /// Class which provides syntactic sugar for getting a completed task with no return value.
    /// </summary>
    public class Completed
    {
        /// <summary>
        /// Get a completed task with no return type.
        /// </summary>
        /// <remarks>
        /// This property is intended to provide syntactic sugar for replicating the behaviour
        /// of <see cref="Task.FromResult{TResult}(TResult)"/>, but where no specific return
        /// type is required.
        /// </remarks>
        /// <example>
        /// An example of how to use the completed task.
        /// <code>
        /// var task = Completed.Task;
        /// </code>
        /// </example>
        public static Task Task
        {
            get
            {
                return Task.FromResult<object>(null);
            }
        }
    }
}
