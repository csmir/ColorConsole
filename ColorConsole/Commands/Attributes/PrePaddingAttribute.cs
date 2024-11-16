using Commands;
using Commands.Conditions;
using Commands.Reflection;

namespace ColorConsole.Commands.Attributes
{
    public class PrePaddingAttribute : PreconditionAttribute
    {
        public override ValueTask<ConditionResult> Evaluate(ConsumerBase consumer, CommandInfo command, IServiceProvider services, CancellationToken cancellationToken)
        {
            if (consumer is ColorConsumer colorConsumer)
            {
                colorConsumer.Send();
            }

            return ValueTask.FromResult(Success());
        }
    }
}
