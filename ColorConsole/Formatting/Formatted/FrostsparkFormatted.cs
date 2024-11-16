using System.Drawing;

namespace ColorConsole.Formatting
{
    public readonly struct FrostsparkFormatted : IFormatted
    {
        public Color Color { get; }

        public string Value { get; }

        public string RawValue { get; }

        public static string Format
            => "<color:hex={0:X2}{1:X2}{2:X2}>{3}<color:pop>";

        public FrostsparkFormatted(Color color, string rawValue, bool isNoneValue)
        {
            Color = color;
            RawValue = rawValue;
            Value = string.Format(Format, color.R, color.G, color.B, isNoneValue ? " " : rawValue);
        }

        public string GetMarkupCompatibleValue()
        {
            return Value;
        }
    }
}
