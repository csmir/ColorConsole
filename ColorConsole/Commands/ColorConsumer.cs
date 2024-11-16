using Commands;
using Spectre.Console.Rendering;

namespace ColorConsole.Commands
{
    public sealed class ColorConsumer : ConsoleConsumerBase
    {
        public override Task Send(object response)
        {
            if (response is IRenderable renderable)
            {
                Console.Write(renderable);
            }
            else
            {
                return base.Send(response);
            }

            return Task.CompletedTask;
        }
    }
}
