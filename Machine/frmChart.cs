using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infrastructure;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Machine
{
    public partial class frmChart : UserControl
    {
        public ES.UserCtrl.UserCtrlTools UserCtrl = new ES.UserCtrl.UserCtrlTools();
        public static LiveCharts.WinForms.PieChart chart;
        static Timer timer;
        public static frmChart Page = new frmChart();
        public frmChart()
        {
            InitializeComponent();
            
            timer = new Timer();
            timer.Interval = 2000; // 2 seconds
            timer.Tick += UpdateChart;
            timer.Start();
        }

        public void ShowPage(Control parent)
        {
            DrawPieChart();
            //this.Controls.Clear();
            //this.Controls.Add(chart);
            this.motionIO_groupBox.Controls.Clear();
            this.motionIO_groupBox.Controls.Add(chart);
            this.Parent = parent;
            this.Show();
        }

        public void HidePage()
        {
            Visible = false;
        }
        public static void DrawPieChart()
        {
            // Create a new chart object
            chart = new LiveCharts.WinForms.PieChart();

            // Set chart data
            chart.Series = new SeriesCollection
    {
        new PieSeries
        {
            Title = "Unloading", // Example title, replace with appropriate label
            Values = new ChartValues<double> { UnloadingRatio }, // Example data point, replace with your data
            DataLabels = true,
            LabelPoint = point => $"{point.Y} ({UnloadingCnt})"
        },
        new PieSeries
        {
            Title = "OCR Fail", // Example title, replace with appropriate label
            Values = new ChartValues<double> { OCRFailRatio }, // Example data point, replace with your data
            DataLabels = true,
            LabelPoint = point => $"{point.Y} ({OcrFailCnt})"
        },
        new PieSeries
        {
            Title = "Top Keyence Fail", // Example title, replace with appropriate label
            Values = new ChartValues<double> { TopKeyFailRatio }, // Example data point, replace with your data
            DataLabels = true,
            LabelPoint = point => $"{point.Y} ({TopKeyFailCnt})"
        },
        new PieSeries
        {
            Title = "Btm Keyence Fail", // Example title, replace with appropriate label
            Values = new ChartValues<double> { BtmKeyFailRatio }, // Example data point, replace with your data
            DataLabels = true,
            LabelPoint = point => $"{point.Y} ({BtmKeyFailCnt})"
        },
    };

            // Optionally, if you want to modify values for the second series after creating it
            // ((PieSeries)chart.Series[1]).Values = new ChartValues<double> { TopKeyFailRatio, BtmKeyFailRatio };

            // Set chart legend
            chart.LegendLocation = LegendLocation.Right;

            //// Create a new form
            //var form = new Form();
            //form.Text = "Pie Chart";

            // Add chart to form
            chart.Dock = DockStyle.Fill;
            

            
            //Application.Run(form);
        }


        static double UnloadingCnt = 0;
        static double RejectCnt = 0;
        static double OcrFailCnt = 0;
        static double TopKeyFailCnt = 0;
        static double BtmKeyFailCnt = 0;
        static double OveralCnt = 0;

        static double UnloadingRatio = 0;
        static double OCRFailRatio = 0;
        static double TopKeyFailRatio = 0;
        static double BtmKeyFailRatio = 0;

        static void UpdateChart(object sender, EventArgs e)
        {
            //DrawPieChart();

            UnloadingCnt = frmMain.SequenceRun.GetProdCntNum((int)TotalModule.UnloadingStn);
            RejectCnt = frmMain.SequenceRun.GetProdCntNum((int)TotalModule.RejectBin);
            OcrFailCnt = frmMain.SequenceRun.GetProdCntNum((int)TotalModule.OCR_Reader);
            TopKeyFailCnt = frmMain.SequenceRun.GetProdCntNum((int)TotalModule.KeyenceTop);
            BtmKeyFailCnt = frmMain.SequenceRun.GetProdCntNum((int)TotalModule.KeyenceBtm);

            //100%
            OveralCnt = UnloadingCnt + RejectCnt;
            //Total Pass
            UnloadingRatio = OveralCnt == 0 ? 0:UnloadingCnt / OveralCnt * 100;
            //OCRFail
            OCRFailRatio = RejectCnt == 0 ? 0 : OcrFailCnt / OveralCnt * 100;
            //TopKeyenceFail
            TopKeyFailRatio = RejectCnt == 0 ? 0 : TopKeyFailCnt / OveralCnt * 100;
            //BtmKeyenceFail
            BtmKeyFailRatio = RejectCnt == 0 ? 0 : BtmKeyFailCnt / OveralCnt * 100;

        }
       
        private void button_UpdChart_Click(object sender, EventArgs e)
        {
            DrawPieChart();
            this.motionIO_groupBox.Controls.Clear();
            this.motionIO_groupBox.Controls.Add(chart);
        }
    }
}
