using ColorConsole.Colors;
using ColorConsole.Interop;
using Commands;
using Spectre.Console;
using System.ComponentModel;

namespace ColorConsole.Commands.Modules
{
    [Name("pick")]
    public class PickModule : ConsoleModuleBase<ColorConsumer>
    {
        public string Get(bool toClipboard = true)
        {
            var format = Select(new SelectionPrompt<DisplayType>()
                    .PageSize(3)
                    .AddChoices([DisplayType.UInt32, DisplayType.Hex, DisplayType.RGB])
                    .Title("[grey]What format do you want to copy?[/]"));

            return GetColorString(toClipboard, format);
        }

        [Name("hex")]
        [Description("Gets the hex value of the current cursor position and copies it to the clipboard.")]
        public static string Hex(bool toClipboard = true)
            => GetColorString(toClipboard, DisplayType.Hex);

        [Name("rgb")]
        [Description("Gets the RGB value of the current cursor position and copies it to the clipboard.")]
        public static string Rgb(bool toClipboard = true)
            => GetColorString(toClipboard, DisplayType.RGB);

        private static string GetColorString(bool toClipboard, DisplayType type)
        {
            if (Cursor.TryGetCursorPosition(out var point))
            {
                var color = Pixel.GetColor(point);
                var colorString = color.ToString(type);

                if (toClipboard)
                {
                    Clipboard.SetText(colorString);
                }

                return $"[grey]{type} value of the color at cursor position {point}:[/] [orange1]{colorString}[/]";
            }
            else
            {
                return "[red]Could not get current cursor position.[/].";
            }
        }
    }
}
