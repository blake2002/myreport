using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace PMS.Libraries.ToolControls.PMSChart
{
    [Serializable]
    public class YAxisArea
    {
        private bool enble = false;
        /// <summary>
        /// 是否启用多轴
        /// </summary>
        public bool Enable
        {
            get { return enble; }
            set { enble = value; }
        }

        private float offset = 5f;
        /// <summary>
        /// 轴间距
        /// </summary>
        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        //private List<ChartArea> _YAreaList = new List<ChartArea>();
        ///// <summary>
        ///// 多轴集合
        ///// </summary>
        //public List<ChartArea> YAreaList
        //{
        //    get { return _YAreaList; }
        //    set { _YAreaList = value; }
        //}

        public YAxisArea Clone()
        {
            YAxisArea gs = new YAxisArea();
            gs.Enable = Enable;
            gs.Offset = Offset;
            //for (int i = 0; i < YAreaList.Count; i++)
            //{
            //    gs.YAreaList.Add(YAreaList[i]);
            //}
            return gs;
        }

        public ChartArea GetYAxisAreaByName(List<PMSChartArea> YAreaList, string name)
        {
            ChartArea ca = null;
            for (int i = 0; i < YAreaList.Count; i++)
            {
                if (YAreaList[i].Name == name)
                {
                    ca = new ChartArea();
                    YAreaList[i].SetChartArea(ca);
                }
            }
            return ca;
        }
    }
}

