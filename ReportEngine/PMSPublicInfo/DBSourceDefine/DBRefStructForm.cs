using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.PMSPublicInfo.ConfigFile;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.Data.SqlClient;
using System.IO;
using Oracle.DataAccess.Client;
using System.Data.Common;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.DBSourceDefine
{
    public partial class DBRefStructForm : Form
    {
        #region private member

        private DbConnection _conn = null;
        private string ConfigPathname = ProjectPathClass.RefDBConfigSettingFilePath;
        private SqlAssistance sqlAS = new SqlAssistance();
        private SqlStructure sqlStructure = null;
        private bool bConnected = false;
        private string strServerName = string.Empty;
        private string strDBName = string.Empty;
        private string strUserID = string.Empty;
        private string strPassWord = string.Empty;
        private ConnectType eConnectType;
        private ConnectMode eConnectMode;
        private string strPortID = string.Empty;
        private bool bDirect = false;
        private string strConnectAs = string.Empty;
        
        private string strMsAccessPath;
        private AccessStructure acsStructure = null;
        private OleDbConnection _OleDbConn = null;
        private PMSRefDBConnection _RefDBConnection = null;
                
        private RefDBType _SelectDBType = RefDBType.MSSqlServer;

        private string _connString;
        private PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.OleDbSchema _schema;

        #endregion

        [BrowsableAttribute(false)]
        public bool Connected
        {
            get { return bConnected; }
        }

        [BrowsableAttribute(false)]
        public string ServerName
        {
            get { return strServerName; }
        }

        [BrowsableAttribute(false)]
        public string DBName
        {
            get { return strDBName; }
        }

        [BrowsableAttribute(false)]
        public string UserID
        {
            get { return strUserID; }
        }

        [BrowsableAttribute(false)]
        public string PassWord
        {
            get { return strPassWord; }
        }

        [BrowsableAttribute(false)]
        public ConnectType ConnectType
        {
            get { return eConnectType; }
        }

        [BrowsableAttribute(false)]
        public ConnectMode ConnectMode
        {
            get { return eConnectMode; }
        }

        [BrowsableAttribute(false)]
        public string PortID
        {
            get { return strPortID; }
        }

        [BrowsableAttribute(false)]
        public DbConnection conn
        {
            get { return _conn; }
        }

        [BrowsableAttribute(false)]
        public bool BDirect
        {
            get { return bDirect; }
            set { bDirect = value; }
        }

        [BrowsableAttribute(false)]
        public System.Data.OleDb.OleDbConnection OleDbConn
        {
            get { return _OleDbConn; }
        }

        [BrowsableAttribute(false)]
        public PMS.Libraries.ToolControls.PMSPublicInfo.PMSRefDBConnection RefDBConnection
        {
            get { return _RefDBConnection; }
            set { _RefDBConnection = value; }
        }

        [BrowsableAttribute(false)]
        public string MsAccessPath
        {
            get { return strMsAccessPath; }
        }

        [BrowsableAttribute(false)]
        public RefDBType SelectDBType
        {
            get { return _SelectDBType; }
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        public string ConnectionString
        {
            get { return _connString; }
            set
            {
                if (value != ConnectionString)
                {
                    // this may take a while
                    Cursor = Cursors.WaitCursor;

                    // look for item in the list
                    var items = comboBox_ConnectString.Items;
                    var index = items.IndexOf(value);

                    // get schema for the new connection string
                    _schema = PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.OleDbSchema.GetSchema(value);

                    // handle good connection strings
                    if (_schema != null)
                    {
                        // add good values to the list
                        if (index < 0)
                        {
                            items.Insert(0, value);
                        }
                        else if (index > 0)
                        {
                            items.RemoveAt(index);
                            items.Insert(0, value);
                        }

                        // trim list
                        while (items.Count > comboBox_ConnectString.MaxDropDownItems)
                        {
                            items.RemoveAt(items.Count - 1);
                        }
                    }
                    else // handle bad connection strings
                    {
                        // remove from list
                        if (index >= 0)
                        {
                            items.RemoveAt(index);
                        }

                        // do not store bad values
                        value = string.Empty;
                    }

                    // save new value
                    _connString = value;

                    // show new value in combo box and table tree
                    comboBox_ConnectString.Text = value;

                    // done
                    Cursor = null;
                }
            }
        }

        public DBRefStructForm()
        {
            InitializeComponent();
        }

        private void SaveSettings()
        {
            DBRefConfigSetting xDS = new DBRefConfigSetting();

            DBRefConfigSetting.MESRefDBTypeRow dRow = xDS.MESRefDBType.NewMESRefDBTypeRow();
            dRow.Type = this.tabControl1.SelectedIndex;
            xDS.MESRefDBType.AddMESRefDBTypeRow(dRow);

            DBRefConfigSetting.MSSQLServerRow mRow = xDS.MSSQLServer.NewMSSQLServerRow();
            string myServerName = comboBox_Server.Text.Trim();
            string myDBName = comboBox_DB.Text.Trim();
            string userId = textBox_UserName.Text.Trim();
            string passWord = textBox_Password.Text.Trim();
            int ConnectType = comboBox_Protocol.SelectedIndex + 1;
            string portID = textBox_PortID.Text;
            mRow.ServerName = myServerName;
            mRow.DBName = myDBName;
            mRow.UserID = userId;
            mRow.PassWord = passWord;
            mRow.ConnectType = ConnectType;
            mRow.PortID = portID;
            xDS.MSSQLServer.AddMSSQLServerRow(mRow);

            DBRefConfigSetting.OracleRow oRow = xDS.Oracle.NewOracleRow();
            string OracleServerName = comboBox_OracleServer.Text.Trim();
            string OracleUserId = textBox_OracleUserID.Text.Trim();
            string OraclePassWord = textBox_OracleUserPass.Text.Trim();
            string OracleConnectAs = comboBox_ConnectAs.Text.Trim();
            string OracleConnectMode = comboBox_ConnectMode.Text.Trim();
            string OracleHome = comboBox_OracleHome.Text.Trim();
            string OracleSID = comboBox_SID.Text.Trim();
            string OraclePortID = textBox_OracleProtID.Text.Trim();
            string OracleOwner = textBox_Owner.Text.Trim();
            bool OracleDirect = checkBox_Direct.Checked;
            oRow.ServerName = OracleServerName;
            oRow.UserID = OracleUserId;
            oRow.PassWord = OraclePassWord;
            oRow.ConnectAs = OracleConnectAs;
            oRow.ConnectMode = OracleConnectMode;
            oRow.Home = OracleHome;
            oRow.Direct = OracleDirect;
            oRow.SID = OracleSID;
            oRow.PortID = OraclePortID;
            oRow.Owner = OracleOwner;
            xDS.Oracle.AddOracleRow(oRow);

            DBRefConfigSetting.MSAccessRow aRow = xDS.MSAccess.NewMSAccessRow();
            string strMSAcessDBPath = textBox_MSAccessPath.Text.Trim();
            string strMSAcessUserId = textBox_MSAccessUID.Text.Trim();
            string strMSAcessPassWord = textBox_MSAccessPass.Text.Trim();
            aRow.DBPath = strMSAcessDBPath;
            aRow.UserID = strMSAcessUserId;
            aRow.PassWord = strMSAcessPassWord;
            xDS.MSAccess.AddMSAccessRow(aRow);

            if (this.comboBox_ConnectString.Items.Count > 0)
            {
                foreach (var item in this.comboBox_ConnectString.Items)
                {
                    DBRefConfigSetting.OleDBRow oleRow = xDS.OleDB.NewOleDBRow();
                    oleRow.ConnectString = item.ToString();
                    xDS.OleDB.AddOleDBRow(oleRow);
                }
                xDS.OleDB[0].Selected = this.comboBox_ConnectString.SelectedIndex;
            }

            xDS.WriteXml(ConfigPathname, System.Data.XmlWriteMode.IgnoreSchema);
        }

        private void LoadSettings()
        {
            if (new FileInfo(ConfigPathname).Exists)
            {
                DBRefConfigSetting xDS = new DBRefConfigSetting();
                xDS.ReadXml(ConfigPathname, System.Data.XmlReadMode.IgnoreSchema);

                if (xDS.MESRefDBType.Rows.Count > 0)
                {
                    DBRefConfigSetting.MESRefDBTypeRow dRow = xDS.MESRefDBType[0];
                    if (!dRow.IsTypeNull())
                    {
                        this.tabControl1.SelectedIndex = dRow.Type;
                    }
                }

                if (xDS.MSSQLServer.Rows.Count > 0)
                {
                    DBRefConfigSetting.MSSQLServerRow xRow = xDS.MSSQLServer[0];
                    if (!xRow.IsServerNameNull())
                    {
                        comboBox_Server.Text = xRow.ServerName;
                    }
                    if (!xRow.IsDBNameNull())
                    {
                        comboBox_DB.Text = xRow.DBName;
                    }
                    if (!xRow.IsUserIDNull())
                    {
                        textBox_UserName.Text = xRow.UserID;
                    }
                    if (!xRow.IsPassWordNull())
                    {
                        textBox_Password.Text = xRow.PassWord;
                    }
                    if (!xRow.IsConnectTypeNull())
                    {
                        comboBox_Protocol.SelectedIndex = xRow.ConnectType - 1;
                    }
                    if (!xRow.IsPortIDNull())
                    {
                        textBox_PortID.Text = xRow.PortID;
                    }
                }

                if (xDS.Oracle.Rows.Count > 0)
                {
                    DBRefConfigSetting.OracleRow oRow = xDS.Oracle[0];
                    if (!oRow.IsServerNameNull())
                    {
                        comboBox_OracleServer.Text = oRow.ServerName;
                    }
                    if (!oRow.IsUserIDNull())
                    {
                        textBox_OracleUserID.Text = oRow.UserID;
                    }
                    if (!oRow.IsPassWordNull())
                    {
                        textBox_OracleUserPass.Text = oRow.PassWord;
                    }
                    if (!oRow.IsConnectAsNull())
                    {
                        comboBox_ConnectAs.Text = oRow.ConnectAs;
                    }
                    if (!oRow.IsConnectModeNull())
                    {
                        comboBox_ConnectMode.Text = oRow.ConnectMode;
                    }
                    if (!oRow.IsHomeNull())
                    {
                        comboBox_OracleHome.Text = oRow.Home;
                    }
                    if (!oRow.IsSIDNull())
                    {
                        comboBox_SID.Text = oRow.SID;
                    }
                    if (!oRow.IsPortIDNull())
                    {
                        textBox_OracleProtID.Text = oRow.PortID;
                    }
                    if (!oRow.IsOwnerNull())
                    {
                        textBox_Owner.Text = oRow.Owner;
                    }
                    if (!oRow.IsDirectNull())
                    {
                        this.checkBox_Direct.Checked = oRow.Direct;
                    }
                }

                if (xDS.MSAccess.Rows.Count > 0)
                {
                    DBRefConfigSetting.MSAccessRow aRow = xDS.MSAccess[0];
                    if (!aRow.IsDBPathNull())
                    {
                        textBox_MSAccessPath.Text = aRow.DBPath;
                    }
                    if (!aRow.IsUserIDNull())
                    {
                        textBox_MSAccessUID.Text = aRow.UserID;
                    }
                    if (!aRow.IsPassWordNull())
                    {
                        textBox_MSAccessPass.Text = aRow.PassWord;
                    }
                }

                if (xDS.OleDB.Rows.Count > 0)
                {
                    DBRefConfigSetting.OleDBDataTable aRowTable = xDS.OleDB;
                    foreach (DBRefConfigSetting.OleDBRow aRow in aRowTable)
                    {
                        this.comboBox_ConnectString.Items.Add(aRow.ConnectString);
                    }

                    DBRefConfigSetting.OleDBRow oleRow = xDS.OleDB[0];
                    this.comboBox_ConnectString.SelectedIndex = oleRow.Selected;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = textBox_MSAccessPath.Text.Trim();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string stra = openFileDialog1.FileName;
                textBox_MSAccessPath.Text = stra;
            }
        }

        private void RefreshServer()
        {
            this.button_RefreshServer.Enabled = false;
            this.comboBox_Server.Enabled = false;
            this.comboBox_Server.Items.Clear();
            this.comboBox_Server.Text = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_RetrievingDataSources");
            System.Windows.Forms.Application.DoEvents();

            Cursor.Current = Cursors.WaitCursor;

            System.Data.DataTable dataTable = sqlAS.EnumSqlservers();

            DisplayData(dataTable);
            if (this.comboBox_Server.Items.Count > 0)
            {
                comboBox_Server.SelectedIndex = 0;
            }
            else
            {
                comboBox_Server.Text = "";
            }

            this.button_RefreshServer.Enabled = true;
            this.comboBox_Server.Enabled = true;

            Cursor.Current = Cursors.Default;
        }

        private void DisplayData(System.Data.DataTable table)
        {
            //multiColumnComboBox_Server.Items.Clear();
            //table.Rows.Add(new String[] { "D1", "Natalia", "Developer", "32" });

            //multiColumnComboBox_Server.DataSource = table;
            //ServerName,InstanceName,IsClustered,Version
            //multiColumnComboBox_Server.DisplayMember = "ServerName";
            //multiColumnComboBox_Server.ValueMember = "ServerName";

            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox_Server.Items.Add(table.Rows[i]["ServerName"] + "\\" + table.Rows[i]["InstanceName"]);
            }

            //foreach (System.Data.DataRow row in table.Rows)
            //{
            //    //foreach (System.Data.DataColumn col in table.Columns)
            //    {
            //        //System.Data.DataColumn col = new System.Data.DataColumn("ServerName");
            //        //Console.WriteLine("{0} = {1}", col.ColumnName, row[col]);
            //        comboBox_Server.Items.Add(row["ServerName"].ToString());
            //    }
            //}
        }

        private void RefreshDB()
        {
            string myServerName = comboBox_Server.Text.Trim();
            string userId = textBox_UserName.Text.Trim();
            string passWord = textBox_Password.Text.Trim();
            if (myServerName.CompareTo("") == 0)
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_DBServerNameIsNull");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.button_RefreshDB.Enabled = false;
            this.comboBox_DB.Enabled = false;
            this.comboBox_DB.Items.Clear();
            this.comboBox_DB.Text = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_RetrievingDataSources");
            System.Windows.Forms.Application.DoEvents();

            Cursor.Current = Cursors.WaitCursor;

            if (!sqlAS.ConnectSMOServer(myServerName, userId, passWord))
            {
                comboBox_DB.Text = "";
                this.button_RefreshDB.Enabled = true;
                this.comboBox_DB.Enabled = true;

                Cursor.Current = Cursors.Default;
                return;
            }

            List<string> databases = sqlAS.GetServerDatabases();
            comboBox_DB.Items.Clear();

            if (null != databases)
            {
                foreach (string dbName in databases)
                    comboBox_DB.Items.Add(dbName);
            }
            if (this.comboBox_DB.Items.Count > 0)
            {
                comboBox_DB.SelectedIndex = 0;
            }
            else
            {
                comboBox_DB.Text = "";
            }

            this.button_RefreshDB.Enabled = true;
            this.comboBox_DB.Enabled = true;

            Cursor.Current = Cursors.Default;
        }

        private void button_ConnectTest_Click(object sender, EventArgs e)
        {
            string myServerName = comboBox_Server.Text.Trim();
            string myDBName = comboBox_DB.Text.Trim();
            string userId = textBox_UserName.Text.Trim();
            string passWord = textBox_Password.Text.Trim();
            int ConnectType = comboBox_Protocol.SelectedIndex + 1;
            string portID = textBox_PortID.Text;
            string strConnect = "";
            if (ConnectType == 1)
                strConnect = "Pooling = 'false';Server=" + myServerName +
                    ";Initial Catalog=" + myDBName +
                    ";User Id=" + userId +
                    ";Password=" + passWord +
                    ";";
            else if (ConnectType == 2)//Pooling = 'false';
                strConnect = "Server=tcp:" + myServerName + "," + portID +
                 ";Initial Catalog=" + myDBName +
                    ";User Id=" + userId +
                    ";Password=" + passWord +
                    ";";
            //strConnect = "Server=TCP:" + myServerName + "," + portID +
            //    ";Initial Catalog=" + myDBName +
            //    ";User Id=" + userId +
            //    ";Password=" + passWord +
            //    ";";
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(strConnect);
                conn.Open();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            if (conn.State == ConnectionState.Open)
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_DBConTestSucceed");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Succeed");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                conn.Close();
            }
            else
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_DBConTestFail");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Fail");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_RefreshServer_Click(object sender, EventArgs e)
        {
            RefreshServer();
            comboBox_Server.DroppedDown = true;
        }

        private void button_RefreshDB_Click(object sender, EventArgs e)
        {
            RefreshDB();
            comboBox_DB.DroppedDown = true;
        }

        private void comboBox_DB_DropDown(object sender, EventArgs e)
        {
            if (comboBox_DB.Items.Count == 0)
            {
                RefreshDB();
            }
        }

        private void comboBox_Server_DropDown(object sender, EventArgs e)
        {
            if (comboBox_Server.Items.Count == 0)
            {
                RefreshServer();
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 0 -- 数据库连接失败
        /// 1 -- 用户登录失败
        /// 2 -- 连接成功
        /// </returns>
        private int ConnectDB()
        {
            this.button_OK.Enabled = false;
            bConnected = false;
            if(_SelectDBType == RefDBType.MSSqlServer)
            {
                string myServerName = comboBox_Server.Text.Trim();
                if (string.IsNullOrEmpty(myServerName))
                {
                    string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBServerNULL");
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                    MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.button_OK.Enabled = true;
                    return 0;
                }
                string myDBName = comboBox_DB.Text.Trim();
                if (string.IsNullOrEmpty(myDBName))
                {
                    string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBDBNameNULL");
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                    MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.button_OK.Enabled = true;
                    return 0;
                }
                string userId = textBox_UserName.Text.Trim();
                if (string.IsNullOrEmpty(userId))
                {
                    string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBUserIDNULL");
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                    MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.button_OK.Enabled = true;
                    return 0;
                }
                string passWord = textBox_Password.Text.Trim();

                ConnectType iProtocolType = (ConnectType)(comboBox_Protocol.SelectedIndex + 1);

                string portID = textBox_PortID.Text.Trim();
                //string strConnect = "Data Source=" + myServerName +
                //        ";Initial Catalog=" + myDBName +
                //        ";User Id=" + userId +
                //        ";Password=" + passWord +
                //        ";";
                try
                {
                    _RefDBConnection = new PMSRefDBConnection();
                    _RefDBConnection.RefDBType = RefDBType.MSSqlServer;
                    _RefDBConnection.StrServerName = myServerName;
                    _RefDBConnection.StrDBName = myDBName;
                    _RefDBConnection.StrUserID = userId;
                    _RefDBConnection.StrPassWord = passWord;
                    _RefDBConnection.EConnectType = iProtocolType;
                    _RefDBConnection.StrPortID = portID;
                    _conn = _RefDBConnection.GetSqlConnection();

                    //sqlStructure = new SqlStructure(myServerName, myDBName, userId, passWord, iProtocolType, portID);

                    //_conn = SqlStructure.GetSqlConncetion();
                    if (_conn == null)
                    {
                        bConnected = false;
                        this.button_OK.Enabled = true;
                        return 0;
                    }
                    //if (!SqlStructure.ConnectSMOServer())
                    //{
                    //    bConnected = false;
                    //    this.button_OK.Enabled = true;
                    //    return 0;
                    //}

                    //SqlStructure.PMSCenterDataContext = new PMSCenterDataContext(_conn);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    bConnected = false;
                    this.button_OK.Enabled = true;
                    return 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    bConnected = false;
                    this.button_OK.Enabled = true;
                    return 0;
                }

                if (_conn.State == ConnectionState.Open)
                {
                    // 连接数据库成功
                    SaveSettings();
                    bConnected = true;
                    strServerName = myServerName;
                    strDBName = myDBName;
                    strUserID = userId;
                    strPassWord = passWord;

                    // 连接成功关闭
                    this.Close();
                    this.DialogResult = DialogResult.OK;
                    return 2;
                }
                else
                {
                    bConnected = false;
                    return 0;
                }
            }
            else if (_SelectDBType == RefDBType.Oracle)
            {
                string myServerName = comboBox_OracleServer.Text.Trim();
                if (string.IsNullOrEmpty(myServerName))
                {
                    string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBServerNULL");
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                    MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.button_OK.Enabled = true;
                    return 0;
                }
                string userId = textBox_OracleUserID.Text.Trim();
                if (string.IsNullOrEmpty(userId))
                {
                    string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBUserIDNULL");
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                    MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.button_OK.Enabled = true;
                    return 0;
                }
                string passWord = textBox_OracleUserPass.Text.Trim();
                string OracleConnectAs = comboBox_ConnectAs.Text.Trim();
                string OracleConnectMode = comboBox_ConnectMode.Text.Trim();
                string OracleHome = comboBox_OracleHome.Text.Trim();
                string OracleSID = string.Empty;
                string OraclePortID = string.Empty;
                string OracleOwner = textBox_Owner.Text.Trim();
                bool OracleDirect = checkBox_Direct.Checked;
                if (OracleDirect)
                {
                    OracleSID = comboBox_SID.Text.Trim();
                    if (string.IsNullOrEmpty(OracleSID))
                    {
                        string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBOracleSIDNULL");
                        string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                        MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.button_OK.Enabled = true;
                        return 0;
                    }
                    OraclePortID = textBox_OracleProtID.Text.Trim();
                    if (string.IsNullOrEmpty(OraclePortID))
                    {
                        string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBOraclePortIDNULL");
                        string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                        MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.button_OK.Enabled = true;
                        return 0;
                    }
                }

                try
                {
                    _RefDBConnection = new PMSRefDBConnection();
                    _RefDBConnection.RefDBType = RefDBType.Oracle;
                    _RefDBConnection.StrServerName = myServerName;
                    _RefDBConnection.StrDBName = OracleSID;
                    _RefDBConnection.StrUserID = userId;
                    _RefDBConnection.StrPassWord = passWord;
                    _RefDBConnection.BDirect = OracleDirect;
                    _RefDBConnection.StrConnectAs = OracleConnectAs;
                    _RefDBConnection.StrConnectMode = OracleConnectMode;
                    _RefDBConnection.StrPortID = OraclePortID;
                    _RefDBConnection.Owner = OracleOwner;
                    _conn = _RefDBConnection.GetOracleConnection();

                    if (_conn == null)
                    {
                        bConnected = false;
                        this.button_OK.Enabled = true;
                        return 0;
                    }
                    
                }
                catch (Oracle.DataAccess.Client.OracleException ex)
                {
                    MessageBox.Show(ex.ToString());
                    bConnected = false;
                    this.button_OK.Enabled = true;
                    return 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    bConnected = false;
                    this.button_OK.Enabled = true;
                    return 0;
                }

                if (_conn.State == ConnectionState.Open)
                {
                    // 连接数据库成功
                    SaveSettings();
                    bConnected = true;
                    strServerName = myServerName;
                    strDBName = OracleSID;
                    strUserID = userId;
                    strPassWord = passWord;

                    // 连接成功关闭
                    this.Close();
                    this.DialogResult = DialogResult.OK;
                    return 2;
                }
                else
                {
                    bConnected = false;
                    return 0;
                }
            }
            else if (_SelectDBType == RefDBType.MSAccess)
            {
                strMsAccessPath = textBox_MSAccessPath.Text.Trim();
                if (string.IsNullOrEmpty(strMsAccessPath))
                {
                    string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBAccessPathNULL");
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                    MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.button_OK.Enabled = true;
                    return 0;
                }

                string userId = textBox_MSAccessUID.Text.Trim();
                string passWord = textBox_MSAccessPass.Text.Trim();

                try
                {
                    _RefDBConnection = new PMSRefDBConnection();
                    _RefDBConnection.RefDBType = RefDBType.MSAccess;
                    _RefDBConnection.StrDBPath = strMsAccessPath;
                    _OleDbConn = _RefDBConnection.GetOleConnection();
                    // 利用 OleDbConnectionStringBuilder 对象来构建
                    // 连接字符串。
                    //acsStructure = new AccessStructure(strMsAccessPath, userId, passWord);
                    //if (AccessStructure.ConnectDB())
                    //    _OleDbConn = AccessStructure.OleDbConn;
                    if (_OleDbConn == null)
                    {
                        string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                        MessageBox.Show(_RefDBConnection.GetLastError(), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        bConnected = false;
                        this.button_OK.Enabled = true;
                        return 0;
                    }
                }
                catch(System.Data.OleDb.OleDbException ex)
                {
                    MessageBox.Show(ex.ToString());
                    bConnected = false;
                    this.button_OK.Enabled = true;
                    return 0;
                }

                if (_OleDbConn.State ==  ConnectionState.Open)
                {
                    // 连接数据库成功
                    SaveSettings();
                    bConnected = true;

                    return 2;
                }
                else
                {
                    bConnected = false;
                    return 0;
                }
            }
            else if (_SelectDBType == RefDBType.OleDB)
            {
                ConnectionString = comboBox_ConnectString.Text.Trim();
                if(string.IsNullOrEmpty(ConnectionString))
                {
                    bConnected = false;
                    return 0;
                }

                try
                {
                    _RefDBConnection = new PMSRefDBConnection();
                    _RefDBConnection.RefDBType = RefDBType.OleDB;
                    _RefDBConnection.ConnectString = ConnectionString;
                    _OleDbConn = _RefDBConnection.GetOleConnection();

                    if (_OleDbConn == null)
                    {
                        string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                        MessageBox.Show(_RefDBConnection.GetLastError(), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        bConnected = false;
                        this.button_OK.Enabled = true;
                        return 0;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    bConnected = false;
                    this.button_OK.Enabled = true;
                    return 0;
                }

                if (_OleDbConn.State == ConnectionState.Open)
                {
                    // 连接数据库成功
                    SaveSettings();
                    bConnected = true;
                    return 2;
                }
                else
                {
                    bConnected = false;
                    return 0;
                }
            }

            return 0;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            int iret = ConnectDB();
            if ( iret == 2)
            {
                // 连接成功关闭ParentForm
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
            else if(iret == 0)
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_DBConTestFail");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Fail");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //else if(iret == 0)
            //{
            //    string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_UserLoginFail");
            //    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Fail");
            //    MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.tabControl1.SelectedTab.Text.Equals("MS SQLServer"))
            {
                _SelectDBType = RefDBType.MSSqlServer;
            }
            else if (this.tabControl1.SelectedTab.Text.Equals("Oracle"))
            {
                _SelectDBType = RefDBType.Oracle;
            }
            else if (this.tabControl1.SelectedTab.Text.Equals("MS Access"))
            {
                _SelectDBType = RefDBType.MSAccess;
            }
            else if(this.tabControl1.SelectedTab.Text.Equals("Ole DB"))
            {
                _SelectDBType = RefDBType.OleDB;
            }
        }

        private void button_BuildString_Click(object sender, EventArgs e)
        {
            // get starting connection string
            // (if empty or no provider, start with SQL source as default)
            string connString = comboBox_ConnectString.Text;
            if (string.IsNullOrEmpty(connString) || connString.IndexOf("provider=", StringComparison.OrdinalIgnoreCase) < 0)
            {
                connString = "Provider=SQLOLEDB.1;";
            }

            // let user change it
            ConnectionString = PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.OleDbConnString.EditConnectionString(this, connString);
        }

        private void button_ConnectOracleTest_Click(object sender, EventArgs e)
        {
            string OracleServerName = comboBox_OracleServer.Text.Trim();
            string OracleUserId = textBox_OracleUserID.Text.Trim();
            string OraclePassWord = textBox_OracleUserPass.Text.Trim();
            string OracleConnectAs = comboBox_ConnectAs.Text.Trim();
            string OracleConnectMode = comboBox_ConnectMode.Text.Trim();
            string OracleHome = comboBox_OracleHome.Text.Trim();
            string OracleSID = comboBox_SID.Text.Trim();
            string OraclePortID = textBox_OracleProtID.Text.Trim();
            bool OracleDirect = checkBox_Direct.Checked;
            string strConnect = "";
            if (OracleDirect == false)
            {
                strConnect =   //Pooling = False;
                    "User Id=" + OracleUserId +
                    ";Password=" + OraclePassWord +
                    ";Data Source=" + OracleServerName +
                    ";";
                if (OracleConnectAs != "Normal" && OracleConnectAs != "")
                    strConnect += "DBA Privilege=" + OracleConnectAs +
                    ";";
            }
            else
                strConnect = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + OracleServerName +
                    ")(PORT=" + OraclePortID +
                    ")))(CONNECT_DATA=(SERVER=" + OracleConnectMode +
                    ")(SERVICE_NAME=" + OracleSID + 
                    ")));User Id=" + OracleUserId +
                    ";Password=" + OraclePassWord +
                    ";";

            OracleConnection conn = null;

            try
            {
                conn = new OracleConnection(strConnect);
                conn.Open();
            }
            catch (Oracle.DataAccess.Client.OracleException ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            if (conn.State == ConnectionState.Open)
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_DBConTestSucceed");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Succeed");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                conn.Close();
            }
            else
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_DBConTestFail");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Fail");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkBox_Direct_CheckedChanged(object sender, EventArgs e)
        {
            label15.Visible = !label15.Visible;
            comboBox_OracleHome.Visible = !comboBox_OracleHome.Visible;

            label17.Visible = !label17.Visible;
            comboBox_SID.Visible = !comboBox_SID.Visible;
            label11.Visible = !label11.Visible;
            textBox_OracleProtID.Visible = !textBox_OracleProtID.Visible;
            button_RefreshOracleDB.Visible = !button_RefreshOracleDB.Visible;
        }

        public void InitRefDBConnection(PMSRefDBConnection refdbc)
        {
            if (refdbc.RefDBType != RefDBType.NULL)
            {
                this.tabControl1.SelectedIndex = (int)(refdbc.RefDBType);
            }

            if (refdbc.RefDBType == RefDBType.MSAccess)
            {
                textBox_MSAccessPath.Text = refdbc.StrDBPath;
                textBox_MSAccessUID.Text = refdbc.StrUserID;
                textBox_MSAccessPass.Text = refdbc.StrPassWord;
            }
            else if (refdbc.RefDBType == RefDBType.MSSqlServer)
            {
                comboBox_Server.Text = refdbc.StrServerName;
                comboBox_DB.Text = refdbc.StrDBName;
                textBox_UserName.Text = refdbc.StrUserID;
                textBox_Password.Text = refdbc.StrPassWord;
                comboBox_Protocol.SelectedIndex = (int)(refdbc.EConnectType)-1;
                textBox_PortID.Text = refdbc.StrPortID;
            }
            else if (refdbc.RefDBType == RefDBType.Oracle)
            {
                comboBox_OracleServer.Text = refdbc.StrServerName;
                textBox_OracleUserID.Text = refdbc.StrUserID;
                textBox_OracleUserPass.Text = refdbc.StrPassWord;
                comboBox_ConnectAs.Text = refdbc.StrConnectAs;
                comboBox_ConnectMode.Text = refdbc.StrConnectMode;
                //comboBox_OracleHome.Text = refdbc.;
                comboBox_SID.Text = refdbc.StrDBName;
                textBox_OracleProtID.Text = refdbc.StrPortID;
                textBox_Owner.Text = refdbc.Owner;
                this.checkBox_Direct.Checked = refdbc.BDirect;
            }
            else if (refdbc.RefDBType == RefDBType.OleDB)
            {
                this.comboBox_ConnectString.Text = refdbc.ConnectString;
            }
        }

        private void DBRefStructForm_Load(object sender, EventArgs e)
        {
            if (null != RefDBConnection)
            {
                InitRefDBConnection(RefDBConnection);
            }
            else
                LoadSettings();

            tabControl1_SelectedIndexChanged(null,null);
        }

        private void button_ConnectAccessTest_Click(object sender, EventArgs e)
        {
            string strMsAccessPath = textBox_MSAccessPath.Text.Trim();
            strMsAccessPath = ProjectPathClass.ParseStringWithMacro(strMsAccessPath);
            if (string.IsNullOrEmpty(strMsAccessPath))
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBAccessPathNULL");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string userId = textBox_MSAccessUID.Text.Trim();
            string passWord = textBox_MSAccessPass.Text.Trim();

            try
            {
                OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                connectStringBuilder.DataSource = strMsAccessPath;
                connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                OleDbConnection _OleDbConn = new OleDbConnection(connectStringBuilder.ConnectionString);
                _OleDbConn.Open();
                
                if (_OleDbConn == null)
                {
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                    MessageBox.Show(_RefDBConnection.GetLastError(), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (_OleDbConn.State == ConnectionState.Open)
                {
                    string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_DBConTestSucceed");
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Succeed");
                    MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    _OleDbConn.Close();
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip1.Show((Button)sender, new Point(e.X, e.Y));
            }
        }

        private void toolStripMenuItem_PRJPATH_Click(object sender, EventArgs e)
        {
            textBox_MSAccessPath.Paste(sender.ToString());
        }

        private void Btn_OleDBTest_Click(object sender, EventArgs e)
        {
            string strOleConnectString = comboBox_ConnectString.Text.Trim();
            if (string.IsNullOrEmpty(strOleConnectString))
            {
                string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSOleDBConnectStringNull");
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                OleDbConnection _OleDbConn = new OleDbConnection(strOleConnectString);
                _OleDbConn.Open();

                if (_OleDbConn == null)
                {
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                    MessageBox.Show(_RefDBConnection.GetLastError(), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_OleDbConn.State == ConnectionState.Open)
                {
                    string strText = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSDBConnectCtrl_DBConTestSucceed");
                    string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Succeed");
                    MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    _OleDbConn.Close();
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
