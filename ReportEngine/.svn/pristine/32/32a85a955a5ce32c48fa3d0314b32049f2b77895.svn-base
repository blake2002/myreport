using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PMS.Libraries.ToolControls.Report.Element;
using System.Windows.Forms;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Elements.Util;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;

namespace PMS.Libraries.ToolControls.Report.Element
{
    /// <summary>
    /// 不直接实现IPrintable因为有些元素可能不需要打印
    /// </summary>
    public interface IElement 
    {
        /// <summary>
        /// 名字
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 元素宽
        /// </summary>
        int Width { get; set; }
        /// <summary>
        /// 元素高
        /// </summary>
        int Height { get; set; }
        /// <summary>
        /// 报表元素的父元素
        /// <remarks>在报表模块都是<see cref="IElement"/>的派生类</remarks>
        /// </summary>
        IElement Parent { get; set; }
        /// <summary>
        /// 元素的背景颜色
        /// </summary>
        Color BackColor { get; set; }

        /// <summary>
        /// 元素的背景颜色
        /// </summary>
        Color ForeColor { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        Image BackgroundImage { get; set; }
        /// <summary>
        /// 背景图片的布局
        /// </summary>
        ImageLayout BackgroundImageLayout { get; set; }
        /// <summary>
        /// 是否拥有边框
        /// </summary>
        bool HasBorder { get; set; }
        /// <summary>
        /// 元素边框
        /// </summary>
        ElementBorder Border { get; set; }
        /// <summary>
        /// 是否自动伸展大小
        /// </summary>
        bool AutoSize { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        Point Location { get; set; }
        /// <summary>
        /// X偏移量
        /// </summary>
        float MoveX { get; set; }
        /// <summary>
        /// Y偏移量
        /// </summary>
        float MoveY { get; set; }
        /// <summary>
        /// 刷新
        /// </summary>
        void Invalidate();
        /// <summary>
        /// 额外的数据
        /// </summary>
        List<ExternData> ExternDatas { get; set; }
        /// <summary>
        /// 是否能刷新
        /// </summary>
        bool CanInvalidate { get; }
        /// <summary>
        /// 文本信息
        /// </summary>
        string Text { get; set; }
        /// <summary>
        /// 文本字体
        /// </summary>
        Font Font { get; set; }
        /// <summary>
        /// 边框名
        /// </summary>
        string BorderName { get; set; }

        ///// <summary>
        ///// 将绑定的字段根据传入的参数替换
        ///// </summary>
        ///// <param name="values">参数，值对</param>
        //void BindValue(IDictionary<string, Object> values);
        /// <summary>
        /// 是否拥有左边框
        /// </summary>
        bool HasLeftBorder { get; set; }
        /// <summary>
        /// 是否拥有上边框
        /// </summary>
        bool HasTopBorder { get; set; }

        /// <summary>
        /// 是否拥有下边框
        /// </summary>
        bool HasBottomBorder { get; set; }
        /// <summary>
        /// 是否拥有右边框        ///// <summary>
        ///// 是否可见
        ///// </summary>
        //bool Visible { get; set; }
        /// </summary>
        bool HasRightBorder { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        MESVarType MESType { get; set; }



        /// <summary>
        /// 额外的存储结构
        /// </summary>
        ExtendObject ExtendObject { get; set; }

    }
}
