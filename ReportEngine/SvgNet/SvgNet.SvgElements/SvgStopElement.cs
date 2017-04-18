using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgStopElement : SvgStyledTransformedElement
	{
		public override string Name
		{
			get
			{
				return "stop";
			}
		}

		public SvgLength Offset
		{
			get
			{
				return (SvgLength)this._atts["offset"];
			}
			set
			{
				this._atts["offset"] = value;
			}
		}

		public SvgStopElement()
		{
		}

		public SvgStopElement(SvgLength num, SvgColor col)
		{
			this.Offset = num;
			base.Style.Set("stop-color", col);
		}
	}
}
