using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class frmDisplay : Form
    {
        public frmDisplay()
        {
            InitializeComponent();
        }
        

        public frmDisplay(Station stn, params StringBuilder[] msgs)
        {
            lbl_Msg = new Label();
            for (int x = 0; x < msgs.Length; x++)
            {
                lbl_Msg.Text += stn.stations[x].ToString();
                lbl_Msg.Text += msgs[x].ToString();
                lbl_Msg.Text += "\n";
            }
            Controls.Add(lbl_Msg);
            Task Waitdone = Task.Run(() => WaitDone(msgs));
            this.Show();

            Task.WaitAny(Waitdone);
        }

        public async void WaitDone(params StringBuilder[] msgs)
        {
            int count = msgs.Length;
            bool alldone = false;

            while (!alldone)
            {
                for (int x = 0; x < count; x++)
                {
                    if(msgs[x].ToString() == "Done")
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                        break;
                    }
                }
            }

            
        }
    }

    public class Station
    {
        public string[] stations;
    }
}
