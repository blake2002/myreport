using System;
using System.Xml;

namespace SvgNet
{
	public class TextNode : SvgElement
	{
		private string _s;

		public override string Name
		{
			get
			{
				return "a text node, not an svg element";
			}
		}

		public string Text
		{
			get
			{
				return this._s;
			}
			set
			{
				this._s = value;
			}
		}

		public TextNode()
		{
		}

		public TextNode(string s)
		{
			this.Text = s;
		}

		public override void AddChild(SvgElement ch)
		{
			throw new SvgException("A TextNode cannot have children");
		}

		public override void AddChildren(params SvgElement[] ch)
		{
			throw new SvgException("A TextNode cannot have children");
		}

		public override void ReadXmlElement(XmlDocument doc, XmlElement el)
		{
			throw new SvgException("TextNode::ReadXmlElement should not be called; the value should be filled in with a string when the XML doc is being read.", "");
		}

		public override void WriteXmlElements(XmlDocument doc, XmlElement parent)
		{
			XmlText newChild = doc.CreateTextNode(this._s);
			if (parent == null)
			{
				doc.AppendChild(newChild);
			}
			else
			{
				parent.AppendChild(newChild);
			}
		}
	}
}
