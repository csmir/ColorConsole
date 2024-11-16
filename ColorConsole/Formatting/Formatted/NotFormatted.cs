using System.Drawing;

namespace ColorConsole.Formatting
{
    public readonly struct NotFormatted : IFormatted
    {
        public Color Color { get; }

        public string Value { get; }

        public string RawValue { get; }

        public static string Format
            => "#{0:X2}{1:X2}{2:X2}";

        public NotFormatted(Color color, string rawValue)
        {
            Color = color;
            RawValue = rawValue;
            Value = string.Format(Format, color.R, color.G, color.B);
        }

        public string GetMarkupCompatibleValue()
        {
            return Value;
        }
    }
}
