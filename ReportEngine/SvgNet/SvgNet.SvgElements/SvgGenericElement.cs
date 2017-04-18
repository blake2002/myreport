using System;

namespace SvgNet.SvgElements
{
	public class SvgGenericElement : SvgElement
	{
		private string _name;

		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		public SvgGenericElement()
		{
			this._name = "generic svg node";
		}

		public SvgGenericElement(string name)
		{
			this._name = name;
		}
	}
}
