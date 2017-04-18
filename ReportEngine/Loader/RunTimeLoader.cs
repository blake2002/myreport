using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Runtime.Serialization;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using PMS.Libraries.ToolControls.PMSPublicInfo;

namespace Loader
{
	public class RunTimeLoader : IDisposable
	{
		//todo:qiuleilei 20161215
		private readonly string[] ExceptTypes = {
			"PMS.Libraries.ToolControls.PMSReport.ReportViewerToolBar"
		};
		private IEventBindingService _EventBindingService;

		public IEventBindingService EventBindingService {
			get { return _EventBindingService; }
			set { _EventBindingService = value; }
		}

		private Control _BaseForm;

		public Control BaseForm {
			get { return _BaseForm; }
			set { _BaseForm = value; }
		}

		private IContainer _ComponentContainer;

		public IContainer ComponentContainer {
			get { return _ComponentContainer; }
			set { _ComponentContainer = value; }
		}

		public IDictionary<string, object> RemoteVariables {
			get { return _RemoteVariables; }
		}

		private string _strFilePath;

		public RunTimeLoader (string strFilePath)
		{
			_strFilePath = strFilePath;
		}

		public RunTimeLoader ()
		{
		}

		XmlDocument _FormXml = null;

		public RunTimeLoader (XmlDocument doc)
		{
			_FormXml = doc;
		}

		public void Dispose ()
		{
			//todo:qiuleilei 20161215
			if (null != _RemoteVariables) {
				foreach (var kv in _RemoteVariables) {
					var o = kv.Value as Component;
					if (o != null) {
						o.Dispose ();
					}
				}
				_RemoteVariables.Clear ();
				_RemoteVariables = null;
			}
			if (null != _ComponentContainer) {
				_ComponentContainer.Dispose ();
				_ComponentContainer = null;
			}
			if (null != _BaseForm) {
				_BaseForm.Dispose ();
				_BaseForm = null;
			}
		}

		public void LoadToBaseForm ()
		{
			XmlDocument xmlDocument;
			ArrayList errors = new ArrayList ();
			ReadFile (_strFilePath, errors, out xmlDocument);
		}

		//默认为一般模式ModeFlag = 0
		//ModeFlag = 1 报表drpt阅读器模式
		//ModeFlag = 2 表单阅读器模式
		private int ModeFlag = 0;

		public void LoadToRuntimeForm ()
		{
			ModeFlag = 1;
			XmlDocument xmlDocument;
			ArrayList errors = new ArrayList ();
			ReadFile (_FormXml, errors, out xmlDocument);
		}

		public Form GetRuntimeForm ()
		{
			ModeFlag = 1;
			XmlDocument xmlDocument;
			ArrayList errors = new ArrayList ();
			ReadFormFile (_FormXml, errors, out xmlDocument);
			return _ReadForm;
		}

		/// Simple helper method that returns true if the given type converter supports
		/// two-way conversion of the given type.
		private bool GetConversionSupported (TypeConverter converter, Type conversionType)
		{
			return (converter.CanConvertFrom (conversionType) && converter.CanConvertTo (conversionType));
		}

		///  Reads an Event node and binds the event.
		private void ReadEvent (XmlNode childNode, object instance, ArrayList errors)
		{
			return;

			IEventBindingService bindings = EventBindingService;
			if (bindings == null) {
				errors.Add ("Unable to contact event binding service so we can't bind any events");
				return;
			}

			XmlAttribute nameAttr = childNode.Attributes ["name"];
			if (nameAttr == null) {
				errors.Add ("No event name");
				return;
			}

			EventDescriptor evt = TypeDescriptor.GetEvents (instance) [nameAttr.Value];
			if (evt == null) {
				errors.Add (string.Format ("Event {0} does not exist on {1}", nameAttr.Value, instance.GetType ().FullName));
				return;
			}

			PropertyDescriptor prop = bindings.GetEventProperty (evt);
			Debug.Assert (prop != null, "Bad event binding service");

			object value;
			if (ReadValue (childNode, prop.Converter, errors, out value)) {
				// ReadValue succeeded.  Fill in the property value.
				//
				try {
					prop.SetValue (instance, value);
				} catch (Exception ex) {
					errors.Add (ex.Message);
				}
			}

			//XmlAttribute methodAttr = childNode.Attributes["method"];
			//if (methodAttr == null || methodAttr.Value == null || methodAttr.Value.Length == 0)
			//{ 
			//    errors.Add(string.Format("Event {0} has no method bound to it", nameAttr.Value));
			//    return;
			//}
			//
			//try
			//{
			//    prop.SetValue(instance, methodAttr.Value);
			//}
			//catch (Exception ex)
			//{
			//    errors.Add(ex.Message);
			//}
		}

		private IDictionary<string, object> _RemoteVariables = new Dictionary<string, object> ();

		private XmlDocument CurrentDoc = null;

		/// This method is used to parse the given file.  Before calling this 
		/// method the host member variable must be setup.  This method will
		/// create a data set, read the data set from the XML stored in the
		/// file, and then walk through the data set and create components
		/// stored within it.  The data set is stored in the persistedData
		/// member variable upon return.
		/// 
		/// This method never throws exceptions.  It will set the successful
		/// ref parameter to false when there are catastrophic errors it can't
		/// resolve (like being unable to parse the XML).  All error exceptions
		/// are added to the errors array list, including minor errors.
		private string ReadFile (string fileName, ArrayList errors, out XmlDocument document)
		{
			_RemoteVariables.Clear ();

			string baseClass = null;

			// Anything unexpected is a fatal error.
			//
			try {
				// The main form and items in the component tray will be at the
				// same level, so we have to create a higher super-root in order
				// to construct our XmlDocument.
				//
				StreamReader sr = new StreamReader (fileName);
				string cleandown = sr.ReadToEnd ();
				cleandown = "<DOCUMENT_ELEMENT>" + cleandown + "</DOCUMENT_ELEMENT>";
				XmlDocument doc = new XmlDocument ();
				doc.LoadXml (cleandown);

				//XmlDocument doc = new XmlDocument();
				//doc.Load(fileName);
				//doc.InnerXml = "<DOCUMENT_ELEMENT>" + doc.InnerXml + "</DOCUMENT_ELEMENT>";

				CurrentDoc = doc;

				int nodeIndex = 0;
				// Now, walk through the document's elements.
				//
				foreach (XmlNode node in doc.DocumentElement.ChildNodes) {
					if (nodeIndex++ == 0) {
						foreach (XmlNode node1 in doc.DocumentElement.ChildNodes[0]) {
							if (node1.Name.Equals ("Object")) {
								ReadObject (node1, errors);
							}
						}
					} else {
						if (node.Name.Equals ("Object")) {
							object ob = ReadObject (node, errors);
							if (ob != null) {
								if (ComponentContainer != null)
								if (ob is Component) {
									ComponentContainer.Add ((Component)ob);
									string name = node.Attributes ["name"].Value;// SelectSingleNode("//@name").InnerText;
									if (!_RemoteVariables.Keys.Contains (name))
										_RemoteVariables.Add (name, ob);
								}
							}
						} else {
							errors.Add (string.Format ("Node type {0} is not allowed here.", node.Name));
						}
					}
				}

				document = doc;

				//sr.Close();
			} catch (Exception ex) {
				document = null;
				errors.Add (ex);
				MessageBox.Show (ex.Message);
			}

			return baseClass;
		}

		private string ReadFile (XmlDocument doc, ArrayList errors, out XmlDocument document)
		{
			_RemoteVariables.Clear ();

			string baseClass = null;

			// Anything unexpected is a fatal error.
			//
			try {
				// The main form and items in the component tray will be at the
				// same level, so we have to create a higher super-root in order
				// to construct our XmlDocument.
				//
				//StreamReader sr = new StreamReader(fileName);
				//string cleandown = sr.ReadToEnd();
				//cleandown = "<DOCUMENT_ELEMENT>" + cleandown + "</DOCUMENT_ELEMENT>";
				//doc.InnerXml = "<DOCUMENT_ELEMENT>" + doc.InnerXml + "</DOCUMENT_ELEMENT>";
				//doc.LoadXml(cleandown);

				CurrentDoc = doc;

				int nodeIndex = 0;
				// Now, walk through the document's elements.
				//
				foreach (XmlNode node in doc.DocumentElement.ChildNodes) {
					if (nodeIndex++ == 0) {
						foreach (XmlNode node1 in doc.DocumentElement.ChildNodes[0]) {
							if (node1.Name.Equals ("Object")) {
								ReadObject (node1, errors);
							}
						}
					} else {
						if (node.Name.Equals ("Object")) {
							object ob = ReadObject (node, errors);
							if (ob != null) {
								if (ComponentContainer != null)
								if (ob is Component) {
									ComponentContainer.Add ((Component)ob);
									string name = node.Attributes ["name"].Value;// SelectSingleNode("//@name").InnerText;
									if (!_RemoteVariables.Keys.Contains (name))
										_RemoteVariables.Add (name, ob);
								}
							}
						} else {
							errors.Add (string.Format ("Node type {0} is not allowed here.", node.Name));
						}
					}
				}

				document = doc;

				//sr.Close();
			} catch (Exception ex) {
				document = null;
				errors.Add (ex);
				MessageBox.Show (ex.Message);
			}

			return baseClass;
		}

		private string ReadFormFile (XmlDocument doc, ArrayList errors, out XmlDocument document)
		{
			_RemoteVariables.Clear ();

			string baseClass = null;

			// Anything unexpected is a fatal error.
			//
			try {
				CurrentDoc = doc;

				// Now, walk through the document's elements.
				//
				foreach (XmlNode node in doc.DocumentElement.ChildNodes) {
					if (baseClass == null) {
						baseClass = node.Attributes ["name"].Value;
					} else {
						if (node.Name.Equals ("Object")) {
							ReadRefrence (node, node.Attributes ["name"].Value, errors);
						} else {
							errors.Add (string.Format ("Node type {0} is not allowed here.", node.Name));
						}
						continue;
					}
                    
					if (node.Name.Equals ("Object")) {
						object ob = ReadObject (node, errors);
						if (ob != null) {
							if (ComponentContainer != null)
							if (ob is Component) {
								ComponentContainer.Add ((Component)ob);
								string name = node.Attributes ["name"].Value;// SelectSingleNode("//@name").InnerText;
								if (!_RemoteVariables.Keys.Contains (name))
									_RemoteVariables.Add (name, ob);
							}
						}
					} else {
						errors.Add (string.Format ("Node type {0} is not allowed here.", node.Name));
					}
                    
				}

				document = doc;

				//sr.Close();
			} catch (Exception ex) {
				document = null;
				errors.Add (ex);
				MessageBox.Show (ex.Message);
			}

			return baseClass;
		}

		private object ReadInstanceDescriptor (XmlNode node, ArrayList errors)
		{
			try {

				// First, need to deserialize the member
				//
				XmlAttribute memberAttr = node.Attributes ["member"];
				if (memberAttr == null) {
					errors.Add ("No member attribute on instance descriptor");
					return null;
				}

				byte[] data = Convert.FromBase64String (memberAttr.Value);
				BinaryFormatter formatter = new BinaryFormatter ();
				MemoryStream stream = new MemoryStream (data);
				MemberInfo mi = (MemberInfo)formatter.Deserialize (stream);
				object[] args = null;

				// Check to see if this member needs arguments.  If so, gather
				// them from the XML.
				if (mi is MethodBase) {
					ParameterInfo[] paramInfos = ((MethodBase)mi).GetParameters ();
					args = new object[paramInfos.Length];
					int idx = 0;

					foreach (XmlNode child in node.ChildNodes) {
						if (child.Name.Equals ("Argument")) {
							object value;
							if (!ReadValue (child, TypeDescriptor.GetConverter (paramInfos [idx].ParameterType), errors, out value)) {
								return null;
							}

							args [idx++] = value;
						}
					}

					if (idx != paramInfos.Length) {
						errors.Add (string.Format ("Member {0} requires {1} arguments, not {2}.", mi.Name, args.Length, idx));
						return null;
					}
				}

				InstanceDescriptor id = new InstanceDescriptor (mi, args);
				object instance = id.Invoke ();

				// Ok, we have our object.  Now, check to see if there are any properties, and if there are, 
				// set them.
				//
				foreach (XmlNode prop in node.ChildNodes) {
					if (prop.Name.Equals ("Property")) {
						ReadProperty (prop, instance, errors);
					}
				}

				stream.Close ();

				return instance;
			} catch (System.Exception ex) {
				return null;
			}
		}

		public Control LoadControl (XmlDocument doc)
		{
			if (null == doc)
				return null;
			if (null == doc.FirstChild)
				return null;
			XmlNode controlNode = doc.FirstChild.FirstChild;
			ArrayList al = new ArrayList ();
			return ReadObject (controlNode, al) as Control;
		}

		Form _ReadForm = null;

		/// Reads the "Object" tags. This returns an instance of the
		/// newly created object. Returns null if there was an error.
		private object ReadObject (XmlNode node, ArrayList errors)
		{
			try {

				XmlAttribute typeAttr = node.Attributes ["type"];
				if (typeAttr == null) {
					errors.Add ("<Object> tag is missing required type attribute");
					return null;
				}

				typeAttr.Value = ProcessPublicKeyToken (typeAttr.Value);
				//todo:qiuleilei 20161215
				if (CurrentPrjInfo.CurrentEnvironment == MESEnvironment.MESReportServer) {
					bool flag = false;
					foreach (var t in ExceptTypes) {
						if (typeAttr.Value.Contains (t)) {
							flag = true;
							break;

						}
					}
					if (flag) {
						return null;
					}
				}
				//----
				Type type = Type.GetType (typeAttr.Value);
				if (type == null) {
					ToolboxLibrary.ToolboxItems.Mode = ModeFlag;
					ToolboxLibrary.ToolboxItems tollBoxItems = new ToolboxLibrary.ToolboxItems ();

					if (ModeFlag == 0) {
						#region 自定义控件的创建
						foreach (Type tp in ToolboxLibrary.ToolboxItems.userControlsToolTypes) {
							if (typeAttr.Value.StartsWith (tp.FullName)) {
								type = tp;
								break;
							}
						}
						#endregion
					} else if (ModeFlag == 1) {
						#region 报表控件的创建
						foreach (Type tp in ToolboxLibrary.ToolboxItems.reportControlsToolTypes) {
							if (typeAttr.Value.StartsWith (tp.FullName)) {
								type = tp;
								break;
							}
						}
						#endregion
					} else if (ModeFlag == 2) {
						#region 表单控件的创建
						foreach (Type tp in ToolboxLibrary.ToolboxItems.frmControlsToolTypes) {
							if (typeAttr.Value.StartsWith (tp.FullName)) {
								type = tp;
								break;
							}
						}
						#endregion
					}

					if (type == null) {
						errors.Add (string.Format ("Type {0} could not be loaded.", typeAttr.Value));
						return null;
					}
				}

				// This can be null if there is no name for the object.
				//
				XmlAttribute nameAttr = node.Attributes ["name"];
				object instance;

				//if (typeof(IComponent).IsAssignableFrom(type))
				//{
				//    if (nameAttr == null)
				//    {
				//        instance = Activator.CreateComponent(type);
				//    }
				//    else
				//    {
				//        instance = Activator.CreateComponent(type, nameAttr.Value);
				//    }
				//}
				//else
				{
					instance = Activator.CreateInstance (type);
				}
				if (nameAttr.Value == "Form1") {
					_ReadForm = instance as Form;
				} else if (nameAttr.Value != "Form1") {
                    
					if (ToolboxLibrary.ToolboxItems.CustomControlType.Count > 0) {
						if (null != ToolboxLibrary.ToolboxItems.PmsSheetName && ToolboxLibrary.ToolboxItems.CustomControlType.ContainsKey (ToolboxLibrary.ToolboxItems.PmsSheetName)) {
							//PmsSheetCtrlType
							if (type == ToolboxLibrary.ToolboxItems.CustomControlType [ToolboxLibrary.ToolboxItems.PmsSheetName]) {
								PropertyInfo RunMode = instance.GetType ().GetProperty ("RunMode");
								RunMode.SetValue (instance, 1, null);

								//MethodInfo SetColumnInfo = instance.GetType().GetMethod("SetColumnInfo", new Type[0]);
								//SetColumnInfo.Invoke(instance, new object[0]);
							}
						}

						if (null != ToolboxLibrary.ToolboxItems.PmsOrgSheetName && ToolboxLibrary.ToolboxItems.CustomControlType.ContainsKey (ToolboxLibrary.ToolboxItems.PmsOrgSheetName)) {
							//PmsOrgSheetType
							if (type == ToolboxLibrary.ToolboxItems.CustomControlType [ToolboxLibrary.ToolboxItems.PmsOrgSheetName]) {
								PropertyInfo RunMode = instance.GetType ().GetProperty ("RunMode");
								RunMode.SetValue (instance, 1, null);

								//MethodInfo SetColumnInfo = instance.GetType().GetMethod("SetColumnInfo", new Type[0]);
								//SetColumnInfo.Invoke(instance, new object[0]);
							}
						}

						if (null != ToolboxLibrary.ToolboxItems.PmsOrgChartName && ToolboxLibrary.ToolboxItems.CustomControlType.ContainsKey (ToolboxLibrary.ToolboxItems.PmsOrgChartName)) {
							//PmsOrgChartType
							if (type == ToolboxLibrary.ToolboxItems.CustomControlType [ToolboxLibrary.ToolboxItems.PmsOrgChartName]) {
								PropertyInfo RunMode = instance.GetType ().GetProperty ("RunMode");
								RunMode.SetValue (instance, 1, null);

								//MethodInfo SetColumnInfo = instance.GetType().GetMethod("SetColumnInfo", new Type[0]);
								//SetColumnInfo.Invoke(instance, new object[0]);
							}
						}
					}
				}

				// Got an object, now we must process it.  Check to see if this tag
				// offers a child collection for us to add children to.
				//
				XmlAttribute childAttr = node.Attributes ["children"];
				IList childList = null;
				if (childAttr != null) {
					PropertyDescriptor childProp = TypeDescriptor.GetProperties (instance) [childAttr.Value];
					if (childProp == null) {
						errors.Add (string.Format ("The children attribute lists {0} as the child collection but this is not a property on {1}", childAttr.Value, instance.GetType ().FullName));
					} else {
						childList = childProp.GetValue (instance) as IList;
						if (childList == null) {
							errors.Add (string.Format ("The property {0} was found but did not return a valid IList", childProp.Name));
						}
					}
				}

				// Now, walk the rest of the tags under this element.
				//
				foreach (XmlNode childNode in node.ChildNodes) {
					if (childNode.Name.Equals ("Object")) {
						// Another object.  In this case, create the object, and
						// parent it to ours using the children property.  If there
						// is no children property, bail out now.
						if (childAttr == null) {
							errors.Add ("Child object found but there is no children attribute");
							continue;
						}

						// no sense doing this if there was an error getting the property.  We've already reported the
						// error above.
						if (childList != null) {
							object childInstance = ReadObject (childNode, errors);
							childList.Add (childInstance);
						}
					} else if (childNode.Name.Equals ("Property")) {
						// A property.  Ask the property to parse itself.
						//
						ReadProperty (childNode, instance, errors);
					} else if (childNode.Name.Equals ("Event")) {
						// An event.  Ask the event to parse itself.
						//
						ReadEvent (childNode, instance, errors);
					}
				}

				if (nameAttr.Value != "Form1") {
					// 只加入一级object
					if (node.ParentNode.ParentNode.Name == "DOCUMENT_ELEMENT") {
						BaseForm.Controls.Add ((Control)instance);
						//if (!_RemoteVariables.ContainsKey(((Control)instance).Name))
						//    _RemoteVariables.Add(((Control)instance).Name, (Control)instance);
					}
					if (instance is Control) {
						if (!_RemoteVariables.ContainsKey (((Control)instance).Name))
							_RemoteVariables.Add (((Control)instance).Name, (Control)instance);
					} else if (instance is Component) {
						if (!_RemoteVariables.ContainsKey (nameAttr.Value))
							_RemoteVariables.Add (nameAttr.Value, (Component)instance);
					}
					//else if (node.ParentNode.Name == "DOCUMENT_ELEMENT")
					//{
					//    // 与Form同一级控件
					//    if (!IsReferenceObExisted(instance))
					//    {
					//        // 且未被其他控件引用
					//        //BaseForm.Container.Add((Component)instance);
					//        if (!_RemoteVariables.ContainsKey(((Component)instance).ToString()))
					//            _RemoteVariables.Add(((Component)instance).ToString(), (Component)instance);
					//    }
					//}
				}

				return instance;
			} catch (System.Exception ex) {
				return null;
			}
		}

		// 如果是强命名的，且强命名key为PMSKey.snk，既PublicKeyToken=6f26a2b4b031fc89的则处理为不强命名的既PublicKeyToken=null
		private string ProcessPublicKeyToken (string strValue)
		{
			if (null == strValue)
				return null;
			//return strValue;
			return strValue.Replace ("PublicKeyToken=6f26a2b4b031fc89", "PublicKeyToken=null");
		}

		/// Parses the given XML node and sets the resulting property value.
		private void ReadProperty (XmlNode node, object instance, ArrayList errors)
		{
			try {

				XmlAttribute nameAttr = node.Attributes ["name"];
				if (nameAttr == null) {
					errors.Add ("Property has no name");
					return;
				}

				PropertyDescriptor prop = TypeDescriptor.GetProperties (instance) [nameAttr.Value];
				if (prop == null) {
					errors.Add (string.Format ("Property {0} does not exist on {1}", nameAttr.Value, instance.GetType ().FullName));
					return;
				}

				// Get the type of this property.  We have three options:
				// 1.  A normal read/write property.
				// 2.  A "Content" property.
				// 3.  A collection.
				//
				bool isContent = prop.Attributes.Contains (DesignerSerializationVisibilityAttribute.Content);

				if (isContent) {
					object value = prop.GetValue (instance);

					// Handle the case of a content property that is a collection.
					//
					if (value is IList) {
						foreach (XmlNode child in node.ChildNodes) {
							if (child.Name.Equals ("Item")) {
								object item;
								XmlAttribute typeAttr = child.Attributes ["type"];
								if (typeAttr == null) {
									errors.Add ("Item has no type attribute");
									continue;
								}

								Type type = Type.GetType (typeAttr.Value);
								if (type == null) {
									errors.Add (string.Format ("Item type {0} could not be found.", typeAttr.Value));
									continue;
								}

								if (ReadValue (child, TypeDescriptor.GetConverter (type), errors, out item)) {
									try {
										((IList)value).Add (item);
									} catch (Exception ex) {
										errors.Add (ex.Message);
									}
								}
							} else {
								errors.Add (string.Format ("Only Item elements are allowed in collections, not {0} elements.", child.Name));
							}
						}
					} else {
						// Handle the case of a content property that consists of child properties.
						//
						foreach (XmlNode child in node.ChildNodes) {
							if (child.Name.Equals ("Property")) {
								ReadProperty (child, value, errors);
							} else {
								errors.Add (string.Format ("Only Property elements are allowed in content properties, not {0} elements.", child.Name));
							}
						}
					}
				} else {
					object value;
					if (ReadValue (node, prop.Converter, errors, out value)) {
						// ReadValue succeeded.  Fill in the property value.
						//
						try {
							prop.SetValue (instance, value);
						} catch (Exception ex) {
							errors.Add (ex.Message);
						}
					}
				}
			} catch (System.Exception ex) {

			}
		}

		/// Reads the "Refrence" tags. This returns an refrence instance of the
		/// newly created object. Returns null if there was an error.
		private object ReadRefrence (XmlNode node, string RefreceName, ArrayList errors)
		{
			try {

				XmlAttribute typeAttr = node.Attributes ["type"];
				if (typeAttr == null) {
					errors.Add ("<Object> tag is missing required type attribute");
					return null;
				}

				typeAttr.Value = ProcessPublicKeyToken (typeAttr.Value);

				Type type = ResolveType.GetType (typeAttr.Value);

				if (type == null) {
					#region 自定义控件的创建
					foreach (Type tp in ToolboxLibrary.ToolboxItems.userControlsToolTypes) {
						if (typeAttr.Value.StartsWith (tp.FullName)) {
							type = tp;
							break;
						}
					}
					#endregion

					if (type == null) {
						errors.Add (string.Format ("Type {0} could not be loaded.", typeAttr.Value));
						return null;
					}
				}

				// This can be null if there is no name for the object.
				//
				XmlAttribute nameAttr = node.Attributes ["name"];

				object instance = null;

				//if (typeof(IComponent).IsAssignableFrom(type))
				//{
				//    if (nameAttr == null)
				//    {
				//        instance = host.CreateComponent(type);
				//    }
				//    else
				//    {
				//        bool finded = false;
				//        foreach (Component ic in host.Container.Components)
				//        {
				//            if (ic.GetType() == type && ic.ToString().StartsWith(nameAttr.Value))
				//            {
				//                finded = true;
				//                instance = ic;
				//                break;
				//            }
				//        }
				//        if (!finded)
				//            instance = host.CreateComponent(type, nameAttr.Value);
				//    }
				//}
				//else
				{
					if (node.Attributes ["type"].Value == typeof(System.Windows.Forms.Form).AssemblyQualifiedName) {
						instance = this._BaseForm;
						return instance;
					}

					foreach (Control cl in _BaseForm.Controls) {
						if (cl.GetType () == type && cl.Name == nameAttr.Value) {
							instance = cl;
							return instance;
						}
					}

					instance = Activator.CreateInstance (type);
				}

				if (RefreceName != null)
				if (AddToReferenceObDictionary (RefreceName, instance) == false)
					return GetRefrecedObject (RefreceName);

				// Got an object, now we must process it.  Check to see if this tag
				// offers a child collection for us to add children to.
				//
				XmlAttribute childAttr = node.Attributes ["children"];
				IList childList = null;
				if (childAttr != null && instance != null) {
					PropertyDescriptor childProp = TypeDescriptor.GetProperties (instance) [childAttr.Value];
					if (childProp == null) {
						errors.Add (string.Format ("The children attribute lists {0} as the child collection but this is not a property on {1}", childAttr.Value, instance.GetType ().FullName));
					} else {
						childList = childProp.GetValue (instance) as IList;
						if (childList == null) {
							errors.Add (string.Format ("The property {0} was found but did not return a valid IList", childProp.Name));
						}
					}
				}

				// Now, walk the rest of the tags under this element.
				//
				foreach (XmlNode childNode in node.ChildNodes) {
					if (childNode.Name.Equals ("Object")) {
						// Another object.  In this case, create the object, and
						// parent it to ours using the children property.  If there
						// is no children property, bail out now.
						if (childAttr == null) {
							errors.Add ("Child object found but there is no children attribute");
							continue;
						}

						// no sense doing this if there was an error getting the property.  We've already reported the
						// error above.
						if (childList != null) {
							object childInstance = ReadObject (childNode, errors);
							childList.Add (childInstance);
						}
					} else if (childNode.Name.Equals ("Property")) {
						// A property.  Ask the property to parse itself.
						//
						ReadProperty (childNode, instance, errors);
					} else if (childNode.Name.Equals ("Event")) {
						// An event.  Ask the event to parse itself.
						//
						ReadEvent (childNode, instance, errors);
					}
				}

				return instance;
			} catch (System.Exception ex) {
				System.Diagnostics.Debug.WriteLine (ex.Message);
				errors.Add (string.Format ("msg:{0},stack:{1}", ex.Message, ex.StackTrace));
				return null;
			}
		}

		private Dictionary<string, object> _ReferenceObCreatingDictionary = new Dictionary<string, object> ();

		private bool AddToReferenceObDictionary (string obName, object ob)
		{
			if (!_ReferenceObCreatingDictionary.ContainsKey (obName)) {
				_ReferenceObCreatingDictionary.Add (obName, ob);
				return true;
			}
			return false;
		}

		private bool IsReferenceObExisted (string obName)
		{
			return _ReferenceObCreatingDictionary.ContainsKey (obName);
		}

		private object GetRefrecedObject (string obName)
		{
			object ob = null;
			_ReferenceObCreatingDictionary.TryGetValue (obName, out ob);
			return ob;
		}

		private List<string> _ReferenceObCreatingNameList = new List<string> ();

		private bool AddToReferenceObCreatingList (string obName)
		{
			if (!_ReferenceObCreatingNameList.Contains (obName)) {
				_ReferenceObCreatingNameList.Add (obName);
				return true;
			}
			return false;
		}

		private bool IsReferenceObCreatingExisted (string obName)
		{
			return _ReferenceObCreatingNameList.Contains (obName);
		}

		private object ReadRefrence (string RefreceName, ArrayList errors)
		{
			object value = null;
			// Anything unexpected is a fatal error.
			//
			try {
				if (CurrentDoc == null)
					return null;

				// Now, walk through the document's elements.
				//
				foreach (XmlNode node in CurrentDoc.DocumentElement.ChildNodes) {
					if (node.Attributes ["type"].Value == typeof(System.Windows.Forms.Form).AssemblyQualifiedName) {
						if (node.Attributes ["name"].Value == RefreceName) {
							value = this._BaseForm;
							break;
						}
						continue;
					}
					if (node.Name.Equals ("Object")) {
						if (node.Attributes ["name"].Value == RefreceName) {
							//if (!IsReferenceObCreatingExisted(RefreceName))
							{
								//AddToReferenceObCreatingList(RefreceName);
								value = ReadRefrence (node, RefreceName, errors);
							}
							//else
							//value = GetRefrecedObject(RefreceName);
                            
							break;
						}
					} else {
						value = null;
						errors.Add (string.Format ("Node type {0} is not allowed here.", node.Name));
					}
				}
			} catch (Exception ex) {
				value = null;
				errors.Add (ex);
			}

			return value;
		}

		/// Generic function to read an object value.  Returns true if the read
		/// succeeded.
		private bool ReadValue (XmlNode node, TypeConverter converter, ArrayList errors, out object value)
		{
			try {
				foreach (XmlNode child in node.ChildNodes) {
					if (child.NodeType == XmlNodeType.Text) {
						value = converter.ConvertFromInvariantString (node.InnerText);
						return true;
					} else if (child.Name.Equals ("Binary")) {
						byte[] data = Convert.FromBase64String (child.InnerText);

						// Binary blob.  Now, check to see if the type converter
						// can convert it.  If not, use serialization.
						//
						if (GetConversionSupported (converter, typeof(byte[]))) {
							value = converter.ConvertFrom (null, CultureInfo.InvariantCulture, data);
							return true;
						} else {
							BinaryFormatter formatter = new BinaryFormatter ();
							formatter.Binder = new UBinder ();
							MemoryStream stream = new MemoryStream (data);
							value = formatter.Deserialize (stream);
							stream.Close ();
							return true;
						}
					} else if (child.Name.Equals ("InstanceDescriptor")) {
						value = ReadInstanceDescriptor (child, errors);
						return (value != null);
					} else if (child.Name.Equals ("Reference")) {
						// 引用机制
						//if(child.ParentNode.Name.Equals("Item"))
						{
							value = ReadRefrence (child.Attributes ["name"].Value, errors);
							return (value != null);
						}
					} else {
						errors.Add (string.Format ("Unexpected element type {0}", child.Name));
						value = null;
						return false;
					}
				}

				// If we get here, it is because there were no nodes.  No nodes and no inner
				// text is how we signify null.
				//
				value = null;
				return true;
			} catch (Exception ex) {
				errors.Add (ex.Message);
				value = null;
				return false;
			}
		}

		private static readonly Attribute[] propertyAttributes = new Attribute[] {
			DesignOnlyAttribute.No
		};

		/// This method writes a given byte[] into the XML document, returning the node that
		/// it just created.  Byte arrays have the following XML:
		/// 
		/// <c>
		/// <Binary>
		///		64 bit encoded string representing binary data
		/// </Binary>
		/// </c>
		private XmlNode WriteBinary (XmlDocument document, byte[] value)
		{
			try {

				XmlNode node = document.CreateElement ("Binary");
				node.InnerText = Convert.ToBase64String (value);
				return node;
			} catch (System.Exception ex) {
				return null;
			}
		}

		/// Writes the given IList contents into the given parent node.
		private void WriteCollection (XmlDocument document, IList list, XmlNode parent)
		{
			try {

				foreach (object obj in list) {
					XmlNode node = document.CreateElement ("Item");
					XmlAttribute typeAttr = document.CreateAttribute ("type");
					typeAttr.Value = obj.GetType ().AssemblyQualifiedName;
					node.Attributes.Append (typeAttr);
					WriteValue (document, obj, node);
					parent.AppendChild (node);
				}
			} catch (System.Exception ex) {

			}
		}

		/// This method writes a given instance descriptor into the XML document, returning a node
		/// that it just created.  Instance descriptors have the following XML:
		/// 
		/// <c>
		/// <InstanceDescriptor member="asdfasdfasdf">
		///		<Object>
		///			// param value
		///		</Object>
		/// </InstanceDescriptor>
		/// </c>
		/// 
		/// Here, member is a 64 bit encoded string representing the member, and there is one Parameter
		/// tag for each parameter of the descriptor.
		private XmlNode WriteInstanceDescriptor (XmlDocument document, InstanceDescriptor desc, object value)
		{
			try {

				XmlNode node = document.CreateElement ("InstanceDescriptor");
				BinaryFormatter formatter = new BinaryFormatter ();
				MemoryStream stream = new MemoryStream ();
				formatter.Serialize (stream, desc.MemberInfo);
				XmlAttribute memberAttr = document.CreateAttribute ("member");
				memberAttr.Value = Convert.ToBase64String (stream.ToArray ());
				stream.Close ();
				node.Attributes.Append (memberAttr);

				foreach (object arg in desc.Arguments) {
					XmlNode argNode = document.CreateElement ("Argument");
					if (WriteValue (document, arg, argNode)) {
						node.AppendChild (argNode);
					}
				}

				// Instance descriptors also support "partial" creation, where 
				// properties must also be persisted.
				//
				if (!desc.IsComplete) {
					PropertyDescriptorCollection props = TypeDescriptor.GetProperties (value, propertyAttributes);
					WriteProperties (document, props, value, node, "Property");
				}

				return node;
			} catch (System.Exception ex) {
				return null;
			}
		}

		public XmlDocument SaveControl (Control ct)
		{
			try {
				XmlDocument document = new XmlDocument ();

				document.AppendChild (document.CreateElement ("DOCUMENT_ELEMENT"));

				IComponent root = ct;
				Hashtable nametable = new Hashtable (ct.Controls.Count);

				XmlNode xn = WriteObject (document, nametable, root);
				if (xn != null)
					document.DocumentElement.AppendChild (xn);

				foreach (IComponent comp in ct.Controls) {
					if (comp != root && !nametable.ContainsKey (comp)) {
						XmlNode xn1 = WriteObject (document, nametable, comp);
						if (xn1 != null)
							document.DocumentElement.AppendChild (xn1);
					}
				}

				return document;
			} catch (System.Exception ex) {
				return null;
			}
		}

		/// This method writes the given object out to the XML document.  Objects have
		/// the following XML:
		/// 
		/// <c>
		/// <Object type="<object type>" name="<object name>" children="<child property name>">
		/// 
		/// </Object>
		/// </c>
		/// 
		/// Here, Object is the element that defines a custom object.  Type is required
		/// and specifies the data type of the object.  Name is optional.  If present, it names
		/// this object, adding it to the container if the object is an IComponent.
		/// Finally, the children attribute is optional.  If present, this object can have
		/// nested objects, and those objects will be added to the given property name.  The
		/// property must be a collection property that returns an object that implements IList.
		/// 
		/// Inside the object tag there can be zero or more of the following subtags:
		/// 
		///		InstanceDescriptor -- describes how to create an instance of the object.
		///		Property -- a property set on the object
		///		Event -- an event binding
		///		Binary -- binary data
		private XmlNode WriteObject (XmlDocument document, IDictionary nametable, object value)
		{
			try {

				Debug.Assert (value != null, "Should not invoke WriteObject with a null value");

				XmlNode node = document.CreateElement ("Object");

				XmlAttribute typeAttr = document.CreateAttribute ("type");
				typeAttr.Value = value.GetType ().AssemblyQualifiedName;
				node.Attributes.Append (typeAttr);

				// Does this object have a name?
				//
				Control component = value as Control;
				if (component != null && component.Site == null) {
					XmlAttribute nameAttr = document.CreateAttribute ("name");
					nameAttr.Value = component.Name;
					node.Attributes.Append (nameAttr);
					Debug.Assert (nametable [component] == null, "WriteObject should not be called more than once for the same object.  Use WriteReference instead");
					nametable [value] = component.Name;
				}

				// Special case:  We want Windows Forms controls to "nest", so child
				// elements are child controls on the form.  This requires either an
				// extensible serialization mechanism (like the existing CodeDom
				// serialization scheme), or it requires special casing in the
				// serialization code.  We choose the latter in order to make
				// this example easier to understand.
				//
				bool isControl = (value is Control);

				if (isControl) {
					XmlAttribute childAttr = document.CreateAttribute ("children");
					childAttr.Value = "Controls";
					node.Attributes.Append (childAttr);
				}

				if (component != null) {
					// We have a component.  Write out the definition for the component here.  If the
					// component is also a control, recurse so we build up the parent hierarchy.
					//
					if (isControl) {
						foreach (Control child in ((Control)value).Controls) {
							//if (child.Site != null && child.Site.Container == _BaseForm.Container)
							{
								node.AppendChild (WriteObject (document, nametable, child));
							}
						}
					}

					// Now do our own properties.
					//
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties (value, propertyAttributes);

					if (isControl) {
						// If we are a control and we can locate the control property, we should remove
						// the property from the collection. The collection that comes back from TypeDescriptor
						// is read-only, however, so we must clone it first.
						//
						PropertyDescriptor controlProp = properties ["Controls"];
						if (controlProp != null) {
							PropertyDescriptor[] propArray = new PropertyDescriptor[properties.Count - 1];
							int idx = 0;
							foreach (PropertyDescriptor p in properties) {
								if (p != controlProp) {
									propArray [idx++] = p;
								}
							}

							properties = new PropertyDescriptorCollection (propArray);
						}
					}

					WriteProperties (document, properties, value, node, "Property");

					// 20090923 Events 运行时未存储
					//EventDescriptorCollection events = TypeDescriptor.GetEvents(value, propertyAttributes);
					//IEventBindingService bindings = _BaseForm.GetService(typeof(IEventBindingService)) as IEventBindingService;
					//if (bindings != null)
					//{
					//    properties = bindings.GetEventProperties(events);
					//    WriteProperties(document, properties, value, node, "Event");
					//}
				} else {
					// Not a component, so we just write out the value.
					//
					WriteValue (document, value, node);
				}
				return node;
			} catch (System.Exception ex) {
				return null;
			}
		}

		/// This method writes zero or more property elements into the given parent node.  
		private void WriteProperties (XmlDocument document, PropertyDescriptorCollection properties, object value, XmlNode parent, string elementName)
		{
			try {

				foreach (PropertyDescriptor prop in properties) {
					if (prop.Name == "AutoScaleBaseSize") {
						string _DEBUG_ = prop.Name;
					}

					if (prop.ShouldSerializeValue (value)) {
						XmlNode node = document.CreateElement (elementName);
						XmlAttribute attr = document.CreateAttribute ("name");
						attr.Value = prop.Name;
						node.Attributes.Append (attr);

						DesignerSerializationVisibilityAttribute visibility = (DesignerSerializationVisibilityAttribute)prop.Attributes [typeof(DesignerSerializationVisibilityAttribute)];
						switch (visibility.Visibility) {
						case DesignerSerializationVisibility.Visible:
							if (!prop.IsReadOnly && WriteValue (document, prop.GetValue (value), node)) {
								parent.AppendChild (node);
							}
							break;

						case DesignerSerializationVisibility.Content:
                                // A "Content" property needs to have its properties stored here, not the actual value.  We 
                                // do another special case here to account for collections.  Collections are content properties
                                // that implement IList and are read-only.
                                //
							object propValue = prop.GetValue (value);

							if (typeof(IList).IsAssignableFrom (prop.PropertyType)) {
								WriteCollection (document, (IList)propValue, node);
							} else {
								PropertyDescriptorCollection props = TypeDescriptor.GetProperties (propValue, propertyAttributes);
								WriteProperties (document, props, propValue, node, elementName);
							}
							if (node.ChildNodes.Count > 0) {
								parent.AppendChild (node);
							}
							break;

						default:
							break;
						}
					}
				}
			} catch (System.Exception ex) {

			}
		}

		/// Writes a reference to the given component.  Emits the following
		/// XML:
		/// 
		/// <c>
		/// <Reference name="component name"></Reference>
		/// </c>
		private XmlNode WriteReference (XmlDocument document, IComponent value)
		{
			try {
				Debug.Assert (value != null, "Invalid component passed to WriteReference");

				XmlNode node = document.CreateElement ("Reference");
				XmlAttribute attr = document.CreateAttribute ("name");
				attr.Value = (value as Control).Name;
				node.Attributes.Append (attr);
				return node;
			} catch (System.Exception ex) {
				return null;
			}
		}

		/// This method writes the given object into the given parent node.  It returns
		/// true if it was successful, or false if it was unable to convert the object
		/// to XML.
		private bool WriteValue (XmlDocument document, object value, XmlNode parent)
		{
			try {


				// For empty values, we just return.  This creates an empty node.
				if (value == null) {
					return true;
				}

				TypeConverter converter = TypeDescriptor.GetConverter (value);

				if (GetConversionSupported (converter, typeof(string))) {
					// Strings have the most fidelity.  If this object
					// supports being converted to a string, do so, and then
					// we're done.
					//
					parent.InnerText = (string)converter.ConvertTo (null, CultureInfo.InvariantCulture, value, typeof(string));
				} else if (GetConversionSupported (converter, typeof(byte[]))) {
					// Binary blobs are converted by encoding as a binary element.
					// 
					byte[] data = (byte[])converter.ConvertTo (null, CultureInfo.InvariantCulture, value, typeof(byte[]));
					parent.AppendChild (WriteBinary (document, data));
				} else if (GetConversionSupported (converter, typeof(InstanceDescriptor))) {
					// InstanceDescriptors are encoded as an InstanceDescriptor element.
					//
					InstanceDescriptor id = (InstanceDescriptor)converter.ConvertTo (null, CultureInfo.InvariantCulture, value, typeof(InstanceDescriptor));
					parent.AppendChild (WriteInstanceDescriptor (document, id, value));
				} else if (value is IComponent) {
					// IComponent.  Treat this as a reference.
					//
					parent.AppendChild (WriteReference (document, (IComponent)value));
				} else if (value.GetType ().IsSerializable) {
					// Finally, check to see if this object is serializable.  If it is, we serialize it here
					// and then write it as a binary.
					//
					BinaryFormatter formatter = new BinaryFormatter ();
					MemoryStream stream = new MemoryStream ();
					formatter.Serialize (stream, value);
					XmlNode binaryNode = WriteBinary (document, stream.ToArray ());
					parent.AppendChild (binaryNode);
					stream.Close ();
				} else {
					return false;
				}
			} catch (Exception) {
				return false;
			}
			return true;
		}
	}
}
