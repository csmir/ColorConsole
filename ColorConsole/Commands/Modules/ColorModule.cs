using ColorConsole.Colors;
using ColorConsole.Commands.Attributes;
using ColorConsole.Extensions;
using Commands;
using Spectre.Console;
using System.ComponentModel;

namespace ColorConsole.Commands.Modules
{
    [Name("color")]
    public class ColorModule : ConsoleModuleBase<ColorConsumer>
    {
        [Name("breakdown")]
        [Description("Shows a breakdown of the provided color.")]
        [PrePadding, PostPadding]
        public async Task Get(
            [Description("The color to provide a breakdown of.")] IntegrityColor color)
        {
            var spectreColor = color.Color.ToSpectreColor();

            var table = new Table()
                .Title($"Information about the color {color.Color.Name}")
                .NoBorder()
                .AddColumn("Core")
                .HideHeaders();

            var coreTable = new Table()
                .Expand()
                .RoundedBorder()
                .BorderColor(spectreColor)
                .AddColumn("Raw Values", c => c.PadLeft(2))
                .AddColumn("Breakdown", c => c.PadRight(1));

            var spectrumTable = new Table()
                .Title("Related colors")
                .Expand()
                .RoundedBorder()
                .BorderColor(spectreColor)
                .AddColumn("Name")
                .AddColumn("Hex Value")
                .AddColumn("RGB Value")
                .AddColumn("Integrity");

            var sortedColors = Spectrum.GetSortedSpectrum();

            var range = sortedColors.GetWrappedRange(color, 5).ToArray();

            foreach (var item in range)
            {
                var style = new Style(item.Color.ToSpectreColor());

                spectrumTable.AddRow(
                    new Markup($"{item.Color.Name}", style),
                    new Markup($"{item.ToString(DisplayType.Hex)}", style),
                    new Markup($"{item.ToString(DisplayType.RGB)}", style),
                    new Markup($"{item.Integrity}", style));
            }

            coreTable.AddRow(
                color.RenderCodes(),
                spectreColor.RenderBreakdown());

            table.AddRow(
                coreTable);
            table.AddRow(
                spectrumTable);

            await Send(table);
        }

        [Name("list")]
        [Description("Lists all available colors to the system by name.")]
        [PrePadding, PostPadding]
        public async Task List()
        {
            var colors = Spectrum.GetSortedSpectrum().Items;

            var table = new Table()
                .Title("All available colors:")
                .Expand()
                .AddColumn("1")
                .AddColumn("2")
                .AddColumn("3")
                .AddColumn("4")
                .AddColumn("5")
                .RoundedBorder()
                .BorderColor(Color.Grey)
                .HideHeaders();

            int maxEntriesPerColumn = (int)Math.Ceiling(colors.Count / 5f);
            for (int i = 0; i < maxEntriesPerColumn; i++)
            {
                var markup1 = GetMarkup(colors, i);
                var markup2 = GetMarkup(colors, i + maxEntriesPerColumn);
                var markup3 = GetMarkup(colors, i + (maxEntriesPerColumn * 2));
                var markup4 = GetMarkup(colors, i + (maxEntriesPerColumn * 3));
                var markup5 = GetMarkup(colors, i + (maxEntriesPerColumn * 4));

                table.AddRow(markup1, markup2, markup3, markup4, markup5);
            }

            await Send(table);
        }

        [Name("sort")]
        [Description("Sorts a range of colors into a sorted spectrum.")]
        [PrePadding, PostPadding]
        public Task Sort([Name("gradient-steps")] int gradientSteps = 3)
        {
            return Sort(gradientSteps, [.. Spectrum.GetSortedSpectrum().Items]);
        }

        [Name("sort")]
        [Description("Sorts a range of colors into a sorted spectrum.")]
        [PrePadding, PostPadding]
        public async Task Sort([Name("gradient-steps")] int gradientSteps, [Description("The range of colors to sort.")] params IntegrityColor[] colors)
        {
            var sorted = colors.OrderByDescending(x => x, new ColorComparer()).ToList();

            var table = new Table()
                .Title("Sorted all provided colors:")
                .AddColumn("chart")
                .RoundedBorder()
                .BorderColor(Color.Grey)
                .HideHeaders();

            var barChart = new BarChart()
                .LeftAlignLabel()
                .ShowValues(false);

            for (var i = 0; i < sorted.Count; i++)
            {
                var color = sorted[i];

                if (gradientSteps > 0 && i + 1 != sorted.Count)
                {
                    var gradient = color.Color.GenerateGradient(sorted[i + 1].Color, gradientSteps).ToArray();

                    foreach (IntegrityColor item in gradient[..^1])
                    {
                        barChart.AddItem(
                            label: $"{item.ToString(DisplayType.RGB)} ({item.ToString(DisplayType.Hex)})",
                            value: 100,
                            color: item.Color.ToSpectreColor());
                    }
                }
                else
                {
                    barChart.AddItem(
                        label: $"{color.ToString(DisplayType.RGB)} ({color.ToString(DisplayType.Hex)})",
                        value: 100,
                        color: color.Color.ToSpectreColor());
                }
            }

            table.AddRow(barChart);

            await Send(table);
        }

        private static Markup GetMarkup(List<IntegrityColor> colors, int targetIndex)
        {
            if (colors.Count > targetIndex + 1)
            {
                var color1 = colors[targetIndex];
                return new Markup($"{color1.Color.Name}", new Style(color1.Color.ToSpectreColor()));
            }
            else
                return new Markup($" ");
        }
    }
}
