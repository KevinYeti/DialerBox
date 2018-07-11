using DialerBox.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using SystemCommonLibrary.API.Win;

namespace UnitTest
{
    [TestClass]
    public class HookTest
    {
        [TestMethod]
        public void TestHook()
        {
            KeyboardHook.HookKeyboard((int nCode, IntPtr wParam, IntPtr lParam) => {
                Console.WriteLine("nCode:{0} wParam:{1} lParam:{2}", nCode, wParam, lParam);

                return IntPtr.Zero;
            });

            KeyboardHook.UnhookKeyboard();
        }
    }
}
