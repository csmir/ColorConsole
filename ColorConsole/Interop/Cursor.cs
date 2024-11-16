using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ColorConsole.Interop
{
    public static partial class Cursor
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [LibraryImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetCursorPos(out PT point);

        [StructLayout(LayoutKind.Sequential), EditorBrowsable(EditorBrowsableState.Never)]
        public struct PT
        {
            public int X;

            public int Y;

            public static implicit operator Point(PT point)
                => new(point.X, point.Y);
        }

        public static bool TryGetCursorPosition(out Point point)
        {
            point = default!;
            if (GetCursorPos(out var pt))
            {
                point = pt;
                return true;
            }
            return false;
        }
    }
}