using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Core.Database
{
    public class SQLserver : SQLShareData
    {
        public SQLserver()
        {
            IsConnected = false;
            IsOpen = false;
            ConnectionString = string.Empty;
            Conn = null;
            DataAdaptor = null;
            _Table = null;
            LotNumbers = new List<string>();
        }

        public bool ConnectDatabase(string strConnection)
        {
            if (IsConnected)
            {
                ErrorMessage = "The database is connected.\r\n" + strConnection;
                return false;
            }
            else
            {
                ConnectionString = strConnection;
                try
                {
                    Conn = new SqlConnection();
                    Conn.ConnectionString = ConnectionString;
                    Conn.Open();
                    IsConnected = true;
                    Conn.Close();
                    IsOpen = false;
                    ErrorMessage = string.Empty;
                    return true;
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    IsOpen = false;
                    ErrorMessage = ex.Message;
                    return false;
                }
            }
        }

        public bool ConnectDatabase(string Server, string UserName, string password, string database)
        {
            ConnectionString = "server=" + Server + ";";
            ConnectionString += "User ID=" + UserName + ";";
            ConnectionString += "Password=" + password + ";";
            ConnectionString += "Initial Catalog=" + database + ";";

            try
            {
                return ConnectDatabase(this.ConnectionString);
            }
            catch (Exception ex)
            {
                IsConnected = false;
                IsOpen = false;
                ErrorMessage = ex.Message;
                return false;
            }
        }

        private void SQLExecuteQuery(string sql)
        {
            SqlTransaction _trans = null;
            try
            {
                if (!IsOpen)
                {
                    throw new Exception("Database is not open!");
                }

                SqlCommand _command = new SqlCommand(sql, Conn);

                if (sql.ToUpper().Contains("SELECT"))
                {
                    if (_Table == null) { _Table = new DataTable(); }
                    else { _Table.Reset(); _Table = null; _Table = new DataTable(); }
                    DataAdaptor = new SqlDataAdapter(_command);
                    DataAdaptor.Fill(_Table);
                    Conn.Close();
                }
                else
                {
                    _trans = Conn.BeginTransaction();
                    _command.Transaction = _trans;

                    if (_Table != null)
                    {
                        _Table.Reset();
                        _Table = null;
                    }
                    _command.ExecuteNonQuery();
                    _trans.Commit();
                }
            }
            catch (Exception ex)
            {
                if (_trans != null) { _trans.Rollback(); _trans = null; }
                throw ex;
            }
        }

        /// <summary>
        /// the last query data table
        /// </summary>
        public DataTable DataTable
        {
            get
            {
                if (_Table != null)
                {
                    return _Table.Copy();
                }

                return null;
            }
        }

        /// <summary>
        /// the Error Code Description Table. InitErrorCode() to initial table.
        /// </summary>
        public DataTable ErrorTable
        {
            get
            {
                if (_ErrorTable != null)
                {
                    return _ErrorTable.Copy();
                }

                return null;
            }
        }

        /// <summary>
        /// Open Database
        /// </summary>
        public void Open()
        {
            if (!this.IsConnected)
            {
                throw new Exception("SQLserver still no connected.");
            }

            IsOpen = true;
            Conn.Open();
        }

        /// <summary>
        /// Close Database
        /// </summary>
        public void Close()
        {
            if (IsOpen)
            {
                IsOpen = false;
                Conn.Close();
            }
        }

        /// <summary>
        /// Disconnect Database
        /// </summary>
        public void DisconnectDatabase()
        {
            if (_Table != null)
            {
                _Table.Reset();
                _Table = null;
            }

            IsConnected = false;
            IsOpen = false;
            ConnectionString = "";

            if (DataAdaptor != null)
            {
                DataAdaptor.Dispose();
                DataAdaptor = null;
            }

            if (Conn != null)
            {
                Conn = null;
            }
        }

        /// <summary>
        /// Initial the ErrorTable
        /// </summary>
        /// <returns>True, Execution success</returns>
        public bool InitError()
        {
            try
            {
                this.Open();

                SQLExecuteQuery("SELECT * FROM " + Err);

                if (DataTable.Rows.Count > 0)
                {
                    _ErrorTable = DataTable;
                }

                this.Close();

                this.ErrorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
        }

        #region Function for User/Page Access
        public bool GetPageAccessLevel(out int[] pageAccessLevel)
        {
            string sqlString;
            pageAccessLevel = new int[0];
            try
            {
                sqlString = "SELECT * FROM " + Page + " ORDER BY Id ASC";

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                pageAccessLevel = new int[DataTable.Rows.Count];

                for (int i = 0; i < DataTable.Rows.Count; i++)
                {
                    pageAccessLevel[i] = Convert.ToInt32(DataTable.Rows[i]["UserLevel"]);
                }

                ErrorMessage = "";

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool UserLogin(string BadgeNumber, string Password)
        {
            string sqlString;

            try
            {
                sqlString = "SELECT * FROM " + User;
                sqlString = string.Format("{0} WHERE [BadgeNumber]='{1}' AND [UserPassword]='{2}'", sqlString, BadgeNumber, Password);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                if (DataTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    this.ErrorMessage = string.Empty;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool GetUserLoginInfo(string BadgeNumber, string Password, out string Username, out int UserLevel)
        {
            string sqlString;
            Username = string.Empty;
            UserLevel = 0;
            try
            {
                sqlString = "SELECT * From " + User;
                sqlString = string.Format("{0} WHERE [BadgeNumber]='{1}' AND [UserPassword]='{2}'", sqlString, BadgeNumber, Password);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                if (DataTable.Rows.Count <= 0)
                {
                    ErrorMessage = string.Format("{0} not found from database!", BadgeNumber);
                    Username = string.Empty;
                    UserLevel = 0;
                    return false;
                }
                else
                {
                    Username = DataTable.Rows[0]["UserName"].ToString();
                    UserLevel = Convert.ToInt32(DataTable.Rows[0]["UserLevel"]);
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool CheckUserAccountExist(string BadgeNumber)
        {
            string sqlString;
            try
            {
                sqlString = "SELECT * From " + User;
                sqlString = String.Format("{0} WHERE [BadgeNumber]='{1}' ORDER BY [Id] ASC", sqlString, BadgeNumber);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                return DataTable.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool GetPageDataTable()
        {
            string sqlString;
            try
            {
                sqlString = "SELECT * FROM " + Page + " ORDER BY Id ASC";

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                if (DataTable.Rows.Count <= 0)
                {
                    ErrorMessage = string.Format("Page data table not found from database!");
                    return false;
                }

                ErrorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool GetUserDataTable()
        {
            string sqlString;
            try
            {
                sqlString = "SELECT * FROM " + User + " ORDER BY Id ASC";

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                if (DataTable.Rows.Count <= 0)
                {
                    ErrorMessage = string.Format("Access level data table not found from database!");
                    return false;
                }

                ErrorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool AddUser(string BadgeNumber, string UserName, string Password, string UserLevel)
        {
            string sqlString;
            try
            {
                sqlString = string.Format("INSERT INTO " + User + " VALUES ('{0}', '{1}', '{2}', '{3}')",
                                           BadgeNumber, UserName, Password, UserLevel);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                ErrorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool UpdateUser(string BadgeNumber, string UserName, string Password, string UserLevel, string CurrentBadgeNumber)
        {
            string sqlString;
            try
            {
                sqlString = string.Format("UPDATE " + User + " SET [BadgeNumber]='{0}', [UserName]='{1}', [UserPassword]='{2}', [UserLevel]='{3}' WHERE [BadgeNumber]='{4}'",
                                           BadgeNumber, UserName, Password, UserLevel, CurrentBadgeNumber);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                ErrorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool DeleteUser(string Id, string BadgeNumber)
        {
            string sqlString;
            try
            {
                sqlString = string.Format("DELETE FROM " + User + " WHERE [Id]='{0}' AND [BadgeNumber]='{1}' ", Id, BadgeNumber);

                this.Open();
                this.SQLExecuteQuery(sqlString);
                this.Close();

                ErrorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool UpdatePage(int PageId, string AccessLevel)
        {
            string sqlString;
            try
            {
                sqlString = string.Format("UPDATE " + Page + " SET [UserLevel]='{0}' WHERE [Id]='{1}'",
                                           AccessLevel, PageId);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                ErrorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        #endregion


        #region DB Log
        /// <summary>
        /// Initial the SysLogTable
        /// </summary>
        /// <returns>True, Execution success</returns>
        public bool InitSystemLog()
        {
            try
            {
                this.Open();

                SQLExecuteQuery("SELECT * FROM " + SysLog + " ORDER BY Id ASC");

                if (DataTable.Rows.Count > 0)
                {
                    _ErrorTable = DataTable;
                    return true;
                }

                this.Close();

                this.ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool FindSystemLogByDate(string eventType, string startDate, string endDate)
        {
            string sqlString;
            try
            {
                Open();
                sqlString =  String.Format("SELECT * from " + SysLog + "WHERE EventType LIKE '{0}%' AND Date BETWEEN '{1}' AND '{2}' ORDER BY Id ASC", eventType, startDate, endDate);
                SQLExecuteQuery(sqlString);

                if (DataTable.Rows.Count > 0)
                {
                    _ErrorTable = DataTable;
                }

                Close();

                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool LogInfo(String UserID, String InfoName, String Description = "")
        {
            return LogEvent(EventType.Info, UserID, InfoName, Description);
        }

        public bool LogMachine(String UserID, String DeviceName, String Description = "")
        {
            return LogEvent(EventType.Machine, UserID, DeviceName, Description);
        }

        public bool LogAction(String UserID, String ButtonName, String Description = "")
        {
            return LogEvent(EventType.Action, UserID, ButtonName, Description);
        }

        public bool LogError(String UserID = "", String ErrorCode = "", String Description = "")
        {
            return LogEvent(EventType.Error, UserID, ErrorCode, Description);
        }

        /// <summary>
        /// Log Data into SysLog 
        /// </summary>
        /// <param name="Type">Message Type</param>
        /// <param name="UserID">User ID</param>
        /// <param name="ErrorCode">Error Code / Name</param>
        /// <param name="Description">Full Message</param>
        /// <returns></returns>
        public bool LogEvent(EventType Type, String UserID, String ErrorCode, String Description)
        {
            try
            {               
                String Date = DateTime.Now.ToString(DateFormat);
                String Time = DateTime.Now.ToString(TimeFormat);

                string sqlString = string.Format("INSERT INTO [SysLog] VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                                           Date, Time, UserID, Type.ToString(), ErrorCode, Description);

                Open();

                SQLExecuteQuery(sqlString);

                Close();

                ErrorMessage = "";
                return true;         
            }
            catch (Exception ex)
            {
                //throw ex;
                this.ErrorMessage = ex.Message;
                return false;
            }
        }
        #endregion

        #region Record Data **For project RK PC0036**
        public bool AddLotRecord(string LotNumber, string startDate, string startTime, string endDate, string endTime)
        {
            string sqlString;
            try
            {
                sqlString = string.Format("INSERT INTO " + LotRecord + " VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                                           LotNumber, startDate, startTime, endDate, endTime);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool UpdateLotRecord(string LotNumber, string startDate, string startTime, string endDate, string endTime)
        {
            string sqlString;
            try
            {
                sqlString = string.Format("UPDATE " + LotRecord + " SET [EndDate]='{0}', [EndTime]='{1}' WHERE [LotNumber]='{2}' AND [StartDate]='{3}' AND [StartTime]='{4}'",
                                           endDate, endTime, LotNumber, startDate, startTime);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                ErrorMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool CountLotRecord(string LotNumber, out int Count)
        {
            string sqlString;
            Count = 0;
            try
            {
                sqlString = "SELECT * FROM " + LotRecord;
                sqlString = string.Format("{0} WHERE [LotNumber] = '{1}'", sqlString, LotNumber);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                ErrorMessage = "";
                Count = DataTable.Rows.Count;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool CountLotRecordByDate(string LotNumber, string StartDate, out int Count)
        {
            string sqlString;
            Count = 0;
            try
            {
                sqlString = "SELECT * FROM " + LotRecord;
                sqlString = string.Format("{0} WHERE [LotNumber] = '{1}' AND [StartDate] = '{2}'", sqlString, LotNumber, StartDate);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                ErrorMessage = "";
                Count = DataTable.Rows.Count;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool CountLotRecordByLotNumber(string LotNumber, out int Count)
        {
            string sqlString;
            Count = 0;
            try
            {
                sqlString = "SELECT * FROM " + LotRecord;
                sqlString = string.Format("{0} WHERE [LotNumber] = '{1}'", sqlString, LotNumber);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                ErrorMessage = "";
                Count = DataTable.Rows.Count;
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public bool GetLotRecordStartTime(string LotNumber, string StartDate, out string[] StartTime)
        {
            StartTime = new string[0];
            string sqlString;
            try
            {
                sqlString = "SELECT [StartTime] FROM " + LotRecord;
                sqlString = string.Format("{0} WHERE [LotNumber]='{1}' AND [StartDate]='{2}' ORDER BY [Id] DESC", sqlString, LotNumber, StartDate);

                Open();
                SQLExecuteQuery(sqlString);
                Close();
             
                StartTime = new string[DataTable.Rows.Count];

                for (int i = 0; i < DataTable.Rows.Count; i++)
                {
                    StartTime[i] = DataTable.Rows[i]["StartTime"].ToString();
                }

                ErrorMessage = "";

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;                
                return false;
            }
        }

        public bool GetLotRecordCountByDate(string StartDate)
        {
            string sqlString;
            try
            {
                sqlString = "SELECT [LotNumber], COUNT(*) AS LotRecordCount FROM " + LotRecord;
                sqlString = string.Format("{0} WHERE [StartDate]='{1}' GROUP BY [LotNumber]", sqlString, StartDate);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                ErrorMessage = "";

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public DataTable LotRecordCountByDate(string StartDate)
        {
            string sqlString;
            DataTable table = new DataTable();
            table.Columns.Add("LotNumber", typeof(string));
            table.Columns.Add("Count", typeof(int));
            try
            {
                #region Get Lot Number By Date
                sqlString = "SELECT [LotNumber] FROM " + LotRecord;
                sqlString = string.Format("{0} WHERE [StartDate]='{1}'", sqlString, StartDate);

                Open();
                SQLExecuteQuery(sqlString);
                Close();

                if (DataTable.Rows.Count <= 0) { ErrorMessage = ""; return table; }

                LotNumbers = new List<string>(DataTable.Rows.Count);

                // Add data into list with the data table
                foreach (DataRow row in DataTable.Rows)
                {
                    LotNumbers.Add(row["LotNumber"].ToString());
                }
                #endregion

                List<string> uniqueLotNumbers = LotNumbers.Distinct().ToList();

                #region Get Lot Number Count From DB
                foreach (string lotNumber in uniqueLotNumbers)
                {
                    sqlString = "SELECT [LotNumber], COUNT(*) AS LotRecordCount FROM " + LotRecord;
                    sqlString = string.Format("{0} WHERE [LotNumber]='{1}' GROUP BY [LotNumber]", sqlString, lotNumber);

                    Open();
                    SQLExecuteQuery(sqlString);
                    Close();

                    foreach (DataRow row in DataTable.Rows)
                    {
                        table.Rows.Add(row.ItemArray);
                    }
                }
                #endregion

                ErrorMessage = "";

                return table;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return table;
            }
        }
        #endregion
    }
}
