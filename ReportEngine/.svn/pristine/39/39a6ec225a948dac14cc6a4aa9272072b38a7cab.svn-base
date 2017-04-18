using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using System.Data;
using System.Data.Sql;
using System.Data.OleDb;
using System.Linq;
using System.Data.Linq;
using System.Data.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;
using Oracle.DataAccess.Client;
using System.Threading;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    public enum ConnectType
    {
        namepipe = 1,
        tcpip
    }

    public enum ConnectMode
    {
        Dedicated = 1,
        Shared
    }

    public enum RelationStatus
    {
        Existed,
        None,
        Disable
    };

    public class MITableRelationEqualityComparer : IEqualityComparer<View_GetMITableRelation>
    {
        #region IEqualityComparer<View_GetMITableRelation> Members

        public bool Equals(View_GetMITableRelation x, View_GetMITableRelation y)
        {
            if (x.RelationTable == y.RelationTable)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(View_GetMITableRelation obj)
        {
            //由于是根据RelationTable来进行判等的，
            //所以哈希值的求取过程也要进行一定的修改，
            //确保相等对象的哈希值也是相等的。
            return obj.RelationTable.GetHashCode();
        }

        #endregion
    }

    public class IITableRelationEqualityComparer : IEqualityComparer<View_GetIITableRelation>
    {
        #region IEqualityComparer<View_GetIITableRelation> Members

        public bool Equals(View_GetIITableRelation x, View_GetIITableRelation y)
        {
            if (x.RelationTable == y.RelationTable)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(View_GetIITableRelation obj)
        {
            //由于是根据RelationTable来进行判等的，
            //所以哈希值的求取过程也要进行一定的修改，
            //确保相等对象的哈希值也是相等的。
            return obj.RelationTable.GetHashCode();
        }

        #endregion
    }

    public class SqlAssistance
    {
        #region private member

        private SqlConnection _conn = null;
        private Server _server = null;
        private ServerConnection _ServerConnection = null;
        private string _serverInstance;
        private string _databaseName;
        private string _userName;
        private string _password;
        private string _portID;
        private ConnectType _connectType = ConnectType.namepipe;
        private OleDbConnection _OleDBConn = null;
        private string _ConnectionString = string.Empty;

        #endregion

        #region property

        public string SqlServerInstance
        {
            get { return _serverInstance; }
            set { _serverInstance = value; }
        }
        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public OleDbConnection OleDBConn
        {
            get { return _OleDBConn; }
            set { _OleDBConn = value; }
        }

        public SqlConnection SqlConnection
        {
            get { return _conn; }
            set { _conn = value; }
        }

        public string PortID
        {
            get { return _portID; }
            set { _portID = value; }
        }

        public ConnectType ConnectType
        {
            get { return _connectType; }
            set { _connectType = value; }
        }

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        #endregion

        public SqlAssistance()
        {
            _serverInstance = "";
            _databaseName = "";
            _userName = "";
            _password = "";
        }

        /// <summary>
        /// 枚举所有sql服务器(包括本地和网络)
        /// </summary>
        /// <returns>服务器列表</returns>
        public DataTable EnumSqlservers()
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable dtServers = instance.GetDataSources();
            return dtServers;
        }

        /// <summary>
        /// 使用SMO枚举所有sql服务器( 是否本地)
        /// </summary>
        /// <param name="localOnly">是否只枚举本地</param>
        /// <returns>服务器列表</returns>
        public DataTable EnumSqlservers(bool localOnly)
        {
            DataTable dtServers = SmoApplication.EnumAvailableSqlServers(localOnly);
            return dtServers;
        }

        public bool ConnectSMOServer()
        {
            return this.ConnectSMOServer(_serverInstance, _userName, _password);
        }

        public bool ConnectSMOServer(string serverInstance)
        {
            return this.ConnectSMOServer(serverInstance, _userName, _password);
        }

        public bool ConnectSMOServer(SqlConnection sqlconnect)
        {
            try
            {
                _ServerConnection = new ServerConnection(sqlconnect);
                //_ServerConnection.NonPooledConnection = true;
                _ServerConnection.Connect();
                _server = new Server(_ServerConnection);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            catch (SmoException smoe)
            {
                MessageBox.Show(smoe.ToString());
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        public Server ConnectNewSMOServer(SqlConnection sqlconnect)
        {
            try
            {
                ServerConnection sc = new ServerConnection(sqlconnect);
                //_ServerConnection.NonPooledConnection = true;
                sc.Connect();
                Server s = new Server(_ServerConnection);
                s.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;
                return s;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (SmoException smoe)
            {
                MessageBox.Show(smoe.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return null;
        }

        public bool ConnectSMOServer(string serverInstance, string userName, string password)
        {
            if (serverInstance == _serverInstance)
                return true;
            
            try
            {
                _ServerConnection = new ServerConnection(serverInstance, userName, password);
                //_ServerConnection.NonPooledConnection = true;
                _ServerConnection.Connect();
                _server = new Server(_ServerConnection);

                _serverInstance = serverInstance;
                _userName = userName;
                _password = password;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            catch (SmoException smoe)
            {
                MessageBox.Show(smoe.ToString());
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        public bool DisConnectSMOServer()
        {
            try
            {
                if(_ServerConnection.IsOpen == true)
                {
                    _ServerConnection.Disconnect();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            catch (SmoException smoe)
            {
                MessageBox.Show(smoe.ToString());
                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        public bool ConnectDatabase()
        {
            if(_connectType == ConnectType.namepipe)
                return this.ConnectDatabase(_serverInstance, _databaseName, _userName, _password);
            else if(_connectType == ConnectType.tcpip)
                return this.ConnectRemoteDatabase(_serverInstance, _databaseName, _userName, _password, _portID);
            return false;
        }

        public SqlConnection GetNewConnection(int timeout = 30)
        {
            try
            {
                string connectString = string.Empty;
                if (_connectType == ConnectType.namepipe)
                    connectString = this.ConnectDatabaseString(_serverInstance, _databaseName, _userName, _password, timeout);
                else if (_connectType == ConnectType.tcpip)
                    connectString = this.ConnectRemoteDatabaseString(_serverInstance, _databaseName, _userName, _password, _portID, timeout);
                SqlConnection conn = new SqlConnection(connectString);
                conn.Open();
                return conn;
            }
            catch (System.Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.ToString());
            }
            return null;
        }

        public bool ConnectDatabase(string strServer, string strDatabase, string strUserID, string strPassword)
        {//Pooling = 'false';
            _ConnectionString = "Server=" + strServer +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout=30;";
            return this.ConnectDatabase(_ConnectionString);
        }

        public string ConnectDatabaseString(string strServer, string strDatabase, string strUserID, string strPassword)
        {//Pooling = 'false';
            string ConnectionString = "Server=" + strServer +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout=30;";
            return ConnectionString;
        }

        public string ConnectDatabaseString(string strServer, string strDatabase, string strUserID, string strPassword, int timeout = 30)
        {//Pooling = 'false';
            string ConnectionString = "Server=" + strServer +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout="+ timeout +";";
            return ConnectionString;
        }

        public string ConnectDatabaseString(string strServer, string strDatabase, string strUserID)
        {//Pooling = 'false';
            string ConnectionString = "Server=" + strServer +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Connect Timeout=30;";
            return ConnectionString;
        }

        public string ConnectDatabaseString(string strServer, string strDatabase, string strUserID, int timeout = 30)
        {//Pooling = 'false';
            string ConnectionString = "Server=" + strServer +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Connect Timeout="+ timeout +";";
            return ConnectionString;
        }

        public bool ConnectRemoteDatabase()
        {
            return this.ConnectRemoteDatabase(_serverInstance, _databaseName, _userName, _password, _portID);
        }

        public bool ConnectRemoteDatabase(string strServer, string strDatabase, string strUserID, string strPassword, string strPortID)
        {//Pooling = 'false';
            _ConnectionString = "Server=tcp:" + strServer + "," + strPortID +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout=30;";
            return this.ConnectDatabase(_ConnectionString);
        }

        public string ConnectRemoteDatabaseString(string strServer, string strDatabase, string strUserID, string strPassword, string strPortID)
        {
            return ConnectRemoteDatabaseString(strServer, strDatabase, strUserID, strPassword, strPortID, 30);
        }

        public string ConnectRemoteDatabaseString(string strServer, string strDatabase, string strUserID, string strPassword, string strPortID, int timeout = 30)
        {//Pooling = 'false';
            string ConnectionString = "Server=tcp:" + strServer + "," + strPortID +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout="+ timeout +";";
            return ConnectionString;
        }

        public string ConnectRemoteDatabaseString(string strServer, string strDatabase, string strUserID, string strPortID)
        {//Pooling = 'false';
            string ConnectionString = "Server=tcp:" + strServer + "," + strPortID +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Connect Timeout=30;";
            return ConnectionString;
        }

        public bool ConnectDatabase(string sqlConnectionString)
        {
            try
            {
                _conn = new SqlConnection(sqlConnectionString);
                _conn.Open();
                _ConnectionString = sqlConnectionString;
                //if (_conn.State != ConnectionState.Open)
                //{
                //    MessageBox.Show((string.Format(_getStringFromPR.GetStringFromPublicResource("PMS_ConnectError"),strServer,strDatabase)));
                //    return false;
                //}
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(sqlConnectionString+ex.ToString());
                return false;
            }
            return true;
        }

        public bool ConnectOleDB()
        {
            if (_connectType == ConnectType.namepipe)
                return this.ConnectOleDB(_serverInstance, _databaseName, _userName, _password);
            else if (_connectType == ConnectType.tcpip)
                return this.ConnectRemoteOleDB(_serverInstance, _databaseName, _userName, _password, _portID);
            return false;
        }

        public bool ConnectOleDB(string strServer, string strDatabase, string strUserID, string strPassword)
        {//Pooling = 'false';
            _ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=True;" + 
                                        ";Data Source=" +strServer +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout=30;";
            return this.ConnectOleDB(_ConnectionString);
        }

        public string ConnectOleDBString(string strServer, string strDatabase, string strUserID, string strPassword)
        {
            string ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=True;" +
                                        ";Data Source=" + strServer +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout=30;";
            return ConnectionString;
        }

        public string ConnectOleDBString(string strServer, string strDatabase, string strUserID)
        {
            string ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=True;" +
                                        ";Data Source=" + strServer +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Connect Timeout=30;";
            return ConnectionString;
        }

        public bool ConnectRemoteOleDB()
        {
            return this.ConnectRemoteOleDB(_serverInstance, _databaseName, _userName, _password, _portID);
        }

        public bool ConnectRemoteOleDB(string strServer, string strDatabase, string strUserID, string strPassword, string strPortID)
        {//Pooling = 'false';
            _ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=True;" +
                                        "Data Source=" + strServer + "," + strPortID +
                                        ";Network Library=DBMSSOCN" +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout=30;";
            return this.ConnectOleDB(_ConnectionString);
        }

        public string ConnectRemoteOleDBString(string strServer, string strDatabase, string strUserID, string strPassword, string strPortID)
        {
            string ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=True;" +
                                        "Data Source=" + strServer + "," + strPortID +
                                        ";Network Library=DBMSSOCN" +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout=30;";
            return ConnectionString;
        }

        public string ConnectRemoteOleDBString(string strServer, string strDatabase, string strUserID, string strPortID)
        {
            string ConnectionString = "Provider=SQLOLEDB.1;Persist Security Info=True;" +
                                        "Data Source=" + strServer + "," + strPortID +
                                        ";Network Library=DBMSSOCN" +
                                        ";Initial Catalog=" + strDatabase +
                                        ";User Id=" + strUserID +
                                        ";Connect Timeout=30;";
            return ConnectionString;
        }

        public bool ConnectOleDB(string sqlConnectionString)
        {
            try
            {
                _OleDBConn = new  OleDbConnection(sqlConnectionString);
                _OleDBConn.Open();
                //if (_conn.State != ConnectionState.Open)
                //{
                //    MessageBox.Show((string.Format(_getStringFromPR.GetStringFromPublicResource("PMS_ConnectError"),strServer,strDatabase)));
                //    return false;
                //}
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public bool DisConnectDatabase()
        {
            try
            {
                if (_conn != null)
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }

                if (_OleDBConn != null)
                {
                    if (_OleDBConn.State == ConnectionState.Open)
                    {
                        _OleDBConn.Close();
                    }
                }
                
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public bool ExecuteSqlScript(string strFilePath)
        {
            if (File.Exists(strFilePath) == false)
            {
                //string strText = strFilePath+_getStringFromPR.GetStringFromPublicResource("PMS_NotExisted");
                //string strCaption = _getStringFromPR.GetStringFromPublicResource("PMS_Warnning");
                //MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            FileInfo file = new FileInfo(strFilePath);

            string script = file.OpenText().ReadToEnd();

            if (_server != null)
            {
                _server.ConnectionContext.ExecuteNonQuery(script);
            }
            else
            {
                return false;
            }
            return true;
        }

        #region Get all databases from current instance

        public string GetDatabaseDescription(string dbName)
        {
            try
            {
                foreach (Microsoft.SqlServer.Management.Smo.Database db in _server.Databases)
                {
                    //We don't want to be adding the System databases to our list
                    //Check if database is system database
                    if (!db.IsSystemObject)
                    {
                        if (db.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase))
                            if (db.ExtendedProperties["MS_Description"] != null)
                                return db.ExtendedProperties["MS_Description"].Value.ToString();
                    }
                }
                return null;
            }
            catch (SmoException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string GetDatabaseExtendedPropertie(string dbName,string propertyName)
        {
            try
            {
                foreach (Microsoft.SqlServer.Management.Smo.Database db in _server.Databases)
                {
                    //We don't want to be adding the System databases to our list
                    //Check if database is system database
                    if (!db.IsSystemObject)
                    {
                        if (db.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase))
                            if (db.ExtendedProperties[propertyName] != null)
                                return db.ExtendedProperties[propertyName].Value.ToString();
                    }
                }
                return null;
            }
            catch (SmoException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public List<string> GetServerDatabases()
        {
            try
            {
                // create a list of strings to hold name of databases
                List<string> databases = new List<string>();

                if (_server == null)
                    return null;

                // iterate through each database that exist in _server object
                foreach (Microsoft.SqlServer.Management.Smo.Database db in _server.Databases)
                {
                    //We don't want to be adding the System databases to our list
                    //Check if database is system database
                    if (!db.IsSystemObject)
                    {
                        databases.Add(db.Name);
                    }
                }
                return databases;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void GetServerDatabases(TreeView tree)
        {
            try
            {
                // clear current nodes (to avoid cross-thread problem, use anonymous delegates)
                tree.Invoke(new MethodInvoker(delegate
                {
                    tree.Nodes.Clear();
                }));

                // create a list of strings to hold name of databases
                List<string> databases = new List<string>();

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // iterate through each database that exist in _server object
                foreach (Microsoft.SqlServer.Management.Smo.Database db in _server.Databases)
                    databases.Add(db.Name);

                // iterate through list object and add each item (database name) to treeView
                foreach (string str in databases)
                {
                    // create a node to hold database name (main node or level 0)
                    TreeNode dbNode = new TreeNode(str);

                    // create a node named 'Stored Procedures' to hold storedProcedures for each database (level 1)
                    // then add child named 'Objects' to add + mark for 'spsNode' node
                    TreeNode spsNode = new TreeNode("Stored Procedures");
                    spsNode.Nodes.Add("Objects");

                    // create a node named 'Tables' to hold tables for each database (level 1)
                    // then add child named 'Objects' to add + mark for 'Tables' node
                    TreeNode tablesNode = new TreeNode("Tables");
                    tablesNode.Nodes.Add("Objects");

                    // add Tabales and StoredProcedures node as child to database node
                    dbNode.Nodes.Add(spsNode);
                    dbNode.Nodes.Add(tablesNode);

                    // add db node to treeView (to avoid cross-thread problem, use anonymous delegates)
                    tree.Invoke(new MethodInvoker(delegate
                    {
                        tree.Nodes.Add(dbNode);
                    }
                    ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetServerDatabases(TreeView tree, ImageList imageList, int dbImageIndex, int tableImageIndex, int spImageIndex)
        {
            try
            {
                // clear current nodes (to avoid cross-thread problem, use anonymous delegates)
                tree.Invoke(new MethodInvoker(delegate
                {
                    tree.Nodes.Clear();
                }));

                // create a list of strings to hold name of databases
                List<string> databases = new List<string>();

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // iterate through each database that exist in _server object
                foreach (Microsoft.SqlServer.Management.Smo.Database db in _server.Databases)
                    databases.Add(db.Name);

                // iterate through list object and add each item (database name) to treeView
                foreach (string str in databases)
                {
                    // create a node to hold database name (main node or level 0)
                    TreeNode dbNode = new TreeNode(str);
                    dbNode.ImageIndex = dbImageIndex;

                    // create a node named 'Stored Procedures' to hold storedProcedures for each database (level 1)
                    // then add child named 'Objects' to add + mark for 'spsNode' node
                    TreeNode spsNode = new TreeNode("Stored Procedures");
                    spsNode.ImageIndex = spImageIndex;
                    spsNode.SelectedImageIndex = spImageIndex;
                    spsNode.Nodes.Add("Objects");

                    // create a node named 'Tables' to hold tables for each database (level 1)
                    // then add child named 'Objects' to add + mark for 'Tables' node
                    TreeNode tablesNode = new TreeNode("Tables");
                    tablesNode.ImageIndex = tableImageIndex;
                    tablesNode.SelectedImageIndex = tableImageIndex;
                    tablesNode.Nodes.Add("Objects");

                    // add Tabales and StoredProcedures node as child to database node                    
                    dbNode.Nodes.Add(spsNode);
                    dbNode.Nodes.Add(tablesNode);

                    // add db node to treeView (to avoid cross-thread problem, use anonymous delegates)
                    tree.Invoke(new MethodInvoker(delegate
                    {
                        // set tree.ImageList to our imageList parameter to use it's imageIndex
                        tree.ImageList = imageList;

                        tree.Nodes.Add(dbNode);
                    }
                    ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Get all tables from current databse

        public string GetTableDescription(string tableName)
        {
            try
            {
                foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[_databaseName].Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        if (table.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase))
                        {
                            if (table.ExtendedProperties["Description"] != null)
                                return table.ExtendedProperties["Description"].Value.ToString();
                            else
                                return null;
                        }
                    }
                }
                return null;
            }
            catch(SmoException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string GetTableExtendedPropertie(string tableName,string propertyName)
        {
            try
            {
                foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[_databaseName].Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        if (table.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase))
                        {
                            if (table.ExtendedProperties[propertyName] != null)
                                return table.ExtendedProperties[propertyName].Value.ToString();
                            else
                                return null;
                        }
                    }
                }
                return null;
            }
            catch (SmoException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public List<string> GetDatabaseTables()
        {
            return this.GetDatabaseTables(_databaseName);
        }

        public Microsoft.SqlServer.Management.Smo.Table GetDatabaseTableObj(string tableName)
        {
            try
            {
                return _server.Databases[_databaseName].Tables[tableName];
            }
            catch
            {
                return null;
            }
        }

        public List<string> GetDatabaseTables(string database)
        {
            try
            {
                _databaseName = database;
                List<string> tableNames = new List<string>();
                foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[database].Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        //foreach (ForeignKey key in table.ForeignKeys) 
                        //{ 
                        //    foreach (ForeignKeyColumn column in key.Columns) 
                        //    { 
                        //        Console.WriteLine("Column: {0} is a foreign key to Table: {1}", column.Name, key.ReferencedTable); 
                        //    } 
                        //}
                        tableNames.Add(table.Name);
                    }
                }
                return tableNames;
            }
            catch
            {
                return null;
            }
        }

        public TableCollection GetDatabaseTableCollection(string database)
        {
            try
            {
                _databaseName = database;
                return _server.Databases[database].Tables;
            }
            catch
            {
                return null;
            }
        }

        public List<string> GetDatabaseTablesByType(string tableType)
        {
            try
            {
                if (string.IsNullOrEmpty(_databaseName))
                    return null;
                List<string> tableNames = new List<string>();
                foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[_databaseName].Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        if (GetTableExtendedPropertie(table.Name, "TableType") == tableType)
                            tableNames.Add(table.Name);
                    }
                }
                return tableNames;
            }
            catch
            {
                return null;
            }
        }
        
        public void GetDatabaseTables(TreeView tree, TreeNode tablesNode)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // create a list of strings to hold name of tables
                List<string> tables = new List<string>();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = tablesNode.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for Table type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Table), "IsSystemObject");

                // iterate through each table that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[database].Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        tables.Add(table.Name);
                    }
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDatabaseTables(TreeView tree, TreeNode tablesNode, string strDBName, ImageList imageList, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of strings to hold name of tables
                List<string> tables = new List<string>();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = strDBName;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for Table type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Table), "IsSystemObject");

                // iterate through each table that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[database].Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        tables.Add(table.Name);
                    }
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.ImageIndex = tableImageIndex;
                    tableNode.SelectedImageIndex = tableImageIndex;
                    PMSRefDBTableProp refTableProp = new PMSRefDBTableProp();
                    refTableProp.StrTableName = str;
                    refTableProp.PMSRefDBConnection = (PMSRefDBConnection)(tablesNode.Tag);
                    tableNode.Tag = refTableProp;
                    tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDatabaseTables(TreeView tree, TreeNode tablesNode, ImageList imageList, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of strings to hold name of tables
                List<string> tables = new List<string>();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = tablesNode.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for Table type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Table), "IsSystemObject");

                // iterate through each table that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[database].Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        tables.Add(table.Name);
                    }
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.ImageIndex = tableImageIndex;
                    tableNode.SelectedImageIndex = tableImageIndex;
                    PMSDBTableProp TableProp = new PMSDBTableProp();
                    TableProp.StrTableName = str;
                    TableProp.ExProps = PMSDBStructure.GetTableProp(str);
                    //TableProp.ExProps = new Dictionary<string, PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp>();
                    //PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp exp = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp();
                    //exp.StrPropName = PMS.Libraries.ToolControls.PMSPublicInfo.TablePropertyName.TableType;
                    //exp.StrPropValue = PMSDBStructure.GetTablePropertie(str, TablePropertyName.TableType);
                    //TableProp.ExProps.Add(exp.StrPropName, exp);
                    //exp = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp();
                    //exp.StrPropName = PMS.Libraries.ToolControls.PMSPublicInfo.TablePropertyName.Description;
                    //exp.StrPropValue = PMSDBStructure.GetTableDescription(str);
                    //TableProp.ExProps.Add(exp.StrPropName, exp);

                    TableProp.FieldPropCollection = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBFieldPropCollection();
                    tableNode.Tag = TableProp;
                    tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Dictionary<string, PMSRefDBTableProp> GetDatabaseTables(PMSRefDBConnection rc)
        {
            try
            {
                List<string> tables = new List<string>();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = _databaseName;

                if (rc.ConnectString != this._ServerConnection.SqlConnectionObject.ConnectionString)
                {
                    DisConnectSMOServer();
                    ConnectSMOServer(rc.StrServerName,rc.StrUserID,rc.StrPassWord);
                }
                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for Table type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Table), "IsSystemObject");

                // iterate through each table that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[rc.StrDBName].Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        tables.Add(table.Name);
                    }
                }

                Dictionary<string, PMSRefDBTableProp> dic = new Dictionary<string, PMSRefDBTableProp>();

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    PMSRefDBTableProp refTableProp = new PMSRefDBTableProp();
                    refTableProp.StrTableName = str;
                    refTableProp.PMSRefDBConnection = rc;

                    dic.Add(str, refTableProp);
                }
                return dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public PMSDBTableProp GetPMSDBTableProp(string tablename)
        {
            try
            {
                PMSDBTableProp TableProp = null;

                string database = _databaseName;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for Table type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Table), "IsSystemObject");

                // iterate through each table that exist in database object
                Microsoft.SqlServer.Management.Smo.Table table = _server.Databases[database].Tables[tablename];

                if (table != null)
                {
                    TableProp = new PMSDBTableProp();
                    TableProp.StrTableName = tablename;
                    TableProp.ExProps = new Dictionary<string, PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp>();
                    PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp exp = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp();
                    exp.StrPropName = PMS.Libraries.ToolControls.PMSPublicInfo.TablePropertyName.TableType;
                    exp.StrPropValue = PMSDBStructure.GetTablePropertie(tablename, TablePropertyName.TableType);
                    TableProp.ExProps.Add(exp.StrPropName, exp);
                    exp = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp();
                    exp.StrPropName = PMS.Libraries.ToolControls.PMSPublicInfo.TablePropertyName.Description;
                    exp.StrPropValue = PMSDBStructure.GetTableDescription(tablename);
                    TableProp.ExProps.Add(exp.StrPropName, exp);

                    TableProp.FieldPropCollection = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBFieldPropCollection();

                    // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all columns
                    List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();

                    // iterate through each column that exist in table object
                    foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[database].Tables[tablename].Columns)
                    {
                        columns.Add(col);
                    }

                    // iterate through list object and add each item (column name) to treeView
                    foreach (Microsoft.SqlServer.Management.Smo.Column col in columns)
                    {
                        PMSDBFieldProp DBFieldProp = new PMSDBFieldProp();
                        DBFieldProp.IFildID = col.ID;
                        DBFieldProp.StrFieldName = col.Name;
                        DBFieldProp.StrFieldType = col.DataType.ToString();
                        DBFieldProp.IFieldLength = col.DataType.MaximumLength;
                        DBFieldProp.BFieldNullable = col.Nullable;
                        DBFieldProp.BFieldPrimaryKey = col.InPrimaryKey;
                        DBFieldProp.BFieldForeignKey = col.IsForeignKey;
                        DBFieldProp.BFieldIdentity = col.Identity;
                        DBFieldProp.BFieldIsSystem = PMSDBStructure.IsTableColumnSystem(tablename, col.Name);
                        DBFieldProp.StrFieldDefault = (col.DefaultConstraint == null) ? null : col.DefaultConstraint.Text;
                        DBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(tablename, col.Name);
                        DBFieldProp.StrTableName = tablename;
                        TableProp.FieldPropCollection.Add(DBFieldProp);
                    }
                }

                return TableProp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void GetDatabaseTables(string database, TreeNode tablesNode, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // create a list of strings to hold name of tables
                List<string> tables = new List<string>();

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for Table type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Table), "IsSystemObject");

                // iterate through each table that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[database].Tables)
                {
                    if (!table.IsSystemObject)
                    {
                        tables.Add(table.Name);
                    }
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.ImageIndex = tableImageIndex;
                    tableNode.SelectedImageIndex = tableImageIndex;
                    //tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool IsTableExisted(string tableName)
        {
            List<string> tableNameList = this.GetDatabaseTables(_databaseName);

            return tableNameList.Contains(tableName, new DBTableNameComparer());
        }

        #endregion

        #region Get all stored procedures from current database

        public void GetDatabaseSps(TreeView tree, TreeNode spsNode)
        {
            try
            {
                // clear current nodes
                spsNode.Nodes.Clear();

                // create a list of strings to hold name of sps
                List<string> sps = new List<string>();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = spsNode.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for StoredProcedure type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedure), "IsSystemObject");

                // iterate through each sp that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.StoredProcedure sp in _server.Databases[database].StoredProcedures)
                {
                    if (!sp.IsSystemObject)
                    {
                        sps.Add(sp.Name);
                    }
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in sps)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for sp node
                    TreeNode spNode = new TreeNode(str);
                    spNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    spsNode.Nodes.Add(spNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDatabaseSps(TreeView tree, TreeNode spsNode, string strDBName, ImageList imageList, int spImageIndex)
        {
            try
            {
                // clear current nodes
                spsNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of strings to hold name of sps
                List<string> sps = new List<string>();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = strDBName;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for StoredProcedure type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedure), "IsSystemObject");

                // iterate through each sp that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.StoredProcedure sp in _server.Databases[database].StoredProcedures)
                {
                    if (!sp.IsSystemObject)
                    {
                        sps.Add(sp.Name);
                    }
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in sps)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for sp node
                    TreeNode spNode = new TreeNode(str);
                    spNode.ImageIndex = spImageIndex;
                    spNode.SelectedImageIndex = spImageIndex;
                    PMSRefDBSpProp refTableSpProp = new PMSRefDBSpProp();
                    refTableSpProp.StrTableName = str;
                    refTableSpProp.PMSRefDBConnection = (PMSRefDBConnection)(spsNode.Tag);
                    spNode.Tag = refTableSpProp;
                    spNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    spsNode.Nodes.Add(spNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region get all views from current database

        public void GetDatabaseViews(TreeView tree, TreeNode viewsNode)
        {
            try
            {
                // clear current nodes
                viewsNode.Nodes.Clear();

                // create a list of strings to hold name of sps
                List<string> views = new List<string>();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = viewsNode.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for StoredProcedure type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.View), "IsSystemObject");

                // iterate through each sp that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.View view in _server.Databases[database].Views)
                {
                    if (!view.IsSystemObject)
                    {
                        views.Add(view.Name);
                    }
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in views)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for sp node
                    TreeNode spNode = new TreeNode(str);
                    spNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    viewsNode.Nodes.Add(spNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDatabaseViews(TreeView tree, TreeNode viewsNode, string strDBName, ImageList imageList, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                viewsNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of strings to hold name of tables
                List<string> tables = new List<string>();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = strDBName;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for Table type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Table), "IsSystemObject");

                // iterate through each table that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.View view in _server.Databases[database].Views)
                {
                    if (!view.IsSystemObject)
                    {
                        tables.Add(view.Name);
                    }
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode viewNode = new TreeNode(str);
                    viewNode.ImageIndex = tableImageIndex;
                    viewNode.SelectedImageIndex = tableImageIndex;
                    PMSRefDBViewProp refTableProp = new PMSRefDBViewProp();
                    refTableProp.StrTableName = str;
                    refTableProp.PMSRefDBConnection = (PMSRefDBConnection)(viewsNode.Tag);
                    viewNode.Tag = refTableProp;
                    viewNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    viewsNode.Nodes.Add(viewNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Dictionary<string, PMSRefDBViewProp> GetDatabaseViews(PMSRefDBConnection rc)
        {
            try
            {
                List<string> views = new List<string>();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = _databaseName;

                if (rc.ConnectString != this._ServerConnection.SqlConnectionObject.ConnectionString)
                {
                    DisConnectSMOServer();
                    ConnectSMOServer(rc.StrServerName, rc.StrUserID, rc.StrPassWord);
                }
                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set SystemObject for Table type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Table), "IsSystemObject");

                // iterate through each table that exist in database object
                foreach (Microsoft.SqlServer.Management.Smo.View view in _server.Databases[rc.StrDBName].Views)
                {
                    if (!view.IsSystemObject)
                    {
                        views.Add(view.Name);
                    }
                }

                Dictionary<string, PMSRefDBViewProp> dic = new Dictionary<string, PMSRefDBViewProp>();

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in views)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    
                    PMSRefDBViewProp refViewProp = new PMSRefDBViewProp();
                    refViewProp.StrTableName = str;
                    refViewProp.PMSRefDBConnection = rc;

                    dic.Add(str, refViewProp);
                }
                return dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        #endregion

        #region Get all columns from current table

        public string GetTableColumnDescription(string tableName,string columnName)
        {
            try
            {
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[_databaseName].Tables[tableName].Columns)
                {
                    if (col.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (col.ExtendedProperties["MS_Description"] != null)
                            return col.ExtendedProperties["MS_Description"].Value.ToString();
                        else
                            return null;
                    }
                }
                return null;
            }
            catch (SmoException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public string GetTableColumnExtendedPropertie(string tableName, string columnName,string propertyName)
        {
            try
            {
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[_databaseName].Tables[tableName].Columns)
                {
                    if (col.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (col.ExtendedProperties[propertyName] != null)
                            return col.ExtendedProperties[propertyName].ToString();
                        else
                            return null;
                    }
                }
                return null;
            }
            catch (SmoException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        
        public List<Microsoft.SqlServer.Management.Smo.Column> GetTableColumns(string tableName)
        {
            try
            {
                List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[_databaseName].Tables[tableName].Columns)
                {
                    columns.Add(col);
                }
                return columns;
            }
            catch
            {
                return null;
            }
        }

        public void GetTableColumns(TreeView tree, TreeNode tableNode)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all columns
                List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();

                // get database name from parent node (database node is parent node of tablesNode, the tablesNode is parent of tableNode)
                string database = tableNode.Parent.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set Name,DataType for Column type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Column), new string[] { "Name", "DataType" });

                // iterate through each column that exist in table object
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[database].Tables[tableNode.Text].Columns)
                {
                    columns.Add(col);
                }

                // iterate through list object and add each item (column name) to treeView
                foreach (Microsoft.SqlServer.Management.Smo.Column col in columns)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.Name);
                    colNode.Text += "," + col.DataType.ToString();

                    // add tableNode to tablesNode
                    tableNode.Nodes.Add(colNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all columns
                List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();

                // get database name from parent node (database node is parent node of tablesNode, the tablesNode is parent of tableNode)
                string database = tableNode.Parent.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set Name,DataType for Column type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Column), new string[] { "Name", "DataType" });

                string TableName = "";
                if (tableNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    TableName = tableNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());

                // iterate through each column that exist in table object
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[database].Tables[TableName].Columns)
                {
                    columns.Add(col);
                }

                // iterate through list object and add each item (column name) to treeView
                foreach (Microsoft.SqlServer.Management.Smo.Column col in columns)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.Name);
                    colNode.ImageIndex = columnImageIndex;
                    colNode.SelectedImageIndex = columnImageIndex;
                    string strSize = "";
                    if (col.DataType.ToString().Contains("char"))
                        strSize = string.Format("({0})", col.DataType.MaximumLength.ToString());
                    string strNullable;
                    if(col.Nullable)
                        strNullable = "null";
                    else
                        strNullable = "not null";
                    colNode.Text = string.Format("{0} ({1}{2}, {3})", colNode.Text, col.DataType.ToString(), strSize, strNullable);

                    // add tableNode to tablesNode
                    tableNode.Nodes.Add(colNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all columns
                List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();

                // get database name from parent node (database node is parent node of tablesNode, the tablesNode is parent of tableNode)
                string database = DatabaseName;//tableNode.Parent.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set Name,DataType for Column type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Column), new string[] { "Name", "DataType" });

                string TableName = "";
                if (tableNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    TableName = tableNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    TableName = tableNode.Text;

                // iterate through each column that exist in table object
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[database].Tables[TableName].Columns)
                {
                    columns.Add(col);
                }

                // iterate through list object and add each item (column name) to treeView
                foreach (Microsoft.SqlServer.Management.Smo.Column col in columns)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.Name);
                    string strpfKey = "";
                    if (col.InPrimaryKey && col.IsForeignKey)
                    {
                        colNode.ImageIndex = pfkeyImageIndex;
                        colNode.SelectedImageIndex = pfkeyImageIndex;
                        strpfKey = "PK, FK, ";
                    }
                    else if (col.InPrimaryKey)
                    {
                        colNode.ImageIndex = pkeyImageIndex;
                        colNode.SelectedImageIndex = pkeyImageIndex;
                        strpfKey = "PK, ";
                    }
                    else if (col.IsForeignKey)
                    {
                        colNode.ImageIndex = fkeyImageIndex;
                        colNode.SelectedImageIndex = fkeyImageIndex;
                        strpfKey = "FK, ";
                    }
                    else
                    {
                        colNode.ImageIndex = columnImageIndex;
                        colNode.SelectedImageIndex = columnImageIndex;
                    }

                    string strSize = "";
                    if (col.DataType.ToString().Contains("char"))
                        strSize = string.Format("({0})", col.DataType.MaximumLength.ToString());
                    string strNullable;
                    if (col.Nullable)
                        strNullable = "null";
                    else
                        strNullable = "not null";
                    colNode.Text = string.Format("{0} ({1}{2}{3}, {4})", colNode.Text, strpfKey, col.DataType.ToString(), strSize, strNullable);

                    PMSDBFieldProp DBFieldProp = new PMSDBFieldProp();
                    DBFieldProp.IFildID = col.ID;
                    DBFieldProp.StrFieldName = col.Name;
                    DBFieldProp.StrFieldType = col.DataType.ToString();
                    DBFieldProp.IFieldLength = col.DataType.MaximumLength;
                    DBFieldProp.BFieldNullable = col.Nullable;
                    DBFieldProp.BFieldPrimaryKey = col.InPrimaryKey;
                    DBFieldProp.BFieldForeignKey = col.IsForeignKey;
                    DBFieldProp.BFieldIdentity = col.Identity;
                    DBFieldProp.BFieldIsSystem = PMSDBStructure.IsTableColumnSystem(TableName, col.Name);
                    DBFieldProp.StrFieldDefault = (col.DefaultConstraint == null) ? null : col.DefaultConstraint.Text;
                    DBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(TableName, col.Name);
                    DBFieldProp.StrTableName = TableName;
                    DBFieldProp.ExProps = PMSDBStructure.GetTableFieldProp(TableName, col.Name);
                    colNode.Tag = DBFieldProp;
                    // add tableNode to tablesNode
                    tableNode.Nodes.Add(colNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetRefTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all columns
                List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();

                // get database name from parent node (database node is parent node of tablesNode, the tablesNode is parent of tableNode)
                string database = DatabaseName;//tableNode.Parent.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set Name,DataType for Column type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Column), new string[] { "Name", "DataType" });

                string TableName = "";
                if (tableNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    TableName = tableNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    TableName = tableNode.Text;

                // iterate through each column that exist in table object
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[database].Tables[TableName].Columns)
                {
                    columns.Add(col);
                }

                // iterate through list object and add each item (column name) to treeView
                foreach (Microsoft.SqlServer.Management.Smo.Column col in columns)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.Name);
                    string strpfKey = "";
                    if (col.InPrimaryKey && col.IsForeignKey)
                    {
                        colNode.ImageIndex = pfkeyImageIndex;
                        colNode.SelectedImageIndex = pfkeyImageIndex;
                        strpfKey = "PK, FK, ";
                    }
                    else if (col.InPrimaryKey)
                    {
                        colNode.ImageIndex = pkeyImageIndex;
                        colNode.SelectedImageIndex = pkeyImageIndex;
                        strpfKey = "PK, ";
                    }
                    else if (col.IsForeignKey)
                    {
                        colNode.ImageIndex = fkeyImageIndex;
                        colNode.SelectedImageIndex = fkeyImageIndex;
                        strpfKey = "FK, ";
                    }
                    else
                    {
                        colNode.ImageIndex = columnImageIndex;
                        colNode.SelectedImageIndex = columnImageIndex;
                    }

                    string strSize = "";
                    if (col.DataType.ToString().Contains("char"))
                        strSize = string.Format("({0})", col.DataType.MaximumLength.ToString());
                    string strNullable;
                    if (col.Nullable)
                        strNullable = "null";
                    else
                        strNullable = "not null";
                    colNode.Text = string.Format("{0} ({1}{2}{3}, {4})",colNode.Text, strpfKey, col.DataType.ToString(), strSize, strNullable);

                    PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                    RefDBFieldProp.PMSRefDBConnection = (PMSRefDBConnection)(tableNode.Parent.Tag);
                    RefDBFieldProp.StrTableName = TableName;
                    RefDBFieldProp.StrFieldName = col.Name;
                    RefDBFieldProp.StrFieldType = col.DataType.ToString();
                    RefDBFieldProp.DbType = RefDBInfo.GetDbType(col.DataType.ToString());
                    RefDBFieldProp.Type = RefDBInfo.GetCSharpType(col.DataType.ToString());
                    RefDBFieldProp.IFildID = col.ID;
                    RefDBFieldProp.IFieldLength = col.DataType.MaximumLength;
                    RefDBFieldProp.StrFieldDefault = (col.DefaultConstraint == null) ? null : col.DefaultConstraint.Text;
                    if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                        RefDBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(TableName, col.Name);
                    RefDBFieldProp.BFieldIdentity = col.Identity;
                    RefDBFieldProp.BFieldNullable = col.Nullable;
                    RefDBFieldProp.BFieldPrimaryKey = col.InPrimaryKey;
                    colNode.Tag = RefDBFieldProp;
                    // add tableNode to tablesNode
                    tableNode.Nodes.Add(colNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Dictionary<string, PMSRefDBFieldProp> GetRefTableColumns(string tableName, PMSRefDBConnection rc)
        {
            try
            {
                // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all columns
                List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();

                // get database name from parent node (database node is parent node of tablesNode, the tablesNode is parent of tableNode)
                string database = DatabaseName;//tableNode.Parent.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set Name,DataType for Column type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Column), new string[] { "Name", "DataType" }); 

                // iterate through each column that exist in table object
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[database].Tables[tableName].Columns)
                {
                    columns.Add(col);
                }

                Dictionary<string, PMSRefDBFieldProp> dic = new Dictionary<string, PMSRefDBFieldProp>();

                // iterate through list object and add each item (column name) to treeView
                foreach (Microsoft.SqlServer.Management.Smo.Column col in columns)
                {
                    PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                    RefDBFieldProp.PMSRefDBConnection = rc;
                    RefDBFieldProp.StrTableName = tableName;
                    RefDBFieldProp.StrFieldName = col.Name;
                    RefDBFieldProp.StrFieldType = col.DataType.ToString();
                    RefDBFieldProp.DbType = RefDBInfo.GetDbType(col.DataType.ToString());
                    RefDBFieldProp.Type = RefDBInfo.GetCSharpType(col.DataType.ToString());
                    RefDBFieldProp.IFildID = col.ID;
                    RefDBFieldProp.IFieldLength = col.DataType.MaximumLength;
                    RefDBFieldProp.StrFieldDefault = (col.DefaultConstraint == null) ? null : col.DefaultConstraint.Text;
                    if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                        RefDBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(tableName, col.Name);
                    RefDBFieldProp.BFieldIdentity = col.Identity;
                    RefDBFieldProp.BFieldNullable = col.Nullable;
                    RefDBFieldProp.BFieldPrimaryKey = col.InPrimaryKey;

                    dic.Add(col.Name, RefDBFieldProp);
                }

                return dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        #endregion

        #region Get all columns from current view

        public void GetRefViewColumns(TreeView tree, TreeNode viewNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            try
            {
                // clear current nodes
                viewNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all columns
                List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();

                // get database name from parent node (database node is parent node of tablesNode, the tablesNode is parent of tableNode)
                string database = DatabaseName;//tableNode.Parent.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set Name,DataType for Column type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Column), new string[] { "Name", "DataType" });

                string ViewName = "";
                if (viewNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    ViewName = viewNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    ViewName = viewNode.Text;

                // iterate through each column that exist in table object
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[database].Views[ViewName].Columns)
                {
                    columns.Add(col);
                }

                // iterate through list object and add each item (column name) to treeView
                foreach (Microsoft.SqlServer.Management.Smo.Column col in columns)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.Name);
                    string strpfKey = "";
                    if (col.InPrimaryKey && col.IsForeignKey)
                    {
                        colNode.ImageIndex = pfkeyImageIndex;
                        colNode.SelectedImageIndex = pfkeyImageIndex;
                        strpfKey = "PK, FK, ";
                    }
                    else if (col.InPrimaryKey)
                    {
                        colNode.ImageIndex = pkeyImageIndex;
                        colNode.SelectedImageIndex = pkeyImageIndex;
                        strpfKey = "PK, ";
                    }
                    else if (col.IsForeignKey)
                    {
                        colNode.ImageIndex = fkeyImageIndex;
                        colNode.SelectedImageIndex = fkeyImageIndex;
                        strpfKey = "FK, ";
                    }
                    else
                    {
                        colNode.ImageIndex = columnImageIndex;
                        colNode.SelectedImageIndex = columnImageIndex;
                    }

                    string strSize = "";
                    if (col.DataType.ToString().Contains("char"))
                        strSize = string.Format("({0})", col.DataType.MaximumLength.ToString());
                    string strNullable;
                    if (col.Nullable)
                        strNullable = "null";
                    else
                        strNullable = "not null";
                    colNode.Text = string.Format("{0} ({1}{2}{3}, {4})", colNode.Text, strpfKey, col.DataType.ToString(), strSize, strNullable);

                    PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                    RefDBFieldProp.PMSRefDBConnection = (PMSRefDBConnection)(viewNode.Parent.Tag);
                    RefDBFieldProp.StrTableName = ViewName;
                    RefDBFieldProp.StrFieldName = col.Name;
                    RefDBFieldProp.StrFieldType = col.DataType.ToString();
                    RefDBFieldProp.DbType = RefDBInfo.GetDbType(col.DataType.ToString());
                    RefDBFieldProp.Type = RefDBInfo.GetCSharpType(col.DataType.ToString());
                    RefDBFieldProp.IFildID = col.ID;
                    RefDBFieldProp.IFieldLength = col.DataType.MaximumLength;
                    RefDBFieldProp.StrFieldDefault = (col.DefaultConstraint == null) ? null : col.DefaultConstraint.Text;
                    if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                        RefDBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(ViewName, col.Name);
                    RefDBFieldProp.BFieldIdentity = col.Identity;
                    RefDBFieldProp.BFieldNullable = col.Nullable;
                    RefDBFieldProp.BFieldPrimaryKey = col.InPrimaryKey;
                    colNode.Tag = RefDBFieldProp;
                    // add tableNode to tablesNode
                    viewNode.Nodes.Add(colNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Dictionary<string, PMSRefDBFieldProp> GetRefViewColumns(string tableName, PMSRefDBConnection rc)
        {
            try
            {
                // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all columns
                List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();

                // get database name from parent node (database node is parent node of tablesNode, the tablesNode is parent of tableNode)
                string database = DatabaseName;//tableNode.Parent.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set Name,DataType for Column type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.Column), new string[] { "Name", "DataType" });

                // iterate through each column that exist in table object
                foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[database].Views[tableName].Columns)
                {
                    columns.Add(col);
                }

                Dictionary<string, PMSRefDBFieldProp> dic = new Dictionary<string, PMSRefDBFieldProp>();

                // iterate through list object and add each item (column name) to treeView
                foreach (Microsoft.SqlServer.Management.Smo.Column col in columns)
                {
                    PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                    RefDBFieldProp.PMSRefDBConnection = rc;
                    RefDBFieldProp.StrTableName = tableName;
                    RefDBFieldProp.StrFieldName = col.Name;
                    RefDBFieldProp.StrFieldType = col.DataType.ToString();
                    RefDBFieldProp.DbType = RefDBInfo.GetDbType(col.DataType.ToString());
                    RefDBFieldProp.Type = RefDBInfo.GetCSharpType(col.DataType.ToString());
                    RefDBFieldProp.IFildID = col.ID;
                    RefDBFieldProp.IFieldLength = col.DataType.MaximumLength;
                    RefDBFieldProp.StrFieldDefault = (col.DefaultConstraint == null) ? null : col.DefaultConstraint.Text;
                    if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                        RefDBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(tableName, col.Name);
                    RefDBFieldProp.BFieldIdentity = col.Identity;
                    RefDBFieldProp.BFieldNullable = col.Nullable;
                    RefDBFieldProp.BFieldPrimaryKey = col.InPrimaryKey;

                    dic.Add(col.Name, RefDBFieldProp);
                }

                return dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        #endregion

        #region Get all stored procedure parameters from current stored procedures

        public void GetProcedureParameters(TreeView tree, TreeNode spNode)
        {
            try
            {
                // clear current nodes
                spNode.Nodes.Clear();

                // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all stored procedure parameters
                List<Microsoft.SqlServer.Management.Smo.StoredProcedureParameter> parameters = new List<Microsoft.SqlServer.Management.Smo.StoredProcedureParameter>();

                // get database name from parent node (database node is parent node of spsNode, the spsNode is parent of spNode)
                string database = spNode.Parent.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set Name,DataType for StoredProcedureParameter type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedureParameter), new string[] { "Name", "DataType" });

                // iterate through each storedProcedureParameter that exist in storedProcedure object
                foreach (Microsoft.SqlServer.Management.Smo.StoredProcedureParameter parameter in _server.Databases[database].StoredProcedures[spNode.Text].Parameters)
                {
                    parameters.Add(parameter);
                }

                // iterate through list object and add each item (storedProcedureParameter name) to treeView
                foreach (Microsoft.SqlServer.Management.Smo.StoredProcedureParameter parameter in parameters)
                {
                    // create a node to hold parameter name (level 2)
                    TreeNode parameterNode = new TreeNode(parameter.Name);
                    parameterNode.Text += "," + parameter.DataType.ToString() + "(" + parameter.DataType.MaximumLength.ToString() + ")";

                    // add tableNode to tablesNode
                    spNode.Nodes.Add(parameterNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetProcedureParameters(TreeView tree, TreeNode spNode, ImageList imageList, int spInParameterImageIndex, int spInOutParameterImageIndex, int spRetParameterImageIndex)
        {
            try
            {
                // clear current nodes
                spNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all stored procedure parameters
                List<Microsoft.SqlServer.Management.Smo.StoredProcedureParameter> parameters = new List<Microsoft.SqlServer.Management.Smo.StoredProcedureParameter>();

                // get database name from parent node (database node is parent node of spsNode, the spsNode is parent of spNode)
                string database = DatabaseName;// spNode.Parent.Parent.Text;

                // create a _server object to interact with sql _server inctance
                Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

                // set Name,DataType for StoredProcedureParameter type when _server loading data from _server to avoid overhead and prevent to hang
                _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedureParameter), new string[] { "Name", "DataType" });

                string spName = "";
                if (spNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    spName = spNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    spName = spNode.Text;

                // iterate through each storedProcedureParameter that exist in storedProcedure object
                foreach (Microsoft.SqlServer.Management.Smo.StoredProcedureParameter parameter in _server.Databases[database].StoredProcedures[spName].Parameters)
                {
                    parameters.Add(parameter);
                }

                // iterate through list object and add each item (storedProcedureParameter name) to treeView
                foreach (Microsoft.SqlServer.Management.Smo.StoredProcedureParameter parameter in parameters)
                {
                    // create a node to hold parameter name (level 2)
                    TreeNode parameterNode = new TreeNode(parameter.Name);
                    if (parameter.IsOutputParameter)
                    {
                        parameterNode.ImageIndex = spInOutParameterImageIndex;
                        parameterNode.SelectedImageIndex = spInOutParameterImageIndex;
                    }
                    else
                    {
                        parameterNode.ImageIndex = spInParameterImageIndex;
                        parameterNode.SelectedImageIndex = spInParameterImageIndex;
                    }

                    string strSize = "";
                    if (parameter.DataType.ToString().Contains("char"))
                        strSize = string.Format("({0})", parameter.DataType.MaximumLength.ToString());

                    StoreProcedureItemEnum spie = StoreProcedureItemEnum.Input;
                    string inout = "in";
                    if (parameter.IsOutputParameter)
                    {
                        inout = "in/out";
                        spie = StoreProcedureItemEnum.InOutput;
                    }
                    
                    string defaultValue = "NoDefaultValue";
                    if(!string.IsNullOrEmpty(parameter.DefaultValue))
                        defaultValue = parameter.DefaultValue;
                    parameterNode.Text = string.Format("{0} ({1}{2}, {3}, {4})", parameter.Name, parameter.DataType.ToString(), strSize, inout, defaultValue);

                    PMSRefDBSPItemProp RefDBSPItemProp = new PMSRefDBSPItemProp();
                    RefDBSPItemProp.PMSRefDBConnection = (PMSRefDBConnection)(spNode.Parent.Tag);
                    RefDBSPItemProp.StrTableName = spName;
                    RefDBSPItemProp.StrFieldName = parameter.Name;
                    RefDBSPItemProp.StrFieldType = parameter.DataType.ToString();
                    RefDBSPItemProp.DbType = RefDBInfo.GetDbType(parameter.DataType.ToString());
                    RefDBSPItemProp.Type = RefDBInfo.GetCSharpType(parameter.DataType.ToString());
                    RefDBSPItemProp.IFildID = parameter.ID;
                    RefDBSPItemProp.IFieldLength = parameter.DataType.MaximumLength;
                    RefDBSPItemProp.StrFieldDefault = (parameter.DefaultValue == null) ? null : parameter.DefaultValue.ToString();
                    //if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                    //    RefDBSPItemProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(spName, parameter.Name);
                    //RefDBSPItemProp.BFieldIdentity = parameter.Identity;
                    //RefDBSPItemProp.BFieldNullable = parameter.Nullable;
                    //RefDBSPItemProp.BFieldPrimaryKey = parameter.InPrimaryKey;
                    RefDBSPItemProp.SPIType = spie;
                    parameterNode.Tag = RefDBSPItemProp;

                    // add tableNode to spNode
                    spNode.Nodes.Add(parameterNode);
                }

                // create a node to hold return value parameter name
                TreeNode retnParameterNode = new TreeNode("ReturnValue");
                retnParameterNode.ImageIndex = spRetParameterImageIndex;
                retnParameterNode.SelectedImageIndex = spRetParameterImageIndex;
                retnParameterNode.Text = "Return integer";

                PMSRefDBSPItemProp RefDBSPReturnProp = new PMSRefDBSPItemProp();
                RefDBSPReturnProp.PMSRefDBConnection = (PMSRefDBConnection)(spNode.Parent.Tag);
                RefDBSPReturnProp.StrTableName = spName;
                RefDBSPReturnProp.StrFieldName = "ReturnValue";
                RefDBSPReturnProp.StrFieldType = DataType.Int.ToString();
                RefDBSPReturnProp.DbType = RefDBInfo.GetDbType(DataType.Int.ToString());
                RefDBSPReturnProp.Type = RefDBInfo.GetCSharpType(DataType.Int.ToString());
                //RefDBSPReturnProp.IFildID = parameter.ID;
                RefDBSPReturnProp.IFieldLength = DataType.Int.MaximumLength;
                //RefDBSPReturnProp.StrFieldDefault = (parameter.DefaultValue == null) ? null : parameter.DefaultValue.ToString();
                //if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                //    RefDBSPItemProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(spName, parameter.Name);
                //RefDBSPItemProp.BFieldIdentity = parameter.Identity;
                //RefDBSPItemProp.BFieldNullable = parameter.Nullable;
                //RefDBSPItemProp.BFieldPrimaryKey = parameter.InPrimaryKey;
                RefDBSPReturnProp.SPIType = StoreProcedureItemEnum.Return;
                retnParameterNode.Tag = RefDBSPReturnProp;

                spNode.Nodes.Add(retnParameterNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Alter sth of db

        public bool RenameTableName(string oldname ,string newname)
        {
            try
            {
                _server.Databases[_databaseName].Tables.Refresh();
                _server.Databases[_databaseName].Tables[oldname].Rename(newname);
                //_server.Databases[_databaseName].Tables[oldname].Alter();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            } 
        }

        public bool RenameTableColumnName(string tablename,string oldname,string newname)
        {
            try
            {
                _server.Databases[_databaseName].Tables.Refresh();
                _server.Databases[_databaseName].Tables[tablename].Columns.Refresh();
                _server.Databases[_databaseName].Tables[tablename].Columns[oldname].Rename(newname);
                //_server.Databases[_databaseName].Tables[oldname].Columns[oldname].Alter();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteTable(string tablename)
        {
            try
            {
                _server.Databases[_databaseName].Tables.Refresh();
                _server.Databases[_databaseName].Tables[tablename].Drop();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteTableColumn(string tablename, string columnname)
        {
            try
            {
                _server.Databases[_databaseName].Tables.Refresh();
                _server.Databases[_databaseName].Tables[tablename].Columns.Refresh();
                if (_server.Databases[_databaseName].Tables[tablename].Columns[columnname].DefaultConstraint != null)
                    _server.Databases[_databaseName].Tables[tablename].Columns[columnname].DefaultConstraint.Drop();
                _server.Databases[_databaseName].Tables[tablename].Columns[columnname].Drop();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException.InnerException.Message);
                return false;
            }
        }

        #endregion
    }

    public class OracleAssistance
    {
        #region private member

        private OracleConnection _conn = null;
        private string _serverInstance = string.Empty;
        private string _databaseName = string.Empty;
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private string _portID = string.Empty;
        private string _connectAs = string.Empty;
        private string _connectMode = string.Empty;
        private string _Owner = string.Empty;
        private bool _bDirect = false;
        private OleDbConnection _OleDBConn = null;
        private string _ConnectionString = string.Empty;

        #endregion

        #region property

        public string OracleServerInstance
        {
            get { return _serverInstance; }
            set { _serverInstance = value; }
        }
        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public OleDbConnection OleDBConn
        {
            get { return _OleDBConn; }
            set { _OleDBConn = value; }
        }

        public OracleConnection OracleConnection
        {
            get { return _conn; }
            set { _conn = value; }
        }

        public string PortID
        {
            get { return _portID; }
            set { _portID = value; }
        }

        public bool Direct
        {
            get { return _bDirect; }
            set { _bDirect = value; }
        }

        public string ConnectAs
        {
            get { return _connectAs; }
            set { _connectAs = value; }
        }

        public string ConnectMode
        {
            get { return _connectMode; }
            set { _connectMode = value; }
        }

        public string Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        #endregion

        public OracleAssistance()
        {
            
        }

        /// <summary>
        /// 枚举所有Oracle服务器(take Oracle 10g 10.2 for e.g D:\oracle\product\10.2.0\db_1\network\admin\tnsnames.ora) )
        /// </summary>
        /// <returns>服务器列表</returns>
        public DataTable EnumOracleServers()
        {
            try
            {
                string ProviderName = "Oracle.DataAccess.Client";

                DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);

                if (factory.CanCreateDataSourceEnumerator)
                {
                    DbDataSourceEnumerator dsenum = factory.CreateDataSourceEnumerator();
                    DataTable dt = dsenum.GetDataSources();
                    return dt;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 枚举所有Oracle服务器(take Oracle 10g 10.2 for e.g D:\oracle\product\10.2.0\db_1\network\admin\tnsnames.ora) )
        /// </summary>
        /// <returns>服务器列表</returns>
        public List<string> EnumOracleServersList()
        {
            try
            {
                string ProviderName = "Oracle.DataAccess.Client";

                DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);

                if (factory.CanCreateDataSourceEnumerator)
                {
                    DbDataSourceEnumerator dsenum = factory.CreateDataSourceEnumerator();
                    DataTable dt = dsenum.GetDataSources();
                    List<string> strList = new List<string>();
                    foreach (DataRow dtvar in dt.Rows)
                    {
                        strList.Add(dtvar.ItemArray[2].ToString());
                    }
                    return strList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        
        public bool ConnectDatabase()
        {
            if (_bDirect == false)
                return this.ConnectDatabase(_serverInstance, _userName, _password, _connectAs);
            else
                return this.ConnectDirectDatabase(_serverInstance, _databaseName, _userName, _password, _portID, _connectMode);
        }

        public OracleConnection GetNewConnection()
        {
            try
            {
                string connectString = string.Empty;
                if (_bDirect == false)
                    connectString = this.ConnectDatabaseString(_serverInstance, _userName, _password, _connectAs);
                else
                    connectString = this.ConnectDirectDatabaseString(_serverInstance, _databaseName, _userName, _password, _portID);
                OracleConnection conn = new OracleConnection(connectString);
                conn.Open();
                return conn;
            }
            catch (System.Exception ex)
            {
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.ToString());
            }
            return null;
        }

        public bool ConnectDatabase(string strServer, string strUserID, string strPassword,string strConnectAs)
        {//Pooling = 'false';
            _ConnectionString = "User Id=" + strUserID +
                    ";Password=" + strPassword +
                    ";Data Source=" + strServer +
                    ";Connect Timeout=30;";
            if (strConnectAs != "Normal" && strConnectAs != "")
                _ConnectionString += "DBA Privilege=" + strConnectAs +
                ";";
            return this.ConnectDatabase(_ConnectionString);
        }

        public string ConnectDatabaseString(string strServer, string strUserID, string strPassword, string strConnectAs)
        {//Pooling = 'false';
            string ConnectionString = "User Id=" + strUserID +
                    ";Password=" + strPassword +
                    ";Data Source=" + strServer +
                    ";Connect Timeout=30;";
            if (strConnectAs != "Normal" && strConnectAs != "")
                ConnectionString += "DBA Privilege=" + strConnectAs +
                ";";
            return ConnectionString;
        }

        public string ConnectDatabaseString(string strServer, string strUserID, string strConnectAs)
        {//Pooling = 'false';
            string ConnectionString = "User Id=" + strUserID +
                    ";Data Source=" + strServer +
                    ";Connect Timeout=30;";
            if (strConnectAs != "Normal" && strConnectAs != "")
                ConnectionString += "DBA Privilege=" + strConnectAs +
                ";";
            return ConnectionString;
        }

        public bool ConnectDirectDatabase()
        {
            return this.ConnectDirectDatabase(_serverInstance, _databaseName, _userName, _password, _portID, _connectMode);
        }

        public bool ConnectDirectDatabase(string strServer, string strSID, string strUserID, string strPassword, string strPortID, string strConnectMode)
        {//Pooling = 'false';
            _ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + strServer +
                    ")(PORT=" + strPortID +
                    ")))(CONNECT_DATA=(SERVER=" + strConnectMode +
                    ")(SERVICE_NAME=" + strSID +
                    ")));User Id=" + strUserID +
                    ";Password=" + strPassword +
                    ";";
            return this.ConnectDatabase(_ConnectionString);
        }

        public string ConnectDirectDatabaseString(string strServer, string strSID, string strUserID, string strPassword, string strPortID, string strConnectMode)
        {//Pooling = 'false';
            string ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + strServer +
                    ")(PORT=" + strPortID +
                    ")))(CONNECT_DATA=(SERVER=" + strConnectMode +
                    ")(SERVICE_NAME=" + strSID +
                    ")));User Id=" + strUserID +
                    ";Password=" + strPassword +
                    ";";
            return ConnectionString;
        }

        public string ConnectDirectDatabaseString(string strServer, string strSID, string strUserID, string strPortID, string strConnectMode)
        {//Pooling = 'false';
            string ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + strServer +
                    ")(PORT=" + strPortID +
                    ")))(CONNECT_DATA=(SERVER=" + strConnectMode +
                    ")(SERVICE_NAME=" + strSID +
                    ")));User Id=" + strUserID +
                    ";";
            return ConnectionString;
        }

        public bool ConnectDatabase(string sqlConnectionString)
        {
            try
            {
                _ConnectionString = sqlConnectionString;
                _conn = new OracleConnection(sqlConnectionString);
                _conn.Open();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public bool ConnectOleDB()
        {
            if (_bDirect == false)
                return this.ConnectOleDB(_serverInstance, _userName, _password);
            else
                return this.ConnectDirectOleDB(_serverInstance, _databaseName, _userName, _password, _portID, _connectMode);
        }

        public bool ConnectOleDB(string strServer, string strUserID, string strPassword)
        {//Pooling = 'false';
            _ConnectionString = "Provider=OraOLEDB.Oracle;" +
                                        ";Data Source=" + strServer +
                                        ";User Id=" + strUserID +
                                        ";Password=" + strPassword +
                                        ";Connect Timeout=30;";
            return this.ConnectOleDB(_ConnectionString);
        }

        public bool ConnectDirectOleDB()
        {
            return this.ConnectDirectOleDB(_serverInstance, _databaseName, _userName, _password, _portID, _connectMode);
        }

        public bool ConnectDirectOleDB(string strServer, string strSID, string strUserID, string strPassword, string strPortID, string strConnectMode)
        {//Pooling = 'false';
            //Provider = OraOLEDB.Oracle; Data Source = (DESCRIPTION = (CID = GTU_APP)(ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = myHost)(PORT = myPort)))(CONNECT_DATA = (SID = MyOracleSID)(SERVER = DEDICATED))); User Id = myUsername; Password = myPassword;
            _ConnectionString = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + strServer +
                    ")(PORT=" + strPortID +
                    ")))(CONNECT_DATA=(SERVER=" + strConnectMode +
                    ")(SERVICE_NAME=" + strSID +
                    ")));User Id=" + strUserID +
                    ";Password=" + strPassword +
                    ";";
            return this.ConnectOleDB(_ConnectionString);
        }

        public bool ConnectOleDB(string sqlConnectionString)
        {
            try
            {
                _OleDBConn = new OleDbConnection(sqlConnectionString);
                _OleDBConn.Open();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public bool DisConnectDatabase()
        {
            try
            {
                if (_conn != null)
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }

                if (_OleDBConn != null)
                {
                    if (_OleDBConn.State == ConnectionState.Open)
                    {
                        _OleDBConn.Close();
                    }
                }

            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public bool ExecuteSqlScript(string strFilePath)
        {
            try
            {
                if (File.Exists(strFilePath) == false)
                {
                    //string strText = strFilePath+_getStringFromPR.GetStringFromPublicResource("PMS_NotExisted");
                    //string strCaption = _getStringFromPR.GetStringFromPublicResource("PMS_Warnning");
                    //MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                FileInfo file = new FileInfo(strFilePath);

                string script = file.OpenText().ReadToEnd();

                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = "begin " + script + " end;";
                    oCommand.Connection = _conn;
                    oCommand.ExecuteNonQuery();

                    // Clean up
                    oCommand.Dispose();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //0 FOREIGNCOLUMN
        //1 FOREIGNTABLE
        //2 PRIMARYTABLE
        //3 PRIMARYCOLUMN
        public PMSDBTablePFKeyRelationCollection GetTablePFKeyRelation(string tablename)
        {
            try
            {
                string strSql =
                @"SELECT DISTINCT(COL.COLUMN_NAME) AS FOREIGNCOLUMN
                    ,COL.TABLE_NAME AS FOREIGNTABLE
                    ,R.TABLE_NAME AS PRIMARYTABLE
                    ,R.COLUMN_NAME AS PRIMARYCOLUMN
                FROM ALL_CONSTRAINTS CON,
                    ALL_CONS_COLUMNS COL, 
                    (SELECT T2.TABLE_NAME,T2.COLUMN_NAME,T1.R_CONSTRAINT_NAME 
                     FROM ALL_CONSTRAINTS T1,ALL_CONS_COLUMNS T2 
                     WHERE T1.R_CONSTRAINT_NAME=T2.CONSTRAINT_NAME 
                     ) R 
                WHERE CON.CONSTRAINT_NAME=COL.CONSTRAINT_NAME 
                    AND CON.R_CONSTRAINT_NAME=R.R_CONSTRAINT_NAME 
                    AND CON.OWNER = 'DBO_MESCENTER'
                    AND R.TABLE_NAME = 'S_USERINFO'
                        WHERE C.OWNER = '" + _Owner +
                        "'AND T.TABLE_NAME = '" + tablename + 
                        @"' AND C.TABLE_TYPE = 'TABLE'
                        ORDER BY T.TABLE_NAME,T.COLUMN_ID";

                if (_conn != null)
                {
                    PMSDBTablePFKeyRelationCollection PFKeyRelationCollection = new PMSDBTablePFKeyRelationCollection();
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = _conn;
                    OracleDataReader odr = oCommand.ExecuteReader();
                    while (odr.Read())
                    {
                        PMSDBTablePFKeyRelation PFKeyRelation = new PMSDBTablePFKeyRelation();
                        PFKeyRelation.PrimaryTable = odr.GetString(2);
                        PFKeyRelation.PrimaryColumn = odr.GetString(3);
                        PFKeyRelation.ForeignTable = odr.GetString(1);
                        PFKeyRelation.ForengnColumn = odr.GetString(0);
                        PFKeyRelationCollection.Add(PFKeyRelation);
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();

                    return PFKeyRelationCollection;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public PMSDBTablePFKeyRelationCollection GetTablePFKeyRelation()
        {
            try
            {
                string strSql =
                @"SELECT DISTINCT(COL.COLUMN_NAME) AS FOREIGNCOLUMN
                    ,COL.TABLE_NAME AS FOREIGNTABLE
                    ,R.TABLE_NAME AS PRIMARYTABLE
                    ,R.COLUMN_NAME AS PRIMARYCOLUMN
                FROM ALL_CONSTRAINTS CON,
                    ALL_CONS_COLUMNS COL, 
                    (SELECT T2.TABLE_NAME,T2.COLUMN_NAME,T1.R_CONSTRAINT_NAME 
                     FROM ALL_CONSTRAINTS T1,ALL_CONS_COLUMNS T2 
                     WHERE T1.R_CONSTRAINT_NAME=T2.CONSTRAINT_NAME 
                     ) R 
                WHERE CON.CONSTRAINT_NAME=COL.CONSTRAINT_NAME 
                    AND CON.R_CONSTRAINT_NAME=R.R_CONSTRAINT_NAME 
                    AND CON.OWNER = 'DBO_MESCENTER'
                    AND R.TABLE_NAME = 'S_USERINFO'
                        WHERE C.OWNER = '" + _Owner +
                        "' AND C.TABLE_TYPE = 'TABLE' ORDER BY T.TABLE_NAME,T.COLUMN_ID";

                if (_conn != null)
                {
                    PMSDBTablePFKeyRelationCollection PFKeyRelationCollection = new PMSDBTablePFKeyRelationCollection();
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = _conn;
                    OracleDataReader odr = oCommand.ExecuteReader();
                    while (odr.Read())
                    {
                        PMSDBTablePFKeyRelation PFKeyRelation = new PMSDBTablePFKeyRelation();
                        PFKeyRelation.PrimaryTable = odr.GetString(2);
                        PFKeyRelation.PrimaryColumn = odr.GetString(3);
                        PFKeyRelation.ForeignTable = odr.GetString(1);
                        PFKeyRelation.ForengnColumn = odr.GetString(0);
                        PFKeyRelationCollection.Add(PFKeyRelation);
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();

                    return PFKeyRelationCollection;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //#region Get all databases from current server

        //public void GetServerDatabases(TreeView tree)
        //{
        //    try
        //    {
        //        // clear current nodes (to avoid cross-thread problem, use anonymous delegates)
        //        tree.Invoke(new MethodInvoker(delegate
        //        {
        //            tree.Nodes.Clear();
        //        }));

        //        // create a list of strings to hold name of databases
        //        List<string> databases = new List<string>();

        //        // create a _server object to interact with sql _server inctance
        //        Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

        //        // iterate through each database that exist in _server object
        //        foreach (Microsoft.SqlServer.Management.Smo.Database db in _server.Databases)
        //            databases.Add(db.Name);

        //        // iterate through list object and add each item (database name) to treeView
        //        foreach (string str in databases)
        //        {
        //            // create a node to hold database name (main node or level 0)
        //            TreeNode dbNode = new TreeNode(str);

        //            // create a node named 'Stored Procedures' to hold storedProcedures for each database (level 1)
        //            // then add child named 'Objects' to add + mark for 'spsNode' node
        //            TreeNode spsNode = new TreeNode("Stored Procedures");
        //            spsNode.Nodes.Add("Objects");

        //            // create a node named 'Tables' to hold tables for each database (level 1)
        //            // then add child named 'Objects' to add + mark for 'Tables' node
        //            TreeNode tablesNode = new TreeNode("Tables");
        //            tablesNode.Nodes.Add("Objects");

        //            // add Tabales and StoredProcedures node as child to database node
        //            dbNode.Nodes.Add(spsNode);
        //            dbNode.Nodes.Add(tablesNode);

        //            // add db node to treeView (to avoid cross-thread problem, use anonymous delegates)
        //            tree.Invoke(new MethodInvoker(delegate
        //            {
        //                tree.Nodes.Add(dbNode);
        //            }
        //            ));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public void GetServerDatabases(TreeView tree, ImageList imageList, int dbImageIndex, int tableImageIndex, int spImageIndex)
        //{
        //    try
        //    {
        //        // clear current nodes (to avoid cross-thread problem, use anonymous delegates)
        //        tree.Invoke(new MethodInvoker(delegate
        //        {
        //            tree.Nodes.Clear();
        //        }));

        //        // create a list of strings to hold name of databases
        //        List<string> databases = new List<string>();

        //        // create a _server object to interact with sql _server inctance
        //        Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

        //        // iterate through each database that exist in _server object
        //        foreach (Microsoft.SqlServer.Management.Smo.Database db in _server.Databases)
        //            databases.Add(db.Name);

        //        // iterate through list object and add each item (database name) to treeView
        //        foreach (string str in databases)
        //        {
        //            // create a node to hold database name (main node or level 0)
        //            TreeNode dbNode = new TreeNode(str);
        //            dbNode.ImageIndex = dbImageIndex;

        //            // create a node named 'Stored Procedures' to hold storedProcedures for each database (level 1)
        //            // then add child named 'Objects' to add + mark for 'spsNode' node
        //            TreeNode spsNode = new TreeNode("Stored Procedures");
        //            spsNode.ImageIndex = spImageIndex;
        //            spsNode.SelectedImageIndex = spImageIndex;
        //            spsNode.Nodes.Add("Objects");

        //            // create a node named 'Tables' to hold tables for each database (level 1)
        //            // then add child named 'Objects' to add + mark for 'Tables' node
        //            TreeNode tablesNode = new TreeNode("Tables");
        //            tablesNode.ImageIndex = tableImageIndex;
        //            tablesNode.SelectedImageIndex = tableImageIndex;
        //            tablesNode.Nodes.Add("Objects");

        //            // add Tabales and StoredProcedures node as child to database node                    
        //            dbNode.Nodes.Add(spsNode);
        //            dbNode.Nodes.Add(tablesNode);

        //            // add db node to treeView (to avoid cross-thread problem, use anonymous delegates)
        //            tree.Invoke(new MethodInvoker(delegate
        //            {
        //                // set tree.ImageList to our imageList parameter to use it's imageIndex
        //                tree.ImageList = imageList;

        //                tree.Nodes.Add(dbNode);
        //            }
        //            ));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //#endregion

        #region Get all tables from current databse

        //public string GetTableDescription(string tableName)
        //{
        //    try
        //    {
        //        foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[_databaseName].Tables)
        //        {
        //            if (!table.IsSystemObject)
        //            {
        //                if (table.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase))
        //                {
        //                    if (table.ExtendedProperties["Description"] != null)
        //                        return table.ExtendedProperties["Description"].Value.ToString();
        //                    else
        //                        return null;
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //    catch (SmoException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}

        //public string GetTableExtendedPropertie(string tableName, string propertyName)
        //{
        //    try
        //    {
        //        foreach (Microsoft.SqlServer.Management.Smo.Table table in _server.Databases[_databaseName].Tables)
        //        {
        //            if (!table.IsSystemObject)
        //            {
        //                if (table.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase))
        //                {
        //                    if (table.ExtendedProperties[propertyName] != null)
        //                        return table.ExtendedProperties[propertyName].Value.ToString();
        //                    else
        //                        return null;
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //    catch (SmoException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}

        public List<string> GetDatabaseOwnerTables()
        {
            return this.GetDatabaseOwnerTables(_Owner);
        }

        public List<string> GetDatabaseOwnerTables(string owner)
        {
            try
            {
                _Owner = owner;
                List<string> tableNames = new List<string>();
                string strSql = "SELECT TABLE_NAME FROM ALL_TABLES T WHERE UPPER(T.OWNER) = UPPER('" + owner + "') ORDER BY t.TABLE_NAME";
                if (string.IsNullOrEmpty(owner))
                {
                    strSql = "SELECT TABLE_NAME FROM ALL_TABLES T ORDER BY t.TABLE_NAME";
                }
                
                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql; 
                    oCommand.Connection = _conn;
                    OracleDataReader odr = oCommand.ExecuteReader();
                    while(odr.Read())
                    {
                        tableNames.Add(odr.GetString(0));
                    }
    
                    odr.Close();
                    // Clean up
                    oCommand.Dispose();
                }

                return tableNames;
            }
            catch
            {
                return null;
            }
        }

        public Dictionary<string, PMSRefDBTableProp> GetDatabaseTables(PMSRefDBConnection rc)
        {
            try
            {
                Dictionary<string, PMSRefDBTableProp> tableNames = new Dictionary<string, PMSRefDBTableProp>();
                string strSql = "SELECT TABLE_NAME FROM ALL_TABLES T WHERE UPPER(T.OWNER) = UPPER('" + rc.Owner + "') ORDER BY t.TABLE_NAME";
                if (string.IsNullOrEmpty(rc.Owner))
                {
                    strSql = "SELECT TABLE_NAME FROM ALL_TABLES T ORDER BY t.TABLE_NAME";
                }
                OracleConnection oc = rc.GetOracleConnection();
                if (oc != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = oc;
                    OracleDataReader odr = oCommand.ExecuteReader();
                    while (odr.Read())
                    {
                        tableNames.Add(
                            odr.GetString(0), 
                            new PMSRefDBTableProp() 
                            { 
                                StrTableName = odr.GetString(0), 
                                PMSRefDBConnection = rc 
                            }
                        );
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();
                }

                return tableNames;
            }
            catch
            {
                return null;
            }
        }
        
        public void GetDatabaseTables(TreeView tree, TreeNode tablesNode)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // create a list of strings to hold name of tables
                List<string> tables = this.GetDatabaseOwnerTables();

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDatabaseTables(TreeView tree, TreeNode tablesNode, string strDBName, ImageList imageList, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of strings to hold name of tables
                List<string> tables = this.GetDatabaseOwnerTables();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = strDBName;

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.ImageIndex = tableImageIndex;
                    tableNode.SelectedImageIndex = tableImageIndex;
                    PMSRefDBTableProp refTableProp = new PMSRefDBTableProp();
                    refTableProp.StrTableName = str;
                    refTableProp.PMSRefDBConnection = (PMSRefDBConnection)(tablesNode.Tag);
                    tableNode.Tag = refTableProp;
                    tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDatabaseTables(TreeView tree, TreeNode tablesNode, ImageList imageList, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of strings to hold name of tables
                List<string> tables = this.GetDatabaseOwnerTables();

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.ImageIndex = tableImageIndex;
                    tableNode.SelectedImageIndex = tableImageIndex;
                    PMSDBTableProp TableProp = new PMSDBTableProp();
                    TableProp.StrTableName = str;
                    TableProp.ExProps = PMSDBStructure.GetTableProp(str);

                    TableProp.FieldPropCollection = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBFieldPropCollection();
                    tableNode.Tag = TableProp;
                    tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public PMSDBTableProp GetPMSDBTableProp(string tablename)
        {
            try
            {
                PMSDBTableProp TableProp = null;

                string database = _databaseName;

                string strSql =
                    @"SELECT T.COLUMN_ID
                          ,T.COLUMN_NAME
                          ,T.DATA_TYPE
                          ,T.DATA_LENGTH
                          ,(CASE WHEN T.NULLABLE = 'N' THEN 0 ELSE 1 END) AS NULLABLE
                          ,(CASE WHEN P.COLUMN_NAME = T.COLUMN_NAME THEN 1 ELSE 0 END) AS ISPRIMARYKEY
                          ,(CASE WHEN T.COLUMN_NAME = R.COLUMN_NAME THEN 1 ELSE 0 END) AS ISFOREIGNKEY
                          ,T.DATA_DEFAULT
                     FROM ALL_TAB_COMMENTS C
                    LEFT JOIN ALL_TAB_COLUMNS T ON C.TABLE_NAME = T.TABLE_NAME
                    LEFT JOIN (
                    SELECT T2.TABLE_NAME,T2.COLUMN_NAME
                         FROM ALL_CONSTRAINTS T1 LEFT JOIN ALL_CONS_COLUMNS T2 
                     ON T1.CONSTRAINT_NAME=T2.CONSTRAINT_NAME 
                     WHERE T1.CONSTRAINT_TYPE = 'P' AND T2.POSITION = 1
                     ) P ON T.TABLE_NAME = P.TABLE_NAME 
                    LEFT JOIN (
                    SELECT DISTINCT T2.TABLE_NAME,T2.COLUMN_NAME,T1.R_CONSTRAINT_NAME 
                     FROM ALL_CONSTRAINTS T1 LEFT JOIN ALL_CONS_COLUMNS T2 
                     ON T1.CONSTRAINT_NAME=T2.CONSTRAINT_NAME 
                     WHERE T1.CONSTRAINT_TYPE = 'R'
                     ) R ON T.TABLE_NAME = R.TABLE_NAME AND T.COLUMN_NAME = R.COLUMN_NAME
                    WHERE UPPER(C.OWNER) = UPPER('" + _Owner +
                    "') AND UPPER(T.TABLE_NAME) = UPPER('" + tablename + 
                    @"') AND C.TABLE_TYPE = 'TABLE'
                    ORDER BY T.TABLE_NAME,T.COLUMN_ID";

                if (_conn != null)
                {
                    TableProp = new PMSDBTableProp();
                    TableProp.StrTableName = tablename;
                    TableProp.ExProps = new Dictionary<string, PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp>();
                    PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp exp = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp();
                    exp.StrPropName = PMS.Libraries.ToolControls.PMSPublicInfo.TablePropertyName.TableType;
                    exp.StrPropValue = PMSDBStructure.GetTablePropertie(tablename, TablePropertyName.TableType);
                    TableProp.ExProps.Add(exp.StrPropName, exp);
                    exp = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp();
                    exp.StrPropName = PMS.Libraries.ToolControls.PMSPublicInfo.TablePropertyName.Description;
                    exp.StrPropValue = PMSDBStructure.GetTableDescription(tablename);
                    TableProp.ExProps.Add(exp.StrPropName, exp);

                    TableProp.FieldPropCollection = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBFieldPropCollection();

                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = _conn;
                    OracleDataReader odr = oCommand.ExecuteReader();
                    while (odr.Read())
                    {
                        PMSDBFieldProp DBFieldProp = new PMSDBFieldProp();
                        DBFieldProp.IFildID = odr.GetInt32(0);
                        DBFieldProp.StrFieldName = odr.GetString(1);
                        DBFieldProp.StrFieldType = odr.GetString(2);
                        DBFieldProp.IFieldLength = odr.GetInt32(3);
                        DBFieldProp.BFieldNullable = odr.GetBoolean(4);
                        DBFieldProp.BFieldPrimaryKey = odr.GetBoolean(5);
                        DBFieldProp.BFieldForeignKey = odr.GetBoolean(6);
                        DBFieldProp.BFieldIdentity = false;
                        DBFieldProp.BFieldIsSystem = PMSDBStructure.IsTableColumnSystem(tablename, odr.GetString(1));
                        DBFieldProp.StrFieldDefault = (odr.GetString(7) == null) ? null : odr.GetString(7);
                        DBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(tablename, odr.GetString(1));
                        DBFieldProp.StrTableName = tablename;
                        TableProp.FieldPropCollection.Add(DBFieldProp);
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();
                }

                return TableProp;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void GetDatabaseTables(TreeNode tablesNode, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // create a list of strings to hold name of tables
                List<string> tables = this.GetDatabaseOwnerTables(_Owner);

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.ImageIndex = tableImageIndex;
                    tableNode.SelectedImageIndex = tableImageIndex;
                    //tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool IsTableExisted(string tableName)
        {
            List<string> tableNameList = this.GetDatabaseOwnerTables(_Owner);
            return tableNameList.Contains(tableName, new DBTableNameComparer());
        }

        #endregion

        //#region Get all stored procedures from current database

        //public void GetDatabaseSps(TreeView tree, TreeNode spsNode)
        //{
        //    try
        //    {
        //        // clear current nodes
        //        spsNode.Nodes.Clear();

        //        // create a list of strings to hold name of sps
        //        List<string> sps = new List<string>();

        //        // get database name from parent node (database node is parent node of tablesNode)
        //        string database = spsNode.Parent.Text;

        //        // create a _server object to interact with sql _server inctance
        //        Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

        //        // set SystemObject for StoredProcedure type when _server loading data from _server to avoid overhead and prevent to hang
        //        _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedure), "IsSystemObject");

        //        // iterate through each sp that exist in database object
        //        foreach (Microsoft.SqlServer.Management.Smo.StoredProcedure sp in _server.Databases[database].StoredProcedures)
        //        {
        //            if (!sp.IsSystemObject)
        //            {
        //                sps.Add(sp.Name);
        //            }
        //        }

        //        // iterate through list object and add each item (table name) to treeView
        //        foreach (string str in sps)
        //        {
        //            // create a node to hold table name (child node or level 1)
        //            // then add child named 'Objects' to add + mark for sp node
        //            TreeNode spNode = new TreeNode(str);
        //            spNode.Nodes.Add("Objects");

        //            // add tableNode to tablesNode
        //            spsNode.Nodes.Add(spNode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public void GetDatabaseSps(TreeView tree, TreeNode spsNode, ImageList imageList, int spImageIndex)
        //{
        //    try
        //    {
        //        // clear current nodes
        //        spsNode.Nodes.Clear();

        //        // set tree.ImageList to our imageList parameter to use it's imageIndex
        //        tree.ImageList = imageList;

        //        // create a list of strings to hold name of sps
        //        List<string> sps = new List<string>();

        //        // get database name from parent node (database node is parent node of tablesNode)
        //        string database = spsNode.Parent.Text;

        //        // create a _server object to interact with sql _server inctance
        //        Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

        //        // set SystemObject for StoredProcedure type when _server loading data from _server to avoid overhead and prevent to hang
        //        _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedure), "IsSystemObject");

        //        // iterate through each sp that exist in database object
        //        foreach (Microsoft.SqlServer.Management.Smo.StoredProcedure sp in _server.Databases[database].StoredProcedures)
        //        {
        //            if (!sp.IsSystemObject)
        //            {
        //                sps.Add(sp.Name);
        //            }
        //        }

        //        // iterate through list object and add each item (table name) to treeView
        //        foreach (string str in sps)
        //        {
        //            // create a node to hold table name (child node or level 1)
        //            // then add child named 'Objects' to add + mark for sp node
        //            TreeNode spNode = new TreeNode(str);
        //            spNode.ImageIndex = spImageIndex;
        //            spNode.SelectedImageIndex = spImageIndex;
        //            spNode.Nodes.Add("Objects");

        //            // add tableNode to tablesNode
        //            spsNode.Nodes.Add(spNode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //#endregion

        #region get all views from current database

        public List<string> GetDatabaseOwnerViews()
        {
            return this.GetDatabaseOwnerViews(_Owner);
        }

        public List<string> GetDatabaseOwnerViews(string owner)
        {
            try
            {
                _Owner = owner;
                List<string> tableNames = new List<string>();
                string strSql = "SELECT VIEW_NAME FROM ALL_VIEWS T WHERE UPPER(T.OWNER) = UPPER('" + owner + "') ORDER BY t.VIEW_NAME";
                if (string.IsNullOrEmpty(owner))
                {
                    strSql = "SELECT VIEW_NAME FROM ALL_TABLES T ORDER BY t.VIEW_NAME";
                }

                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = _conn;
                    OracleDataReader odr = oCommand.ExecuteReader();
                    while (odr.Read())
                    {
                        tableNames.Add(odr.GetString(0));
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();
                }

                return tableNames;
            }
            catch
            {
                return null;
            }
        }

        public void GetDatabaseViews(TreeView tree, TreeNode viewsNode)
        {
            try
            {
                // clear current nodes
                viewsNode.Nodes.Clear();

                // create a list of strings to hold name of sps
                List<string> views = GetDatabaseOwnerViews();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = viewsNode.Parent.Text;

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in views)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for sp node
                    TreeNode spNode = new TreeNode(str);
                    spNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    viewsNode.Nodes.Add(spNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetDatabaseViews(TreeView tree, TreeNode viewsNode, string strDBName, ImageList imageList, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                viewsNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of strings to hold name of tables
                List<string> tables = GetDatabaseOwnerViews();

                // get database name from parent node (database node is parent node of tablesNode)
                string database = strDBName;

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode viewNode = new TreeNode(str);
                    viewNode.ImageIndex = tableImageIndex;
                    viewNode.SelectedImageIndex = tableImageIndex;
                    PMSRefDBViewProp refTableProp = new PMSRefDBViewProp();
                    refTableProp.StrTableName = str;
                    refTableProp.PMSRefDBConnection = (PMSRefDBConnection)(viewsNode.Tag);
                    viewNode.Tag = refTableProp;
                    viewNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    viewsNode.Nodes.Add(viewNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Dictionary<string, PMSRefDBViewProp> GetDatabaseViews(PMSRefDBConnection rc)
        {
            try
            {
                string owner = rc.Owner;
                Dictionary<string, PMSRefDBViewProp> dic = new Dictionary<string, PMSRefDBViewProp>();
                string strSql = "SELECT VIEW_NAME FROM ALL_VIEWS T WHERE UPPER(T.OWNER) = UPPER('" + owner + "') ORDER BY t.VIEW_NAME";
                if (string.IsNullOrEmpty(owner))
                {
                    strSql = "SELECT VIEW_NAME FROM ALL_TABLES T ORDER BY t.VIEW_NAME";
                }

                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = rc.GetOracleConnection();
                    OracleDataReader odr = oCommand.ExecuteReader();
                    while (odr.Read())
                    {
                        PMSRefDBViewProp vp = new PMSRefDBViewProp();
                        string viewName = odr.GetString(0);
                        vp.StrTableName = viewName;
                        vp.PMSRefDBConnection = rc;
                        dic.Add(viewName, vp);
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();
                    return dic;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Get all columns from current table

        //public string GetTableColumnDescription(string tableName, string columnName)
        //{
        //    try
        //    {
        //        foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[_databaseName].Tables[tableName].Columns)
        //        {
        //            if (col.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))
        //            {
        //                if (col.ExtendedProperties["MS_Description"] != null)
        //                    return col.ExtendedProperties["MS_Description"].Value.ToString();
        //                else
        //                    return null;
        //            }
        //        }
        //        return null;
        //    }
        //    catch (SmoException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}

        //public string GetTableColumnExtendedPropertie(string tableName, string columnName, string propertyName)
        //{
        //    try
        //    {
        //        foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[_databaseName].Tables[tableName].Columns)
        //        {
        //            if (col.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))
        //            {
        //                if (col.ExtendedProperties[propertyName] != null)
        //                    return col.ExtendedProperties[propertyName].ToString();
        //                else
        //                    return null;
        //            }
        //        }
        //        return null;
        //    }
        //    catch (SmoException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return null;
        //    }
        //}

        //public List<Microsoft.SqlServer.Management.Smo.Column> GetTableColumns(string tableName)
        //{
        //    try
        //    {
        //        List<Microsoft.SqlServer.Management.Smo.Column> columns = new List<Microsoft.SqlServer.Management.Smo.Column>();
        //        foreach (Microsoft.SqlServer.Management.Smo.Column col in _server.Databases[_databaseName].Tables[tableName].Columns)
        //        {
        //            columns.Add(col);
        //        }
        //        return columns;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public List<PMSDBFieldProp> GetTableColumns(string strTableName)
        {
            PMSDBTableProp prop = GetPMSDBTableProp(strTableName);
            List<PMSDBFieldProp> propList = prop.FieldPropCollection.ToList();
            return propList;
        }

        public void GetTableColumns(TreeView tree, TreeNode tableNode)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                string tablename = tableNode.Text;

                PMSDBTableProp prop = this.GetPMSDBTableProp(tablename);

                // iterate through list object and add each item (column name) to treeView
                foreach (PMSDBFieldProp col in prop.FieldPropCollection)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.StrFieldName);
                    colNode.Text += "," + col.StrFieldType;

                    // add tableNode to tablesNode
                    tableNode.Nodes.Add(colNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                string tablename = "";
                if (tableNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    tablename = tableNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    tablename = tableNode.Text;

                PMSDBTableProp prop = this.GetPMSDBTableProp(tablename);

                // iterate through list object and add each item (column name) to treeView
                foreach (PMSDBFieldProp col in prop.FieldPropCollection)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.StrFieldName);
                    colNode.ImageIndex = columnImageIndex;
                    colNode.SelectedImageIndex = columnImageIndex;
                    string strSize = "";
                    if (col.StrFieldType.Contains("char"))
                        strSize = string.Format("({0})", col.IFieldLength.ToString());
                    string strNullable;
                    if (col.BFieldNullable)
                        strNullable = "null";
                    else
                        strNullable = "not null";
                    colNode.Text = string.Format("{0} ({1}{2}, {3})", colNode.Text, col.StrFieldType, strSize, strNullable);

                    // add tableNode to tablesNode
                    tableNode.Nodes.Add(colNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                string tablename = "";
                if (tableNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    tablename = tableNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    tablename = tableNode.Text;

                
                PMSDBTableProp prop = this.GetPMSDBTableProp(tablename);

                // iterate through list object and add each item (column name) to treeView
                foreach (PMSDBFieldProp col in prop.FieldPropCollection)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.StrFieldName);
                    string strpfKey = "";
                    if (col.BFieldPrimaryKey && col.BFieldForeignKey)
                    {
                        colNode.ImageIndex = pfkeyImageIndex;
                        colNode.SelectedImageIndex = pfkeyImageIndex;
                        strpfKey = "PK, FK, ";
                    }
                    else if (col.BFieldPrimaryKey)
                    {
                        colNode.ImageIndex = pkeyImageIndex;
                        colNode.SelectedImageIndex = pkeyImageIndex;
                        strpfKey = "PK, ";
                    }
                    else if (col.BFieldForeignKey)
                    {
                        colNode.ImageIndex = fkeyImageIndex;
                        colNode.SelectedImageIndex = fkeyImageIndex;
                        strpfKey = "FK, ";
                    }
                    else
                    {
                        colNode.ImageIndex = columnImageIndex;
                        colNode.SelectedImageIndex = columnImageIndex;
                    }

                    string strSize = "";
                    if (col.StrFieldType.Contains("char"))
                        strSize = string.Format("({0})", col.IFieldLength.ToString());
                    string strNullable;
                    if (col.BFieldNullable)
                        strNullable = "null";
                    else
                        strNullable = "not null";
                    colNode.Text = string.Format("{0} ({1}{2}{3}, {4})", colNode.Text, strpfKey, col.StrFieldType, strSize, strNullable);

                    colNode.Tag = prop;
                    // add tableNode to tablesNode
                    tableNode.Nodes.Add(colNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetRefTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                string tablename = "";
                if (tableNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    tablename = tableNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    tablename = tableNode.Text;

                PMSRefDBFieldPropCollection collect = new PMSRefDBFieldPropCollection();
                //PMSDBTableProp prop = this.GetPMSDBTableProp(tablename);
                //PMSDBTableProp TableProp = null;
                string database = _databaseName;
                string strSql =
                    @"SELECT T.COLUMN_ID
                          ,T.COLUMN_NAME
                          ,T.DATA_TYPE
                          ,T.DATA_LENGTH
                          ,(CASE WHEN T.NULLABLE = 'N' THEN 0 ELSE 1 END) AS NULLABLE
                          ,(CASE WHEN P.COLUMN_NAME = T.COLUMN_NAME THEN 1 ELSE 0 END) AS ISPRIMARYKEY
                          ,(CASE WHEN T.COLUMN_NAME = R.COLUMN_NAME THEN 1 ELSE 0 END) AS ISFOREIGNKEY
                          ,T.DATA_DEFAULT
                     FROM ALL_TAB_COMMENTS C
                    LEFT JOIN ALL_TAB_COLUMNS T ON C.TABLE_NAME = T.TABLE_NAME
                    LEFT JOIN (
                    SELECT T2.TABLE_NAME,T2.COLUMN_NAME
                         FROM ALL_CONSTRAINTS T1 LEFT JOIN ALL_CONS_COLUMNS T2 
                     ON T1.CONSTRAINT_NAME=T2.CONSTRAINT_NAME 
                     WHERE T1.CONSTRAINT_TYPE = 'P' AND T2.POSITION = 1
                     ) P ON T.TABLE_NAME = P.TABLE_NAME 
                    LEFT JOIN (
                    SELECT DISTINCT T2.TABLE_NAME,T2.COLUMN_NAME,T1.R_CONSTRAINT_NAME 
                     FROM ALL_CONSTRAINTS T1 LEFT JOIN ALL_CONS_COLUMNS T2 
                     ON T1.CONSTRAINT_NAME=T2.CONSTRAINT_NAME 
                     WHERE T1.CONSTRAINT_TYPE = 'R'
                     ) R ON T.TABLE_NAME = R.TABLE_NAME AND T.COLUMN_NAME = R.COLUMN_NAME
                    WHERE UPPER(C.OWNER) = UPPER('" + _Owner +
                    "') AND UPPER(T.TABLE_NAME) = UPPER('" + tablename +
                    @"') AND C.TABLE_TYPE = 'TABLE'
                    ORDER BY T.TABLE_NAME,T.COLUMN_ID";

                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = _conn;
                    OracleDataReader odr = oCommand.ExecuteReader();
                    
                    while (odr.Read())
                    {
                        PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                        RefDBFieldProp.PMSRefDBConnection = (PMSRefDBConnection)(tableNode.Parent.Tag);
                        RefDBFieldProp.StrTableName = tablename;
                        RefDBFieldProp.IFildID = odr.GetOracleDecimal(0).ToInt32();
                        RefDBFieldProp.StrFieldName = odr.GetString(1);
                        RefDBFieldProp.StrFieldType = odr.GetString(2);
                        RefDBFieldProp.DbType = RefDBInfo.GetDbType(odr.GetString(2));
                        RefDBFieldProp.Type = RefDBInfo.GetCSharpType(RefDBFieldProp.DbType.ToString());
                        RefDBFieldProp.IFieldLength = odr.GetOracleDecimal(3).ToInt32();
                        RefDBFieldProp.BFieldNullable = Convert.ToBoolean(odr.GetOracleDecimal(4).Value);
                        RefDBFieldProp.BFieldPrimaryKey = Convert.ToBoolean(odr.GetOracleDecimal(5).Value);
                        RefDBFieldProp.BFieldForeignKey = Convert.ToBoolean(odr.GetOracleDecimal(6).Value);
                        RefDBFieldProp.BFieldIdentity = false;
                        RefDBFieldProp.StrFieldDefault = (odr.GetOracleString(7).IsNull) ? null : odr.GetString(7);
                        if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                            RefDBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(tablename, odr.GetString(1));
                        collect.Add(RefDBFieldProp);
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();
                }

                // iterate through list object and add each item (column name) to treeView
                foreach (PMSRefDBFieldProp col in collect)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.StrFieldName);
                    string strpfKey = "";
                    if (col.BFieldPrimaryKey && col.BFieldForeignKey)
                    {
                        colNode.ImageIndex = pfkeyImageIndex;
                        colNode.SelectedImageIndex = pfkeyImageIndex;
                        strpfKey = "PK, FK, ";
                    }
                    else if (col.BFieldPrimaryKey)
                    {
                        colNode.ImageIndex = pkeyImageIndex;
                        colNode.SelectedImageIndex = pkeyImageIndex;
                        strpfKey = "PK, ";
                    }
                    else if (col.BFieldForeignKey)
                    {
                        colNode.ImageIndex = fkeyImageIndex;
                        colNode.SelectedImageIndex = fkeyImageIndex;
                        strpfKey = "FK, ";
                    }
                    else
                    {
                        colNode.ImageIndex = columnImageIndex;
                        colNode.SelectedImageIndex = columnImageIndex;
                    }

                    string strSize = "";
                    if (col.StrFieldType.ToLower().Contains("char"))
                        strSize = string.Format("({0})", col.IFieldLength.ToString());
                    string strNullable;
                    if (col.BFieldNullable)
                        strNullable = "null";
                    else
                        strNullable = "not null";
                    colNode.Text = string.Format("{0} ({1}{2}{3}, {4})", colNode.Text, strpfKey, col.StrFieldType, strSize, strNullable);

                    colNode.Tag = col;
                    // add tableNode to tablesNode
                    tableNode.Nodes.Add(colNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        public Dictionary<string, PMSRefDBFieldProp> GetRefTableColumns(string tableName, PMSRefDBConnection rc)
        {
            try
            {
                Dictionary<string, PMSRefDBFieldProp> collect = new Dictionary<string, PMSRefDBFieldProp>();
                string strSql =
                    @"SELECT T.COLUMN_ID
                          ,T.COLUMN_NAME
                          ,T.DATA_TYPE
                          ,T.DATA_LENGTH
                          ,(CASE WHEN T.NULLABLE = 'N' THEN 0 ELSE 1 END) AS NULLABLE
                          ,(CASE WHEN P.COLUMN_NAME = T.COLUMN_NAME THEN 1 ELSE 0 END) AS ISPRIMARYKEY
                          ,(CASE WHEN T.COLUMN_NAME = R.COLUMN_NAME THEN 1 ELSE 0 END) AS ISFOREIGNKEY
                          ,T.DATA_DEFAULT
                     FROM ALL_TAB_COMMENTS C
                    LEFT JOIN ALL_TAB_COLUMNS T ON C.TABLE_NAME = T.TABLE_NAME
                    LEFT JOIN (
                    SELECT T2.TABLE_NAME,T2.COLUMN_NAME
                         FROM ALL_CONSTRAINTS T1 LEFT JOIN ALL_CONS_COLUMNS T2 
                     ON T1.CONSTRAINT_NAME=T2.CONSTRAINT_NAME 
                     WHERE T1.CONSTRAINT_TYPE = 'P' AND T2.POSITION = 1
                     ) P ON T.TABLE_NAME = P.TABLE_NAME 
                    LEFT JOIN (
                    SELECT DISTINCT T2.TABLE_NAME,T2.COLUMN_NAME,T1.R_CONSTRAINT_NAME 
                     FROM ALL_CONSTRAINTS T1 LEFT JOIN ALL_CONS_COLUMNS T2 
                     ON T1.CONSTRAINT_NAME=T2.CONSTRAINT_NAME 
                     WHERE T1.CONSTRAINT_TYPE = 'R'
                     ) R ON T.TABLE_NAME = R.TABLE_NAME AND T.COLUMN_NAME = R.COLUMN_NAME
                    WHERE UPPER(C.OWNER) = UPPER('" + rc.Owner +
                    "') AND UPPER(T.TABLE_NAME) = UPPER('" + tableName +
                    @"') AND C.TABLE_TYPE = 'TABLE'
                    ORDER BY T.TABLE_NAME,T.COLUMN_ID";

                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = _conn;
                    OracleDataReader odr = oCommand.ExecuteReader();

                    while (odr.Read())
                    {
                        PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                        RefDBFieldProp.PMSRefDBConnection = rc;
                        RefDBFieldProp.StrTableName = tableName;
                        RefDBFieldProp.IFildID = odr.GetOracleDecimal(0).ToInt32();
                        RefDBFieldProp.StrFieldName = odr.GetString(1);
                        RefDBFieldProp.StrFieldType = odr.GetString(2);
                        RefDBFieldProp.DbType = RefDBInfo.GetDbType(odr.GetString(2));
                        RefDBFieldProp.Type = RefDBInfo.GetCSharpType(RefDBFieldProp.DbType.ToString());
                        RefDBFieldProp.IFieldLength = odr.GetOracleDecimal(3).ToInt32();
                        RefDBFieldProp.BFieldNullable = Convert.ToBoolean(odr.GetOracleDecimal(4).Value);
                        RefDBFieldProp.BFieldPrimaryKey = Convert.ToBoolean(odr.GetOracleDecimal(5).Value);
                        RefDBFieldProp.BFieldForeignKey = Convert.ToBoolean(odr.GetOracleDecimal(6).Value);
                        RefDBFieldProp.BFieldIdentity = false;
                        RefDBFieldProp.StrFieldDefault = (odr.GetOracleString(7).IsNull) ? null : odr.GetString(7);
                        if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                            RefDBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(tableName, odr.GetString(1));
                        collect.Add(RefDBFieldProp.StrFieldName, RefDBFieldProp);
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();
                    return collect;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        #endregion

        #region Get all columns from current view

        public void GetRefViewColumns(TreeView tree, TreeNode viewNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            try
            {
                // clear current nodes
                viewNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                string tablename = "";
                if (viewNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    tablename = viewNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    tablename = viewNode.Text;

                PMSRefDBFieldPropCollection collect = new PMSRefDBFieldPropCollection();
                //PMSDBTableProp prop = this.GetPMSDBTableProp(tablename);
                //PMSDBTableProp TableProp = null;
                string database = _databaseName;
                string strSql =
                    @"SELECT T.COLUMN_ID
                          ,T.COLUMN_NAME
                          ,T.DATA_TYPE
                          ,T.DATA_LENGTH
                          ,(CASE WHEN T.NULLABLE = 'N' THEN 0 ELSE 1 END) AS NULLABLE
                          ,0 AS ISPRIMARYKEY
                          ,0 AS ISFOREIGNKEY
                          ,T.DATA_DEFAULT
                     FROM ALL_TAB_COLS T
                    LEFT JOIN ALL_VIEWS C ON T.TABLE_NAME = C.VIEW_NAME
                    WHERE UPPER(C.OWNER) = UPPER('" + _Owner +
                    "') AND UPPER(T.TABLE_NAME) = UPPER('" + tablename +
                    "') ORDER BY T.TABLE_NAME,T.COLUMN_ID";

                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = _conn;
                    OracleDataReader odr = oCommand.ExecuteReader();

                    while (odr.Read())
                    {
                        PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                        RefDBFieldProp.PMSRefDBConnection = (PMSRefDBConnection)(viewNode.Parent.Tag);
                        RefDBFieldProp.StrTableName = tablename;
                        RefDBFieldProp.IFildID = odr.GetOracleDecimal(0).ToInt32();
                        RefDBFieldProp.StrFieldName = odr.GetString(1);
                        RefDBFieldProp.StrFieldType = odr.GetString(2);
                        RefDBFieldProp.DbType = RefDBInfo.GetDbType(odr.GetString(2));
                        RefDBFieldProp.Type = RefDBInfo.GetCSharpType(RefDBFieldProp.DbType.ToString());
                        RefDBFieldProp.IFieldLength = odr.GetOracleDecimal(3).ToInt32();
                        RefDBFieldProp.BFieldNullable = Convert.ToBoolean(odr.GetOracleDecimal(4).Value);
                        RefDBFieldProp.BFieldPrimaryKey = Convert.ToBoolean(odr.GetOracleDecimal(5).Value);
                        RefDBFieldProp.BFieldForeignKey = Convert.ToBoolean(odr.GetOracleDecimal(6).Value);
                        RefDBFieldProp.BFieldIdentity = false;
                        RefDBFieldProp.StrFieldDefault = (odr.GetOracleString(7).IsNull) ? null : odr.GetString(7);
                        if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                            RefDBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(tablename, odr.GetString(1));
                        collect.Add(RefDBFieldProp);
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();
                }

                // iterate through list object and add each item (column name) to treeView
                foreach (PMSRefDBFieldProp col in collect)
                {
                    // create a node to hold column name (level 2)
                    // then add dataType to it's text
                    TreeNode colNode = new TreeNode(col.StrFieldName);
                    string strpfKey = "";
                    if (col.BFieldPrimaryKey && col.BFieldForeignKey)
                    {
                        colNode.ImageIndex = pfkeyImageIndex;
                        colNode.SelectedImageIndex = pfkeyImageIndex;
                        strpfKey = "PK, FK, ";
                    }
                    else if (col.BFieldPrimaryKey)
                    {
                        colNode.ImageIndex = pkeyImageIndex;
                        colNode.SelectedImageIndex = pkeyImageIndex;
                        strpfKey = "PK, ";
                    }
                    else if (col.BFieldForeignKey)
                    {
                        colNode.ImageIndex = fkeyImageIndex;
                        colNode.SelectedImageIndex = fkeyImageIndex;
                        strpfKey = "FK, ";
                    }
                    else
                    {
                        colNode.ImageIndex = columnImageIndex;
                        colNode.SelectedImageIndex = columnImageIndex;
                    }

                    string strSize = "";
                    if (col.StrFieldType.ToLower().Contains("char"))
                        strSize = string.Format("({0})", col.IFieldLength.ToString());
                    string strNullable;
                    if (col.BFieldNullable)
                        strNullable = "null";
                    else
                        strNullable = "not null";
                    colNode.Text = string.Format("{0} ({1}{2}{3}, {4})", colNode.Text, strpfKey, col.StrFieldType, strSize, strNullable);

                    colNode.Tag = col;
                    // add tableNode to tablesNode
                    viewNode.Nodes.Add(colNode);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Dictionary<string, PMSRefDBFieldProp> GetRefViewColumns(string tableName, PMSRefDBConnection rc)
        {
            try
            {
                // get database name from parent node (database node is parent node of tablesNode, the tablesNode is parent of tableNode)
                string database = DatabaseName;//tableNode.Parent.Parent.Text;
                string strSql =
                    @"SELECT T.COLUMN_ID
                          ,T.COLUMN_NAME
                          ,T.DATA_TYPE
                          ,T.DATA_LENGTH
                          ,(CASE WHEN T.NULLABLE = 'N' THEN 0 ELSE 1 END) AS NULLABLE
                          ,0 AS ISPRIMARYKEY
                          ,0 AS ISFOREIGNKEY
                          ,T.DATA_DEFAULT
                     FROM ALL_TAB_COLS T
                    LEFT JOIN ALL_VIEWS C ON T.TABLE_NAME = C.VIEW_NAME
                    WHERE UPPER(C.OWNER) = UPPER('" + rc.Owner +
                    "') AND UPPER(T.TABLE_NAME) = UPPER('" + tableName +
                    "') ORDER BY T.TABLE_NAME,T.COLUMN_ID";

                Dictionary<string, PMSRefDBFieldProp> dic = new Dictionary<string, PMSRefDBFieldProp>();

                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = strSql;
                    oCommand.Connection = _conn;
                    OracleDataReader odr = oCommand.ExecuteReader();

                    while (odr.Read())
                    {
                        PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                        RefDBFieldProp.PMSRefDBConnection = rc;
                        RefDBFieldProp.StrTableName = tableName;
                        RefDBFieldProp.IFildID = odr.GetOracleDecimal(0).ToInt32();
                        RefDBFieldProp.StrFieldName = odr.GetString(1);
                        RefDBFieldProp.StrFieldType = odr.GetString(2);
                        RefDBFieldProp.DbType = RefDBInfo.GetDbType(odr.GetString(2));
                        RefDBFieldProp.Type = RefDBInfo.GetCSharpType(RefDBFieldProp.DbType.ToString());
                        RefDBFieldProp.IFieldLength = odr.GetOracleDecimal(3).ToInt32();
                        RefDBFieldProp.BFieldNullable = Convert.ToBoolean(odr.GetOracleDecimal(4).Value);
                        RefDBFieldProp.BFieldPrimaryKey = Convert.ToBoolean(odr.GetOracleDecimal(5).Value);
                        RefDBFieldProp.BFieldForeignKey = Convert.ToBoolean(odr.GetOracleDecimal(6).Value);
                        RefDBFieldProp.BFieldIdentity = false;
                        RefDBFieldProp.StrFieldDefault = (odr.GetOracleString(7).IsNull) ? null : odr.GetString(7);
                        if (!CurrentPrjInfo.IsIndependentDesignerMode && !CurrentPrjInfo.IsReportViewerMode)
                            RefDBFieldProp.StrFieldDescription = PMSDBStructure.GetTableColumnDescription(tableName, odr.GetString(1));

                        dic.Add(RefDBFieldProp.StrFieldName, RefDBFieldProp);
                    }

                    odr.Close();
                    // Clean up
                    oCommand.Dispose();
                }

                return dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        #endregion

        //#region Get all stored procedure parameters from current stored procedures

        //public void GetProcedureParameters(TreeView tree, TreeNode spNode)
        //{
        //    try
        //    {
        //        // clear current nodes
        //        spNode.Nodes.Clear();

        //        // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all stored procedure parameters
        //        List<Microsoft.SqlServer.Management.Smo.StoredProcedureParameter> parameters = new List<Microsoft.SqlServer.Management.Smo.StoredProcedureParameter>();

        //        // get database name from parent node (database node is parent node of spsNode, the spsNode is parent of spNode)
        //        string database = spNode.Parent.Parent.Text;

        //        // create a _server object to interact with sql _server inctance
        //        Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

        //        // set Name,DataType for StoredProcedureParameter type when _server loading data from _server to avoid overhead and prevent to hang
        //        _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedureParameter), new string[] { "Name", "DataType" });

        //        // iterate through each storedProcedureParameter that exist in storedProcedure object
        //        foreach (Microsoft.SqlServer.Management.Smo.StoredProcedureParameter parameter in _server.Databases[database].StoredProcedures[spNode.Text].Parameters)
        //        {
        //            parameters.Add(parameter);
        //        }

        //        // iterate through list object and add each item (storedProcedureParameter name) to treeView
        //        foreach (Microsoft.SqlServer.Management.Smo.StoredProcedureParameter parameter in parameters)
        //        {
        //            // create a node to hold parameter name (level 2)
        //            TreeNode parameterNode = new TreeNode(parameter.Name);
        //            parameterNode.Text += "," + parameter.DataType.ToString() + "(" + parameter.DataType.MaximumLength.ToString() + ")";

        //            // add tableNode to tablesNode
        //            spNode.Nodes.Add(parameterNode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public void GetProcedureParameters(TreeView tree, TreeNode spNode, ImageList imageList, int spParameterImageIndex)
        //{
        //    try
        //    {
        //        // clear current nodes
        //        spNode.Nodes.Clear();

        //        // set tree.ImageList to our imageList parameter to use it's imageIndex
        //        tree.ImageList = imageList;

        //        // create a list of Microsoft.SqlServer.Management.Smo.Column to hold information of all stored procedure parameters
        //        List<Microsoft.SqlServer.Management.Smo.StoredProcedureParameter> parameters = new List<Microsoft.SqlServer.Management.Smo.StoredProcedureParameter>();

        //        // get database name from parent node (database node is parent node of spsNode, the spsNode is parent of spNode)
        //        string database = spNode.Parent.Parent.Text;

        //        // create a _server object to interact with sql _server inctance
        //        Microsoft.SqlServer.Management.Smo.Server _server = new Microsoft.SqlServer.Management.Smo.Server(this._ServerConnection);

        //        // set Name,DataType for StoredProcedureParameter type when _server loading data from _server to avoid overhead and prevent to hang
        //        _server.SetDefaultInitFields(typeof(Microsoft.SqlServer.Management.Smo.StoredProcedureParameter), new string[] { "Name", "DataType" });

        //        // iterate through each storedProcedureParameter that exist in storedProcedure object
        //        foreach (Microsoft.SqlServer.Management.Smo.StoredProcedureParameter parameter in _server.Databases[database].StoredProcedures[spNode.Text].Parameters)
        //        {
        //            parameters.Add(parameter);
        //        }

        //        // iterate through list object and add each item (storedProcedureParameter name) to treeView
        //        foreach (Microsoft.SqlServer.Management.Smo.StoredProcedureParameter parameter in parameters)
        //        {
        //            // create a node to hold parameter name (level 2)
        //            TreeNode parameterNode = new TreeNode(parameter.Name);
        //            parameterNode.ImageIndex = spParameterImageIndex;
        //            parameterNode.SelectedImageIndex = spParameterImageIndex;
        //            parameterNode.Text += "," + parameter.DataType.ToString() + "(" + parameter.DataType.MaximumLength.ToString() + ")";

        //            // add tableNode to tablesNode
        //            spNode.Nodes.Add(parameterNode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //#endregion

        #region Alter sth of db

        public bool RenameTableName(string oldname, string newname)
        {
            try
            {
                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = "ALTER TABLE " + _Owner + "." + oldname + " RENAME TO " + newname;
                    oCommand.Connection = _conn;
                    oCommand.ExecuteNonQuery();

                    // Clean up
                    oCommand.Dispose();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool RenameTableColumnName(string tablename, string oldname, string newname)
        {
            try
            {
                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = "ALTER TABLE " + _Owner + "." + tablename + " RENAME COLUMN " + oldname + " TO " + newname;
                    oCommand.Connection = _conn;
                    oCommand.ExecuteNonQuery();

                    // Clean up
                    oCommand.Dispose();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteTable(string tablename)
        {
            try
            {
                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = "DROP TABLE " + _Owner + "." + tablename;
                    oCommand.Connection = _conn;
                    oCommand.ExecuteNonQuery();

                    // Clean up
                    oCommand.Dispose();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteTableColumn(string tablename, string columnname)
        {
            try
            {
                if (_conn != null)
                {
                    OracleCommand oCommand = new OracleCommand();
                    oCommand.CommandType = CommandType.Text;
                    oCommand.CommandText = "ALTER TABLE " + _Owner + "." + tablename + "DROP COLUMN " + columnname;
                    oCommand.Connection = _conn;
                    oCommand.ExecuteNonQuery();

                    // Clean up
                    oCommand.Dispose();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException.InnerException.Message);
                return false;
            }
        }

        #endregion
    }

    [Serializable]
    public struct TableField
    {
        public string tableName;
        public string fieldName;
    }

    public class SqlStructureHolder
    {
        public static SqlAssistance sa = new SqlAssistance();
        private static string _serverInstance = string.Empty;
        private static string _databaseName = string.Empty;
        private static string _userName = string.Empty;
        private static string _password = string.Empty;
        private static ConnectType _protocolType = ConnectType.namepipe;
        private static string _portID = "1433";
        private static bool _bSMOConnected = false;
        public static Dictionary<string, List<Microsoft.SqlServer.Management.Smo.Column>> tableColumnInfo = new Dictionary<string, List<Microsoft.SqlServer.Management.Smo.Column>>();
        public static List<string> tableList = new List<string>();
        //外键列表
        public static Dictionary<TableField, TableField> foreignKeyInfo = new Dictionary<TableField, TableField>();
        //表内树形结构列表
        public static Dictionary<string, TreeView> relationInfo = new Dictionary<string, TreeView>();

        #region property

        public static string SqlServerInstance
        {
            get { return _serverInstance; }
            set { _serverInstance = value; }
        }
        public static string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }
        public static string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public static ConnectType ProtocolType
        {
            get { return _protocolType; }
            set { _protocolType = value; }
        }
        public static string PortID
        {
            get { return _portID; }
            set { _portID = value; }
        }

        public static string ConnectString
        {
            get { return sa.ConnectionString; }
            set { sa.ConnectionString = value; }
        }

        public static bool bSMOConnected
        {
            get { return _bSMOConnected; }
            set { _bSMOConnected = value; }
        }

        #endregion

        public static bool bReconnected = false;

        public void InitSqlStructure(string instance, string database, string userName, string password, ConnectType protocolType, string portID)
        {
            SqlStructure.InitSqlStructure(instance, database, userName, password, protocolType, portID);
        }

        public SqlConnection GetSqlConncetion()
        {
            return SqlStructure.GetSqlConncetion();
        }
    }

    public class SqlStructure
    {
        //private static SqlAssistance sa = new SqlAssistance();
        //private static string _serverInstance = string.Empty;
        //private static string _databaseName = string.Empty;
        //private static string _userName = string.Empty;
        //private static string _password = string.Empty;
        //private static ConnectType _protocolType = ConnectType.namepipe;
        //private static string _portID = "1433";
        //private static bool _bSMOConnected = false;
        //public static Dictionary<string, List<Microsoft.SqlServer.Management.Smo.Column>> tableColumnInfo = new Dictionary<string, List<Microsoft.SqlServer.Management.Smo.Column>>();
        //public static List<string> tableList = new List<string>();
        ////外键列表
        //public static Dictionary<TableField, TableField> foreignKeyInfo = new Dictionary<TableField, TableField>();
        ////表内树形结构列表
        //public static Dictionary<string, TreeView> relationInfo = new Dictionary<string, TreeView>();

        //#region property

        //public static string SqlServerInstance
        //{
        //    get { return _serverInstance; }
        //    set { _serverInstance = value; }
        //}
        //public static string DatabaseName
        //{
        //    get { return _databaseName; }
        //    set { _databaseName = value; }
        //}
        //public static string UserName
        //{
        //    get { return _userName; }
        //    set { _userName = value; }
        //}
        //public static string Password
        //{
        //    get { return _password; }
        //    set { _password = value; }
        //}
        //public static ConnectType ProtocolType
        //{
        //    get { return _protocolType; }
        //    set { _protocolType = value; }
        //}
        //public static string PortID
        //{
        //    get { return _portID; }
        //    set { _portID = value; }
        //}

        //public static string ConnectString
        //{
        //    get { return sa.ConnectionString; }
        //    set { sa.ConnectionString = value; }
        //}

        //public static bool bSMOConnected
        //{
        //    get { return SqlStructure._bSMOConnected; }
        //    set { SqlStructure._bSMOConnected = value; }
        //}

        //#endregion

        //private static bool bReconnected = false;

        //public SqlStructure(string instance, string database, string userName, string password, ConnectType protocolType, string portID)
        //{
        //    InitSqlStructure(instance, database, userName, password, protocolType, portID);
        //}

        public static void InitSqlStructure(string instance, string database, string userName, string password, ConnectType protocolType, string portID)
        {
            SqlStructureHolder.bReconnected = false;
            if (string.Compare(SqlStructureHolder.SqlServerInstance, instance) != 0 || string.Compare(SqlStructureHolder.DatabaseName, database) != 0 || string.Compare(SqlStructureHolder.PortID, portID) != 0 || protocolType != SqlStructureHolder.ProtocolType)
                SqlStructureHolder.bReconnected = true;
            SqlStructureHolder.sa.DatabaseName = SqlStructureHolder.DatabaseName = database;
            SqlStructureHolder.sa.Password = SqlStructureHolder.Password = password;
            SqlStructureHolder.sa.SqlServerInstance = SqlStructureHolder.SqlServerInstance = instance;
            SqlStructureHolder.sa.UserName = SqlStructureHolder.UserName = userName;
            SqlStructureHolder.sa.ConnectType = SqlStructureHolder.ProtocolType = protocolType;
            SqlStructureHolder.sa.PortID = SqlStructureHolder.PortID = portID;
        }

        /// <summary>
        /// 初始化表名列表,表字段列表和外键列表
        /// </summary>
        public static void InitialStructure()
        {
            try
            {
                bool bAll = true;
                if (SqlStructureHolder.sa.ConnectSMOServer(SqlStructureHolder.SqlServerInstance, SqlStructureHolder.UserName, SqlStructureHolder.Password))
                {
                    SqlStructureHolder.tableList = SqlStructureHolder.sa.GetDatabaseTables(SqlStructureHolder.DatabaseName);

                    if (SqlStructureHolder.tableList == null)
                    {
                        string strText = string.Format(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSSqlAssistance_DBNULL"), SqlStructureHolder.DatabaseName);
                        string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                        MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (bAll)
                    {
                        TableCollection tc = SqlStructureHolder.sa.GetDatabaseTableCollection(SqlStructureHolder.DatabaseName);
                        foreach (Microsoft.SqlServer.Management.Smo.Table table in tc)
                        {
                            if (!table.IsSystemObject)
                            {
                                foreach (ForeignKey key in table.ForeignKeys)
                                {
                                    foreach (ForeignKeyColumn column in key.Columns)
                                    {
                                        TableField tf = new TableField();
                                        tf.tableName = table.Name;
                                        tf.fieldName = column.Name;
                                        TableField tp = new TableField();
                                        tp.tableName = key.ReferencedTable;
                                        tp.fieldName = column.ReferencedColumn;
                                        if (!SqlStructureHolder.foreignKeyInfo.ContainsKey(tf))
                                            SqlStructureHolder.foreignKeyInfo.Add(tf, tp);
                                        //Console.WriteLine("Column: {0} is a foreign key to Table: {1}", column.Name, key.ReferencedTable);
                                    }
                                }
                            }
                        }
                    }

                    foreach (string strTable in SqlStructureHolder.tableList)
                    {
                        List<Microsoft.SqlServer.Management.Smo.Column> cols = SqlStructureHolder.sa.GetTableColumns(strTable);

                        //表与字段列表对应
                        if (!SqlStructureHolder.tableColumnInfo.ContainsKey(strTable))
                            SqlStructureHolder.tableColumnInfo.Add(strTable, cols);

                        //if (bAll)
                        //{
                        //    //生成外键对应主键列表
                        //    foreach (Microsoft.SqlServer.Management.Smo.Column col in cols)
                        //    {
                        //        DataTable tTable = col.EnumForeignKeys();
                        //        for (int i = 0; i < tTable.Rows.Count; i++)
                        //        {
                        //            TableField tf = new TableField();
                        //            tf.tableName = (string)tTable.Rows[i].ItemArray[1];
                        //            tf.fieldName = col.Name;
                        //            if (!foreignKeyInfo.ContainsKey(tf))
                        //                foreignKeyInfo.Add(tf, strTable);
                        //        }
                        //    }
                        //}
                    }
                    SqlStructureHolder.bSMOConnected = true;
                }
                else
                    SqlStructureHolder.bSMOConnected = false;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获得表字段信息
        /// </summary>
        /// <param name="strTable">表名</param>
        /// <returns>字段列表</returns>
        public static List<Microsoft.SqlServer.Management.Smo.Column> getColumnInfo(string strTable)
        {
            try
            {
                if (SqlStructureHolder.tableColumnInfo.ContainsKey(strTable))
                {
                    return SqlStructureHolder.tableColumnInfo[strTable];
                }
                else
                {
                    List<Microsoft.SqlServer.Management.Smo.Column> cols = SqlStructureHolder.sa.GetTableColumns(strTable);
                    if (!SqlStructureHolder.tableColumnInfo.ContainsKey(strTable))
                        SqlStructureHolder.tableColumnInfo.Add(strTable, cols);
                    return cols;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得主表表名
        /// </summary>
        /// <param name="tableField">外键表名和字段名结构</param>
        /// <returns>主表表名</returns>
        public static TableField getMainKeyTable(TableField tableField)
        {
            try
            {
                if (SqlStructureHolder.foreignKeyInfo.ContainsKey(tableField))
                {
                    return SqlStructureHolder.foreignKeyInfo[tableField];
                }
                else
                {
                    return new TableField();
                }
            }
            catch
            {
                return new TableField();
            }
        }

        public static bool ConnectSMOServer()
        {
            try
            {
                //if (sa.ConnectSMOServer(_serverInstance, _userName, _password))
                if (SqlStructureHolder.sa.ConnectSMOServer(GetSqlConncetion()))
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
  
        public static SqlConnection GetSqlConncetion(bool wantNewTempConnection = false,int timeout = 30)
        {
            try
            {
                if (wantNewTempConnection)
                    return SqlStructureHolder.sa.GetNewConnection(timeout);
                SqlConnection conn = SqlStructureHolder.sa.SqlConnection;
                if (null != conn && !PMSDBConnection.TestConnection(conn))
                {
                    //原数据库连接失效，重新创建新的连接
                    conn.Close();
                    conn.Dispose();
                    if (SqlStructureHolder.sa.ConnectDatabase())
                    {
                        conn = SqlStructureHolder.sa.SqlConnection;
                        if (conn.State != ConnectionState.Open || SqlStructureHolder.bReconnected)
                        {
                            conn.Close();
                            conn.Open();
                        }
                    }
                    else
                        conn = null;
                }

                if (conn == null || !(conn.State == ConnectionState.Open) || SqlStructureHolder.bReconnected)
                {
                    if (SqlStructureHolder.sa.ConnectDatabase())
                    {
                        conn = SqlStructureHolder.sa.SqlConnection;
                        if (conn.State != ConnectionState.Open || SqlStructureHolder.bReconnected)
                        {
                            conn.Close();
                            conn.Open();
                        }
                    }
                    else
                        conn = null;
                }
                return conn;
            }
            catch(System.Data.SqlClient.SqlException ex)
            {
                string strText = ex.Message;
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Fail");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public static SqlConnection GetSqlConncetion(string connectstring)
        {
            try
            {
                SqlConnection conn = SqlStructureHolder.sa.SqlConnection;
                if (null != conn && !PMSDBConnection.TestConnection(conn))
                {
                    //原数据库连接失效，重新创建新的连接
                    conn.Close();
                    conn.Dispose();
                    if (SqlStructureHolder.sa.ConnectDatabase(connectstring))
                    {
                        conn = SqlStructureHolder.sa.SqlConnection;
                        if (conn.State != ConnectionState.Open || SqlStructureHolder.bReconnected)
                        {
                            conn.Close();
                            conn.Open();
                        }
                    }
                    else
                        conn = null;
                }

                if (conn == null || !(conn.State == ConnectionState.Open) || SqlStructureHolder.bReconnected)
                {
                    if (SqlStructureHolder.sa.ConnectDatabase(connectstring))
                    {
                        conn = SqlStructureHolder.sa.SqlConnection;
                        if (conn.State != ConnectionState.Open || SqlStructureHolder.bReconnected)
                        {
                            conn.Close();
                            conn.Open();
                        }
                    }
                    else
                        conn = null;
                }
                return conn;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string strText = ex.Message;
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Fail");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public static IDbConnection GetOleDBConncetion()
        {
            try
            {
                OleDbConnection conn = SqlStructureHolder.sa.OleDBConn;
                if (conn == null || !(conn.State == ConnectionState.Open) || SqlStructureHolder.bReconnected)
                {
                    if (SqlStructureHolder.sa.ConnectOleDB())
                    {
                        conn = SqlStructureHolder.sa.OleDBConn;
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                    }
                    else
                        conn = null;
                }
                return conn;
            }
            catch
            {
                return null;
            }
        }

        public static bool DisConnect()
        {
            if (SqlStructureHolder.sa.DisConnectDatabase())
            {
                if (SqlStructureHolder.sa.DisConnectSMOServer())
                {
                    return true;
                }
            }
            return false;
        }

        public static TreeView LoadCategoryData(string tableName)
        {
            try
            {
                TreeView treeView = new TreeView();
                DataTable dt = new DataTable();

                SqlConnection SqlConnection1 = GetSqlConncetion();
                if (SqlConnection1.State != ConnectionState.Open)
                    return null;
                SqlCommand thisCommand = SqlConnection1.CreateCommand();
                thisCommand.CommandText =
                     "SELECT * from [" + tableName + "]";
                SqlDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);

                //DocumentController.GetInstance().FillCategories(dt);
                Stack<TreeNode> stack = new Stack<TreeNode>();
                treeView.BeginUpdate();

                DataRow[] cateRow1 = dt.Select("ParentID = 0");

                if (cateRow1.Length != 1)
                    return null;
                //add rootNode
                TreeNode rootNode = new TreeNode(((int)(cateRow1[0][0])).ToString() + "-" + ((string)cateRow1[0][1]));
                treeView.Nodes.Add(rootNode);
                rootNode.Tag = cateRow1[0][0];
                stack.Push(rootNode);

                while (stack.Count > 0)//添加一级分类下面的子分类
                {
                    TreeNode parentNode = (TreeNode)stack.Pop();
                    //int ID = (int)parentNode.Tag;

                    foreach (DataRow cateRow in dt.Select("ParentId = " + parentNode.Tag.ToString()))
                    {
                        TreeNode childNode = new TreeNode((string)cateRow[1]);
                        childNode.Tag = cateRow[0];
                        parentNode.Nodes.Add(childNode);
                        stack.Push(childNode);
                    }
                }
                treeView.EndUpdate();
                thisReader.Close();
                thisCommand.Dispose();
                return treeView;
            }
            catch
            {
                return null;
            }
        }

        public static string GetDatabaseDescription(string dbName)
        {
            return SqlStructureHolder.sa.GetDatabaseDescription(dbName);
        }

        public static string GetDatabaseExtendedPropertie(string dbName, string propertyName)
        {
            return SqlStructureHolder.sa.GetDatabaseExtendedPropertie(dbName, propertyName);
        }

        public static string GetTableDescription(string tableName)
        {
            return SqlStructureHolder.sa.GetTableDescription(tableName);
        }

        public static string GetTableExtendedPropertie(string tableName,string propertyName)
        {
            return SqlStructureHolder.sa.GetTableExtendedPropertie(tableName, propertyName);
        }

        public static void GetDatabaseTables(TreeView tree, TreeNode tablesNode, ImageList imageList, int tableImageIndex)
        {
            SqlStructureHolder.sa.GetDatabaseTables(tree, tablesNode, imageList, tableImageIndex);
        }

        public static void GetDatabaseTables(TreeView tree, TreeNode tablesNode, string strDBName, ImageList imageList, int tableImageIndex)
        {
            SqlStructureHolder.sa.GetDatabaseTables(tree, tablesNode, strDBName, imageList, tableImageIndex);
        }

        public static void GetDatabaseTables(TreeNode tablesNode, int tableImageIndex)
        {
            if (SqlStructureHolder.DatabaseName != string.Empty)
                SqlStructureHolder.sa.GetDatabaseTables(SqlStructureHolder.DatabaseName, tablesNode, tableImageIndex);
        }

        // 查询所有表
        public static List<string> GetDatabaseTables()
        {
            return SqlStructureHolder.sa.GetDatabaseTables();
        }

        public static Microsoft.SqlServer.Management.Smo.Table GetDatabaseTableObj(string tableName)
        {
            return SqlStructureHolder.sa.GetDatabaseTableObj(tableName);
        }

        /// <summary>
        /// 根据类型查询所有表
        /// </summary>
        /// <param name="tableType"></param>
        /// <returns></returns>
        public static List<string> GetDatabaseTablesByType(string tableType)
        {
            return SqlStructureHolder.sa.GetDatabaseTablesByType(tableType);
        }

        public static bool IsTableExisted(string tableName)
        {
            if (SqlStructureHolder.DatabaseName != string.Empty && tableName != string.Empty)
            {
                return SqlStructureHolder.sa.IsTableExisted(tableName);
            }
            return false;
        }

        public static string GetRelationTableName(string tableName)
        {
            List<Microsoft.SqlServer.Management.Smo.Column> cols = SqlStructureHolder.sa.GetTableColumns(tableName);
            foreach (Microsoft.SqlServer.Management.Smo.Column col in cols)
            {
                if (col.InPrimaryKey /*&& col.DataType == Microsoft.SqlServer.Management.Smo.DataType.UniqueIdentifier*/)
                {
                    DataTable tTable = col.EnumForeignKeys();
                    if (tTable.Rows.Count > 0)
                    
                        return (string)tTable.Rows[0].ItemArray[1];
                }
            }
            return null;
        }

        public static List<string> GetRelationTableNames(string tableName)
        {
            List<Microsoft.SqlServer.Management.Smo.Column> cols = SqlStructureHolder.sa.GetTableColumns(tableName);
            foreach (Microsoft.SqlServer.Management.Smo.Column col in cols)
            {
                if (col.InPrimaryKey /*&& col.DataType == Microsoft.SqlServer.Management.Smo.DataType.UniqueIdentifier*/)
                {
                    List<string> ls = new List<string>();
                    DataTable tTable = col.EnumForeignKeys();
                    foreach (DataRow dr in tTable.Rows)
                    {
                        ls.Add((string)(dr.ItemArray[1]));
                    }
                    return ls;
                }
            }
            return null;
        }

        public static string GetTableColumnDescription(string tableName,string columnName)
        {
            return SqlStructureHolder.sa.GetTableColumnDescription(tableName, columnName);
        }

        public static string GetTableExtendedPropertie(string tableName, string columnName, string propertyName)
        {
            return SqlStructureHolder.sa.GetTableColumnExtendedPropertie(tableName, columnName, propertyName);
        }

        public static void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex)
        {
            SqlStructureHolder.sa.GetTableColumns(tree, tableNode, imageList, columnImageIndex);
        }

        public static void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            SqlStructureHolder.sa.GetTableColumns(tree, tableNode, imageList, columnImageIndex, pkeyImageIndex, fkeyImageIndex, pfkeyImageIndex);
        }

        #region Alter sth of db

        public static bool RenameTableName(string oldname, string newname)
        {
            return SqlStructureHolder.sa.RenameTableName(oldname, newname);
        }

        public static bool RenameTableColumnName(string tablename, string oldname, string newname)
        {
            return SqlStructureHolder.sa.RenameTableColumnName(tablename, oldname, newname);
        }

        public static bool DeleteTable(string tablename)
        {
            return SqlStructureHolder.sa.DeleteTable(tablename);
        }

        public static bool DeleteTableColumn(string tablename, string columnname)
        {
            return SqlStructureHolder.sa.DeleteTableColumn(tablename, columnname);
        }

        public static PMSDBTableProp GetPMSDBTableProp(string tablename)
        {
            return SqlStructureHolder.sa.GetPMSDBTableProp(tablename);
        }

        #endregion
    }

    public class OracleStructureHolder
    {
        public static OracleAssistance sa = new OracleAssistance();
        private static string _serverInstance = string.Empty;
        private static string _databaseName = string.Empty;
        private static string _userName = string.Empty;
        private static string _password = string.Empty;
        private static string _portID = string.Empty;
        private static string _connectAs = string.Empty;
        private static string _Owner = string.Empty;
        private static bool _bDirect = false;
        public static bool _bConnected = false;
        public static Dictionary<string, List<PMSDBFieldProp>> tableColumnInfo = new Dictionary<string, List<PMSDBFieldProp>>();
        public static List<string> tableList = new List<string>();
        //外键列表
        public static Dictionary<TableField, TableField> foreignKeyInfo = new Dictionary<TableField, TableField>();
        //表内树形结构列表
        public static Dictionary<string, TreeView> relationInfo = new Dictionary<string, TreeView>();
        #region property

        public static string OracleServerInstance
        {
            get { return _serverInstance; }
            set { _serverInstance = value; }
        }
        public static string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }
        public static string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public static string PortID
        {
            get { return _portID; }
            set { _portID = value; }
        }

        public static bool Direct
        {
            get { return _bDirect; }
            set { _bDirect = value; }
        }

        public static string ConnectAs
        {
            get { return _connectAs; }
            set { _connectAs = value; }
        }

        public static string Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        public static string OwnerDot
        {
            get { return _Owner + "."; }
        }

        public static string ConnectString
        {
            get { return sa.ConnectionString; }
            set { sa.ConnectionString = value; }
        }

        #endregion

        public static bool bReconnected = false;

        public void InitOracleStructure(string instance, string database, string userName, string password, bool direct, string portID, string connectAs, string Owner)
        {
            OracleStructure.InitOracleStructure(instance, database, userName, password, direct, portID, connectAs, Owner);
        }

        public OracleConnection GetOracleConncetion()
        {
            return OracleStructure.GetOracleConncetion();
        }
    }

    public class OracleStructure
    {
        //private static OracleAssistance sa = new OracleAssistance();
        //private static string _serverInstance = string.Empty;
        //private static string _databaseName = string.Empty;
        //private static string _userName = string.Empty;
        //private static string _password = string.Empty;
        //private static string _portID = string.Empty;
        //private static string _connectAs = string.Empty;
        //private static string _Owner = string.Empty;
        //private static bool _bDirect = false;
        //private static bool _bConnected = false;
        //public static Dictionary<string, List<PMSDBFieldProp>> tableColumnInfo = new Dictionary<string, List<PMSDBFieldProp>>();
        //public static List<string> tableList = new List<string>();
        ////外键列表
        //public static Dictionary<TableField, TableField> foreignKeyInfo = new Dictionary<TableField, TableField>();
        ////表内树形结构列表
        //public static Dictionary<string, TreeView> relationInfo = new Dictionary<string, TreeView>();
        //#region property

        //public static string OracleServerInstance
        //{
        //    get { return _serverInstance; }
        //    set { _serverInstance = value; }
        //}
        //public static string DatabaseName
        //{
        //    get { return _databaseName; }
        //    set { _databaseName = value; }
        //}
        //public static string UserName
        //{
        //    get { return _userName; }
        //    set { _userName = value; }
        //}
        //public static string Password
        //{
        //    get { return _password; }
        //    set { _password = value; }
        //}
        //public static string PortID
        //{
        //    get { return _portID; }
        //    set { _portID = value; }
        //}

        //public static bool Direct
        //{
        //    get { return _bDirect; }
        //    set { _bDirect = value; }
        //}

        //public static string ConnectAs
        //{
        //    get { return _connectAs; }
        //    set { _connectAs = value; }
        //}

        //public static string Owner
        //{
        //    get { return _Owner; }
        //    set { _Owner = value; }
        //}

        //public static string OwnerDot
        //{
        //    get { return _Owner + "."; }
        //}

        //public static string ConnectString
        //{
        //    get { return sa.ConnectionString; }
        //    set { sa.ConnectionString = value; }
        //}

        //#endregion

        //private static bool bReconnected = false;

        //public OracleStructure(string instance, string database, string userName, string password, bool direct, string portID, string connectAs, string Owner)
        //{
        //    bReconnected = false;
        //    if (string.Compare(_serverInstance, instance) != 0 || string.Compare(_databaseName, database) != 0 || string.Compare(_portID, portID) != 0 || direct != _bDirect || string.Compare(connectAs , _connectAs) != 0)
        //        bReconnected = true;
        //    sa.DatabaseName = _databaseName = database;
        //    sa.Password = _password = password;
        //    sa.OracleServerInstance = _serverInstance = instance;
        //    sa.UserName = _userName = userName;
        //    sa.Direct = _bDirect = direct;
        //    sa.PortID = _portID = portID;
        //    sa.ConnectAs = _connectAs = connectAs;
        //    sa.Owner = _Owner = Owner;
        //}

        public static void InitOracleStructure(string instance, string database, string userName, string password, bool direct, string portID, string connectAs, string Owner)
        {
            OracleStructureHolder.bReconnected = false;
            if (string.Compare(OracleStructureHolder.OracleServerInstance, instance) != 0 || string.Compare(OracleStructureHolder.DatabaseName, database) != 0 || string.Compare(OracleStructureHolder.PortID, portID) != 0 || direct != OracleStructureHolder.Direct || string.Compare(connectAs, OracleStructureHolder.ConnectAs) != 0)
                OracleStructureHolder.bReconnected = true;
            OracleStructureHolder.sa.DatabaseName = OracleStructureHolder.DatabaseName = database;
            OracleStructureHolder.sa.Password = OracleStructureHolder.Password = password;
            OracleStructureHolder.sa.OracleServerInstance = OracleStructureHolder.OracleServerInstance = instance;
            OracleStructureHolder.sa.UserName = OracleStructureHolder.UserName = userName;
            OracleStructureHolder.sa.Direct = OracleStructureHolder.Direct = direct;
            OracleStructureHolder.sa.PortID = OracleStructureHolder.PortID = portID;
            OracleStructureHolder.sa.ConnectAs = OracleStructureHolder.ConnectAs = connectAs;
            OracleStructureHolder.sa.Owner = OracleStructureHolder.Owner = Owner;
        }

        /// <summary>
        /// 初始化表名列表,表字段列表和外键列表
        /// </summary>
        public static void InitialStructure()
        {
            try
            {
                bool bAll = true;
                if (OracleStructureHolder.sa.ConnectDatabase())
                {
                    OracleStructureHolder.tableList = OracleStructureHolder.sa.GetDatabaseOwnerTables(OracleStructureHolder.Owner);

                    if (OracleStructureHolder.tableList == null)
                    {
                        string strText = string.Format(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMSSqlAssistance_DBNULL"), OracleStructureHolder.DatabaseName);
                        string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Warnning");
                        MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (bAll)
                    {
                        PMSDBTablePFKeyRelationCollection PFKeyRelationCollection = OracleStructureHolder.sa.GetTablePFKeyRelation();

                        foreach (PMSDBTablePFKeyRelation relation in PFKeyRelationCollection)
                        {
                            TableField tf = new TableField();
                            tf.tableName = relation.PrimaryTable;
                            tf.fieldName = relation.PrimaryColumn;
                            TableField tp = new TableField();
                            tp.tableName = relation.ForeignTable;
                            tp.fieldName = relation.ForengnColumn;
                            if (!OracleStructureHolder.foreignKeyInfo.ContainsKey(tf))
                                OracleStructureHolder.foreignKeyInfo.Add(tf, tp);
                        }
                    }

                    foreach (string strTable in OracleStructureHolder.tableList)
                    {
                        List<PMSDBFieldProp> propList = OracleStructureHolder.sa.GetTableColumns(strTable);
                        
                        //表与字段列表对应
                        if (!OracleStructureHolder.tableColumnInfo.ContainsKey(strTable))
                            OracleStructureHolder.tableColumnInfo.Add(strTable, propList);

                        //if (bAll)
                        //{
                        //    //生成外键对应主键列表
                        //    foreach (Microsoft.SqlServer.Management.Smo.Column col in cols)
                        //    {
                        //        DataTable tTable = col.EnumForeignKeys();
                        //        for (int i = 0; i < tTable.Rows.Count; i++)
                        //        {
                        //            TableField tf = new TableField();
                        //            tf.tableName = (string)tTable.Rows[i].ItemArray[1];
                        //            tf.fieldName = col.Name;
                        //            if (!foreignKeyInfo.ContainsKey(tf))
                        //                foreignKeyInfo.Add(tf, strTable);
                        //        }
                        //    }
                        //}
                    }
                    OracleStructureHolder._bConnected = true;
                }
                else
                    OracleStructureHolder._bConnected = false;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获得表字段信息
        /// </summary>
        /// <param name="strTable">表名</param>
        /// <returns>字段列表</returns>
        public static List<PMSDBFieldProp> getColumnInfo(string strTable)
        {
            try
            {
                if (OracleStructureHolder.tableColumnInfo.ContainsKey(strTable))
                {
                    return OracleStructureHolder.tableColumnInfo[strTable];
                }
                else
                {
                    List<PMSDBFieldProp> propList = OracleStructureHolder.sa.GetTableColumns(strTable);
                    if (!OracleStructureHolder.tableColumnInfo.ContainsKey(strTable))
                        OracleStructureHolder.tableColumnInfo.Add(strTable, propList);
                    return propList;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得主表表名
        /// </summary>
        /// <param name="tableField">外键表名和字段名结构</param>
        /// <returns>主表表名</returns>
        public static TableField getMainKeyTable(TableField tableField)
        {
            try
            {
                if (OracleStructureHolder.foreignKeyInfo.ContainsKey(tableField))
                {
                    return OracleStructureHolder.foreignKeyInfo[tableField];
                }
                else
                {
                    return new TableField();
                }
            }
            catch
            {
                return new TableField();
            }
        }

        public static OracleConnection GetOracleConncetion(bool wantNewTempConnection = false)
        {
            try
            {
                if (wantNewTempConnection)
                    return OracleStructureHolder.sa.GetNewConnection();
                OracleConnection conn = OracleStructureHolder.sa.OracleConnection;
                if (conn == null || !(conn.State == ConnectionState.Open) || OracleStructureHolder.bReconnected)
                {
                    if (OracleStructureHolder.sa.ConnectDatabase())
                    {
                        conn = OracleStructureHolder.sa.OracleConnection;
                        if (conn.State != ConnectionState.Open || OracleStructureHolder.bReconnected)
                        {
                            conn.Close();
                            conn.Open();
                        }
                    }
                    else
                        conn = null;
                }
                return conn;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string strText = ex.Message;
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Fail");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public static OracleConnection GetOracleConncetion(string connectstring)
        {
            try
            {
                OracleConnection conn = OracleStructureHolder.sa.OracleConnection;
                if (conn == null || !(conn.State == ConnectionState.Open) || OracleStructureHolder.bReconnected)
                {
                    if (OracleStructureHolder.sa.ConnectDatabase(connectstring))
                    {
                        conn = OracleStructureHolder.sa.OracleConnection;
                        if (conn.State != ConnectionState.Open || OracleStructureHolder.bReconnected)
                        {
                            conn.Close();
                            conn.Open();
                        }
                    }
                    else
                        conn = null;
                }
                return conn;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string strText = ex.Message;
                string strCaption = GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_Fail");
                MessageBox.Show(strText, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public static IDbConnection GetOleDBConncetion()
        {
            try
            {
                OleDbConnection conn = OracleStructureHolder.sa.OleDBConn;
                if (conn == null || !(conn.State == ConnectionState.Open) || OracleStructureHolder.bReconnected)
                {
                    if (OracleStructureHolder.sa.ConnectOleDB())
                    {
                        conn = OracleStructureHolder.sa.OleDBConn;
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                    }
                    else
                        conn = null;
                }
                return conn;
            }
            catch
            {
                return null;
            }
        }

        public static bool DisConnect()
        {
            if (OracleStructureHolder.sa.DisConnectDatabase())
            {
                return true;
            }
            return false;
        }

        public static TreeView LoadCategoryData(string tableName)
        {
            try
            {
                TreeView treeView = new TreeView();
                DataTable dt = new DataTable();

                OracleConnection OracleConnection1 = GetOracleConncetion();
                if (OracleConnection1.State != ConnectionState.Open)
                    return null;
                OracleCommand thisCommand = OracleConnection1.CreateCommand();
                thisCommand.CommandText =
                     "SELECT * from [" + tableName + "]";
                OracleDataReader thisReader = thisCommand.ExecuteReader();
                dt.Load(thisReader);

                //DocumentController.GetInstance().FillCategories(dt);
                Stack<TreeNode> stack = new Stack<TreeNode>();
                treeView.BeginUpdate();

                DataRow[] cateRow1 = dt.Select("ParentID = 0");

                if (cateRow1.Length != 1)
                    return null;
                //add rootNode
                TreeNode rootNode = new TreeNode(((int)(cateRow1[0][0])).ToString() + "-" + ((string)cateRow1[0][1]));
                treeView.Nodes.Add(rootNode);
                rootNode.Tag = cateRow1[0][0];
                stack.Push(rootNode);

                while (stack.Count > 0)//添加一级分类下面的子分类
                {
                    TreeNode parentNode = (TreeNode)stack.Pop();
                    //int ID = (int)parentNode.Tag;

                    foreach (DataRow cateRow in dt.Select("ParentId = " + parentNode.Tag.ToString()))
                    {
                        TreeNode childNode = new TreeNode((string)cateRow[1]);
                        childNode.Tag = cateRow[0];
                        parentNode.Nodes.Add(childNode);
                        stack.Push(childNode);
                    }
                }
                treeView.EndUpdate();
                return treeView;
            }
            catch
            {
                return null;
            }
        }

        //public static string GetDatabaseDescription(string dbName)
        //{
        //    return sa.GetDatabaseDescription(dbName);
        //}

        //public static string GetDatabaseExtendedPropertie(string dbName, string propertyName)
        //{
        //    return sa.GetDatabaseExtendedPropertie(dbName, propertyName);
        //}

        //public static string GetTableDescription(string tableName)
        //{
        //    return sa.GetTableDescription(tableName);
        //}

        //public static string GetTableExtendedPropertie(string tableName, string propertyName)
        //{
        //    return sa.GetTableExtendedPropertie(tableName, propertyName);
        //}

        public static void GetDatabaseTables(TreeView tree, TreeNode tablesNode, ImageList imageList, int tableImageIndex)
        {
            OracleStructureHolder.sa.GetDatabaseTables(tree, tablesNode, imageList, tableImageIndex);
        }

        public static void GetDatabaseTables(TreeView tree, TreeNode tablesNode, string strDBName, ImageList imageList, int tableImageIndex)
        {
            OracleStructureHolder.sa.GetDatabaseTables(tree, tablesNode, strDBName, imageList, tableImageIndex);
        }

        public static void GetDatabaseTables(TreeNode tablesNode, int tableImageIndex)
        {
            if (OracleStructureHolder.DatabaseName != string.Empty)
                OracleStructureHolder.sa.GetDatabaseTables(tablesNode, tableImageIndex);
        }

        // 查询所有表
        public static List<string> GetDatabaseTables()
        {
            return OracleStructureHolder.sa.GetDatabaseOwnerTables();
        }

        public static Dictionary<string, PMSRefDBTableProp> GetDatabaseTables(PMSRefDBConnection rc)
        {
            return OracleStructureHolder.sa.GetDatabaseTables(rc);
        }

        public static Dictionary<string, PMSRefDBViewProp> GetDatabaseViews(PMSRefDBConnection rc)
        {
            return OracleStructureHolder.sa.GetDatabaseViews(rc);
        }

        /// <summary>
        /// 根据类型查询所有表
        /// </summary>
        /// <param name="tableType"></param>
        /// <returns></returns>
        //public static List<string> GetDatabaseTablesByType(string tableType)
        //{
        //    return sa.GetDatabaseTablesByType(tableType);
        //}

        public static bool IsTableExisted(string tableName)
        {
            if (OracleStructureHolder.DatabaseName != string.Empty && tableName != string.Empty)
            {
                return OracleStructureHolder.sa.IsTableExisted(tableName);
            }
            return false;
        }

        public static string GetRelationTableName(string tableName)
        {
            PMSDBTablePFKeyRelationCollection cc = OracleStructureHolder.sa.GetTablePFKeyRelation(tableName);
            if (cc.Count > 0)
            {
                foreach (PMSDBTablePFKeyRelation cl in cc)
                {
                    return cl.ForeignTable;
                }
            }
            return null;
        }

        public static List<string> GetRelationTableNames(string tableName)
        {
            PMSDBTablePFKeyRelationCollection cc = OracleStructureHolder.sa.GetTablePFKeyRelation(tableName);
            List<string> list = new List<string>();
            if (cc.Count > 0)
            {
                foreach (PMSDBTablePFKeyRelation cl in cc)
                {
                    list.Add(cl.ForeignTable);
                }
                return list;
            }

            return null;
        }

        //public static string GetTableColumnDescription(string tableName, string columnName)
        //{
        //    return sa.GetTableColumnDescription(tableName, columnName);
        //}

        //public static string GetTableExtendedPropertie(string tableName, string columnName, string propertyName)
        //{
        //    return sa.GetTableColumnExtendedPropertie(tableName, columnName, propertyName);
        //}

        public static void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex)
        {
            OracleStructureHolder.sa.GetTableColumns(tree, tableNode, imageList, columnImageIndex);
        }

        public static void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            OracleStructureHolder.sa.GetTableColumns(tree, tableNode, imageList, columnImageIndex, pkeyImageIndex, fkeyImageIndex, pfkeyImageIndex);
        }

        public static Dictionary<string, PMSRefDBFieldProp> GetTableColumns(string tableName, PMSRefDBConnection rc)
        {
            return OracleStructureHolder.sa.GetRefTableColumns(tableName, rc);
        }

        #region Alter sth of db

        public static bool RenameTableName(string oldname, string newname)
        {
            return OracleStructureHolder.sa.RenameTableName(oldname, newname);
        }

        public static bool RenameTableColumnName(string tablename, string oldname, string newname)
        {
            return OracleStructureHolder.sa.RenameTableColumnName(tablename, oldname, newname);
        }

        public static bool DeleteTable(string tablename)
        {
            return OracleStructureHolder.sa.DeleteTable(tablename);
        }

        public static bool DeleteTableColumn(string tablename, string columnname)
        {
            return OracleStructureHolder.sa.DeleteTableColumn(tablename, columnname);
        }

        public static PMSDBTableProp GetPMSDBTableProp(string tablename)
        {
            return OracleStructureHolder.sa.GetPMSDBTableProp(tablename);
        }

        #endregion
    }

    public class AccessStructure
    {
        private static string _dbPath = string.Empty;
        private static string _userName = string.Empty;
        private static string _password = string.Empty;
        private static OleDbConnection _OleDbConn = null;

        private static bool bReconnected = false;

        public AccessStructure(string dbPath, string userName, string password)
        {
            bReconnected = false;
            if (string.Compare(_dbPath, dbPath) != 0)
                bReconnected = true;
            _dbPath = dbPath;
            _userName = userName;
            _password = password;
        }

        public static OleDbConnection OleDbConn
        {
            get { return _OleDbConn; }
            set { _OleDbConn = value; }
        }

        public static OleDbConnection GetOleDbConncetion()
        {
            return _OleDbConn;
        }

        public static bool ConnectDB()
        {
            try
            {
                if (_OleDbConn == null || bReconnected)
                {
                    // 利用 OleDbConnectionStringBuilder 对象来构建
                    // 连接字符串。
                    OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                    connectStringBuilder.DataSource = _dbPath;
                    connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                    _OleDbConn = new OleDbConnection(connectStringBuilder.ConnectionString);
                    //{
                        //DataSet ds=new DataSet();
                        //OleDbCommand cmdLiming = new OleDbCommand("SELECT * FROM 学生", _OleDbConn);
                        _OleDbConn.Open();
                    //}
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DisConnect()
        {
            try
            {
                if (_OleDbConn != null)
                {
                    if (_OleDbConn.State == ConnectionState.Open)
                    {
                        _OleDbConn.Close();
                        return true;
                    }
                }
            }
            catch (System.Exception e)
            {
            	return false;
            }
            return true;
        }

        public static void GetDatabaseTables(TreeView tree, TreeNode tablesNode, ImageList imageList, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of strings to hold name of tables
                List<string> tables = new List<string>();

                DataTable dt = _OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow dr in dt.Rows)
                {
                    tables.Add(dr[2].ToString());
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.ImageIndex = tableImageIndex;
                    tableNode.SelectedImageIndex = tableImageIndex;
                    PMSRefDBTableProp refTableProp = new PMSRefDBTableProp();
                    refTableProp.StrTableName = str;
                    refTableProp.PMSRefDBConnection = (PMSRefDBConnection)(tablesNode.Tag);
                    tableNode.Tag = refTableProp;
                    tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Dictionary<string,PMSRefDBTableProp> GetDatabaseTables(PMSRefDBConnection rc)
        {
            try
            {
                // create a list of strings to hold name of tables
                List<string> tables = new List<string>();

                DataTable dt = _OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow dr in dt.Rows)
                {
                    tables.Add(dr[2].ToString());
                }

                Dictionary<string, PMSRefDBTableProp> dic = new Dictionary<string, PMSRefDBTableProp>();

                foreach (string str in tables)
                {
                    PMSRefDBTableProp refTableProp = new PMSRefDBTableProp();
                    refTableProp.StrTableName = str;
                    refTableProp.PMSRefDBConnection = rc;

                    dic.Add(str, refTableProp);
                }
                return dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                string TableName = "";
                if (tableNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    TableName = tableNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    TableName = tableNode.Text;

                string sSql = string.Empty;

                
                DataTable dtColumnsInfo = _OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, TableName, null });

                foreach (DataRow dr in dtColumnsInfo.Rows)
                {
                    string columnName = dr[3].ToString();
                    sSql = "SELECT [" + columnName + "] FROM [" + TableName + "]";

                    OleDbCommand cmd = new OleDbCommand(sSql, _OleDbConn);
                    //_OleDbConn.Open();
                    
                    OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                    DataTable schemaTable = rdr.GetSchemaTable();
                    StringBuilder sb = new StringBuilder();


                    var selectedRows = from r in schemaTable.AsEnumerable() orderby r.Field<int>("ColumnOrdinal") select r;

                    EnumerableRowCollection<DataRow> roomEnumer = selectedRows;
                    //////////////////////////////////////////////////////////////////////////
                    /// myField DataRow Description
                    ///	[0]	{ColumnName}	
                    ///	[1]	{ColumnOrdinal}	
                    ///	[2]	{ColumnSize}	
                    ///	[3]	{NumericPrecision}	
                    ///	[4]	{NumericScale}	
                    ///	[5]	{DataType}	
                    ///	[6]	{ProviderType}	
                    ///	[7]	{IsLong}	
                    ///	[8]	{AllowDBNull}	
                    ///	[9]	{IsReadOnly}	
                    ///	[10]	{IsRowVersion}	
                    ///	[11]	{IsUnique}	
                    ///	[12]	{IsKey}	
                    ///	[13]	{IsAutoIncrement}	
                    ///	[14]	{BaseSchemaName}	
                    ///	[15]	{BaseCatalogName}	
                    ///	[16]	{BaseTableName}	
                    ///	[17]	{BaseColumnName}	
                    //////////////////////////////////////////////////////////////////////////
                    foreach (DataRow myField in roomEnumer)
                    {
                        // create a node to hold column name (level 2)
                        // then add dataType to it's text
                        TreeNode colNode = new TreeNode(myField["ColumnName"].ToString());
                        string strpfKey = "";
                        bool bPKey = false;
                        //if (col.InPrimaryKey && col.IsForeignKey)
                        //{
                        //    colNode.ImageIndex = pfkeyImageIndex;
                        //    colNode.SelectedImageIndex = pfkeyImageIndex;
                        //    strpfKey = "PK, FK, ";
                        //}
                        //else
                        if (myField["IsKey"].ToString().Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            colNode.ImageIndex = pkeyImageIndex;
                            colNode.SelectedImageIndex = pkeyImageIndex;
                            strpfKey = "PK, ";
                            bPKey = true;
                        }
                        //else if (col.IsForeignKey)
                        //{
                        //    colNode.ImageIndex = fkeyImageIndex;
                        //    colNode.SelectedImageIndex = fkeyImageIndex;
                        //    strpfKey = "FK, ";
                        //}
                        else
                        {
                            colNode.ImageIndex = columnImageIndex;
                            colNode.SelectedImageIndex = columnImageIndex;
                        }

                        string strSize = "";
                        int size = 0;
                        if (myField["DataType"].ToString().Contains("char"))
                        {
                            size = Int32.Parse(myField["ColumnSize"].ToString());
                            strSize = string.Format("({0})", myField["ColumnSize"].ToString());
                        }
                        string strNullable;
                        bool bNullable = true;
                        if (myField["AllowDBNull"].ToString().Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            strNullable = "null";
                            bNullable = true;
                        }
                        else
                        {
                            strNullable = "not null";
                            bNullable = false;
                        }

                        string strDataType = string.Empty;
                        strDataType = myField["DataType"].ToString();
                        //if(strDataType == "System.Decimal")
                        //{
                        //    strDataType = "System.Double";
                        //}
                        colNode.Text = string.Format("{0} ({1}{2}{3}, {4})", colNode.Text, strpfKey, strDataType, strSize, strNullable);

                        PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                        RefDBFieldProp.PMSRefDBConnection = (PMSRefDBConnection)(tableNode.Parent.Tag);
                        RefDBFieldProp.StrTableName = TableName;
                        RefDBFieldProp.StrFieldName = columnName;
                        RefDBFieldProp.StrFieldType = strDataType;
                        RefDBFieldProp.DbType = RefDBInfo.GetDbType(strDataType);
                        RefDBFieldProp.Type = RefDBInfo.GetCSharpType(strDataType);
                        RefDBFieldProp.IFildID = Int32.Parse(myField["ColumnOrdinal"].ToString());
                        RefDBFieldProp.IFieldLength = size;
                        //RefDBFieldProp.StrFieldDefault = col.Default;
                        //RefDBFieldProp.StrFieldDescription = GetTableColumnDescription(TableName, col.Name);
                        //RefDBFieldProp.BFieldIdentity = col.Identity;
                        RefDBFieldProp.BFieldNullable = bNullable;
                        RefDBFieldProp.BFieldPrimaryKey = bPKey;
                        colNode.Tag = RefDBFieldProp;

                        // add tableNode to tablesNode
                        tableNode.Nodes.Add(colNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Dictionary<string, PMSRefDBFieldProp> GetTableColumns(string tableName, PMSRefDBConnection rc)
        {
            try
            {
                string sSql = string.Empty;

                DataTable dtColumnsInfo = _OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, null });
                Dictionary<string, PMSRefDBFieldProp> dic = new Dictionary<string, PMSRefDBFieldProp>();

                foreach (DataRow dr in dtColumnsInfo.Rows)
                {
                    string columnName = dr[3].ToString();
                    sSql = "SELECT [" + columnName + "] FROM [" + tableName + "]";

                    OleDbCommand cmd = new OleDbCommand(sSql, _OleDbConn);
                    //_OleDbConn.Open();

                    OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                    DataTable schemaTable = rdr.GetSchemaTable();
                    StringBuilder sb = new StringBuilder();


                    var selectedRows = from r in schemaTable.AsEnumerable() orderby r.Field<int>("ColumnOrdinal") select r;

                    EnumerableRowCollection<DataRow> roomEnumer = selectedRows;
                    //////////////////////////////////////////////////////////////////////////
                    /// myField DataRow Description
                    ///	[0]	{ColumnName}	
                    ///	[1]	{ColumnOrdinal}	
                    ///	[2]	{ColumnSize}	
                    ///	[3]	{NumericPrecision}	
                    ///	[4]	{NumericScale}	
                    ///	[5]	{DataType}	
                    ///	[6]	{ProviderType}	
                    ///	[7]	{IsLong}	
                    ///	[8]	{AllowDBNull}	
                    ///	[9]	{IsReadOnly}	
                    ///	[10]	{IsRowVersion}	
                    ///	[11]	{IsUnique}	
                    ///	[12]	{IsKey}	
                    ///	[13]	{IsAutoIncrement}	
                    ///	[14]	{BaseSchemaName}	
                    ///	[15]	{BaseCatalogName}	
                    ///	[16]	{BaseTableName}	
                    ///	[17]	{BaseColumnName}	
                    //////////////////////////////////////////////////////////////////////////
                    foreach (DataRow myField in roomEnumer)
                    {
                        // create a node to hold column name (level 2)
                        // then add dataType to it's text
                        string strpfKey = "";
                        bool bPKey = false;
                        //if (col.InPrimaryKey && col.IsForeignKey)
                        //{
                        //    colNode.ImageIndex = pfkeyImageIndex;
                        //    colNode.SelectedImageIndex = pfkeyImageIndex;
                        //    strpfKey = "PK, FK, ";
                        //}
                        //else
                        if (myField["IsKey"].ToString().Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            strpfKey = "PK, ";
                            bPKey = true;
                        }
                        //else if (col.IsForeignKey)
                        //{
                        //    colNode.ImageIndex = fkeyImageIndex;
                        //    colNode.SelectedImageIndex = fkeyImageIndex;
                        //    strpfKey = "FK, ";
                        //}
                        else
                        {
                            
                        }

                        string strSize = "";
                        int size = 0;
                        if (myField["DataType"].ToString().Contains("char"))
                        {
                            size = Int32.Parse(myField["ColumnSize"].ToString());
                            strSize = string.Format("({0})", myField["ColumnSize"].ToString());
                        }
                        string strNullable = null;
                        bool bNullable = true;
                        if (myField["AllowDBNull"].ToString().Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            strNullable = "null";
                            bNullable = true;
                        }
                        else
                        {
                            strNullable = "not null";
                            bNullable = false;
                        }

                        string strDataType = string.Empty;
                        strDataType = myField["DataType"].ToString();
                        //if(strDataType == "System.Decimal")
                        //{
                        //    strDataType = "System.Double";
                        //}

                        PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                        RefDBFieldProp.PMSRefDBConnection = rc;
                        RefDBFieldProp.StrTableName = tableName;
                        RefDBFieldProp.StrFieldName = columnName;
                        RefDBFieldProp.StrFieldType = strDataType;
                        RefDBFieldProp.DbType = RefDBInfo.GetDbType(strDataType);
                        RefDBFieldProp.Type = RefDBInfo.GetCSharpType(strDataType);
                        RefDBFieldProp.IFildID = Int32.Parse(myField["ColumnOrdinal"].ToString());
                        RefDBFieldProp.IFieldLength = size;
                        //RefDBFieldProp.StrFieldDefault = col.Default;
                        //RefDBFieldProp.StrFieldDescription = GetTableColumnDescription(TableName, col.Name);
                        //RefDBFieldProp.BFieldIdentity = col.Identity;
                        RefDBFieldProp.BFieldNullable = bNullable;
                        RefDBFieldProp.BFieldPrimaryKey = bPKey;

                        dic.Add(columnName, RefDBFieldProp);
                    }
                }
                return dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        } 
    }

    public class AccessAssistant
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private string _dbPath = string.Empty;
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private OleDbConnection _OleDbConn = null;
        private OleDbCommand _thisOleCommand =null;
        private bool bReconnected = false;

        public AccessAssistant(string dbPath, string userName, string password)
        {
            bReconnected = false;
            if (string.Compare(_dbPath, dbPath) != 0)
                bReconnected = true;
            _dbPath = dbPath;
            _userName = userName;
            _password = password;
        }

        public OleDbConnection OleDbConn
        {
            get { return _OleDbConn; }
            set { _OleDbConn = value; }
        }

        public OleDbConnection GetOleDbConncetion()
        {
            return _OleDbConn;
        }

        public bool ConnectDB()
        {
            try
            {
                if (_OleDbConn == null || bReconnected)
                {
                    // 利用 OleDbConnectionStringBuilder 对象来构建
                    // 连接字符串。
                    OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                    connectStringBuilder.DataSource = _dbPath;
                    connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                    _OleDbConn = new OleDbConnection(connectStringBuilder.ConnectionString);
                    //{
                        //DataSet ds=new DataSet();
                        //OleDbCommand cmdLiming = new OleDbCommand("SELECT * FROM 学生", _OleDbConn);
                        _OleDbConn.Open();
                    //}
                }
                return true;
            }
            catch(Exception ex)
            {
                if(null != logger)
                    logger.Error("ConnectDB error:{0}", ex.Message);
                return false;
            }
        }

        public bool DisConnect()
        {
            try
            {
                if (_OleDbConn != null)
                {
                    if (_OleDbConn.State == ConnectionState.Open)
                    {
                        _OleDbConn.Close();
                        return true;
                    }
                }
            }
            catch (System.Exception e)
            {
            	return false;
            }
            return true;
        }

        private bool TestConnect(OleDbConnection conn = null)
        {
            try
            {
                OleDbConnection OleDbConn = null;
                if (null == conn)
                {
                    if (null == _OleDbConn)
                        return false;
                    OleDbConn = _OleDbConn;
                }
                else
                {
                    OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                    connectStringBuilder.DataSource = _dbPath;
                    connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                    OleDbConn = new OleDbConnection(connectStringBuilder.ConnectionString);
                }
                OleDbConn.Open();
                OleDbCommand OleCommand = OleDbConn.CreateCommand();
                OleCommand.CommandText = "SELECT MSysObjects.Name FROM MsysObjects WHERE 1<>1 ";
                int iret = OleCommand.ExecuteNonQuery();
                _thisOleCommand.Dispose();
                _thisOleCommand = null;
                return true;
            }
            catch 
            {

            }
            return false;
        }

        public DataTable ExecuteCommand(string strcommand)
        {
            try
            {
                if (null == _OleDbConn)
                    return null;
                DataTable dt = new DataTable();
                OleDbConnection DbConnection = _OleDbConn;
                if (DbConnection.State != ConnectionState.Open)
                    return null;
                if (_thisOleCommand == null)
                    _thisOleCommand = DbConnection.CreateCommand();
                _thisOleCommand.CommandText = strcommand;
                OleDbDataReader thisReader = _thisOleCommand.ExecuteReader();
                dt.Load(thisReader);
                if (!thisReader.IsClosed)
                    thisReader.Close();
                _thisOleCommand.Dispose();
                _thisOleCommand = null;
                return dt;
            }
            catch
            {

            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strcommand">sql命令</param>
        /// <returns>
        /// >=0 成功 返回值代表命令所影响的记录数
        /// -1 失败 但是连接OK
        /// -2 失败 连接损坏
        /// </returns>
        public int ExecuteCommandNonQuery(string strcommand)
        {
            try
            {
                if (null == _OleDbConn)
                    return -1;
                DataTable dt = new DataTable();
                OleDbConnection DbConnection = _OleDbConn;
                if (DbConnection.State != ConnectionState.Open)
                    return 0;
                if (_thisOleCommand == null)
                    _thisOleCommand = DbConnection.CreateCommand();
                _thisOleCommand.CommandText = strcommand;
                int iret = _thisOleCommand.ExecuteNonQuery();
                _thisOleCommand.Dispose();
                _thisOleCommand = null;
                if (null != logger)
                    logger.Info("ExecuteCommandNonQuery sql:{0},result = {1}", strcommand, iret);
                return iret;
            }
            catch(Exception ex)
            {
                if (null != logger)
                    logger.Error("ConnectDB error:{0}", ex.Message);
                if (!TestConnect(_OleDbConn))
                {
                    return -2;
                }
            }
            return -1;
        }
    }

    public class OleDBStructure
    {
        private static string _ConnectString = string.Empty;

        private static OleDbConnection _OleDbConn = null;

        private static PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.QueryBuilder _builder = null;

        private static bool bReconnected = false;

        public OleDBStructure(string ConnectString)
        {
            bReconnected = false;
            if (string.Compare(_ConnectString, ConnectString) != 0)
                bReconnected = true;
            _ConnectString = ConnectString;
            // create query builder
            _builder = new PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.QueryBuilder(new PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.OleDbSchema());
        }

        public static OleDbConnection OleDbConn
        {
            get { return _OleDbConn; }
            set { _OleDbConn = value; }
        }

        // for easy access
        public static PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.OleDbSchema Schema
        {
            get { return _builder.Schema; }
        }

        public static OleDbConnection GetOleDbConncetion()
        {
            return _OleDbConn;
        }

        public static bool ConnectDB()
        {
            try
            {
                if (_OleDbConn == null || bReconnected)
                {
                    // 利用 OleDbConnectionStringBuilder 对象来构建
                    // 连接字符串。
                    _OleDbConn = new OleDbConnection(_ConnectString);
                    _OleDbConn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DisConnect()
        {
            try
            {
                if (_OleDbConn != null)
                {
                    if (_OleDbConn.State == ConnectionState.Open)
                    {
                        _OleDbConn.Close();
                        _OleDbConn = null;
                        return true;
                    }
                }
            }
            catch (System.Exception e)
            {
            	return false;
            }
            return true;
        }

        public static void GetDatabaseTables(TreeView tree, TreeNode tablesNode, ImageList imageList, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                // create a list of strings to hold name of tables
                List<string> tables = new List<string>();

                DataTable dt = _OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow dr in dt.Rows)
                {
                    tables.Add(dr[2].ToString());
                }

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.ImageIndex = tableImageIndex;
                    tableNode.SelectedImageIndex = tableImageIndex;
                    PMSRefDBTableProp refTableProp = new PMSRefDBTableProp();
                    refTableProp.StrTableName = str;
                    refTableProp.PMSRefDBConnection = (PMSRefDBConnection)(tablesNode.Tag);
                    tableNode.Tag = refTableProp;
                    tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //try
            //{
            //    // clear current nodes
            //    tablesNode.Nodes.Clear();

            //    // set tree.ImageList to our imageList parameter to use it's imageIndex
            //    tree.ImageList = imageList;

            //    // create a list of strings to hold name of tables
            //    List<string> tables = new List<string>();

            //    //DataTable dt = _OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            //    var ndTables = new TreeNode(GetStringFromPublicResourceClass.GetStringFromPublicResource("DBStructAnalyseNavigateBarControl_Tables"), 9, 9);
            //    var ndViews = new TreeNode(GetStringFromPublicResourceClass.GetStringFromPublicResource("DBStructAnalyseNavigateBarControl_Views"), 9, 9);

            //    // populate using current schema
            //    if (Schema != null)
            //    {
            //        // populate the tree
            //        foreach (DataTable dt in Schema.Tables)
            //        {
            //            // create new node, save table in tag property
            //            var node = new TreeNode(dt.TableName);

            //            PMSRefDBTableProp refTableProp = new PMSRefDBTableProp();

            //            // add new node to appropriate parent
            //            // create a node to hold table name (child node or level 1)
            //            // then add child named 'Objects' to add + mark for table node
            //            switch (PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.OleDbSchema.GetTableType(dt))
            //            {
            //                case PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.TableType.Table:
            //                    ndTables.Nodes.Add(node);
            //                    node.ImageIndex = node.SelectedImageIndex = 1;
            //                    refTableProp.StrTableName = dt.TableName;
            //                    refTableProp.PMSRefDBConnection = (PMSRefDBConnection)(tablesNode.Tag);
            //                    node.Tag = refTableProp;
            //                    node.Nodes.Add("Objects");
            //                    //AddDataColumns(node, dt);
            //                    break;
            //                case PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager.TableType.View:
            //                    ndViews.Nodes.Add(node);
            //                    node.ImageIndex = node.SelectedImageIndex = 10;
            //                    refTableProp.StrTableName = dt.TableName;
            //                    refTableProp.PMSRefDBConnection = (PMSRefDBConnection)(tablesNode.Tag);
            //                    node.Tag = refTableProp;
            //                    node.Nodes.Add("Objects");
            //                    //AddDataColumns(node, dt);
            //                    break;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        public static Dictionary<string, PMSRefDBTableProp> GetDatabaseTables(PMSRefDBConnection rc)
        {
            try
            {
                // create a list of strings to hold name of tables
                List<string> tables = new List<string>();

                DataTable dt = _OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow dr in dt.Rows)
                {
                    tables.Add(dr[2].ToString());
                }

                Dictionary<string, PMSRefDBTableProp> dic = new Dictionary<string, PMSRefDBTableProp>();

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    PMSRefDBTableProp refTableProp = new PMSRefDBTableProp();
                    refTableProp.StrTableName = str;
                    refTableProp.PMSRefDBConnection = rc;

                    dic.Add(str, refTableProp);
                }
                return dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static void GetTableColumns(TreeView tree, TreeNode tableNode, ImageList imageList, int columnImageIndex, int pkeyImageIndex, int fkeyImageIndex, int pfkeyImageIndex)
        {
            try
            {
                // clear current nodes
                tableNode.Nodes.Clear();

                // set tree.ImageList to our imageList parameter to use it's imageIndex
                tree.ImageList = imageList;

                string TableName = "";
                if (tableNode.Text.EndsWith(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding")))
                    TableName = tableNode.Text.TrimEnd(GetStringFromPublicResourceClass.GetStringFromPublicResource("PMS_IsExpanding").ToCharArray());
                else
                    TableName = tableNode.Text;

                string sSql = string.Empty;

                
                DataTable dtColumnsInfo = _OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, TableName, null });

                foreach (DataRow dr in dtColumnsInfo.Rows)
                {
                    string columnName = dr[3].ToString();
                    sSql = "SELECT [" + columnName + "] FROM [" + TableName + "]";

                    OleDbCommand cmd = new OleDbCommand(sSql, _OleDbConn);
                    //_OleDbConn.Open();
                    
                    OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.Default);
                    DataTable schemaTable = rdr.GetSchemaTable();
                    StringBuilder sb = new StringBuilder();


                    var selectedRows = from r in schemaTable.AsEnumerable() orderby r.Field<int>("ColumnOrdinal") select r;

                    EnumerableRowCollection<DataRow> roomEnumer = selectedRows;
                    //////////////////////////////////////////////////////////////////////////
                    /// myField DataRow Description
                    ///	[0]	{ColumnName}	
                    ///	[1]	{ColumnOrdinal}	
                    ///	[2]	{ColumnSize}	
                    ///	[3]	{NumericPrecision}	
                    ///	[4]	{NumericScale}	
                    ///	[5]	{DataType}	
                    ///	[6]	{ProviderType}	
                    ///	[7]	{IsLong}	
                    ///	[8]	{AllowDBNull}	
                    ///	[9]	{IsReadOnly}	
                    ///	[10]	{IsRowVersion}	
                    ///	[11]	{IsUnique}	
                    ///	[12]	{IsKey}	
                    ///	[13]	{IsAutoIncrement}	
                    ///	[14]	{BaseSchemaName}	
                    ///	[15]	{BaseCatalogName}	
                    ///	[16]	{BaseTableName}	
                    ///	[17]	{BaseColumnName}	
                    //////////////////////////////////////////////////////////////////////////
                    foreach (DataRow myField in roomEnumer)
                    {
                        // create a node to hold column name (level 2)
                        // then add dataType to it's text
                        TreeNode colNode = new TreeNode(myField["ColumnName"].ToString());
                        string strpfKey = "";
                        bool bPKey = false;
                        //if (col.InPrimaryKey && col.IsForeignKey)
                        //{
                        //    colNode.ImageIndex = pfkeyImageIndex;
                        //    colNode.SelectedImageIndex = pfkeyImageIndex;
                        //    strpfKey = "PK, FK, ";
                        //}
                        //else
                        if (myField["IsKey"].ToString().Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            colNode.ImageIndex = pkeyImageIndex;
                            colNode.SelectedImageIndex = pkeyImageIndex;
                            strpfKey = "PK, ";
                            bPKey = true;
                        }
                        //else if (col.IsForeignKey)
                        //{
                        //    colNode.ImageIndex = fkeyImageIndex;
                        //    colNode.SelectedImageIndex = fkeyImageIndex;
                        //    strpfKey = "FK, ";
                        //}
                        else
                        {
                            colNode.ImageIndex = columnImageIndex;
                            colNode.SelectedImageIndex = columnImageIndex;
                        }

                        string strSize = "";
                        int size = 0;
                        if (myField["DataType"].ToString().Contains("char"))
                        {
                            size = Int32.Parse(myField["ColumnSize"].ToString());
                            strSize = string.Format("({0})", myField["ColumnSize"].ToString());
                        }
                        string strNullable;
                        bool bNullable = true;
                        if (myField["AllowDBNull"].ToString().Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            strNullable = "null";
                            bNullable = true;
                        }
                        else
                        {
                            strNullable = "not null";
                            bNullable = false;
                        }

                        string strDataType = string.Empty;
                        strDataType = myField["DataType"].ToString();
                        //if(strDataType == "System.Decimal")
                        //{
                        //    strDataType = "System.Double";
                        //}
                        colNode.Text = string.Format("{0} ({1}{2}{3}, {4})", colNode.Text, strpfKey, strDataType, strSize, strNullable);

                        PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                        RefDBFieldProp.PMSRefDBConnection = (PMSRefDBConnection)(tableNode.Parent.Tag);
                        RefDBFieldProp.StrTableName = TableName;
                        RefDBFieldProp.StrFieldName = columnName;
                        RefDBFieldProp.StrFieldType = strDataType;
                        RefDBFieldProp.DbType = RefDBInfo.GetDbType(strDataType);
                        RefDBFieldProp.Type = RefDBInfo.GetCSharpType(strDataType);
                        RefDBFieldProp.IFildID = Int32.Parse(myField["ColumnOrdinal"].ToString());
                        RefDBFieldProp.IFieldLength = size;
                        //RefDBFieldProp.StrFieldDefault = col.Default;
                        //RefDBFieldProp.StrFieldDescription = GetTableColumnDescription(TableName, col.Name);
                        //RefDBFieldProp.BFieldIdentity = col.Identity;
                        RefDBFieldProp.BFieldNullable = bNullable;
                        RefDBFieldProp.BFieldPrimaryKey = bPKey;
                        colNode.Tag = RefDBFieldProp;

                        // add tableNode to tablesNode
                        tableNode.Nodes.Add(colNode);
                    }
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Dictionary<string, PMSRefDBFieldProp> GetTableColumns(string tableName, PMSRefDBConnection rc)
        {
            try
            {
                string sSql = string.Empty;


                DataTable dtColumnsInfo = _OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, null });

                Dictionary<string, PMSRefDBFieldProp> dic = new Dictionary<string, PMSRefDBFieldProp>();

                foreach (DataRow dr in dtColumnsInfo.Rows)
                {
                    string columnName = dr[3].ToString();
                    sSql = "SELECT [" + columnName + "] FROM [" + tableName + "]";

                    OleDbCommand cmd = new OleDbCommand(sSql, _OleDbConn);
                    //_OleDbConn.Open();

                    OleDbDataReader rdr = cmd.ExecuteReader(CommandBehavior.Default);
                    DataTable schemaTable = rdr.GetSchemaTable();
                    StringBuilder sb = new StringBuilder();

                    var selectedRows = from r in schemaTable.AsEnumerable() orderby r.Field<int>("ColumnOrdinal") select r;

                    EnumerableRowCollection<DataRow> roomEnumer = selectedRows;
                    //////////////////////////////////////////////////////////////////////////
                    /// myField DataRow Description
                    ///	[0]	{ColumnName}	
                    ///	[1]	{ColumnOrdinal}	
                    ///	[2]	{ColumnSize}	
                    ///	[3]	{NumericPrecision}	
                    ///	[4]	{NumericScale}	
                    ///	[5]	{DataType}	
                    ///	[6]	{ProviderType}	
                    ///	[7]	{IsLong}	
                    ///	[8]	{AllowDBNull}	
                    ///	[9]	{IsReadOnly}	
                    ///	[10]	{IsRowVersion}	
                    ///	[11]	{IsUnique}	
                    ///	[12]	{IsKey}	
                    ///	[13]	{IsAutoIncrement}	
                    ///	[14]	{BaseSchemaName}	
                    ///	[15]	{BaseCatalogName}	
                    ///	[16]	{BaseTableName}	
                    ///	[17]	{BaseColumnName}	
                    //////////////////////////////////////////////////////////////////////////
                    foreach (DataRow myField in roomEnumer)
                    {
                        // create a node to hold column name (level 2)
                        // then add dataType to it's text
                        string strpfKey = "";
                        bool bPKey = false;
                        //if (col.InPrimaryKey && col.IsForeignKey)
                        //{
                        //    colNode.ImageIndex = pfkeyImageIndex;
                        //    colNode.SelectedImageIndex = pfkeyImageIndex;
                        //    strpfKey = "PK, FK, ";
                        //}
                        //else
                        if (myField["IsKey"].ToString().Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            strpfKey = "PK, ";
                            bPKey = true;
                        }
                        //else if (col.IsForeignKey)
                        //{
                        //    colNode.ImageIndex = fkeyImageIndex;
                        //    colNode.SelectedImageIndex = fkeyImageIndex;
                        //    strpfKey = "FK, ";
                        //}
                        else
                        {

                        }

                        string strSize = "";
                        int size = 0;
                        if (myField["DataType"].ToString().Contains("char"))
                        {
                            size = Int32.Parse(myField["ColumnSize"].ToString());
                            strSize = string.Format("({0})", myField["ColumnSize"].ToString());
                        }
                        string strNullable;
                        bool bNullable = true;
                        if (myField["AllowDBNull"].ToString().Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            strNullable = "null";
                            bNullable = true;
                        }
                        else
                        {
                            strNullable = "not null";
                            bNullable = false;
                        }

                        string strDataType = string.Empty;
                        strDataType = myField["DataType"].ToString();
                        //if(strDataType == "System.Decimal")
                        //{
                        //    strDataType = "System.Double";
                        //}

                        PMSRefDBFieldProp RefDBFieldProp = new PMSRefDBFieldProp();
                        RefDBFieldProp.PMSRefDBConnection = rc;
                        RefDBFieldProp.StrTableName = tableName;
                        RefDBFieldProp.StrFieldName = columnName;
                        RefDBFieldProp.StrFieldType = strDataType;
                        RefDBFieldProp.DbType = RefDBInfo.GetDbType(strDataType);
                        RefDBFieldProp.Type = RefDBInfo.GetCSharpType(strDataType);
                        RefDBFieldProp.IFildID = Int32.Parse(myField["ColumnOrdinal"].ToString());
                        RefDBFieldProp.IFieldLength = size;
                        //RefDBFieldProp.StrFieldDefault = col.Default;
                        //RefDBFieldProp.StrFieldDescription = GetTableColumnDescription(TableName, col.Name);
                        //RefDBFieldProp.BFieldIdentity = col.Identity;
                        RefDBFieldProp.BFieldNullable = bNullable;
                        RefDBFieldProp.BFieldPrimaryKey = bPKey;

                        dic.Add(columnName, RefDBFieldProp);
                    }
                    rdr.Close();
                }

                return dic;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

    }

    #region PMSDBProp

    [Serializable]
    public class PMSDBExtendedProp
    {
        private string _strPropName;
        public string StrPropName
        {
            get { return _strPropName; }
            set { _strPropName = value; }
        }

        private string _strPropValue;
        public string StrPropValue
        {
            get { return _strPropValue; }
            set { _strPropValue = value; }
        }
    }

    [Serializable]
    public class PMSDBTableProp
    {
        private string _strTableName;
        public string StrTableName
        {
            get { return _strTableName; }
            set { _strTableName = value; }
        }

        private PMSDBFieldPropCollection _FieldPropCollection;
        public PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBFieldPropCollection FieldPropCollection
        {
            get { return _FieldPropCollection; }
            set { _FieldPropCollection = value; }
        }

        private Dictionary<string, PMSDBExtendedProp> _ExProps;
        public Dictionary<string, PMSDBExtendedProp> ExProps
        {
            get { return _ExProps; }
            set { _ExProps = value; }
        }
        
    }

    [Serializable]
    public class PMSDBFieldProp
    {
        private string _strTableName;
        public string StrTableName
        {
            get { return _strTableName; }
            set { _strTableName = value; }
        }

        private string _strFieldName;
        public string StrFieldName
        {
            get { return _strFieldName; }
            set { _strFieldName = value; }
        }

        private int _iFildID;
        public int IFildID
        {
            get { return _iFildID; }
            set { _iFildID = value; }
        }

        private string _strFieldType;
        public string StrFieldType
        {
            get { return _strFieldType; }
            set { _strFieldType = value; }
        }

        private int _iFieldLength;
        public int IFieldLength
        {
            get { return _iFieldLength; }
            set { _iFieldLength = value; }
        }

        private bool _bFieldNullable;
        public bool BFieldNullable
        {
            get { return _bFieldNullable; }
            set { _bFieldNullable = value; }
        }

        private bool _bFieldPrimaryKey = false;
        public bool BFieldPrimaryKey
        {
            get { return _bFieldPrimaryKey; }
            set { _bFieldPrimaryKey = value; }
        }

        private bool _bFieldForeignKey = false;
        public bool BFieldForeignKey
        {
            get { return _bFieldForeignKey; }
            set { _bFieldForeignKey = value; }
        }

        private bool _bFieldIdentity;
        public bool BFieldIdentity
        {
            get { return _bFieldIdentity; }
            set { _bFieldIdentity = value; }
        }

        private bool _bFieldIsSystem;
        public bool BFieldIsSystem
        {
            get { return _bFieldIsSystem; }
            set { _bFieldIsSystem = value; }
        }

        private string _strFieldDefault;
        public string StrFieldDefault
        {
            get { return _strFieldDefault; }
            set { _strFieldDefault = value; }
        }

        private string _strFieldDescription;
        public string StrFieldDescription
        {
            get { return _strFieldDescription; }
            set { _strFieldDescription = value; }
        }

        private Dictionary<string, PMSDBExtendedProp> _ExProps;
        public Dictionary<string, PMSDBExtendedProp> ExProps
        {
            get { return _ExProps; }
            set { _ExProps = value; }
        }

        public string GetDBString()
        {
            string strpfKey = "";
            if (_bFieldPrimaryKey && _bFieldForeignKey)
            {
                strpfKey = "PK, FK, ";
            }
            else if (_bFieldPrimaryKey)
            {
                strpfKey = "PK, ";
            }
            else if (_bFieldForeignKey)
            {
                strpfKey = "FK, ";
            }

            string strSize = "";
            if (_strFieldType.Contains("char"))
                strSize = string.Format("({0})", _iFieldLength);
            string strNullable;
            if (_bFieldNullable)
                strNullable = "null";
            else
                strNullable = "not null";
            return string.Format("{0} ({1}{2}{3}, {4})", _strFieldName, strpfKey, _strFieldType, strSize, strNullable);
        }
    }

    public class PMSDBFieldPropCollection : System.Collections.ObjectModel.Collection<PMSDBFieldProp>
    {
    }

    [Serializable]
    public class PMSDBTablePFKeyRelation
    {
        // FOREIGNCOLUMN
        // FOREIGNTABLE
        // PRIMARYTABLE
        // PRIMARYCOLUMN

        private string _PrimaryTable;
        public string PrimaryTable
        {
            get { return _PrimaryTable; }
            set { _PrimaryTable = value; }
        }

        private string _PrimaryColumn;
        public string PrimaryColumn
        {
            get { return _PrimaryColumn; }
            set { _PrimaryColumn = value; }
        }

        private string _ForeignTable;
        public string ForeignTable
        {
            get { return _ForeignTable; }
            set { _ForeignTable = value; }
        }

        private string _ForengnColumn;
        public string ForengnColumn
        {
            get { return _ForengnColumn; }
            set { _ForengnColumn = value; }
        }
    }

    public class PMSDBTablePFKeyRelationCollection : System.Collections.ObjectModel.Collection<PMSDBTablePFKeyRelation>
    {
    }

    #endregion

    #region PMSDBRefProp

    [Serializable]
    public class PMSRefObjProp
    {
        private string _strTableName;
        public string StrTableName
        {
            get { return _strTableName; }
            set { _strTableName = value; }
        }

        private PMSRefDBConnection _PMSRefDBConnection;
        public PMS.Libraries.ToolControls.PMSPublicInfo.PMSRefDBConnection PMSRefDBConnection
        {
            get { return _PMSRefDBConnection; }
            set { _PMSRefDBConnection = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is PMSRefObjProp && this == (PMSRefObjProp)obj;
        }

        public static bool operator ==(PMSRefObjProp x, PMSRefObjProp y)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(x, y))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }
            return x._strTableName == y._strTableName && x._PMSRefDBConnection == y._PMSRefDBConnection;
        }

        public static bool operator !=(PMSRefObjProp x, PMSRefObjProp y)
        {
            return !(x == y);
        }

    }

    [Serializable]
    public class PMSRefDBTableProp : PMSRefObjProp
    {

    }

    [Serializable]
    public class PMSRefDBViewProp : PMSRefObjProp
    {

    }

    [Serializable]
    public class PMSRefDBSpProp : PMSRefObjProp
    {

    }

    [Serializable]
    public class PMSDataModuleProp : PMSRefObjProp
    {
        /// <summary>
        /// 数据模型名
        /// </summary>
        private string _moduleName = string.Empty;
        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; }
        }
    }

    [Serializable]
    public class PMSRefDBFieldProp : PMSRefObjProp
    {
        //private string _strTableName;
        //public string StrTableName
        //{
        //    get { return _strTableName; }
        //    set { _strTableName = value; }
        //}

        private string _strFieldName;
        public string StrFieldName
        {
            get { return _strFieldName; }
            set { _strFieldName = value; }
        }

        private int _iFildID;
        public int IFildID
        {
            get { return _iFildID; }
            set { _iFildID = value; }
        }

        private string _strFieldType;
        public string StrFieldType
        {
            get { return _strFieldType; }
            set { _strFieldType = value; }
        }

        private DbType _dbType;
        public System.Data.DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        private Type _type;
        public System.Type Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private int _iFieldLength;
        public int IFieldLength
        {
            get { return _iFieldLength; }
            set { _iFieldLength = value; }
        }

        private bool _bFieldNullable;
        public bool BFieldNullable
        {
            get { return _bFieldNullable; }
            set { _bFieldNullable = value; }
        }

        private bool _bFieldPrimaryKey;
        public bool BFieldPrimaryKey
        {
            get { return _bFieldPrimaryKey; }
            set { _bFieldPrimaryKey = value; }
        }

        private bool _bFieldForeignKey;
        public bool BFieldForeignKey
        {
            get { return _bFieldForeignKey; }
            set { _bFieldForeignKey = value; }
        }

        private bool _bFieldIdentity;
        public bool BFieldIdentity
        {
            get { return _bFieldIdentity; }
            set { _bFieldIdentity = value; }
        }

        private string _strFieldDefault;
        public string StrFieldDefault
        {
            get { return _strFieldDefault; }
            set { _strFieldDefault = value; }
        }

        private string _strFieldDescription;
        public string StrFieldDescription
        {
            get { return _strFieldDescription; }
            set { _strFieldDescription = value; }
        }

        //private PMSRefDBConnection _PMSRefDBConnection;
        //public PMS.Libraries.ToolControls.PMSPublicInfo.PMSRefDBConnection PMSRefDBConnection
        //{
        //    get { return _PMSRefDBConnection; }
        //    set { _PMSRefDBConnection = value; }
        //}
    }

    [Serializable]
    public enum StoreProcedureItemEnum
    {
        /// <summary>
        /// 输入 
        /// </summary>
        Input,
        /// <summary>
        /// 输入/输出
        /// </summary>
        InOutput,
        /// <summary>
        /// 返回值
        /// </summary>
        Return,
    }

    public interface IStoreProcedureItem
    {
        StoreProcedureItemEnum SPIType
        {
            get;
            set;
        }
    }

    [Serializable]
    public class PMSRefDBSPItemProp : PMSRefDBFieldProp, IStoreProcedureItem
    {
        public StoreProcedureItemEnum SPIType
        {
            get;
            set;
        }
    }

    public class PMSRefDBFieldPropCollection : System.Collections.ObjectModel.Collection<PMSRefDBFieldProp>
    {
    }

    #endregion

    public class DBConnectionManager
    {
        private static Dictionary<string,DbConnection> _connectionDict= new Dictionary<string,DbConnection>();

        private static bool _EnableCache = true;
        public static bool EnableCache
        {
            get { return _EnableCache; }
            set { _EnableCache = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <returns>
        /// 1-成功
        /// 0-失败
        /// -1-已经存在
        /// </returns>
        public static int Add(DbConnection conn)
        {

            if (!_EnableCache)
                return 0;
            try
            {
                if (_connectionDict.ContainsKey(conn.ConnectionString))
                    return -1;
                else
                {
                    _connectionDict.Add(conn.ConnectionString, conn);
                    conn.StateChange += new StateChangeEventHandler(conn_StateChange);
                    return 1;
                }
            }
            catch (System.Exception e)
            {
                return 0;
            }
        }

        static void conn_StateChange(object sender, StateChangeEventArgs e)
        {
            // connection 关闭
            if (e.CurrentState == ConnectionState.Closed)
            {
                if (sender is IDbConnection)
                    Remove(sender as IDbConnection);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <returns>
        /// 1-成功
        /// 0-失败
        /// -1-不存在
        /// </returns>
        public static int Remove(IDbConnection conn)
        {
            if (!_EnableCache)
                return 0;
            try
            {
                if (_connectionDict.ContainsKey(conn.ConnectionString))
                {
                    if (_connectionDict.Remove(conn.ConnectionString))
                        return 1;
                }
                else
                {
                    return -1;
                }
                return 0;
            }
            catch (System.Exception e)
            {
                return 0;
            }
        }

        public static DbConnection GetConnection(string strConnectionString)
        {
            if (!_EnableCache)
                return null;
            try
            {
                if (_connectionDict.ContainsKey(strConnectionString))
                {
                    DbConnection conn = null;
                    if (_connectionDict.TryGetValue(strConnectionString, out conn))
                    {
                        if (TestConnection(conn))
                            return conn;
                        else
                        {
                            Remove(conn);
                        }
                    }
                }
                return null;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        private static bool TestConnection(IDbConnection conn)
        {
            try
            {
                if (null == conn)
                    return false;

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                IDbCommand comm = conn.CreateCommand();
                comm.CommandTimeout = 5;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "select 0";
                comm.ExecuteNonQuery();
                comm.Dispose();
                comm = null;
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }
    }

    //public class DBConnHeartBeatTimer : IDisposable
    //{
    //    private object _lockObj = null;

    //    public IDbConnection _conn
    //    {
    //        get;
    //        set;
    //    }

    //    public System.Timers.Timer _timer
    //    {
    //        get;
    //        set;
    //    }

    //    public DBConnHeartBeatTimer(IDbConnection conn)
    //    {
    //        _lockObj = new object();
    //        _conn = conn;
    //        _timer = new System.Timers.Timer();
    //        TimeSpan ts = new TimeSpan(0, 0, 50);
    //        _timer.Interval = ts.TotalMilliseconds - 1;
    //        _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
    //        _timer.Enabled = true;
    //        Start();
    //    }

    //    void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    //    {
    //        lock (_lockObj)
    //        {
    //            try
    //            {
    //                IDbCommand comm = _conn.CreateCommand();
    //                comm.CommandType = CommandType.Text;
    //                comm.CommandText = "";
    //                comm.ExecuteNonQuery();
    //                comm.Dispose();
    //            }
    //            catch (System.Exception ex)
    //            {
    //                string strposition = PMS.Libraries.ToolControls.PMSPublicInfo.PublicFunctionClass.CodeRunningPosition();
    //                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(strposition + ex.Message);
    //                _timer.Stop();
    //                OnDisConnect();

    //                while (!TestReConnect())
    //                    Thread.Sleep(10000);

    //                _timer.Start();

    //                CloseClient();
    //                OnReConnect();
    //            }
    //        }

    //        //SessionTimeout(this, new SessionTimeoutArgs(_client.Identity));
    //    }

    //    private bool CloseClient()
    //    {
    //        switch (ServiceType)
    //        {
    //            case MESServiceType.MESAuthorizationService:
    //                try
    //                {
    //                    if (_client is ICommunicationObject)
    //                    {
    //                        (_client as ICommunicationObject).Close();
    //                    }
    //                }
    //                catch (System.Exception ex)
    //                {
    //                    return false;
    //                }
    //                break;
    //            case MESServiceType.MESFileTransforService:
    //                try
    //                {
    //                    if (_client is MESFileTransforClient)
    //                    {
    //                        (_client as MESFileTransforClient).Close();
    //                    }
    //                }
    //                catch (System.Exception ex)
    //                {
    //                    return false;
    //                }
    //                break;
    //            case MESServiceType.MESPrivilege:
    //                try
    //                {
    //                    if (_client is MESPrivilegeClient)
    //                    {
    //                        (_client as MESPrivilegeClient).Close();
    //                    }
    //                }
    //                catch (System.Exception ex)
    //                {
    //                    return false;
    //                }
    //                break;
    //            case MESServiceType.MESWFService:
    //                try
    //                {
    //                    if (_client is WorkFlowProcessClient)
    //                    {
    //                        (_client as WorkFlowProcessClient).Close();
    //                    }
    //                }
    //                catch (System.Exception ex)
    //                {
    //                    return false;
    //                }
    //                break;
    //            default:
    //                break;
    //        }
    //        return true;
    //    }

    //    private bool TestReConnect()
    //    {
    //        try
    //        {
    //            IDbCommand comm = _conn.CreateCommand();
    //            comm.CommandType = CommandType.Text;
    //            comm.CommandText = "";
    //            comm.ExecuteNonQuery();
    //            comm.Dispose();
    //        }
    //        catch (System.Exception ex)
    //        {
    //            return false;
    //        }

    //        return true;
    //    }

    //    public void OnDisConnect()
    //    {
    //        if (_client is IMESServiceExceptionProcess)
    //        {
    //            (_client as IMESServiceExceptionProcess).OnDisConnect();
    //        }
    //    }

    //    public void OnReConnect()
    //    {
    //        if (_client is IMESServiceExceptionProcess)
    //        {
    //            (_client as IMESServiceExceptionProcess).OnReConnect();
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        Stop();
    //    }

    //    public void Start()
    //    {
    //        lock (_lockObj)
    //        {
    //            _timer.Start();
    //        }
    //    }

    //    public void Stop()
    //    {
    //        lock (_lockObj)
    //        {
    //            _timer.Stop();
    //        }
    //    }
    //}

    [Serializable]
    public class PMSRefDBConnection : ICloneable
    {
        [NonSerialized]
        private DbConnection dbConnection = null;

        public DbConnection DBConnection
        {
            get { return dbConnection; }
        }

        [NonSerialized]
        private SqlCommand _thisSqlCommand = null;

        [NonSerialized]
        private OracleCommand _thisOracleCommand = null;

        [NonSerialized]
        private OleDbCommand _thisOleCommand = null;

        [NonSerialized]
        static private SqlAssistance sa = new SqlAssistance();

        [BrowsableAttribute(false)]
        public PMS.Libraries.ToolControls.PMSPublicInfo.SqlAssistance Sa
        {
            get { return sa; }
        }

        [NonSerialized]
        static private OracleAssistance oa = new OracleAssistance();

        [BrowsableAttribute(false)]
        public PMS.Libraries.ToolControls.PMSPublicInfo.OracleAssistance Oa
        {
            get { return oa; }
        }

        private string _strSourceName = string.Empty;
        [Browsable(false)]
        public string StrSourceName
        {
            get { return _strSourceName; }
            set { _strSourceName = value; }
        }

        private string _strDBPath = string.Empty;
        public string StrDBPath
        {
            get { return _strDBPath; }
            set { _strDBPath = value; }
        }

        private string _strServerName = string.Empty;
        public string StrServerName
        {
            get { return _strServerName; }
            set { _strServerName = value; }
        }

        private string _strDBName = string.Empty;
        public string StrDBName
        {
            get { return _strDBName; }
            set { _strDBName = value; }
        }

        private string _strUserID = string.Empty;
        public string StrUserID
        {
            get { return _strUserID; }
            set { _strUserID = value; }
        }

        private string _strPassWord = string.Empty;
        public string StrPassWord
        {
            get { return _strPassWord; }
            set { _strPassWord = value; }
        }

        private ConnectType _eConnectType = ConnectType.namepipe;
        public PMS.Libraries.ToolControls.PMSPublicInfo.ConnectType EConnectType
        {
            get { return _eConnectType; }
            set { _eConnectType = value; }
        }

        private string _strPortID = string.Empty;
        public string StrPortID
        {
            get { return _strPortID; }
            set { _strPortID = value; }
        }

        private string _strConnectAs = string.Empty;
        public string StrConnectAs
        {
            get { return _strConnectAs; }
            set { _strConnectAs = value; }
        }

        private string _strConnectMode = string.Empty;
        public string StrConnectMode
        {
            get { return _strConnectMode; }
            set { _strConnectMode = value; }
        }

        private string _strOwner = string.Empty;
        public string Owner
        {
            get { return _strOwner; }
            set { _strOwner = value; }
        }

        private bool _bDirect = false;
        public bool BDirect
        {
            get { return _bDirect; }
            set { _bDirect = value; }
        }

        private string _connectString = string.Empty;
        public string ConnectString
        {
            get { return _connectString; }
            set { _connectString = value; }
        }

        private RefDBType _RefDBType = RefDBType.MSSqlServer;
        public RefDBType RefDBType
        {
            get { return _RefDBType; }
            set { _RefDBType = value; }
        }

        private string _error = null;
        
        public string GetLastError()
        {
            return _error;
        }

        public SqlConnection GetSqlConnection(bool wantNewTempConnection = false)
        {
            try
            {
                if (!wantNewTempConnection)
                {
                    string cs = GetSqlConnectionStringWithoutPassword();
                    if (cs != null)
                    {
                        DbConnection dc = DBConnectionManager.GetConnection(cs);
                        if (dc != null)
                        {
                            dbConnection = dc;
                            return (SqlConnection)dc;
                        }
                    }
                }

                if (RefDBType == RefDBType.MSSqlServer)
                {
                    bool bConnect = false;
                    if (EConnectType == ConnectType.namepipe)
                        bConnect = sa.ConnectDatabase(StrServerName, StrDBName, StrUserID, StrPassWord);
                    else if (EConnectType == ConnectType.tcpip)
                        bConnect = sa.ConnectRemoteDatabase(StrServerName, StrDBName, StrUserID, StrPassWord, StrPortID);
                    else
                        bConnect = sa.ConnectDatabase(StrServerName, StrDBName, StrUserID, StrPassWord);

                    if (bConnect == true)
                    {
                        sa.DatabaseName = StrDBName;
                        if (!wantNewTempConnection)
                            sa.ConnectSMOServer(sa.SqlConnection);
                        DBConnectionManager.Add(sa.SqlConnection);
                        dbConnection = sa.SqlConnection;
                        ConnectString = dbConnection.ConnectionString;
                        return sa.SqlConnection;
                    }
                    else
                        return null;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                _error = ex.Message;
                return null;
            }
            return null;
        }

        private string GetSqlConnectionString()
        {
            try
            {
                string connectstring = null;
                if (RefDBType == RefDBType.MSSqlServer)
                {
                    if (EConnectType == ConnectType.namepipe)
                        connectstring = sa.ConnectDatabaseString(StrServerName, StrDBName, StrUserID, StrPassWord);
                    else if (EConnectType == ConnectType.tcpip)
                        connectstring = sa.ConnectRemoteDatabaseString(StrServerName, StrDBName, StrUserID, StrPassWord, StrPortID);
                    else
                        connectstring = sa.ConnectDatabaseString(StrServerName, StrDBName, StrUserID, StrPassWord);
                }
                return connectstring;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                return null;
            }
        }

        private string GetSqlConnectionStringWithoutPassword()
        {
            try
            {
                string connectstring = null;
                if (RefDBType == RefDBType.MSSqlServer)
                {
                    if (EConnectType == ConnectType.namepipe)
                        connectstring = sa.ConnectDatabaseString(StrServerName, StrDBName, StrUserID);
                    else if (EConnectType == ConnectType.tcpip)
                        connectstring = sa.ConnectRemoteDatabaseString(StrServerName, StrDBName, StrUserID, StrPortID);
                    else
                        connectstring = sa.ConnectDatabaseString(StrServerName, StrDBName, StrUserID);
                }
                return connectstring;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                return null;
            }
        }

        public OracleConnection GetOracleConnection(bool wantNewTempConnection = false)
        {
            try
            {
                if (!wantNewTempConnection)
                {
                    string cs = GetOracleConnectionStringWithoutPassword();
                    if (cs != null)
                    {
                        DbConnection dc = DBConnectionManager.GetConnection(cs);
                        if (dc != null)
                        {
                            dbConnection = dc;
                            return (OracleConnection)dc;
                        }
                    }
                }

                if (RefDBType == RefDBType.Oracle)
                {
                    bool bConnect = false;
                    if (!BDirect)
                        bConnect = oa.ConnectDatabase(StrServerName, StrUserID, StrPassWord, StrConnectAs);
                    else
                        bConnect = oa.ConnectDirectDatabase(StrServerName, StrDBName, StrUserID, StrPassWord, StrPortID, StrConnectMode);
                    
                    if (bConnect == true)
                    {
                        oa.Owner = Owner;
                        DBConnectionManager.Add(oa.OracleConnection);
                        dbConnection = oa.OracleConnection;
                        ConnectString = dbConnection.ConnectionString;
                        return oa.OracleConnection;
                    }
                    else
                        return null;
                }
            }
            catch (OracleException ex)
            {
                _error = ex.ToString();
                return null;
            }
            return null;
        }

        private string GetOracleConnectionString()
        {
            try
            {
                string connectstring = null;
                if (RefDBType == RefDBType.Oracle)
                {
                    if (!BDirect)
                        connectstring = oa.ConnectDatabaseString(StrServerName, StrUserID, StrPassWord, StrConnectAs);
                    else
                        connectstring = oa.ConnectDirectDatabaseString(StrServerName, StrDBName, StrUserID, StrPassWord, StrPortID);
                }
                return connectstring;
            }
            catch (OracleException ex)
            {
                _error = ex.Message;
                return null;
            }
        }

        private string GetOracleConnectionStringWithoutPassword()
        {
            try
            {
                string connectstring = null;
                if (RefDBType == RefDBType.Oracle)
                {
                    if (!BDirect)
                        connectstring = oa.ConnectDatabaseString(StrServerName, StrUserID, StrConnectAs);
                    else
                        connectstring = oa.ConnectDirectDatabaseString(StrServerName, StrDBName, StrUserID, StrPortID, StrConnectMode);
                }
                return connectstring;
            }
            catch (OracleException ex)
            {
                _error = ex.Message;
                return null;
            }
        }

        public OleDbConnection GetOleConnection()
        {
            try
            {
                string cs = GetOleConnectionString();
                if (cs != null)
                {
                    DbConnection dc = DBConnectionManager.GetConnection(cs);
                    if (dc != null)
                    {
                        dbConnection = dc;
                        return (OleDbConnection)dc;
                    }
                }

                if(RefDBType == RefDBType.MSAccess)
                {
                    // 利用 OleDbConnectionStringBuilder 对象来构建
                    // 连接字符串。
                    OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                    connectStringBuilder.DataSource = ProjectPathClass.ParseStringWithMacro(StrDBPath);
                    connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                    OleDbConnection oleconn = new OleDbConnection(connectStringBuilder.ConnectionString);
                    oleconn.Open();
                    DBConnectionManager.Add(oleconn);
                    dbConnection = oleconn;
                    ConnectString = dbConnection.ConnectionString;
                    return oleconn;
                }
                else if (RefDBType == RefDBType.MSSqlServer)
                {
                    bool bConnect = false;
                    if (EConnectType == ConnectType.namepipe)
                        bConnect = sa.ConnectOleDB(StrServerName, StrDBName, StrUserID, StrPassWord);
                    else if(EConnectType == ConnectType.tcpip)
                        bConnect = sa.ConnectRemoteOleDB(StrServerName, StrDBName, StrUserID, StrPassWord, StrPortID);
                    else
                        bConnect = sa.ConnectOleDB(StrServerName, StrDBName, StrUserID, StrPassWord);

                    if (bConnect == true)
                    {
                        DBConnectionManager.Add(sa.OleDBConn);
                        dbConnection = sa.OleDBConn;
                        ConnectString = dbConnection.ConnectionString;
                        return sa.OleDBConn;
                    }
                    else
                        return null;
                }
                else if(RefDBType == RefDBType.Oracle)
                {
                    return null;
                }
                else if(RefDBType == RefDBType.OleDB)
                {
                    OleDbConnection oleconn = new OleDbConnection(ProjectPathClass.ParseStringWithMacro(ConnectString));
                    oleconn.Open();
                    DBConnectionManager.Add(oleconn);
                    dbConnection = oleconn;
                    ConnectString = dbConnection.ConnectionString;
                    return oleconn;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                _error = ex.Message;
                return null;
            }
            return null;
        }

        private string GetOleConnectionString()
        {
            try
            {
                string connectstring = null;
                if (RefDBType == RefDBType.MSAccess)
                {
                    // 利用 OleDbConnectionStringBuilder 对象来构建
                    // 连接字符串。
                    OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                    connectStringBuilder.DataSource = StrDBPath;
                    connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                    connectstring = connectStringBuilder.ConnectionString;
                }
                else if (RefDBType == RefDBType.MSSqlServer)
                {
                    if (EConnectType == ConnectType.namepipe)
                        connectstring = sa.ConnectOleDBString(StrServerName, StrDBName, StrUserID, StrPassWord);
                    else if (EConnectType == ConnectType.tcpip)
                        connectstring = sa.ConnectRemoteOleDBString(StrServerName, StrDBName, StrUserID, StrPassWord, StrPortID);
                    else
                        connectstring = sa.ConnectOleDBString(StrServerName, StrDBName, StrUserID, StrPassWord);
                }
                else if (RefDBType == RefDBType.Oracle)
                {
                    return null;
                }
                else if (RefDBType == RefDBType.OleDB)
                {
                    connectstring = ConnectString;
                }
                return connectstring;
            }
            catch (OracleException ex)
            {
                _error = ex.Message;
                return null;
            }
        }

        public IDbConnection GetConnection(bool wantNewTempConnection = false)
        {
            try
            {
                if (!wantNewTempConnection)
                {
                    string cs = GetConnectionStringWithoutPassword();
                    if (this.ConnectString != null)
                    {
                        DbConnection dc = DBConnectionManager.GetConnection(cs);
                        if (dc != null)
                            return dc;
                    }
                }

                if (RefDBType == RefDBType.MSAccess)
                {
                    // 利用 OleDbConnectionStringBuilder 对象来构建
                    // 连接字符串。
                    OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                    connectStringBuilder.DataSource = ProjectPathClass.ParseStringWithMacro(StrDBPath);
                    connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                    OleDbConnection oleconn = new OleDbConnection(connectStringBuilder.ConnectionString);
                    oleconn.Open();
                    DBConnectionManager.Add(oleconn);
                    dbConnection = oleconn;
                    ConnectString = dbConnection.ConnectionString;
                    return oleconn;
                }
                else if (RefDBType == RefDBType.MSSqlServer)
                {
                    return GetSqlConnection(wantNewTempConnection);
                }
                else if (RefDBType == RefDBType.Oracle)
                {
                    return GetOracleConnection(wantNewTempConnection);
                }
                else if (RefDBType == RefDBType.OleDB)
                {
                    OleDbConnection oleconn = new OleDbConnection(ConnectString);
                    oleconn.Open();
                    DBConnectionManager.Add(oleconn);
                    dbConnection = oleconn;
                    ConnectString = dbConnection.ConnectionString;
                    return oleconn;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                _error = ex.Message;
                return null;
            }
            return null;
        }

        public string GetConnectionString()
        {
            try
            {

                string connectstring = null;
                if (RefDBType == RefDBType.MSAccess)
                {
                    // 利用 OleDbConnectionStringBuilder 对象来构建
                    // 连接字符串。
                    OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                    connectStringBuilder.DataSource = StrDBPath;
                    connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                    connectstring = connectStringBuilder.ConnectionString;
                }
                else if (RefDBType == RefDBType.MSSqlServer)
                {
                    connectstring = GetSqlConnectionString();
                }
                else if (RefDBType == RefDBType.Oracle)
                {
                    connectstring = GetOracleConnectionString();
                }
                else if (RefDBType == RefDBType.OleDB)
                {
                    connectstring = ConnectString;
                }
                return connectstring;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                _error = ex.Message;
                return null;
            }
        }

        private string GetConnectionStringWithoutPassword()
        {
            try
            {

                string connectstring = null;
                if (RefDBType == RefDBType.MSAccess)
                {
                    // 利用 OleDbConnectionStringBuilder 对象来构建
                    // 连接字符串。
                    OleDbConnectionStringBuilder connectStringBuilder = new OleDbConnectionStringBuilder();
                    connectStringBuilder.DataSource = StrDBPath;
                    connectStringBuilder.Provider = "Microsoft.Jet.OLEDB.4.0";
                    connectstring = connectStringBuilder.ConnectionString;
                }
                else if (RefDBType == RefDBType.MSSqlServer)
                {
                    connectstring = GetSqlConnectionStringWithoutPassword();
                }
                else if (RefDBType == RefDBType.Oracle)
                {
                    connectstring = GetOracleConnectionStringWithoutPassword();
                }
                else if (RefDBType == RefDBType.OleDB)
                {
                    connectstring = ConnectString;
                }
                return connectstring;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                _error = ex.Message;
                return null;
            }
        }

        public DataTable ExecuteCommand(string strcommand)
        {
            if (RefDBType == RefDBType.MSAccess
                || RefDBType == RefDBType.OleDB)
            {
                DataTable dt = new DataTable();
                OleDbConnection DbConnection = GetOleConnection();
                if (DbConnection.State != ConnectionState.Open)
                    return null;
                if (_thisOleCommand == null)
                    _thisOleCommand = DbConnection.CreateCommand();
                _thisOleCommand.CommandText = strcommand;
                OleDbDataReader thisReader = _thisOleCommand.ExecuteReader();
                dt.Load(thisReader);
                if (!thisReader.IsClosed)
                    thisReader.Close();
                _thisOleCommand.Dispose();
                _thisOleCommand = null;
                return dt;
            }
            else if (RefDBType == RefDBType.MSSqlServer)
            {
                DataTable dt = new DataTable();
                SqlConnection SqlConnection1 = GetSqlConnection();
                if (SqlConnection1.State != ConnectionState.Open)
                    return null;
                if (_thisSqlCommand == null)
                    _thisSqlCommand = SqlConnection1.CreateCommand();
                _thisSqlCommand.CommandText = strcommand;
                SqlDataReader thisReader = _thisSqlCommand.ExecuteReader();
                dt.Load(thisReader);
                if (!thisReader.IsClosed)
                    thisReader.Close();
                _thisSqlCommand.Dispose();
                _thisSqlCommand = null;
                return dt;
            }
            else if (RefDBType == RefDBType.Oracle)
            {
                DataTable dt = new DataTable();
                OracleConnection OraclelConnection1 = GetOracleConnection();
                if (OraclelConnection1.State != ConnectionState.Open)
                    return null;
                if (_thisOracleCommand == null)
                    _thisOracleCommand = OraclelConnection1.CreateCommand();
                _thisOracleCommand.CommandText = strcommand;
                OracleDataReader thisReader = _thisOracleCommand.ExecuteReader();
                dt.Load(thisReader);
                if (!thisReader.IsClosed)
                    thisReader.Close();
                _thisOracleCommand.Dispose();
                _thisOracleCommand = null;
                return dt;
            }
            return null;
        }

        public int ExecuteCommandNonQuery(string strcommand)
        {
            if (RefDBType == RefDBType.MSAccess
                || RefDBType == RefDBType.OleDB)
            {
                DataTable dt = new DataTable();
                OleDbConnection DbConnection = GetOleConnection();
                if (DbConnection.State != ConnectionState.Open)
                    return 0;
                if (_thisOleCommand == null)
                    _thisOleCommand = DbConnection.CreateCommand();
                _thisOleCommand.CommandText = strcommand;
                int iret = _thisOleCommand.ExecuteNonQuery();
                _thisOleCommand.Dispose();
                _thisOleCommand = null;
                return iret;
                
            }
            else if (RefDBType == RefDBType.MSSqlServer)
            {
                DataTable dt = new DataTable();
                SqlConnection SqlConnection1 = GetSqlConnection();
                if (SqlConnection1.State != ConnectionState.Open)
                    return 0;
                if (_thisSqlCommand == null)
                    _thisSqlCommand = SqlConnection1.CreateCommand();
                _thisSqlCommand.CommandText = strcommand;
                int iret = _thisSqlCommand.ExecuteNonQuery();
                _thisSqlCommand.Dispose();
                _thisSqlCommand = null;
                return iret;
            }
            else if (RefDBType == RefDBType.Oracle)
            {
                DataTable dt = new DataTable();
                OracleConnection OraclelConnection1 = GetOracleConnection();
                if (OraclelConnection1.State != ConnectionState.Open)
                    return 0;
                if (_thisOracleCommand == null)
                    _thisOracleCommand = OraclelConnection1.CreateCommand();
                _thisOracleCommand.CommandText = strcommand;
                int iret = _thisOracleCommand.ExecuteNonQuery();
                _thisOracleCommand.Dispose();
                _thisOracleCommand = null;
                return iret;
            }
            return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is PMSRefDBConnection && this == (PMSRefDBConnection)obj;
        }

        public override int GetHashCode()
        {
            return GetConnectionString().GetHashCode();
        }

        public static bool operator ==(PMSRefDBConnection x, PMSRefDBConnection y)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(x, y))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            return x.GetConnectionString() == y.GetConnectionString();
        }

        public static bool operator !=(PMSRefDBConnection x, PMSRefDBConnection y)
        {
            return !(x == y);
        }

        public object Clone()
        {
            PMSRefDBConnection copy = new PMSRefDBConnection();
            copy.StrSourceName = this.StrSourceName;
            copy.StrDBPath = this.StrDBPath;
            copy.StrServerName = this.StrServerName;
            copy.StrDBName = this.StrDBName;
            copy.StrUserID = this.StrUserID;
            copy.StrPassWord = this.StrPassWord;
            copy.EConnectType = this.EConnectType;
            copy.StrPortID = this.StrPortID;
            copy.StrConnectAs = this.StrConnectAs;
            copy.StrConnectMode = this.StrConnectMode;
            copy.Owner = this.Owner;
            copy.BDirect = this.BDirect;
            copy.ConnectString = this.ConnectString;
            copy.RefDBType = this.RefDBType;
            return copy;
        }
    }

    [Serializable]
    public class PMSRefDBConnectionObj : ICloneable
    {
        private string _strName = string.Empty;
        public string StrName
        {
            get { return _strName; }
            set { _strName = value; }
        }

        private string _strDescription = string.Empty;
        public string StrDescription
        {
            get { return _strDescription; }
            set { _strDescription = value; }
        }

        public bool _bDefault = false;
        public bool BDefault
        {
            get { return _bDefault; }
            set { _bDefault = value; }
        }

        private PMSRefDBConnection _refDBConnection = null;
        public PMS.Libraries.ToolControls.PMSPublicInfo.PMSRefDBConnection RefDBConnection
        {
            get { return _refDBConnection; }
            set { _refDBConnection = value; }
        }

        public override bool Equals(object obj)
        {
            return obj is PMSRefDBConnectionObj && this == (PMSRefDBConnectionObj)obj;
        }

        public override int GetHashCode()
        {
            return StrName.GetHashCode();
        }

        public static bool operator ==(PMSRefDBConnectionObj x, PMSRefDBConnectionObj y)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(x, y))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            return x.StrName == y.StrName && x.RefDBConnection == y.RefDBConnection;
        }

        public static bool operator !=(PMSRefDBConnectionObj x, PMSRefDBConnectionObj y)
        {
            return !(x == y);
        }

        public object Clone()
        {
            PMSRefDBConnectionObj copy = new PMSRefDBConnectionObj();
            copy.BDefault = this.BDefault;
            copy.StrName = this.StrName;
            copy.StrDescription = this.StrDescription;
            copy.RefDBConnection = this.RefDBConnection.Clone() as PMSRefDBConnection;
            return copy;
        }
    }

    public class PMSDBConnection
    {
        public static MESDBType MESDBType
        {
            get { return CurrentPrjInfo.CurrentDBType; }
            set { CurrentPrjInfo.CurrentDBType = value; }
        }

        public static bool InitialConnection(string connectstring ,MESDBType dbtype)
        {
            CurrentPrjInfo.CurrentDBType = dbtype;
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                SqlStructure.GetSqlConncetion(connectstring);
            }
            else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
            {
                OracleStructure.GetOracleConncetion(connectstring);
            }
            return true;
        }

        public static bool InitialConnection(DBConfigSetting xDS)
        {
            if (xDS == null) return false;//xds is null
            if (xDS.MESDBType.Rows.Count > 0)
            {
                DBConfigSetting.MESDBTypeRow dRow = xDS.MESDBType[0];
                if (!dRow.IsTypeNull())
                {
                    if (dRow.Type == 0)
                    {
                        CurrentPrjInfo.CurrentDBType = MESDBType.MSSqlServer;
                    }
                    else if (dRow.Type == 1)
                    {
                        CurrentPrjInfo.CurrentDBType = MESDBType.Oracle;
                    }
                }
            }
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                if (xDS.MSSQLServer.Rows.Count > 0)
                {
                    DBConfigSetting.MSSQLServerRow xRow = xDS.MSSQLServer[0];
                   
                    if (xRow.IsServerNameNull() || xRow.IsDBNameNull() || xRow.IsUserIDNull() || xRow.IsPassWordNull() || xRow.IsConnectTypeNull() || xRow.IsPortIDNull())
                    {
                        return false;
                    }

                    ConnectType iProtocolType = (ConnectType)(xRow.ConnectType);

                    try
                    {
                        //new SqlStructure(xRow.ServerName, xRow.DBName, xRow.UserID, xRow.PassWord, iProtocolType, xRow.PortID);
                        //SqlStructure.InitSqlStructure(xRow.ServerName, xRow.DBName, xRow.UserID, xRow.PassWord, iProtocolType, xRow.PortID);
                        SqlStructureHolder holder = new SqlStructureHolder();
                        holder.InitSqlStructure(xRow.ServerName, xRow.DBName, xRow.UserID, xRow.PassWord, iProtocolType, xRow.PortID);
                        holder.GetSqlConncetion();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        return false;
                    }
                }
            }
            else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
            {
                if (xDS.Oracle.Rows.Count > 0)
                {
                    DBConfigSetting.OracleRow oRow = xDS.Oracle[0];
                    if (oRow.IsServerNameNull()
                        || oRow.IsUserIDNull()
                        || oRow.IsPassWordNull()
                        || oRow.IsConnectAsNull()
                        || oRow.IsHomeNull()
                        || oRow.IsSIDNull()
                        || oRow.IsPortIDNull()
                        || oRow.IsOwnerNull()
                        || oRow.IsDirectNull())
                    {
                        return false;
                    }

                    try
                    {
                        //new OracleStructure(oRow.ServerName, oRow.SID, oRow.UserID, oRow.PassWord, oRow.Direct, oRow.PortID, oRow.ConnectAs, oRow.Owner);
                        //OracleStructure.GetOracleConncetion();
                        OracleStructureHolder holder = new OracleStructureHolder();
                        holder.InitOracleStructure(oRow.ServerName, oRow.SID, oRow.UserID, oRow.PassWord, oRow.Direct, oRow.PortID, oRow.ConnectAs, oRow.Owner);
                        holder.GetOracleConncetion();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static SqlConnection GetSqlConncetion(bool wantNewTempConnection = false)
        {
            return SqlStructure.GetSqlConncetion(wantNewTempConnection);
        }

        private static OracleConnection GetOracleConncetion(bool wantNewTempConnection = false)
        {
            return OracleStructure.GetOracleConncetion(wantNewTempConnection);
        }

        public static IDbConnection GetConnection(bool wantNewTempConnection = false)
        {
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                return GetSqlConncetion(wantNewTempConnection);
            }
            else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
            {
                return GetOracleConncetion(wantNewTempConnection);
            }

            return null;
        }

        public static string GetConnectionString()
        {
            return SqlStructureHolder.ConnectString;
        }

        private static SqlTransaction BeginSqlTransaction()
        {
            return SqlStructure.GetSqlConncetion().BeginTransaction();
        }

        private static OracleTransaction BeginOracleTransaction()
        {
            return OracleStructure.GetOracleConncetion().BeginTransaction();
        }

        public static System.Data.Common.DbTransaction BeginTransaction()
        {
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                return PMSDBConnectionHolder.Trans = BeginSqlTransaction();
            }
            else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
            {
                return PMSDBConnectionHolder.Trans = BeginOracleTransaction();
            }

            return null;
        }

        public static bool Commit()
        {
            if (PMSDBConnectionHolder.Trans == null)
                return false;

            try
            {
                PMSDBConnectionHolder.Trans.Commit();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public static bool Rollback()
        {
            if (PMSDBConnectionHolder.Trans == null)
                return false;

            try
            {
                PMSDBConnectionHolder.Trans.Rollback();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private static DataTable ExecuteSqlCommand(string strcommand)
        {
            DataTable dt = new DataTable();
            SqlConnection SqlConnection1 = (SqlConnection)(GetConnection());
            if (SqlConnection1.State != ConnectionState.Open)
                return null;
            SqlCommand _thisCommand = null;
            if (_thisCommand == null)
                _thisCommand = SqlConnection1.CreateCommand();
            string cmd = string.Format(strcommand, "");
            _thisCommand.CommandText = cmd;
            if (PMSDBConnectionHolder.Trans != null)
                _thisCommand.Transaction = (SqlTransaction)PMSDBConnectionHolder.Trans;
            SqlDataReader thisReader = _thisCommand.ExecuteReader();
            dt.Load(thisReader);
            if (!thisReader.IsClosed)
                thisReader.Close();
            _thisCommand.Dispose();
            _thisCommand = null;
            return dt;
        }

        private static DataTable ExecuteOracleCommand(string strcommand)
        {
            DataTable dt = new DataTable();
            OracleConnection OracleConnection1 = (OracleConnection)(GetConnection());
            if (OracleConnection1.State != ConnectionState.Open)
                return null;
            if (PMSDBConnectionHolder.ThisOCommand == null)
                PMSDBConnectionHolder.ThisOCommand = OracleConnection1.CreateCommand();
            string cmd = string.Format(strcommand, OracleStructureHolder.OwnerDot);
            PMSDBConnectionHolder.ThisOCommand.CommandText = cmd;
            if (PMSDBConnectionHolder.Trans != null)
                PMSDBConnectionHolder.ThisOCommand.Transaction = (OracleTransaction)PMSDBConnectionHolder.Trans;
            OracleDataReader thisReader = PMSDBConnectionHolder.ThisOCommand.ExecuteReader();
            dt.Load(thisReader);
            if (!thisReader.IsClosed)
                thisReader.Close();
            PMSDBConnectionHolder.ThisOCommand.Dispose();
            PMSDBConnectionHolder.ThisOCommand = null;
            return dt;
        }

        public static DataTable ExecuteCommand(string strcommand)
        {
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                return ExecuteSqlCommand(strcommand);
            }
            else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
            {
                return ExecuteOracleCommand(strcommand);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strcommand"></param>
        /// <param name="dataSourceName">数据源名</param>
        /// <returns></returns>
        public static DataTable ExecuteCommand(string strcommand,string dataSourceName)
        {
            List<PMSRefDBConnectionObj> rdcList = CurrentPrjInfo.GetRefDBConnectionObjList();
            PMSDBConnectionHolder.StrCompare = dataSourceName;
            PMSRefDBConnectionObj rco = rdcList.Find(FindRefDBConnectionObj);

            if (null == rco)
                return null;
            return rco.RefDBConnection.ExecuteCommand(strcommand);
        }

        private static bool FindRefDBConnectionObj(PMSRefDBConnectionObj rco)
        {
            if (rco.StrName == PMSDBConnectionHolder.StrCompare)
            {
                return true;
            }
            return false;
        }

        private static int ExecuteSqlCommandNonQuery(string strcommand)
        {
            SqlConnection SqlConnection1 = (SqlConnection)(GetConnection());
            if (SqlConnection1.State != ConnectionState.Open)
                return 0;
            if (PMSDBConnectionHolder.ThisCommand == null)
                PMSDBConnectionHolder.ThisCommand = SqlConnection1.CreateCommand();
            string cmd = string.Format(strcommand, "");
            PMSDBConnectionHolder.ThisCommand.CommandText = cmd;
            if (PMSDBConnectionHolder.Trans != null)
                PMSDBConnectionHolder.ThisCommand.Transaction = (SqlTransaction)PMSDBConnectionHolder.Trans;
            int ret = PMSDBConnectionHolder.ThisCommand.ExecuteNonQuery();
            PMSDBConnectionHolder.ThisCommand.Dispose();
            PMSDBConnectionHolder.ThisCommand = null;
            return ret;
        }

        private static int ExecuteOracleCommandNonQuery(string strcommand)
        {
            OracleConnection OracleConnection1 = (OracleConnection)(GetConnection());
            if (OracleConnection1.State != ConnectionState.Open)
                return 0;
            if (PMSDBConnectionHolder.ThisOCommand == null)
                PMSDBConnectionHolder.ThisOCommand = OracleConnection1.CreateCommand();
            string cmd = string.Format(strcommand, OracleStructureHolder.OwnerDot);
            PMSDBConnectionHolder.ThisOCommand.CommandText = cmd;
            if (PMSDBConnectionHolder.Trans != null)
                PMSDBConnectionHolder.ThisOCommand.Transaction = (OracleTransaction)PMSDBConnectionHolder.Trans;
            int ret = PMSDBConnectionHolder.ThisOCommand.ExecuteNonQuery();
            PMSDBConnectionHolder.ThisOCommand.Dispose();
            PMSDBConnectionHolder.ThisOCommand = null;
            return ret;
        }

        public static int ExecuteCommandNonQuery(string strcommand)
        {
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                return ExecuteSqlCommandNonQuery(strcommand);
            }
            else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
            {
                return ExecuteOracleCommandNonQuery(strcommand);
            }
            return 0;
        }

        public static int ExecuteCommandNonQuery(string strcommand, string dataSourceName)
        {
            List<PMSRefDBConnectionObj> rdcList = CurrentPrjInfo.GetRefDBConnectionObjList();
            PMSDBConnectionHolder.StrCompare = dataSourceName;
            PMSRefDBConnectionObj rco = rdcList.Find(FindRefDBConnectionObj);

            if (null == rco)
                return 0;
            return rco.RefDBConnection.ExecuteCommandNonQuery(strcommand);
        }

        public static bool ExecuteSqlFile(string varFileFullName)
        {
            if (!File.Exists(varFileFullName))
            {
                return false;
            }
            StreamReader rs = new StreamReader(varFileFullName, System.Text.Encoding.Default);//注意编码
            ArrayList alSql = new ArrayList();
            string commandText = "";
            string varLine = "";
            while (rs.Peek() > -1)
            {
                varLine = rs.ReadLine();
                if (varLine == "")
                {
                    continue;
                }
                if (varLine != "GO")
                {
                    commandText += varLine;
                    commandText += "\r\n";
                }
                else
                {
                     commandText += "";
                     alSql.Add(commandText);
                }
            }
            rs.Close();
            try
            {
                ExecuteCommand(alSql);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool ExecuteSqlString(string varSqlString)
        {
            string[] split = varSqlString.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int count = split.Count();
            ArrayList alSql = new ArrayList();
            string commandText = string.Empty;
            for (int i = 0; i < count;i++ )
            {
                string varLine = split[i];
                if (varLine == "")
                {
                    continue;
                }
                if (varLine != "GO")
                {
                    commandText += varLine;
                    commandText += "\r\n";
                }
                else
                {
                    commandText += "";
                    alSql.Add(commandText);
                    commandText = string.Empty;
                }
            }
            try
            {
                ExecuteCommand(alSql);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void ExecuteSqlCommand(ArrayList varSqlList)
        {
            SqlConnection SqlConnection1 = (SqlConnection)(GetConnection());
            SqlTransaction varTrans = SqlConnection1.BeginTransaction();
            SqlCommand command = new SqlCommand();
            command.Connection = SqlConnection1;
            command.Transaction = varTrans;
            try
            {
                foreach (string varcommandText in varSqlList)
                {
                    command.CommandText = varcommandText;
                    command.ExecuteNonQuery();
                }
                varTrans.Commit();
                command.Dispose();
            }
            catch (Exception ex)
            {
                varTrans.Rollback();
                command.Dispose();
                throw ex;
            }
        }

        private static void ExecuteOracleCommand(ArrayList varSqlList)
        {
            OracleConnection OracleConnection1 = (OracleConnection)(GetConnection());
            OracleTransaction varTrans = OracleConnection1.BeginTransaction();
            OracleCommand command = new OracleCommand();
            command.Connection = OracleConnection1;
            command.Transaction = varTrans;
            try
            {
                foreach (string varcommandText in varSqlList)
                {
                    command.CommandText = varcommandText;
                    command.ExecuteNonQuery();
                }
                varTrans.Commit();
                command.Dispose();
            }
            catch (Exception ex)
            {
                varTrans.Rollback();
                command.Dispose();
                throw ex;
            }
        }

        private static void ExecuteCommand(ArrayList varSqlList)
        {
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                ExecuteSqlCommand(varSqlList);
            }
            else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
            {
                ExecuteOracleCommand(varSqlList);
            }
        }

        private static SqlParameter AddSqlParameter(DbParameter value)
        {
            return PMSDBConnection.AddParameter((SqlParameter)value);
        }

        private static OracleParameter AddOracleParameter(DbParameter value)
        {
            return PMSDBConnection.AddParameter((OracleParameter)value);
        }

        public static DbParameter AddParameter(DbParameter value)
        {
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                return AddSqlParameter(value);
            }
            else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
            {
                return AddOracleParameter(value);
            }
            return null;
        }

        private static SqlParameter AddSqlParameter(string parameterName, int DbType)
        {
            return PMSDBConnection.AddParameter(parameterName, (SqlDbType)DbType);
        }

        private static OracleParameter AddOracleParameter(string parameterName, int DbType)
        {
            return PMSDBConnection.AddParameter(parameterName, (OracleDbType)DbType);
        }

        public static DbParameter AddParameter(string parameterName, int DbType)
        {
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                return AddSqlParameter(parameterName, DbType);
            }
            else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
            {
                return AddOracleParameter(parameterName, DbType);
            }
            return null;
        }

        private static SqlParameter AddParameter(SqlParameter value)
        {
            try
            {
                SqlConnection SqlConnection1 = (SqlConnection)(GetConnection());
                if (SqlConnection1.State != ConnectionState.Open)
                    return null;
                if (PMSDBConnectionHolder.ThisCommand == null)
                    PMSDBConnectionHolder.ThisCommand = SqlConnection1.CreateCommand();
                return PMSDBConnectionHolder.ThisCommand.Parameters.Add(value);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private static SqlParameter AddParameter(string parameterName, SqlDbType sqlDbType)
        {
            try
            {
                SqlConnection SqlConnection1 = (SqlConnection)(GetConnection());
                if (SqlConnection1.State != ConnectionState.Open)
                    return null;
                if (PMSDBConnectionHolder.ThisCommand == null)
                    PMSDBConnectionHolder.ThisCommand = SqlConnection1.CreateCommand();
                return PMSDBConnectionHolder.ThisCommand.Parameters.Add(parameterName, sqlDbType);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private static OracleParameter AddParameter(OracleParameter value)
        {
            try
            {
                OracleConnection OracleConnection1 = (OracleConnection)(GetConnection());
                if (OracleConnection1.State != ConnectionState.Open)
                    return null;
                if (PMSDBConnectionHolder.ThisOCommand == null)
                    PMSDBConnectionHolder.ThisOCommand = OracleConnection1.CreateCommand();
                return PMSDBConnectionHolder.ThisOCommand.Parameters.Add(value);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private static OracleParameter AddParameter(string parameterName, OracleDbType oracleDbType)
        {
            if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
            {
                OracleConnection OracleConnection1 = (OracleConnection)(GetConnection());
                if (OracleConnection1.State != ConnectionState.Open)
                    return null;
                if (PMSDBConnectionHolder.ThisOCommand == null)
                    PMSDBConnectionHolder.ThisOCommand = OracleConnection1.CreateCommand();
                return PMSDBConnectionHolder.ThisOCommand.Parameters.Add(parameterName, oracleDbType);
            }
            return null;
        }

        public static bool TestConnection(IDbConnection conn)
        {
            try
            {
                if (null == conn)
                    return false;

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                IDbCommand comm = conn.CreateCommand();
                comm.CommandTimeout = 5;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "select 0";
                comm.ExecuteNonQuery();
                comm.Dispose();
                comm = null;
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }
    }

    public class PMSDBConnectionHolder
    {
        private static SqlCommand _thisCommand = null;
        public static System.Data.SqlClient.SqlCommand ThisCommand
        {
            get { return _thisCommand; }
            set { _thisCommand = value; }
        }
        private static OracleCommand _thisOCommand = null;
        public static Oracle.DataAccess.Client.OracleCommand ThisOCommand
        {
            get { return _thisOCommand; }
            set { _thisOCommand = value; }
        }
        private static System.Data.Common.DbTransaction _trans = null;
        public static System.Data.Common.DbTransaction Trans
        {
            get { return _trans; }
            set { _trans = value; }
        }
        private static string _strCompare = string.Empty;
        public static string StrCompare
        {
            get { return _strCompare; }
            set { _strCompare = value; }
        }

        //public static MESDBType MESDBType
        //{
        //    get { return CurrentPrjInfo.CurrentDBType; }
        //    set { CurrentPrjInfo.CurrentDBType = value; }
        //}

        //public static SqlParameterCollection Parameters
        //{
        //    get
        //    {
        //        if (PMSDBConnectionHolder.ThisCommand != null)
        //            return PMSDBConnectionHolder.ThisCommand.Parameters;
        //        return null;
        //    }
        //}

        public bool InitialConnection(DBConfigSetting xDS)
        {
            return PMSDBConnection.InitialConnection(xDS);
        }

        public void SetEntityConnectionRepair(bool enable = true)
        {
            PMSDBStructure.EnableEntityConnectionRepair = enable;
        }
    }

    public class PMSDBStructure
    {
        /// <summary>
        /// 表类型
        /// </summary>
        public enum TableType
        {
            NONE,
            Table,
            MapTable,
            InfoTable,
            IIRelationTable,
            IMRelationTable
        }
        #region property
        [ThreadStatic]
        private static MESCenterEntities _PMSCenterDataContext = null;

        private static object _lockObj = new object();

        public static string ConnectString
        {
            get 
            {
                if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
                    return SqlStructureHolder.ConnectString;
                else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
                    return OracleStructureHolder.ConnectString;
                else
                    return null;
            }
        }

        [ThreadStatic]
        private static EntityConnection econn = null;

        /// <summary>
        /// 外部实例化entity使用
        /// </summary>
        public static EntityConnection EConn
        {
            get { return econn; }
            set { econn = value; }
        }

        private static string EntityConnectString = string.Empty;

        private static bool TestEntityConnection()
        {
            try
            {
                if (_PMSCenterDataContext.Connection.State == ConnectionState.Broken || _PMSCenterDataContext.Connection.State == ConnectionState.Closed)
                    return false;
                
                var query = from FlagTable in _PMSCenterDataContext.s_FlagTable
                            select FlagTable.DBVersion;

                var q = query.First();
                return true;
            }
            catch(EntityCommandExecutionException ex)
            {
                
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static bool EnableEntityConnectionRepair = false;

        public static MESCenterEntities PMSCenterDataContext
        {
            get
            {
                if (EnableEntityConnectionRepair)
                {
                    lock (_lockObj)
                    {
                        if (null != econn)
                        {
                            if (econn.State == ConnectionState.Closed)
                                econn.ConnectionString = EntityConnectString;
                            if (!TestEntityConnection())
                            {
                                // 原数据库连接无效，创建新的数据库连接
                                econn.Close();
                                econn.Dispose();
                                econn = new EntityConnection(EntityConnectString); //创建连接
                                if (econn.State == ConnectionState.Closed)
                                    econn.Open();
                                if (null != _PMSCenterDataContext)
                                {
                                    _PMSCenterDataContext.Dispose();
                                    _PMSCenterDataContext = null;
                                }
                                _PMSCenterDataContext = new MESCenterEntities(econn);
                                //throw new Exception(string.Format("Db connection [{0}] can not established,please try again!", econn.ConnectionString));
                            }
                        }
                    }
                }

                if (_PMSCenterDataContext == null)
                {
                    if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
                        EntityConnectString = MESEntityHelp.GetEntityConnectString(DatabaseProvider.SqlProvider, ConnectString);
                    else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
                        //EntityConnectString = "metadata=./MESEntityForOracle.csdl|./MESEntityForOracle.ssdl|./MESEntityForOracle.msl;provider=Devart.Data.Oracle;provider connection string=\"User Id=dbo_mescenter;Password=123;Data Source=netscada;Connect Timeout=30;\"";
                        //EntityConnectString = "provider=EFOracleProvider;metadata=./MESEntity.csdl|./MESEntity.ssdl|./MESEntity.msl;provider connection string=\"User Id=dbo_MESCenter;Password=123;Data Source=NetSCADA;DBA Privilege=SysDba;\"";
                        //EntityConnectString = MESEntityHelp.GetOracleEntityConnectString(DatabaseProvider.OracleDevartProvider, ConnectString);
                        EntityConnectString = MESEntityHelp.GetOracleEntityConnectString(DatabaseProvider.OracleProvider, ConnectString);
                    //EntityConnectString = MESEntityHelp.GetEntityConnectString(DatabaseProvider.EFOracleProvider, ConnectString);
                    if (null == econn)
                    {
                        econn = new EntityConnection(EntityConnectString); //创建连接
                    }
                    if (econn.State == ConnectionState.Closed)
                        econn.Open();

                    _PMSCenterDataContext = new MESCenterEntities(econn);
                }
                return _PMSCenterDataContext;
            }
            set 
            {
                _PMSCenterDataContext = value; 
            }
        }

        #endregion

        private delegate void InvokeGetDatabaseTablesEventHandler(TreeView tree, TreeNode tablesNode, ImageList imageList, int tableImageIndex);
        public static void GetDatabaseTables(TreeView tree, TreeNode tablesNode, ImageList imageList, int tableImageIndex)
        {
            if (tree.InvokeRequired)
            {
                tree.Invoke(new InvokeGetDatabaseTablesEventHandler(GetDatabaseTables), new object[] { tree, tablesNode, imageList, tableImageIndex });
            }
            else
            {
                if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
                    SqlStructure.GetDatabaseTables(tree, tablesNode, imageList, tableImageIndex);
                else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
                    OracleStructure.GetDatabaseTables(tree, tablesNode, imageList, tableImageIndex);
            }
        }
        /// <summary>
        /// 获取表类型
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns>enum TableType</returns>
        public static TableType GetTableType(string tableName)
        {
            string strProp = GetTablePropertie(tableName, TablePropertyName.TableType);
            if (strProp == null)
                return TableType.NONE;
            TableType retType = TableType.NONE;
            switch (strProp)
            {
                case PMS.Libraries.ToolControls.PMSPublicInfo.TableType.Table: retType = TableType.Table; break;
                case PMS.Libraries.ToolControls.PMSPublicInfo.TableType.MapTable: retType = TableType.MapTable; break;
                case PMS.Libraries.ToolControls.PMSPublicInfo.TableType.InfoTable: retType = TableType.InfoTable; break;
                case PMS.Libraries.ToolControls.PMSPublicInfo.TableType.IMRelationTable: retType = TableType.IMRelationTable; break;
                case PMS.Libraries.ToolControls.PMSPublicInfo.TableType.IIRelationTable: retType = TableType.IIRelationTable; break;
                default: break;
            }
            return retType;
        }

        public static List<string> GetAllTables()
        {
            var TableInfos = from tableinfo in PMSDBStructure.PMSCenterDataContext.s_TableInfo
                             orderby tableinfo.TableName
                              select tableinfo.TableName;
            return TableInfos.ToList();
        }

        public static List<string> GetInfoTables()
        {
            var TableInfos = from tableinfo in PMSDBStructure.PMSCenterDataContext.View_GetInfoTable
                             orderby tableinfo.TableName
                             select tableinfo.TableName;
            return TableInfos.ToList();
        }

        public static List<string> GetMapTables()
        {
            var TableInfos = from tableinfo in PMSDBStructure.PMSCenterDataContext.View_GetMapTable
                             orderby tableinfo.TableName
                             select tableinfo.TableName;
            return TableInfos.ToList();
        }

        public static List<string> GetMapAndInfoTables()
        {
            var Tables = from tableinfo in PMSDBStructure.PMSCenterDataContext.View_GetMapAndInfoTable
                             orderby tableinfo.TableName
                             select tableinfo.TableName;
            return Tables.ToList();
        }

        public static DataTable GetIITableRelations()
        {
            return PMSDBConnection.ExecuteCommand("SELECT * FROM {0}[View_GetIITableRelation]");
        }

        public static string GetIIRelationTable(string strInfoTableName, string strInfoTableName1)
        {
            var relationTable = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetIITableRelation
                                where (infotable.InfoTable1 == strInfoTableName1 && infotable.InfoTable == strInfoTableName) ||
                                (infotable.InfoTable1 == strInfoTableName && infotable.InfoTable == strInfoTableName1)
                                orderby infotable.RelationTable
                                select infotable.RelationTable;
            if (relationTable.Count() == 0)
                return null;
            return relationTable.First().ToString();
        }

        public static List<string> GetInfoTablesFromInfoTable(string strInfotableName)
        {
            var infoTables = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetIITableRelation
                             where infotable.InfoTable == strInfotableName || infotable.InfoTable1 == strInfotableName
                             orderby infotable.InfoTable1
                             select infotable.InfoTable1;
            return infoTables.ToList();
        }

        public static List<string> GetRelationTablesFrom2InfoTable(string strInfotableName ,string strInfoTableName1)
        {
            var infoTables = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetIITableRelation
                             where infotable.InfoTable == strInfotableName && infotable.InfoTable1 == strInfoTableName1
                             orderby infotable.RelationTable
                             select infotable.RelationTable;
            return infoTables.ToList();
        }

        public static List<string> GetRelationTablesFromInfoTable(string strInfotableName)
        {
            var infoTables = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetIITableRelation
                             where infotable.InfoTable == strInfotableName
                             orderby infotable.RelationTable
                             select infotable.RelationTable;
            return infoTables.ToList();
        }

        public static List<string> GetAllIIRelationTables(string strInfotableName)
        {
            var infoTables = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetIITableRelation
                             where infotable.InfoTable == strInfotableName
                             orderby infotable.RelationTable
                             select infotable.RelationTable;
            return infoTables.ToList();
        }

        public static DataTable GetMITableRelations()
        {
            return PMSDBConnection.ExecuteCommand("SELECT * FROM {0}[View_GetMITableRelation]");
        }

        public static string GetMIRelationTable(string strMapTableName,string strInfoTableName)
        {
            var relationTable = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetMITableRelation
                                where infotable.TableName == strMapTableName && infotable.InfoTable == strInfoTableName
                                orderby infotable.RelationTable
                                select infotable.RelationTable;
            if (relationTable.Count() == 0)
                return null;
            return relationTable.First().ToString();
        }

        public static List<string> GetInfoTablesFromMapTable(string strMaptableName)
        {
            var infoTables = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetMITableRelation
                             where infotable.TableName == strMaptableName
                             orderby infotable.InfoTable
                             select infotable.InfoTable;
            return infoTables.ToList();
        }

        public static List<string> GetMapTablesFromInfoTable(string strInfotableName)
        {
            var infoTables = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetMITableRelation
                             where infotable.InfoTable == strInfotableName
                             orderby infotable.TableName
                             select infotable.TableName;
            return infoTables.ToList();
        }

        public static DataTable GetTableFieldInfo()
        {
            return PMSDBConnection.ExecuteCommand("SELECT * FROM {0}[View_GetTableFieldInfo] ORDER BY [TableName], [FieldID]");
        }

        /// <summary>
        /// SELECT [TableName]
        ///   ,[FieldID]
        ///   ,[FieldName]
        ///   ,[FieldType]
        ///   ,[FieldLength]
        ///   ,[FieldNullAble]
        ///   ,[FieldPrimaryKey]
        ///   ,[FieldIdentity]
        ///   ,[FieldDefault]
        ///   ,[FieldDescription]
        ///FROM [PMSCenter].[dbo].[View_GetTableFieldInfo]
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public static DataTable GetTableFieldInfo(string strTableName)
        {
            return PMSDBConnection.ExecuteCommand("SELECT * FROM {0}[View_GetTableFieldInfo] WHERE [TableName] = '" + strTableName + "' ORDER BY [FieldID]");
        }

        public static DataTable GetTableFieldProp()
        {
            return PMSDBConnection.ExecuteCommand("SELECT * FROM {0}[View_GetTableFieldProp] ORDER BY [TableName]");
        }

        public static string GetTablePropertie(string tableName, string propertyName)
        {
            var tableprops = from tableprop in PMSDBStructure.PMSCenterDataContext.s_TableProperty
                             where MESCenterFunctions.fn_GetTableNameFromGuid(tableprop.TableGuid) == tableName &&
                             tableprop.PropertyName == propertyName
                             select tableprop.PropertyValue;
            if (tableprops.Count() == 0)
                return null;
            return tableprops.First();
        }

        public static string GetTableDescription(string tableName)
        {
            return GetTablePropertie(tableName, PMS.Libraries.ToolControls.PMSPublicInfo.TablePropertyName.Description);
        }

        public static string GetTablePrimaryColumn(string tableName)
        {
            var tablefieldinfos = from tablefieldinfo in PMSDBStructure.PMSCenterDataContext.View_GetTableFieldInfo
                                  where tablefieldinfo.TableName == tableName &&
                                  tablefieldinfo.FieldPrimaryKey == true 
                                  select tablefieldinfo.FieldName;
            if (tablefieldinfos.Count() == 0)
                return null;
            return tablefieldinfos.First();
        }

        public static string GetTableColumnPropertie(string tableName, string columnName, string propertyName)
        {
            var tablefieldprops = from tablefieldprop in PMSDBStructure.PMSCenterDataContext.View_GetTableFieldProp
                                  where tablefieldprop.TableName == tableName &&
                                  tablefieldprop.FieldName == columnName &&
                                  tablefieldprop.PropertyName == propertyName
                                  select tablefieldprop.PropertyValue;
            if (tablefieldprops.Count() == 0)
                return null;
            return tablefieldprops.First();
        }

        public static string GetTableColumnDescription(string tableName, string columnName)
        {
            //            if (CurrentPrjInfo.IsIndependentDesignerMode)
//            {
//                string strsql =
//                @"SELECT     TOP (100) PERCENT dbo.s_TableInfo.TableName, dbo.s_TableFieldInfo.FieldID, dbo.s_TableFieldInfo.FieldName, dbo.s_TableFieldInfo.FieldType, 
//                      dbo.s_TableFieldInfo.FieldLength, dbo.s_TableFieldInfo.FieldNullAble, dbo.s_TableFieldInfo.FieldPrimaryKey, dbo.s_TableFieldInfo.FieldIdentity, 
//                      dbo.s_TableFieldInfo.FieldIsSystem, dbo.s_TableFieldInfo.FieldDefault, dbo.s_TableFieldInfo.FieldDescription
//FROM         dbo.s_TableInfo LEFT OUTER JOIN
//                      dbo.s_TableFieldInfo ON dbo.s_TableInfo.TableGuid = dbo.s_TableFieldInfo.TableGuid
//ORDER BY dbo.s_TableInfo.TableName, dbo.s_TableFieldInfo.FieldID";

            
//            }

            var tablefieldinfos = from tablefieldinfo in PMSDBStructure.PMSCenterDataContext.View_GetTableFieldInfo
                                  where tablefieldinfo.TableName == tableName &&
                                  tablefieldinfo.FieldName == columnName
                                  select tablefieldinfo.FieldDescription;

            if (tablefieldinfos.Count() == 0)
                return null;
            return tablefieldinfos.First();
        }

        public static bool IsTableColumnSystem(string tableName, string columnName)
        {
            var tablefieldinfos = from tablefieldinfo in PMSDBStructure.PMSCenterDataContext.View_GetTableFieldInfo
                                  where tablefieldinfo.TableName == tableName &&
                                  tablefieldinfo.FieldName == columnName
                                  select tablefieldinfo.FieldIsSystem;
            if (tablefieldinfos.Count() == 0)
                return false;
            return tablefieldinfos.First().Value;
        }


        public static DataTable GetTableColumnProperties(string tableName, string columnName)
        {
            return PMSDBConnection.ExecuteCommand("SELECT [PropertyName],[PropertyValue] FROM {0}[View_GetTableFieldProp] WHERE [TableName] = '" + tableName + "' AND [FieldName] = '" + columnName + "'ORDER BY [TableName]");
        }

        public static string GetPMSDBVersion()
        {
            var query = from FlagTable in PMSDBStructure.PMSCenterDataContext.s_FlagTable
                        select FlagTable.DBVersion;

            if (query.Count() == 0)
            {
                return null;
            }

            var q = query.First();
            return q;
        }

        public static List<View_GetTableKeyRelation> GetTableKeyRelation()
        {
            var query = from TableKeyRelation in PMSDBStructure.PMSCenterDataContext.View_GetTableKeyRelation
                        orderby TableKeyRelation.PrimaryKeyTableName
                        select TableKeyRelation;

            if (query.Count() == 0)
            {
                return null;
            }

            var q = query.ToList();
            return q;
        }

        public static List<View_GetTableKeyRelation> GetTableKeyRelationFromPKTable(string PKTableName)
        {
            var query = from TableKeyRelation in PMSDBStructure.PMSCenterDataContext.View_GetTableKeyRelation
                        where TableKeyRelation.PrimaryKeyTableName == PKTableName
                        orderby TableKeyRelation.PrimaryKeyTableName
                        select TableKeyRelation;
                        

            if (query.Count() == 0)
            {
                return null;
            }

            var q = query.ToList();
            return q;
        }

        public static List<View_GetTableKeyRelation> GetTableKeyRelationFromFKTable(string FKTableName)
        {
            var query = from TableKeyRelation in PMSDBStructure.PMSCenterDataContext.View_GetTableKeyRelation
                        where TableKeyRelation.ForeignKeyTableName == FKTableName
                        orderby TableKeyRelation.PrimaryKeyTableName
                        select TableKeyRelation;

            if (query.Count() == 0)
            {
                return null;
            }

            var q = query.ToList();
            return q;
        }

        public static DataTable GetTableKeyRelationDataTable()
        {
            return PMSDBConnection.ExecuteCommand("SELECT [PrimaryKeyTableName],[PrimaryKeyFieldName],[ForeignKeyTableName],[ForeignKeyFieldName] FROM {0}[View_GetTableKeyRelation] ORDER BY [PrimaryKeyTableName]");
        }

        public static DataTable GetTableKeyRelationDataTableFromPKTable(string PKTableName)
        {
            return PMSDBConnection.ExecuteCommand("SELECT [PrimaryKeyTableName],[PrimaryKeyFieldName],[ForeignKeyTableName],[ForeignKeyFieldName] FROM {0}[View_GetTableKeyRelation] WHERE [PrimaryKeyTableName] = '" + PKTableName + "'ORDER BY [PrimaryKeyTableName]");
        }

        public static DataTable GetTableKeyRelationDataTableFromFKTable(string FKTableName)
        {
            return PMSDBConnection.ExecuteCommand("SELECT [PrimaryKeyTableName],[PrimaryKeyFieldName],[ForeignKeyTableName],[ForeignKeyFieldName] FROM {0}[View_GetTableKeyRelation] WHERE [ForeignKeyTableName] = '" + FKTableName + "'ORDER BY [PrimaryKeyTableName]");
        }

        public static void GetMapAndInfoTables(TreeNode tablesNode, int tableImageIndex)
        {
            try
            {
                // clear current nodes
                tablesNode.Nodes.Clear();

                // create a list of strings to hold name of tables
                List<string> tables = GetMapAndInfoTables();

                // iterate through list object and add each item (table name) to treeView
                foreach (string str in tables)
                {
                    // create a node to hold table name (child node or level 1)
                    // then add child named 'Objects' to add + mark for table node
                    TreeNode tableNode = new TreeNode(str);
                    tableNode.ImageIndex = tableImageIndex;
                    tableNode.SelectedImageIndex = tableImageIndex;
                    tableNode.Nodes.Add("Objects");

                    // add tableNode to tablesNode
                    tablesNode.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Dictionary<string, PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp> GetTableProp(string tablename)
        {
            try
            {
                var tableprops = from tableprop in PMSDBStructure.PMSCenterDataContext.View_GetTableProp
                                 where tableprop.TableName == tablename
                                 select tableprop;
                if (tableprops.Count() == 0)
                    return null;

                Dictionary<string, PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp> ExProps = new Dictionary<string, PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp>();

                foreach (View_GetTableProp p in tableprops)
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp exp = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp();
                    exp.StrPropName = p.PropertyName;
                    exp.StrPropValue = p.PropertyValue;
                    ExProps.Add(exp.StrPropName, exp);
                }
                return ExProps;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        public static Dictionary<string, PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp> GetTableFieldProp(string tablename,string fieldname)
        {
            try
            {
                var tablefieldprops = from tablefieldprop in PMSDBStructure.PMSCenterDataContext.View_GetTableFieldProp
                                      where tablefieldprop.TableName == tablename &&
                                      tablefieldprop.FieldName == fieldname
                                      select tablefieldprop;
                if (tablefieldprops.Count() == 0)
                    return null;

                Dictionary<string, PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp> ExProps = new Dictionary<string, PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp>();

                foreach (View_GetTableFieldProp p in tablefieldprops)
                {
                    PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp exp = new PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBExtendedProp();
                    exp.StrPropName = p.PropertyName;
                    exp.StrPropValue = p.PropertyValue;
                    ExProps.Add(exp.StrPropName, exp);
                }
                return ExProps;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
        }
        
        public static bool DeleteTable(string tablename)
        {
            try
            {
                if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
                    return SqlStructure.DeleteTable(tablename);
                else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
                    return OracleStructure.DeleteTable(tablename);
                //PMSDBConnection.ExecuteCommandNonQuery("DROP TABLE [" + tablename + "]");
                //return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static bool DeleteTableColumn(string tablename, string columnname)
        {
            try
            {
                if (CurrentPrjInfo.CurrentDBType == MESDBType.MSSqlServer)
                    return SqlStructure.DeleteTableColumn(tablename, columnname);
                else if (CurrentPrjInfo.CurrentDBType == MESDBType.Oracle)
                    return OracleStructure.DeleteTableColumn(tablename, columnname);
                //PMSDBConnection.ExecuteCommandNonQuery("ALTER TABLE [" + tablename + "] DROP COLUMN [" + columnname + "]");
                //if (PMSDBStructure.DeleteTableColumn(tablename, columnname))
                //    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static bool IsMITableRelationed(string maptable,string infotable)
        {
            if (GetMIRelationTable(maptable, infotable) == null)
                return false;
            return true;
        }

        public static bool IsIITableRelationed(string infotable, string infotable1)
        {
            if (GetIIRelationTable(infotable, infotable1) == null)
                return false;
            return true;
        }

        public static RelationStatus GetMITableRelationStatus(string strMapTableName, string strInfoTableName)
        {
            var relationTable = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetMITableRelation
                                where infotable.TableName == strMapTableName && infotable.InfoTable == strInfoTableName
                                orderby infotable.RelationTable
                                select infotable;
            if (relationTable.Count() == 0)
                return RelationStatus.None;
            View_GetMITableRelation mitr = (View_GetMITableRelation)(relationTable.First());
            if (mitr.Enable)
                return RelationStatus.Existed;
            else
                return RelationStatus.Disable;
        }

        public static List<View_GetMITableRelation> GetMITableRelationList(string strMapTableName, string strInfoTableName)
        {
            var relationTable = (from infotable in PMSDBStructure.PMSCenterDataContext.View_GetMITableRelation
                                where infotable.TableName == strMapTableName && infotable.InfoTable == strInfoTableName
                                orderby infotable.RelationTable
                                 select infotable).Distinct();//new MITableRelationEqualityComparer()
            if (relationTable.Count() == 0)
                return null;
            return relationTable.ToList();
        }

        public static RelationStatus GetIITableRelationStatus(string strInfoTableName, string strInfoTableName1)
        {
            var relationTable = from infotable in PMSDBStructure.PMSCenterDataContext.View_GetIITableRelation
                                where (infotable.InfoTable1 == strInfoTableName1 && infotable.InfoTable == strInfoTableName) ||
                                (infotable.InfoTable1 == strInfoTableName && infotable.InfoTable == strInfoTableName1)
                                orderby infotable.RelationTable
                                select infotable;
            if (relationTable.Count() == 0)
                return RelationStatus.None;
            View_GetIITableRelation iitr = (View_GetIITableRelation)(relationTable.First());
            if (iitr.Enable)
                return RelationStatus.Existed;
            else
                return RelationStatus.Disable;
        }

        public static List<View_GetIITableRelation> GetIITableRelationList(string strInfoTableName, string strInfoTableName1)
        {
            var relationTable = (from infotable in PMSDBStructure.PMSCenterDataContext.View_GetIITableRelation
                                where (infotable.InfoTable1 == strInfoTableName1 && infotable.InfoTable == strInfoTableName) ||
                                (infotable.InfoTable1 == strInfoTableName && infotable.InfoTable == strInfoTableName1)
                                orderby infotable.RelationTable
                                 select infotable).Distinct();//new IITableRelationEqualityComparer()
            if (relationTable.Count() == 0)
                return null;
            return relationTable.ToList();
        }


        public static bool UploadToSystable(string tablename,string tabledescription,string tabletype)
        {
            try
            {
                // 系统表
                // 1.PMSSys_TableInfo
                Guid newTableGuid = new Guid("ff0a5e58-5ca3-48bd-ac1d-226b21d751df");
                PMS.Libraries.ToolControls.PMSPublicInfo.s_TableInfo PMSSys_TableInfoObj = new PMS.Libraries.ToolControls.PMSPublicInfo.s_TableInfo();
                PMSSys_TableInfoObj.TableGuid = newTableGuid;
                PMSSys_TableInfoObj.TableName = tablename;
                PMS.Libraries.ToolControls.PMSPublicInfo.DBFileManager.UploadSysTable(PMSSys_TableInfoObj);

                // 2.PMSSys_TableProperty
                Guid newTablePropertyGuid = System.Guid.NewGuid();
                PMS.Libraries.ToolControls.PMSPublicInfo.s_TableProperty PMSSys_TablePropertyObj = new PMS.Libraries.ToolControls.PMSPublicInfo.s_TableProperty();
                PMSSys_TablePropertyObj.PropertyGuid = newTablePropertyGuid;
                PMSSys_TablePropertyObj.TableGuid = newTableGuid;
                PMSSys_TablePropertyObj.PropertyName = PMS.Libraries.ToolControls.PMSPublicInfo.TablePropertyName.TableType;
                PMSSys_TablePropertyObj.PropertyValue = tabletype;
                PMS.Libraries.ToolControls.PMSPublicInfo.DBFileManager.UploadTableProp(PMSSys_TablePropertyObj);

                newTablePropertyGuid = System.Guid.NewGuid();
                PMSSys_TablePropertyObj = new PMS.Libraries.ToolControls.PMSPublicInfo.s_TableProperty();
                PMSSys_TablePropertyObj.PropertyGuid = newTablePropertyGuid;
                PMSSys_TablePropertyObj.TableGuid = newTableGuid;
                PMSSys_TablePropertyObj.PropertyName = PMS.Libraries.ToolControls.PMSPublicInfo.TablePropertyName.Description;
                PMSSys_TablePropertyObj.PropertyValue = tabledescription;
                PMS.Libraries.ToolControls.PMSPublicInfo.DBFileManager.UploadTableProp(PMSSys_TablePropertyObj);

                // 获取表基本信息
                PMS.Libraries.ToolControls.PMSPublicInfo.PMSDBTableProp tp = PMS.Libraries.ToolControls.PMSPublicInfo.SqlStructure.GetPMSDBTableProp(tablename);

                // 3.根据获取的 PMSDBTableProp 插入相关字段信息至 PMSSys_TableFieldInfo
                if (PMS.Libraries.ToolControls.PMSPublicInfo.DBFileManager.UploadTableFieldInfo(tp.FieldPropCollection, newTableGuid))
                {
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }

            return true;
        }

        public static bool IsDBSourceNameExisted(string sourceName)
        {
            var tablefieldinfos = from tablefieldinfo in PMSDBStructure.PMSCenterDataContext.s_DBSourceDefine
                                  where tablefieldinfo.Name == sourceName
                                  select tablefieldinfo;
            if (tablefieldinfos.Count() == 0)
                return false;
            return true;
        }

        public static Guid fn_GetGuidFromTableName(
               string TableName)
        {
            var v = from t in PMSCenterDataContext.s_TableInfo
                    where t.TableName == TableName
                    select t.TableGuid;
            return v.First();
        }

        public static string fn_GetTableNameFromGuid(
               Guid TableGuid)
        {
            var v = from t in PMSCenterDataContext.s_TableInfo
                    where t.TableGuid == TableGuid
                    select t.TableName;
            return v.First();
        }

        //public static string fn_GetTablePrimaryKeyColumnName(
        //       string TableName)
        //{
            

        //}

        //public static string fn_GetTablePrimaryKeyColumnTypeName(
        //       string TableName)
        //{


        //}

        public static Guid fn_GetFieldGuidFromName(
               string TableName, string FieldName)
        {
            var v = from t in PMSCenterDataContext.s_TableFieldInfo
                    where t.TableGuid == MESCenterFunctions.fn_GetGuidFromTableName(TableName) && t.FieldName == FieldName
                    select t.FieldGuid;
            return v.First();
        }

        public static string fn_GetTableFieldNameFromGuid(
               Guid FieldGuid)
        {
            var v = from t in PMSCenterDataContext.s_TableFieldInfo
                    where t.FieldGuid == FieldGuid
                    select t.FieldName;
            return v.First();
        }
    }

    /// <summary>
    /// 引用数据源管理
    /// </summary>
    public class PMSRefDBConnectionManager
    {
        public PMSRefDBConnectionManager()
        {
            DBConnectionManager.EnableCache = false;
        }
        private object locker = new object();

        private string _lastError = string.Empty;
        #region 数据源连接管理
        private Dictionary<string, IDbConnection> _DbConnections = new Dictionary<string, IDbConnection>();

        public bool AddDBConnect(string name, IDbConnection con)
        {
            if (string.IsNullOrEmpty(name) || con == null)
            {
                return false;
            }
            string nameTemp = name.ToLower();
            if (!_DbConnections.ContainsKey(nameTemp))
            {
                lock (locker)
                {
                    _DbConnections[nameTemp] = con;
                }
                return true;
            }
            return false;
        }
        public bool DeleteDBConnect(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            string nameTemp = name.ToLower();
            if (_DbConnections.ContainsKey(nameTemp))
            {
                try
                {
                    _DbConnections[nameTemp].Close();
                }
                catch
                {
                }
                lock (locker)
                {
                    _DbConnections.Remove(nameTemp);
                }
                return true;
            }
            return false;
        }
        public IDbConnection GetDBConnect(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            string nameTemp = name.ToLower();
            if (_DbConnections.ContainsKey(nameTemp))
            {
                IDbConnection dbc = _DbConnections[nameTemp];
                if (TestConnection(dbc))
                    return dbc;
                else
                {
                    DeleteDBConnect(nameTemp);
                }
            }
            return null;
        }

        public void CloseAllDBConnects()
        {
            try
            {
                foreach (KeyValuePair<string, IDbConnection> con in _DbConnections)
                {
                    if (con.Value != null)
                    {
                        lock (locker)
                        {
                            con.Value.Close();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _lastError = ex.ToString();
            }
            finally
            {
                lock (locker)
                {
                    _DbConnections.Clear();
                }
            }
        }
        #endregion

        #region 数据库查询

        private List<PMSRefDBConnectionObj> _DBSourceConfigObjList = null;
        public List<PMSRefDBConnectionObj> DBSourceConfigObjList
        {
            get { return _DBSourceConfigObjList; }
            set { _DBSourceConfigObjList = value; }
        }

        public string GetLastError()
        {
            return _lastError;
        }

        public bool Initialize(string dataSourceCfgPath)
        {
            try
            {
                if (File.Exists(dataSourceCfgPath))
                {
                    ProjectPathClass.RefDBSourcesFilePath = dataSourceCfgPath;
                }
                _DBSourceConfigObjList = PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.GetRefDBConnectionObjListFromLocalFile(dataSourceCfgPath);
                CloseAllDBConnects();
            }
            catch (System.Exception ex)
            {
                string strError = string.Format("datasour config file initial fail:{0}", dataSourceCfgPath);
                _lastError = strError + ex.ToString();
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(_lastError);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 从数据源获取数据表
        /// </summary>
        /// <param name="dataSourceName">数据源名</param>
        /// <param name="sqlCmd">SQL语句</param>
        /// <returns></returns>
        public DataTable GetDataTableFromDataSource(string dataSourceName, string sqlCmd)
        {
            if (_DBSourceConfigObjList == null)
            {
                return null;
            }
            PMSRefDBConnectionObj curConnection = null;
            if (!string.IsNullOrEmpty(dataSourceName))
            {
                foreach (PMSRefDBConnectionObj db in _DBSourceConfigObjList)
                {
                    if (string.Compare(db.StrName, dataSourceName, true) == 0)
                    {
                        curConnection = db;
                        break;
                    }
                }
            }
            else//如果没有配置数据源，则启用默认数据源
            {
                foreach (PMSRefDBConnectionObj db in _DBSourceConfigObjList)
                {
                    if (db.BDefault)
                    {
                        curConnection = db;
                        break;
                    }
                }
            }

            if (curConnection == null || curConnection.RefDBConnection == null)
            {
                System.Exception ex = new System.Exception(string.Format("Don't found data source:{0}", dataSourceName));
                _lastError = ex.ToString();
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.ToString());
                return null;
            }

            try
            {
                lock (curConnection)
                {
                    IDbConnection tempDbConnection = GetDBConnect(curConnection.StrName);
                    if (tempDbConnection == null)
                    {
                        tempDbConnection = curConnection.RefDBConnection.GetConnection();
                        if (tempDbConnection == null)
                        {
                            System.Exception ex = new System.Exception(string.Format("Failed to connect data source:{0}", dataSourceName));
                            _lastError = ex.ToString();
                            PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex.ToString());
                            return null;
                        }
                        else
                        {
                            AddDBConnect(curConnection.StrName, tempDbConnection);
                        }
                    }

                    IDbCommand tempDbCommand = tempDbConnection.CreateCommand();
                    tempDbCommand.CommandText = sqlCmd;
                    IDataReader tempDbDataReader = tempDbCommand.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(tempDbDataReader);
                    tempDbDataReader.Close();
                    tempDbCommand.Dispose();
                    return dt;
                }
            }
            catch(DataException ex)
            {
                DeleteDBConnect(curConnection.StrName);
                System.Exception ex1 = new System.Exception(string.Format("Query error:{0}{1}", ex.Message, sqlCmd));
                _lastError = ex1.ToString();
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex1.ToString());
            }
            catch (Exception ex)
            {
                System.Exception ex1 = new System.Exception(string.Format("Query error:{0}{1}", ex.Message, sqlCmd));
                _lastError = ex1.ToString();
                PMS.Libraries.ToolControls.PMSPublicInfo.Message.Error(ex1.ToString());
            }
            return null;
        }

        public bool AddRefDBConnectionObj(PMSRefDBConnectionObj toAdd)
        {
            if(null == toAdd)
                return false;
            try
            {
                PMSRefDBConnectionObj finded = _DBSourceConfigObjList.Find(o => o.StrName == toAdd.StrName);
                if(null == finded)
                {
                    //Add
                    _DBSourceConfigObjList.Add(toAdd);
                }
                else
                {
                    if(finded.RefDBConnection != toAdd.RefDBConnection)
                    {
                        //Update
                        finded.RefDBConnection = toAdd.RefDBConnection;
                        DeleteDBConnect(finded.StrName);
                    }
                }
            }
            catch (System.Exception ex)
            {
                _lastError = ex.ToString();
                return false;
            }
            return true;
        }

        public bool Save()
        {
            try
            {
                if (null != _DBSourceConfigObjList)
                    return PMS.Libraries.ToolControls.PMSPublicInfo.CurrentPrjInfo.SaveRefDBConnectionObjList(_DBSourceConfigObjList);
            }
            catch (System.Exception ex)
            {
                _lastError = ex.ToString();
            }
            return false;
        }

        private bool TestConnection(IDbConnection conn)
        {
            try
            {
                if (null == conn)
                    return false;

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                IDbCommand comm = conn.CreateCommand();
                comm.CommandTimeout = 5;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "select 0";
                comm.ExecuteNonQuery();
                comm.Dispose();
                comm = null;
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}