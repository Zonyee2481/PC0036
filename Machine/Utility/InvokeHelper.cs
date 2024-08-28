using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public static class InvokeHelper
    {
        public static void Enable<T>(T CTrl, bool bState)
        {
            var ctrl = CTrl as Control;
            if (!ctrl.InvokeRequired)
            {
                ctrl.Enabled = bState;
            }
            else
            {
                ctrl.Invoke(new EventHandler((delegate
                {
                    ctrl.Enabled = bState;
                })));
            }
        }

        public static void ForeColor<T>(T CTrl, Color clr)
        {
            var ctrl = CTrl as Control;
            if (!ctrl.InvokeRequired)
            {
                ctrl.ForeColor = clr;
            }
            else
            {
                ctrl.Invoke(new EventHandler((delegate
                {
                    ctrl.ForeColor = clr;
                })));
            }
        }

        public static void Text<T>(T CTrl, string txt)
        {
            var ctrl = CTrl as Control;            
            if (!ctrl.InvokeRequired)
            {
                ctrl.Text = txt;
                ctrl.BackColor = txt.Contains("Bypass")? Color.Gray: txt.Contains("Pass") == true? Color.Green : txt.Contains("New") == true ? Color.Yellow :txt.Contains("Fail") == true ? Color.Red : Color.AliceBlue;
            }
            else
            {
                ctrl.Invoke(new EventHandler((delegate
                {
                    ctrl.Text = txt;
                    ctrl.BackColor = txt.Contains("Bypass") ? Color.Gray : txt.Contains("Pass") == true ? Color.Green : txt.Contains("New") == true ? Color.Yellow : txt.Contains("Fail") == true ? Color.Red : Color.AliceBlue;
                })));
            }
        }



        public static void BackColor<T>(T CTrl, Color clr)
        {
            var ctrl = CTrl as Control;
            if (!ctrl.InvokeRequired)
            {
                ctrl.BackColor = clr;
            }
            else
            {
                ctrl.Invoke(new EventHandler((delegate
                {
                    ctrl.BackColor = clr;
                })));
            }
        }

        public static void Focus<T>(T CTrl, bool bState)
        {
            var ctrl = CTrl as Control;
            if (!ctrl.InvokeRequired)
            {
                if (bState)
                {
                    ctrl.Focus();
                    ctrl.Select();
                }
            }
            else
            {
                ctrl.Invoke(new EventHandler((delegate
                {
                    if (bState)
                    {
                        ctrl.Focus();
                        ctrl.Select();
                    }
                })));
            }
        }

        public static void Visible<T>(T CTrl, bool bState)
        {
            var ctrl = CTrl as Control;
            if (!ctrl.InvokeRequired)
            {
                ctrl.Visible = bState;
            }
            else
            {
                ctrl.Invoke(new EventHandler((delegate
                {
                    ctrl.Visible = bState;
                })));
            }
        }
    }

    public static class ExtensionMethods
    {
        public static IEnumerable<T> GetControls<T>(this Control root) where T : Control
        {
            foreach (Control control in root.Controls)
            {
                T test = control as T;
                if (test != null) yield return test;
                foreach (T subcontrol in GetControls<T>(control))
                    yield return subcontrol;
            }
        }
    }
}
