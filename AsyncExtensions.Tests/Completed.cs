using System.Threading.Tasks;
using FluentAssertions;
using Xbehave;

namespace AsyncExtensions.Tests
{
    public class CompletedFeature
    {
        [Scenario]
        public void CompletedTask(Task task)
        {
            "Given a completed task"
                ._(() => task = Completed.Task);

            "Then it should already be completed"
                ._(() => task.IsCompleted.Should().BeTrue());
        }
    }
}