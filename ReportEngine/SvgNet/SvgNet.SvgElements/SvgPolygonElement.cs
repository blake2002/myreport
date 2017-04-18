using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgPolygonElement : SvgStyledTransformedElement
	{
		public override string Name
		{
			get
			{
				return "polygon";
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

		public SvgPolygonElement()
		{
		}

		public SvgPolygonElement(SvgPoints points)
		{
			this.Points = points;
		}
	}
}
