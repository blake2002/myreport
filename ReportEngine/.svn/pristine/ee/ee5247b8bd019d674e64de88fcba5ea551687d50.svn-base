using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SvgNet.SvgTypes
{
	public class SvgColor : ICloneable
	{
		private Color _col;

		private string _original_string;

		private static Hashtable _stdcols = new Hashtable();

		public Color Color
		{
			get
			{
				return this._col;
			}
			set
			{
				this._col = value;
			}
		}

		public SvgColor(string s)
		{
			this.FromString(s);
		}

		public SvgColor(Color c)
		{
			this._col = c;
		}

		public SvgColor(Color c, string s)
		{
			this._col = c;
			this._original_string = s;
		}

		public object Clone()
		{
			return new SvgColor(this._col, this._original_string);
		}

		public void FromString(string s)
		{
			this._original_string = s;
			if (s.StartsWith("#"))
			{
				this.FromHexString(s);
			}
			else
			{
				Regex regex = new Regex("[rgbRGB]{3}");
				if (regex.Match(s).Success)
				{
					this.FromRGBString(s);
				}
				else
				{
					this._col = Color.FromName(s);
					if (this._col.A == 0)
					{
						throw new SvgException("Invalid SvgColor", s);
					}
				}
			}
		}

		public override string ToString()
		{
			string result;
			if (this._original_string != null)
			{
				result = this._original_string;
			}
			else
			{
				string text = "rgb(";
				text += this._col.R.ToString();
				text += ",";
				text += this._col.G.ToString();
				text += ",";
				text += this._col.B.ToString();
				text += ")";
				result = text;
			}
			return result;
		}

		public static implicit operator SvgColor(Color c)
		{
			return new SvgColor(c);
		}

		public static implicit operator SvgColor(string s)
		{
			return new SvgColor(s);
		}

		private void FromHexString(string s)
		{
			s = s.Substring(1);
			if (s.Length == 3)
			{
				int num = int.Parse(s.Substring(0, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				int num2 = int.Parse(s.Substring(1, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				int num3 = int.Parse(s.Substring(2, 1), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				num += num * 16;
				num2 += num2 * 16;
				num3 += num3 * 16;
				this._col = Color.FromArgb(num, num2, num3);
			}
			else
			{
				if (s.Length != 6)
				{
					throw new SvgException("Invalid SvgColor", s);
				}
				int num = int.Parse(s.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				int num2 = int.Parse(s.Substring(2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				int num3 = int.Parse(s.Substring(4, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
				this._col = Color.FromArgb(num, num2, num3);
			}
		}

		private void FromRGBString(string s)
		{
			Regex regex = new Regex("[rgbRGB ]+\\( *(?<r>\\d+)[, ]+(?<g>\\d+)[, ]+(?<b>\\d+) *\\)");
			Match match = regex.Match(s);
			if (match.Success)
			{
				int red = int.Parse(match.Groups["r"].Captures[0].Value, CultureInfo.InvariantCulture);
				int green = int.Parse(match.Groups["g"].Captures[0].Value, CultureInfo.InvariantCulture);
				int blue = int.Parse(match.Groups["b"].Captures[0].Value, CultureInfo.InvariantCulture);
				this._col = Color.FromArgb(red, green, blue);
			}
			else
			{
				regex = new Regex("[rgbRGB ]+\\( *(?<r>\\d+)%[, ]+(?<g>\\d+)%[, ]+(?<b>\\d+)% *\\)");
				match = regex.Match(s);
				if (!match.Success)
				{
					throw new SvgException("Invalid SvgColor", s);
				}
				int red = int.Parse(match.Groups["r"].Captures[0].Value, CultureInfo.InvariantCulture) * 255 / 100;
				int green = int.Parse(match.Groups["g"].Captures[0].Value, CultureInfo.InvariantCulture) * 255 / 100;
				int blue = int.Parse(match.Groups["b"].Captures[0].Value, CultureInfo.InvariantCulture) * 255 / 100;
				this._col = Color.FromArgb(red, green, blue);
			}
		}
	}
}
