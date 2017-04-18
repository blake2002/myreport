using System;

namespace SvgNet
{
	public class SvgException : Exception
	{
		private string _msg;

		private string _ctx;

		public string Msg
		{
			get
			{
				return this._msg;
			}
		}

		public string Ctx
		{
			get
			{
				return this._ctx;
			}
		}

		public SvgException(string msg, string ctx)
		{
			this._msg = msg;
			this._ctx = ctx;
		}

		public SvgException(string msg)
		{
			this._msg = msg;
			this._ctx = "";
		}
	}
}
