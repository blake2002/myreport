using System;
using System.Collections;
using System.Globalization;

namespace SvgNet.SvgTypes
{
	public class SvgPath : ICloneable
	{
		private ArrayList _path;

		public PathSeg this[int idx]
		{
			get
			{
				return (PathSeg)this._path[idx];
			}
			set
			{
				this._path[idx] = value;
			}
		}

		public int Count
		{
			get
			{
				return this._path.Count;
			}
		}

		public SvgPath(string s)
		{
			this.FromString(s);
		}

		public object Clone()
		{
			return new SvgPath(this.ToString());
		}

		public void FromString(string s)
		{
			string[] array = s.Split(new char[]
			{
				' ',
				',',
				'\t',
				'\r',
				'\n'
			});
			int num = 0;
			SvgPathSegType svgPathSegType = SvgPathSegType.SVG_SEGTYPE_UNKNOWN;
			bool a = false;
			int i = 0;
			this._path = new ArrayList();
			while (i < array.Length)
			{
				if (array[i] == "")
				{
					i++;
				}
				else
				{
					if (char.IsLetter(array[i][0]))
					{
						char c = array[i][0];
						if (c == 'M' || c == 'm')
						{
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_MOVETO;
							a = (c == 'M');
							num = 2;
						}
						else if (c == 'Z' || c == 'z')
						{
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_CLOSEPATH;
							num = 0;
						}
						else if (c == 'L' || c == 'l')
						{
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_LINETO;
							a = (c == 'L');
							num = 2;
						}
						else if (c == 'H' || c == 'h')
						{
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_HLINETO;
							a = (c == 'H');
							num = 1;
						}
						else if (c == 'V' || c == 'v')
						{
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_VLINETO;
							a = (c == 'V');
							num = 1;
						}
						else if (c == 'C' || c == 'c')
						{
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_CURVETO;
							a = (c == 'C');
							num = 6;
						}
						else if (c == 'S' || c == 's')
						{
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_SMOOTHCURVETO;
							a = (c == 'S');
							num = 4;
						}
						else if (c == 'Q' || c == 'q')
						{
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_BEZIERTO;
							a = (c == 'Q');
							num = 4;
						}
						else if (c == 'T' || c == 't')
						{
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_SMOOTHBEZIERTO;
							a = (c == 'T');
							num = 2;
						}
						else
						{
							if (c != 'A' && c != 'a')
							{
								throw new SvgException("Invalid SvgPath", s);
							}
							svgPathSegType = SvgPathSegType.SVG_SEGTYPE_ARCTO;
							a = (c == 'A');
							num = 7;
						}
						array[i] = array[i].Substring(1);
						if (array[i] == "")
						{
							i++;
						}
					}
					if (svgPathSegType == SvgPathSegType.SVG_SEGTYPE_UNKNOWN)
					{
						throw new SvgException("Invalid SvgPath", s);
					}
					float[] array2 = new float[num];
					for (int j = 0; j < num; j++)
					{
						array2[j] = float.Parse(array[i + j], CultureInfo.InvariantCulture);
					}
					PathSeg value = new PathSeg(svgPathSegType, a, array2);
					this._path.Add(value);
					i += num;
				}
			}
		}

		public override string ToString()
		{
			PathSeg pathSeg = null;
			string text = "";
			foreach (PathSeg pathSeg2 in this._path)
			{
				if (pathSeg == null || pathSeg.Type != pathSeg2.Type || pathSeg.Abs != pathSeg2.Abs)
				{
					text += pathSeg2.Char;
					text += " ";
				}
				float[] data = pathSeg2.Data;
				for (int i = 0; i < data.Length; i++)
				{
					text += data[i].ToString();
					text += " ";
				}
				pathSeg = pathSeg2;
			}
			return text;
		}

		public static implicit operator SvgPath(string s)
		{
			return new SvgPath(s);
		}
	}
}
