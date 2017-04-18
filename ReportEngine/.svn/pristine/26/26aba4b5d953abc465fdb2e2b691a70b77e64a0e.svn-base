using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgPatternElement : SvgStyledTransformedElement
	{
		public override string Name
		{
			get
			{
				return "pattern";
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

		public SvgNumList ViewBox
		{
			get
			{
				return (SvgNumList)this._atts["viewbox"];
			}
			set
			{
				this._atts["viewbox"] = value;
			}
		}

		public string PreserveAspectRatio
		{
			get
			{
				return (string)this._atts["preserveAspectRatio"];
			}
			set
			{
				this._atts["preserveAspectRatio"] = value;
			}
		}

		public string PatternUnits
		{
			get
			{
				return (string)this._atts["patternUnits"];
			}
			set
			{
				this._atts["patternUnits"] = value;
			}
		}

		public string PatternContentUnits
		{
			get
			{
				return (string)this._atts["patternContentUnits"];
			}
			set
			{
				this._atts["patternContentUnits"] = value;
			}
		}

		public SvgTransformList PatternTransform
		{
			get
			{
				return (SvgTransformList)this._atts["patternTransform"];
			}
			set
			{
				this._atts["patternTransform"] = value;
			}
		}

		public SvgPatternElement()
		{
		}

		public SvgPatternElement(SvgLength width, SvgLength height, SvgNumList vport)
		{
			this.Width = width;
			this.Height = height;
			this.ViewBox = vport;
		}

		public SvgPatternElement(SvgLength x, SvgLength y, SvgLength width, SvgLength height, SvgNumList vport)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
			this.ViewBox = vport;
		}
	}
}
