using System.Drawing;

namespace ColorConsole.Formatting
{
    public static class Formatter
    {
        public static Dictionary<string, string> FormatProvider { get; } = [];

        public static IEnumerable<IFormatted> Format(this IEnumerable<Color> colors, string? text, string type)
        {
            if (string.IsNullOrEmpty(text))
                return FormatEmpty(colors.ToArray(), type);
            return FormatFilled(colors.ToArray(), type, text);
        }

        private static IEnumerable<IFormatted> FormatFilled(Color[] colors, string type, string text)
        {
            float stepsPerColor = text.Length / (float)colors.Length;

            int minIndex = 0;
            for (int i = 1; i <= colors.Length; i++)
            {
                int maxIndex = (int)(stepsPerColor * i);
                var characters = text[minIndex..maxIndex];
                minIndex = maxIndex;

                if (Enum.TryParse<FormatType>(type, true, out var formatType))
                {
                    switch (formatType)
                    {
                        case FormatType.Terraria:
                            yield return new TerrariaFormatted(colors[i - 1], characters, false);
                            break;
                        case FormatType.ScrapMechanic:
                            yield return new ScrapMechanicFormatted(colors[i - 1], characters, false);
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                }
                else
                    yield return new CustomFormatted(type, colors[i - 1], characters, false);
            }
        }

        private static IEnumerable<IFormatted> FormatEmpty(Color[] colors, string type)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (Enum.TryParse<FormatType>(type, true, out var formatType))
                {
                    switch (formatType)
                    {
                        case FormatType.None:
                            yield return new NotFormatted(colors[i], "N/A");
                            break;
                        case FormatType.Terraria:
                            yield return new TerrariaFormatted(colors[i], "N/A", true);
                            break;
                        case FormatType.ScrapMechanic:
                            yield return new ScrapMechanicFormatted(colors[i], "N/A", true);
                            break;
                    }
                }
                else
                    yield return new CustomFormatted(type, colors[i], "N/A", true);
            }
        }

        public static void AddFormatProvider(string name, string formattableString)
            => FormatProvider.TryAdd(name, formattableString);

        public static List<string> GetFormatNames()
            => [.. FormatProvider.Keys];
    }
}
