using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Infrastructure;

namespace Machine
{
    public partial class frmLotInformation : Form
    {
        public frmLotInformation()
        {
            InitializeComponent();
        }

        void UpdateDisplay()
        {
            txtDateTime.Text = DateTime.Now.ToString("yy") + "/" + GDefine.GetMonth();
            //txt2DID.Text = GenerateBarcode();
            btnStartLot.Enabled = !TaskLotInfo.LotInfo.Activated;
            btnEndLot.Enabled = TaskLotInfo.LotInfo.Activated;
            txtCounterNo.ReadOnly = TaskLotInfo.LotInfo.Activated;
            txtProductNo.ReadOnly = TaskLotInfo.LotInfo.Activated;
            txtLotNo.ReadOnly = TaskLotInfo.LotInfo.Activated;
            cmbProductNo.Visible = !TaskLotInfo.LotInfo.Activated;
            txtProductNo.Text = SM.RecipeName;
            txtProductNo.Visible = TaskLotInfo.LotInfo.Activated;
            if (TaskLotInfo.LotInfo.Activated)
            {
                txtLotNo.Text = TaskDeviceRecipe._LotInfo.LotNum;
                txtCounterNo.Text = TaskDeviceRecipe._LotInfo.Counter.ToString();
                txt2DID.Text = GenerateBarcode();
            }
        }

        public static string GenerateBarcode()
        {
            string FixCode = GDefine.WP;
            string _sYear = DateTime.Now.ToString("yy");
            string _sMonth = GDefine.GetMonth();
            string _sRowCol = TaskDeviceRecipe._iRow.ToString() + TaskDeviceRecipe._iCol.ToString();
            string _OutsourceCode = GDefine.OutSource;
            string _RunningNum = GDefine.Combinations[TaskDeviceRecipe._LotInfo.Counter - 1];

            return FixCode + _sYear + _sMonth + _sRowCol + _OutsourceCode + _RunningNum;
        }

        private void txtLotNo_TextChanged(object sender, EventArgs e)
        { 
            TaskDeviceRecipe._LotInfo.LotNum = txtLotNo.Text;
        }

        private void cmbProductNo_SelectedIndexChanged(object sender, EventArgs e)
        {            
            TaskDeviceRecipe._LotInfo.PartNum = cmbProductNo.GetItemText(cmbProductNo.SelectedItem);
            TaskDeviceRecipe.LoadDeviceRecipe(GDefine.DevicePath, TaskDeviceRecipe._LotInfo.PartNum + GDefine.DeviceRecipeExt);
            TaskDeviceRecipe._iRow = TaskDeviceRecipe._LotInfo._RecipeInfo._iRow;
            TaskDeviceRecipe._iCol = TaskDeviceRecipe._LotInfo._RecipeInfo._iCol;  
            TaskLotInfo.LotInformation();
            txtCounterNo.Text = TaskDeviceRecipe._LotInfo.Counter.ToString();
        }

        private void frmLotInformation_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            TaskLotInfo.ComboBoxAddProductNum(cmbProductNo, GDefine.DevicePath);
        }

        private void tmrUpdateDisplay_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;

            tmrUpdateDisplay.Enabled = false;
            UpdateDisplay();
            tmrUpdateDisplay.Enabled = true;
        }

        private void btnStartLot_Click(object sender, EventArgs e)
        {
            Enabled = false;
            if (!TaskLotInfo.StartLot())
            {
                DialogResult = DialogResult.Abort;
                this.Close();
                return;
            }
            if (!File.Exists(GDefine.DevicePath + "\\" + TaskDeviceRecipe._LotInfo.PartNum + GDefine.DeviceRecipeExt))
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Device Recipe Not Exist! Fail To Start Lot! Please create Device Recipe For " + TaskDeviceRecipe._LotInfo.PartNum,
                        frmMessaging.TMsgBtn.smbOK | frmMessaging.TMsgBtn.smbAlmClr);
                while (!frmMain.frmMsg.ShowMsgClear(MsgID))
                {
                    Application.DoEvents();
                }
                switch (frmMain.frmMsg.GetMsgRes(MsgID))
                {
                    case frmMessaging.TMsgRes.smrOK:
                        return;
                }
                return;
            }
            //Load Lot Counter
            //Load Device Recipe
            //Set Lot To sequence
            //Set recipe teach point
            SM.McAssistCount = 0;
            TaskLotInfo.LotInformation();
            var strPartnum = TaskDeviceRecipe._LotInfo.PartNum.Substring(0, Math.Min(7, TaskDeviceRecipe._LotInfo.PartNum.Length));
            var Part = TaskDeviceRecipe.Part._Partnumber.FirstOrDefault(x => x.PartNo.Contains(strPartnum));
            TaskDeviceRecipe._LotInfo.PartName = Part == null? TaskDeviceRecipe._LotInfo.PartNum : Part.PartName;
            TaskDeviceRecipe.LoadDeviceRecipe(GDefine.DevicePath, TaskDeviceRecipe._LotInfo.PartNum + GDefine.DeviceRecipeExt);
            TaskDeviceRecipe._LotInfo.Combinations = GDefine.Combinations;
            frmMain.SequenceRun.RecipeInfo(TaskDeviceRecipe._LotInfo);
            SM.RecipeName = TaskDeviceRecipe._LotInfo.PartNum;
            TaskLotInfo.InitLot();

            if (GDefine._bEnabCognexOCR)
            {
#if !SIMULATION    
                frmMain.MainConnection.DcSingleConnection(TcpModuleID.CognexServer);
                frmMain.MainConnection.BuildSingleConnection(TcpModuleID.CognexServer, GDefine.CognexMainIP, GDefine.CognexMainPort);
                if (!frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer)) return;
                if (!uctrlCommunication.SetOCRRecipe((Enum_OCRRecipe)TaskDeviceRecipe._LotInfo._RecipeInfo._iOCRRecipe))
                {
                    Enabled = true;
                    return;
                }

                    uctrlAuto.Page.PromptMessageOkCancel($"OCR Server: Recipe Change Successful!({TaskDeviceRecipe._LotInfo._RecipeInfo._iOCRRecipe.ToString()})");
                frmMain.MainConnection.BuildSingleConnection(TcpModuleID.CognexOCR, GDefine.OCRIP, GDefine.OCRPort);
                if (!frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexOCR))
                {
                    uctrlAuto.Page.PromptMessageOkCancel($"OCR: Change Back device fail");
                }
#endif
            }

            if(GDefine._bEnabInLaser)
            {
                if (!uctrlCommunication.ChangeLaserModel($"D:\\MarkingFile\\{TaskDeviceRecipe._LotInfo.PartNum}", TcpModuleID.LaserTop, Command.ChangeModel, Command.ChangeModel_OK))
                {
                    Enabled = true;
                    return;
                }
                uctrlAuto.Page.PromptMessageOkCancel($"Laser Top Change Model Succesfully!({TaskDeviceRecipe._LotInfo.PartNum})");
            }
            if (GDefine._bEnabOutLaser)
            {
                if (!uctrlCommunication.ChangeLaserModel($"E:\\MarkingFile\\{TaskDeviceRecipe._LotInfo.PartNum}", TcpModuleID.LaserBtm, Command.ChangeModel, Command.ChangeModel_OK))
                {
                    Enabled = true;
                    return;
                }
                uctrlAuto.Page.PromptMessageOkCancel($"Laser Bottom Change Model Succesfully!({TaskDeviceRecipe._LotInfo.PartNum})");
            }

            //GDefine.Barcode = GenerateBarcode();
            string D = DateTime.Now.Date.ToString("dd-MM-yyyy");
            string T = DateTime.Now.ToString("HH:mm:ss tt");
            TaskLotInfo.LotInfo.DateIn = D;
            TaskLotInfo.LotInfo.TimeIn = T;
            TaskLotInfo.LotInfo.DateOut = "";
            TaskLotInfo.LotInfo.TimeOut = "";
            TaskLotInfo.LotInfo.Activated = true;
            GDefine._iWaffleInputCount = 0;
            GDefine._iWafflePassCount = 0;
            //TaskUnloadRotary._iUnloadCounter = 0;
            frmMain.SequenceRun.LotCounter = new LotCounter();
            frmMain.SequenceRun.ResetProdCntNum((int)TotalModule.Stn0);
            frmMain.SequenceRun.ResetProdCntNum((int)TotalModule.UnloadingStn);
            frmMain.SequenceRun.ResetProdCntNum((int)TotalModule.RejectBin);
            frmMain.SequenceRun.ResetProdCntNum((int)TotalModule.OCR_Reader);
            frmMain.SequenceRun.ResetProdCntNum((int)TotalModule.KeyenceTop);
            frmMain.SequenceRun.ResetProdCntNum((int)TotalModule.KeyenceBtm);

            frmMain.mytagSeqFlag.VacOnInterval = GDefine._bVacOnvalue;
            frmMain.mytagSeqFlag.VacOffInterval = GDefine._bVacOffvalue;
            frmMain.mytagSeqFlag.CylTimeOut = GDefine._bCylTimeOut;
            frmMain.mytagSeqFlag.CylBuffer = GDefine._bCylBuffer;
            frmMain.mytagSeqFlag.SettlingiTime = GDefine._bWaitInterval;
            frmMain.mytagSeqFlag.InProdCount = GDefine._bInProdCnt;
            frmMain.mytagSeqFlag.OutProdCount = GDefine._bOutProdCnt;
            frmMain.SequenceRun.MachineConfig = frmMain.mytagSeqFlag;

            GDefine._iWaffleRejectCount = 0;
            GDefine._iIn2DIDRRejectCounter = 0;
            GDefine._iOut2DIDRejectCounter = 0;
            GDefine._iOCRRejectCounter = 0;
            GDefine._iOCRDirectFailCounter = 0;
            GDefine._iIn2DIDDirectFailCounter = 0;
            GDefine._iOut2DIDDirectFailCounter = 0;
            DialogResult = DialogResult.OK;
            try
            {
                TaskLotInfo.SaveLotInfo(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //goto _END;
            }
            Enabled = true;
            this.Close();
        }

        private void btnEndLot_Click(object sender, EventArgs e)
        {
            var _data = frmMain.SequenceRun.FixZoneData;
            if (_data.Any(x => x.Unit.IsPsnt == true))
            {
                MessageBox.Show("Unit Available in Machine, Wait all the unit finish unloaded");
                return;
            }
            Enabled = false;
            TaskLotInfo.LotInfo.Activated = false;
            string D = DateTime.Now.Date.ToString("dd-MM-yyyy");
            string T = DateTime.Now.ToString("HH:mm:ss tt");
            TaskLotInfo.LotInfo.DateOut = D;
            TaskLotInfo.LotInfo.TimeOut = T;
            try
            {
                TaskLotInfo.SaveLotInfo(true);
                //TaskLotInfo.GenerateFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            TaskLotInfo.InitLot();
            Enabled = true;
            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
