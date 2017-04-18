using System;
using System.Collections;
using System.Drawing.Drawing2D;

namespace SvgNet.SvgTypes
{
	public class SvgTransformList : ICloneable
	{
		private ArrayList _t = new ArrayList();

		public SvgTransform this[int idx]
		{
			get
			{
				return (SvgTransform)this._t[idx];
			}
			set
			{
				this._t[idx] = value;
			}
		}

		public int Count
		{
			get
			{
				return this._t.Count;
			}
		}

		public SvgTransformList()
		{
		}

		public SvgTransformList(string s)
		{
			this.FromString(s);
		}

		public SvgTransformList(Matrix m)
		{
			SvgTransform value = new SvgTransform(m);
			this._t.Add(value);
		}

		public object Clone()
		{
			return new SvgTransformList(this.ToString());
		}

		public void Add(string trans)
		{
			this._t.Add(new SvgTransform(trans));
		}

		public void Add(Matrix m)
		{
			this._t.Add(new SvgTransform(m));
		}

		public void FromString(string s)
		{
			int num = -1;
			while (true)
			{
				int num2 = s.IndexOf(")", num + 1);
				if (num2 == -1)
				{
					break;
				}
				SvgTransform value = new SvgTransform(s.Substring(num + 1, num2 - num));
				this._t.Add(value);
				num = num2;
			}
		}

		public override string ToString()
		{
			string text = "";
			foreach (SvgTransform svgTransform in this._t)
			{
				text += svgTransform.ToString();
				text += " ";
			}
			return text;
		}

		public static implicit operator SvgTransformList(string s)
		{
			return new SvgTransformList(s);
		}

		public static implicit operator SvgTransformList(Matrix m)
		{
			return new SvgTransformList(m);
		}
	}
}
