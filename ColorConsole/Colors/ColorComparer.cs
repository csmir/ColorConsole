namespace ColorConsole.Colors
{
    public class ColorComparer : IComparer<IntegrityColor>
    {
        public int Compare(IntegrityColor x, IntegrityColor y)
        {
            return (int)(x.Integrity - y.Integrity);
        }
    }
}
