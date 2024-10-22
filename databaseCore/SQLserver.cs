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
        #endregion
    }
}
