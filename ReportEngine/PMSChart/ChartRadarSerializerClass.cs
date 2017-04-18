using System;
using System.Collections.Generic;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.Report.Elements.Util;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [Serializable]
    class ChartRadarSerializerClass:IControlTranslator,IDisposable,IElement
    {
        private Point _Location;
        private int _Width;
        private int _Height;
        private byte[] _Context;


        public Point Location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
            }
        }
        public int Width
        {
            get
            {
                return _Width;
            }
            set
            {
                _Width = value;
            }
        }
        public int Height
        {
            get
            {
                return _Height;
            }
            set
            {
                _Height = value;
            }
        }
        public byte[] Context
        {
            get
            {
                return _Context;
            }
            set
            {
                _Context = value;
            }
        }
        #region 2011.01.13 增加 多余属性
        public MESVarType MESType
        {
            get;
            set;
        }
        public bool HasRightBorder
        {
            get;
            set;
        }
        public bool HasLeftBorder { get; set; }
        public bool HasTopBorder { get; set; }
        public bool HasBottomBorder { get; set; }
        public string BorderName { get; set; }
        public bool CanInvalidate { get; set; }
        public List<ExternData> ExternDatas { get; set; }
        public float MoveX { get; set; }
        public float MoveY { get; set; }
        public ElementBorder Border { get; set; }
        public bool HasBorder { get; set; }
        public IElement Parent
        {
            get;
            set;
        }
        public ExtendObject ExtendObject { get; set; }
        public Font Font { get; set; }
        public string Text { get; set; }
        public bool AutoSize { get; set; }
        public Color BackColor { get; set; }
        public Image BackgroundImage { get; set; }
        public ImageLayout BackgroundImageLayout { get; set; }
        public string Name { get; set; }
        public void Invalidate()
        {
        }
        public Color ForeColor { get; set; }
        #endregion
        public Control ToControl(bool childTranslate = false)
        {
            RadarAlertChart result = null;
            if (_Context != null)
            {
                MemoryStream temp = new MemoryStream(_Context);
                result = new RadarAlertChart(temp);
            }
            if (result != null)
            {
                result.Width = this.Width;
                result.Height = this.Height;
                result.Location = this.Location;
            }
            return result as Control;
        }
        public void Dispose()
        {
            _Context = null;
        }
    }
}
