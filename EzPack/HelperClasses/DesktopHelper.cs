using System;
using System.Runtime.InteropServices;

namespace EzPack.HelperClasses
{
    public static class DesktopHelper
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern void SHChangeNotify(int eventId, uint flags, IntPtr item1, IntPtr item2);

        public static void RefreshDesktop()
        {
            const int SHCNE_ASSOCCHANGED = 0x08000000;
            const uint SHCNF_FLUSH = 0x1000;

            SHChangeNotify(SHCNE_ASSOCCHANGED, SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
