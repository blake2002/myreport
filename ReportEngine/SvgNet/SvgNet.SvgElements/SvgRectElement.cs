using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgRectElement : SvgStyledTransformedElement
	{
		public override string Name
		{
			get
			{
				return "rect";
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

		public SvgRectElement()
		{
		}

		public SvgRectElement(SvgLength x, SvgLength y, SvgLength w, SvgLength h)
		{
			this.X = x;
			this.Y = y;
			this.Width = w;
			this.Height = h;
		}
	}
}
