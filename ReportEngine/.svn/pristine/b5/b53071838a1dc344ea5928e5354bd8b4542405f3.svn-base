using System;

namespace SvgNet.SvgElements
{
	public class SvgTitleElement : SvgElement, IElementWithText
	{
		public override string Name
		{
			get
			{
				return "title";
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

		public SvgTitleElement()
		{
			TextNode ch = new TextNode("");
			this.AddChild(ch);
		}

		public SvgTitleElement(string s)
		{
			TextNode ch = new TextNode(s);
			this.AddChild(ch);
		}
	}
}
