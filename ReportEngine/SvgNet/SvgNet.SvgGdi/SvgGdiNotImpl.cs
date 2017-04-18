using System;

namespace SvgNet.SvgGdi
{
	public class SvgGdiNotImpl : Exception
	{
		private string _method;

		public string Method
		{
			get
			{
				return this._method;
			}
		}

		public SvgGdiNotImpl(string method)
		{
			this._method = method;
		}
	}
}
