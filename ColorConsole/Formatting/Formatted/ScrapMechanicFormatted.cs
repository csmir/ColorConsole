﻿using System.Drawing;

namespace ColorConsole.Formatting
{
    public readonly struct ScrapMechanicFormatted : IFormatted
    {
        public Color Color { get; }

        public string Value { get; }

        public string RawValue { get; }

        public static string Format
            => "#{0:X2}{1:X2}{2:X2}{3}";

        public ScrapMechanicFormatted(Color color, string rawValue, bool isNoneValue)
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
