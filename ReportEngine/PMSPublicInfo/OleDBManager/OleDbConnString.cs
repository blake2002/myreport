using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.OleDBManager
{
    /// <summary>
    /// OleDb Connection string utilities.
    /// </summary>
    public static class OleDbConnString
    {
        /// <summary>
        /// Prompts the user and gets a connection string.
        /// </summary>
        public static string GetConnectionString(IWin32Window owner)
        {
            return EditConnectionString(owner, string.Empty);
        }
        /// <summary>
        /// Prompts the user and edits a connection string.
        /// </summary>
        public static string EditConnectionString(IWin32Window owner, string connString)
        {
            try
            {
                // create objects we'll need
                var dlinks = new MSDASC.DataLinksClass();
                var conn = new ADODB.ConnectionClass();

                // sanity
                if (dlinks == null || conn == null)
                {
                    Warning(owner, @"Failed to create DataLinks.

Please check that oledb32.dll is properly installed and registered.

(the usual location is c:\Program Files\Common Files\System\Ole DB\oledb32.dll).");
                    return connString;
                }

                // initialize object
                if (!string.IsNullOrEmpty(connString))
                {
                    conn.ConnectionString = connString;
                }

                // show connection picker dialog
                object obj = conn;
                if (owner != null)
                {
                    dlinks.hWnd = (int)owner.Handle;
                }
                if (dlinks.PromptEdit(ref obj))
                {
                    connString = conn.ConnectionString;
                }
            }
            catch (Exception x)
            {
                Warning(owner, @"Failed to build connection string:
{0}", x.Message);
            }

            // done
            return connString;
        }
        /// <summary>
        /// Trims a connection string for display.
        /// </summary>
        public static string TrimConnectionString(string text)
        {
            string[] keys = new string[] { "Provider", "Initial Catalog", "Data Source" };
            var sb = new StringBuilder();
            foreach (var item in text.Split(';'))
            {
                foreach (var key in keys)
                {
                    if (item.IndexOf(key, StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append("...");
                        }
                        sb.Append(item.Split('=')[1].Trim());
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Translates Sql and Access ODBC connection strings into OleDb.
        /// </summary>
        /// <param name="connString">ODBC connection string.</param>
        /// <returns>An equivalent OleDb connection string.</returns>
        public static string TranslateConnectionString(string connString)
        {
            // we are only interested in the MSDASQL provider (ODBC data sources)
            if (connString == null ||
                connString.IndexOf("provider=msdasql", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return connString;
            }

            // get name of ODBC data source
            var match = Regex.Match(connString, "Data Source=(?<ds>[^;]+)", RegexOptions.IgnoreCase);
            string ds = match.Groups["ds"].Value;
            if (ds == null || ds.Length == 0)
            {
                return connString;
            }

            // look up ODBC entry in registry (LocalMachine and CurrentUser) <<B166>>
            string keyName = @"software\odbc\odbc.ini\" + ds;
            using (var key = Registry.LocalMachine.OpenSubKey(keyName))
            {
                if (key != null)
                {
                    return TranslateConnectionString(connString, key);
                }
            }
            using (var key = Registry.CurrentUser.OpenSubKey(keyName))
            {
                if (key != null)
                {
                    return TranslateConnectionString(connString, key);
                }
            }

            // key not found...
            return connString;
        }
        static string TranslateConnectionString(string connString, RegistryKey key)
        {
            // get driver
            string driver = key.GetValue("driver") as string;

            // translate Access (jet) data sources
            if (driver != null && driver.ToLower().IndexOf("odbcjt") > -1)
            {
                string mdb = key.GetValue("dbq") as string;
                if (mdb != null && mdb.ToLower().EndsWith(".mdb"))
                {
                    return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdb + ";";
                }
            }

            // translate SqlServer data sources
            if (driver != null && driver.ToLower().IndexOf("sqlsrv") > -1)
            {
                string server = key.GetValue("server") as string;
                string dbase = key.GetValue("database") as string;
                if (server != null && server.Length > 0 && dbase != null && dbase.Length > 0)
                {
                    string fmt =
                        "Provider=SQLOLEDB.1;Integrated Security=SSPI;" +
                        "Initial Catalog={0};Data Source={1}";
                    return string.Format(fmt, dbase, server);
                }
            }

            // unsupported data source...
            return connString;
        }

        // issue a warning
        static void Warning(IWin32Window owner, string format, params object[] args)
        {
            string msg = string.Format(format, args);
            MessageBox.Show(owner, msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
