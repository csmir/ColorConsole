using System.Drawing;

namespace ColorConsole.Extensions
{
    public static class ColorHelper
    {
        public static Tuple<double, float, double> ToHSV(this Color color)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            return new Tuple<double, float, double>(color.GetHue(), (float)(max == 0 ? 0 : 1d - 1d * min / max), max / 255d);
        }
    }
}
