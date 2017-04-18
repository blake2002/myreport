using SvgNet.SvgTypes;
using System;

namespace SvgNet.SvgElements
{
	public class SvgTextElement : SvgStyledTransformedElement, IElementWithText
	{
		public override string Name
		{
			get
			{
				return "text";
			}
		}

		public SvgLength DX
		{
			get
			{
				return (SvgLength)this._atts["dx"];
			}
			set
			{
				this._atts["dx"] = value;
			}
		}

		public SvgLength DY
		{
			get
			{
				return (SvgLength)this._atts["dy"];
			}
			set
			{
				this._atts["dy"] = value;
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

		public SvgNumList Rotate
		{
			get
			{
				return (SvgNumList)this._atts["rotate"];
			}
			set
			{
				this._atts["rotate"] = value;
			}
		}

		public SvgLength TextLength
		{
			get
			{
				return (SvgLength)this._atts["textLength"];
			}
			set
			{
				this._atts["textLength"] = value;
			}
		}

		public string LengthAdjust
		{
			get
			{
				return (string)this._atts["lengthAdjust"];
			}
			set
			{
				this._atts["lengthAdjust"] = value;
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

		public SvgTextElement()
		{
		}

		public SvgTextElement(string s)
		{
			TextNode ch = new TextNode(s);
			this.AddChild(ch);
		}

		public SvgTextElement(string s, float x, float y)
		{
			TextNode ch = new TextNode(s);
			this.AddChild(ch);
			this.X = x;
			this.Y = y;
		}
	}
}
