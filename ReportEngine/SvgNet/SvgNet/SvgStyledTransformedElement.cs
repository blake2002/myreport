using SvgNet.SvgTypes;
using System;
using System.Xml;

namespace SvgNet
{
	public class SvgStyledTransformedElement : SvgElement
	{
		public SvgStyle Style
		{
			get
			{
				object obj = this._atts["style"];
				SvgStyle result;
				if (obj == null)
				{
					SvgStyle svgStyle = new SvgStyle();
					this._atts["style"] = svgStyle;
					result = svgStyle;
				}
				else if (obj.GetType() == typeof(SvgStyle))
				{
					result = (SvgStyle)obj;
				}
				else
				{
					SvgStyle svgStyle = new SvgStyle(obj.ToString());
					this._atts["style"] = svgStyle;
					result = svgStyle;
				}
				return result;
			}
			set
			{
				this._atts["style"] = value;
			}
		}

		public SvgTransformList Transform
		{
			get
			{
				object obj = this._atts["transform"];
				SvgTransformList result;
				if (obj == null)
				{
					SvgTransformList svgTransformList = new SvgTransformList();
					this._atts["transform"] = svgTransformList;
					result = svgTransformList;
				}
				else if (obj.GetType() == typeof(SvgTransformList))
				{
					result = (SvgTransformList)obj;
				}
				else
				{
					SvgTransformList svgTransformList = new SvgTransformList(obj.ToString());
					this._atts["transform"] = svgTransformList;
					result = svgTransformList;
				}
				return result;
			}
			set
			{
				this._atts["transform"] = value;
			}
		}

		public SvgStyledTransformedElement()
		{
		}

		public SvgStyledTransformedElement(string id) : base(id)
		{
		}

		public override void ReadXmlElement(XmlDocument doc, XmlElement el)
		{
			foreach (XmlAttribute xmlAttribute in el.Attributes)
			{
				if (xmlAttribute.Name == "style")
				{
					this.Style = new SvgStyle(xmlAttribute.Value);
				}
				else if (xmlAttribute.Name == "transform")
				{
					this.Transform = new SvgTransformList(xmlAttribute.Value);
				}
				else
				{
					base[xmlAttribute.Name] = xmlAttribute.Value;
				}
			}
		}

		public override void WriteXmlElements(XmlDocument doc, XmlElement parent)
		{
			XmlElement xmlElement = doc.CreateElement("", this.Name, doc.NamespaceURI);
			foreach (string text in this._atts.Keys)
			{
				if (text == "style")
				{
					this.WriteStyle(doc, xmlElement, this._atts[text]);
				}
				else if (text == "transform")
				{
					this.WriteTransform(doc, xmlElement, this._atts[text]);
				}
				else
				{
					xmlElement.SetAttribute(text, doc.NamespaceURI, this._atts[text].ToString());
				}
			}
			foreach (SvgElement svgElement in this._children)
			{
				svgElement.WriteXmlElements(doc, xmlElement);
			}
			if (parent == null)
			{
				doc.AppendChild(xmlElement);
			}
			else
			{
				parent.AppendChild(xmlElement);
			}
		}

		private void WriteStyle(XmlDocument doc, XmlElement me, object o)
		{
			if (o.GetType() != typeof(SvgStyle))
			{
				me.SetAttribute("style", doc.NamespaceURI, o.ToString());
			}
			else
			{
				SvgStyle svgStyle = (SvgStyle)o;
				me.SetAttribute("style", doc.NamespaceURI, svgStyle.ToString());
				doc.CreateEntityReference("pingu");
			}
		}

		private void WriteTransform(XmlDocument doc, XmlElement me, object o)
		{
			me.SetAttribute("transform", doc.NamespaceURI, o.ToString());
		}
	}
}
