using System;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace SvgNet.SvgTypes
{
	public class SvgTransform : ICloneable
	{
		private Matrix _m;

		public Matrix Matrix
		{
			get
			{
				return this._m;
			}
			set
			{
				this._m = value;
			}
		}

		public SvgTransform()
		{
			this._m = new Matrix();
		}

		public SvgTransform(string s)
		{
			this.FromString(s);
		}

		public SvgTransform(Matrix m)
		{
			this._m = m;
		}

		public object Clone()
		{
			return new SvgTransform(this._m.Clone());
		}

		public void FromString(string s)
		{
			this._m = new Matrix();
			int num = s.IndexOf("(");
			if (num != -1)
			{
				string text = s.Substring(0, num).Trim();
				int num2 = s.IndexOf(")");
				if (num2 != -1)
				{
					string s2 = s.Substring(num + 1, num2 - num - 1);
					float[] array = SvgNumList.String2Floats(s2);
					if (text.IndexOf("matrix") != -1)
					{
						if (array.Length == 6)
						{
							this._m = new Matrix(array[0], array[1], array[2], array[3], array[4], array[5]);
							return;
						}
					}
					else if (text.IndexOf("translate") != -1)
					{
						if (array.Length == 1)
						{
							this._m.Translate(array[0], 0f);
							return;
						}
						if (array.Length == 2)
						{
							this._m.Translate(array[0], array[1]);
							return;
						}
					}
					else if (text.IndexOf("scale") != -1)
					{
						if (array.Length == 1)
						{
							this._m.Scale(array[0], 0f);
							return;
						}
						if (array.Length == 2)
						{
							this._m.Scale(array[0], array[1]);
							return;
						}
					}
					else if (text.IndexOf("rotate") != -1)
					{
						if (array.Length == 1)
						{
							this._m.Rotate(array[0]);
							return;
						}
						if (array.Length == 3)
						{
							this._m.Translate(array[1], array[2]);
							this._m.Rotate(array[0]);
							this._m.Translate(array[1] * -1f, array[2] * -1f);
							return;
						}
					}
					else if (text.IndexOf("skewX") != -1)
					{
						if (array.Length == 1)
						{
							this._m.Shear(array[0], 0f);
							return;
						}
					}
					else if (text.IndexOf("skewY") != -1)
					{
						if (array.Length == 1)
						{
							this._m.Shear(0f, array[0]);
							return;
						}
					}
				}
			}
			throw new SvgException("Invalid SvgTransformation", s);
		}

		public override string ToString()
		{
			string str = "matrix(";
			float[] elements = this._m.Elements;
			for (int i = 0; i < elements.Length; i++)
			{
				str += elements[i].ToString("F", CultureInfo.InvariantCulture);
				str += " ";
			}
			return str + ")";
		}
	}
}
