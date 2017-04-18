using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace SvgNet.SvgTypes
{
	public class SvgStyle : ICloneable
	{
		private Hashtable _styles = new Hashtable();

		public ICollection Keys
		{
			get
			{
				return this._styles.Keys;
			}
		}

		public object this[string attname]
		{
			get
			{
				return this._styles[attname];
			}
			set
			{
				this._styles[attname] = value;
			}
		}

		public SvgStyle()
		{
		}

		public SvgStyle(string s)
		{
			this.FromString(s);
		}

		public object Clone()
		{
			SvgStyle lhs = new SvgStyle();
			return lhs + this;
		}

		public SvgStyle(Pen pen)
		{
			SvgColor val = new SvgColor(((SolidBrush)pen.Brush).Color);
			this.Set("stroke", val);
			this.Set("stroke-width", pen.Width);
			this.Set("fill", "none");
			switch (pen.EndCap)
			{
			case LineCap.Flat:
				this.Set("stroke-linecap", "butt");
				break;
			case LineCap.Square:
				this.Set("stroke-linecap", "square");
				break;
			case LineCap.Round:
				this.Set("stroke-linecap", "round");
				break;
			}
			switch (pen.LineJoin)
			{
			case LineJoin.Miter:
				this.Set("stroke-linejoin", "miter");
				break;
			case LineJoin.Bevel:
				this.Set("stroke-linejoin", "bevel");
				break;
			case LineJoin.Round:
				this.Set("stroke-linejoin", "round");
				break;
			}
			this.Set("stroke-miterlimit", pen.MiterLimit / 2f + 4f);
			float[] array = null;
			switch (pen.DashStyle)
			{
			case DashStyle.Dash:
				array = new float[]
				{
					3f,
					1f
				};
				break;
			case DashStyle.Dot:
				array = new float[]
				{
					1f,
					1f
				};
				break;
			case DashStyle.DashDot:
				array = new float[]
				{
					3f,
					1f,
					1f,
					1f
				};
				break;
			case DashStyle.DashDotDot:
				array = new float[]
				{
					3f,
					1f,
					1f,
					1f,
					1f
				};
				break;
			case DashStyle.Custom:
				array = pen.DashPattern;
				break;
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] *= pen.Width;
				}
				this.Set("stroke-dasharray", new SvgNumList(array));
			}
			this.Set("opacity", (float)pen.Color.A / 255f);
		}

		public SvgStyle(SolidBrush brush)
		{
			SvgColor val = new SvgColor(brush.Color);
			this.Set("fill", val);
			this.Set("stroke", "none");
			this.Set("opacity", (float)brush.Color.A / 255f);
		}

		public SvgStyle(Font font)
		{
			this.Set("font-family", font.FontFamily.Name);
			if (font.Bold)
			{
				this.Set("font-weight", "bolder");
			}
			if (font.Italic)
			{
				this.Set("font-style", "italic");
			}
			if (font.Underline)
			{
				this.Set("text-decoration", "underline");
			}
			this.Set("font-size", font.SizeInPoints.ToString("F", CultureInfo.InvariantCulture) + "pt");
		}

		public void Set(string key, object val)
		{
			if (val == null || val.ToString() == "")
			{
				this._styles.Remove(key);
			}
			else
			{
				this._styles[key] = val;
			}
		}

		public object Get(string key)
		{
			return this._styles[key];
		}

		public void FromString(string s)
		{
			try
			{
				string[] array = s.Split(new char[]
				{
					';'
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					string[] array3 = text.Split(new char[]
					{
						':'
					});
					if (array3.Length == 2)
					{
						this.Set(array3[0].Trim(), array3[1].Trim());
					}
				}
			}
			catch (Exception)
			{
				throw new SvgException("Invalid style string", s);
			}
		}

		public override string ToString()
		{
			string text = "";
			foreach (string text2 in this._styles.Keys)
			{
				string str = this._styles[text2].ToString();
				text += text2;
				text += ":";
				text += str;
				text += ";";
			}
			return text;
		}

		public static implicit operator SvgStyle(string s)
		{
			return new SvgStyle(s);
		}

		public static SvgStyle operator +(SvgStyle lhs, SvgStyle rhs)
		{
			SvgStyle svgStyle = new SvgStyle();
			foreach (string attname in lhs._styles.Keys)
			{
				object obj = lhs[attname];
				if (typeof(ICloneable).IsInstanceOfType(obj))
				{
					svgStyle[attname] = ((ICloneable)obj).Clone();
				}
				else
				{
					svgStyle[attname] = obj;
				}
			}
			foreach (string attname in rhs._styles.Keys)
			{
				object obj = rhs[attname];
				if (typeof(ICloneable).IsInstanceOfType(obj))
				{
					svgStyle[attname] = ((ICloneable)obj).Clone();
				}
				else
				{
					svgStyle[attname] = obj;
				}
			}
			return svgStyle;
		}
	}
}
