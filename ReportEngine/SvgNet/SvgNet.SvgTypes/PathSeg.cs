using System;

namespace SvgNet.SvgTypes
{
	public class PathSeg : ICloneable
	{
		public SvgPathSegType _type;

		public bool _abs;

		public float[] _data;

		public float[] Data
		{
			get
			{
				return this._data;
			}
		}

		public SvgPathSegType Type
		{
			get
			{
				return this._type;
			}
		}

		public bool Abs
		{
			get
			{
				return this._abs;
			}
		}

		public string Char
		{
			get
			{
				string result;
				switch (this._type)
				{
				case SvgPathSegType.SVG_SEGTYPE_MOVETO:
					result = (this._abs ? "M" : "m");
					break;
				case SvgPathSegType.SVG_SEGTYPE_CLOSEPATH:
					result = "z";
					break;
				case SvgPathSegType.SVG_SEGTYPE_LINETO:
					result = (this._abs ? "L" : "l");
					break;
				case SvgPathSegType.SVG_SEGTYPE_HLINETO:
					result = (this._abs ? "H" : "h");
					break;
				case SvgPathSegType.SVG_SEGTYPE_VLINETO:
					result = (this._abs ? "V" : "v");
					break;
				case SvgPathSegType.SVG_SEGTYPE_CURVETO:
					result = (this._abs ? "C" : "c");
					break;
				case SvgPathSegType.SVG_SEGTYPE_SMOOTHCURVETO:
					result = (this._abs ? "S" : "s");
					break;
				case SvgPathSegType.SVG_SEGTYPE_BEZIERTO:
					result = (this._abs ? "Q" : "q");
					break;
				case SvgPathSegType.SVG_SEGTYPE_SMOOTHBEZIERTO:
					result = (this._abs ? "T" : "t");
					break;
				case SvgPathSegType.SVG_SEGTYPE_ARCTO:
					result = (this._abs ? "A" : "a");
					break;
				default:
					throw new SvgException("Invalid PathSeg type", this._type.ToString());
				}
				return result;
			}
		}

		public PathSeg(SvgPathSegType t, bool a, float[] arr)
		{
			this._type = t;
			this._abs = a;
			this._data = arr;
		}

		public object Clone()
		{
			return new PathSeg(this._type, this._abs, (float[])this._data.Clone());
		}
	}
}
