using System;
using System.Collections;
using System.Globalization;

namespace SvgNet.SvgTypes
{
	public class SvgNumList : ICloneable
	{
		private ArrayList _pts = new ArrayList();

		public float this[int idx]
		{
			get
			{
				return (float)this._pts[idx];
			}
			set
			{
				this._pts[idx] = value;
			}
		}

		public int Count
		{
			get
			{
				return this._pts.Count;
			}
		}

		public SvgNumList(string s)
		{
			this.FromString(s);
		}

		public SvgNumList(float[] pts)
		{
			for (int i = 0; i < pts.Length; i++)
			{
				float num = pts[i];
				this._pts.Add(num);
			}
		}

		public object Clone()
		{
			return new SvgNumList((float[])this._pts.ToArray(typeof(float)));
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
				throw new SvgException("Invalid SvgNumList", s);
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

		public static implicit operator SvgNumList(string s)
		{
			return new SvgNumList(s);
		}

		public static implicit operator SvgNumList(float[] f)
		{
			return new SvgNumList(f);
		}

		public static float[] String2Floats(string s)
		{
			float[] result;
			try
			{
				string[] array = s.Split(new char[]
				{
					',',
					' ',
					'\t',
					'\r',
					'\n'
				});
				ArrayList arrayList = new ArrayList();
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					if (text != "")
					{
						text.Trim();
						arrayList.Add(float.Parse(text, CultureInfo.InvariantCulture));
					}
				}
				result = (float[])arrayList.ToArray(typeof(float));
			}
			catch (Exception)
			{
				throw new SvgException("Invalid number list", s);
			}
			return result;
		}
	}
}
