using ColorConsole.Commands;
using ColorConsole.Commands.Converters;
using Commands;
using Spectre.Console;

var builder = CommandManager.CreateDefaultBuilder()
    .AddResultResolver((consumer, result, services) =>
    {
        if (!result.Success)
        {
            consumer.Send("[red]An error occurred while executing the command:[/] " + result.Exception!.Message);
        }
    })
    .AddTypeConverter(new ColorConverter());

var manager = builder.Build();

while (true)
{
    var command = AnsiConsole.Ask<string>("[grey]Command: ([/][orange1]'help'[/] [grey]for more info)[/]");

    if (command is null)
        break;

    await manager.Execute(new ColorConsumer(), command, new CommandOptions()
    {
        AsyncMode = AsyncMode.Await,
    });
}