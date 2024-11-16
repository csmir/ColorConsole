
using ColorConsole.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace ColorConsole.Colors
{
    public readonly struct IntegrityColor
    {
        private const double _rTarget = .241;
        private const double _gTarget = .691;
        private const double _bTarget = .068;

        public Color Color { get; }

        public byte R { get; }

        public byte G { get; }

        public byte B { get; }

        public double Hue { get; }

        public double Luminosity { get; }

        public double Value { get; }

        public double Saturation { get; }

        public double Integrity { get; }

        private IntegrityColor(Color color, double hue, double lum, double val, double sat)
        {
            R = color.R;
            G = color.G;
            B = color.B;

            Color = color;
            Hue = hue;
            Luminosity = lum;
            Value = val;
            Saturation = sat;
            Integrity = hue + lum + val + sat;
        }

        public static implicit operator IntegrityColor(Color color)
            => Create(color);

        public override string ToString()
            => ToString(ColorType.Hex);

        public string ToString(ColorType type)
        {
            switch (type)
            {
                case ColorType.UInt32:
                    string hexValue = $"{R:X2}{G:X2}{B:X2}";
                    return Convert.ToUInt32(hexValue, 16).ToString();
                case ColorType.Hex:
                    return $"{R:X2}{G:X2}{B:X2}";
                case ColorType.RGB:
                    return $"{R}, {G}, {B}";
                default:
                    throw new NotImplementedException();
            }
        }

        public static IntegrityColor Create(Color color, int repetitions = 8)
        {
            var (r, g, b) = ((float)color.R, (float)color.G, (float)color.B);
            var lum = Math.Sqrt(_rTarget * r + _gTarget * g + _bTarget * b);
            var (h, s, v) = color.ToHSV();

            h *= repetitions;
            lum *= repetitions;
            v *= repetitions;

            if (h % 2 is 1)
            {
                v = repetitions - v;
                lum = repetitions - lum;
            }

            return new(color, h, lum, v, s);
        }

        public static bool TryParse(string? value, [NotNullWhen(true)] out IntegrityColor? color)
        {
            color = default;
            if (string.IsNullOrEmpty(value))
                return false;

            var param = value.Split(',', StringSplitOptions.TrimEntries);
            var rawInput = value.Replace("#", "");

            if (param.Length is 3)
            {
                if (!byte.TryParse(param[0], out byte r))
                    return false;
                if (!byte.TryParse(param[1], out byte g))
                    return false;
                if (!byte.TryParse(param[2], out byte b))
                    return false;

                color = Color.FromArgb(r, g, b);
                return true;
            }
            else if (rawInput.Length is 6)
            {
                try
                {
                    var @uint = Convert.ToUInt32(rawInput, 16);
                    color = Color.FromArgb((int)@uint);
                    return true;
                }
                catch
                {
                    try
                    {
                        color = Color.FromName(value);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else if (uint.TryParse(value, out uint integer))
            {
                try
                {
                    color = Color.FromArgb((int)integer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    color = Color.FromName(value);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
