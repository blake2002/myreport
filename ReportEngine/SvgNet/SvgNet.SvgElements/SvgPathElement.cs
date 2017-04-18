using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgPathElement : SvgStyledTransformedElement
	{
		public override string Name
		{
			get
			{
				return "path";
			}
		}

		public SvgPath D
		{
			get
			{
				return (SvgPath)this._atts["d"];
			}
			set
			{
				this._atts["d"] = value.ToString();
			}
		}

		public SvgNumber PathLength
		{
			get
			{
				return (SvgNumber)this._atts["pathlength"];
			}
			set
			{
				this._atts["pathlength"] = value;
			}
		}
	}
}
