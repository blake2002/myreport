using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgEllipseElement : SvgStyledTransformedElement
	{
		public override string Name
		{
			get
			{
				return "ellipse";
			}
		}

		public SvgLength CX
		{
			get
			{
				return (SvgLength)this._atts["cx"];
			}
			set
			{
				this._atts["cx"] = value;
			}
		}

		public SvgLength CY
		{
			get
			{
				return (SvgLength)this._atts["cy"];
			}
			set
			{
				this._atts["cy"] = value;
			}
		}

		public SvgLength RX
		{
			get
			{
				return (SvgLength)this._atts["rx"];
			}
			set
			{
				this._atts["rx"] = value;
			}
		}

		public SvgLength RY
		{
			get
			{
				return (SvgLength)this._atts["ry"];
			}
			set
			{
				this._atts["ry"] = value;
			}
		}

		public SvgEllipseElement()
		{
		}

		public SvgEllipseElement(SvgLength cx, SvgLength cy, SvgLength rx, SvgLength ry)
		{
			this.CX = cx;
			this.CY = cy;
			this.RX = rx;
			this.RY = ry;
		}
	}
}
