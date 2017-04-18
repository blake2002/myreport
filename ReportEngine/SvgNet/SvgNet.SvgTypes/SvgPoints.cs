using System;
using System.Collections;
using System.Drawing;
using System.Globalization;

namespace SvgNet.SvgTypes
{
	public class SvgPoints : ICloneable
	{
		private ArrayList _pts = new ArrayList();

		public SvgPoints(string s)
		{
			this.FromString(s);
		}

		public SvgPoints(PointF[] pts)
		{
			for (int i = 0; i < pts.Length; i++)
			{
				PointF pointF = pts[i];
				this._pts.Add(pointF.X);
				this._pts.Add(pointF.Y);
			}
		}

		public object Clone()
		{
			return new SvgPoints((PointF[])this._pts.ToArray(typeof(PointF)));
		}

		public SvgPoints(float[] pts)
		{
			if (pts.Length % 2 != 0)
			{
				throw new SvgException("Invalid SvgPoints", pts.ToString());
			}
			for (int i = 0; i < pts.Length; i++)
			{
				float num = pts[i];
				this._pts.Add(num);
			}
		}

		public void FromString(string s)
		{
			try
			{
				float[] array = SvgNumList.String2Floats(s);
				float[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					float num = array2[i];
					this._pts.Add(num);
				}
			}
			catch (Exception)
			{
				throw new SvgException("Invalid SvgPoints", s);
			}
			if (this._pts.Count % 2 != 0)
			{
				throw new SvgException("Invalid SvgPoints", s);
			}
		}

		public override string ToString()
		{
			string text = "";
			foreach (float num in this._pts)
			{
				text += num.ToString("F", CultureInfo.InvariantCulture);
				text += " ";
			}
			return text;
		}

		public static implicit operator SvgPoints(string s)
		{
			return new SvgPoints(s);
		}

		public static implicit operator SvgPoints(PointF[] pts)
		{
			return new SvgPoints(pts);
		}
	}
}
