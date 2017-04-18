using System;

namespace NetSCADA.MESReportServer
{
	public class SchedulerRuler
	{
		/// <summary>
		/// 定时任务名称
		/// </summary>
		/// <value>The name of the job.</value>
		public string JobName { get; set; }

		/// <summary>
		/// 0 只执行1次， 1 周期性执行
		/// </summary>
		/// <value>The type of the run.</value>
		public int RunType { get; set; }

		/// <summary>
		/// Y-年 M-月 W-周 D-日 H-时 Min-分 S-秒
		/// </summary>
		/// <value>The type of the cycle.</value>
		public string CycleType { get; set; }

		/// <summary>
		/// 如果RunType = 1 && CycleType 为 Y M W, 则需要指定 具体哪一天 
		/// 例如 CycleType = Y OffsetDays =1 则 是说明在每年的第一天执行
		/// CycleType = M OffsetDays =2 则 是说明在每月的第二天执行
		/// CycleType = W OffsetDays =3 则 是说明在每周的第三天执行
		/// </summary>
		/// <value>The off days.</value>
		public int OffsetDays { get; set; }

		/// <summary>
		/// 具体的执行时间 
		/// 如果runtype=0 则 规则为yyyyMMddHHmmss
		/// 如果runtype=1 则 规则为HHmmss
		/// </summary>
		/// <value>The real time.</value>
		public string RealTime { get; set; }

		/// <summary>
		/// 说明
		/// </summary>
		/// <value>The comment.</value>
		//		public string Comment {
		//			get {
		//				return @"RunType:0 只执行1次， 1 周期性执行;
		//					 CycleType:Y-年 M-月 W-周 D-日 H-时 Min-分 S-秒;
		//					 OffsetDays: 如果RunType = 1 && CycleType 为 Y M W, 则需要指定 具体哪一天
		//					 例如 CycleType = Y OffsetDays =1 则 是说明在每年的第一天执行
		//					 CycleType = M OffsetDays =2 则 是说明在每月的第二天执行
		//					 CycleType = W OffsetDays =3 则 是说明在每周的第三天执行;
		//					RealTime:具体的执行时间
		//					如果runtype=0 则 规则为yyyyMMddHHmmss
		//					如果runtype=1 则 规则为HHmmss";
		//			}
		//		}

	}

	public class JobRuler
	{
		public string RptName{ get; set; }

		public SchedulerRuler SchedulerRuler{ get; set; }
	}
}

