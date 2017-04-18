#region MyRegion
using PMS.Libraries.ToolControls.MESTable.Borders;
using PMS.Libraries.ToolControls.MESTable.CellStyles;
using PMS.Libraries.ToolControls.Report.Element;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using PMS.Libraries.ToolControls.MESTable;
using HtmlTags;
using System.Drawing.Drawing2D;
using PMS.Libraries.ToolControls.Report;
using System.Drawing.Imaging;
using System.Linq;
using PMS.Libraries.ToolControls.TwoDCodeLib;
using PMS.Libraries.ToolControls.PMSChart;
using System.Data;
using ECharts;
using ECharts.Entities;
using ECharts.Entities.axis;
using ECharts.Entities.series;
using System.IO;
using ECharts.Entities.style;
using Newtonsoft.Json.Linq;
using PMS.Libraries.ToolControls;
using System.Reflection;
using PMS.Libraries.ToolControls.PMSPublicInfo;
using Org.BouncyCastle.Crypto.Tls;
using MES.FormLib.Controls.Expressions.Functions;
using MES.FormLib.Controls.Expressions.Operators;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Yahoo.Yui.Compressor;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using Microsoft.Extensions.ObjectPool;
using System.Runtime.Remoting.Lifetime;

#endregion

namespace NetSCADA.ReportEngine
{
	public static class External
	{
		public static Image GetBarCode (this BarCode barCode)
		{
			var bc = new PMS.Libraries.ToolControls.BarcodeLib.Barcode ();
			bc.Alignment = barCode.BarCodeAlign;
			bc.IncludeLabel = barCode.IncludeLabel;
			bc.LabelFont = barCode.LabelFont;
			Image img = bc.Encode (barCode.BarCodeType, barCode.RealText, barCode.ForeColor, barCode.BackColor, barCode.Width, barCode.Height);
			return img;
		}

		public static Image GetQrCode (this QRCode qrCode)
		{
			var _QRCodeControl = new QRCodeControl ();
			try {
				_QRCodeControl.IncludeLabel = qrCode.IncludeLabel;
				_QRCodeControl.backColor = qrCode.BackColor;
				_QRCodeControl.foreColor = qrCode.ForeColor;
				_QRCodeControl.CorrectionLevel = qrCode.CorrectionLevel;
				_QRCodeControl.EncodedMode = qrCode.EncodedMode;

				int h = qrCode.Height;
				int w = qrCode.Width;
				int scale = Math.Min (h, w);
				scale = scale / 45;
				_QRCodeControl.QRCodeScale = scale;
				_QRCodeControl.Version = qrCode.Version;
				_QRCodeControl.RawData = qrCode.RealText;
				Image img = _QRCodeControl.PictureBoxImage;
				return img;
			} finally {
				_QRCodeControl.Dispose ();
				_QRCodeControl = null;

			}
		}

		public static HtmlTag StyleEx (this HtmlTag tag, string styleName, string styleVal, CssDefault defaultStyle)
		{
			if (defaultStyle.Exists (styleName.Trim (), styleVal.Trim ()))
				return tag;
			return tag.Style (styleName, styleVal);
		}
			
	}

	public class MyRectF
	{
		public float X { get; set; }

		public float Y { get; set; }

		public float Left { get; set; }

		public float Top { get; set; }

		public float Width { get; set; }

		public float Height { get; set; }
	}

	public class CssDefault
	{
		private Dictionary<string,Dictionary<string,string>> cssClassDict = null;
		public static readonly string DefaultCssStyleName = "element";

		private CssDefault ()
		{
			cssClassDict = new Dictionary<string,Dictionary<string,string>> ();
			var defalutStyle = new Dictionary<string, string> () {
				{ "position", "absolute" },
				{ "color", "Black" },
				{ "background", "#FFFFFF" },
				{ "text-align", "left" },
				{ "border", "solid" },
				{ "border-color", "Black" },
				{ "border-width", "1px" }, {
					"font-family",
					string.Format (@"'{0}'", SystemFonts.DefaultFont.FontFamily.Name)
				}, {
					"font-size",
					string.Format ("{0}px", FontSize2Px (SystemFonts.DefaultFont.Size))
				}
			};
			cssClassDict.Add (string.Format (".{0}", DefaultCssStyleName), defalutStyle);

		}

		public int FontSize2Px (float fontSize)
		{
			return (int)System.Math.Floor (fontSize / 72 * 96);
		}

		public static CssDefault Instance ()
		{
			return new CssDefault ();
		}

		public bool Exists (string styleName, string styleVal, string className = "div")
		{
			Dictionary<string,string> dict = null;
			if (cssClassDict.TryGetValue (className, out dict)) {
				string val = string.Empty;
				if (dict.TryGetValue (styleName, out val)) {
					return val == styleVal;
				} else {
					return false;
				}
			} else {
				return false;
			}
		}

		public string ToCss ()
		{
			var sb = new StringBuilder (256);
			foreach (var item in cssClassDict) {
				sb.AppendLine (item.Key + "{");
				foreach (var style in item.Value) {
					sb.AppendLine (string.Format ("{0}:{1};", style.Key, style.Value));
				}
				sb.AppendLine ("}");
			}
			return sb.ToString ();
		}
	}

	internal class PieDataItem
	{
		//(new {name = xdata [i].ToString (),
		//value = datas [se.Name] [i].ToString (),
		//selected = false,itemStyle = default(object)});

		public string name { get; set; }

		public string value { get; set; }

		public bool selected { get; set; }

		public object itemStyle { get; set; }
	}

	internal class CurveDataItem
	{
		//new {Y = "", LineColor = "", LineStyle = 0, LineWidth = 1,	Data = new List<object[]> ()});
		public string Y { get; set; }

		public string LineColor { get; set; }

		public int LineStyle { get; set; }

		public int LineWidth { get; set; }

		public  List<object[]> Data { get; set; }
	}

	public class Extension
	{
		static readonly string HtmlDoc = @"<!DOCTYPE html><html><head><meta charset=""UTF-8""><title>{0}</title><script src=""../jquery-1.10.2.min.js""></script><script src=""../echarts.min.js""></script><style type=""text/css"">{2}</style></head><body>{1}</body></html>";
		static readonly string[] CustomKeys = {
			"#LABEL",
			"#LEGENDTEXT",
			"#AXISLABEL",
			"#SERIESNAME",
			"#SER",
			"#TOTAL",
			"#AVG",
			"#MAX",
			"#MIN",
			"#FIRST",
			"#LAST",
			"#INDEX",
			"#PERCENT",
			"#VALX",
			"#VAL"
		};

		CssDefault cssDefault = null;
		//todo: qiuleilei
		Dictionary<int,StringBuilder> HtmlDict = null;
		Dictionary<int,StringBuilder> JavaScriptDict = null;
		DefaultObjectPool<DivTag> tagPool = null;
		DefaultPooledObjectPolicy<DivTag> tagPolicy = null;

		public Extension ()
		{
			cssDefault = CssDefault.Instance ();
			HtmlDict = new Dictionary<int, StringBuilder> ();
			JavaScriptDict = new Dictionary<int, StringBuilder> ();
			tagPolicy = new DefaultPooledObjectPolicy<DivTag> ();
			tagPool = new DefaultObjectPool<DivTag> (tagPolicy);
		}


		public string ToHtml (ReportPage page)
		{
			if (HtmlDict == null || HtmlDict.Count == 0) {
				return string.Empty;
			}
			StringBuilder sb = null;
			StringBuilder jsb = null;
			if (HtmlDict.TryGetValue (page.PageNumber, out sb)) {
				var script = string.Empty;

				if (JavaScriptDict != null && JavaScriptDict.TryGetValue (page.PageNumber, out jsb)) {
					/*script = @" <script type=""text/javascript"">  $(function () {" + jsb.ToString () + " });  </script>";*/
					script = string.Format (@" <script type=""text/javascript"">{0}</script>", jsb.ToString ());
				}
				return string.Format (HtmlDoc, page.PageNumber, sb.ToString () + script, CssDefault.Instance ().ToCss ());
			} else {
				return string.Empty;
			}
		}


		public void CleartHtml ()
		{
			foreach (var kv in HtmlDict) {
				kv.Value.Clear ();
			}
			HtmlDict.Clear ();
			foreach (var kv in JavaScriptDict) {
				kv.Value.Clear ();
			}
			JavaScriptDict.Clear ();
		}

		public void Release ()
		{
			CleartHtml ();
			this.HtmlDict = null;
			this.JavaScriptDict = null;
			tagPolicy = null;
			tagPool = null;
		}

		public int FontSize2Px (float fontSize)
		{
			return (int)System.Math.Floor (fontSize / 72 * 96);
		}

		public void Cell2Html (ReportPage page, ICell cell, PointF location)
		{
			if (cell == null)
				return;
			var tag = tagPool.Get ();//new DivTag ();
			tag.Dispose ();// 清理数据
			tag.AddClass (CssDefault.DefaultCssStyleName);
			tag.Text (cell.RealText)
				.StyleEx ("position", "absolute", cssDefault)
				.StyleEx ("width", string.Format ("{0}px", cell.Width), cssDefault)
				.StyleEx ("height", string.Format ("{0}px", cell.Height), cssDefault)
				.StyleEx ("top", string.Format ("{0}px", location.Y), cssDefault)
				.StyleEx ("left", string.Format ("{0}px", location.X), cssDefault)
				.StyleEx ("color", ColorTranslator.ToHtml (cell.Style.ForeColor), cssDefault);
//				.Style ("font-family", string.Format ("\'{0}\'", cell.Style.Font.FontFamily.Name))
//				.Style ("font-size", string.Format ("{0}px", cell.Style.Font.Size));
			if (cell.Style.Font.Bold) {
				tag.StyleEx ("font-weight", "bold", cssDefault);
			}
			if (cell.Style.Font.Italic) {
				tag.StyleEx ("font-style", "italic", cssDefault);
			}
			if (cell.Style.Font.Underline) {
				tag.StyleEx ("text-decoration", "underline", cssDefault);
			}
			
			var gradient = cell.Style.BkColor as LinearGradientColor;
			if (gradient == null) {
				var singleColor = cell.Style.BkColor as SingleBkColor;
				if (singleColor != null)
					tag.StyleEx ("background", ColorTranslator.ToHtml (Color.FromArgb (singleColor.BkColor.R, singleColor.BkColor.G, singleColor.BkColor.B)), cssDefault);
			} else {
				tag.StyleEx ("background", string.Format ("linear-gradient({0}deg, {1}, {2})", gradient.Angle, ColorTranslator.ToHtml (gradient.StartColor), ColorTranslator.ToHtml (gradient.EndColor)), cssDefault);
			}



			switch (cell.TextAlign) {
			case ContentAlignment.TopLeft:
			case ContentAlignment.BottomLeft:
			case ContentAlignment.MiddleLeft:
				tag.StyleEx ("text-align", "left", cssDefault);
				break;
			case ContentAlignment.MiddleCenter:
			case ContentAlignment.TopCenter:
			case ContentAlignment.BottomCenter:
				tag.StyleEx ("text-align", "center", cssDefault);
				break;
			case ContentAlignment.BottomRight:
			case ContentAlignment.TopRight:
			case ContentAlignment.MiddleRight:
				tag.StyleEx ("text-align", "right", cssDefault);
				break;
			}

			if (cell.Border != null && cell.Border.Edge != BorderEdge.None) {
				var borderStyle = "solid";
				var colorStr = ColorTranslator.ToHtml (cell.Border.Color);

				switch (cell.Border.DashStyle) {
				case DashStyle.Dash:
					borderStyle = "dashed";
					break;
				case DashStyle.DashDot:
				case DashStyle.DashDotDot:
				case DashStyle.Dot:
					borderStyle = "dotted";
					break;
				default:
					break;
				}

				if (cell.Border.Edge != BorderEdge.All) {
					switch (cell.Border.Edge) {
					case BorderEdge.Bottom:
						tag.StyleEx ("border-bottom", borderStyle, cssDefault);
						break;
					case BorderEdge.Left:
						tag.StyleEx ("border-left", borderStyle, cssDefault);
						break;
					case BorderEdge.Right:
						tag.StyleEx ("border-right", borderStyle, cssDefault);
						break;
					case BorderEdge.Top:
						tag.StyleEx ("bordet-top", borderStyle, cssDefault);
						break;
					}
				} else {
					tag.StyleEx ("border", borderStyle, cssDefault);
				}

				tag.StyleEx ("border-color", colorStr, cssDefault).StyleEx ("border-width", "1px", cssDefault);
			}
			int fontSize = FontSize2Px (cell.Style.Font.Size);
			tag.StyleEx ("font-family", string.Format ("\'{0}\'", cell.Style.Font.FontFamily.Name), cssDefault)
				.StyleEx ("font-size", string.Format ("{0}px", fontSize), cssDefault);
				
			StringBuilder sb = null;
			if (HtmlDict.TryGetValue (page.PageNumber, out sb)) {
				sb.AppendLine (tag.ToHtmlString ());

			} else {
				sb = new StringBuilder (256);
				sb.AppendLine (tag.ToHtmlString ());
				HtmlDict.Add (page.PageNumber, sb);
			}
			//tag.Dispose ();
			//tag = null;
		}

		public HtmlTag Element2Html (ReportPage page, IElement element, PointF location, float width, float height, bool isClosed = true)
		{
			if (element == null)
				return null;
			element.Width = (int)width;
			element.Height = (int)height;
			var tag = tagPool.Get ();//new DivTag ();
			tag.Dispose ();// 清理数据
			tag.AddClass (CssDefault.DefaultCssStyleName);
			if (element is ElementBase) {
				tag.Text ((element as ElementBase).RealText);
			}
			tag.StyleEx ("position", "absolute", cssDefault)
				.StyleEx ("width", string.Format ("{0}px", element.Width), cssDefault)
				.StyleEx ("height", string.Format ("{0}px", element.Height), cssDefault)
				.StyleEx ("top", string.Format ("{0}px", location.Y), cssDefault)
				.StyleEx ("left", string.Format ("{0}px", location.X), cssDefault);
			var flag = element is PmsPanel;
			if (!flag)
				tag.StyleEx ("color", ColorTranslator.ToHtml (Color.FromArgb (element.ForeColor.R, element.ForeColor.G, element.ForeColor.B)), cssDefault);
//				.Style ("font-family", string.Format ("\'{0}\'", element.Font.FontFamily.Name))
//				.Style ("font-size", string.Format ("{0}px", element.Font.Size));


			var bkColor = element.BackColor;
			if (bkColor == Color.Transparent) {
				//background-color:rgba(0,0,0,0.2)
				tag.StyleEx ("background", ColorTranslator.ToHtml (Color.White), cssDefault).Style ("background-color", "rgba(0,0,0,0)");
			} else {
				tag.StyleEx ("background", ColorTranslator.ToHtml (Color.FromArgb (element.BackColor.R, element.BackColor.G, element.BackColor.B)), cssDefault);
			}
				
			if (element.Border != null && element.HasBorder) {
				var borderStyle = "solid";
				var colorStr = ColorTranslator.ToHtml (Color.FromArgb (element.Border.BorderColor.R, element.Border.BorderColor.G, element.Border.BorderColor.B));

				switch (element.Border.DashStyle) {
				case DashStyle.Dash:
					borderStyle = "dashed";
					break;
				case DashStyle.DashDot:
				case DashStyle.DashDotDot:
				case DashStyle.Dot:
					borderStyle = "dotted";
					break;
				default:
					break;
				}

				if ((element.HasBottomBorder && element.HasLeftBorder && element.HasRightBorder && element.HasTopBorder)) {
					tag.StyleEx ("border", borderStyle, cssDefault);

				} else {
					if (element.HasBottomBorder)
						tag.StyleEx ("border-bottom", borderStyle, cssDefault);
					if (element.HasLeftBorder)
						tag.StyleEx ("border-left", borderStyle, cssDefault);
					if (element.HasRightBorder)
						tag.StyleEx ("border-right", borderStyle, cssDefault);
					if (element.HasTopBorder)
						tag.StyleEx ("bordet-top", borderStyle, cssDefault);
				}

				tag.StyleEx ("border-color", colorStr, cssDefault).StyleEx ("border-width", string.Format ("{0}px", element.Border.BorderWidth), cssDefault);
			}
			if (!flag) {
				int fontSize = FontSize2Px (element.Font.Size);
				tag.StyleEx ("font-family", string.Format ("\'{0}\'", element.Font.FontFamily.Name), cssDefault)
					.StyleEx ("font-size", string.Format ("{0}px", fontSize), cssDefault);
				if (element.Font.Bold) {
					tag.StyleEx ("font-weight", "bold", cssDefault);
				}
				if (element.Font.Italic) {
					tag.StyleEx ("font-style", "italic", cssDefault);
				}
				if (element.Font.Underline) {
					tag.StyleEx ("text-decoration", "underline", cssDefault);
				}
			}
			if (isClosed) {
				AppendToHtmlStr (page, tag);
				//tag.Dispose ();
				return null;
			}

			return tag;
		}

		public void Label2Html (ReportPage page, PmsLabel label, PointF location, float width, float height)
		{
			label.Width = (int)width;
			label.Height = (int)height;
			var tag = Element2Html (page, label, location, width, height, false);
			if (tag == null)
				return;
			tag.AddClass (CssDefault.DefaultCssStyleName);
			switch (label.TextAlign) {
			case ContentAlignment.TopLeft:
			case ContentAlignment.BottomLeft:
			case ContentAlignment.MiddleLeft:
				tag.StyleEx ("text-align", "left", cssDefault);
				break;
			case ContentAlignment.MiddleCenter:
			case ContentAlignment.TopCenter:
			case ContentAlignment.BottomCenter:
				tag.StyleEx ("text-align", "center", cssDefault);
				break;
			case ContentAlignment.BottomRight:
			case ContentAlignment.TopRight:
			case ContentAlignment.MiddleRight:
				tag.StyleEx ("text-align", "right", cssDefault);
				break;
			}

			AppendToHtmlStr (page, tag);
			//tag.Dispose ();
			//tag = null;
		}

		public void Image2Html (ReportPage page, PmsImageBox img, PointF location, float Width, float height)
		{
			var tag = Element2Html (page, img, location, Width, height, false);
			if (tag == null)
				return;
			tag.Text (string.Empty);
			tag.AddClass (CssDefault.DefaultCssStyleName);
			img.Width = (int)Width;
			img.Height = (int)height;
			if (img.Image != null) {
				try {
					using (var m = new MemoryStream ()) {
						img.Image.Save (m, ImageFormat.Png);
						byte[] b = m.GetBuffer ();
						string base64string = Convert.ToBase64String (b);
						string url = string.Format ("url(data:image/png;base64,{0})", base64string);
						if (img.Mode == PictureBoxSizeMode.StretchImage)
							tag.Style ("background-image", url).StyleEx ("background-size", "100% 100%", cssDefault);
						else {
							tag.Style ("background-image", url).StyleEx ("background-size", "contain", cssDefault);
						}

						tag.StyleEx ("background-repeat", "no-repeat", cssDefault);
						AppendToHtmlStr (page, tag);
						//tag.Dispose ();
						//tag = null;
					}
				} catch (Exception e) {
					System.Diagnostics.Debug.WriteLine (e.Message);
				}
			}
		}

		public void BarCode2Html (ReportPage page, BarCode barCode, PointF location, float Width, float height)
		{
			var tag = Element2Html (page, barCode, location, Width, height, false);
			if (tag == null)
				return;
			tag.AddClass (CssDefault.DefaultCssStyleName);
			tag.Text (string.Empty).Style ("border", "none");
			barCode.Width = (int)Width;
			barCode.Height = (int)height;
			using (var img = barCode.GetBarCode ()) {
				if (img != null) {
					try {
						using (System.IO.MemoryStream m = new System.IO.MemoryStream ()) {
							img.Save (m, ImageFormat.Png);
							byte[] b = m.GetBuffer ();
							string base64string = Convert.ToBase64String (b);
							string url = string.Format ("url(data:image/png;base64,{0})", base64string);
							tag.Style ("background-image", url).StyleEx ("background-size", "contain", cssDefault).StyleEx ("background-repeat", "no-repeat", cssDefault);
							AppendToHtmlStr (page, tag);
							tag.Dispose ();
							//tag = null;
						}
					} catch (Exception e) {
						System.Diagnostics.Debug.WriteLine (e.Message);
					}
				}
			}
		}

		public void QrCode2Html (ReportPage page, QRCode qrCode, PointF location, float Width, float height)
		{
			var tag = Element2Html (page, qrCode, location, Width, height, false);
			if (tag == null)
				return;
			tag.Text (string.Empty).Style ("border", "none");
			tag.AddClass (CssDefault.DefaultCssStyleName);
			qrCode.Width = (int)Width;
			qrCode.Height = (int)height;
		
			using (var img = qrCode.GetQrCode ()) {
				if (img != null) {
					try {
						using (System.IO.MemoryStream m = new System.IO.MemoryStream ()) {
							img.Save (m, ImageFormat.Png);
							byte[] b = m.GetBuffer ();
							string base64string = Convert.ToBase64String (b);
							string url = string.Format ("url(data:image/png;base64,{0})", base64string);
							tag.Style ("background-image", url).StyleEx ("background-size", "contain", cssDefault).StyleEx ("background-repeat", "no-repeat", cssDefault);
							AppendToHtmlStr (page, tag);
							//tag.Dispose ();
							//tag = null;
						}
					} catch (Exception e) {
						System.Diagnostics.Debug.WriteLine (e.Message);
					}
				}
			}
		}

		/// <summary>
		/// 页眉 数据区 页脚 区域
		/// </summary>
		/// <param name="page">Page.</param>
		/// <param name="rect">Rect.</param>
		/// <param name="color">Color.</param>
		/// <param name="isExport">If set to <c>true</c> is export.</param>
		public void ReportPageType2Html (ReportPage page, MyRectF rect, Color color, bool isExport)
		{
			if (!(isExport && System.Environment.OSVersion.Platform == PlatformID.Unix))
				return;
			var tag = tagPool.Get ();//new DivTag ();
			tag.Dispose ();// 清理数据
			tag.StyleEx ("position", "absolute", cssDefault)
				.StyleEx ("width", string.Format ("{0}px", rect.Width), cssDefault)
				.StyleEx ("height", string.Format ("{0}px", rect.Height), cssDefault)
				.StyleEx ("top", string.Format ("{0}px", rect.Y), cssDefault)
				.StyleEx ("left", string.Format ("{0}px", rect.X), cssDefault)
				.StyleEx ("border", "none", cssDefault);
			if (color == Color.Transparent) {
				//background-color:rgba(0,0,0,0.2)
				tag.StyleEx ("background", ColorTranslator.ToHtml (Color.White), cssDefault).Style ("background-color", "rgba(0,0,0,0)");
			} else {
				tag.StyleEx ("background", ColorTranslator.ToHtml (Color.FromArgb (color.R, color.G, color.B)), cssDefault);
			}
			AppendToHtmlStr (page, tag);
			//tag.Dispose ();
			//tag = null;
		}

		public void ReportPageType2Html (ReportPage page, RectangleF rect, Color color, bool isExport)
		{
			if (!(isExport && System.Environment.OSVersion.Platform == PlatformID.Unix))
				return;
			var tag = tagPool.Get ();//new DivTag ();
			tag.Dispose ();// 清理数据
			tag.StyleEx ("position", "absolute", cssDefault)
				.StyleEx ("width", string.Format ("{0}px", rect.Width), cssDefault)
				.StyleEx ("height", string.Format ("{0}px", rect.Height), cssDefault)
				.StyleEx ("top", string.Format ("{0}px", rect.Y), cssDefault)
				.StyleEx ("left", string.Format ("{0}px", rect.X), cssDefault)
				.StyleEx ("border", "none", cssDefault);
			if (color == Color.Transparent) {
				//background-color:rgba(0,0,0,0.2)
				tag.StyleEx ("background", ColorTranslator.ToHtml (Color.White), cssDefault).Style ("background-color", "rgba(0,0,0,0)");
			} else {
				tag.StyleEx ("background", ColorTranslator.ToHtml (Color.FromArgb (color.R, color.G, color.B)), cssDefault);
			}
			AppendToHtmlStr (page, tag);
			//tag.Dispose ();
			//tag = null;
		}

		public void ToHtml (ReportPage page, ReportRuntime rptRuntime, object obj, PointF location, float Width, float height)
		{
//			var ctl = obj as Control;
//			if (ctl != null && reportElementIds.Contains(ctl.Name)) {
//				return;
//			}
//			if (ctl != null && !reportElementIds.Contains(ctl.Name)) {
//				reportElementIds.Add (ctl.Name);
//			}

			if (obj is PmsImageBox) {
				Image2Html (page, obj as PmsImageBox, location, Width, height);
				return;
			} else if (obj is PmsLabel) {
				Label2Html (page, obj as PmsLabel, location, Width, height);
				return;
			} else if (obj is ICell) {
				Cell2Html (page, obj as ICell, location);
				return;
			} else if (obj is BarCode) {
				BarCode2Html (page, obj as BarCode, location, Width, height);
				return;
			} else if (obj is QRCode) {
				QrCode2Html (page, obj as QRCode, location, Width, height);
				return;
			} else if (obj is BarChart) {
				BarChart2Html (page, rptRuntime.DataTables, obj as BarChart, location, Width, height);
				return;
			} //PieChart2Html
			else if (obj is PieChart) {
				PieChart2Html (page, rptRuntime.DataTables, obj as PieChart, location, Width, height);
				return;
			} else if (obj is NetSCADACurve) {
				CurveChart2Html (page, obj as NetSCADACurve, location, Width, height);
				return;
			} else if (obj is IElement) {
				Element2Html (page, obj as IElement, location, Width, height);
			}
		}

		public void CurveChart2Html (ReportPage page, NetSCADACurve curve, PointF location, float width, float height, bool isClosed = true)
		{
			if (curve == null)
				return;
			curve.Width = (int)width;
			curve.Height = (int)height;
			var tag = tagPool.Get ();//new DivTag ();
			tag.Dispose ();// 清理数据
			var curveName = string.Format ("{0}{1}", curve.Name, Guid.NewGuid ().ToString ("N"));
			tag.Id (curveName);
			tag.StyleEx ("position", "absolute", cssDefault)
				.StyleEx ("width", string.Format ("{0}px", curve.Width), cssDefault)
				.StyleEx ("height", string.Format ("{0}px", curve.Height), cssDefault)
				.StyleEx ("top", string.Format ("{0}px", location.Y), cssDefault)
				.StyleEx ("left", string.Format ("{0}px", location.X), cssDefault);
			if (curve.AryCurve.GetShowCount () == 0)
				return;
			//组织xy数据
			var cures = curve.AryCurve.GetShowCurveAry ();
			Dictionary<string,CurveDataItem> xyDataDict = new Dictionary<string, CurveDataItem> ();
			List<object[]> dataList = null;
			Dictionary<string,float> max = new Dictionary<string, float> ();
			var format = "yyyy-MM-dd HH:mm:ss";
			//var axisDisplayFromat = curve.CoordinateX.TimeFormat;
			var xData = cures.FirstOrDefault ().data.Select (d => d.DateTime.ToString (format)).Distinct ().ToList ();
			foreach (Curve c in cures) {
				
				if (!xyDataDict.ContainsKey (c.Name)) {
					dataList = c.data.Select ((d) => {
						//max = max < d.Value ? d.Value : max;
						if (!max.ContainsKey (c.strCoordY) || max [c.strCoordY] < d.Value) {
							max [c.strCoordY] = d.Value;
						}
						return new object[] { d.DateTime.ToString (format), d.Value };
					}).ToList ();
					xyDataDict.Add (c.Name, new CurveDataItem {
						Y = c.strCoordY,
						LineColor = ColorTranslator.ToHtml (Color.FromArgb (c.Line.Color.R, c.Line.Color.G, c.Line.Color.B)),
						LineStyle = c.Line.Style,
						LineWidth = c.Line.Width,
						Data = dataList
					});
				} 
			}

			// 基准线数据
			var datumCurves = curve.AryDatumCurve.GetShowCurveAry ();
			foreach (Curve c in datumCurves) {
				if (!xyDataDict.ContainsKey (c.Name)) {
					dataList = c.data.Select ((d) => {
						if (!max.ContainsKey (c.strCoordY) || max [c.strCoordY] < d.Value) {
							max [c.strCoordY] = d.Value;
						}
						var k = d.DateTime.ToString (format);
						if (xData.All (x => x != k)) {
							xData.Add (k);
						}
						return new object[] { k, d.Value };
					}).ToList ();
					xyDataDict.Add (c.Name, new CurveDataItem {Y = c.strCoordY,
						LineColor = ColorTranslator.ToHtml (Color.FromArgb (c.Line.Color.R, c.Line.Color.G, c.Line.Color.B)),
						LineStyle = c.Line.Style,
						LineWidth = c.Line.Width,
						Data = dataList
					});
				} 
			}
			if (max != null && max.Count != 0) {
				var mx = max.FirstOrDefault ();
				var ymax = curve.AryY._coordinateYList.ToArray ().FirstOrDefault (y => (y as CoordinateY).Name == mx.Key) as CoordinateY;
				if (ymax != null && mx.Value < ymax.Max) {
					max [mx.Key] = ymax.Max;
				}
			}

			var featureTagDict = new Dictionary<string,List<string[]>> ();
			//特征标签
			foreach (var f in curve.AryFeatureTags) {
				var ftag = f as FeatureTags;
				if (ftag == null || !ftag.ShowLine)
					continue;
				if (!xyDataDict.ContainsKey (ftag.Name)) {
					var featureDatas = ftag.TagsData.Select ((d) => {
						var k = d.DateTime.ToString (format);
						if (xData.All (x => x != k)) {
							xData.Add (k);
						}
						var values = d.StringArray.ConvertAll ((s) => s.ToString ());
						return new string[] { k, string.Join (" ", values) };
					}).ToList ();
					featureTagDict [ftag.Name] = featureDatas;
					var datas = ftag.TagsData.Select ((d) => {
						return new object[] {
							d.DateTime.ToString (format),
							max.FirstOrDefault ().Value
						};
					}).ToList ();
					xyDataDict.Add (ftag.Name, new CurveDataItem {Y = max.FirstOrDefault ().Key,
						LineColor = ColorTranslator.ToHtml (Color.FromArgb (ftag.Linellae.Color.R, 
							ftag.Linellae.Color.G, 
							ftag.Linellae.Color.B)),
						LineStyle = ftag.Linellae.Style,
						LineWidth = ftag.Linellae.Width,
						Data = datas
					});
				} 
			}

			if (xyDataDict != null && xyDataDict.Count != 0) {
				xData.Sort ();
				var op = GetCurveChartOption (curve, xData, xyDataDict, featureTagDict);
				//string script = @" var myChart" + curve.Name + " = echarts.init(document.getElementById('" + curve.Name + "'));" + @"  var option =" + op + @"; myChart" + curve.Name + ".setOption(option);\n";
				string script = string.Format (@"$(function () {{ var my{0} = echarts.init(document.getElementById('{1}')); var option{2} ={3}; my{4}.setOption(option{5});}});", curveName, curveName, curveName, op, curveName, curveName);		
				AppendToJavaScriptStr (page, script);
			}
			AppendToHtmlStr (page, tag);
			//tag.Dispose ();
			//tag = null;
			//return tag;
		}

		void SetDataZoom (ChartOption option)
		{
			// x 轴缩放
			option.DataZoom ().Show (true).height = 30;
			option.DataZoom ().xAxisIndex = 0;
			option.DataZoom ().bottom = 0;
			option.DataZoom ().Start (0).End (100);
		}

		string GetCurveChartOption (NetSCADACurve curve, List<string> xData, Dictionary<string,CurveDataItem> xyDataDict, Dictionary<string,List<string[]>> featureTagDict)
		{
			if (xyDataDict == null || xyDataDict.Count == 0 || xData == null || xData.Count == 0)
				return "{}";
			var option = new ChartOption ();
//			var title = new Title ();
			if (!string.IsNullOrEmpty (curve.Desgin.TitleBar.Text)) {
				option.Title ().Text (curve.Desgin.TitleBar.Text);
				switch (curve.Desgin.TitleBar.Area) {
				case PMS.Libraries.ToolControls.Base.Area.BOTTOM:
					option.Title ().Y (VerticalType.bottom).X (HorizontalType.center);
					break;
				case PMS.Libraries.ToolControls.Base.Area.TOP:
					option.Title ().Y (VerticalType.top).X (HorizontalType.center);
					break;
				}
				option.Title ().TextStyle ().Color (ColorTranslator.ToHtml (Color.FromArgb (curve.Desgin.TitleBar.Color.R, curve.Desgin.TitleBar.Color.G, curve.Desgin.TitleBar.Color.B)))
					.FontFamily (curve.Desgin.TitleBar.Font.FontFamily.Name).FontSize (FontSize2Px (curve.Desgin.TitleBar.Font.Size));
				if (curve.Desgin.TitleBar.Font.Bold)
					option.Title ().TextStyle ().FontWeight ("bold");
				if (curve.Desgin.TitleBar.Font.Italic)
					option.Title ().TextStyle ().FontStyle (FontStyleType.italic);
			}
			var flag = curve.AryFeatureTags.ToArray ().Count (f => {
				var ff = f as FeatureTags;
				if (ff == null)
					return false;
				return ff.ShowLine;
			});
			if (flag == 0) {
				option.ToolTip ().Trigger (TriggerType.axis);
			} else {
				var features = string.Join (",", featureTagDict.Keys.Select (f => string.Format ("'{0}'", f)));
				option.ToolTip ().Trigger (TriggerType.axis).Formatter (new JRaw (@"function(p,ticket,callback){var result=[];" + string.Format (" seriesNames=[{0}]", features) + @";p.forEach(function(e){if(seriesNames.indexOf(e.seriesName)==-1){if(e.value != undefined){var a='<li><span style=""color:'+e.color+'"">●</span> '+e.seriesName+' : '+e.value.toString()+'</li>';result.push(a)}}});if(result.length!=0){var r='<ul style=""margin:0;padding:0;color:#ddd;font-size:15px;list-style-type: none"">'+result.join('')+'</ul>';return r}else return''}"));
			}

			if (curve.Desgin.Legend.Show) {
				option.legend = new Legend () { 
					data = xyDataDict.Keys.Where (k => !featureTagDict.ContainsKey (k))	 
				};
				var lend = curve.Desgin.Legend;
//				var x2 = 10;
				if (lend != null) {
					switch (lend.Area) {
					case PMS.Libraries.ToolControls.Base.Area.RIGHT:
						option.Legend ().Orient (OrientType.vertical).X (HorizontalType.right);
						option.Grid ().X2 (80);
						break;
					case PMS.Libraries.ToolControls.Base.Area.TOP:
						option.Legend ().Orient (OrientType.horizontal).Y (VerticalType.top);
						if (curve.Desgin.TitleBar.Area == PMS.Libraries.ToolControls.Base.Area.TOP) {
							option.Grid ().X2 (80);
							option.Legend ().Orient (OrientType.vertical).X (HorizontalType.right).Y (VerticalType.top);
						}
						break;
					}
				}
			}
			var axisDisplayFromat = curve.CoordinateX.TimeFormat;
			var xDisplayData = xData.Select ((x) => {
				DateTime dt = DateTime.MaxValue;
				if (DateTime.TryParse (x, out dt)) {
					return new string[]{ x, dt.ToString (axisDisplayFromat) };
				}
				return null;
			}).ToArray ();
			var xDisplayDataStr = JsonConvert.SerializeObject (xDisplayData);
			var xAxis = new List<Axis> () {
				new CategoryAxis {
					name = curve.CoordinateX.Describe,
//					splitArea = new SplitArea (){ show = true },
//					splitLine = new SplitLine (){ show = true },
					splitLine = new SplitLine () { show = curve.Bisectrix.bisectrixNumX != 0,
						lineStyle = new LineStyle () {
							color = ColorTranslator.ToHtml (Color.FromArgb (curve.Bisectrix.bisectrixX.Color.R, curve.Bisectrix.bisectrixX.Color.G, curve.Bisectrix.bisectrixX.Color.B)),
							width = curve.Bisectrix.bisectrixX.Width,
							type = getLineStyleType (curve.Bisectrix.bisectrixX.Style)
						}
					},
					type = null,
					data = xData,
					axisTick = new AxisTick () {
						alignWithLabel = true
					},
					axisLabel = new AxisLabel () {
						//interval = 0,
						rotate = 360 - (int)curve.CoordinateX.LabelRotateDegree,
						formatter = new JRaw (@"function(value,index){var data=" + xDisplayDataStr + ";var item='';for(x in data){if(data[x][0]==value){item=data[x][1];break}}return item}")
					}
				}
			};
			const int width = 40;
			option.xAxis = xAxis;
			SetDataZoom (option);
			var yAxis = new List<Axis> ();
			var series = new List<object> ();
			var coordinateYs = curve.AryY._coordinateYList.ToArray ().ToList ().ConvertAll ((o) => o as CoordinateY);
			var ys = coordinateYs;//.Where ((o) => o.ShowAxis).ToList ();
			var yLeftCount = coordinateYs.Where ((o) => /*o.ShowAxis && */o.DisPosition == false).ToList ();
			var yRightCount = coordinateYs.Where ((o) => /*o.ShowAxis && */o.DisPosition).ToList ();
			int xx = (option.Grid ().x2 == null ? 0 : Convert.ToInt32 (option.Grid ().x2)) + yRightCount.Count * width;
			option.Grid ().X (yLeftCount.Count * width).X2 (xx);
			var index = -1;
			var yFirst = coordinateYs.FirstOrDefault ();
			foreach (var y in coordinateYs) {
				if (!y.DisPosition) { //left
					index = yLeftCount.FindIndex (c => c.Name == y.Name);

				} else {
					index = yRightCount.FindIndex (c => c.Name == y.Name);
				}

				if (index == -1)
					continue;
				var off = index * width;

				var yaxis = new ValueAxis () {
					type = AxisType.value,
					name = string.IsNullOrEmpty (y.Explain) ? y.Name : y.Explain,
					axisLine = new AxisLine () { lineStyle = new LineStyle () {
							width = y.AxisLine.Width,
							color = ColorTranslator.ToHtml (Color.FromArgb (y.AxisLine.Color.R, y.AxisLine.Color.G, y.AxisLine.Color.B))
						}
					},
//					splitArea = new SplitArea (){ show = true },
					splitLine = new SplitLine (){ show = false },
					position = y.DisPosition ? "right" : "left",
					offset = off
				};
				if (y.Name == yFirst.Name) {
					yaxis.splitLine = new SplitLine () { show = curve.Bisectrix.bisectrixNumY != 0,
						lineStyle = new LineStyle () {
							color = ColorTranslator.ToHtml (Color.FromArgb (curve.Bisectrix.bisectrixY.Color.R, curve.Bisectrix.bisectrixY.Color.G, curve.Bisectrix.bisectrixY.Color.B)),
							width = curve.Bisectrix.bisectrixY.Width,
							type = getLineStyleType (curve.Bisectrix.bisectrixY.Style)
						}
					};
				}
				if (!y.AutoInterval) {
					if (!double.IsNaN (y.Min)) {
						yaxis.min = y.Min;
					}
					if (!double.IsNaN (y.Max)) {
						yaxis.max = y.Max;
					}
				} else {
					yaxis.Max ("dataMax");
				}

				yAxis.Add (yaxis);
			}
			option.yAxis = yAxis;
			// series

			foreach (var kv in xyDataDict) {
				var da = kv.Value;///ChangeType (kv.Value, new {Y = "", LineColor = "", LineStyle = 0, LineWidth = 1,	Data = new List<object[]> ()});
				var i = ys.FindIndex ((y) => y.Name == da.Y);
				if (i == -1)
					continue;
				if (featureTagDict == null || featureTagDict.Count == 0 || !featureTagDict.ContainsKey (kv.Key)) {
					var se = new Line () {
						name = kv.Key,
						data = da.Data,
						yAxisIndex = i
					};
					var lineStyle = se.ItemStyle ().Normal ().Color (da.LineColor).LineStyle ().Width (da.LineWidth);
					switch (da.LineStyle) {
					case 0:
						lineStyle.Type (LineStyleType.solid);
						break;
					case 1:
						lineStyle.Type (LineStyleType.dashed);
						break;
					case 2:
						lineStyle.Type (LineStyleType.dotted);
						break;
					default:
						lineStyle.Type (LineStyleType.dotted);
						break;
					}

					series.Add (se);
				} else {
					var ss = JsonConvert.SerializeObject (featureTagDict);
					var se = new Bar () {
						name = kv.Key,
						data = da.Data,
						barWidth = da.LineWidth,
						yAxisIndex = i,
						itemStyle = new ItemStyle () {
							normal = new Normal () {
								color = da.LineColor,
								label = new StyleLabel () {
									show = true,
									position = StyleLabelTyle.inside,
									textStyle = new TextStyle (){ color = "#000000" },
									formatter = new JRaw (@"function(p){var a=" + ss + @";return function(list){var result='';for(var d in list){var aa=list[d];if(aa[0]==p.value[0]){result=aa[1];break;}}return result;}(a[p.seriesName])}")
								}
							}
						}
					};
					series.Add (se);
				}
			}
				
			option.series = series;

			var result = JsonTools.ObjectToJson2 (option);
			option = null;
			return result;
		}

		//		T ChangeType<T> (object obj, T t)
		//		{
		//			return (T)obj;
		//		}

		public void BarChart2Html (ReportPage page, DataTableManager dm, BarChart barChart, PointF location, float width, float height, bool isClosed = true)
		{
			if (barChart == null)
				return;
			barChart.Width = (int)width;
			barChart.Height = (int)height;
			var tag = tagPool.Get ();//new DivTag ();
			tag.Dispose ();// 清理数据
			var barChartName = string.Format ("{0}{1}", barChart.Name, Guid.NewGuid ().ToString ("N"));
			tag.Id (barChartName);
			tag.StyleEx ("position", "absolute", cssDefault)
				.StyleEx ("width", string.Format ("{0}px", barChart.Width), cssDefault)
				.StyleEx ("height", string.Format ("{0}px", barChart.Height), cssDefault)
				.StyleEx ("top", string.Format ("{0}px", location.Y), cssDefault)
				.StyleEx ("left", string.Format ("{0}px", location.X), cssDefault);
			var fieldNameDict = new Dictionary<string,Dictionary<string,string>> ();
			var dataDict = new Dictionary<string,List<string>> ();
			var yHash = new Dictionary<string,Tuple<string,int,double,double>> ();
			var lendDict = new Dictionary<string,string> ();
			var seriesColorDict = new Dictionary<string,string> ();
			var i = 0;
			barChart.Apperence.SeriesList.ForEach ((se) => {
				if (string.IsNullOrEmpty (se.CustomProperties))
					return;
				var dict = se.CustomProperties.Split (',').ToDictionary (s => s.Split ('=').FirstOrDefault ().Trim (), s => s.Split ('=').LastOrDefault ().Trim ());
				if (!yHash.ContainsKey (dict ["YAxisAreaName"])) {
					yHash.Add (dict ["YAxisAreaName"], new Tuple<string, int,double,double> (se.Name, i, double.MaxValue, double.MaxValue));
				}
				i++;
				if (!fieldNameDict.ContainsKey (se.Name))
					fieldNameDict.Add (se.Name, dict);
				if (!lendDict.ContainsKey (se.Name)) {
					// 如果自定义 则替换成序列名 具体说明 见GetBarChartOption 方法
					var lTxt = (string.IsNullOrEmpty (se.LegendText) || CustomKeys.Any (se.LegendText.Contains)) ? se.Name : se.LegendText;
					lendDict.Add (se.Name, lTxt);
				}
//				if (!seriesColorDict.ContainsKey (se.Name)) {	
//					if (se.Color != Color.Empty) {
//						seriesColorDict.Add (se.Name, ColorTranslator.ToHtml (Color.FromArgb (se.Color.R, se.Color.G, se.Color.B)));
//					} else {
//						seriesColorDict.Add (se.Name, "#418CF0");
//					}
//				}
			});
			if (fieldNameDict != null && fieldNameDict.Count != 0) {
				#region 原来使用数据集 获取数据的方式
//				var dt = dm.GetDataTable (barChart.SourceField.TableName);
//				var x = "x";
//				foreach (DataRow dr in dt.Rows) {
//					// x 轴数据
//
//					fieldNameDict.FirstOrDefault ().Value.TryGetValue ("XBindingField", out x);
//					List<object> xlist = null;
//					if (dataDict.TryGetValue ("x", out xlist)) {
//						xlist.Add (dr [x]);
//					} else {
//						xlist = new List<object> ();
//						xlist.Add (dr [x]);
//						dataDict.Add ("x", xlist);
//					}
//					// y 轴数据 
//					foreach (var s in barChart.Apperence.SeriesList) {
//						var y = fieldNameDict [s.Name] ["YBindingField"];
//						List<object> yList = null;
//						if (dataDict.TryGetValue (s.Name, out yList)) {
//							yList.Add (dr [y]);
//						} else {
//							yList = new List<object> ();
//							yList.Add (dr [y]);
//							dataDict.Add (s.Name, yList);
//						}
//					}
//				}
				#endregion

				var x = "x";
				// x 轴数据
				List<string> xlist = null;
				List<string> yList = null;
				// y 轴数据 
				foreach (var s in barChart.Apperence.SeriesList) {
					var chartSeries = barChart.ChartSeries.FirstOrDefault ((cs) => cs.Name == s.Name);
					if (chartSeries == null)
						continue;
					foreach (var p in chartSeries.Points) {
						
						if (dataDict.TryGetValue (x, out xlist)) {
							xlist.Add (p.AxisLabel);
						} else {
							xlist = new List<string> ();
							xlist.Add (p.AxisLabel);
							dataDict.Add (x, xlist);
						}

						if (dataDict.TryGetValue (s.Name, out yList)) {
							yList.Add (p.YValues [0].ToString ());
						} else {
							yList = new List<string> ();
							yList.Add (p.YValues [0].ToString ());
							dataDict.Add (s.Name, yList);
						}
					}
				}



				GetGridStyle (tag, barChart.Apperence.ChartAreaList.FirstOrDefault ());
				var op = GetBarChartOption (barChart, dataDict, yHash, lendDict, seriesColorDict, fieldNameDict);
				//string script = @" var myChart" + barChart.Name + " = echarts.init(document.getElementById('" + barChart.Name + "'));" + @"  var option =" + op + @"; myChart" + barChart.Name + ".setOption(option);";
				string script = string.Format (@"$(function () {{ var my{0} = echarts.init(document.getElementById('{1}')); var option{2} ={3}; my{4}.setOption(option{5});}});", barChartName, barChartName, barChartName, op, barChartName, barChartName);
				AppendToJavaScriptStr (page, script);
			}
			AppendToHtmlStr (page, tag);
			//tag.Dispose ();
			//tag = null;
			//return tag;
		}

		public void PieChart2Html (ReportPage page, DataTableManager dm, PieChart pieChart, PointF location, float width, float height, bool isClosed = true)
		{
			if (pieChart == null)
				return;
			pieChart.Width = (int)width;
			pieChart.Height = (int)height;
			string pieChartName = string.Format ("{0}{1}", pieChart.Name, Guid.NewGuid ().ToString ("N"));
			var tag = tagPool.Get ();
			tag.Dispose ();
			tag.Id (pieChartName);
			tag.StyleEx ("position", "absolute", cssDefault)
				.StyleEx ("width", string.Format ("{0}px", pieChart.Width), cssDefault)
				.StyleEx ("height", string.Format ("{0}px", pieChart.Height), cssDefault)
				.StyleEx ("top", string.Format ("{0}px", location.Y), cssDefault)
				.StyleEx ("left", string.Format ("{0}px", location.X), cssDefault);
			var fieldNameDict = new Dictionary<string,Dictionary<string,string>> ();
			var dataDict = new Dictionary<string,List<string>> ();
			var yHash = new Dictionary<string,Tuple<string,int>> ();
			var lendDict = new Dictionary<string,string> ();
			var seriesColorDict = new Dictionary<string,string> ();
			var i = 0;
			pieChart.Apperence.SeriesList.ForEach ((se) => {
				if (string.IsNullOrEmpty (se.CustomProperties))
					return;
				var dict = se.CustomProperties.Split (',').ToDictionary (s => s.Split ('=').FirstOrDefault ().Trim (), s => s.Split ('=').LastOrDefault ().Trim ());
				//string seName = string.IsNullOrEmpty (se.Name) ? "Series1" : se.Name;
				if (!yHash.ContainsKey (dict ["YBindingField"])) {
					yHash.Add (dict ["YBindingField"], new Tuple<string, int> (se.Name, i));
				}
				i++;
				if (!fieldNameDict.ContainsKey (se.Name))
					fieldNameDict.Add (se.Name, dict);
				if (!lendDict.ContainsKey (se.Name)) {
					var lTxt = string.IsNullOrEmpty (se.LegendText) ? se.Name : se.LegendText;
					lendDict.Add (se.Name, lTxt);
				}
				/*			if (!seriesColorDict.ContainsKey (se.Name)) {	
					if (se.Color != Color.Empty) {
						seriesColorDict.Add (se.Name, ColorTranslator.ToHtml (Color.FromArgb (se.Color.R, se.Color.G, se.Color.B)));
					} else {
						seriesColorDict.Add (se.Name, "#418CF0");
					}
				}*/
			});
			if (fieldNameDict != null && fieldNameDict.Count != 0) {
				#region MyRegion
//				var dt = dm.GetDataTable (pieChart.SourceField.TableName);
//				var x = "x";
//				foreach (DataRow dr in dt.Rows) {
//					// x 轴数据
//
//					fieldNameDict.FirstOrDefault ().Value.TryGetValue ("XBindingField", out x);
//					List<object> xlist = null;
//					if (dataDict.TryGetValue ("x", out xlist)) {
//						xlist.Add (dr [x]);
//					} else {
//						xlist = new List<object> ();
//						xlist.Add (dr [x]);
//						dataDict.Add ("x", xlist);
//					}
//					// y 轴数据 
//					foreach (var s in pieChart.Apperence.SeriesList) {
//						var y = fieldNameDict [s.Name] ["YBindingField"];
//						List<object> yList = null;
//						if (dataDict.TryGetValue (s.Name, out yList)) {
//							yList.Add (dr [y]);
//						} else {
//							yList = new List<object> ();
//							yList.Add (dr [y]);
//							dataDict.Add (s.Name, yList);
//						}
//					}
//				}

				var x = "x";
				// x 轴数据
				List<string> xlist = null;
				List<string> yList = null;
				// y 轴数据 
				foreach (var s in pieChart.Apperence.SeriesList) {
					var chartSeries = pieChart.ChartSeries.FirstOrDefault ((cs) => cs.Name == "Series1");
					if (chartSeries == null)
						continue;
					foreach (var p in chartSeries.Points) {

						if (dataDict.TryGetValue (x, out xlist)) {
							xlist.Add (p.AxisLabel);
						} else {
							xlist = new List<string> ();
							xlist.Add (p.AxisLabel);
							dataDict.Add (x, xlist);
						}

						if (dataDict.TryGetValue (s.Name, out yList)) {
							yList.Add (p.YValues [0].ToString ());
						} else {
							yList = new List<string> ();
							yList.Add (p.YValues [0].ToString ());
							dataDict.Add (s.Name, yList);
						}
					}
				}

				#endregion
				var op = GetPieChartOption (tag, pieChart, dataDict, yHash, lendDict, seriesColorDict, fieldNameDict);
				//script = string.Format ("$(function () {{{0}}});", script);
				string script = string.Format (@"$(function () {{ var my{0} = echarts.init(document.getElementById('{1}')); var option{2} ={3}; my{4}.setOption(option{5});}});", pieChartName, pieChartName, pieChartName, op, pieChartName, pieChartName);
				AppendToJavaScriptStr (page, script);
			}
			AppendToHtmlStr (page, tag);
			//tag.Dispose ();
			//tag = null;
			//return tag;
		}

		TextStyle GetTextStyle (Title title, PMSTitle titleApperence)
		{

			var txtStyle = new TextStyle (){ color = ColorTranslator.ToHtml (Color.FromArgb (titleApperence.ForeColor.R, titleApperence.ForeColor.G, titleApperence.ForeColor.B)) };

			if (!cssDefault.Exists ("font-family", titleApperence.Font.FontFamily.Name))
				txtStyle.FontFamily (titleApperence.Font.FontFamily.Name);
			if (!cssDefault.Exists ("font-size", FontSize2Px (titleApperence.Font.Size).ToString ()))
				txtStyle.FontSize (FontSize2Px (titleApperence.Font.Size));
			if (titleApperence.Font.Bold)
				txtStyle.FontWeight ("bold");
			if (titleApperence.Font.Italic)
				txtStyle.FontStyle (FontStyleType.italic);
			switch (titleApperence.Alignment) {
			case ContentAlignment.TopLeft:
				//txtStyle.Align (HorizontalType.left).Baseline (VerticalType.top);
				title.X (HorizontalType.left);
			//	title.Y(VerticalType.top);
				break;
			case ContentAlignment.BottomLeft:
				//txtStyle.Align(HorizontalType.left).Baseline(VerticalType.bottom);
				title.X (HorizontalType.left);
				//title.Y(VerticalType.bottom);
				break;
			case ContentAlignment.MiddleLeft:
				//txtStyle.Align(HorizontalType.left).Baseline(VerticalType.middle);
				title.X (HorizontalType.left);
				//title.Y(VerticalType.middle);
				break;	
			case ContentAlignment.TopCenter:
				//txtStyle.Align(HorizontalType.center).Baseline(VerticalType.top);
				title.X (HorizontalType.center);
				//title.Y(VerticalType.top);
				break;
			case ContentAlignment.BottomCenter:
				//txtStyle.Align(HorizontalType.center).Baseline(VerticalType.bottom);
				title.X (HorizontalType.center);
				//title.Y(VerticalType.bottom);
				break;
			case ContentAlignment.MiddleCenter:
				//txtStyle.Align(HorizontalType.center).Baseline(VerticalType.middle);
				title.X (HorizontalType.center);
				//title.Y(VerticalType.middle);
				break;	
			case ContentAlignment.TopRight:
				txtStyle.Align (HorizontalType.right).Baseline (VerticalType.top);
				title.X (HorizontalType.right);
				//title.Y(VerticalType.top);
				break;
			case ContentAlignment.BottomRight:
				//txtStyle.Align(HorizontalType.right).Baseline(VerticalType.bottom);
				title.X (HorizontalType.right);
				//title.Y(VerticalType.bottom);
				break;
			case ContentAlignment.MiddleRight:
				//txtStyle.Align(HorizontalType.right).Baseline(VerticalType.middle);
				title.X (HorizontalType.right);
				//title.Y(VerticalType.middle);
				break;	
			}
			return txtStyle;
		}

		string GetPieChartOption (DivTag divTag, PieChart pieChart, Dictionary<string,List<string>> datas, Dictionary<string,Tuple<string,int>> Ys, Dictionary<string,string> lendDict, Dictionary<string,string> seriesColorDict, Dictionary<string,Dictionary<string,string>> fieldNameDict)
		{
			if (datas == null || datas.Count == 0)
				return "{}";
			var option = new ChartOption ();
			var title = new Title ();
			if (pieChart.Apperence.TitleList.Count >= 1) {
				var titleApperence = pieChart.Apperence.TitleList.First ();

				var txtStyle = GetTextStyle (title, titleApperence);
				TextStyle subtxtStyle = null;
				var subTxt = string.Empty;
				title.Text (titleApperence.Text).textStyle = txtStyle;

				if (pieChart.Apperence.TitleList.Count >= 2) {
					subTxt = pieChart.Apperence.TitleList [1].Text;
					subtxtStyle = GetTextStyle (title, pieChart.Apperence.TitleList [1]);
					title.Subtext (subTxt).subtextStyle = subtxtStyle;
				}

				option.title = title;
			}
			GetGridStyle (divTag, pieChart.Apperence.ChartAreaList.FirstOrDefault ());
			//option.color = seriesColorDict.Values.ToList ();
			SetOptionColor (option);
			option.tooltip = new ECharts.Entities.ToolTip () { 
				trigger = TriggerType.item,
				formatter = "{a} <br/>{b} : {c} ({d}%)"
			};

			//option.legend = new Legend ();
			var lend = pieChart.Apperence.LegendList.First ();
			var x2 = 10;
			List<string> xdata = null;
			if (lend != null && pieChart.Apperence.SeriesList.FirstOrDefault ().IsVisibleInLegend) {
				switch (lend.Docking) {
				case System.Windows.Forms.DataVisualization.Charting.Docking.Bottom:
					option.Legend ()
					//.BorderWidth (lend.BorderWidth)
					//.BorderColor (ColorTranslator.ToHtml (Color.FromArgb(lend.BorderColor.R,lend.BorderColor.G,lend.BorderColor.B)))
						.Orient (OrientType.horizontal).Y (VerticalType.bottom)
						.TextStyle ()
					//.Baseline(VerticalType.bottom)
					//.Align(HorizontalType.center)
						.Color (ColorTranslator.ToHtml (Color.FromArgb (lend.ForeColor.R, lend.ForeColor.G, lend.ForeColor.B)));
					switch (lend.Alignment) {
					case StringAlignment.Near:
						option.Legend ().X (HorizontalType.left);
						break;
					case StringAlignment.Center:
						option.Legend ().X (HorizontalType.center);
						break;
					case StringAlignment.Far:
						option.Legend ().X (HorizontalType.right);
						break;
					}
					break;
				case System.Windows.Forms.DataVisualization.Charting.Docking.Left:
					option.Legend ()
					//.BorderWidth (lend.BorderWidth)
					//.BorderColor (ColorTranslator.ToHtml (Color.FromArgb(lend.BorderColor.R,lend.BorderColor.G,lend.BorderColor.B)))
						.Orient (OrientType.vertical).X (HorizontalType.left)
						.TextStyle ()
					//.Baseline(VerticalType.middle)
					//.Align(HorizontalType.left)
						.Color (ColorTranslator.ToHtml (Color.FromArgb (lend.ForeColor.R, lend.ForeColor.G, lend.ForeColor.B)));
					switch (lend.Alignment) {
					case StringAlignment.Near:
						option.Legend ().Y (VerticalType.top);
						break;
					case StringAlignment.Center:
						option.Legend ().Y (VerticalType.middle);
						break;
					case StringAlignment.Far:
						option.Legend ().Y (VerticalType.bottom);
						break;
					}
					break;

				case System.Windows.Forms.DataVisualization.Charting.Docking.Right:
					option.Legend ()
					//.BorderWidth (lend.BorderWidth)
					//.BorderColor (ColorTranslator.ToHtml (Color.FromArgb(lend.BorderColor.R,lend.BorderColor.G,lend.BorderColor.B)))
						.Orient (OrientType.vertical).X (HorizontalType.right)
						.TextStyle ()
					//.Baseline (VerticalType.middle)
					//.Align (HorizontalType.right)
						.Color (ColorTranslator.ToHtml (Color.FromArgb (lend.ForeColor.R, lend.ForeColor.G, lend.ForeColor.B)));
					switch (lend.Alignment) {
					case StringAlignment.Near:
						option.Legend ().Y (VerticalType.top);
						break;
					case StringAlignment.Center:
						option.Legend ().Y (VerticalType.middle);
						break;
					case StringAlignment.Far:
						option.Legend ().Y (VerticalType.bottom);
						break;
					}
					x2 = 80;
					option.Grid ().X2 (x2);
					break;
				case System.Windows.Forms.DataVisualization.Charting.Docking.Top:
					option.Legend ()
					//.BorderWidth (lend.BorderWidth)
					//.BorderColor (ColorTranslator.ToHtml (Color.FromArgb(lend.BorderColor.R,lend.BorderColor.G,lend.BorderColor.B)))
						.Orient (OrientType.horizontal).Y (VerticalType.top)
						.TextStyle ()
					//.Baseline(VerticalType.top)
					//.Align(HorizontalType.center)
						.Color (ColorTranslator.ToHtml (Color.FromArgb (lend.ForeColor.R, lend.ForeColor.G, lend.ForeColor.B)));
					switch (lend.Alignment) {
					case StringAlignment.Near:
						option.Legend ().Y (HorizontalType.left);
						break;
					case StringAlignment.Center:
						option.Legend ().Y (HorizontalType.center);
						break;
					case StringAlignment.Far:
						option.Legend ().Y (HorizontalType.right);
						break;
					}
					break;
				}

				datas.TryGetValue ("x", out xdata);
				if (xdata == null || xdata.Count == 0)
					return "{}";
				option.Legend ().Data (xdata);

			}

			var firstSeries = pieChart.Apperence.SeriesList.First ();
			var isCustomLegend = CustomKeys.Any (firstSeries.LegendText.Contains);
			var legendText = string.Empty;
			if (isCustomLegend) {
				legendText = ReplacePieKeywords (firstSeries.LegendText, xdata, datas);
				if (!string.IsNullOrEmpty (legendText)) {
					option.Legend ().Formatter (new JRaw (string.Format (@"function (name) {{ var aa={0};return aa[name];}}", legendText)));
				}
			}
				
			// 数据标签
			object dataLabelFormatter = null;
			var isCustomLabel = CustomKeys.Any (firstSeries.Label.Contains);
			var labelText = string.Empty;
			if (isCustomLabel) {
				labelText = ReplacePieKeywords (firstSeries.Label, xdata, datas);
				if (!string.IsNullOrEmpty (labelText)) {
					dataLabelFormatter = new JRaw (string.Format (@"function (parm) {{var aa={0}; return aa[parm.name];}}", labelText));
				}
			}
			List<PieDataItem> list = new List<PieDataItem> ();

			var series = new List<object> ();
			var seList = pieChart.Apperence.SeriesList.ToList ().Reverse<PMSSeries> ();
			foreach (var se in seList) {
				//var ss = fieldNameDict [se.Name] ["YAxisAreaName"];
				for (var i = 0; i < xdata.Count; i++) {
					list.Add (new PieDataItem () {
						name = xdata [i].ToString (),
						value = datas [se.Name] [i].ToString (),
						selected = false,
						itemStyle = default(object)
					});
				}

				// 默认排序
				/*		list.Sort ((x, y) => { 
					var xx = ChangeType (x, new {name = "",value = "",selected = false});
					var yy = ChangeType (y, new {name = "",value = "",selected = false});
					return -1 * xx.value.CompareTo (yy.value.ToString ());
				});*/

				if (se.IsVisibleInLegend) {
					var legendData = list.Select (o => {
						//var oo = ChangeType (o, new {name = "",value = "",selected = false,itemStyle = new Object ()});
						return o.name;
					}).ToList ();
					// 默认排序 lend data x轴数据顺序也需要变化
					option.Legend ().Data (legendData);
				}

				if (bool.TrueString == fieldNameDict [se.Name] ["CollectedSliceExploded"]) {

					List<string> legendDa = new List<string> ();
					var isCollect = CollectedSlice (se.Name, list, legendDa, fieldNameDict);
					if (isCollect) {
						option.Legend ().Data (legendDa);
						var str = string.Format ("{{'{0}':'{1}'}}", fieldNameDict [se.Name] ["CollectedLegendText"], fieldNameDict [se.Name] ["CollectedLabel"]);
						if (isCustomLegend) {
							if (!(string.IsNullOrEmpty (fieldNameDict [se.Name] ["CollectedLegendText"]) && string.IsNullOrEmpty (fieldNameDict [se.Name] ["CollectedLabel"])))
								option.Legend ().Formatter (new JRaw (string.Format (@"function (name) {{ var aa={0},bb={1};return aa[name]==undefined?bb[name]:aa[name];}}", legendText, str)));
		
						}
						if (!string.IsNullOrEmpty (labelText)) {
							if (!(string.IsNullOrEmpty (fieldNameDict [se.Name] ["CollectedLegendText"]) && string.IsNullOrEmpty (fieldNameDict [se.Name] ["CollectedLabel"])))
								dataLabelFormatter = new JRaw (string.Format (@"function (parm) {{var aa={0},bb={1}; return aa[name]==undefined?bb[name]:aa[name];}}", labelText, str));
				
						}
					}

				}


				var b = new Pie () {
					type = ChartType.pie,
					name = se.Name,
					data = list
				};
				var lbPosition = fieldNameDict [se.Name] ["PieLabelStyle"] == "Outside" ? StyleLabelTyle.outer : StyleLabelTyle.inside;
				var itemStyle = b.ItemStyle ();
				var flag = se.IsValueShownAsLabel || isCustomLabel;

				if (dataLabelFormatter == null) {
					dataLabelFormatter = new JRaw (@"function(o){ if(o.name==='" + fieldNameDict [se.Name] ["CollectedLegendText"] + "') return '" + fieldNameDict [se.Name] ["CollectedLabel"] + "'; return o.value;}");
				}
				var styleLabel = itemStyle.Normal ().Label ().Show (flag).Position (lbPosition)
					.Formatter (dataLabelFormatter);
				styleLabel.TextStyle ().FontSize (FontSize2Px (se.Font.Size));

				if (flag && se.LabelForeColor != Color.Empty && se.LabelForeColor != Color.Transparent) {
					styleLabel.TextStyle ().Color (ColorTranslator.ToHtml (Color.FromArgb (se.LabelForeColor.R, se.LabelForeColor.G, se.LabelForeColor.B)));
				}

				if (se.Font.Bold)
					itemStyle.Normal ().Label ().TextStyle ().FontWeight ("bold");
				if (se.Font.Italic)
					itemStyle.Normal ().Label ().TextStyle ().FontStyle (FontStyleType.italic);
				itemStyle.Normal ().LabelLine ().Show (flag);

				if (pieChart.ChartType == PieChartType.Pie) {
					b.Radius ("65%").Center (new List<string> (){ "30%", "55%" });
					//itemStyle.Normal ().LineStyle ().Color (Color.FromArgb (int.Parse (fieldNameDict [se.Name] ["PieLineColor"])));
					itemStyle.Emphasis ().LineStyle ().ShadowBlur (10).ShadowOffsetX (0).ShadowColor ("'rgba(0, 0, 0, 0.5)'");
				} else if (pieChart.ChartType == PieChartType.Doughnut) {
					b.Radius (new List<string> (){ "65%", "15%" }).Center (new List<string> () {
						"30%",
						"55%"
					});

					itemStyle.Emphasis ().Label ().Show (true).TextStyle ().FontSize (30).FontWeight ("bold");
				}
				series.Add (b);
			}
			option.series = series;

			var result = JsonTools.ObjectToJson2 (option);
			option = null;
			return result;
		}

		void GetGridStyle (DivTag divTag, PMSChartArea area)
		{
			if (area.BorderDashStyle == System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet)
				return;
			var borderStyle = "solid";
			switch (area.BorderDashStyle) {
			case System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash:
				borderStyle = "dashed";
				break;
			case System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot:
			case System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot:
			case System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot:
				borderStyle = "dotted";
				break;
			default:
				borderStyle = "solid";
				break;
			}
			divTag.Style ("border", borderStyle);
			divTag.Style ("border-color", ColorTranslator.ToHtml (Color.FromArgb (area.BorderColor.R, area.BorderColor.G, area.BorderColor.B)))
				.Style ("border-width", string.Format ("{0}px", area.BorderWidth));
//			area.BackGradientStyle
//			if (gradient == null) {
//				var singleColor = cell.Style.BkColor as SingleBkColor;
//				if (singleColor != null)
//					tag.StyleEx ("background", ColorTranslator.ToHtml (Color.FromArgb (singleColor.BkColor.R, singleColor.BkColor.G, singleColor.BkColor.B)), cssDefault);
//			} else {
//				tag.StyleEx ("background", string.Format ("linear-gradient({0}deg, {1}, {2})", gradient.Angle, ColorTranslator.ToHtml (gradient.StartColor), ColorTranslator.ToHtml (gradient.EndColor)), cssDefault);
//			}
		}

		LineStyleType getLineStyleType (System.Windows.Forms.DataVisualization.Charting.ChartDashStyle type)
		{
			var lineType = LineStyleType.solid;
			switch (type) {
			case System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash:
				lineType = LineStyleType.dashed;
				break;
			case System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot:
			case System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDotDot:
			case System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot:
				lineType = LineStyleType.dotted;
				break;
			default:
				lineType = LineStyleType.solid;
				break;
			}
			return lineType;
		}

		LineStyleType getLineStyleType (int style)
		{
			var lineStyle = LineStyleType.solid;
			switch (style) {
			case 0:
				lineStyle = LineStyleType.solid;
				break;
			case 1:
				lineStyle = LineStyleType.dashed;
				break;
			case 2:
				lineStyle = LineStyleType.dotted;
				break;
			default:
				lineStyle = LineStyleType.dotted;
				break;
			}
			return lineStyle;
		}

		bool CollectedSlice (string serieName, List<PieDataItem> list, List<string> xData, Dictionary<string,Dictionary<string,string>> fieldNameDict)
		{
			try {
				if (fieldNameDict == null || fieldNameDict.Count == 0 || fieldNameDict [serieName].Count == 0
				    || fieldNameDict [serieName] ["CollectedSliceExploded"] == bool.FalseString) {
					return false;
				}	
				
				var collectedThreshold = 0.0;
				double.TryParse (fieldNameDict [serieName] ["CollectedThreshold"], out collectedThreshold);
				var collectedThresholdUsePercent = fieldNameDict [serieName] ["CollectedThresholdUsePercent"] == bool.TrueString;
				var sum = 0.0;
				if (collectedThresholdUsePercent) {
					sum = list.Sum ((x) => { 
						//var xx = ChangeType (x, new {name = "",value = "",selected = false,itemStyle = default(object)});
						return double.Parse (x.value);
					});
				}
				var per = collectedThresholdUsePercent ? sum * collectedThreshold * 0.01 : collectedThreshold;
				var less = list.Where (xx => {
					//var xx = ChangeType (ll, new {name = "",value = "",selected = false,itemStyle = default(object)});
					return xx.value.CompareTo (per.ToString ()) <= 0;
				}).ToList ();
				if (less == null || less.Count <= 1)
					return false;
				
				var lessStrList = less.Select (xx => {
					//var xx = ChangeType (l, new {name = "",value = "",selected = false,itemStyle = default(object)});
					return xx.name + xx.value;
				});
				//				var lessFirst = ChangeType (less.FirstOrDefault (), new {name = "",value = ""});
				//				var index = list.FindIndex ((l) => {
				//					var xx = ChangeType (l, new {name = "",value = ""});
				//					return xx.name == lessFirst.name && xx.value == lessFirst.value;
				//				});
				//				if (index == -1)
				//					return;
				var lessSum = less.Sum ((xx) => {
					//var xx = ChangeType (l, new {name = "",value = "",selected = false,itemStyle = default(object)});
					return  double.Parse (xx.value);
				});
				
				list.RemoveAll ((xx) => {
					//var xx = ChangeType (r, new {name = "",value = "",selected = false,itemStyle = default(object)});
					return lessStrList.Any (s => s == xx.name + xx.value);
				});
				int collectColor = 0;
				if (int.TryParse (fieldNameDict [serieName] ["CollectedColor"], out collectColor)) {
					Color c = Color.FromArgb (collectColor);
					string cc = ColorTranslator.ToHtml (c);
					if (cc != "#000000") // 黑色 
						list.Add (new PieDataItem () {
							name = fieldNameDict [serieName] ["CollectedLegendText"],
							value = lessSum.ToString (),
							selected = true,
							itemStyle = new {normal = new {color = cc}}
						});
					else
						list.Add (new PieDataItem () {
							name = fieldNameDict [serieName] ["CollectedLegendText"],
							value = lessSum.ToString (),
							selected = true,
							itemStyle = default(object)
						});
				} else {
					list.Add (new PieDataItem () {
						name = fieldNameDict [serieName] ["CollectedLegendText"],
						value = lessSum.ToString (),
						selected = true,
						itemStyle = default(object)
					});
				}
				var legend = list.Select (s => {
					return s.GetType ().GetProperty ("name").GetValue (s, null).ToString ();
				});
				
				if (legend != null && legend.Count () != 0)
					xData.AddRange (legend);

				return true;
			} catch (Exception ex) {
				return false;
			}
		}

		void SetOptionColor (ChartOption option)
		{
			option.color = new string[] {
				"#418CF0",
				"#FCB441",
				"#E0400A",
				"#056492",
				"#BFBFBF",
				"#1A3B69",
				"#FFE382",
				"#129CDD",
				"#CA6B4B",
				"#005CDB",
				"#F3D288",
				"#506381",
				"#F1B9A8",
				"#E0830A",
				"#7893BE"
			};
		}

		string GetBarChartOption (BarChart barChart, Dictionary<string,List<string>> datas, Dictionary<string,Tuple<string,int,double,double>> Ys, Dictionary<string,string> lendDict, Dictionary<string,string> seriesColorDict, Dictionary<string,Dictionary<string,string>> fieldNameDict)
		{
			if (datas == null || datas.Count == 0)
				return "{}";
			var option = new ChartOption ();
			var title = new Title ();
			if (barChart.Apperence.TitleList.Count >= 1) {
				var titleApperence = barChart.Apperence.TitleList.First ();
				var txtStyle = GetTextStyle (title, titleApperence);
				TextStyle subtxtStyle = null;
				var subTxt = string.Empty;
				title.Text (titleApperence.Text).textStyle = txtStyle;

				if (barChart.Apperence.TitleList.Count >= 2) {
					subTxt = barChart.Apperence.TitleList [1].Text;
					subtxtStyle = GetTextStyle (title, barChart.Apperence.TitleList [1]);
					title.Subtext (subTxt).subtextStyle = subtxtStyle;
				}

				option.title = title;
			}

			//option.color = seriesColorDict.Values.ToList ();
			SetOptionColor (option);
			option.tooltip = new ECharts.Entities.ToolTip () { 
				trigger = TriggerType.item
			};
			SetDataZoom (option);
			option.legend = new Legend () { 
				data = lendDict.Values.ToList ()	 
			};

			var lend = barChart.Apperence.LegendList.First ();
			var x2 = 10;
			if (lend != null) {
				switch (lend.Docking) {
				case System.Windows.Forms.DataVisualization.Charting.Docking.Bottom:
					option.legend
					//.BorderWidth (lend.BorderWidth)
					//.BorderColor (ColorTranslator.ToHtml (Color.FromArgb(lend.BorderColor.R,lend.BorderColor.G,lend.BorderColor.B)))
						.Orient (OrientType.horizontal).Y (VerticalType.bottom)
							.TextStyle ()
					//.Baseline(VerticalType.bottom)
					//.Align(HorizontalType.center)
							.Color (ColorTranslator.ToHtml (Color.FromArgb (lend.ForeColor.R, lend.ForeColor.G, lend.ForeColor.B)));
					switch (lend.Alignment) {
					case StringAlignment.Near:
						option.legend.X (HorizontalType.left);
						break;
					case StringAlignment.Center:
						option.legend.X (HorizontalType.center);
						break;
					case StringAlignment.Far:
						option.legend.X (HorizontalType.right);
						break;
					}
					break;
				case System.Windows.Forms.DataVisualization.Charting.Docking.Left:
					option.legend
					//.BorderWidth (lend.BorderWidth)
					//.BorderColor (ColorTranslator.ToHtml (Color.FromArgb(lend.BorderColor.R,lend.BorderColor.G,lend.BorderColor.B)))
						.Orient (OrientType.vertical).X (HorizontalType.left)
								.TextStyle ()
					//.Baseline(VerticalType.middle)
					//.Align(HorizontalType.left)
								.Color (ColorTranslator.ToHtml (Color.FromArgb (lend.ForeColor.R, lend.ForeColor.G, lend.ForeColor.B)));
					switch (lend.Alignment) {
					case StringAlignment.Near:
						option.legend.Y (VerticalType.top);
						break;
					case StringAlignment.Center:
						option.legend.Y (VerticalType.middle);
						break;
					case StringAlignment.Far:
						option.legend.Y (VerticalType.bottom);
						break;
					}
					break;

				case System.Windows.Forms.DataVisualization.Charting.Docking.Right:
					option.legend
					//.BorderWidth (lend.BorderWidth)
					//.BorderColor (ColorTranslator.ToHtml (Color.FromArgb(lend.BorderColor.R,lend.BorderColor.G,lend.BorderColor.B)))
						.Orient (OrientType.vertical).X (HorizontalType.right)
						.TextStyle ()
					//.Baseline (VerticalType.middle)
					//.Align (HorizontalType.right)
						.Color (ColorTranslator.ToHtml (Color.FromArgb (lend.ForeColor.R, lend.ForeColor.G, lend.ForeColor.B)));
					switch (lend.Alignment) {
					case StringAlignment.Near:
						option.legend.Y (VerticalType.top);
						break;
					case StringAlignment.Center:
						option.legend.Y (VerticalType.middle);
						break;
					case StringAlignment.Far:
						option.legend.Y (VerticalType.bottom);
						break;
					}
					x2 = 80;
					option.grid = new ECharts.Entities.Grid () {
						x2 = x2
					};
					break;
				case System.Windows.Forms.DataVisualization.Charting.Docking.Top:
					option.legend
					//.BorderWidth (lend.BorderWidth)
					//.BorderColor (ColorTranslator.ToHtml (Color.FromArgb(lend.BorderColor.R,lend.BorderColor.G,lend.BorderColor.B)))
						.Orient (OrientType.horizontal).Y (VerticalType.top)
						.TextStyle ()
					//.Baseline(VerticalType.top)
					//.Align(HorizontalType.center)
						.Color (ColorTranslator.ToHtml (Color.FromArgb (lend.ForeColor.R, lend.ForeColor.G, lend.ForeColor.B)));
					switch (lend.Alignment) {
					case StringAlignment.Near:
						option.legend.Y (HorizontalType.left);
						break;
					case StringAlignment.Center:
						option.legend.Y (HorizontalType.center);
						break;
					case StringAlignment.Far:
						option.legend.Y (HorizontalType.right);
						break;
					}
					break;
				}


			}
				
			var axisLabelFormatter = new JRaw (@"function(val,index){ if(index%2==0) val=""\r\n""+val; return val;}");
			List<string> xdata = null;
			var chartArea = barChart.Apperence.ChartAreaList.FirstOrDefault (c => c.Name == "ChartArea1");
			datas.TryGetValue ("x", out xdata);
			//××××××× 说明：
			//自定义 值 处理
			// legend  由于自定义值是针对每一个点的，但是柱图的legend是相对于序列而言的，比如有一个序列，就有一个图例，
			// 有两个就有两个图例，但是数据点却有更多，图例没法显示，所以微软对于自定义legend没有处理，比如：自定义#XVAL-#VAL 
			// 那么就显示 #XVAL-#VAL
			// 因此 这里也不用处理，但是需要在 给legend的Data属性处理成如果有自定义，则替换成序列名称
			var firstSeries = barChart.Apperence.SeriesList.First ();
//			var isCustomLegend = CustomKeys.Any (firstSeries.LegendText.Contains);
//			if (isCustomLegend) {
//				var legendText = ReplaceKeywords (firstSeries.LegendText, xdata, datas);
//				if (!string.IsNullOrEmpty (legendText)) {
//					option.Legend ().Formatter (new JRaw (@""));
//				}
//			}

			// 数据标签
			object dataLabelFormatter = null;
			var isCustomLabel = CustomKeys.Any (firstSeries.Label.Contains);
			if (isCustomLabel) {
				var labelText = ReplaceKeywords (firstSeries.Label, xdata, datas);
				if (!string.IsNullOrEmpty (labelText)) {
					dataLabelFormatter = new JRaw (string.Format (@"function (parm) {{var aa={0}; return aa[parm.seriesName][parm.dataIndex];}}", labelText));
				}
			}
			var xAxis = new List<Axis> () {
				new CategoryAxis () {
					type = AxisType.category,
					//splitArea = new SplitArea (){ show = true },
					splitLine = new SplitLine () { show = chartArea.AxisX.MajorGrid.Enabled,
						lineStyle = new LineStyle () {
							color = ColorTranslator.ToHtml (Color.FromArgb (chartArea.AxisX.MajorGrid.LineColor.R, chartArea.AxisX.MajorGrid.LineColor.G, chartArea.AxisX.MajorGrid.LineColor.B)),
							width = chartArea.AxisX.MajorGrid.LineWidth,
							type = getLineStyleType (chartArea.AxisX.MajorGrid.LineDashStyle)
						}
					},
					data = xdata,
					axisLabel = new AxisLabel () {
						//interval = 0, // 当为0时 显示x每一个 值，
						rotate = chartArea.AxisX.IsLabelAutoFit ? 0 : 360 - chartArea.AxisX.LabelStyle.Angle,
						formatter = axisLabelFormatter
					},
					axisTick = new AxisTick () {
						alignWithLabel = true
					}
				}
			};

			var yAxis = new List<Axis> ();
			var series = new List<Bar> ();
			if (Ys.Count > 1) {
				if (Ys.Count == 2) {
					option.grid = new ECharts.Entities.Grid () {
						x = "160",
						x2 = x2
					};
				} else if (Ys.Count == 3) {
					option.grid = new ECharts.Entities.Grid () {
						x = "240",
						x2 = x2
					};
				} else {
					option.grid = new ECharts.Entities.Grid () {
						x = "320",
						x2 = x2
					};
				}
			}
			var yFirst = Ys.Keys.FirstOrDefault ();
			var yMaxMins = new Dictionary<string,Tuple<double,double>> ();
			foreach (var y in Ys) {
				var off = 80;
				if (y.Value.Item2 == 0) {
					off = 0;
				} else if (y.Value.Item2 == 1) {
					off = 80;
				} else if (y.Value.Item2 == 2) {
					off = 160;
				} else if (y.Value.Item2 == 3) {
					off = 240;
				}
					
				var yaxis = new ValueAxis () {
					type = AxisType.value,
					name = y.Value.Item1.Contains ("序列") ? "" : y.Value.Item1,
					//axisLine = new AxisLine (){ lineStyle = new LineStyle (){ color = seriesColorDict [y.Value.Item1] } },
//					splitArea = new SplitArea (){ show = true },
					splitLine = new SplitLine () { show = chartArea.AxisY.MajorGrid.Enabled && chartArea.AxisY.MajorGrid.LineColor != Color.Transparent && y.Key == yFirst,
						lineStyle = new LineStyle () {
							color = ColorTranslator.ToHtml (Color.FromArgb (chartArea.AxisY.MajorGrid.LineColor.R, chartArea.AxisY.MajorGrid.LineColor.G, chartArea.AxisY.MajorGrid.LineColor.B)),
							width = chartArea.AxisY.MajorGrid.LineWidth,
							type = getLineStyleType (chartArea.AxisY.MajorGrid.LineDashStyle)
						}
					},
					position = "left",
					offset = off
				};

				if (Ys.Count == 1 && y.Key == yFirst) {
					var type = getLineStyleType (chartArea.AxisY.LineDashStyle);
					yaxis.AxisLine ().LineStyle ().Color (chartArea.AxisY.LineColor).Width (chartArea.AxisY.LineWidth).Type (type);
				}

				var area = barChart.Apperence.ChartAreaList.Find ((ca) => ca.Name == y.Key);
				if (area != null) {
					var yTitle = (area.AxisY.Title + " " + area.AxisY.Tag).Trim ();
					if (!string.IsNullOrEmpty (yTitle))
						yaxis.Name (yTitle);
					var min = area.AxisY.Minimum;
					var max = area.AxisY.Maximum;
					max = double.IsNaN (max) ? double.MaxValue : max;
					min = double.IsNaN (min) ? double.MaxValue : min;
					yMaxMins [y.Key] = new Tuple<double,double> (max, min);
				}
				yAxis.Add (yaxis);
			}

			// 微软的序列显示顺序是 倒过来的 
			var sList = barChart.Apperence.SeriesList.ToList ().Reverse<PMSSeries> ();
			foreach (var se in sList) {
				var ss = fieldNameDict [se.Name] ["YAxisAreaName"];
				var b = new Bar () {
					type = ChartType.bar,
					name = se.Name,
					data = datas [se.Name],
					yAxisIndex = Ys [ss].Item2
				};

				if (!(yMaxMins [ss].Item1 == double.MaxValue && yMaxMins [ss].Item2 == double.MaxValue)) {
					b.markLine = new MarkLine () {
						data = new List<object> () {
							new MarkData () {
								type = MarkType.max,
								name = "区间上限"
							},
							new MarkData () {
								type = MarkType.min,
								name = "区间下限"
							}
						}
					};
				}
				if (se.IsValueShownAsLabel || isCustomLabel) {
					var styleLabel = b.ItemStyle ().Normal ().Label ().Show (true).Position (StyleLabelTyle.top).Formatter (dataLabelFormatter);
					if (se.LabelForeColor != Color.Empty && se.LabelForeColor != Color.Transparent) {
						styleLabel.TextStyle ().Color (ColorTranslator.ToHtml (Color.FromArgb (se.LabelForeColor.R, se.LabelForeColor.G, se.LabelForeColor.B)));
					}
				}
				series.Add (b);
			}
			option.series = series;

			if (barChart.ChartType == BarChartType.Column) {
				option.xAxis = xAxis;
				option.yAxis = yAxis;
			} else if (barChart.ChartType == BarChartType.Bar) {
				if (Ys.Count > 1) {
					return "{}"; // 不支持多y轴
				}
				option.Grid ().x = 80;
				yAxis.ForEach (y => y.AxisLabel ().Formatter (axisLabelFormatter));
				xAxis.ForEach (y => y.AxisLabel ().Formatter (null));
				option.xAxis = yAxis;
				option.yAxis = xAxis;
			} else if (barChart.ChartType == BarChartType.StackedBar) {
				if (Ys.Count > 1) {
					return "{}"; // 不支持多y轴
				}

				option.Grid ().x = 80;
				yAxis.ForEach (y => y.AxisLabel ().Formatter (axisLabelFormatter));
				xAxis.ForEach (y => y.AxisLabel ().Formatter (null));
				option.xAxis = yAxis;
				option.yAxis = xAxis;
				foreach (var se in series) {
					var b = se;//as Bar;
					b.Stack ("总量").ItemStyle ().Normal ().Label ().Show (true).Position (StyleLabelTyle.insideRight);
				}
			} else if (barChart.ChartType == BarChartType.StackedBar100) {
				if (Ys.Count > 1 || datas.Values.Count == 0) {
					return "{}"; // 不支持多y轴
				}
				option.Grid ().x = 80;
				foreach (var y in yAxis) {
					(y as ValueAxis).Min (0).Max (100).axisLine = null;
				}
				yAxis.ForEach (y => y.AxisLabel ().Formatter (axisLabelFormatter));
				xAxis.ForEach (y => y.AxisLabel ().Formatter (null));
				option.xAxis = yAxis;
				option.yAxis = xAxis;
				var percentDict = GetPercent (datas);
				foreach (var se in series) {
					var b = se as Bar;
					b.data = percentDict [b.name];
					if (dataLabelFormatter == null) {
						dataLabelFormatter = "{c}%";
					}
					b.Stack ("总量").ItemStyle ().Normal ().Label ().Show (true).Position (StyleLabelTyle.insideRight);//.Formatter (dataLabelFormatter);//.TextStyle ().FontSize (8).Color ("#000000");
				}
			} else if (barChart.ChartType == BarChartType.StackedColumn) {
				option.xAxis = xAxis;
				option.yAxis = yAxis;
				foreach (var se in series) {
					var b = se;//as Bar;
					b.Stack ("总量").ItemStyle ().Normal ().Label ().Show (true).Position (StyleLabelTyle.insideTop);//.Formatter (dataLabelFormatter);
				}
			} else if (barChart.ChartType == BarChartType.StackedColumn100) {
				foreach (var y in yAxis) {
					(y as ValueAxis).Min (0).Max (100).axisLine = null;
				}
				option.xAxis = xAxis;
				option.yAxis = yAxis;
				var percentDict = GetPercent (datas);
				foreach (var se in series) {
					var b = se;//as Bar;
					b.data = percentDict [b.name];
					if (dataLabelFormatter == null) {
						dataLabelFormatter = "{c}%";
					}
					b.Stack ("总量").ItemStyle ().Normal ().Label ().Show (true).Position (StyleLabelTyle.insideRight);//.Formatter (dataLabelFormatter);//.TextStyle ().FontSize (8).Color ("#000000");
				}
			}
			//var iStyle = new ItemStyle ();
			//iStyle.Normal ().Color("#418CF0");


			var result = JsonTools.ObjectToJson2 (option);
			option = null;
			return result;
		}

		Dictionary<string,List<double>> GetPercent (Dictionary<string,List<string>> datas)
		{
			// 计算系列个百分比
			var ydatas = datas.Where ((s) => s.Key != "x").ToDictionary ((kv) => kv.Key, (kv) => kv.Value);
			var percentDict = new Dictionary<string,List<double>> ();
			List<double> list = null;
			var lastKey = ydatas.Keys.Last ();
			foreach (var kvDatas in ydatas) {
				for (int i = 0; i < kvDatas.Value.Count (); i++) {
					var sum = ydatas.Sum ((kv) => {
						return Convert.ToDouble (kv.Value [i]); 
					});
					if (sum == 0)
						continue;
					//list = null;
					if (percentDict.TryGetValue (kvDatas.Key, out list)) {
						if (kvDatas.Key == lastKey) {
							list.Add ((Math.Round (100 - (sum - Convert.ToDouble (kvDatas.Value [i])) / sum * 100, 2)));
						} else
							list.Add (Math.Round (Convert.ToDouble (kvDatas.Value [i]) / sum * 100, 2));
					} else {
						list = new List<double> ();
						if (kvDatas.Key == lastKey)
							list.Add ((Math.Round (100 - (sum - Convert.ToDouble (kvDatas.Value [i])) / sum * 100, 2)));
						else
							list.Add (Math.Round (Convert.ToDouble (kvDatas.Value [i]) / sum * 100, 2));
						percentDict.Add (kvDatas.Key, list);
					}
				}
			}
			return percentDict;
		}

		void AppendToHtmlStr (ReportPage page, HtmlTag tag)
		{
			StringBuilder sb = null;
			if (HtmlDict.TryGetValue (page.PageNumber, out sb)) {
				sb.AppendLine (tag.ToHtmlString ());
			} else {
				sb = new StringBuilder (256);
				sb.AppendLine (tag.ToHtmlString ());
				HtmlDict.Add (page.PageNumber, sb);
			}
		}

		void AppendToJavaScriptStr (ReportPage page, string script)
		{
			StringBuilder sb = null;

			//#if(!DEBUG)
			// js压缩
//			var js = new JavaScriptCompressor ();
//			js.Encoding = System.Text.Encoding.UTF8;
//			js.CompressionType = CompressionType.Standard;
//			script = js.Compress (script);
			//#endif
			//script = string.Format ("$(function () {{{0}}});", script);
			if (JavaScriptDict.TryGetValue (page.PageNumber, out sb)) {
				sb.Append (script);
			} else {
				sb = new StringBuilder (256);
				sb.Append (script);
				JavaScriptDict.Add (page.PageNumber, sb);
			}
		}

		#region 自定义值

		public string ReplacePieKeywords (string strOriginal, List<string> xDatas, Dictionary<string,List<string>> datas)
		{
			List<double> dataList = null;
			Dictionary<string,string> result = new Dictionary<string, string> ();
			foreach (var se in datas) {
				try {
					dataList = se.Value.ConvertAll ((d) => Convert.ToDouble (d));
				} catch {
					continue;
				}
				if (dataList == null || dataList.Count == 0) {
					continue;
				}
				var total = dataList.Sum ();
				var avg = Math.Round (total / dataList.Count, 2);
				var max = dataList.Max ();
				var min = dataList.Min ();
				var first = dataList.First ();
				var last = dataList.Last ();

				for (var i = 0; i < dataList.Count; i++) {
					if (!result.ContainsKey (xDatas [i].ToString ())) {
						var percdent = string.Format ("{0}%", Math.Round (dataList [i] / total * 100));
						var text = ReplaceKeywords (strOriginal, se.Key, total.ToString (), avg.ToString (), 
							           max.ToString (), min.ToString (), first.ToString (), last.ToString (), 
							           i.ToString (), percdent, xDatas [i].ToString (), dataList [i].ToString ());
						result.Add (xDatas [i].ToString (), text);
					}
				}


			}
			return JsonConvert.SerializeObject (result);
		}

		public string ReplaceKeywords (string strOriginal, List<string> xDatas, Dictionary<string,List<string>> datas)
		{

			List<string> list = null;
			List<double> dataList = null;
			Dictionary<string,List<string>> result = new Dictionary<string, List<string>> ();
			foreach (var se in datas) {
				try {
					dataList = se.Value.ConvertAll ((d) => Convert.ToDouble (d));
				} catch {
					continue;
				}
				if (dataList == null || dataList.Count == 0) {
					continue;
				}
				var total = dataList.Sum ();
				var avg = Math.Round (total / dataList.Count, 2);
				var max = dataList.Max ();
				var min = dataList.Min ();
				var first = dataList.First ();
				var last = dataList.Last ();
				if (result.TryGetValue (se.Key, out list)) {
					for (var i = 0; i < dataList.Count; i++) {
						var percdent = string.Format ("{0}%", Math.Round (dataList [i] / total * 100));
						var text = ReplaceKeywords (strOriginal, se.Key, total.ToString (), avg.ToString (), 
							           max.ToString (), min.ToString (), first.ToString (), last.ToString (), 
							           i.ToString (), percdent, xDatas [i].ToString (), dataList [i].ToString ());
						list.Add (text);
					}
				} else {
					list = new List<string> ();
					result.Add (se.Key, list);
					for (var i = 0; i < dataList.Count; i++) {
						var percdent = string.Format ("{0}%", Math.Round (dataList [i] / total * 100));
						var text = ReplaceKeywords (strOriginal, se.Key, total.ToString (), avg.ToString (), 
							           max.ToString (), min.ToString (), first.ToString (), last.ToString (), 
							           i.ToString (), percdent, xDatas [i].ToString (), dataList [i].ToString ());
						list.Add (text);
					}
				}

			}
			return JsonConvert.SerializeObject (result);
		}

		public string ReplaceKeywords (string strOriginal, string seriesName,
		                               string total, string avg, string max,
		                               string min, string first, string last, string index,
		                               string percent, string xval, string val)
		{
			if (string.IsNullOrEmpty (strOriginal)) {
				return strOriginal;
			}
			//serie.YValueType="";
			string text = strOriginal.Replace ("\\n", "\n")
			.Replace ("#LABEL", string.Empty)
			.Replace ("#LEGENDTEXT", string.Empty)
			.Replace ("#AXISLABEL", string.Empty)
			.Replace ("#SERIESNAME", seriesName)
			.Replace ("#SER", seriesName)
			.Replace ("#TOTAL", total)
			.Replace ("#AVG", avg)
			.Replace ("#MAX", max)
			.Replace ("#MIN", min)
			.Replace ("#FIRST", first)
			.Replace ("#LAST", last)
			.Replace ("#INDEX", index)
			.Replace ("#PERCENT", percent)
			.Replace ("#VALX", xval)
			.Replace ("#VAL", val);

			return text;
		}

		#endregion
	}
}

