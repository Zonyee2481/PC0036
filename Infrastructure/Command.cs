using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public enum Command
    {
        //Send Command
        [Description("CheckStatus,@")]
        CheckStatus,
        [Description("ChangeModel,@")]
        ChangeModel,
        [Description("ChangeFile,@D:\\MarkingFile\\MarkInfo.txt")]
        ChangeFile_DDrive,
        [Description("Mark,@")]
        Mark,


        //Receive Command
        [Description("CheckStatus_OK")]
        CheckStatus_OK,
        [Description("ChangeModel_OK")]
        ChangeModel_OK,
        [Description("ChangeFile_OK")]
        ChangeFile_OK,
        [Description("Receive_Mark")]
        Receive_Mark,
        [Description("Mark_OK")]
        Mark_OK,
        [Description("NG")]
        NG,


        #region CognexServer
        //Send Command
        [Description("admin\r\n")]
        Login,
        [Description("\r\n")]
        Password,
        [Description("lfbig\r\n")]
        LoadBigCR,
        [Description("lfsmall\r\n")]
        LoadSmallCR,
        [Description("so1\r\n")]
        SetOnline,
        [Description("so0\r\n")]
        SetOffLine,



        //Receive Pass Command
        [Description("User Logged In")]
        LoggedIn,
        [Description("Password:")]
        Received_Password,
        [Description("1")]
        CognexServerSetDone,


        //Receive Fail Command
        [Description("Failed to login Cognex main IP!")]
        LoggedIn_Fail,
        [Description("0")]
        CognexServerSetFail,
        #endregion


        #region CognexOCR
        [Description("T01\r\n")]
        TriggerAutomode,

        #endregion

        #region Keyence
        [Description("LON\r\n")]
        TriggerSnap,

        #endregion
    }
}
