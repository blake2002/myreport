using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgAElement : SvgStyledTransformedElement, IElementWithXRef
	{
		public override string Name
		{
			get
			{
				return "a";
			}
		}

		public SvgXRef XRef
		{
			get
			{
				return new SvgXRef(this);
			}
			set
			{
				value.WriteToElement(this);
			}
		}

		public string Href
		{
			get
			{
				return (string)this._atts["xlink:href"];
			}
			set
			{
				this._atts["xlink:href"] = value;
			}
		}

		public string Target
		{
			get
			{
				return (string)this._atts["target"];
			}
			set
			{
				this._atts["target"] = value;
			}
		}

		public SvgAElement()
		{
		}

		public SvgAElement(string href)
		{
			this.Href = href;
		}
	}
}
