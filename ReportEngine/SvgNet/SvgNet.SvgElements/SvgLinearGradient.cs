using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgLinearGradient : SvgStyledTransformedElement
	{
		public override string Name
		{
			get
			{
				return "linearGradient";
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

		public string GradientUnits
		{
			get
			{
				return (string)this._atts["gradientUnits"];
			}
			set
			{
				this._atts["gradientUnits"] = value;
			}
		}

		public SvgTransformList GradientTransform
		{
			get
			{
				return (SvgTransformList)this._atts["gradientTransform"];
			}
			set
			{
				this._atts["gradientTransform"] = value;
			}
		}

		public string SpreadMethod
		{
			get
			{
				return (string)this._atts["spreadMethod"];
			}
			set
			{
				this._atts["spreadMethod"] = value;
			}
		}

		public SvgLinearGradient()
		{
		}

		public SvgLinearGradient(SvgLength x1, SvgLength y1, SvgLength x2, SvgLength y2)
		{
			this.X1 = x1;
			this.Y1 = y1;
			this.X2 = x2;
			this.Y2 = y2;
		}
	}
}
