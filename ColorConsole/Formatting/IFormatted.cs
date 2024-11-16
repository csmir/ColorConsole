using System.Drawing;

namespace ColorConsole.Formatting
{
    public interface IFormatted
    {
        public Color Color { get; }

        public string Value { get; }

        public string RawValue { get; }

        public string GetMarkupCompatibleValue();
    }
}
