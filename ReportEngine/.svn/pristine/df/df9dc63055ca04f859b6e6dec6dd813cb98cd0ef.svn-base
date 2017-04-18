using System;
using System.Globalization;

namespace SvgNet.SvgTypes
{
	public class SvgAngle : ICloneable
	{
		private float _num;

		private SvgAngleType _type;

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

		public SvgAngleType Type
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

		public SvgAngle(string s)
		{
			this.FromString(s);
		}

		public SvgAngle(float num, SvgAngleType type)
		{
			this._num = num;
			this._type = type;
		}

		public object Clone()
		{
			return new SvgAngle(this._num, this._type);
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
				this._num = (float)int.Parse(s.Substring(0, num + 1), CultureInfo.InvariantCulture);
				string text = s.Substring(num + 1);
				if (text != null)
				{
					if (text == "grad")
					{
						this._type = SvgAngleType.SVG_ANGLETYPE_GRAD;
						return;
					}
					if (text == "rad")
					{
						this._type = SvgAngleType.SVG_ANGLETYPE_RAD;
						return;
					}
					if (text == "deg")
					{
						this._type = SvgAngleType.SVG_ANGLETYPE_DEG;
						return;
					}
					if (text == "")
					{
						this._type = SvgAngleType.SVG_ANGLETYPE_UNSPECIFIED;
						return;
					}
				}
				throw new SvgException("Invalid SvgAngle", s);
			}
		}

		public override string ToString()
		{
			string text = this._num.ToString("F", CultureInfo.InvariantCulture);
			switch (this._type)
			{
			case SvgAngleType.SVG_ANGLETYPE_UNSPECIFIED:
			case SvgAngleType.SVG_ANGLETYPE_DEG:
				text += "deg";
				break;
			case SvgAngleType.SVG_ANGLETYPE_RAD:
				text += "rad";
				break;
			case SvgAngleType.SVG_ANGLETYPE_GRAD:
				text += "grad";
				break;
			}
			return text;
		}

		public static implicit operator SvgAngle(string s)
		{
			return new SvgAngle(s);
		}
	}
}
