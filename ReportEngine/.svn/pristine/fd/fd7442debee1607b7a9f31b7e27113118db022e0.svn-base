using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace SvgNet.SvgGdi
{
	public class GdiGraphics : IGraphics
	{
		private Graphics _g;

		public CompositingMode CompositingMode
		{
			get
			{
				return this._g.CompositingMode;
			}
			set
			{
				this._g.CompositingMode = value;
			}
		}

		public Point RenderingOrigin
		{
			get
			{
				return this._g.RenderingOrigin;
			}
			set
			{
				this._g.RenderingOrigin = value;
			}
		}

		public CompositingQuality CompositingQuality
		{
			get
			{
				return this._g.CompositingQuality;
			}
			set
			{
				this._g.CompositingQuality = value;
			}
		}

		public TextRenderingHint TextRenderingHint
		{
			get
			{
				return this._g.TextRenderingHint;
			}
			set
			{
				this._g.TextRenderingHint = value;
			}
		}

		public int TextContrast
		{
			get
			{
				return this._g.TextContrast;
			}
			set
			{
				this._g.TextContrast = value;
			}
		}

		public SmoothingMode SmoothingMode
		{
			get
			{
				return this._g.SmoothingMode;
			}
			set
			{
				this._g.SmoothingMode = value;
			}
		}

		public PixelOffsetMode PixelOffsetMode
		{
			get
			{
				return this._g.PixelOffsetMode;
			}
			set
			{
				this._g.PixelOffsetMode = value;
			}
		}

		public InterpolationMode InterpolationMode
		{
			get
			{
				return this._g.InterpolationMode;
			}
			set
			{
				this._g.InterpolationMode = value;
			}
		}

		public Matrix Transform
		{
			get
			{
				return this._g.Transform;
			}
			set
			{
				this._g.Transform = value;
			}
		}

		public GraphicsUnit PageUnit
		{
			get
			{
				return this._g.PageUnit;
			}
			set
			{
				this._g.PageUnit = value;
			}
		}

		public float PageScale
		{
			get
			{
				return this._g.PageScale;
			}
			set
			{
				this._g.PageScale = value;
			}
		}

		public float DpiX
		{
			get
			{
				return this._g.DpiX;
			}
		}

		public float DpiY
		{
			get
			{
				return this._g.DpiY;
			}
		}

		public Region Clip
		{
			get
			{
				return this._g.Clip;
			}
			set
			{
				this._g.Clip = value;
			}
		}

		public RectangleF ClipBounds
		{
			get
			{
				return this._g.ClipBounds;
			}
		}

		public bool IsClipEmpty
		{
			get
			{
				return this._g.IsClipEmpty;
			}
		}

		public RectangleF VisibleClipBounds
		{
			get
			{
				return this._g.VisibleClipBounds;
			}
		}

		public bool IsVisibleClipEmpty
		{
			get
			{
				return this._g.IsVisibleClipEmpty;
			}
		}

		public GdiGraphics(Graphics g)
		{
			this._g = g;
		}

		public void Flush()
		{
			this._g.Flush();
		}

		public void Flush(FlushIntention intention)
		{
			this._g.Flush(intention);
		}

		public void ResetTransform()
		{
			this._g.ResetTransform();
		}

		public void MultiplyTransform(Matrix matrix)
		{
			this._g.MultiplyTransform(matrix);
		}

		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			this._g.MultiplyTransform(matrix, order);
		}

		public void TranslateTransform(float dx, float dy)
		{
			this._g.TranslateTransform(dx, dy);
		}

		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			this._g.TranslateTransform(dx, dy, order);
		}

		public void ScaleTransform(float sx, float sy)
		{
			this._g.ScaleTransform(sx, sy);
		}

		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			this._g.ScaleTransform(sx, sy);
		}

		public void RotateTransform(float angle)
		{
			this._g.RotateTransform(angle);
		}

		public void RotateTransform(float angle, MatrixOrder order)
		{
			this._g.RotateTransform(angle, order);
		}

		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
		{
			this._g.TransformPoints(destSpace, srcSpace, pts);
		}

		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)
		{
			this._g.TransformPoints(destSpace, srcSpace, pts);
		}

		public Color GetNearestColor(Color color)
		{
			return this._g.GetNearestColor(color);
		}

		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
		{
			this._g.DrawLine(pen, x1, y1, x2, y2);
		}

		public void DrawLine(Pen pen, PointF pt1, PointF pt2)
		{
			this._g.DrawLine(pen, pt1, pt2);
		}

		public void DrawLines(Pen pen, PointF[] points)
		{
			this._g.DrawLines(pen, points);
		}

		public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
		{
			this._g.DrawLine(pen, x1, y1, x2, y2);
		}

		public void DrawLine(Pen pen, Point pt1, Point pt2)
		{
			this._g.DrawLine(pen, pt1, pt2);
		}

		public void DrawLines(Pen pen, Point[] points)
		{
			this._g.DrawLines(pen, points);
		}

		public void DrawArc(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			this._g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
		}

		public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			this._g.DrawArc(pen, rect, startAngle, sweepAngle);
		}

		public void DrawArc(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			this._g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
		}

		public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			this._g.DrawArc(pen, rect, startAngle, sweepAngle);
		}

		public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			this._g.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
		}

		public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
		{
			this._g.DrawBezier(pen, pt1, pt2, pt3, pt4);
		}

		public void DrawBeziers(Pen pen, PointF[] points)
		{
			this._g.DrawBeziers(pen, points);
		}

		public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
		{
			this._g.DrawBezier(pen, pt1, pt2, pt3, pt4);
		}

		public void DrawBeziers(Pen pen, Point[] points)
		{
			this._g.DrawBeziers(pen, points);
		}

		public void DrawRectangle(Pen pen, Rectangle rect)
		{
			this._g.DrawRectangle(pen, rect);
		}

		public void DrawRectangle(Pen pen, float x, float y, float width, float height)
		{
			this._g.DrawRectangle(pen, x, y, width, height);
		}

		public void DrawRectangles(Pen pen, RectangleF[] rects)
		{
			this._g.DrawRectangles(pen, rects);
		}

		public void DrawRectangle(Pen pen, int x, int y, int width, int height)
		{
			this._g.DrawRectangle(pen, x, y, width, height);
		}

		public void DrawRectangles(Pen pen, Rectangle[] rects)
		{
			this._g.DrawRectangles(pen, rects);
		}

		public void DrawEllipse(Pen pen, RectangleF rect)
		{
			this._g.DrawEllipse(pen, rect);
		}

		public void DrawEllipse(Pen pen, float x, float y, float width, float height)
		{
			this._g.DrawEllipse(pen, x, y, width, height);
		}

		public void DrawEllipse(Pen pen, Rectangle rect)
		{
			this._g.DrawEllipse(pen, rect);
		}

		public void DrawEllipse(Pen pen, int x, int y, int width, int height)
		{
			this._g.DrawEllipse(pen, x, y, width, height);
		}

		public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			this._g.DrawPie(pen, rect, startAngle, sweepAngle);
		}

		public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			this._g.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
		}

		public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			this._g.DrawPie(pen, rect, startAngle, sweepAngle);
		}

		public void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			this._g.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
		}

		public void DrawPolygon(Pen pen, PointF[] points)
		{
			this._g.DrawPolygon(pen, points);
		}

		public void DrawPolygon(Pen pen, Point[] points)
		{
			this._g.DrawPolygon(pen, points);
		}

		public void DrawPath(Pen pen, GraphicsPath path)
		{
			this._g.DrawPath(pen, path);
		}

		public void DrawCurve(Pen pen, PointF[] points)
		{
			this._g.DrawCurve(pen, points);
		}

		public void DrawCurve(Pen pen, PointF[] points, float tension)
		{
			this._g.DrawCurve(pen, points, tension);
		}

		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
		{
			this._g.DrawCurve(pen, points, offset, numberOfSegments);
		}

		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
		{
			this._g.DrawCurve(pen, points, offset, numberOfSegments, tension);
		}

		public void DrawCurve(Pen pen, Point[] points)
		{
			this._g.DrawCurve(pen, points);
		}

		public void DrawCurve(Pen pen, Point[] points, float tension)
		{
			this._g.DrawCurve(pen, points, tension);
		}

		public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension)
		{
			this._g.DrawCurve(pen, points, offset, numberOfSegments, tension);
		}

		public void DrawClosedCurve(Pen pen, PointF[] points)
		{
			this._g.DrawClosedCurve(pen, points);
		}

		public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode)
		{
			this._g.DrawClosedCurve(pen, points, tension, fillmode);
		}

		public void DrawClosedCurve(Pen pen, Point[] points)
		{
			this._g.DrawClosedCurve(pen, points);
		}

		public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode)
		{
			this._g.DrawClosedCurve(pen, points, tension, fillmode);
		}

		public void Clear(Color color)
		{
			this._g.Clear(color);
		}

		public void FillRectangle(Brush brush, RectangleF rect)
		{
			this._g.FillRectangle(brush, rect);
		}

		public void FillRectangle(Brush brush, float x, float y, float width, float height)
		{
			this._g.FillRectangle(brush, x, y, width, height);
		}

		public void FillRectangles(Brush brush, RectangleF[] rects)
		{
			this._g.FillRectangles(brush, rects);
		}

		public void FillRectangle(Brush brush, Rectangle rect)
		{
			this._g.FillRectangle(brush, rect);
		}

		public void FillRectangle(Brush brush, int x, int y, int width, int height)
		{
			this._g.FillRectangle(brush, x, y, width, height);
		}

		public void FillRectangles(Brush brush, Rectangle[] rects)
		{
			this._g.FillRectangles(brush, rects);
		}

		public void FillPolygon(Brush brush, PointF[] points)
		{
			this._g.FillPolygon(brush, points);
		}

		public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
		{
			this._g.FillPolygon(brush, points, fillMode);
		}

		public void FillPolygon(Brush brush, Point[] points)
		{
			this._g.FillPolygon(brush, points);
		}

		public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
		{
			this._g.FillPolygon(brush, points, fillMode);
		}

		public void FillEllipse(Brush brush, RectangleF rect)
		{
			this._g.FillEllipse(brush, rect);
		}

		public void FillEllipse(Brush brush, float x, float y, float width, float height)
		{
			this._g.FillEllipse(brush, x, y, width, height);
		}

		public void FillEllipse(Brush brush, Rectangle rect)
		{
			this._g.FillEllipse(brush, rect);
		}

		public void FillEllipse(Brush brush, int x, int y, int width, int height)
		{
			this._g.FillEllipse(brush, x, y, width, height);
		}

		public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle)
		{
			this._g.FillPie(brush, rect, startAngle, sweepAngle);
		}

		public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			this._g.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
		}

		public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			this._g.FillPie(brush, x, y, width, height, startAngle, sweepAngle);
		}

		public void FillPath(Brush brush, GraphicsPath path)
		{
			this._g.FillPath(brush, path);
		}

		public void FillClosedCurve(Brush brush, PointF[] points)
		{
			this._g.FillClosedCurve(brush, points);
		}

		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode)
		{
			this._g.FillClosedCurve(brush, points, fillmode);
		}

		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension)
		{
			this._g.FillClosedCurve(brush, points, fillmode, tension);
		}

		public void FillClosedCurve(Brush brush, Point[] points)
		{
			this._g.FillClosedCurve(brush, points);
		}

		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode)
		{
			this._g.FillClosedCurve(brush, points, fillmode);
		}

		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension)
		{
			this._g.FillClosedCurve(brush, points, fillmode, tension);
		}

		public void FillRegion(Brush brush, Region region)
		{
			this._g.FillRegion(brush, region);
		}

		public void DrawString(string s, Font font, Brush brush, float x, float y)
		{
			this._g.DrawString(s, font, brush, x, y);
		}

		public void DrawString(string s, Font font, Brush brush, PointF point)
		{
			this._g.DrawString(s, font, brush, point);
		}

		public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
		{
			this._g.DrawString(s, font, brush, x, y, format);
		}

		public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
		{
			this._g.DrawString(s, font, brush, point, format);
		}

		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
		{
			this._g.DrawString(s, font, brush, layoutRectangle);
		}

		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
		{
			this._g.DrawString(s, font, brush, layoutRectangle, format);
		}

		public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled)
		{
			int num;
			int num2;
			SizeF result = this._g.MeasureString(text, font, layoutArea, stringFormat, out num, out num2);
			charactersFitted = num;
			linesFilled = num2;
			return result;
		}

		public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
		{
			return this._g.MeasureString(text, font, origin, stringFormat);
		}

		public SizeF MeasureString(string text, Font font, SizeF layoutArea)
		{
			return this._g.MeasureString(text, font, layoutArea);
		}

		public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
		{
			return this._g.MeasureString(text, font, layoutArea, stringFormat);
		}

		public SizeF MeasureString(string text, Font font)
		{
			return this._g.MeasureString(text, font);
		}

		public SizeF MeasureString(string text, Font font, int width)
		{
			return this._g.MeasureString(text, font, width);
		}

		public SizeF MeasureString(string text, Font font, int width, StringFormat format)
		{
			return this._g.MeasureString(text, font, width, format);
		}

		public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
		{
			return this._g.MeasureCharacterRanges(text, font, layoutRect, stringFormat);
		}

		public void DrawIcon(Icon icon, int x, int y)
		{
			this._g.DrawIcon(icon, x, y);
		}

		public void DrawIcon(Icon icon, Rectangle targetRect)
		{
			this._g.DrawIcon(icon, targetRect);
		}

		public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
		{
			this._g.DrawIconUnstretched(icon, targetRect);
		}

		public void DrawImage(Image image, PointF point)
		{
			this._g.DrawImage(image, point);
		}

		public void DrawImage(Image image, float x, float y)
		{
			this._g.DrawImage(image, x, y);
		}

		public void DrawImage(Image image, RectangleF rect)
		{
			this._g.DrawImage(image, rect);
		}

		public void DrawImage(Image image, float x, float y, float width, float height)
		{
			this._g.DrawImage(image, x, y, width, height);
		}

		public void DrawImage(Image image, Point point)
		{
			this._g.DrawImage(image, point);
		}

		public void DrawImage(Image image, int x, int y)
		{
			this._g.DrawImage(image, x, y);
		}

		public void DrawImage(Image image, Rectangle rect)
		{
			this._g.DrawImage(image, rect);
		}

		public void DrawImage(Image image, int x, int y, int width, int height)
		{
			this._g.DrawImage(image, x, y, width, height);
		}

		public void DrawImageUnscaled(Image image, Point point)
		{
			this._g.DrawImageUnscaled(image, point);
		}

		public void DrawImageUnscaled(Image image, int x, int y)
		{
			this._g.DrawImageUnscaled(image, x, y);
		}

		public void DrawImageUnscaled(Image image, Rectangle rect)
		{
			this._g.DrawImageUnscaled(image, rect);
		}

		public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
		{
			this._g.DrawImageUnscaled(image, x, y, width, height);
		}

		public void DrawImage(Image image, PointF[] destPoints)
		{
			this._g.DrawImage(image, destPoints);
		}

		public void DrawImage(Image image, Point[] destPoints)
		{
			this._g.DrawImage(image, destPoints);
		}

		public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			this._g.DrawImage(image, x, y, srcRect, srcUnit);
		}

		public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			this._g.DrawImage(image, x, y, srcRect, srcUnit);
		}

		public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			this._g.DrawImage(image, destRect, srcRect, srcUnit);
		}

		public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			this._g.DrawImage(image, destRect, srcRect, srcUnit);
		}

		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			this._g.DrawImage(image, destPoints, srcRect, srcUnit);
		}

		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			this._g.DrawImage(image, destPoints, srcRect, srcUnit);
		}

		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			this._g.DrawImage(image, destPoints, srcRect, srcUnit);
		}

		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			this._g.DrawImage(image, destPoints, srcRect, srcUnit);
		}

		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			this._g.DrawImage(image, destPoints, srcRect, srcUnit);
		}

		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit)
		{
			this._g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
		}

		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
		{
			this._g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
		}

		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit)
		{
			this._g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
		}

		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			this._g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit, imageAttr);
		}

		public void SetClip(Graphics g)
		{
			this._g.SetClip(g);
		}

		public void SetClip(Graphics g, CombineMode combineMode)
		{
			this._g.SetClip(g, combineMode);
		}

		public void SetClip(Rectangle rect)
		{
			this._g.SetClip(rect);
		}

		public void SetClip(Rectangle rect, CombineMode combineMode)
		{
			this._g.SetClip(rect, combineMode);
		}

		public void SetClip(RectangleF rect)
		{
			this._g.SetClip(rect);
		}

		public void SetClip(RectangleF rect, CombineMode combineMode)
		{
			this._g.SetClip(rect, combineMode);
		}

		public void SetClip(GraphicsPath path)
		{
			this._g.SetClip(path);
		}

		public void SetClip(GraphicsPath path, CombineMode combineMode)
		{
			this._g.SetClip(path, combineMode);
		}

		public void SetClip(Region region, CombineMode combineMode)
		{
			this._g.SetClip(region, combineMode);
		}

		public void IntersectClip(Rectangle rect)
		{
			this._g.IntersectClip(rect);
		}

		public void IntersectClip(RectangleF rect)
		{
			this._g.IntersectClip(rect);
		}

		public void IntersectClip(Region region)
		{
			this._g.IntersectClip(region);
		}

		public void ExcludeClip(Rectangle rect)
		{
			this._g.ExcludeClip(rect);
		}

		public void ExcludeClip(Region region)
		{
			this._g.ExcludeClip(region);
		}

		public void ResetClip()
		{
			this._g.ResetClip();
		}

		public void TranslateClip(float dx, float dy)
		{
			this._g.TranslateClip(dx, dy);
		}

		public void TranslateClip(int dx, int dy)
		{
			this._g.TranslateClip(dx, dy);
		}

		public bool IsVisible(int x, int y)
		{
			return this._g.IsVisible(x, y);
		}

		public bool IsVisible(Point point)
		{
			return this._g.IsVisible(point);
		}

		public bool IsVisible(float x, float y)
		{
			return this._g.IsVisible(x, y);
		}

		public bool IsVisible(PointF point)
		{
			return this._g.IsVisible(point);
		}

		public bool IsVisible(int x, int y, int width, int height)
		{
			return this._g.IsVisible(x, y, width, height);
		}

		public bool IsVisible(Rectangle rect)
		{
			return this._g.IsVisible(rect);
		}

		public bool IsVisible(float x, float y, float width, float height)
		{
			return this._g.IsVisible(x, y, width, height);
		}

		public bool IsVisible(RectangleF rect)
		{
			return this._g.IsVisible(rect);
		}

		public GraphicsState Save()
		{
			return this._g.Save();
		}

		public void Restore(GraphicsState gstate)
		{
			this._g.Restore(gstate);
		}

		public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)
		{
			return this._g.BeginContainer(dstrect, srcrect, unit);
		}

		public GraphicsContainer BeginContainer()
		{
			return this._g.BeginContainer();
		}

		public void EndContainer(GraphicsContainer container)
		{
			this._g.EndContainer(container);
		}

		public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)
		{
			return this._g.BeginContainer(dstrect, srcrect, unit);
		}

		public void AddMetafileComment(byte[] data)
		{
			this._g.AddMetafileComment(data);
		}
	}
}
