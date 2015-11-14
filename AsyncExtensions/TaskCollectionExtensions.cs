using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncExtensions
{
    /// <summary>
    /// Extensions for collections of tasks.
    /// </summary>
    public static class TaskCollectionExtensions
    {
        /// <summary>
        /// Returns a task that completes when all of the tasks currently in
        /// the collection of tasks complete.
        /// </summary>
        /// <remarks>
        /// Adding further tasks to the collection after this method has been
        /// called will not be taken account of when waiting for tasks
        /// to complete.
        /// </remarks>
        /// <param name="tasks">The collection of tasks.</param>
        /// <returns>A task which completes when all tasks in the collection complete.</returns>
        public static Task WhenAll(this IEnumerable<Task> tasks)
        {
            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Returns a task that completes when all of the tasks currently in
        /// the collection of tasks complete.
        /// </summary>
        /// <remarks>
        /// Adding further tasks to the collection after this method has been
        /// called will not be taken account of when waiting for tasks
        /// to complete.
        /// This overload supports tasks which have a return type.
        /// </remarks>
        /// <param name="tasks">The collection of tasks.</param>
        /// <returns>A task which completes when all tasks in the collection complete.</returns>
        public static Task<T[]> WhenAll<T>(this IEnumerable<Task<T>> tasks)
        {
            return Task.WhenAll(tasks);
        }
    }
}
