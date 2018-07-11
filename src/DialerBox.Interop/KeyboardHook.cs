using System;
using System.Diagnostics;
using SystemCommonLibrary.API.Win;

namespace DialerBox.Interop
{
    public static class KeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;

        private static IntPtr _hHook = IntPtr.Zero;

        public static void HookKeyboard(HookProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                var module = KERNEL32.GetModuleHandle(curModule.ModuleName);

                UnhookKeyboard();

                HookProc hook = (int nCode, IntPtr wParam, IntPtr lParam) => {
                    proc(nCode, wParam, lParam);

                    return USER32.CallNextHookEx(_hHook, nCode, wParam, lParam);
                };

                _hHook = USER32.SetWindowsHookEx(WH_KEYBOARD_LL, hook, module, 0);
            }
        }

        public static bool UnhookKeyboard()
        {
            bool result = true;
            if (_hHook != IntPtr.Zero)
            {
                result = USER32.UnhookWindowsHookEx(_hHook);
                _hHook = IntPtr.Zero;
            }

            return result;
        }
    }
}
