using Commands;
using Commands.Reflection;
using Spectre.Console;
using System.ComponentModel;

namespace ColorConsole.Commands.Modules
{
    [Name("help")]
    public class HelpModule : ConsoleModuleBase<ColorConsumer>
    {
        public async void Help()
        {
            await Send();

            var commands = Manager.GetCommands().Where(x => !x.FullName.Contains("help")).OrderBy(x => x.Name).ThenBy(x => x.Arguments.Length);

            var table = new Table()
                .Title("All commands")
                .AddColumn("Command", c => c.Centered())
                .AddColumn("Parameters", c => c.Centered())
                .SimpleBorder();

            foreach (var command in commands)
            {
                var commandTable = new Table()
                    .AddColumn("", c => c.NoWrap().RightAligned().Width(15))
                    .AddColumn("", c => c.Width(30))
                    .HideHeaders()
                    .SimpleBorder();

                commandTable.AddRow(
                        new Markup($"{command.FullName.TrimEnd()}:", new(Color.Orange1, decoration: Decoration.Bold | Decoration.Underline)),
                        new Markup($"{command.GetAttribute<DescriptionAttribute>()?.Description}"));

                var paramTable = new Table()
                    .AddColumn("Name", c => c.Width(15).NoWrap())
                    .AddColumn("Required?")
                    .AddColumn("Description")
                    .SimpleBorder()
                    .Expand();

                foreach (var parameter in command.Arguments)
                {
                    paramTable.AddRow(
                        new Markup($"{parameter!.Name}", new(Color.Orange1, decoration: Decoration.Bold | Decoration.Underline)),
                        new Markup($"{(parameter.IsOptional ? "No" : "Yes")}", new(parameter.IsOptional ? Color.Yellow : Color.Red)), 
                        new Markup($"{parameter.Attributes.Where(x => x is DescriptionAttribute).Select(x => x as DescriptionAttribute).FirstOrDefault()?.Description}"));
                }

                if (paramTable.Rows.Count == 0)
                    paramTable.AddEmptyRow()
                        .HideHeaders();

                table.AddRow(
                    commandTable,
                    paramTable);
            }

            await Send(table);
        }
    }
}
