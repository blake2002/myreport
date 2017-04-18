using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgFilterElement : SvgElement
	{
		public override string Name
		{
			get
			{
				return "filter";
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

		public string FilterRes
		{
			get
			{
				return (string)this._atts["filterRes"];
			}
			set
			{
				this._atts["filterRes"] = value;
			}
		}

		public string FilterUnits
		{
			get
			{
				return (string)this._atts["filterUnits"];
			}
			set
			{
				this._atts["filterUnits"] = value;
			}
		}

		public string PrimitiveUnits
		{
			get
			{
				return (string)this._atts["primitiveUnits"];
			}
			set
			{
				this._atts["primitiveUnits"] = value;
			}
		}

		public SvgFilterElement()
		{
		}

		public SvgFilterElement(SvgLength x, SvgLength y, SvgLength w, SvgLength h)
		{
			this.X = x;
			this.Y = y;
			this.Width = w;
			this.Height = h;
		}
	}
}
