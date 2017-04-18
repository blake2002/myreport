using System;

namespace SvgNet.SvgTypes
{
	public class SvgUriReference : ICloneable
	{
		private string _href;

		public string Href
		{
			get
			{
				return this._href;
			}
			set
			{
				this._href = value;
			}
		}

		public SvgUriReference()
		{
		}

		public SvgUriReference(string href)
		{
			this._href = href;
		}

		public SvgUriReference(SvgElement target)
		{
			this._href = "#" + target.Id;
			if (target.Id == "")
			{
				throw new SvgException("Uri Reference cannot refer to an element with no id.", target.ToString());
			}
		}

		public object Clone()
		{
			return new SvgUriReference(this._href);
		}

		public override string ToString()
		{
			return "url(" + this._href + ")";
		}
	}
}
