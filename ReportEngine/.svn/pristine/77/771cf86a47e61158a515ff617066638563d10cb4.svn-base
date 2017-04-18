using System;
using System.Globalization;

namespace SvgNet.SvgTypes
{
	public class SvgNumber : ICloneable
	{
		private float _num;

		public SvgNumber(string s)
		{
			this.FromString(s);
		}

		public SvgNumber(int n)
		{
			this._num = (float)n;
		}

		public SvgNumber(float n)
		{
			this._num = n;
		}

		public object Clone()
		{
			return new SvgNumber(this._num);
		}

		public void FromString(string s)
		{
			try
			{
				this._num = float.Parse(s, CultureInfo.InvariantCulture);
			}
			catch
			{
				throw new SvgException("Invalid SvgNumber", s);
			}
		}

		public override string ToString()
		{
			return this._num.ToString("F", CultureInfo.InvariantCulture);
		}

		public static implicit operator SvgNumber(string s)
		{
			return new SvgNumber(s);
		}

		public static implicit operator SvgNumber(int n)
		{
			return new SvgNumber(n);
		}

		public static implicit operator SvgNumber(float n)
		{
			return new SvgNumber(n);
		}
	}
}
