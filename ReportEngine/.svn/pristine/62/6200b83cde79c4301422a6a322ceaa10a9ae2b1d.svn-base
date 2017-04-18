using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Libraries.ToolControls.PMSPublicInfo
{
    public class ListViewItemEventArgs : EventArgs
    {
        public string FileName = "";
        public int ColumnNo = 0;
        public int LineNo = 0;
        public ListViewItemEventArgs(string fileName, int lineNo, int colNo)
        {
            FileName = fileName;
            LineNo = lineNo;
            ColumnNo = colNo;
        }
    }

    public class EventDelegate
    {
        public event EventHandler<ListViewItemEventArgs> ItemDoubleClick = null;

        public delegate void ProcessDelegate(object sender, EventArgs e);
    }

    public class SaveCompleteEventArgs : EventArgs
    {
        public string strFilePath
        {
            get;
            set;
        }

        public SaveCompleteEventArgs()
        {
           
        }

        public SaveCompleteEventArgs(string filePath)
        {
            strFilePath = filePath;
        }
    }
}
