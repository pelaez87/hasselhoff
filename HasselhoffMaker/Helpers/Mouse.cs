using System;
using System.Runtime.InteropServices;

namespace HasselhoffMaker.Helpers
{
    internal static class Mouse
    {
        [DllImport("user32.dll")]
        public static extern Int32 SwapMouseButton(Int32 bSwap);
    }
}