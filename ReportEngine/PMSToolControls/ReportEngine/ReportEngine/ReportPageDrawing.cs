using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.Report.Element;
using PMS.Libraries.ToolControls.Report;
using PMS.Libraries.ToolControls.PmsSheet.PmsPublicData;
using PMS.Libraries.ToolControls.PMSChart;
using PMS.Libraries.ToolControls;
using System.Runtime.InteropServices;
using Microsoft.Extensions.ObjectPool;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace NetSCADA.ReportEngine
{
	/// <summary>
	/// 报表页绘图控件
	/// </summary>
	public partial class ReportPageDrawing : UserControl
	{
		float _Left;
		float _Top;
		float _vScroll;
		float _hScroll;

		private float _DpiX = 96;
		private float _DpiY = 96;

		public ReportPageDrawing ()
		{ 
			//todo:qiuleilei 20161214
			if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportServer)
				return;
			InitializeComponent ();    				   
			Graphics ga = null;
			try {
				ga = this.CreateGraphics ();
				_DpiX = ga.DpiX;
				_DpiY = ga.DpiY;
			} catch {
				_DpiX = _DpiY = 96;
			} finally {
				if (ga != null)
					ga.Dispose ();
			} 

			this.DoubleBuffered = true;
		}

		private void SetDrawingChildControlVisibleExpression (Control control, string exp)
		{
			foreach (Control c in control.Controls) {
				if (c is IVisibleExpression) {
					(c as IVisibleExpression).VisibleExpression = exp;
				}
			}
		}

		/// <summary>
		/// 检查按键操作
		/// </summary>
		/// <param name="keyData"></param>
		/// <returns></returns>
		protected override bool IsInputKey (Keys keyData)
		{
			if (keyData == Keys.Left ||
			    keyData == Keys.Right ||
			    keyData == Keys.Up ||
			    keyData == Keys.Down ||
			    keyData == Keys.Home ||
			    keyData == Keys.End ||
			    keyData == Keys.PageDown ||
			    keyData == Keys.PageUp)
				return true;
			return base.IsInputKey (keyData);
		}

		#region 基本属性

		private ReportPages _Pages = null;

		/// <summary>
		/// 报表页集合
		/// </summary>
		public ReportPages Pages {
			get { return _Pages; }
			set { _Pages = value; }
		}

		private int _PageCurrent = 1;

		/// <summary>
		/// 当前页
		/// </summary>
		public int PageCurrent {
			get { return _PageCurrent; }
		}

		#endregion

		#region 尺寸转换

		private float InchesToPixelX (float size)
		{
			return SizeConversion.ConvertInchesToPixel (size, _DpiX);
		}

		private float InchesToPixelY (float size)
		{
			return SizeConversion.ConvertInchesToPixel (size, _DpiY);
		}

		#endregion

		#region 日志管理

		public void AddReportLog (string message)
		{
			if (_Pages == null || _Pages.ReportRuntime == null) {
				return;
			}
			_Pages.ReportRuntime.AddReportLog (message);

		}

		public void AddReportLog (System.Exception ex)
		{
			if (_Pages == null || _Pages.ReportRuntime == null) {
				return;
			}
			_Pages.ReportRuntime.AddReportLog (ex);
		}

		#endregion


		#region 导出报表

		/// <summary>
		/// 导出报表页
		/// </summary>
		/// <param name="g"></param>
		/// <param name="page"></param>
		/// <param name="clipRectangle"></param>
		public void ExportPage (Graphics g, int page, System.Drawing.Rectangle clipRectangle, bool isExport = false, Extension ext = null)
		{ 
			if (_Pages == null) {
				return;
			}
			_DpiX = g.DpiX;
			_DpiY = g.DpiY;
			g.PageUnit = GraphicsUnit.Pixel;
			g.ScaleTransform (1, 1); 
			_Left = 0;
			_Top = 0;
			_hScroll = 0;
			_vScroll = 0; 
			RectangleF r = new RectangleF (0, 0, InchesToPixelX (_Pages.PageWidth), InchesToPixelY (_Pages.PageHeight));
            
			g.SetClip (r);
			g.FillRectangle (Brushes.White, r);
			DrawPage (g, _Pages.Pages [page], r, isExport, ext); 
		}

		DefaultObjectPool<MyRectF> rectfPool = new DefaultObjectPool<MyRectF> (new DefaultPooledObjectPolicy<MyRectF> ());

		void SetRectF (MyRectF rect, float x, float y, float width, float heigth)
		{
			rect.X = rect.Left = x;
			rect.Y = rect.Top = y;
			rect.Width = width;
			rect.Height = heigth;
		}

		void SetPointF (ref PointF point, float x, float y)
		{
			point.X = x;
			point.Y = y;
		}

		public void ExportPage (int page, ref float htmlWidth, ref float htmlHeight, bool isExport = false, Extension ext = null)
		{ 
			if (_Pages == null) {
				return;
			}
			_Left = 0;
			_Top = 0;
			_hScroll = 0;
			_vScroll = 0; 
			MyRectF r = rectfPool.Get ();
			SetRectF (r, 0, 0, InchesToPixelX (_Pages.PageWidth), InchesToPixelY (_Pages.PageHeight));
			if (htmlWidth == -1f) {
				htmlWidth = r.Width;
			}
			if (htmlHeight == -1f) {
				htmlHeight = r.Height;
			}
			DrawPage (_Pages.Pages [page], r, ref htmlWidth, ref htmlHeight, isExport, ext); 
		}

		#endregion

		#region 打印报表

		/// <summary>
		/// 打印报表
		/// </summary>
		/// <param name="g"></param>
		/// <param name="page"></param>
		/// <param name="clipRectangle"></param>
		public void Print (Graphics g, int page, System.Drawing.Printing.PaperSize paperSize)
		{
			if (_Pages == null) {
				return;
			}
			_Left = 0;
			_Top = 0;
			_hScroll = 0;
			_vScroll = 0;
			_DpiX = g.DpiX;
			_DpiY = g.DpiY;
			g.PageUnit = GraphicsUnit.Pixel;
			if (_Pages.EnablePrintZoom == true) {
				//PrintDocument的PaperSize的单位是百分之一英寸  
				float widthZoomTemp = ((float)paperSize.Width / 100) / ((float)_Pages.PageWidth);
				float heightZoomTemp = ((float)paperSize.Height / 100) / ((float)_Pages.PageHeight);  

				g.ScaleTransform (Math.Min (widthZoomTemp, heightZoomTemp), Math.Min (widthZoomTemp, heightZoomTemp));

			} else {
				g.ScaleTransform (1, 1);
			}
			RectangleF r = new RectangleF (0, 0, InchesToPixelX (_Pages.PageWidth), InchesToPixelY (_Pages.PageHeight));
			DrawPage (g, _Pages.Pages [page], r);
            
		}

		#endregion

		#region  绘制报表

		/// <summary>
		/// 绘制报表
		/// </summary>
		/// <param name="g"></param>
		/// <param name="zoom"></param>
		/// <param name="leftMargin">左边距</param>
		/// <param name="pageGap"></param>
		/// <param name="hScroll">水平位置</param>
		/// <param name="vScroll">垂直位置</param>
		/// <param name="clipRectangle"></param>
		/// <param name="IsPreview">是否是导航视图</param>
		public void Draw (Graphics g, float zoom, float leftMargin, float pageGap,
		                  float hScroll, float vScroll, Rectangle clipRectangle, bool IsPreview)
		{

			#region 无报表时绘制
            
			if (_Pages == null || _Pages.PageCount <= 0) {
				RectangleF rfR = new RectangleF (0, 0, this.Width, this.Height);
				//RectangleF rfR = new RectangleF(clipRectangle.X, clipRectangle.Y, clipRectangle.Width - 3, clipRectangle.Height - 6);
				g.FillRectangle (Brushes.White, rfR);

				using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 100, 100, 100))) {
					g.FillRectangle (brs, new RectangleF (rfR.X + rfR.Width, rfR.Y + 2, 2, rfR.Height));
				}
				using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 150, 150, 150))) {
					g.FillRectangle (brs, new RectangleF (rfR.X + rfR.Width + 1, rfR.Y + 2, 2, rfR.Height));
				}
				using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 187, 187, 187))) {
					g.FillRectangle (brs, new RectangleF (rfR.X + rfR.Width + 2, rfR.Y + 2, 3, rfR.Height));
				}


				using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 100, 100, 100))) {
					g.FillRectangle (brs, new RectangleF (rfR.X + 2, rfR.Y + rfR.Height, rfR.Width, 2));
				}
				using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 150, 150, 150))) {
					g.FillRectangle (brs, new RectangleF (rfR.X + 2, rfR.Y + rfR.Height + 1, rfR.Width, 2));
				}
				using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 187, 187, 187))) {
					g.FillRectangle (brs, new RectangleF (rfR.X + 2, rfR.Y + rfR.Height + 2, rfR.Width, 3));
				}   
				return;
			}
			#endregion

			_DpiX = g.DpiX;
			_DpiY = g.DpiY;
			g.PageUnit = GraphicsUnit.Pixel;
			g.ScaleTransform (zoom, zoom);
			RectangleF r = new RectangleF ((clipRectangle.X) / zoom, (clipRectangle.Y) / zoom,
				               (clipRectangle.Width) / zoom, (clipRectangle.Height) / zoom);


			int fpage = (int)(vScroll / (InchesToPixelY (_Pages.PageHeight) + pageGap));
			int lpage = (int)((vScroll + r.Height) / (InchesToPixelY (_Pages.PageHeight) + pageGap)) + 1;
			if (fpage >= _Pages.PageCount)
				return;
			if (lpage > _Pages.PageCount)
				lpage = _Pages.PageCount;
			_PageCurrent = fpage + 1;
			_hScroll = hScroll;
			_Left = leftMargin;
			_Top = pageGap; 

			for (int p = fpage; p < lpage; p++) {
				if (null != _Pages.ReportRuntime.DataTables) {
					_Pages.ReportRuntime.DataTables.SetVariableValue ("#PageIndex#", p + 1);
				} 

				_vScroll = vScroll - p * (InchesToPixelY (_Pages.PageHeight) + pageGap);
  
				RectangleF rfC = new RectangleF ((_Left - _hScroll), (_Top - _vScroll),
					                 InchesToPixelX (_Pages.PageWidth), InchesToPixelY (_Pages.PageHeight));
				g.SetClip (rfC);
				g.FillRectangle (Brushes.White, rfC); 
				DrawPage (g, _Pages.Pages [p], rfC);
                
				// 绘制页外框
				using (Pen pn = new Pen (Brushes.Black, 1)) {
					float z3 = Math.Max ((3f / zoom), 3);
					if (z3 <= 0)
						z3 = 1;
					RectangleF rfR = new RectangleF ((rfC.X + rfC.Width), (rfC.Y + z3 / 3), z3, (rfC.Height));
					g.SetClip (rfR);
					using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 100, 100, 100))) {
						g.FillRectangle (brs, rfR.X, rfR.Y, z3, rfR.Height);
					}
					using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 150, 150, 150))) {
						g.FillRectangle (brs, rfR.X + z3 / 3, rfR.Y, z3, rfR.Height);
					}
					using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 187, 187, 187))) {
						g.FillRectangle (brs, rfR.X + z3 * 2 / 3, rfR.Y, z3, rfR.Height);
					}


					rfR = new RectangleF ((rfC.X + z3 / 3), (rfC.Y + rfC.Height), (rfC.Width), z3);
					g.SetClip (rfR);
					using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 100, 100, 100))) {
						g.FillRectangle (brs, rfR.X, rfR.Y, rfR.Width, z3);
					}
					using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 150, 150, 150))) {
						g.FillRectangle (brs, rfR.X, rfR.Y + z3 / 3, rfR.Width, z3);
					}
					using (SolidBrush brs = new SolidBrush (Color.FromArgb (255, 187, 187, 187))) {
						g.FillRectangle (brs, rfR.X, rfR.Y + z3 * 2 / 3, rfR.Width, z3);
					} 

					if (IsPreview) {//导航视图

						float fontWidth = Math.Max ((int)(4f / zoom), 4);
						RectangleF rfC2 = new RectangleF ((rfC.X - fontWidth), (rfC.Y - fontWidth), (rfC.Width + 2 * fontWidth), (rfC.Height + 2 * fontWidth));
						g.SetClip (rfC2);
						using (Pen pn2 = new Pen (Brushes.Blue, fontWidth)) {
							if (p == (_Pages.SelectedPageNum - 1)) {//高亮显示当前选中页
								g.DrawRectangle (pn2, (rfC.X + fontWidth / 3), (rfC.Y + fontWidth / 3), (rfC.Width - fontWidth), (rfC.Height - fontWidth * 2 / 3));
							}
						}

						Font ft = new Font ("Time New Roman", 10 / zoom); 
						try {
							g.SetClip (new RectangleF (rfC.X, rfC.Y + rfC.Height, rfC.Width, pageGap));

							RectangleF rfC3 = new RectangleF ((rfC.X + rfC.Width / 2 - pageGap), (rfC.Y + rfC.Height + pageGap / 8), (2 * pageGap), (pageGap * 3 / 4));

							using (Pen pn3 = new Pen (Brushes.Black, 1)) {
								g.DrawRectangle (pn3, rfC3.X, rfC3.Y, rfC3.Width, rfC3.Height);
							}
							StringFormat strFormt = new StringFormat ();
							strFormt.Alignment = StringAlignment.Center;
							strFormt.LineAlignment = StringAlignment.Center;
							string str = string.Format ("{0}", p + 1);
							g.DrawString (string.Format ("{0}", p + 1), ft, Brushes.Black, rfC3, strFormt); 
						} catch (System.Exception ex) {
                        	
						} finally {
							if (ft != null) {
								ft.Dispose ();
							}
						}
					}
				} 
			}
		}

		/// <summary>
		/// 绘制报表页
		/// </summary>
		/// <param name="g"></param>
		/// <param name="p"></param>
		/// <param name="clipRect"></param>
		private void DrawPage (Graphics g, ReportPage p, System.Drawing.RectangleF clipRect, bool isExport = false, Extension ext = null) //todo: qiuleilei
		{
			if (_Pages == null) {
				return;
			}
			if (p.PageType == ReportPageType.Data) { 
				//页眉
				RectangleF rfC = new RectangleF (clipRect.X + InchesToPixelX (_Pages.PageLeftMargin), clipRect.Y + InchesToPixelY (_Pages.PageTopMargin) - 1,
					                 clipRect.Width - (InchesToPixelX (_Pages.PageRightMargin) + InchesToPixelX (_Pages.PageLeftMargin)), InchesToPixelY (_Pages.PageHeadHeight));
				g.SetClip (rfC);
				var top = rfC.Top;
				var height = rfC.Height;
				using (SolidBrush brh = new SolidBrush (_Pages.PageHeadBackColor)) { 
					g.FillRectangle (brh, rfC);
					if (ext != null)
						ext.ReportPageType2Html (p, rfC, _Pages.PageHeadBackColor, isExport);
				}
				foreach (ReportElement pi in p.PageManager.PageHeadReportElements) {
					if (pi.Tag is IVisibleExpression) {
						string str = (pi.Tag as IVisibleExpression).VisibleExpression;
						if (!string.IsNullOrEmpty (str) && ((str.IndexOf ("PageIndex") >= 0 || str.IndexOf ("PageCount") >= 0))) {
							object o = _Pages.ReportRuntime.ExpressionEngine.Execute (str, _Pages.ReportRuntime.DataTables, "");
							if (o != null) {
								bool bVisible = Convert.ToBoolean (o);
								if (!bVisible) {
									if (pi.Tag is IPanelElement)
										SetDrawingChildControlVisibleExpression (pi.Tag as Control, str);
									continue;
								}
							}
						}
					}

					PointF location = new PointF (InchesToPixelX (pi.Location.X + _Pages.PageLeftMargin) + _Left - _hScroll, InchesToPixelY (pi.Location.Y + p.PageManager.PageTopMargin) + _Top - _vScroll);
					pi.Page = p;
					pi.Draw (g, location, _Pages.ReportRuntime, rfC, isExport, ext);
				}

				//数据区
				rfC = new RectangleF (clipRect.X + InchesToPixelX (_Pages.PageLeftMargin), clipRect.Y + InchesToPixelY (_Pages.PageTopMargin + _Pages.PageHeadHeight) - 1,
					clipRect.Width - (InchesToPixelX (_Pages.PageRightMargin) + InchesToPixelX (_Pages.PageLeftMargin)), InchesToPixelY (_Pages.PageDataRegionHeight));
               
				g.SetClip (rfC);

				using (SolidBrush brh = new SolidBrush (_Pages.DataRegionBackColor)) {
					g.FillRectangle (brh, rfC);
					if (ext != null)
						ext.ReportPageType2Html (p, rfC, _Pages.DataRegionBackColor, isExport);
				}
				foreach (ReportElement pi in p.Elements) { 
					PointF location = new PointF (InchesToPixelX (pi.Location.X + _Pages.PageLeftMargin) + _Left - _hScroll, InchesToPixelY (pi.Location.Y + p.PageManager.PageHeadHeight + p.PageManager.PageTopMargin) + _Top - _vScroll);
					pi.Page = p;
					if (location.Y < top) {
						location.Y = top + height;
						//pi.Height=
					}
					pi.Draw (g, location, _Pages.ReportRuntime, rfC, isExport, ext); 
				}
					
				//页脚
				rfC = new RectangleF (clipRect.X + InchesToPixelX (_Pages.PageLeftMargin), clipRect.Y + InchesToPixelY (_Pages.PageTopMargin + _Pages.PageHeadHeight + _Pages.PageDataRegionHeight) - 1,
					clipRect.Width - (InchesToPixelX (_Pages.PageRightMargin) + InchesToPixelX (_Pages.PageLeftMargin)), InchesToPixelY (_Pages.PageFootHeight));
				g.SetClip (rfC);

				using (SolidBrush brh = new SolidBrush (_Pages.PageFootBackColor)) {
					g.FillRectangle (brh, rfC);
					if (ext != null)
						ext.ReportPageType2Html (p, rfC, _Pages.PageFootBackColor, isExport);

				}
                
				foreach (ReportElement pi in p.PageManager.PageFootReportElements) {
					if (pi.Tag is IVisibleExpression) {
						string str = (pi.Tag as IVisibleExpression).VisibleExpression;
						if (!string.IsNullOrEmpty (str) && ((str.IndexOf ("PageIndex") >= 0 || str.IndexOf ("PageCount") >= 0))) {
							object o = _Pages.ReportRuntime.ExpressionEngine.Execute (str, _Pages.ReportRuntime.DataTables, "");
							if (o != null) {
								bool bVisible = Convert.ToBoolean (o);
								if (!bVisible) {
									if (pi.Tag is IPanelElement)
										SetDrawingChildControlVisibleExpression (pi.Tag as Control, str);
									continue;
								}
							}
						}
					}

					PointF location = new PointF (InchesToPixelX (pi.Location.X + _Pages.PageLeftMargin) + _Left - _hScroll, InchesToPixelY (pi.Location.Y + p.PageManager.PageHeadHeight + p.PageManager.PageTopMargin + p.PageManager.PageDataRegionHeight) + _Top - _vScroll);
					pi.Page = p;
					pi.Draw (g, location, _Pages.ReportRuntime, rfC, isExport, ext);
				}
			} else { //报表头、报表尾
				//数据区
				RectangleF rfC = new RectangleF (clipRect.X + InchesToPixelX (_Pages.PageLeftMargin), clipRect.Y + InchesToPixelY (_Pages.PageTopMargin) - 1,
					                 clipRect.Width - (InchesToPixelX (_Pages.PageRightMargin) + InchesToPixelX (_Pages.PageLeftMargin)), InchesToPixelY (_Pages.PageDrawRegionHeight));
				g.SetClip (rfC);
			
				if (p.PageType == ReportPageType.ReportHead) {
					using (SolidBrush brh = new SolidBrush (_Pages.ReportHeadBackColor)) {
						g.FillRectangle (brh, rfC);
						if (ext != null)
							ext.ReportPageType2Html (p, rfC, _Pages.ReportHeadBackColor, isExport);

					}
				} else {
					using (SolidBrush brh = new SolidBrush (_Pages.ReportFootBackColor)) {
						g.FillRectangle (brh, rfC);
						if (ext != null)
							ext.ReportPageType2Html (p, rfC, _Pages.ReportFootBackColor, isExport);

					}
				} 
				foreach (ReportElement pi in p.Elements) {
					PointF location = new PointF (InchesToPixelX (pi.Location.X + _Pages.PageLeftMargin) + _Left - _hScroll, InchesToPixelY (pi.Location.Y + p.PageManager.PageTopMargin) + _Top - _vScroll);
					pi.Page = p;
					pi.Draw (g, location, _Pages.ReportRuntime, rfC, isExport, ext);
				}
			}
		}

		private void DrawPage (ReportPage p, MyRectF clipRect, ref float htmlWidth, ref float htmlHeight, bool isExport = false, Extension ext = null) //todo: qiuleilei
		{
			if (_Pages == null) {
				return;
			}
			PointF location = new PointF (0, 0);
			if (p.PageType == ReportPageType.Data) { 
				//页眉
				MyRectF rfC = rectfPool.Get ();
				SetRectF (rfC, clipRect.X + InchesToPixelX (_Pages.PageLeftMargin), clipRect.Y + InchesToPixelY (_Pages.PageTopMargin) - 1,
					clipRect.Width - (InchesToPixelX (_Pages.PageRightMargin) + InchesToPixelX (_Pages.PageLeftMargin)), InchesToPixelY (_Pages.PageHeadHeight));
				var top = rfC.Top;
				var height = rfC.Height;
//				if (htmlWidth == -1f) {
//					htmlWidth = rfC.Width;
//				}
//				if (htmlHeight == -1f) {
//					htmlHeight = rfC.Height;
//				}
				if (ext != null)
					ext.ReportPageType2Html (p, rfC, _Pages.PageHeadBackColor, isExport);
			
				foreach (ReportElement pi in p.PageManager.PageHeadReportElements) {
					if (pi.Tag is IVisibleExpression) {
						string str = (pi.Tag as IVisibleExpression).VisibleExpression;
						if (!string.IsNullOrEmpty (str) && ((str.IndexOf ("PageIndex") >= 0 || str.IndexOf ("PageCount") >= 0))) {
							object o = _Pages.ReportRuntime.ExpressionEngine.Execute (str, _Pages.ReportRuntime.DataTables, "");
							if (o != null) {
								bool bVisible = Convert.ToBoolean (o);
								if (!bVisible) {
									if (pi.Tag is IPanelElement)
										SetDrawingChildControlVisibleExpression (pi.Tag as Control, str);
									continue;
								}
							}
						}
					}

					SetPointF (ref location, InchesToPixelX (pi.Location.X + _Pages.PageLeftMargin) + _Left - _hScroll, InchesToPixelY (pi.Location.Y + p.PageManager.PageTopMargin) + _Top - _vScroll);
					pi.Page = p;
					pi.Draw (location, _Pages.ReportRuntime, rfC, isExport, ext);
				}

				//数据区
				rfC = rectfPool.Get ();
				SetRectF (rfC, clipRect.X + InchesToPixelX (_Pages.PageLeftMargin), clipRect.Y + InchesToPixelY (_Pages.PageTopMargin + _Pages.PageHeadHeight) - 1,
					clipRect.Width - (InchesToPixelX (_Pages.PageRightMargin) + InchesToPixelX (_Pages.PageLeftMargin)), InchesToPixelY (_Pages.PageDataRegionHeight));

				if (ext != null)
					ext.ReportPageType2Html (p, rfC, _Pages.DataRegionBackColor, isExport);
				
				foreach (ReportElement pi in p.Elements) { 
					SetPointF (ref location, InchesToPixelX (pi.Location.X + _Pages.PageLeftMargin) + _Left - _hScroll, InchesToPixelY (pi.Location.Y + p.PageManager.PageHeadHeight + p.PageManager.PageTopMargin) + _Top - _vScroll);
					pi.Page = p;
					if (location.Y < top) {
						location.Y = top + height;
						//pi.Height=
					}
					pi.Draw (location, _Pages.ReportRuntime, rfC, isExport, ext); 
				}

				//页脚
				rfC = rectfPool.Get ();
				SetRectF (rfC, clipRect.X + InchesToPixelX (_Pages.PageLeftMargin), clipRect.Y + InchesToPixelY (_Pages.PageTopMargin + _Pages.PageHeadHeight + _Pages.PageDataRegionHeight) - 1,
					clipRect.Width - (InchesToPixelX (_Pages.PageRightMargin) + InchesToPixelX (_Pages.PageLeftMargin)), InchesToPixelY (_Pages.PageFootHeight));
	
				if (ext != null)
					ext.ReportPageType2Html (p, rfC, _Pages.PageFootBackColor, isExport);



				foreach (ReportElement pi in p.PageManager.PageFootReportElements) {
					if (pi.Tag is IVisibleExpression) {
						string str = (pi.Tag as IVisibleExpression).VisibleExpression;
						if (!string.IsNullOrEmpty (str) && ((str.IndexOf ("PageIndex") >= 0 || str.IndexOf ("PageCount") >= 0))) {
							object o = _Pages.ReportRuntime.ExpressionEngine.Execute (str, _Pages.ReportRuntime.DataTables, "");
							if (o != null) {
								bool bVisible = Convert.ToBoolean (o);
								if (!bVisible) {
									if (pi.Tag is IPanelElement)
										SetDrawingChildControlVisibleExpression (pi.Tag as Control, str);
									continue;
								}
							}
						}
					}

					//PointF location = pointfPool.Get ();
					SetPointF (ref location, InchesToPixelX (pi.Location.X + _Pages.PageLeftMargin) + _Left - _hScroll, InchesToPixelY (pi.Location.Y + p.PageManager.PageHeadHeight + p.PageManager.PageTopMargin + p.PageManager.PageDataRegionHeight) + _Top - _vScroll);
					pi.Page = p;
					pi.Draw (location, _Pages.ReportRuntime, rfC, isExport, ext);
				}
			} else { //报表头、报表尾
				//数据区
				MyRectF rfC = rectfPool.Get ();
				SetRectF (rfC, clipRect.X + InchesToPixelX (_Pages.PageLeftMargin), clipRect.Y + InchesToPixelY (_Pages.PageTopMargin) - 1,
					clipRect.Width - (InchesToPixelX (_Pages.PageRightMargin) + InchesToPixelX (_Pages.PageLeftMargin)), InchesToPixelY (_Pages.PageDrawRegionHeight));
//				if (htmlWidth == -1f) {
//					htmlWidth = rfC.Width;
//				}
//				if (htmlHeight == -1f) {
//					htmlHeight = rfC.Height;
//				}
				var top = rfC.Top;
				var height = rfC.Height;
				if (p.PageType == ReportPageType.ReportHead) {
			
					if (ext != null)
						ext.ReportPageType2Html (p, rfC, _Pages.ReportHeadBackColor, isExport);


				} else {
			
					if (ext != null)
						ext.ReportPageType2Html (p, rfC, _Pages.ReportFootBackColor, isExport);

				} 
				foreach (ReportElement pi in p.Elements) {
					//PointF location = pointfPool.Get ();
					SetPointF (ref location, InchesToPixelX (pi.Location.X + _Pages.PageLeftMargin) + _Left - _hScroll, InchesToPixelY (pi.Location.Y + p.PageManager.PageTopMargin) + _Top - _vScroll);
					pi.Page = p;
					if (location.Y < top) {
						location.Y = top + height;
						//pi.Height=
					}
					pi.Draw (location, _Pages.ReportRuntime, rfC, isExport, ext);
				}
			}
		}

		#endregion
	}
}
