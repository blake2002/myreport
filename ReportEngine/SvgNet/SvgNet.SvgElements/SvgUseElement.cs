using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgUseElement : SvgStyledTransformedElement, IElementWithXRef
	{
		public override string Name
		{
			get
			{
				return "use";
			}
		}

		public SvgLength Width
		{
			get
			{
				return (SvgLength)this._atts["width"];
			}
			set
			{
				this._atts["width"] = value;
			}
		}

		public SvgLength Height
		{
			get
			{
				return (SvgLength)this._atts["height"];
			}
			set
			{
				this._atts["height"] = value;
			}
		}

		public SvgLength X
		{
			get
			{
				return (SvgLength)this._atts["x"];
			}
			set
			{
				this._atts["x"] = value;
			}
		}

		public SvgLength Y
		{
			get
			{
				return (SvgLength)this._atts["y"];
			}
			set
			{
				this._atts["y"] = value;
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

		public SvgUseElement()
		{
		}

		public SvgUseElement(SvgXRef xref)
		{
			this.XRef = xref;
		}

		public SvgUseElement(string href)
		{
			this.Href = href;
		}

		public SvgUseElement(SvgLength x, SvgLength y, SvgXRef xref)
		{
			this.XRef = xref;
			this.X = x;
			this.Y = y;
		}

		public SvgUseElement(SvgLength x, SvgLength y, string href)
		{
			this.Href = href;
			this.X = x;
			this.Y = y;
		}
	}
}
