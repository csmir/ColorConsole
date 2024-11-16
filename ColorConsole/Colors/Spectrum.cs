using System.Drawing;

namespace ColorConsole.Colors
{
    public class Spectrum
    {
        public List<IntegrityColor> Items { get; }

        private Spectrum(List<IntegrityColor> colors)
        {
            Items = colors.OrderByDescending(x => x, new ColorComparer()).ToList();
        }

        public IEnumerable<IntegrityColor> GetWrappedRange(IntegrityColor color, int wrapAround = 4)
        {
            var match = GetClosestMatch(color.Integrity);
            var matchIndex = Items.IndexOf(match);

            while ((matchIndex + wrapAround) > Items.Count)
                matchIndex--;

            while ((matchIndex - wrapAround) < 0)
                matchIndex++;

            for (int i = matchIndex - wrapAround; i < matchIndex + wrapAround; i++)
            {
                yield return Items[i];
            }
        }

        public IntegrityColor GetClosestMatch(double integrity)
        {
            IntegrityColor closest = default;

            for (int i = 0; i < Items.Count; i++)
            {
                var foundIntegrity = Items[i].Integrity;

                if (foundIntegrity > integrity)
                {
                    closest = Items[i];
                }
            }

            return closest;
        }

        public static Spectrum GetSortedSpectrum()
        {
            List<IntegrityColor> colors = [];

            var names = Enum.GetNames<KnownColor>();

            foreach (var name in names)
                colors.Add(Color.FromName(name));

            return new(colors);
        }
    }
}
