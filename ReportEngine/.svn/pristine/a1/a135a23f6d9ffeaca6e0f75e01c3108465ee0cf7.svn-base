using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgSvgElement : SvgElement
	{
		public override string Name
		{
			get
			{
				return "svg";
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

		public string Version
		{
			get
			{
				return (string)this._atts["version"];
			}
			set
			{
				this._atts["version"] = value;
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

		public SvgSvgElement()
		{
		}

		public SvgSvgElement(SvgLength width, SvgLength height)
		{
			this.Width = width;
			this.Height = height;
		}

		public SvgSvgElement(SvgLength width, SvgLength height, SvgNumList vport)
		{
			this.Width = width;
			this.Height = height;
			this.ViewBox = vport;
		}

		public SvgSvgElement(SvgLength x, SvgLength y, SvgLength width, SvgLength height, SvgNumList vport)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
			this.ViewBox = vport;
		}
	}
}
