using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgLineElement : SvgStyledTransformedElement
	{
		public override string Name
		{
			get
			{
				return "line";
			}
		}

		public SvgLength X1
		{
			get
			{
				return (SvgLength)this._atts["x1"];
			}
			set
			{
				this._atts["x1"] = value;
			}
		}

		public SvgLength Y1
		{
			get
			{
				return (SvgLength)this._atts["y1"];
			}
			set
			{
				this._atts["y1"] = value;
			}
		}

		public SvgLength X2
		{
			get
			{
				return (SvgLength)this._atts["x2"];
			}
			set
			{
				this._atts["x2"] = value;
			}
		}

		public SvgLength Y2
		{
			get
			{
				return (SvgLength)this._atts["y2"];
			}
			set
			{
				this._atts["y2"] = value;
			}
		}

		public SvgLineElement()
		{
		}

		public SvgLineElement(SvgLength x1, SvgLength y1, SvgLength x2, SvgLength y2)
		{
			this.X1 = x1;
			this.Y1 = y1;
			this.X2 = x2;
			this.Y2 = y2;
		}
	}
}
