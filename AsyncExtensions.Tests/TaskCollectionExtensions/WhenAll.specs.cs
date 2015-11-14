using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xbehave;

namespace AsyncExtensions.Tests.TaskCollectionExtensions
{
    public class WhenAllFeature
    {
        [Scenario]
        public void CompletedTasksNoType(
            TaskCompletionSource<object> tcs1,
            TaskCompletionSource<object> tcs2,
            TaskCompletionSource<object> tcs3,
            IEnumerable<Task> tasks,
            Task allTask)
        {
            "Given a set of task completion sources"
                ._(() =>
                {
                    tcs1 = new TaskCompletionSource<object>();
                    tcs2 = new TaskCompletionSource<object>();
                    tcs3 = new TaskCompletionSource<object>();
                });

            "And given a collection of tasks from them"
                ._(() =>
                {
                    var taskList = new List<Task>();

                    taskList.Add(tcs1.Task);
                    taskList.Add(tcs2.Task);
                    taskList.Add(tcs3.Task);

                    tasks = taskList;
                });

            "When we request an overall task"
                ._(() => allTask = tasks.WhenAll());

            "Then the task should not be complete yet"
                ._(() => allTask.IsCompleted.Should().BeFalse());

            "When two of the tasks are completed"
                ._(() =>
                {
                    tcs1.SetResult(null);
                    tcs2.SetResult(null);
                });

            "Then the task should still not be completed"
                ._(() => allTask.IsCompleted.Should().BeFalse());

            "When the final task is completed"
                ._(() => tcs3.SetResult(null));

            "Then the task should scomplete"
                ._(() => allTask.IsCompleted.Should().BeTrue());
        }
    }
}
