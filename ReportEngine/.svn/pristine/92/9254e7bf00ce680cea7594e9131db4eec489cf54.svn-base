using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.ConfigFileManager
{

    #region  配置的操作类ModuleConfig
    /// <summary>
    /// 配置的操作类ModuleConfig。
    /// </summary>
    public class ModuleConfig<T>
    {

        string _fileName = "Module.config";
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public T LoadSettings()
        {
            T data = default(T);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(_fileName, FileMode.Open);
            data = (T)serializer.Deserialize(fs);
            fs.Close();

            return data;
        }

        public T LoadSettings(string filePath)
        {
            try
            {
                _fileName = filePath;
                T data = default(T);
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                FileStream fs = new FileStream(_fileName, FileMode.Open);
                data = (T)serializer.Deserialize(fs);
                fs.Close();
                return data;
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }

        public bool SaveSettings(T data)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                // serialize the object
                FileStream fs = new FileStream(_fileName, FileMode.Create);
                serializer.Serialize(fs, data);
                fs.Close();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    
    }
    #endregion



    #region  MESDeveloper工具栏位置配置
    public class MESDeveloperToolStripSettings
    {
        private Point _toolStrip_Align_Location = new Point(0,0);
        private Point _ToolStrip_Basic_Location = new Point(0,0);
        
        [XmlElement]
        public System.Drawing.Point ToolStrip_Align_Location
        {
            get { return _toolStrip_Align_Location; }
            set { _toolStrip_Align_Location = value; }
        }

        public System.Drawing.Point ToolStrip_Basic_Location
        {
            get { return _ToolStrip_Basic_Location; }
            set { _ToolStrip_Basic_Location = value; }
        }
    }
    #endregion 

}
