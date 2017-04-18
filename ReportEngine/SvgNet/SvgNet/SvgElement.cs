using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace SvgNet
{
	public class SvgElement
	{
		private class DummyXmlResolver : XmlResolver
		{
			public override ICredentials Credentials
			{
				set
				{
				}
			}

			public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
			{
				return new MemoryStream();
			}
		}

		protected ArrayList _children;

		protected Hashtable _atts;

		protected SvgElement _parent;

		protected static int _idcounter = 0;

		public string Id
		{
			get
			{
				return (string)this._atts["id"];
			}
			set
			{
				this._atts["id"] = value;
			}
		}

		public ArrayList Children
		{
			get
			{
				return this._children;
			}
		}

		public Hashtable Attributes
		{
			get
			{
				return this._atts;
			}
		}

		public SvgElement Parent
		{
			get
			{
				return this._parent;
			}
		}

		public object this[string attname]
		{
			get
			{
				return this._atts[attname];
			}
			set
			{
				this._atts[attname] = value;
			}
		}

		public virtual string Name
		{
			get
			{
				return "?";
			}
		}

		public SvgElement()
		{
			this.Defaults();
		}

		public SvgElement(string id)
		{
			this.Defaults();
			this.Id = id;
		}

		protected void Defaults()
		{
			this._children = new ArrayList();
			this._atts = new Hashtable();
			this.Id = SvgElement._idcounter.ToString();
			SvgElement._idcounter++;
		}

		public virtual void ReadXmlElement(XmlDocument doc, XmlElement el)
		{
			foreach (XmlAttribute xmlAttribute in el.Attributes)
			{
				if (!(xmlAttribute.Name == "xmlns") && !xmlAttribute.Name.Contains(":"))
				{
					this[xmlAttribute.Name] = xmlAttribute.Value;
				}
			}
		}

		public virtual void WriteXmlElements(XmlDocument doc, XmlElement parent)
		{
			XmlElement xmlElement = doc.CreateElement("", this.Name, doc.NamespaceURI);
			foreach (string text in this._atts.Keys)
			{
				xmlElement.SetAttribute(text, doc.NamespaceURI, this._atts[text].ToString());
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

		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"<",
				this.Name,
				" id='",
				this.Id,
				"'/>"
			});
		}

		public string WriteSVGString(bool compressAttributes)
		{
			string internalSubset = "";
			XmlDocument xmlDocument = new XmlDocument();
			XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", null, "yes");
			xmlDocument.AppendChild(xmlDeclaration);
			this.WriteXmlElements(xmlDocument, null);
			xmlDocument.DocumentElement.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
			if (compressAttributes)
			{
				internalSubset = SvgFactory.CompressXML(xmlDocument, xmlDocument.DocumentElement);
			}
			xmlDocument.XmlResolver = new SvgElement.DummyXmlResolver();
			xmlDocument.InsertAfter(xmlDocument.CreateDocumentType("svg", "-//W3C//DTD SVG 1.1//EN", "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd", internalSubset), xmlDeclaration);
			MemoryStream memoryStream = new MemoryStream();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, new UTF8Encoding());
			xmlTextWriter.Formatting = Formatting.None;
			xmlDocument.Save(xmlTextWriter);
			byte[] array = memoryStream.ToArray();
			string @string = Encoding.UTF8.GetString(array, 0, array.Length);
			xmlTextWriter.Close();
			return @string;
		}

		public virtual void AddChild(SvgElement ch)
		{
			if (ch.Parent != null)
			{
				throw new SvgException("Child already has a parent", ch.ToString());
			}
			this._children.Add(ch);
			ch._parent = this;
		}

		public virtual void AddChildren(params SvgElement[] ch)
		{
			for (int i = 0; i < ch.Length; i++)
			{
				SvgElement ch2 = ch[i];
				this.AddChild(ch2);
			}
		}
	}
}
