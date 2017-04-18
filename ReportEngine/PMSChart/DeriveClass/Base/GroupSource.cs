using System;
using System.ComponentModel;

namespace PMS.Libraries.ToolControls.PMSChart
{
    /// <summary>
    /// GroupSource类
    /// 用于存放实现分组的各类信息
    /// </summary>
    [Serializable]
    public class GroupSource
    {
        /// <summary>
        /// 是否启用分组
        /// </summary>
        [DefaultValue(true)]
        public bool Enable { get; set; }
        /// <summary>
        /// 主统计字段绑定
        /// </summary>
        public string MajorBinding { get; set; }
        /// <summary>
        /// 主统计字段排序方式
        /// </summary>
        public SortType MajorSort { get; set; }
        /// <summary>
        /// 次统计字段绑定
        /// </summary>
        public string MinorBinding { get; set; }
        /// <summary>
        /// 次统计字段排序方式
        /// </summary>
        public SortType MinorSort { get; set; }
        /// <summary>
        /// 统计数据字段绑定
        /// </summary>
        public string ValueBinding { get; set; }
        /// <summary>
        /// 统计函数
        /// </summary>
        public Functions ValueFx { get; set; }

        public GroupSource Clone()
        {
            GroupSource gs = new GroupSource();
            gs.Enable = Enable;
            gs.MajorBinding = MajorBinding;
            gs.MajorSort = MajorSort;
            gs.MinorBinding = MinorBinding;
            gs.MinorSort = MinorSort;
            gs.ValueBinding = ValueBinding;
            gs.ValueFx = ValueFx;
            return gs;
        }
    }

    /// <summary>
    /// 统计函数
    /// </summary>
    [Serializable]
    public enum Functions
    {
        Count,
        Sum,
        Avg,
        Max,
        Min,
    }

    //[Serializable]
    //public enum SortType
    //{
    //    None,
    //    ASC,
    //    DASC,
    //}
}

