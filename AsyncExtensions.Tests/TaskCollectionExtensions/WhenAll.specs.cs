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
                ._(() =>
                {
                    allTask = tasks.WhenAll();
                });

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

        [Scenario]
        public void CompletedTasksWithType(
            TaskCompletionSource<int> tcs1,
            TaskCompletionSource<int> tcs2,
            TaskCompletionSource<int> tcs3,
            List<Task<int>> tasks,
            Task<int[]> allTask)
        {
            "Given a set of task completion sources"
                ._(() =>
                {
                    tcs1 = new TaskCompletionSource<int>();
                    tcs2 = new TaskCompletionSource<int>();
                    tcs3 = new TaskCompletionSource<int>();
                });

            "And given a collection of tasks from them"
                ._(() =>
                {
                    tasks = new List<Task<int>>();

                    tasks.Add(tcs1.Task);
                    tasks.Add(tcs2.Task);
                    tasks.Add(tcs3.Task);
                });

            "When we request an overall task"
                ._(() =>
                {
                    allTask = tasks.WhenAll();
                });

            "Then the task should not be complete yet"
                ._(() => allTask.IsCompleted.Should().BeFalse());

            "When two of the tasks are completed"
                ._(() =>
                {
                    tcs1.SetResult(1);
                    tcs2.SetResult(2);
                });

            "Then the task should still not be completed"
                ._(() => allTask.IsCompleted.Should().BeFalse());

            "When the final task is completed"
                ._(() => tcs3.SetResult(3));

            "Then the task should scomplete"
                ._(() => allTask.IsCompleted.Should().BeTrue());

            "Then the results should be correct"
                ._(() =>
                {
                    allTask.Result[0].ShouldBeEquivalentTo(1);
                    allTask.Result[1].ShouldBeEquivalentTo(2);
                    allTask.Result[2].ShouldBeEquivalentTo(3);
                });
        }
    }
}
