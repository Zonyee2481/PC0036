﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Core.Database
{
    public enum EventType
    {
        Info = 0,
        Action = 1,
        Machine = 2,
        All = 88,
        Error = 99
    }
    public class SQLShareData
    {
        string sysLog = "[dbo].[SysLog]";
        string err = "[dbo].[Error]";
        string lotRecord = "[dbo].[LotRecord]";
        string dateFormat = "yyyy-MM-dd";
        string timeFormat = "HH:mm:ss";
        string[] tableData;
        SqlConnection _conn;
        SqlDataAdapter _dataAdaptor;
        DataTable _table;
        DataTable _errorTable;

        public string SysLog { get { return sysLog; } }
        public string Err { get { return err; } }
        public string LotRecord { get { return lotRecord; } set { lotRecord = value; } }
        public string DateFormat { get { return dateFormat; } }
        public string TimeFormat { get { return timeFormat; } }
        public string[] TableData { get { return tableData; } set { tableData = value; } }
        public SqlConnection Conn { get { return _conn; } set { _conn = value; } }
        public SqlDataAdapter DataAdaptor { get { return _dataAdaptor; } set { _dataAdaptor = value; } }
        public DataTable _Table { get { return _table; } set { _table = value; } }
        public DataTable _ErrorTable { get { return _errorTable; } set { _errorTable = value; } }

        /// <summary>
        /// Is Database connected the database
        /// </summary>
        public bool IsConnected { get; set; }

        /// <summary>
        /// Is Database is connected and open database
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// The Last Error Message for SQLserver class
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Return the MS SQL Connection String
        /// </summary>
        public string ConnectionString { get; set; }

    }
}