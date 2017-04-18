using SvgNet.SvgElements;
using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml;

namespace SvgNet
{
	public class SvgFactory
	{
		private struct EntitySingleton
		{
			public XmlElement Element;

			public string AttributeName;
		}

		private static Hashtable _elementNameDictionary;

		private SvgFactory()
		{
		}

		public static SvgElement LoadFromXML(XmlDocument doc, XmlElement el)
		{
			if (el == null)
			{
				foreach (XmlNode xmlNode in doc.ChildNodes)
				{
					if (xmlNode.GetType() == typeof(XmlElement))
					{
						el = (XmlElement)xmlNode;
						break;
					}
				}
			}
			SvgElement result;
			if (el == null)
			{
				result = null;
			}
			else
			{
				if (SvgFactory._elementNameDictionary == null)
				{
					SvgFactory.BuildElementNameDictionary();
				}
				Type type = (Type)SvgFactory._elementNameDictionary[el.Name];
				SvgElement svgElement = (SvgElement)type.GetConstructor(new Type[0]).Invoke(new object[0]);
				SvgFactory.RecLoadFromXML(svgElement, doc, el);
				result = svgElement;
			}
			return result;
		}

		private static void RecLoadFromXML(SvgElement e, XmlDocument doc, XmlElement el)
		{
			e.ReadXmlElement(doc, el);
			foreach (XmlNode xmlNode in el.ChildNodes)
			{
				if (xmlNode.GetType() == typeof(XmlElement))
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					Type type = (Type)SvgFactory._elementNameDictionary[xmlElement.Name];
					SvgElement svgElement;
					if (type == null)
					{
						svgElement = new SvgGenericElement(xmlElement.Name);
					}
					else
					{
						svgElement = (SvgElement)type.GetConstructor(new Type[0]).Invoke(new object[0]);
					}
					e.AddChild(svgElement);
					SvgFactory.RecLoadFromXML(svgElement, doc, xmlElement);
				}
				else if (xmlNode.GetType() == typeof(XmlText))
				{
					XmlText xmlText = (XmlText)xmlNode;
					TextNode ch = new TextNode(xmlText.InnerText);
					e.AddChild(ch);
				}
			}
		}

		private static void BuildElementNameDictionary()
		{
			SvgFactory._elementNameDictionary = new Hashtable();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Type[] exportedTypes = executingAssembly.GetExportedTypes();
			Type[] array = exportedTypes;
			for (int i = 0; i < array.Length; i++)
			{
				Type type = array[i];
				if (type.IsSubclassOf(typeof(SvgElement)))
				{
					SvgElement svgElement = (SvgElement)type.GetConstructor(new Type[0]).Invoke(new object[0]);
					if (svgElement.Name != "?")
					{
						SvgFactory._elementNameDictionary[svgElement.Name] = svgElement.GetType();
					}
				}
			}
		}

		public static string CompressXML(XmlDocument doc, XmlElement el)
		{
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			int num = 0;
			SvgFactory.RecCompXML(hashtable, hashtable2, doc, el, ref num);
			foreach (DictionaryEntry dictionaryEntry in hashtable2)
			{
				string text = (string)dictionaryEntry.Key;
				SvgFactory.EntitySingleton entitySingleton = (SvgFactory.EntitySingleton)dictionaryEntry.Value;
				entitySingleton.Element.RemoveAttribute(entitySingleton.AttributeName);
				XmlAttribute xmlAttribute = doc.CreateAttribute(entitySingleton.AttributeName);
				xmlAttribute.Value = text;
				entitySingleton.Element.SetAttributeNode(xmlAttribute);
				hashtable.Remove(text);
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (hashtable.Count > 0)
			{
				stringBuilder.Append("\n");
				foreach (string text2 in hashtable.Keys)
				{
					stringBuilder.Append("<!ENTITY ");
					stringBuilder.Append(hashtable[text2]);
					stringBuilder.Append(" '");
					stringBuilder.Append(text2.Replace("%", "&#37;"));
					stringBuilder.Append("'>");
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		private static void RecCompXML(Hashtable entities, Hashtable singletons, XmlDocument doc, XmlElement el, ref int idx)
		{
			ArrayList arrayList = new ArrayList();
			foreach (XmlAttribute xmlAttribute in el.Attributes)
			{
				arrayList.Add(xmlAttribute.Name);
			}
			foreach (string text in arrayList)
			{
				string value = el.Attributes[text].Value;
				if (value.Length > 30)
				{
					string text2;
					if (entities[value] == null)
					{
						idx++;
						text2 = "E" + idx.ToString();
						entities[value] = text2;
						singletons[value] = new SvgFactory.EntitySingleton
						{
							Element = el,
							AttributeName = text
						};
					}
					else
					{
						text2 = (string)entities[value];
						singletons.Remove(value);
					}
					XmlAttribute xmlAttribute2 = doc.CreateAttribute(text);
					xmlAttribute2.AppendChild(doc.CreateEntityReference(text2));
					el.SetAttributeNode(xmlAttribute2);
				}
			}
			foreach (XmlNode xmlNode in el.ChildNodes)
			{
				if (xmlNode.GetType() == typeof(XmlElement))
				{
					SvgFactory.RecCompXML(entities, singletons, doc, (XmlElement)xmlNode, ref idx);
				}
			}
		}

		public static SvgElement CloneElement(SvgElement el)
		{
			SvgElement svgElement = (SvgElement)el.GetType().GetConstructor(new Type[0]).Invoke(new object[0]);
			foreach (string attname in el.Attributes.Keys)
			{
				object obj = el[attname];
				if (typeof(ICloneable).IsInstanceOfType(obj))
				{
					svgElement[attname] = ((ICloneable)obj).Clone();
				}
				else
				{
					svgElement[attname] = obj;
				}
			}
			foreach (SvgElement el2 in el.Children)
			{
				svgElement.AddChild(SvgFactory.CloneElement(el2));
			}
			return svgElement;
		}
	}
}
