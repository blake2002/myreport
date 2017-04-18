using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgSymbolElement : SvgElement
	{
		public override string Name
		{
			get
			{
				return "symbol";
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
	}
}
