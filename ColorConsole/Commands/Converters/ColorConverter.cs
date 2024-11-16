using ColorConsole.Colors;
using Commands;
using Commands.Converters;
using Commands.Reflection;

namespace ColorConsole.Commands.Converters
{
    public class ColorConverter : TypeConverterBase<IntegrityColor>
    {
        public override async ValueTask<ConvertResult> Evaluate(ConsumerBase consumer, IArgument argument, object? value, IServiceProvider services, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (value == null)
            {
                return Error("No value provided.");
            }

            if (IntegrityColor.TryParse(value?.ToString(), out var color))
            {
                return Success(color);
            }

            return Error("Invalid color format.");
        }
    }
}
