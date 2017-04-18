namespace PMS.Libraries.ToolControls.PMSPublicInfo.DBSourceDefine
{
    partial class DBRefStructForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBRefStructForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_MSAccess = new System.Windows.Forms.TabPage();
            this.button_ConnectAccessTest = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_PRJPATH = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_MSAccessPath = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_MSAccessPass = new System.Windows.Forms.TextBox();
            this.textBox_MSAccessUID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage_MSSqlServer = new System.Windows.Forms.TabPage();
            this.button_ConnectTest = new System.Windows.Forms.Button();
            this.textBox_PortID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_Protocol = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_Server = new System.Windows.Forms.ComboBox();
            this.button_RefreshDB = new System.Windows.Forms.Button();
            this.button_RefreshServer = new System.Windows.Forms.Button();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.textBox_UserName = new System.Windows.Forms.TextBox();
            this.comboBox_DB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage_Oracle = new System.Windows.Forms.TabPage();
            this.comboBox_ConnectMode = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.checkBox_Direct = new System.Windows.Forms.CheckBox();
            this.comboBox_OracleHome = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBox_ConnectAs = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_OracleProtID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox_OracleServer = new System.Windows.Forms.ComboBox();
            this.button_ConnectOracleTest = new System.Windows.Forms.Button();
            this.button_RefreshOracleDB = new System.Windows.Forms.Button();
            this.button_RefreshOracleServer = new System.Windows.Forms.Button();
            this.textBox_Owner = new System.Windows.Forms.TextBox();
            this.textBox_OracleUserPass = new System.Windows.Forms.TextBox();
            this.textBox_OracleUserID = new System.Windows.Forms.TextBox();
            this.comboBox_SID = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage_OleDB = new System.Windows.Forms.TabPage();
            this.Btn_OleDBTest = new System.Windows.Forms.Button();
            this.button_BuildString = new System.Windows.Forms.Button();
            this.comboBox_ConnectString = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage_MSAccess.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage_MSSqlServer.SuspendLayout();
            this.tabPage_Oracle.SuspendLayout();
            this.tabPage_OleDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.button_Cancel);
            this.splitContainer1.Panel2.Controls.Add(this.button_OK);
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage_MSAccess);
            this.tabControl1.Controls.Add(this.tabPage_MSSqlServer);
            this.tabControl1.Controls.Add(this.tabPage_Oracle);
            this.tabControl1.Controls.Add(this.tabPage_OleDB);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage_MSAccess
            // 
            resources.ApplyResources(this.tabPage_MSAccess, "tabPage_MSAccess");
            this.tabPage_MSAccess.Controls.Add(this.button_ConnectAccessTest);
            this.tabPage_MSAccess.Controls.Add(this.button2);
            this.tabPage_MSAccess.Controls.Add(this.button1);
            this.tabPage_MSAccess.Controls.Add(this.textBox_MSAccessPath);
            this.tabPage_MSAccess.Controls.Add(this.label9);
            this.tabPage_MSAccess.Controls.Add(this.textBox_MSAccessPass);
            this.tabPage_MSAccess.Controls.Add(this.textBox_MSAccessUID);
            this.tabPage_MSAccess.Controls.Add(this.label5);
            this.tabPage_MSAccess.Controls.Add(this.label6);
            this.tabPage_MSAccess.Name = "tabPage_MSAccess";
            this.tabPage_MSAccess.UseVisualStyleBackColor = true;
            // 
            // button_ConnectAccessTest
            // 
            resources.ApplyResources(this.button_ConnectAccessTest, "button_ConnectAccessTest");
            this.button_ConnectAccessTest.Name = "button_ConnectAccessTest";
            this.button_ConnectAccessTest.UseVisualStyleBackColor = true;
            this.button_ConnectAccessTest.Click += new System.EventHandler(this.button_ConnectAccessTest_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.ContextMenuStrip = this.contextMenuStrip1;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_PRJPATH});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // toolStripMenuItem_PRJPATH
            // 
            resources.ApplyResources(this.toolStripMenuItem_PRJPATH, "toolStripMenuItem_PRJPATH");
            this.toolStripMenuItem_PRJPATH.Name = "toolStripMenuItem_PRJPATH";
            this.toolStripMenuItem_PRJPATH.Click += new System.EventHandler(this.toolStripMenuItem_PRJPATH_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_MSAccessPath
            // 
            resources.ApplyResources(this.textBox_MSAccessPath, "textBox_MSAccessPath");
            this.textBox_MSAccessPath.Name = "textBox_MSAccessPath";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // textBox_MSAccessPass
            // 
            resources.ApplyResources(this.textBox_MSAccessPass, "textBox_MSAccessPass");
            this.textBox_MSAccessPass.Name = "textBox_MSAccessPass";
            // 
            // textBox_MSAccessUID
            // 
            resources.ApplyResources(this.textBox_MSAccessUID, "textBox_MSAccessUID");
            this.textBox_MSAccessUID.Name = "textBox_MSAccessUID";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tabPage_MSSqlServer
            // 
            resources.ApplyResources(this.tabPage_MSSqlServer, "tabPage_MSSqlServer");
            this.tabPage_MSSqlServer.Controls.Add(this.button_ConnectTest);
            this.tabPage_MSSqlServer.Controls.Add(this.textBox_PortID);
            this.tabPage_MSSqlServer.Controls.Add(this.label8);
            this.tabPage_MSSqlServer.Controls.Add(this.comboBox_Protocol);
            this.tabPage_MSSqlServer.Controls.Add(this.label7);
            this.tabPage_MSSqlServer.Controls.Add(this.comboBox_Server);
            this.tabPage_MSSqlServer.Controls.Add(this.button_RefreshDB);
            this.tabPage_MSSqlServer.Controls.Add(this.button_RefreshServer);
            this.tabPage_MSSqlServer.Controls.Add(this.textBox_Password);
            this.tabPage_MSSqlServer.Controls.Add(this.textBox_UserName);
            this.tabPage_MSSqlServer.Controls.Add(this.comboBox_DB);
            this.tabPage_MSSqlServer.Controls.Add(this.label3);
            this.tabPage_MSSqlServer.Controls.Add(this.label2);
            this.tabPage_MSSqlServer.Controls.Add(this.label4);
            this.tabPage_MSSqlServer.Controls.Add(this.label1);
            this.tabPage_MSSqlServer.Name = "tabPage_MSSqlServer";
            this.tabPage_MSSqlServer.UseVisualStyleBackColor = true;
            // 
            // button_ConnectTest
            // 
            resources.ApplyResources(this.button_ConnectTest, "button_ConnectTest");
            this.button_ConnectTest.Name = "button_ConnectTest";
            this.button_ConnectTest.UseVisualStyleBackColor = true;
            this.button_ConnectTest.Click += new System.EventHandler(this.button_ConnectTest_Click);
            // 
            // textBox_PortID
            // 
            resources.ApplyResources(this.textBox_PortID, "textBox_PortID");
            this.textBox_PortID.Name = "textBox_PortID";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // comboBox_Protocol
            // 
            resources.ApplyResources(this.comboBox_Protocol, "comboBox_Protocol");
            this.comboBox_Protocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Protocol.FormattingEnabled = true;
            this.comboBox_Protocol.Items.AddRange(new object[] {
            resources.GetString("comboBox_Protocol.Items"),
            resources.GetString("comboBox_Protocol.Items1")});
            this.comboBox_Protocol.Name = "comboBox_Protocol";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // comboBox_Server
            // 
            resources.ApplyResources(this.comboBox_Server, "comboBox_Server");
            this.comboBox_Server.FormattingEnabled = true;
            this.comboBox_Server.Name = "comboBox_Server";
            this.comboBox_Server.DropDown += new System.EventHandler(this.comboBox_Server_DropDown);
            // 
            // button_RefreshDB
            // 
            resources.ApplyResources(this.button_RefreshDB, "button_RefreshDB");
            this.button_RefreshDB.Name = "button_RefreshDB";
            this.button_RefreshDB.UseVisualStyleBackColor = true;
            this.button_RefreshDB.Click += new System.EventHandler(this.button_RefreshDB_Click);
            // 
            // button_RefreshServer
            // 
            resources.ApplyResources(this.button_RefreshServer, "button_RefreshServer");
            this.button_RefreshServer.Name = "button_RefreshServer";
            this.button_RefreshServer.UseVisualStyleBackColor = true;
            this.button_RefreshServer.Click += new System.EventHandler(this.button_RefreshServer_Click);
            // 
            // textBox_Password
            // 
            resources.ApplyResources(this.textBox_Password, "textBox_Password");
            this.textBox_Password.Name = "textBox_Password";
            // 
            // textBox_UserName
            // 
            resources.ApplyResources(this.textBox_UserName, "textBox_UserName");
            this.textBox_UserName.Name = "textBox_UserName";
            // 
            // comboBox_DB
            // 
            resources.ApplyResources(this.comboBox_DB, "comboBox_DB");
            this.comboBox_DB.FormattingEnabled = true;
            this.comboBox_DB.Name = "comboBox_DB";
            this.comboBox_DB.DropDown += new System.EventHandler(this.comboBox_DB_DropDown);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabPage_Oracle
            // 
            resources.ApplyResources(this.tabPage_Oracle, "tabPage_Oracle");
            this.tabPage_Oracle.Controls.Add(this.comboBox_ConnectMode);
            this.tabPage_Oracle.Controls.Add(this.label19);
            this.tabPage_Oracle.Controls.Add(this.checkBox_Direct);
            this.tabPage_Oracle.Controls.Add(this.comboBox_OracleHome);
            this.tabPage_Oracle.Controls.Add(this.label16);
            this.tabPage_Oracle.Controls.Add(this.label15);
            this.tabPage_Oracle.Controls.Add(this.comboBox_ConnectAs);
            this.tabPage_Oracle.Controls.Add(this.label14);
            this.tabPage_Oracle.Controls.Add(this.textBox_OracleProtID);
            this.tabPage_Oracle.Controls.Add(this.label11);
            this.tabPage_Oracle.Controls.Add(this.comboBox_OracleServer);
            this.tabPage_Oracle.Controls.Add(this.button_ConnectOracleTest);
            this.tabPage_Oracle.Controls.Add(this.button_RefreshOracleDB);
            this.tabPage_Oracle.Controls.Add(this.button_RefreshOracleServer);
            this.tabPage_Oracle.Controls.Add(this.textBox_Owner);
            this.tabPage_Oracle.Controls.Add(this.textBox_OracleUserPass);
            this.tabPage_Oracle.Controls.Add(this.textBox_OracleUserID);
            this.tabPage_Oracle.Controls.Add(this.comboBox_SID);
            this.tabPage_Oracle.Controls.Add(this.label12);
            this.tabPage_Oracle.Controls.Add(this.label13);
            this.tabPage_Oracle.Controls.Add(this.label17);
            this.tabPage_Oracle.Controls.Add(this.label18);
            this.tabPage_Oracle.Name = "tabPage_Oracle";
            this.tabPage_Oracle.UseVisualStyleBackColor = true;
            // 
            // comboBox_ConnectMode
            // 
            resources.ApplyResources(this.comboBox_ConnectMode, "comboBox_ConnectMode");
            this.comboBox_ConnectMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ConnectMode.FormattingEnabled = true;
            this.comboBox_ConnectMode.Items.AddRange(new object[] {
            resources.GetString("comboBox_ConnectMode.Items"),
            resources.GetString("comboBox_ConnectMode.Items1")});
            this.comboBox_ConnectMode.Name = "comboBox_ConnectMode";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // checkBox_Direct
            // 
            resources.ApplyResources(this.checkBox_Direct, "checkBox_Direct");
            this.checkBox_Direct.Name = "checkBox_Direct";
            this.checkBox_Direct.UseVisualStyleBackColor = true;
            this.checkBox_Direct.CheckedChanged += new System.EventHandler(this.checkBox_Direct_CheckedChanged);
            // 
            // comboBox_OracleHome
            // 
            resources.ApplyResources(this.comboBox_OracleHome, "comboBox_OracleHome");
            this.comboBox_OracleHome.FormattingEnabled = true;
            this.comboBox_OracleHome.Name = "comboBox_OracleHome";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // comboBox_ConnectAs
            // 
            resources.ApplyResources(this.comboBox_ConnectAs, "comboBox_ConnectAs");
            this.comboBox_ConnectAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ConnectAs.FormattingEnabled = true;
            this.comboBox_ConnectAs.Items.AddRange(new object[] {
            resources.GetString("comboBox_ConnectAs.Items"),
            resources.GetString("comboBox_ConnectAs.Items1"),
            resources.GetString("comboBox_ConnectAs.Items2")});
            this.comboBox_ConnectAs.Name = "comboBox_ConnectAs";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // textBox_OracleProtID
            // 
            resources.ApplyResources(this.textBox_OracleProtID, "textBox_OracleProtID");
            this.textBox_OracleProtID.Name = "textBox_OracleProtID";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // comboBox_OracleServer
            // 
            resources.ApplyResources(this.comboBox_OracleServer, "comboBox_OracleServer");
            this.comboBox_OracleServer.FormattingEnabled = true;
            this.comboBox_OracleServer.Name = "comboBox_OracleServer";
            // 
            // button_ConnectOracleTest
            // 
            resources.ApplyResources(this.button_ConnectOracleTest, "button_ConnectOracleTest");
            this.button_ConnectOracleTest.Name = "button_ConnectOracleTest";
            this.button_ConnectOracleTest.UseVisualStyleBackColor = true;
            this.button_ConnectOracleTest.Click += new System.EventHandler(this.button_ConnectOracleTest_Click);
            // 
            // button_RefreshOracleDB
            // 
            resources.ApplyResources(this.button_RefreshOracleDB, "button_RefreshOracleDB");
            this.button_RefreshOracleDB.Name = "button_RefreshOracleDB";
            this.button_RefreshOracleDB.UseVisualStyleBackColor = true;
            // 
            // button_RefreshOracleServer
            // 
            resources.ApplyResources(this.button_RefreshOracleServer, "button_RefreshOracleServer");
            this.button_RefreshOracleServer.Name = "button_RefreshOracleServer";
            this.button_RefreshOracleServer.UseVisualStyleBackColor = true;
            // 
            // textBox_Owner
            // 
            resources.ApplyResources(this.textBox_Owner, "textBox_Owner");
            this.textBox_Owner.Name = "textBox_Owner";
            // 
            // textBox_OracleUserPass
            // 
            resources.ApplyResources(this.textBox_OracleUserPass, "textBox_OracleUserPass");
            this.textBox_OracleUserPass.Name = "textBox_OracleUserPass";
            // 
            // textBox_OracleUserID
            // 
            resources.ApplyResources(this.textBox_OracleUserID, "textBox_OracleUserID");
            this.textBox_OracleUserID.Name = "textBox_OracleUserID";
            // 
            // comboBox_SID
            // 
            resources.ApplyResources(this.comboBox_SID, "comboBox_SID");
            this.comboBox_SID.FormattingEnabled = true;
            this.comboBox_SID.Name = "comboBox_SID";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // tabPage_OleDB
            // 
            resources.ApplyResources(this.tabPage_OleDB, "tabPage_OleDB");
            this.tabPage_OleDB.Controls.Add(this.Btn_OleDBTest);
            this.tabPage_OleDB.Controls.Add(this.button_BuildString);
            this.tabPage_OleDB.Controls.Add(this.comboBox_ConnectString);
            this.tabPage_OleDB.Controls.Add(this.label10);
            this.tabPage_OleDB.Name = "tabPage_OleDB";
            this.tabPage_OleDB.UseVisualStyleBackColor = true;
            // 
            // Btn_OleDBTest
            // 
            resources.ApplyResources(this.Btn_OleDBTest, "Btn_OleDBTest");
            this.Btn_OleDBTest.Name = "Btn_OleDBTest";
            this.Btn_OleDBTest.UseVisualStyleBackColor = true;
            this.Btn_OleDBTest.Click += new System.EventHandler(this.Btn_OleDBTest_Click);
            // 
            // button_BuildString
            // 
            resources.ApplyResources(this.button_BuildString, "button_BuildString");
            this.button_BuildString.Name = "button_BuildString";
            this.button_BuildString.UseVisualStyleBackColor = true;
            this.button_BuildString.Click += new System.EventHandler(this.button_BuildString_Click);
            // 
            // comboBox_ConnectString
            // 
            resources.ApplyResources(this.comboBox_ConnectString, "comboBox_ConnectString");
            this.comboBox_ConnectString.FormattingEnabled = true;
            this.comboBox_ConnectString.Name = "comboBox_ConnectString";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // button_Cancel
            // 
            resources.ApplyResources(this.button_Cancel, "button_Cancel");
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            resources.ApplyResources(this.button_OK, "button_OK");
            this.button_OK.Name = "button_OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // DBRefStructForm
            // 
            this.AcceptButton = this.button_OK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DBRefStructForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.DBRefStructForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_MSAccess.ResumeLayout(false);
            this.tabPage_MSAccess.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage_MSSqlServer.ResumeLayout(false);
            this.tabPage_MSSqlServer.PerformLayout();
            this.tabPage_Oracle.ResumeLayout(false);
            this.tabPage_Oracle.PerformLayout();
            this.tabPage_OleDB.ResumeLayout(false);
            this.tabPage_OleDB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_MSSqlServer;
        private System.Windows.Forms.TabPage tabPage_Oracle;
        private System.Windows.Forms.TabPage tabPage_MSAccess;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.TextBox textBox_PortID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox_Protocol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_Server;
        private System.Windows.Forms.Button button_RefreshDB;
        private System.Windows.Forms.Button button_RefreshServer;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.TextBox textBox_UserName;
        private System.Windows.Forms.ComboBox comboBox_DB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_ConnectTest;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_MSAccessPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_MSAccessPass;
        private System.Windows.Forms.TextBox textBox_MSAccessUID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage tabPage_OleDB;
        private System.Windows.Forms.Button button_BuildString;
        private System.Windows.Forms.ComboBox comboBox_ConnectString;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox_Direct;
        private System.Windows.Forms.ComboBox comboBox_OracleHome;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBox_ConnectAs;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox_OracleProtID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox_OracleServer;
        private System.Windows.Forms.Button button_ConnectOracleTest;
        private System.Windows.Forms.Button button_RefreshOracleDB;
        private System.Windows.Forms.Button button_RefreshOracleServer;
        private System.Windows.Forms.TextBox textBox_Owner;
        private System.Windows.Forms.TextBox textBox_OracleUserPass;
        private System.Windows.Forms.TextBox textBox_OracleUserID;
        private System.Windows.Forms.ComboBox comboBox_SID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button_ConnectAccessTest;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Btn_OleDBTest;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_PRJPATH;
        private System.Windows.Forms.ComboBox comboBox_ConnectMode;
        private System.Windows.Forms.Label label19;
    }
}