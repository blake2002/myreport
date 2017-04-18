using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgPolylineElement : SvgStyledTransformedElement
	{
		public override string Name
		{
			get
			{
				return "polyline";
			}
		}

		public SvgPoints Points
		{
			get
			{
				return (SvgPoints)this._atts["points"];
			}
			set
			{
				this._atts["points"] = value;
			}
		}

		public SvgPolylineElement()
		{
		}

		public SvgPolylineElement(SvgPoints points)
		{
			this.Points = points;
		}
	}
}
