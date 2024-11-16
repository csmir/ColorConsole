namespace ColorConsole.Colors
{
    public class ColorComparer : IComparer<IntegrityColor>
    {
        public int Compare(IntegrityColor x, IntegrityColor y)
        {
            if (x.Integrity > y.Integrity)
                return 1;
            if (x.Integrity < y.Integrity)
                return -1;
            return 0;
        }
    }
}
