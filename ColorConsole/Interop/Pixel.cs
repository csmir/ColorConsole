using ColorConsole.Colors;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ColorConsole.Interop
{
    public static partial class Pixel
    {
        private static readonly Bitmap _screenPixel = new(1, 1, PixelFormat.Format32bppArgb);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [LibraryImport("gdi32.dll", SetLastError = true)]
        public static partial int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        public static IntegrityColor GetColor(Point location)
        {
            lock (_screenPixel)
            {
                using (Graphics gdest = Graphics.FromImage(_screenPixel))
                {
                    using Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero);

                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }

                return _screenPixel.GetPixel(0, 0);
            }
        }
    }
}