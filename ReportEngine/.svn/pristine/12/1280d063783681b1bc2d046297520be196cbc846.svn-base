using System;
using System.Globalization;

namespace SvgNet.SvgTypes
{
	public class SvgLength : ICloneable
	{
		private float _num;

		private SvgLengthType _type;

		public float Value
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		public SvgLengthType Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		public SvgLength(string s)
		{
			this.FromString(s);
		}

		public SvgLength(float f)
		{
			this._num = f;
			this._type = SvgLengthType.SVG_LENGTHTYPE_UNKNOWN;
		}

		public SvgLength(float f, SvgLengthType type)
		{
			this._num = f;
			this._type = type;
		}

		public object Clone()
		{
			return new SvgLength(this._num, this._type);
		}

		public void FromString(string s)
		{
			int num = s.LastIndexOfAny(new char[]
			{
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9'
			});
			if (num != -1)
			{
				this._num = float.Parse(s.Substring(0, num + 1), CultureInfo.InvariantCulture);
				string text = s.Substring(num + 1);
				switch (text)
				{
				case "%":
					this._type = SvgLengthType.SVG_LENGTHTYPE_PERCENTAGE;
					return;
				case "em":
					this._type = SvgLengthType.SVG_LENGTHTYPE_EMS;
					return;
				case "ex":
					this._type = SvgLengthType.SVG_LENGTHTYPE_EXS;
					return;
				case "px":
					this._type = SvgLengthType.SVG_LENGTHTYPE_PX;
					return;
				case "cm":
					this._type = SvgLengthType.SVG_LENGTHTYPE_CM;
					return;
				case "mm":
					this._type = SvgLengthType.SVG_LENGTHTYPE_MM;
					return;
				case "in":
					this._type = SvgLengthType.SVG_LENGTHTYPE_IN;
					return;
				case "pt":
					this._type = SvgLengthType.SVG_LENGTHTYPE_PT;
					return;
				case "pc":
					this._type = SvgLengthType.SVG_LENGTHTYPE_PC;
					return;
				case "":
					this._type = SvgLengthType.SVG_LENGTHTYPE_UNKNOWN;
					return;
				}
				throw new SvgException("Invalid SvgLength", s);
			}
		}

		public override string ToString()
		{
			string text = this._num.ToString("F", CultureInfo.InvariantCulture);
			switch (this._type)
			{
			case SvgLengthType.SVG_LENGTHTYPE_PERCENTAGE:
				text += "%";
				break;
			case SvgLengthType.SVG_LENGTHTYPE_EMS:
				text += "em";
				break;
			case SvgLengthType.SVG_LENGTHTYPE_EXS:
				text += "ex";
				break;
			case SvgLengthType.SVG_LENGTHTYPE_PX:
				text += "px";
				break;
			case SvgLengthType.SVG_LENGTHTYPE_CM:
				text += "cm";
				break;
			case SvgLengthType.SVG_LENGTHTYPE_MM:
				text += "mm";
				break;
			case SvgLengthType.SVG_LENGTHTYPE_IN:
				text += "in";
				break;
			case SvgLengthType.SVG_LENGTHTYPE_PT:
				text += "pt";
				break;
			case SvgLengthType.SVG_LENGTHTYPE_PC:
				text += "pc";
				break;
			}
			return text;
		}

		public static implicit operator SvgLength(string s)
		{
			return new SvgLength(s);
		}

		public static implicit operator SvgLength(float s)
		{
			return new SvgLength(s);
		}
	}
}
