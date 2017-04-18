using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Element;

namespace NetSCADA.ReportEngine
{
	public enum ReportPageType
	{
		ReportHead,
		Data,
		ReportFoot
	}

	public class ReportPage
	{
		public ReportPage (ReportPages pageManager, int pageNumber, ReportPageType pageType)
		{
			_PageManager = pageManager;
			_pageNumber = pageNumber;
			_PageType = pageType;
			_firstPosition = 1;
		}

		private ReportPages _PageManager = null;

		public ReportPages PageManager {
			get {
				return _PageManager;
			}
		}

		private ReportPageType _PageType = ReportPageType.Data;

		public ReportPageType PageType {
			get {
				return _PageType;
			}
		}

		private int _pageNumber;

		/// <summary>
		/// 页编号（从1开始计数）
		/// </summary>
		public int PageNumber {
			get { return _pageNumber; }
			set { _pageNumber = value; }
		}

		//todo:qiuleilei
		private int _firstPosition = 1;

		/// <summary>
		/// 在导出html时 标记第一个div position 为relative
		/// </summary>
		/// <value>The first position.</value>
		public int FirstPosition {
			get {
				return _firstPosition;
			}
			set {
				_firstPosition = value;
			}
		}

		private List<ReportElement> _ReportElementList = new List<ReportElement> ();

		public List<ReportElement> Elements {
			get {
				return _ReportElementList;
			}
		}

		public void SetGuidPanelElementHeight (string guid)
		{
			int nCount = Elements.Count;
			for (int i = 0; i < nCount; i++) {
				if (Elements [i].Guid == guid) {
					//Elements[i].Height =0.0f;
					// Elements[i].Width = 0;
					//Elements.Remove(Elements[i]);
				}
			}
		}

		public void AddReportElement (ReportElement element, int index = -1)
		{
			if (null == element) {
				return;
			}
			if (index >= 0 && index < _ReportElementList.Count) {
				_ReportElementList.Insert (index, element);
			} else {
				_ReportElementList.Add (element);
			} 
			element.Page = this;
		}

		public void RemoveReportElement (ReportElement element)
		{
			if (null == element) {
				return;
			} 
			_ReportElementList.Remove (element); 
			element.Release (); 
		}

		public void Release ()
		{
			foreach (ReportElement element in _ReportElementList) {
				if (element != null) {
					element.Release ();
				}
			}
			_ReportElementList.Clear ();
		}
	}
}
