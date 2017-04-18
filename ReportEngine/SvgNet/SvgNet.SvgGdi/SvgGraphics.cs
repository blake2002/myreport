using SvgNet.SvgElements;
using SvgNet.SvgGdi.MetafileTools;
using SvgNet.SvgTypes;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;

namespace SvgNet.SvgGdi
{
	public class SvgGraphics : IGraphics
	{
		private class MatrixStack
		{
			private ArrayList _mx;

			private Matrix _result = null;

			public Matrix Top
			{
				get
				{
					this._result = null;
					return (Matrix)this._mx[this._mx.Count - 1];
				}
				set
				{
					this._mx[this._mx.Count - 1] = value;
					this._result = null;
				}
			}

			public Matrix Result
			{
				get
				{
					Matrix result;
					if (this._result != null)
					{
						result = this._result;
					}
					else
					{
						this._result = new Matrix();
						foreach (Matrix matrix in this._mx)
						{
							if (!matrix.IsIdentity)
							{
								this._result.Multiply(matrix);
							}
						}
						result = this._result;
					}
					return result;
				}
			}

			public MatrixStack()
			{
				this._mx = new ArrayList();
				this.Push();
				this.Push();
			}

			public void Dup()
			{
				this._mx.Insert(this._mx.Count, this.Top.Clone());
				this._result = null;
			}

			public void Pop()
			{
				if (this._mx.Count > 1)
				{
					this._mx.RemoveAt(this._mx.Count - 1);
					this._result = null;
				}
			}

			public void Push()
			{
				this._mx.Add(new Matrix());
			}
		}

		private SvgSvgElement _root;

		private SvgGroupElement _topgroup;

		private SvgRectElement _bg;

		private SvgDefsElement _defs;

		private SvgStyledTransformedElement _cur;

		private SvgGraphics.MatrixStack _transforms;

		private static Graphics _g;

		private SmoothingMode _smoothingMode = SmoothingMode.Invalid;

		public CompositingMode CompositingMode
		{
			get
			{
				throw new SvgGdiNotImpl("get_CompositingMode");
			}
			set
			{
			}
		}

		public Point RenderingOrigin
		{
			get
			{
				throw new SvgGdiNotImpl("get_RenderingOrigin");
			}
			set
			{
			}
		}

		public CompositingQuality CompositingQuality
		{
			get
			{
				throw new SvgGdiNotImpl("get_CompositingQuality");
			}
			set
			{
			}
		}

		public TextRenderingHint TextRenderingHint
		{
			get
			{
				throw new SvgGdiNotImpl("get_TextRenderingHint");
			}
			set
			{
				switch (value)
				{
				case TextRenderingHint.AntiAliasGridFit:
					this._cur.Style.Set("text-rendering", "auto");
					break;
				case TextRenderingHint.AntiAlias:
					this._cur.Style.Set("text-rendering", "auto");
					break;
				case TextRenderingHint.ClearTypeGridFit:
					this._cur.Style.Set("text-rendering", "geometricPrecision");
					break;
				default:
					this._cur.Style.Set("text-rendering", "crispEdges");
					break;
				}
			}
		}

		public int TextContrast
		{
			get
			{
				throw new SvgGdiNotImpl("get_TextContrast");
			}
			set
			{
			}
		}

		public SmoothingMode SmoothingMode
		{
			get
			{
				return this._smoothingMode;
			}
			set
			{
				switch (value)
				{
				case SmoothingMode.Invalid:
					break;
				case SmoothingMode.Default:
					this._cur.Style.Set("shape-rendering", "crispEdges");
					break;
				case SmoothingMode.HighSpeed:
					this._cur.Style.Set("shape-rendering", "optimizeSpeed");
					break;
				case SmoothingMode.HighQuality:
					this._cur.Style.Set("shape-rendering", "geometricPrecision");
					break;
				case SmoothingMode.None:
					this._cur.Style.Set("shape-rendering", "crispEdges");
					break;
				case SmoothingMode.AntiAlias:
					this._cur.Style.Set("shape-rendering", "auto");
					break;
				default:
					this._cur.Style.Set("shape-rendering", "auto");
					break;
				}
				this._smoothingMode = value;
			}
		}

		public PixelOffsetMode PixelOffsetMode
		{
			get
			{
				throw new SvgGdiNotImpl("get_PixelOffsetMode");
			}
			set
			{
			}
		}

		public InterpolationMode InterpolationMode
		{
			get
			{
				throw new SvgGdiNotImpl("get_InterpolationMode");
			}
			set
			{
			}
		}

		public Matrix Transform
		{
			get
			{
				return this._transforms.Result.Clone();
			}
			set
			{
				this._transforms.Top = value;
			}
		}

		public GraphicsUnit PageUnit
		{
			get
			{
				throw new SvgGdiNotImpl("PageUnit");
			}
			set
			{
			}
		}

		public float PageScale
		{
			get
			{
				throw new SvgGdiNotImpl("PageScale");
			}
			set
			{
			}
		}

		public float DpiX
		{
			get
			{
				return 96f;
			}
		}

		public float DpiY
		{
			get
			{
				return 96f;
			}
		}

		public Region Clip
		{
			get
			{
				throw new SvgGdiNotImpl("Clip");
			}
			set
			{
				throw new SvgGdiNotImpl("Clip");
			}
		}

		public RectangleF ClipBounds
		{
			get
			{
				throw new SvgGdiNotImpl("ClipBounds");
			}
		}

		public bool IsClipEmpty
		{
			get
			{
				throw new SvgGdiNotImpl("IsClipEmpty");
			}
		}

		public RectangleF VisibleClipBounds
		{
			get
			{
				throw new SvgGdiNotImpl("VisibleClipBounds");
			}
		}

		public bool IsVisibleClipEmpty
		{
			get
			{
				throw new SvgGdiNotImpl("IsVisibleClipEmpty");
			}
		}

		public SvgGraphics()
		{
			this._root = new SvgSvgElement();
			this._root.Id = "SvgGdi_output";
			this._bg = new SvgRectElement(0f, 0f, "100%", "100%");
			this._bg.Style.Set("fill", new SvgColor(Color.FromName("Control")));
			this._bg.Id = "background";
			this._root.AddChild(this._bg);
			this._topgroup = new SvgGroupElement("root_group");
			this._topgroup.Style.Set("shape-rendering", "crispEdges");
			this._cur = this._topgroup;
			this._root.AddChild(this._topgroup);
			this._defs = new SvgDefsElement("clips_hatches_and_gradients");
			this._root.AddChild(this._defs);
			this._transforms = new SvgGraphics.MatrixStack();
		}

		public string WriteSVGString(bool flag=false)
		{
			return this._root.WriteSVGString(flag);
		}

		public void Flush()
		{
		}

		public void Flush(FlushIntention intention)
		{
		}

		public void ResetTransform()
		{
			this._transforms.Pop();
			this._transforms.Dup();
		}

		public void MultiplyTransform(Matrix matrix)
		{
			this._transforms.Top.Multiply(matrix);
		}

		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			this._transforms.Top.Multiply(matrix, order);
		}

		public void TranslateTransform(float dx, float dy)
		{
			this._transforms.Top.Translate(dx, dy);
		}

		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			this._transforms.Top.Translate(dx, dy, order);
		}

		public void ScaleTransform(float sx, float sy)
		{
			this._transforms.Top.Scale(sx, sy);
		}

		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			this._transforms.Top.Scale(sx, sy, order);
		}

		public void RotateTransform(float angle)
		{
			this._transforms.Top.Rotate(angle);
		}

		public void RotateTransform(float angle, MatrixOrder order)
		{
			this._transforms.Top.Rotate(angle, order);
		}

		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
		{
			throw new SvgGdiNotImpl("TransformPoints (CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)");
		}

		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)
		{
			throw new SvgGdiNotImpl("TransformPoints (CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)");
		}

		public Color GetNearestColor(Color color)
		{
			throw new SvgGdiNotImpl("GetNearestColor (Color color)");
		}

		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
		{
			if (this.IsEndAnchorSimple(pen.StartCap) && this.IsEndAnchorSimple(pen.EndCap))
			{
				SvgLineElement svgLineElement = new SvgLineElement(x1, y1, x2, y2);
				svgLineElement.Style = new SvgStyle(pen);
				if (!this._transforms.Result.IsIdentity)
				{
					svgLineElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
				}
				this._cur.AddChild(svgLineElement);
				this.DrawEndAnchors(pen, new PointF(x1, y1), new PointF(x2, y2), false);
			}
			else
			{
				this.DrawLines(pen, new PointF[]
				{
					new PointF(x1, y1),
					new PointF(x2, y2)
				});
			}
		}

		public void DrawLine(Pen pen, PointF pt1, PointF pt2)
		{
			this.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
		}

		public void DrawLines(Pen pen, PointF[] points)
		{
			if (points.Length > 1)
			{
				if (this.IsEndAnchorSimple(pen.StartCap) && this.IsEndAnchorSimple(pen.EndCap))
				{
					SvgPolylineElement svgPolylineElement = new SvgPolylineElement(points);
					svgPolylineElement.Style = new SvgStyle(pen);
					if (!this._transforms.Result.IsIdentity)
					{
						svgPolylineElement.Transform = new SvgTransformList(this._transforms.Top.Clone());
					}
					this._cur.AddChild(svgPolylineElement);
					this.DrawEndAnchors(pen, points[0], points[points.Length - 1], false);
				}
				else
				{
					float num = points[0].X;
					float num2 = points[0].X;
					float num3 = points[0].Y;
					float num4 = points[0].Y;
					for (int i = 1; i < points.Length; i++)
					{
						PointF pointF = points[i];
						num = Math.Min(num, pointF.X);
						num2 = Math.Max(num2, pointF.X);
						num3 = Math.Min(num3, pointF.Y);
						num4 = Math.Max(num4, pointF.Y);
					}
					RectangleF frameRect = new RectangleF(num, num3, num2 - num + 1f, num4 - num3 + 1f);
					PointF location = frameRect.Location;
					frameRect.Offset(-location.X, -location.Y);
					for (int i = 0; i < points.Length; i++)
					{
						int expr_1D2_cp_1 = i;
						points[expr_1D2_cp_1].X = points[expr_1D2_cp_1].X - location.X;
						int expr_1EE_cp_1 = i;
						points[expr_1EE_cp_1].Y = points[expr_1EE_cp_1].Y - location.Y;
					}
					using (MemoryStream memoryStream = new MemoryStream())
					{
						Metafile metafile = null;
						try
						{
							using (Bitmap bitmap = new Bitmap(1, 1))
							{
								using (Graphics graphics = Graphics.FromImage(bitmap))
								{
									IntPtr hdc = graphics.GetHdc();
									metafile = new Metafile(memoryStream, hdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfOnly);
									graphics.ReleaseHdc();
								}
							}
							using (Graphics graphics2 = Graphics.FromImage(metafile))
							{
								graphics2.DrawLines(pen, points);
							}
						}
						finally
						{
							if (metafile != null)
							{
								metafile.Dispose();
							}
						}
						memoryStream.Position = 0L;
						bool metafileIsEmpty = true;
						MetafileParser metafileParser = new MetafileParser();
						metafileParser.EnumerateMetafile(memoryStream, pen.Width, location, delegate(PointF[] linePoints)
						{
							metafileIsEmpty = false;
							SvgPolylineElement svgPolylineElement2 = new SvgPolylineElement(linePoints);
							svgPolylineElement2.Style = new SvgStyle(pen);
							svgPolylineElement2.Style.Set("stroke-linecap", "round");
							if (!this._transforms.Result.IsIdentity)
							{
								svgPolylineElement2.Transform = new SvgTransformList(this._transforms.Top.Clone());
							}
							this._cur.AddChild(svgPolylineElement2);
						}, delegate(PointF[] linePoints, Brush fillBrush)
						{
						});
						if (metafileIsEmpty)
						{
							for (int i = 0; i < points.Length; i++)
							{
								int expr_361_cp_1 = i;
								points[expr_361_cp_1].X = points[expr_361_cp_1].X + location.X;
								int expr_37D_cp_1 = i;
								points[expr_37D_cp_1].Y = points[expr_37D_cp_1].Y + location.Y;
							}
							SvgPolylineElement svgPolylineElement = new SvgPolylineElement(points);
							svgPolylineElement.Style = new SvgStyle(pen);
							if (!this._transforms.Result.IsIdentity)
							{
								svgPolylineElement.Transform = new SvgTransformList(this._transforms.Top.Clone());
							}
							this._cur.AddChild(svgPolylineElement);
							this.DrawEndAnchors(pen, points[0], points[points.Length - 1], true);
						}
					}
				}
			}
		}

		public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
		{
			this.DrawLine(pen, (float)x1, (float)y1, (float)x2, (float)y2);
		}

		public void DrawLine(Pen pen, Point pt1, Point pt2)
		{
			this.DrawLine(pen, (float)pt1.X, (float)pt1.Y, (float)pt2.X, (float)pt2.Y);
		}

		public void DrawLines(Pen pen, Point[] points)
		{
			PointF[] points2 = SvgGraphics.Point2PointF(points);
			this.DrawLines(pen, points2);
		}

		public void DrawArc(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			string s = SvgGraphics.GDIArc2SVGPath(x, y, width, height, startAngle, sweepAngle, false);
			SvgPathElement svgPathElement = new SvgPathElement();
			svgPathElement.D = s;
			svgPathElement.Style = new SvgStyle(pen);
			if (!this._transforms.Result.IsIdentity)
			{
				svgPathElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgPathElement);
		}

		public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			this.DrawArc(pen, rect.X, rect.X, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		public void DrawArc(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			this.DrawArc(pen, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
		}

		public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			this.DrawArc(pen, (float)rect.X, (float)rect.X, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle);
		}

		public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			SvgPathElement svgPathElement = new SvgPathElement();
			svgPathElement.D = string.Concat(new string[]
			{
				"M ",
				x1.ToString("F", CultureInfo.InvariantCulture),
				" ",
				y1.ToString("F", CultureInfo.InvariantCulture),
				" C ",
				x2.ToString("F", CultureInfo.InvariantCulture),
				" ",
				y2.ToString("F", CultureInfo.InvariantCulture),
				" ",
				x3.ToString("F", CultureInfo.InvariantCulture),
				" ",
				y3.ToString("F", CultureInfo.InvariantCulture),
				" ",
				x4.ToString("F", CultureInfo.InvariantCulture),
				" ",
				y4.ToString("F", CultureInfo.InvariantCulture)
			});
			svgPathElement.Style = new SvgStyle(pen);
			if (!this._transforms.Result.IsIdentity)
			{
				svgPathElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgPathElement);
		}

		public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
		{
			this.DrawBezier(pen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y);
		}

		public void DrawBeziers(Pen pen, PointF[] points)
		{
			SvgPathElement svgPathElement = new SvgPathElement();
			string text = string.Concat(new string[]
			{
				"M ",
				points[0].X.ToString("F", CultureInfo.InvariantCulture),
				" ",
				points[0].Y.ToString("F", CultureInfo.InvariantCulture),
				" C "
			});
			for (int i = 1; i < points.Length; i++)
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					points[i].X.ToString("F", CultureInfo.InvariantCulture),
					" ",
					points[i].Y.ToString("F", CultureInfo.InvariantCulture),
					" "
				});
			}
			svgPathElement.D = text;
			svgPathElement.Style = new SvgStyle(pen);
			if (!this._transforms.Result.IsIdentity)
			{
				svgPathElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgPathElement);
		}

		public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
		{
			this.DrawBezier(pen, (float)pt1.X, (float)pt1.Y, (float)pt2.X, (float)pt2.Y, (float)pt3.X, (float)pt3.Y, (float)pt4.X, (float)pt4.Y);
		}

		public void DrawBeziers(Pen pen, Point[] points)
		{
			PointF[] points2 = SvgGraphics.Point2PointF(points);
			this.DrawBeziers(pen, points2);
		}

		public void DrawRectangle(Pen pen, Rectangle rect)
		{
			this.DrawRectangle(pen, (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
		}

		public void DrawRectangle(Pen pen, float x, float y, float width, float height)
		{
			SvgRectElement svgRectElement = new SvgRectElement(x, y, width, height);
			svgRectElement.Style = new SvgStyle(pen);
			if (!this._transforms.Result.IsIdentity)
			{
				svgRectElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgRectElement);
		}

		public void DrawRectangles(Pen pen, RectangleF[] rects)
		{
			for (int i = 0; i < rects.Length; i++)
			{
				RectangleF rectangleF = rects[i];
				this.DrawRectangle(pen, rectangleF.Left, rectangleF.Top, rectangleF.Width, rectangleF.Height);
			}
		}

		public void DrawRectangle(Pen pen, int x, int y, int width, int height)
		{
			this.DrawRectangle(pen, (float)x, (float)y, (float)width, (float)height);
		}

		public void DrawRectangles(Pen pen, Rectangle[] rects)
		{
			for (int i = 0; i < rects.Length; i++)
			{
				Rectangle rectangle = rects[i];
				this.DrawRectangle(pen, (float)rectangle.Left, (float)rectangle.Top, (float)rectangle.Width, (float)rectangle.Height);
			}
		}

		public void DrawEllipse(Pen pen, RectangleF rect)
		{
			this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void DrawEllipse(Pen pen, float x, float y, float width, float height)
		{
			SvgEllipseElement svgEllipseElement = new SvgEllipseElement(x + width / 2f, y + height / 2f, width / 2f, height / 2f);
			svgEllipseElement.Style = new SvgStyle(pen);
			if (!this._transforms.Result.IsIdentity)
			{
				svgEllipseElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgEllipseElement);
		}

		public void DrawEllipse(Pen pen, Rectangle rect)
		{
			this.DrawEllipse(pen, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
		}

		public void DrawEllipse(Pen pen, int x, int y, int width, int height)
		{
			this.DrawEllipse(pen, (float)x, (float)y, (float)width, (float)height);
		}

		public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			string s = SvgGraphics.GDIArc2SVGPath(x, y, width, height, startAngle, sweepAngle, true);
			SvgPathElement svgPathElement = new SvgPathElement();
			svgPathElement.D = s;
			svgPathElement.Style = new SvgStyle(pen);
			if (!this._transforms.Result.IsIdentity)
			{
				svgPathElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgPathElement);
		}

		public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			this.DrawPie(pen, rect.X, rect.X, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		public void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			this.DrawPie(pen, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
		}

		public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			this.DrawPie(pen, (float)rect.X, (float)rect.X, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle);
		}

		public void DrawPolygon(Pen pen, PointF[] points)
		{
			SvgPolygonElement svgPolygonElement = new SvgPolygonElement(points);
			svgPolygonElement.Style = new SvgStyle(pen);
			if (!this._transforms.Result.IsIdentity)
			{
				svgPolygonElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgPolygonElement);
		}

		public void DrawPolygon(Pen pen, Point[] points)
		{
			PointF[] points2 = SvgGraphics.Point2PointF(points);
			this.DrawPolygon(pen, points2);
		}

		public void DrawPath(Pen pen, GraphicsPath path)
		{
			throw new SvgGdiNotImpl("DrawPath (Pen pen, GraphicsPath path)");
		}

		public void DrawCurve(Pen pen, PointF[] points)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(points, 0, points.Length - 1, false, 0.5f);
			this.DrawBeziers(pen, points2);
		}

		public void DrawCurve(Pen pen, PointF[] points, float tension)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(points, 0, points.Length - 1, false, tension);
			this.DrawBeziers(pen, points2);
		}

		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(points, offset, numberOfSegments, false, 0.5f);
			this.DrawBeziers(pen, points2);
		}

		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(points, offset, numberOfSegments, false, tension);
			this.DrawBeziers(pen, points2);
		}

		public void DrawCurve(Pen pen, Point[] points)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(SvgGraphics.Point2PointF(points), 0, points.Length - 1, false, 0.5f);
			this.DrawBeziers(pen, points2);
		}

		public void DrawCurve(Pen pen, Point[] points, float tension)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(SvgGraphics.Point2PointF(points), 0, points.Length - 1, false, tension);
			this.DrawBeziers(pen, points2);
		}

		public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(SvgGraphics.Point2PointF(points), offset, numberOfSegments, false, tension);
			this.DrawBeziers(pen, points2);
		}

		public void DrawClosedCurve(Pen pen, PointF[] points)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(points, 0, points.Length - 1, true, 0.5f);
			this.DrawBeziers(pen, points2);
		}

		public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(points, 0, points.Length - 1, true, tension);
			this.DrawBeziers(pen, points2);
		}

		public void DrawClosedCurve(Pen pen, Point[] points)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(SvgGraphics.Point2PointF(points), 0, points.Length - 1, true, 0.5f);
			this.DrawBeziers(pen, points2);
		}

		public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(SvgGraphics.Point2PointF(points), 0, points.Length - 1, true, tension);
			this.DrawBeziers(pen, points2);
		}

		public void Clear(Color color)
		{
			this._cur.Children.Clear();
			this._bg.Style.Set("fill", new SvgColor(color));
		}

		public void FillRectangle(Brush brush, RectangleF rect)
		{
			this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void FillRectangle(Brush brush, float x, float y, float width, float height)
		{
			SvgRectElement svgRectElement = new SvgRectElement(x, y, width, height);
			svgRectElement.Style = this.HandleBrush(brush);
			if (!this._transforms.Result.IsIdentity)
			{
				svgRectElement.Transform = this._transforms.Result.Clone();
			}
			this._cur.AddChild(svgRectElement);
		}

		public void FillRectangles(Brush brush, RectangleF[] rects)
		{
			for (int i = 0; i < rects.Length; i++)
			{
				RectangleF rect = rects[i];
				this.FillRectangle(brush, rect);
			}
		}

		public void FillRectangle(Brush brush, Rectangle rect)
		{
			this.FillRectangle(brush, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
		}

		public void FillRectangle(Brush brush, int x, int y, int width, int height)
		{
			this.FillRectangle(brush, (float)x, (float)y, (float)width, (float)height);
		}

		public void FillRectangles(Brush brush, Rectangle[] rects)
		{
			for (int i = 0; i < rects.Length; i++)
			{
				Rectangle rect = rects[i];
				this.FillRectangle(brush, rect);
			}
		}

		public void FillPolygon(Brush brush, PointF[] points)
		{
			this.FillPolygon(brush, points, FillMode.Alternate);
		}

		public void FillPolygon(Brush brush, Point[] points)
		{
			PointF[] points2 = SvgGraphics.Point2PointF(points);
			this.FillPolygon(brush, points2, FillMode.Alternate);
		}

		public void FillPolygon(Brush brush, PointF[] points, FillMode fillmode)
		{
			SvgPolygonElement svgPolygonElement = new SvgPolygonElement(points);
			svgPolygonElement.Style = this.HandleBrush(brush);
			if (fillmode == FillMode.Alternate)
			{
				svgPolygonElement.Style.Set("fill-rule", "evenodd");
			}
			else
			{
				svgPolygonElement.Style.Set("fill-rule", "nonzero");
			}
			if (!this._transforms.Result.IsIdentity)
			{
				svgPolygonElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgPolygonElement);
		}

		public void FillPolygon(Brush brush, Point[] points, FillMode fillmode)
		{
			PointF[] points2 = SvgGraphics.Point2PointF(points);
			this.FillPolygon(brush, points2, fillmode);
		}

		public void FillEllipse(Brush brush, RectangleF rect)
		{
			this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void FillEllipse(Brush brush, float x, float y, float width, float height)
		{
			SvgEllipseElement svgEllipseElement = new SvgEllipseElement(x + width / 2f, y + height / 2f, width / 2f, height / 2f);
			svgEllipseElement.Style = this.HandleBrush(brush);
			if (!this._transforms.Result.IsIdentity)
			{
				svgEllipseElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgEllipseElement);
		}

		public void FillEllipse(Brush brush, Rectangle rect)
		{
			this.FillEllipse(brush, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
		}

		public void FillEllipse(Brush brush, int x, int y, int width, int height)
		{
			this.FillEllipse(brush, (float)x, (float)y, (float)width, (float)height);
		}

		public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle)
		{
			this.FillPie(brush, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle);
		}

		public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			string s = SvgGraphics.GDIArc2SVGPath(x, y, width, height, startAngle, sweepAngle, true);
			SvgPathElement svgPathElement = new SvgPathElement();
			svgPathElement.D = s;
			svgPathElement.Style = this.HandleBrush(brush);
			if (!this._transforms.Result.IsIdentity)
			{
				svgPathElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			}
			this._cur.AddChild(svgPathElement);
		}

		public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			this.FillPie(brush, (float)x, (float)y, (float)width, (float)height, (float)startAngle, (float)sweepAngle);
		}

		public void FillPath(Brush brush, GraphicsPath path)
		{
			throw new SvgGdiNotImpl("FillPath (Brush brush, GraphicsPath path)");
		}

		public void FillClosedCurve(Brush brush, PointF[] points)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(points, 0, points.Length - 1, true, 0.5f);
			this.FillBeziers(brush, points2, FillMode.Alternate);
		}

		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(points, 0, points.Length - 1, true, 0.5f);
			this.FillBeziers(brush, points2, fillmode);
		}

		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(points, 0, points.Length - 1, true, tension);
			this.FillBeziers(brush, points2, fillmode);
		}

		public void FillClosedCurve(Brush brush, Point[] points)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(SvgGraphics.Point2PointF(points), 0, points.Length - 1, true, 0.5f);
			this.FillBeziers(brush, points2, FillMode.Alternate);
		}

		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(SvgGraphics.Point2PointF(points), 0, points.Length - 1, true, 0.5f);
			this.FillBeziers(brush, points2, fillmode);
		}

		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension)
		{
			PointF[] points2 = SvgGraphics.Spline2Bez(SvgGraphics.Point2PointF(points), 0, points.Length - 1, true, tension);
			this.FillBeziers(brush, points2, fillmode);
		}

		public void FillRegion(Brush brush, Region region)
		{
			throw new SvgGdiNotImpl("FillRegion (Brush brush, Region region)");
		}

		public void DrawString(string s, Font font, Brush brush, float x, float y)
		{
			this.DrawText(s, font, brush, new RectangleF(x, y, 0f, 0f), StringFormat.GenericDefault, true);
		}

		public void DrawString(string s, Font font, Brush brush, PointF point)
		{
			this.DrawText(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), StringFormat.GenericDefault, true);
		}

		public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
		{
			this.DrawText(s, font, brush, new RectangleF(x, y, 0f, 0f), format, true);
		}

		public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
		{
			this.DrawText(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), format, true);
		}

		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
		{
			this.DrawText(s, font, brush, layoutRectangle, StringFormat.GenericDefault, false);
		}

		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
		{
			this.DrawText(s, font, brush, layoutRectangle, format, false);
		}

		private float GetFontDescentPercentage(Font font)
		{
			return (float)font.FontFamily.GetCellDescent(font.Style) / (float)font.FontFamily.GetEmHeight(font.Style);
		}

		private void DrawText(string s, Font font, Brush brush, RectangleF rect, StringFormat fmt, bool ignoreRect)
		{
			if (s != null && s.Contains("\n"))
			{
				throw new SvgGdiNotImpl("DrawText multiline text");
			}
			SvgTextElement svgTextElement = new SvgTextElement(s, rect.X, rect.Y);
			svgTextElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			svgTextElement.Style = this.HandleBrush(brush);
			svgTextElement.Style += new SvgStyle(font);
			switch (fmt.Alignment)
			{
			case StringAlignment.Near:
				break;
			case StringAlignment.Center:
				if (ignoreRect)
				{
					throw new SvgGdiNotImpl("DrawText automatic rect");
				}
				svgTextElement.Style.Set("text-anchor", "middle");
				svgTextElement.X = rect.X + rect.Width / 2f;
				break;
			case StringAlignment.Far:
				if (ignoreRect)
				{
					throw new SvgGdiNotImpl("DrawText automatic rect");
				}
				svgTextElement.Style.Set("text-anchor", "end");
				svgTextElement.X = rect.Right;
				break;
			default:
				throw new SvgGdiNotImpl("DrawText horizontal alignment");
			}
			if (!ignoreRect && (fmt.FormatFlags & StringFormatFlags.NoClip) != StringFormatFlags.NoClip)
			{
				SvgClipPathElement svgClipPathElement = new SvgClipPathElement();
				SvgClipPathElement expr_172 = svgClipPathElement;
				expr_172.Id += "_text_clipper";
				SvgRectElement ch = new SvgRectElement(rect.X, rect.Y, rect.Width, rect.Height);
				svgClipPathElement.AddChild(ch);
				this._defs.AddChild(svgClipPathElement);
				svgTextElement.Style.Set("clip-path", new SvgUriReference(svgClipPathElement));
			}
			switch (fmt.LineAlignment)
			{
			case StringAlignment.Near:
			{
				SvgTspanElement svgTspanElement = new SvgTspanElement(s);
				svgTspanElement.DY = new SvgLength(svgTextElement.Style.Get("font-size").ToString());
				svgTextElement.Text = null;
				svgTextElement.AddChild(svgTspanElement);
				break;
			}
			case StringAlignment.Center:
			{
				if (ignoreRect)
				{
					throw new SvgGdiNotImpl("DrawText automatic rect");
				}
				svgTextElement.Y.Value = svgTextElement.Y.Value + rect.Height / 2f;
				SvgTspanElement svgTspanElement = new SvgTspanElement(s);
				svgTspanElement.DY = new SvgLength(svgTextElement.Style.Get("font-size").ToString());
				svgTspanElement.DY.Value = svgTspanElement.DY.Value * (1f - this.GetFontDescentPercentage(font) - 0.5f);
				svgTextElement.Text = null;
				svgTextElement.AddChild(svgTspanElement);
				break;
			}
			case StringAlignment.Far:
			{
				if (ignoreRect)
				{
					throw new SvgGdiNotImpl("DrawText automatic rect");
				}
				svgTextElement.Y.Value = svgTextElement.Y.Value + rect.Height;
				SvgTspanElement svgTspanElement = new SvgTspanElement(s);
				svgTspanElement.DY = new SvgLength(svgTextElement.Style.Get("font-size").ToString());
				svgTspanElement.DY.Value = svgTspanElement.DY.Value * (1f - this.GetFontDescentPercentage(font) - 1f);
				svgTextElement.Text = null;
				svgTextElement.AddChild(svgTspanElement);
				break;
			}
			default:
				throw new SvgGdiNotImpl("DrawText vertical alignment");
			}
			this._cur.AddChild(svgTextElement);
		}

		public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled)
		{
			return SvgGraphics.GetDefaultGraphics().MeasureString(text, font, layoutArea, stringFormat, out charactersFitted, out linesFilled);
		}

		public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
		{
			return SvgGraphics.GetDefaultGraphics().MeasureString(text, font, origin, stringFormat);
		}

		public SizeF MeasureString(string text, Font font, SizeF layoutArea)
		{
			return SvgGraphics.GetDefaultGraphics().MeasureString(text, font, layoutArea);
		}

		public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
		{
			return SvgGraphics.GetDefaultGraphics().MeasureString(text, font, layoutArea, stringFormat);
		}

		public SizeF MeasureString(string text, Font font)
		{
			return SvgGraphics.GetDefaultGraphics().MeasureString(text, font);
		}

		public SizeF MeasureString(string text, Font font, int width)
		{
			return SvgGraphics.GetDefaultGraphics().MeasureString(text, font, width);
		}

		public SizeF MeasureString(string text, Font font, int width, StringFormat format)
		{
			return SvgGraphics.GetDefaultGraphics().MeasureString(text, font, width, format);
		}

		public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
		{
			return SvgGraphics.GetDefaultGraphics().MeasureCharacterRanges(text, font, layoutRect, stringFormat);
		}

		public void DrawIcon(Icon icon, int x, int y)
		{
			Bitmap b = icon.ToBitmap();
			this.DrawBitmapData(b, (float)x, (float)y, (float)icon.Width, (float)icon.Height, false);
		}

		public void DrawIcon(Icon icon, Rectangle targetRect)
		{
			Bitmap b = icon.ToBitmap();
			this.DrawBitmapData(b, (float)targetRect.X, (float)targetRect.Y, (float)targetRect.Width, (float)targetRect.Height, true);
		}

		public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
		{
			Bitmap b = icon.ToBitmap();
			this.DrawBitmapData(b, (float)targetRect.X, (float)targetRect.Y, (float)targetRect.Width, (float)targetRect.Height, false);
		}

		public void DrawImage(Image image, PointF point)
		{
			this.DrawImage(image, point.X, point.Y);
		}

		public void DrawImage(Image image, float x, float y)
		{
			this.DrawImage(image, x, y, (float)image.Width, (float)image.Height);
		}

		public void DrawImage(Image image, RectangleF rect)
		{
			this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void DrawImage(Image image, float x, float y, float width, float height)
		{
			if (!(image.GetType() != typeof(Bitmap)))
			{
				this.DrawBitmapData((Bitmap)image, x, y, width, height, true);
			}
		}

		public void DrawImage(Image image, Point point)
		{
			this.DrawImage(image, (float)point.X, (float)point.Y);
		}

		public void DrawImage(Image image, int x, int y)
		{
			this.DrawImage(image, (float)x, (float)y);
		}

		public void DrawImage(Image image, Rectangle rect)
		{
			this.DrawImage(image, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
		}

		public void DrawImage(Image image, int x, int y, int width, int height)
		{
			this.DrawImage(image, x, y, width, height);
		}

		public void DrawImageUnscaled(Image image, Point point)
		{
			this.DrawImage(image, (float)point.X, (float)point.Y);
		}

		public void DrawImageUnscaled(Image image, int x, int y)
		{
			this.DrawImage(image, (float)x, (float)y);
		}

		public void DrawImageUnscaled(Image image, Rectangle rect)
		{
			this.DrawImageUnscaled(image, rect.X, rect.Y, rect.Width, rect.Height);
		}

		public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
		{
			if (!(image.GetType() != typeof(Bitmap)))
			{
				this.DrawBitmapData((Bitmap)image, (float)x, (float)y, (float)width, (float)height, false);
			}
		}

		public void DrawImage(Image image, PointF[] destPoints)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, PointF[] destPoints)");
		}

		public void DrawImage(Image image, Point[] destPoints)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Point[] destPoints)");
		}

		public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Single x, Single y, RectangleF srcRect, GraphicsUnit srcUnit)");
		}

		public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Int32 x, Int32 y, Rectangle srcRect, GraphicsUnit srcUnit)");
		}

		public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)");
		}

		public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)");
		}

		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)");
		}

		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)");
		}

		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)");
		}

		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)");
		}

		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)");
		}

		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Rectangle destRect, Single srcX, Single srcY, Single srcWidth, Single srcHeight, GraphicsUnit srcUnit)");
		}

		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Rectangle destRect, Single srcX, Single srcY, Single srcWidth, Single srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)");
		}

		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Rectangle destRect, Int32 srcX, Int32 srcY, Int32 srcWidth, Int32 srcHeight, GraphicsUnit srcUnit)");
		}

		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			throw new SvgGdiNotImpl("DrawImage (Image image, Rectangle destRect, Int32 srcX, Int32 srcY, Int32 srcWidth, Int32 srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)");
		}

		public void SetClip(Graphics g)
		{
			throw new SvgGdiNotImpl("SetClip (Graphics g)");
		}

		public void SetClip(Graphics g, CombineMode combineMode)
		{
			throw new SvgGdiNotImpl("SetClip (Graphics g, CombineMode combineMode)");
		}

		public void SetClip(Rectangle rect)
		{
			this.SetClip(new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height));
		}

		public void SetClip(Rectangle rect, CombineMode combineMode)
		{
			throw new SvgGdiNotImpl("SetClip (Rectangle rect, CombineMode combineMode)");
		}

		public void SetClip(RectangleF rect)
		{
			SvgClipPathElement svgClipPathElement = new SvgClipPathElement();
			SvgClipPathElement expr_08 = svgClipPathElement;
			expr_08.Id += "_SetClip";
			SvgRectElement ch = new SvgRectElement(rect.X, rect.Y, rect.Width, rect.Height);
			svgClipPathElement.AddChild(ch);
			this._defs.AddChild(svgClipPathElement);
			this._cur.Style.Set("clip-path", new SvgUriReference(svgClipPathElement));
		}

		public void SetClip(RectangleF rect, CombineMode combineMode)
		{
			throw new SvgGdiNotImpl("SetClip (RectangleF rect, CombineMode combineMode)");
		}

		public void SetClip(GraphicsPath path)
		{
			throw new SvgGdiNotImpl("SetClip (GraphicsPath path)");
		}

		public void SetClip(GraphicsPath path, CombineMode combineMode)
		{
			throw new SvgGdiNotImpl("SetClip (GraphicsPath path, CombineMode combineMode)");
		}

		public void SetClip(Region region, CombineMode combineMode)
		{
			throw new SvgGdiNotImpl("SetClip (Region region, CombineMode combineMode)");
		}

		public void IntersectClip(Rectangle rect)
		{
			throw new SvgGdiNotImpl("IntersectClip (Rectangle rect)");
		}

		public void IntersectClip(RectangleF rect)
		{
			throw new SvgGdiNotImpl("IntersectClip (RectangleF rect)");
		}

		public void IntersectClip(Region region)
		{
			throw new SvgGdiNotImpl("IntersectClip (Region region)");
		}

		public void ExcludeClip(Rectangle rect)
		{
			throw new SvgGdiNotImpl("ExcludeClip (Rectangle rect)");
		}

		public void ExcludeClip(Region region)
		{
			throw new SvgGdiNotImpl("ExcludeClip (Region region)");
		}

		public void ResetClip()
		{
			this._cur.Style.Set("clip-path", null);
		}

		public void TranslateClip(float dx, float dy)
		{
			throw new SvgGdiNotImpl("TranslateClip (Single dx, Single dy)");
		}

		public void TranslateClip(int dx, int dy)
		{
			throw new SvgGdiNotImpl("TranslateClip (Int32 dx, Int32 dy)");
		}

		public bool IsVisible(int x, int y)
		{
			throw new SvgGdiNotImpl("IsVisible (Int32 x, Int32 y)");
		}

		public bool IsVisible(Point point)
		{
			throw new SvgGdiNotImpl("IsVisible (Point point)");
		}

		public bool IsVisible(float x, float y)
		{
			throw new SvgGdiNotImpl("IsVisible (Single x, Single y)");
		}

		public bool IsVisible(PointF point)
		{
			throw new SvgGdiNotImpl("IsVisible (PointF point)");
		}

		public bool IsVisible(int x, int y, int width, int height)
		{
			throw new SvgGdiNotImpl("IsVisible (Int32 x, Int32 y, Int32 width, Int32 height)");
		}

		public bool IsVisible(Rectangle rect)
		{
			throw new SvgGdiNotImpl("IsVisible (Rectangle rect)");
		}

		public bool IsVisible(float x, float y, float width, float height)
		{
			throw new SvgGdiNotImpl("IsVisible (Single x, Single y, Single width, Single height)");
		}

		public bool IsVisible(RectangleF rect)
		{
			throw new SvgGdiNotImpl("IsVisible (RectangleF rect)");
		}

		public GraphicsState Save()
		{
			throw new SvgGdiNotImpl("Save ()");
		}

		public void Restore(GraphicsState gstate)
		{
			throw new SvgGdiNotImpl("Restore (GraphicsState gstate)");
		}

		public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)
		{
			throw new SvgGdiNotImpl("BeginContainer (RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)");
		}

		public GraphicsContainer BeginContainer()
		{
			SvgGroupElement svgGroupElement = new SvgGroupElement();
			this._cur.AddChild(svgGroupElement);
			this._cur = svgGroupElement;
			SvgStyledTransformedElement expr_21 = this._cur;
			expr_21.Id += "_BeginContainer";
			this._transforms.Push();
			return null;
		}

		public void EndContainer(GraphicsContainer container)
		{
			if (this._cur != this._topgroup)
			{
				this._cur = (SvgStyledTransformedElement)this._cur.Parent;
				this._transforms.Pop();
			}
		}

		public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)
		{
			throw new SvgGdiNotImpl("BeginContainer (Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)");
		}

		public void AddMetafileComment(byte[] data)
		{
		}

		private static Graphics GetDefaultGraphics()
		{
			if (SvgGraphics._g == null)
			{
				Bitmap image = new Bitmap(1, 1);
				SvgGraphics._g = Graphics.FromImage(image);
			}
			return SvgGraphics._g;
		}

		private SvgStyle HandleBrush(Brush br)
		{
			SvgStyle result;
			if (br.GetType() == typeof(SolidBrush))
			{
				result = new SvgStyle((SolidBrush)br);
			}
			else if (br.GetType() == typeof(LinearGradientBrush))
			{
				LinearGradientBrush linearGradientBrush = (LinearGradientBrush)br;
				RectangleF rectangle = linearGradientBrush.Rectangle;
				SvgLinearGradient svgLinearGradient = new SvgLinearGradient(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
				WrapMode wrapMode = linearGradientBrush.WrapMode;
				if (wrapMode != WrapMode.Tile)
				{
					if (wrapMode != WrapMode.Clamp)
					{
						svgLinearGradient.SpreadMethod = "reflect";
						svgLinearGradient.GradientUnits = "userSpaceOnUse";
					}
					else
					{
						svgLinearGradient.SpreadMethod = "pad";
						svgLinearGradient.GradientUnits = "objectBoundingBox";
					}
				}
				else
				{
					svgLinearGradient.SpreadMethod = "repeat";
					svgLinearGradient.GradientUnits = "userSpaceOnUse";
				}
				ColorBlend colorBlend = null;
				try
				{
					colorBlend = linearGradientBrush.InterpolationColors;
				}
				catch (Exception)
				{
				}
				if (colorBlend != null)
				{
					for (int i = 0; i < linearGradientBrush.InterpolationColors.Colors.Length; i++)
					{
						svgLinearGradient.AddChild(new SvgStopElement(linearGradientBrush.InterpolationColors.Positions[i], linearGradientBrush.InterpolationColors.Colors[i]));
					}
				}
				else
				{
					svgLinearGradient.AddChild(new SvgStopElement("0%", linearGradientBrush.LinearColors[0]));
					svgLinearGradient.AddChild(new SvgStopElement("100%", linearGradientBrush.LinearColors[1]));
				}
				SvgLinearGradient expr_1DA = svgLinearGradient;
				expr_1DA.Id += "_LinearGradientBrush";
				this._defs.AddChild(svgLinearGradient);
				SvgStyle svgStyle = new SvgStyle();
				svgStyle.Set("fill", new SvgUriReference(svgLinearGradient));
				result = svgStyle;
			}
			else if (br.GetType() == typeof(HatchBrush))
			{
				HatchBrush hatchBrush = (HatchBrush)br;
				SvgPatternElement svgPatternElement = new SvgPatternElement(0f, 0f, 8f, 8f, new SvgNumList("4 4 12 12"));
				svgPatternElement.Style.Set("shape-rendering", "crispEdges");
				svgPatternElement.Style.Set("stroke-linecap", "butt");
				SvgRectElement svgRectElement = new SvgRectElement(0f, 0f, 8f, 8f);
				svgRectElement.Style.Set("fill", new SvgColor(hatchBrush.BackgroundColor));
				svgPatternElement.AddChild(svgRectElement);
				this.AddHatchBrushDetails(svgPatternElement, new SvgColor(hatchBrush.ForegroundColor), hatchBrush.HatchStyle);
				SvgPatternElement expr_326 = svgPatternElement;
				expr_326.Id += "_HatchBrush";
				svgPatternElement.PatternUnits = "userSpaceOnUse";
				svgPatternElement.PatternContentUnits = "userSpaceOnUse";
				this._defs.AddChild(svgPatternElement);
				SvgStyle svgStyle = new SvgStyle();
				svgStyle.Set("fill", new SvgUriReference(svgPatternElement));
				result = svgStyle;
			}
			else
			{
				result = new SvgStyle(new SolidBrush(Color.Salmon));
			}
			return result;
		}

		private void AddHatchBrushDetails(SvgPatternElement patty, SvgColor col, HatchStyle hs)
		{
			SvgStyledTransformedElement svgStyledTransformedElement = null;
			SvgStyledTransformedElement svgStyledTransformedElement2 = null;
			SvgStyledTransformedElement svgStyledTransformedElement3 = null;
			SvgStyledTransformedElement svgStyledTransformedElement4 = null;
			switch (hs)
			{
			case HatchStyle.Horizontal:
				svgStyledTransformedElement = new SvgLineElement(0f, 4f, 8f, 4f);
				break;
			case HatchStyle.Vertical:
				svgStyledTransformedElement4 = new SvgLineElement(0f, 0f, 0f, 8f);
				break;
			case HatchStyle.ForwardDiagonal:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 8f, 8f);
				break;
			case HatchStyle.BackwardDiagonal:
				svgStyledTransformedElement = new SvgLineElement(8f, 0f, 0f, 8f);
				break;
			case HatchStyle.Cross:
				svgStyledTransformedElement = new SvgLineElement(4f, 0f, 4f, 8f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 4f, 8f, 4f);
				break;
			case HatchStyle.DiagonalCross:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 8f, 8f);
				svgStyledTransformedElement2 = new SvgLineElement(8f, 0f, 0f, 8f);
				break;
			case HatchStyle.Percent05:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 1f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 4f, 5f, 4f);
				break;
			case HatchStyle.Percent10:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 1f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 2f, 5f, 2f);
				svgStyledTransformedElement3 = new SvgLineElement(2f, 4f, 3f, 4f);
				svgStyledTransformedElement4 = new SvgLineElement(6f, 6f, 7f, 6f);
				break;
			case HatchStyle.Percent20:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 2f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 2f, 6f, 2f);
				svgStyledTransformedElement3 = new SvgLineElement(2f, 4f, 4f, 4f);
				svgStyledTransformedElement4 = new SvgLineElement(5f, 6f, 7f, 6f);
				break;
			case HatchStyle.Percent25:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 3f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 2f, 6f, 2f);
				svgStyledTransformedElement3 = new SvgLineElement(2f, 4f, 5f, 4f);
				svgStyledTransformedElement4 = new SvgLineElement(5f, 6f, 7f, 6f);
				break;
			case HatchStyle.Percent30:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 3f, 1f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 2f, 6f, 2f);
				svgStyledTransformedElement3 = new SvgRectElement(2f, 4f, 3f, 1f);
				svgStyledTransformedElement4 = new SvgLineElement(5f, 6f, 7f, 6f);
				break;
			case HatchStyle.Percent40:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 3f, 1f);
				svgStyledTransformedElement2 = new SvgRectElement(4f, 2f, 3f, 1f);
				svgStyledTransformedElement3 = new SvgRectElement(2f, 4f, 3f, 1f);
				svgStyledTransformedElement4 = new SvgRectElement(5f, 6f, 3f, 1f);
				break;
			case HatchStyle.Percent50:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 3f, 3f);
				svgStyledTransformedElement2 = new SvgRectElement(4f, 4f, 4f, 4f);
				break;
			case HatchStyle.Percent60:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 4f, 3f);
				svgStyledTransformedElement2 = new SvgRectElement(4f, 4f, 4f, 4f);
				break;
			case HatchStyle.Percent70:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 4f, 5f);
				svgStyledTransformedElement2 = new SvgRectElement(4f, 4f, 4f, 4f);
				break;
			case HatchStyle.Percent75:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 7f, 3f);
				svgStyledTransformedElement2 = new SvgRectElement(0f, 2f, 3f, 7f);
				break;
			case HatchStyle.Percent80:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 7f, 4f);
				svgStyledTransformedElement2 = new SvgRectElement(0f, 2f, 4f, 7f);
				break;
			case HatchStyle.Percent90:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 7f, 5f);
				svgStyledTransformedElement2 = new SvgRectElement(0f, 2f, 5f, 7f);
				break;
			case HatchStyle.LightDownwardDiagonal:
			case HatchStyle.DarkDownwardDiagonal:
				svgStyledTransformedElement = new SvgLineElement(4f, 0f, 8f, 4f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 4f, 4f, 8f);
				svgStyledTransformedElement3 = new SvgLineElement(0f, 0f, 8f, 8f);
				break;
			case HatchStyle.LightUpwardDiagonal:
			case HatchStyle.DarkUpwardDiagonal:
				svgStyledTransformedElement = new SvgLineElement(0f, 4f, 4f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 8f, 8f, 4f);
				svgStyledTransformedElement3 = new SvgLineElement(0f, 8f, 8f, 0f);
				break;
			case HatchStyle.WideDownwardDiagonal:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 8f, 8f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 1f, 8f, 9f);
				svgStyledTransformedElement3 = new SvgLineElement(7f, 0f, 8f, 1f);
				break;
			case HatchStyle.WideUpwardDiagonal:
				svgStyledTransformedElement = new SvgLineElement(8f, 0f, 0f, 8f);
				svgStyledTransformedElement2 = new SvgLineElement(8f, 1f, 0f, 9f);
				svgStyledTransformedElement3 = new SvgLineElement(0f, 1f, -1f, 0f);
				break;
			case HatchStyle.LightVertical:
			case HatchStyle.DarkVertical:
				svgStyledTransformedElement = new SvgLineElement(2f, 0f, 2f, 8f);
				svgStyledTransformedElement2 = new SvgLineElement(6f, 0f, 6f, 8f);
				break;
			case HatchStyle.LightHorizontal:
			case HatchStyle.DarkHorizontal:
				svgStyledTransformedElement = new SvgLineElement(0f, 2f, 8f, 2f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 6f, 8f, 6f);
				break;
			case HatchStyle.NarrowVertical:
				svgStyledTransformedElement = new SvgLineElement(1f, 0f, 1f, 8f);
				svgStyledTransformedElement2 = new SvgLineElement(3f, 0f, 3f, 8f);
				svgStyledTransformedElement3 = new SvgLineElement(5f, 0f, 5f, 8f);
				svgStyledTransformedElement4 = new SvgLineElement(7f, 0f, 7f, 8f);
				break;
			case HatchStyle.NarrowHorizontal:
				svgStyledTransformedElement = new SvgLineElement(0f, 1f, 8f, 1f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 3f, 8f, 3f);
				svgStyledTransformedElement3 = new SvgLineElement(0f, 5f, 8f, 5f);
				svgStyledTransformedElement4 = new SvgLineElement(0f, 7f, 8f, 7f);
				break;
			case HatchStyle.DashedDownwardDiagonal:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 4f, 4f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 0f, 8f, 4f);
				break;
			case HatchStyle.DashedUpwardDiagonal:
				svgStyledTransformedElement = new SvgLineElement(4f, 0f, 0f, 4f);
				svgStyledTransformedElement2 = new SvgLineElement(8f, 0f, 4f, 4f);
				break;
			case HatchStyle.DashedHorizontal:
				svgStyledTransformedElement = new SvgLineElement(0f, 2f, 4f, 2f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 6f, 8f, 6f);
				break;
			case HatchStyle.DashedVertical:
				svgStyledTransformedElement = new SvgLineElement(2f, 0f, 2f, 4f);
				svgStyledTransformedElement2 = new SvgLineElement(6f, 4f, 6f, 8f);
				break;
			case HatchStyle.SmallConfetti:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 2f, 2f);
				svgStyledTransformedElement2 = new SvgLineElement(7f, 3f, 5f, 5f);
				svgStyledTransformedElement3 = new SvgLineElement(2f, 6f, 4f, 4f);
				break;
			case HatchStyle.LargeConfetti:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 1f, 1f);
				svgStyledTransformedElement2 = new SvgRectElement(2f, 3f, 1f, 1f);
				svgStyledTransformedElement3 = new SvgRectElement(5f, 2f, 1f, 1f);
				svgStyledTransformedElement4 = new SvgRectElement(6f, 6f, 1f, 1f);
				break;
			case HatchStyle.ZigZag:
				svgStyledTransformedElement = new SvgLineElement(0f, 4f, 4f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 0f, 8f, 4f);
				break;
			case HatchStyle.Wave:
				svgStyledTransformedElement3 = new SvgLineElement(0f, 4f, 3f, 2f);
				svgStyledTransformedElement4 = new SvgLineElement(3f, 2f, 8f, 4f);
				break;
			case HatchStyle.DiagonalBrick:
				svgStyledTransformedElement = new SvgLineElement(0f, 8f, 8f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 0f, 4f, 4f);
				break;
			case HatchStyle.HorizontalBrick:
				svgStyledTransformedElement = new SvgLineElement(0f, 3f, 8f, 3f);
				svgStyledTransformedElement2 = new SvgLineElement(3f, 0f, 3f, 3f);
				svgStyledTransformedElement3 = new SvgLineElement(0f, 3f, 0f, 7f);
				svgStyledTransformedElement4 = new SvgLineElement(0f, 7f, 7f, 7f);
				break;
			case HatchStyle.Weave:
				svgStyledTransformedElement = new SvgLineElement(0f, 4f, 4f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(8f, 4f, 4f, 8f);
				svgStyledTransformedElement3 = new SvgLineElement(0f, 0f, 0f, 4f);
				svgStyledTransformedElement4 = new SvgLineElement(0f, 4f, 4f, 8f);
				break;
			case HatchStyle.Plaid:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 8f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 3f, 8f, 3f);
				svgStyledTransformedElement3 = new SvgRectElement(0f, 4f, 3f, 3f);
				break;
			case HatchStyle.Divot:
				svgStyledTransformedElement = new SvgLineElement(2f, 2f, 4f, 4f);
				svgStyledTransformedElement2 = new SvgLineElement(4f, 4f, 2f, 6f);
				break;
			case HatchStyle.DottedGrid:
				svgStyledTransformedElement = new SvgLineElement(4f, 0f, 4f, 8f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 4f, 8f, 4f);
				break;
			case HatchStyle.DottedDiamond:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 8f, 8f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 8f, 8f, 0f);
				break;
			case HatchStyle.Shingle:
				svgStyledTransformedElement = new SvgLineElement(0f, 2f, 2f, 0f);
				svgStyledTransformedElement2 = new SvgLineElement(2f, 0f, 7f, 5f);
				svgStyledTransformedElement3 = new SvgLineElement(0f, 3f, 3f, 7f);
				break;
			case HatchStyle.Trellis:
				svgStyledTransformedElement = new SvgLineElement(0f, 1f, 8f, 1f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 3f, 8f, 3f);
				svgStyledTransformedElement3 = new SvgLineElement(0f, 5f, 8f, 5f);
				svgStyledTransformedElement4 = new SvgLineElement(0f, 7f, 8f, 7f);
				break;
			case HatchStyle.Sphere:
				svgStyledTransformedElement = new SvgEllipseElement(3f, 3f, 2f, 2f);
				break;
			case HatchStyle.SmallGrid:
				svgStyledTransformedElement = new SvgLineElement(0f, 2f, 8f, 2f);
				svgStyledTransformedElement2 = new SvgLineElement(0f, 6f, 8f, 6f);
				svgStyledTransformedElement3 = new SvgLineElement(2f, 0f, 2f, 8f);
				svgStyledTransformedElement4 = new SvgLineElement(6f, 0f, 6f, 8f);
				break;
			case HatchStyle.SmallCheckerBoard:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 1f, 1f);
				svgStyledTransformedElement2 = new SvgRectElement(4f, 4f, 1f, 1f);
				svgStyledTransformedElement3 = new SvgRectElement(4f, 0f, 1f, 1f);
				svgStyledTransformedElement4 = new SvgRectElement(0f, 4f, 1f, 1f);
				break;
			case HatchStyle.LargeCheckerBoard:
				svgStyledTransformedElement = new SvgRectElement(0f, 0f, 3f, 3f);
				svgStyledTransformedElement2 = new SvgRectElement(4f, 4f, 4f, 4f);
				break;
			case HatchStyle.OutlinedDiamond:
				svgStyledTransformedElement = new SvgLineElement(0f, 0f, 8f, 8f);
				svgStyledTransformedElement2 = new SvgLineElement(8f, 0f, 0f, 8f);
				break;
			case HatchStyle.SolidDiamond:
				svgStyledTransformedElement = new SvgPolygonElement("3 0 6 3 3 6 0 3");
				break;
			}
			if (svgStyledTransformedElement != null)
			{
				svgStyledTransformedElement.Style.Set("stroke", col);
				svgStyledTransformedElement.Style.Set("fill", col);
				patty.AddChild(svgStyledTransformedElement);
			}
			if (svgStyledTransformedElement2 != null)
			{
				svgStyledTransformedElement2.Style.Set("stroke", col);
				svgStyledTransformedElement2.Style.Set("fill", col);
				patty.AddChild(svgStyledTransformedElement2);
			}
			if (svgStyledTransformedElement3 != null)
			{
				svgStyledTransformedElement3.Style.Set("stroke", col);
				svgStyledTransformedElement3.Style.Set("fill", col);
				patty.AddChild(svgStyledTransformedElement3);
			}
			if (svgStyledTransformedElement4 != null)
			{
				svgStyledTransformedElement4.Style.Set("stroke", col);
				svgStyledTransformedElement4.Style.Set("fill", col);
				patty.AddChild(svgStyledTransformedElement4);
			}
		}

		private void DrawEndAnchors(Pen pen, PointF start, PointF end, bool ignoreUnsupportedLineCaps = false)
		{
			float angle = (float)Math.Atan((double)((start.X - end.X) / (start.Y - end.Y))) * -1f;
			float angle2 = (float)Math.Atan((double)((end.X - start.X) / (end.Y - start.Y))) * -1f;
			CustomLineCap clc = null;
			CustomLineCap clc2 = null;
			try
			{
				clc = pen.CustomStartCap;
			}
			catch (Exception)
			{
			}
			try
			{
				clc2 = pen.CustomEndCap;
			}
			catch (Exception)
			{
			}
			this.DrawEndAnchor(pen.StartCap, clc, pen.Color, pen.Width, start, angle, ignoreUnsupportedLineCaps);
			this.DrawEndAnchor(pen.EndCap, clc2, pen.Color, pen.Width, end, angle2, ignoreUnsupportedLineCaps);
		}

		private bool IsEndAnchorSimple(LineCap lc)
		{
			bool result;
			if (lc != LineCap.Flat)
			{
				switch (lc)
				{
				case LineCap.NoAnchor:
				case LineCap.SquareAnchor:
				case LineCap.RoundAnchor:
				case LineCap.DiamondAnchor:
				case LineCap.ArrowAnchor:
					break;
				default:
					result = false;
					return result;
				}
			}
			result = true;
			return result;
		}

		private void DrawEndAnchor(LineCap lc, CustomLineCap clc, Color col, float w, PointF pt, float angle, bool ignoreUnsupportedLineCaps)
		{
			SvgStyledTransformedElement svgStyledTransformedElement = null;
			if (lc != LineCap.Flat)
			{
				switch (lc)
				{
				case LineCap.NoAnchor:
					break;
				case LineCap.SquareAnchor:
				{
					float num = w / 3f * 2f;
					svgStyledTransformedElement = new SvgRectElement(0f - num, 0f - num, num * 2f, num * 2f);
					break;
				}
				case LineCap.RoundAnchor:
					svgStyledTransformedElement = new SvgEllipseElement(0f, 0f, w, w);
					break;
				case LineCap.DiamondAnchor:
					svgStyledTransformedElement = new SvgPolygonElement(new PointF[]
					{
						new PointF(0f, -w),
						new PointF(w, 0f),
						new PointF(0f, w),
						new PointF(-w, 0f)
					});
					break;
				case LineCap.ArrowAnchor:
					svgStyledTransformedElement = new SvgPolygonElement(new PointF[]
					{
						new PointF(0f, -w / 2f),
						new PointF(-w, w),
						new PointF(w, w)
					});
					break;
				default:
					if (lc != LineCap.Custom)
					{
						if (!ignoreUnsupportedLineCaps)
						{
							throw new SvgGdiNotImpl("DrawEndAnchor " + lc.ToString());
						}
					}
					else if (clc != null)
					{
						if (!ignoreUnsupportedLineCaps)
						{
							throw new SvgGdiNotImpl("DrawEndAnchor custom");
						}
					}
					break;
				}
			}
			if (svgStyledTransformedElement != null)
			{
				SvgStyledTransformedElement expr_1F5 = svgStyledTransformedElement;
				expr_1F5.Id += "_line_anchor";
				svgStyledTransformedElement.Style.Set("fill", new SvgColor(col));
				svgStyledTransformedElement.Style.Set("stroke", "none");
				Matrix matrix = new Matrix();
				matrix.Rotate(angle / 3.14159274f * 180f);
				Matrix matrix2 = new Matrix();
				matrix2.Translate(pt.X, pt.Y);
				svgStyledTransformedElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
				svgStyledTransformedElement.Transform.Add(matrix2);
				svgStyledTransformedElement.Transform.Add(matrix);
				this._cur.AddChild(svgStyledTransformedElement);
			}
		}

		private void FillBeziers(Brush brush, PointF[] points, FillMode fillmode)
		{
			SvgPathElement svgPathElement = new SvgPathElement();
			string text = string.Concat(new string[]
			{
				"M ",
				points[0].X.ToString("F", CultureInfo.InvariantCulture),
				" ",
				points[0].Y.ToString("F", CultureInfo.InvariantCulture),
				" C "
			});
			for (int i = 1; i < points.Length; i++)
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					points[i].X.ToString("F", CultureInfo.InvariantCulture),
					" ",
					points[i].Y.ToString("F", CultureInfo.InvariantCulture),
					" "
				});
			}
			text += "Z";
			svgPathElement.D = text;
			svgPathElement.Style = this.HandleBrush(brush);
			svgPathElement.Transform = new SvgTransformList(this._transforms.Result.Clone());
			this._cur.AddChild(svgPathElement);
		}

		private void DrawBitmapData(Bitmap b, float x, float y, float w, float h, bool scale)
		{
			SvgGroupElement svgGroupElement = new SvgGroupElement("bitmap_at_" + x.ToString("F", CultureInfo.InvariantCulture) + "_" + y.ToString("F", CultureInfo.InvariantCulture));
			float num = 1f;
			float num2 = 1f;
			if (scale)
			{
				num = w / (float)b.Width;
				num2 = h / (float)b.Height;
			}
			for (int i = 0; i < b.Height; i++)
			{
				for (int j = 0; j < b.Width; j++)
				{
					Color pixel = b.GetPixel(j, i);
					if (!scale)
					{
						if ((float)j <= w && (float)i <= h)
						{
							this.DrawImagePixel(svgGroupElement, pixel, x + (float)j, y + (float)i, 1f, 1f);
						}
					}
					else
					{
						this.DrawImagePixel(svgGroupElement, pixel, x + (float)j * num, y + (float)i * num2, num, num2);
					}
				}
			}
			if (!this._transforms.Result.IsIdentity)
			{
				svgGroupElement.Transform = this._transforms.Result.Clone();
			}
			this._cur.AddChild(svgGroupElement);
		}

		private void DrawImagePixel(SvgElement container, Color c, float x, float y, float w, float h)
		{
			if (c.A != 0)
			{
				SvgRectElement svgRectElement = new SvgRectElement(x, y, w, h);
				svgRectElement.Id = "";
				svgRectElement.Style.Set("fill", string.Concat(new object[]
				{
					"rgb(",
					c.R,
					",",
					c.G,
					",",
					c.B,
					")"
				}));
				if (c.A < 255)
				{
					svgRectElement.Style.Set("opacity", (float)c.A / 255f);
				}
				container.AddChild(svgRectElement);
			}
		}

		private static PointF[] Point2PointF(Point[] p)
		{
			PointF[] array = new PointF[p.Length];
			for (int i = 0; i < p.Length; i++)
			{
				array[i] = new PointF((float)p[i].X, (float)p[i].Y);
			}
			return array;
		}

		private static PointF[] Spline2Bez(PointF[] points, int start, int num, bool closed, float tension)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			int num2 = points.Length - 1;
			arrayList2.Add(points[0]);
			arrayList2.Add(SvgGraphics.ControlPoint(points[1], points[0], tension));
			for (int i = 1; i < num2; i++)
			{
				PointF[] array = SvgGraphics.ControlPoints(points[i - 1], points[i + 1], points[i], tension);
				arrayList2.Add(array[0]);
				arrayList2.Add(points[i]);
				arrayList2.Add(array[1]);
			}
			arrayList2.Add(SvgGraphics.ControlPoint(points[num2 - 1], points[num2], tension));
			arrayList2.Add(points[num2]);
			PointF[] result;
			if (closed)
			{
				PointF[] array = SvgGraphics.ControlPoints(points[num2], points[1], points[0], tension);
				arrayList2[1] = array[1];
				array = SvgGraphics.ControlPoints(points[num2 - 1], points[0], points[num2], tension);
				arrayList2[arrayList2.Count - 2] = array[0];
				arrayList2.Add(array[1]);
				array = SvgGraphics.ControlPoints(points[num2], points[1], points[0], tension);
				arrayList2.Add(array[0]);
				arrayList2.Add(points[0]);
				result = (PointF[])arrayList2.ToArray(typeof(PointF));
			}
			else
			{
				ArrayList arrayList3 = new ArrayList();
				for (int i = start * 3; i < (start + num) * 3; i++)
				{
					arrayList3.Add(arrayList2[i]);
				}
				arrayList3.Add(arrayList2[(start + num) * 3]);
				result = (PointF[])arrayList3.ToArray(typeof(PointF));
			}
			return result;
		}

		private static PointF[] ControlPoints(PointF l, PointF r, PointF pt, float t)
		{
			PointF pointF = new PointF(l.X - pt.X, l.Y - pt.Y);
			PointF pointF2 = new PointF(r.X - pt.X, r.Y - pt.Y);
			PointF pointF3 = new PointF(pointF.X - pointF2.X, pointF.Y - pointF2.Y);
			PointF pointF4 = new PointF(pointF2.X - pointF.X, pointF2.Y - pointF.Y);
			float num = (float)Math.Sqrt((double)(pointF3.X * pointF3.X + pointF3.Y * pointF3.Y));
			pointF3.X /= (float)Math.Sqrt((double)(num / (10f * t * t)));
			pointF3.Y /= (float)Math.Sqrt((double)(num / (10f * t * t)));
			float num2 = (float)Math.Sqrt((double)(pointF4.X * pointF4.X + pointF4.Y * pointF4.Y));
			pointF4.X /= (float)Math.Sqrt((double)(num2 / (10f * t * t)));
			pointF4.Y /= (float)Math.Sqrt((double)(num2 / (10f * t * t)));
			return new PointF[]
			{
				new PointF(pt.X + pointF3.X, pt.Y + pointF3.Y),
				new PointF(pt.X + pointF4.X, pt.Y + pointF4.Y)
			};
		}

		private static PointF ControlPoint(PointF l, PointF pt, float t)
		{
			PointF pointF = new PointF(l.X - pt.X, l.Y - pt.Y);
			float num = (float)Math.Sqrt((double)(pointF.X * pointF.X + pointF.Y * pointF.Y));
			pointF.X /= (float)Math.Sqrt((double)(num / (10f * t * t)));
			pointF.Y /= (float)Math.Sqrt((double)(num / (10f * t * t)));
			return new PointF(pt.X + pointF.X, pt.Y + pointF.Y);
		}

		private static string GDIArc2SVGPath(float x, float y, float width, float height, float startAngle, float sweepAngle, bool pie)
		{
			int num = 0;
			PointF pointF = default(PointF);
			PointF pointF2 = default(PointF);
			PointF pointF3 = new PointF(x + width / 2f, y + height / 2f);
			startAngle = startAngle / 360f * 2f * 3.14159274f;
			sweepAngle = sweepAngle / 360f * 2f * 3.14159274f;
			sweepAngle += startAngle;
			if (sweepAngle > startAngle)
			{
				float num2 = startAngle;
				startAngle = sweepAngle;
				sweepAngle = num2;
			}
			if ((double)(sweepAngle - startAngle) > 3.1415926535897931 || (double)(startAngle - sweepAngle) > 3.1415926535897931)
			{
				num = 1;
			}
			pointF.X = (float)Math.Cos((double)startAngle) * (width / 2f) + pointF3.X;
			pointF.Y = (float)Math.Sin((double)startAngle) * (height / 2f) + pointF3.Y;
			pointF2.X = (float)Math.Cos((double)sweepAngle) * (width / 2f) + pointF3.X;
			pointF2.Y = (float)Math.Sin((double)sweepAngle) * (height / 2f) + pointF3.Y;
			string text = string.Concat(new string[]
			{
				"M ",
				pointF.X.ToString("F", CultureInfo.InvariantCulture),
				",",
				pointF.Y.ToString("F", CultureInfo.InvariantCulture),
				" A ",
				(width / 2f).ToString("F", CultureInfo.InvariantCulture),
				" ",
				(height / 2f).ToString("F", CultureInfo.InvariantCulture),
				" 0 ",
				num.ToString(),
				" 0 ",
				pointF2.X.ToString("F", CultureInfo.InvariantCulture),
				" ",
				pointF2.Y.ToString("F", CultureInfo.InvariantCulture)
			});
			if (pie)
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" L ",
					pointF3.X.ToString("F", CultureInfo.InvariantCulture),
					",",
					pointF3.Y.ToString("F", CultureInfo.InvariantCulture)
				});
				text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" L ",
					pointF.X.ToString("F", CultureInfo.InvariantCulture),
					",",
					pointF.Y.ToString("F", CultureInfo.InvariantCulture)
				});
			}
			return text;
		}
	}
}
