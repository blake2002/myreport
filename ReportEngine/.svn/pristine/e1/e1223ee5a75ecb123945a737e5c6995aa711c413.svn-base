using System;

namespace SvgNet.SvgElements
{
	public class SvgDescElement : SvgElement, IElementWithText
	{
		public override string Name
		{
			get
			{
				return "desc";
			}
		}

		public string Text
		{
			get
			{
				return ((TextNode)this._children[0]).Text;
			}
			set
			{
				((TextNode)this._children[0]).Text = value;
			}
		}

		public SvgDescElement()
		{
			TextNode ch = new TextNode("");
			this.AddChild(ch);
		}

		public SvgDescElement(string s)
		{
			TextNode ch = new TextNode(s);
			this.AddChild(ch);
		}
	}
}
