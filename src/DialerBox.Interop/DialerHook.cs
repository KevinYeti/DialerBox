using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialerBox.Interop
{
    public static class DialerHook
    {
        public static void Init(Keys[] keys)
        {
            _hookKeys.Clear();
            for (int i = 0; i < keys.Length; i++)
            {
                _hookKeys.Add(keys[i], i);
            }
            KeyboardHook.HookKeyboard(Proc);
        }

        internal static Dictionary<Keys, int> _hookKeys = new Dictionary<Keys, int>();

        internal static IntPtr Proc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //System.Diagnostics.Debug.WriteLine("{0}, {1}, {2}", nCode, wParam, lParam);
            if (nCode >= 0 && wParam == (IntPtr)KeyboardHook.WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                var key = (Keys)vkCode;
                //System.Diagnostics.Debug.WriteLine("{0}, {1}, {2}", nCode, wParam, key.ToString());

                if (_hookKeys.ContainsKey(key) && Keys.Control == Control.ModifierKeys)
                {
                    System.Diagnostics.Debug.WriteLine(key.ToString());

                }
            }


            return IntPtr.Zero;
        }
    }
}
