using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WAFER_LASER_MARK_SYSTEM
{
  class sqlConnection
  {
    static string connetionString = "Server=USER-PC\\SQLEXPRESS;Database=LM9_DATABASE;Trusted_Connection=Yes";// "Data Source=WIN-D7PBT87T2VJ\\SQLEXPRESS;Initial Catalog=LM8_DATABASE;User ID=sa;Password=888888";
    public static SqlConnection cnn;

    //"Data Source=USER-PC\\SQLEXPRESS;Database=Testing;User ID=ES_Database;Password=ES79896386";

    public static bool OpenSqlCon()
    {
      try
      {
        connetionString = @"Data Source=" + GDefine._sSqlServerName + ";Initial Catalog=" + GDefine._sSqlDatabaseName + ";Integrated Security=True;Pooling=False"; //"Data Source=WIN-D7PBT87T2VJ\\LM9_DATABASE;Initial Catalog=LM9_DATABASE;Trusted_Connection=Yes"; //User ID=sa;Password=888888";
        cnn = new SqlConnection(connetionString);
        cnn.Open();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message + "! SQL Connection Fail To Open! ");
        return false;
      }
      return true;
    }
    public static bool CloseSqlCon()
    {

      try
      {
        if (cnn.State == System.Data.ConnectionState.Open)
        {
          cnn.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message + "! SQL Disconnect To Database Fail! ");
        return false;
      }
      return true;
    }

    public static bool CheckSqlCon()
    {

      try
      {
        if (cnn.State != System.Data.ConnectionState.Open)
        {
          uint MsgID = frmMain.frmMsg.ShowMsg("Handler SQL Database Not Connected! Please manually connect or check database status!",
             frmMessaging.TMsgBtn.smbOK);

          while (!frmMain.frmMsg.ShowMsgClear(MsgID))
          {
            Application.DoEvents();
          }

          switch (frmMain.frmMsg.GetMsgRes(MsgID))
          {
            case frmMessaging.TMsgRes.smrOK:
              return false;
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return false;
      }

      return true;
    }

    public static bool InsertNew2D(string OCR)
    {
      try
      {
        if (!CheckSqlCon()) return false;
        string check = "INSERT INTO dbo.WaferInfo (WAFER_2DID, DATECREATED) VALUES ('" + OCR + "', '" + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + "')";
        SqlCommand cmda = new SqlCommand(check, sqlConnection.cnn);
        cmda.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        //MessageBox.Show(ex.Message);
        return false;
      }
      return true;
    }

    public static bool Check2dDuplicate(string OCR)
    {
      try
      {
        if (!GDefine._bEnabCheck2dDuplicateAndSave) return true;
        if (!CheckSqlCon()) return false;
        string check = @"(Select count(*) from dbo.WaferInfo where WAFER_2DID = '" + OCR + "')";
        SqlCommand cmda = new SqlCommand(check, sqlConnection.cnn);
        int count = (int)cmda.ExecuteScalar();
        if (count > 0)
        {
          uint MsgID = frmMain.frmMsg.ShowMsg(Errcode.Err_DataBaseDuplicateOCR,
              frmMessaging.TMsgBtn.smbOK | frmMessaging.TMsgBtn.smbStop| frmMessaging.TMsgBtn.smbAlmClr);

          while (!frmMain.frmMsg.ShowMsgClear(MsgID))
          {
            GDefine.AppProMsg();
          }

          switch (frmMain.frmMsg.GetMsgRes(MsgID))
          {
            case frmMessaging.TMsgRes.smrOK:
              TaskWaferArm.ProcWaferArm = TaskWaferArm.Proc._eLAUnloadRejectWaferToMag;
              return true;
          }
          return false;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return false;
      }
      return true;
    }
  }
}
