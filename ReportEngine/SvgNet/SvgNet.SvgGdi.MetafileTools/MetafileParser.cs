using SvgNet.SvgGdi.MetafileTools.EmfTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace SvgNet.SvgGdi.MetafileTools
{
	public class MetafileParser
	{
		public enum EmfTransformMode
		{
			MWT_IDENTITY = 1,
			MWT_LEFTMULTIPLY,
			MWT_RIGHTMULTIPLY
		}

		public enum EmfStockObject
		{
			WHITE_BRUSH,
			LTGRAY_BRUSH,
			GRAY_BRUSH,
			DKGRAY_BRUSH,
			BLACK_BRUSH,
			NULL_BRUSH,
			WHITE_PEN,
			BLACK_PEN,
			NULL_PEN,
			OEM_FIXED_FONT = 10,
			ANSI_FIXED_FONT,
			ANSI_VAR_FONT,
			SYSTEM_FONT,
			DEVICE_DEFAULT_FONT,
			DEFAULT_PALETTE,
			SYSTEM_FIXED_FONT,
			DEFAULT_GUI_FONT,
			DC_BRUSH,
			DC_PEN,
			MinValue = 0,
			MaxValue = 19
		}

		public enum EmfBrushStyle
		{
			BS_SOLID,
			BS_NULL,
			BS_HATCHED,
			BS_PATTERN,
			BS_INDEXED,
			BS_DIBPATTERN,
			BS_DIBPATTERNPT,
			BS_PATTERN8X8,
			BS_DIBPATTERN8X8,
			BS_MONOPATTERN
		}

		private class ObjectHandle
		{
			private MetafileParser.EmfStockObject? _stockObject;

			private Brush _brush;

			public bool IsStockObject
			{
				get
				{
					return this._stockObject.HasValue;
				}
			}

			public bool IsBrush
			{
				get
				{
					return this._brush != null;
				}
			}

			public ObjectHandle(MetafileParser.EmfStockObject stockObject)
			{
				this._stockObject = new MetafileParser.EmfStockObject?(stockObject);
			}

			public ObjectHandle(Brush brush)
			{
				this._brush = brush;
			}

			public MetafileParser.EmfStockObject GetStockObject()
			{
				return this._stockObject.Value;
			}

			public Brush GetBrush()
			{
				return this._brush;
			}
		}

		private class NormalizedPoint
		{
			public PointF Point;

			public int VisualIndex;
		}

		private class VisualPoint
		{
			public PointF Point;

			public int VisualIndex;

			public bool IsLocked;

			public int Weight;
		}

		private class LineBuffer
		{
			private List<MetafileParser.NormalizedPoint> _normalizedPoints;

			private List<MetafileParser.VisualPoint> _visualPoints;

			private List<MetafileParser.NormalizedPoint> _points;

			private float _epsilonSquare;

			private static float UnitSizeEpsilon = 2f;

			public bool IsEmpty
			{
				get
				{
					return this._points.Count == 0;
				}
			}

			public LineBuffer(float unitSize)
			{
				this._points = new List<MetafileParser.NormalizedPoint>();
				this._normalizedPoints = new List<MetafileParser.NormalizedPoint>();
				this._visualPoints = new List<MetafileParser.VisualPoint>();
				this._epsilonSquare = MetafileParser.LineBuffer.UnitSizeEpsilon * unitSize * (MetafileParser.LineBuffer.UnitSizeEpsilon * unitSize);
			}

			private MetafileParser.NormalizedPoint GetLastPoint()
			{
				return this._points[this._points.Count - 1];
			}

			private bool IsVisuallyIdentical(MetafileParser.NormalizedPoint a, MetafileParser.NormalizedPoint b)
			{
				return a.VisualIndex == b.VisualIndex;
			}

			private bool IsVisuallyIdentical(MetafileParser.NormalizedPoint a, PointF b)
			{
				bool result;
				for (int i = this._normalizedPoints.Count - 1; i >= 0; i--)
				{
					if (this._normalizedPoints[i].VisualIndex == a.VisualIndex && this.IsVisuallyIdentical(this._normalizedPoints[i].Point, b))
					{
						result = true;
						return result;
					}
				}
				result = false;
				return result;
			}

			private bool IsVisuallyIdentical(PointF a, PointF b)
			{
				float num = a.X - b.X;
				float num2 = a.Y - b.Y;
				return num * num + num2 * num2 <= this._epsilonSquare;
			}

			private MetafileParser.NormalizedPoint Add(PointF point)
			{
				MetafileParser.VisualPoint visualPoint;
				MetafileParser.NormalizedPoint normalizedPoint;
				MetafileParser.NormalizedPoint result;
				for (int i = this._normalizedPoints.Count - 1; i >= 0; i--)
				{
					if (this.IsVisuallyIdentical(this._normalizedPoints[i].Point, point))
					{
						visualPoint = this._visualPoints[this._normalizedPoints[i].VisualIndex];
						visualPoint.Weight++;
						normalizedPoint = new MetafileParser.NormalizedPoint
						{
							Point = point,
							VisualIndex = visualPoint.VisualIndex
						};
						this._normalizedPoints.Add(normalizedPoint);
						result = normalizedPoint;
						return result;
					}
				}
				visualPoint = new MetafileParser.VisualPoint
				{
					IsLocked = false,
					VisualIndex = this._visualPoints.Count,
					Weight = 1
				};
				this._visualPoints.Add(visualPoint);
				normalizedPoint = new MetafileParser.NormalizedPoint
				{
					Point = point,
					VisualIndex = visualPoint.VisualIndex
				};
				this._normalizedPoints.Add(normalizedPoint);
				result = normalizedPoint;
				return result;
			}

			public bool CanAdd(PointF[] points, int offset, int count)
			{
				return this.IsEmpty || this.IsVisuallyIdentical(this.GetLastPoint(), points[offset]);
			}

			private void MakeRoom(int count)
			{
				if (this._points.Capacity < this._points.Count + count)
				{
					this._points.Capacity = this._points.Count + count;
				}
			}

			public void Add(PointF[] points, int offset, int count)
			{
				if (this.IsEmpty)
				{
					this.MakeRoom(count);
					for (int i = 0; i < count; i++)
					{
						this._points.Add(this.Add(points[offset + i]));
					}
				}
				else
				{
					if (!this.IsVisuallyIdentical(this.GetLastPoint(), points[offset]))
					{
						throw new ArgumentOutOfRangeException();
					}
					this.MakeRoom(count - 1);
					this.Add(points[offset]);
					for (int i = 1; i < count; i++)
					{
						this._points.Add(this.Add(points[offset + i]));
					}
				}
			}

			public void Clear()
			{
				this._points.Clear();
			}

			public PointF[] GetPoints()
			{
				List<MetafileParser.NormalizedPoint> list = new List<MetafileParser.NormalizedPoint>();
				list.Add(this._points[0]);
				for (int i = 1; i < this._points.Count; i++)
				{
					if (!this.IsVisuallyIdentical(list[list.Count - 1], this._points[i]))
					{
						list.Add(this._points[i]);
					}
				}
				PointF[] result;
				if (list.Count <= 1)
				{
					result = null;
				}
				else
				{
					List<PointF> list2 = new List<PointF>();
					for (int i = 0; i < list.Count; i++)
					{
						MetafileParser.VisualPoint visualPoint = this._visualPoints[list[i].VisualIndex];
						if (!visualPoint.IsLocked)
						{
							double num = 0.0;
							double num2 = 0.0;
							int num3 = 0;
							int j = visualPoint.Weight;
							while (j > 0)
							{
								if (this._normalizedPoints[num3].VisualIndex == visualPoint.VisualIndex)
								{
									num += (double)this._normalizedPoints[num3].Point.X;
									num2 += (double)this._normalizedPoints[num3].Point.Y;
									j--;
								}
								num3++;
							}
							visualPoint.Point = new PointF((float)(num / (double)visualPoint.Weight), (float)(num2 / (double)visualPoint.Weight));
							visualPoint.IsLocked = true;
						}
						list2.Add(visualPoint.Point);
					}
					result = list2.ToArray();
				}
				return result;
			}
		}

		private Matrix Transform;

		private static uint StockObjectMinCode = 2147483648u;

		private static uint StockObjectMaxCode = 2147483667u;

		private Dictionary<uint, MetafileParser.ObjectHandle> _objects;

		private PointF _zero;

		private DrawLineDelegate _drawLine;

		private FillPolygonDelegate _fillPolygon;

		private PointF _moveTo;

		private PointF _curveOrigin;

		private Brush _brush;

		private MetafileParser.LineBuffer _lineBuffer;

		private void ProcessModifyWorldTransform(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				float m = binaryReader.ReadSingle();
				float m2 = binaryReader.ReadSingle();
				float m3 = binaryReader.ReadSingle();
				float m4 = binaryReader.ReadSingle();
				float dx = binaryReader.ReadSingle();
				float dy = binaryReader.ReadSingle();
				MetafileParser.EmfTransformMode emfTransformMode = (MetafileParser.EmfTransformMode)binaryReader.ReadInt32();
				Matrix matrix = new Matrix(m, m2, m3, m4, dx, dy);
				switch (emfTransformMode)
				{
				case MetafileParser.EmfTransformMode.MWT_IDENTITY:
					this.Transform = new Matrix();
					break;
				case MetafileParser.EmfTransformMode.MWT_LEFTMULTIPLY:
					this.Transform.Multiply(matrix, MatrixOrder.Append);
					break;
				case MetafileParser.EmfTransformMode.MWT_RIGHTMULTIPLY:
					this.Transform.Multiply(matrix, MatrixOrder.Prepend);
					break;
				default:
					throw new NotImplementedException();
				}
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void InternalProcessPolyline16(uint numberOfPolygons, uint totalNumberOfPoints, int[] numberOfPoints, BinaryReader reader)
		{
			PointF[] array = new PointF[totalNumberOfPoints];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].X = (float)reader.ReadInt16();
				array[i].Y = (float)reader.ReadInt16();
			}
			this.Transform.TransformPoints(array);
			int num = 0;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)numberOfPolygons))
			{
				this.DrawLine(array, num, numberOfPoints[num2]);
				num += numberOfPoints[num2];
				num2++;
			}
		}

		private void ProcessPolygon16(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				binaryReader.ReadBytes(16);
				uint num = binaryReader.ReadUInt32();
				this.InternalProcessPolyline16(1u, num, new int[]
				{
					(int)num
				}, binaryReader);
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void ProcessPolyPolygon16(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				binaryReader.ReadBytes(16);
				uint num = binaryReader.ReadUInt32();
				uint totalNumberOfPoints = binaryReader.ReadUInt32();
				int[] array = new int[num];
				int num2 = 0;
				while ((long)num2 < (long)((ulong)num))
				{
					array[num2] = (int)binaryReader.ReadUInt32();
					num2++;
				}
				this.InternalProcessPolyline16(num, totalNumberOfPoints, array, binaryReader);
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void ProcessPolyline16(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				binaryReader.ReadBytes(16);
				uint num = 1u;
				uint num2 = binaryReader.ReadUInt32();
				int[] array = new int[num];
				array[0] = (int)num2;
				this.InternalProcessPolyline16(num, num2, array, binaryReader);
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void ProcessPolylineTo16(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				binaryReader.ReadBytes(16);
				uint num = binaryReader.ReadUInt32();
				PointF[] array = new PointF[1u + num];
				array[0].X = this._moveTo.X;
				array[0].Y = this._moveTo.Y;
				for (int i = 1; i < array.Length; i++)
				{
					array[i].X = (float)binaryReader.ReadInt16();
					array[i].Y = (float)binaryReader.ReadInt16();
				}
				this._moveTo = new PointF(array[array.Length - 1].X, array[array.Length - 1].Y);
				this.Transform.TransformPoints(array);
				this.DrawLine(array, 0, array.Length);
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void ProcessPolyBezierTo16(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				binaryReader.ReadBytes(16);
				uint num = binaryReader.ReadUInt32();
				PointF[] array = new PointF[num];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].X = (float)binaryReader.ReadInt16();
					array[i].Y = (float)binaryReader.ReadInt16();
				}
				uint num2 = num / 3u;
				PointF[] array2 = new PointF[1u + num2];
				array2[0].X = this._moveTo.X;
				array2[0].Y = this._moveTo.Y;
				for (int i = 1; i < array2.Length; i++)
				{
					array2[i] = array[(i - 1) * 3 + 2];
				}
				this._moveTo = new PointF(array2[array2.Length - 1].X, array2[array2.Length - 1].Y);
				this.Transform.TransformPoints(array2);
				this.DrawLine(array2, 0, array2.Length);
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void ProcessCloseFigure(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				PointF[] array = new PointF[2];
				array[0].X = this._moveTo.X;
				array[0].Y = this._moveTo.Y;
				array[1].X = this._curveOrigin.X;
				array[1].Y = this._curveOrigin.Y;
				this.Transform.TransformPoints(array);
				this.DrawLine(array, 0, array.Length);
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void ProcessMoveToEx(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				this._moveTo = default(PointF);
				this._moveTo.X = (float)binaryReader.ReadInt32();
				this._moveTo.Y = (float)binaryReader.ReadInt32();
				this._curveOrigin = this._moveTo;
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void InternalSelectObject(MetafileParser.EmfStockObject stockObject)
		{
			switch (stockObject)
			{
			case MetafileParser.EmfStockObject.WHITE_BRUSH:
				this._brush = new SolidBrush(Color.White);
				break;
			case MetafileParser.EmfStockObject.LTGRAY_BRUSH:
				this._brush = new SolidBrush(Color.LightGray);
				break;
			case MetafileParser.EmfStockObject.GRAY_BRUSH:
				this._brush = new SolidBrush(Color.Gray);
				break;
			case MetafileParser.EmfStockObject.DKGRAY_BRUSH:
				this._brush = new SolidBrush(Color.DarkGray);
				break;
			case MetafileParser.EmfStockObject.BLACK_BRUSH:
				this._brush = new SolidBrush(Color.Black);
				break;
			case MetafileParser.EmfStockObject.NULL_BRUSH:
				this._brush = null;
				break;
			default:
				if (stockObject == MetafileParser.EmfStockObject.DC_BRUSH)
				{
					throw new NotImplementedException();
				}
				break;
			}
		}

		private void ProcessSelectObject(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				uint num = binaryReader.ReadUInt32();
				MetafileParser.ObjectHandle objectHandle;
				if (num >= MetafileParser.StockObjectMinCode && num <= MetafileParser.StockObjectMaxCode)
				{
					MetafileParser.EmfStockObject stockObject = (MetafileParser.EmfStockObject)(num - MetafileParser.StockObjectMinCode);
					this.InternalSelectObject(stockObject);
				}
				else if (this._objects.TryGetValue(num, out objectHandle))
				{
					if (objectHandle.IsStockObject)
					{
						this.InternalSelectObject(objectHandle.GetStockObject());
					}
					else if (objectHandle.IsBrush)
					{
						this._brush = objectHandle.GetBrush();
					}
				}
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void ProcessCreateBrushIndirect(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				uint key = binaryReader.ReadUInt32();
				MetafileParser.EmfBrushStyle emfBrushStyle = (MetafileParser.EmfBrushStyle)binaryReader.ReadUInt32();
				byte red = binaryReader.ReadByte();
				byte green = binaryReader.ReadByte();
				byte blue = binaryReader.ReadByte();
				byte b = binaryReader.ReadByte();
				Color color = Color.FromArgb((int)red, (int)green, (int)blue);
				uint num = binaryReader.ReadUInt32();
				this._objects.Remove(key);
				switch (emfBrushStyle)
				{
				case MetafileParser.EmfBrushStyle.BS_SOLID:
					this._objects.Add(key, new MetafileParser.ObjectHandle(new SolidBrush(color)));
					break;
				case MetafileParser.EmfBrushStyle.BS_NULL:
					this._objects.Add(key, new MetafileParser.ObjectHandle(MetafileParser.EmfStockObject.NULL_BRUSH));
					break;
				case MetafileParser.EmfBrushStyle.BS_HATCHED:
					throw new NotImplementedException();
				default:
					throw new NotImplementedException();
				}
				Debug.Assert(memoryStream.Position == memoryStream.Length);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void ProcessStrokeAndFillPath(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				binaryReader.ReadBytes(16);
				Debug.Assert(memoryStream.Position == memoryStream.Length);
				this.FillPolygon(this._lineBuffer.GetPoints(), this._brush);
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void ProcessBeginPath(byte[] recordData)
		{
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			try
			{
				memoryStream = new MemoryStream(recordData);
				binaryReader = new BinaryReader(memoryStream);
				Debug.Assert(memoryStream.Position == memoryStream.Length);
				this.CommitLine();
			}
			finally
			{
				if (binaryReader != null)
				{
					binaryReader.Close();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
		}

		private void CommitLine()
		{
			if (!this._lineBuffer.IsEmpty)
			{
				PointF[] points = this._lineBuffer.GetPoints();
				this._lineBuffer.Clear();
				if (points != null)
				{
					for (int i = 0; i < points.Length; i++)
					{
						PointF[] expr_49_cp_0 = points;
						int expr_49_cp_1 = i;
						expr_49_cp_0[expr_49_cp_1].X = expr_49_cp_0[expr_49_cp_1].X + this._zero.X;
						PointF[] expr_68_cp_0 = points;
						int expr_68_cp_1 = i;
						expr_68_cp_0[expr_68_cp_1].Y = expr_68_cp_0[expr_68_cp_1].Y + this._zero.Y;
					}
					this._drawLine(points);
				}
			}
		}

		private void FillPolygon(PointF[] linePoints, Brush fillBrush)
		{
			if (linePoints != null && fillBrush != null)
			{
				for (int i = 0; i < linePoints.Length; i++)
				{
					int expr_21_cp_1 = i;
					linePoints[expr_21_cp_1].X = linePoints[expr_21_cp_1].X + this._zero.X;
					int expr_40_cp_1 = i;
					linePoints[expr_40_cp_1].Y = linePoints[expr_40_cp_1].Y + this._zero.Y;
				}
				this._fillPolygon(linePoints, fillBrush);
			}
		}

		private void DrawLine(PointF[] points, int offset, int count)
		{
			if (!this._lineBuffer.CanAdd(points, offset, count))
			{
				this.CommitLine();
			}
			this._lineBuffer.Add(points, offset, count);
		}

		public void EnumerateMetafile(Stream emf, float unitSize, PointF destination, DrawLineDelegate drawLine, FillPolygonDelegate fillPolygon)
		{
			this.Transform = new Matrix();
			this._drawLine = drawLine;
			this._fillPolygon = fillPolygon;
			this._zero = destination;
			this._lineBuffer = new MetafileParser.LineBuffer(unitSize);
			this._objects = new Dictionary<uint, MetafileParser.ObjectHandle>();
			this._brush = null;
			using (EmfReader emfReader = new EmfReader(emf))
			{
				while (!emfReader.IsEndOfFile)
				{
					EmfUnknownRecord emfUnknownRecord = emfReader.Read() as EmfUnknownRecord;
					if (emfUnknownRecord != null)
					{
						EmfPlusRecordType recordType = emfUnknownRecord.RecordType;
						if (recordType <= EmfPlusRecordType.EmfSetPolyFillMode)
						{
							if (recordType != EmfPlusRecordType.EmfHeader && recordType != EmfPlusRecordType.EmfEof && recordType != EmfPlusRecordType.EmfSetPolyFillMode)
							{
								goto IL_1FE;
							}
						}
						else
						{
							switch (recordType)
							{
							case EmfPlusRecordType.EmfMoveToEx:
								this.ProcessMoveToEx(emfUnknownRecord.Data);
								break;
							case EmfPlusRecordType.EmfSetMetaRgn:
							case EmfPlusRecordType.EmfExcludeClipRect:
							case EmfPlusRecordType.EmfIntersectClipRect:
							case EmfPlusRecordType.EmfScaleViewportExtEx:
							case EmfPlusRecordType.EmfScaleWindowExtEx:
							case EmfPlusRecordType.EmfSetWorldTransform:
								goto IL_1FE;
							case EmfPlusRecordType.EmfSaveDC:
							case EmfPlusRecordType.EmfRestoreDC:
							case EmfPlusRecordType.EmfCreatePen:
							case EmfPlusRecordType.EmfDeleteObject:
								break;
							case EmfPlusRecordType.EmfModifyWorldTransform:
								this.ProcessModifyWorldTransform(emfUnknownRecord.Data);
								break;
							case EmfPlusRecordType.EmfSelectObject:
								this.ProcessSelectObject(emfUnknownRecord.Data);
								break;
							case EmfPlusRecordType.EmfCreateBrushIndirect:
								this.ProcessCreateBrushIndirect(emfUnknownRecord.Data);
								break;
							default:
								switch (recordType)
								{
								case EmfPlusRecordType.EmfSetMiterLimit:
									break;
								case EmfPlusRecordType.EmfBeginPath:
									this.ProcessBeginPath(emfUnknownRecord.Data);
									break;
								case EmfPlusRecordType.EmfEndPath:
									break;
								case EmfPlusRecordType.EmfCloseFigure:
									this.ProcessCloseFigure(emfUnknownRecord.Data);
									break;
								case EmfPlusRecordType.EmfFillPath:
									goto IL_1FE;
								case EmfPlusRecordType.EmfStrokeAndFillPath:
									this.ProcessStrokeAndFillPath(emfUnknownRecord.Data);
									break;
								default:
									switch (recordType)
									{
									case EmfPlusRecordType.EmfPolygon16:
										this.ProcessPolygon16(emfUnknownRecord.Data);
										break;
									case EmfPlusRecordType.EmfPolyline16:
										this.ProcessPolyline16(emfUnknownRecord.Data);
										break;
									case EmfPlusRecordType.EmfPolyBezierTo16:
										this.ProcessPolyBezierTo16(emfUnknownRecord.Data);
										break;
									case EmfPlusRecordType.EmfPolylineTo16:
										this.ProcessPolylineTo16(emfUnknownRecord.Data);
										break;
									case EmfPlusRecordType.EmfPolyPolyline16:
									case EmfPlusRecordType.EmfPolyDraw16:
									case EmfPlusRecordType.EmfCreateMonoBrush:
									case EmfPlusRecordType.EmfCreateDibPatternBrushPt:
									case EmfPlusRecordType.EmfPolyTextOutA:
									case EmfPlusRecordType.EmfPolyTextOutW:
										goto IL_1FE;
									case EmfPlusRecordType.EmfPolyPolygon16:
										this.ProcessPolyPolygon16(emfUnknownRecord.Data);
										break;
									case EmfPlusRecordType.EmfExtCreatePen:
									case EmfPlusRecordType.EmfSetIcmMode:
										break;
									default:
										goto IL_1FE;
									}
									break;
								}
								break;
							}
						}
						continue;
						continue;
						IL_1FE:
						throw new NotImplementedException();
					}
				}
			}
			this.CommitLine();
		}
	}
}
