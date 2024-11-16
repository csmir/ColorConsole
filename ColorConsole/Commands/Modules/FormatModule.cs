using ColorConsole.Formatting;
using Commands;
using Spectre.Console;
using System.ComponentModel;

namespace ColorConsole.Commands.Modules
{
    [Name("format")]
    public class FormatModule : ModuleBase<ColorConsumer>
    {
        [Name("add")]
        [Description("Adds a format to the gradient formatter.")]
        public void Add(
            [Description("The key by which the format should be saved.")] string name, 
            [Remainder, Description("The format which should be used to create a gradient.")] string format)
        {
            Formatter.AddFormatProvider(name, format);

            AnsiConsole.MarkupLine($"[grey]Added custom formatter by key:[/] [orange1]'{name}'[/]");
        }
    }
}
