using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel.Design;
using System.Xml;

namespace MES.PublicInterface
{
    /// <summary>
    /// 自定义属性类
    /// </summary>
    public class CustomProperty
    {
        #region Private Variables
        private string _name = string.Empty;
        private object _defaultValue = null;
        private object _value = null;
        private object _objectSource = null;
        private PropertyInfo[] _propertyInfos = null;
        #endregion

        #region Contructors
        public CustomProperty()
        {
        }

        public CustomProperty(string name, string category, string description, object objectSource)
            : this(name, name, null, category, description, objectSource, null)
        {
        }

        public CustomProperty(string name, string propertyName, string category, string description, object objectSource)
            : this(name, propertyName, null, category, description, objectSource, null)
        {
        }

        public CustomProperty(string name, string propertyName, string category, string description, object objectSource, Type editorType)
            : this(name, propertyName, null, category, description, objectSource, editorType)
        {
        }

        public CustomProperty(string name, string propertyName, Type valueType, string category, string description,
            object objectSource, Type editorType)
            : this(name, new string[] { propertyName }, valueType, null, null, false, true, category, description, objectSource, editorType)
        {
        }

        public CustomProperty(string name, string[] propertyNames, string category, string description, object objectSource)
            : this(name, propertyNames, category, description, objectSource, null)
        {
        }

        public CustomProperty(string name, string[] propertyNames, string category, string description, object objectSource, Type editorType)
            : this(name, propertyNames, null, category, description, objectSource, editorType)
        {
        }

        public CustomProperty(string name, string[] propertyNames, Type valueType, string category, string description,
            object objectSource, Type editorType)
            : this(name, propertyNames, valueType, null, null, false, true, category, description, objectSource, editorType)
        {
        }

        public CustomProperty(string name, string[] propertyNames, Type valueType, object defaultValue, object value,
            bool isReadOnly, bool isBrowsable, string category, string description, object objectSource, Type editorType)
        {
            Name = name;
            PropertyNames = propertyNames;
            ValueType = valueType;
            _defaultValue = defaultValue;
            _value = value;
            IsReadOnly = isReadOnly;
            IsBrowsable = isBrowsable;
            Category = category;
            Description = description;
            ObjectSource = objectSource;
            EditorType = editorType;
        }
        #endregion

        #region Public Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                if (PropertyNames == null)
                {
                    PropertyNames = new string[] { _name };
                }
            }
        }

        public string[] PropertyNames { get; set; }

        public Type ValueType { get; set; }

        public object DefaultValue
        {
            get { return _defaultValue; }
            set
            {
                _defaultValue = value;
                if (_defaultValue != null)
                {
                    if (_value == null) _value = _defaultValue;
                    if (ValueType == null) ValueType = _defaultValue.GetType();
                }
            }
        }

        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;

                OnValueChanged();
            }
        }

        public bool IsReadOnly { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool IsBrowsable { get; set; }

        public object ObjectSource
        {
            get { return _objectSource; }
            set
            {
                _objectSource = value;
                OnObjectSourceChanged();
            }
        }

        public Type EditorType { get; set; }
        #endregion

        #region Protected Functions

        protected void OnObjectSourceChanged()
        {
            if (PropertyInfos.Length == 0) return;

            object value = PropertyInfos[0].GetValue(_objectSource, null);
            if (_defaultValue == null) DefaultValue = value;
            _value = value;
        }

        protected void OnValueChanged()
        {
            if (_objectSource == null) return;


            if (_objectSource is Form)
            {
                Form f = (Form)_objectSource;

                if (_name == "Width")
                {
                    int w = (int)(_value);
                    if (w > 32767)
                        w = 32767;

                    //改变视图尺寸时默认将滚动条设置至初始位置（0,0）
                    int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(f.Parent)).HorizontalScroll)).Value;
                    int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(f.Parent)).VerticalScroll)).Value;
                    DesignerForm.SetFormSize(f, x, y, w, f.Height);
                }
                else if (_name == "Height")
                {
                    int h = (int)(_value);
                    if (h > 32767)
                        h = 32767;

                    //改变视图尺寸时默认将滚动条设置至初始位置（0,0）
                    int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(f.Parent)).HorizontalScroll)).Value;
                    int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(f.Parent)).VerticalScroll)).Value;
                    DesignerForm.SetFormSize(f, x, y, f.Width, h);
                }
                else if (_name == "Size")
                {
                    if (_value is Size)
                    {
                        int h = ((Size)_value).Height;
                        int w = ((Size)_value).Width;
                        if (h > 32767)
                            h = 32767;
                        if (w > 32767)
                            w = 32767;

                        //改变视图尺寸时默认将滚动条设置至初始位置（0,0）
                        int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(f.Parent)).HorizontalScroll)).Value;
                        int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(f.Parent)).VerticalScroll)).Value;

                        //((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).HorizontalScroll)).Value = 0;
                        //((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(ctrl.Controls[0])).VerticalScroll)).Value = 0;

                        DesignerForm.SetFormSize(f, x, y, w, h);
                    }
                }

                //return;
            }

            foreach (PropertyInfo propertyInfo in PropertyInfos)
            {
                try
                {
                    propertyInfo.SetValue(_objectSource, _value, null);
                }
                catch { }
            }
        }

        protected PropertyInfo[] PropertyInfos
        {
            get
            {
                if (_propertyInfos == null)
                {
                    Type type = ObjectSource.GetType();
                    _propertyInfos = new PropertyInfo[PropertyNames.Length];
                    for (int i = 0; i < PropertyNames.Length; i++)
                    {
                        _propertyInfos[i] = type.GetProperty(PropertyNames[i]);
                    }
                }
                return _propertyInfos;
            }
        }
        #endregion

        #region Public Functions
        public void ResetValue()
        {
            Value = DefaultValue;
        }
        #endregion
    }

    /// <summary>
    /// 自定义属性类集合,包含属性自定义编辑器
    /// </summary>
    /// 
    public class CustomPropertyCollection : List<CustomProperty>, ICustomTypeDescriptor
    {
        public Control CtrlType;

        #region ICustomTypeDescriptor 成员

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection properties = new PropertyDescriptorCollection(null);

            foreach (CustomProperty cp in this)
            {
                List<Attribute> attrs = new List<Attribute>();
                //[Browsable(false)]
                if (!cp.IsBrowsable)
                {
                    attrs.Add(new BrowsableAttribute(cp.IsBrowsable));
                }
                //[ReadOnly(true)]
                if (cp.IsReadOnly)
                {
                    attrs.Add(new ReadOnlyAttribute(cp.IsReadOnly));
                }
                //[Editor(typeof(editor),typeof(UITypeEditor))]
                if (cp.EditorType != null)
                {
                    attrs.Add(new EditorAttribute(cp.EditorType, typeof(System.Drawing.Design.UITypeEditor)));
                }

                properties.Add(new CustomPropertyDescriptor(cp, attrs.ToArray()));
            }
            return properties;
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion
    }

    /// <summary>
    /// 自定义属性描述器类
    /// </summary>
    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        private CustomProperty _customProperty = null;

        public CustomPropertyDescriptor(CustomProperty customProperty, Attribute[] attrs)
            : base(customProperty.Name, attrs)
        {
            _customProperty = customProperty;
        }

        public override bool CanResetValue(object component)
        {
            return _customProperty.DefaultValue != null;
        }

        public override Type ComponentType
        {
            get { return _customProperty.GetType(); }
        }

        public override object GetValue(object component)
        {
            return _customProperty.Value;
        }

        public override bool IsReadOnly
        {
            get { return _customProperty.IsReadOnly; }
        }

        public override Type PropertyType
        {
            get { return _customProperty.ValueType; }
        }

        public override void ResetValue(object component)
        {
            _customProperty.ResetValue();
        }

        public override void SetValue(object component, object value)
        {
            _customProperty.Value = value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public void SetDisplayName(string pDispalyName)
        {
            _customProperty.DisplayName = pDispalyName;
        }

        public void SetCategory(string pCategory)
        {
            _customProperty.Category = pCategory;
        }

        public void SetBrowsable(bool pIsBrowsable)
        {
            _customProperty.IsBrowsable = pIsBrowsable;
        }

        public void SetDescription(string pDescription)
        {
            _customProperty.Description = pDescription;
        }

        //
        public override string Description
        {
            get
            {
                return _customProperty.Description;
            }
        }

        public override string Category
        {
            get
            {
                return _customProperty.Category;
            }
        }

        public override string DisplayName
        {
            get
            {
                return _customProperty.Name;
            }
        }

        public override bool IsBrowsable
        {
            get
            {
                return _customProperty.IsBrowsable;
            }
        }

        public object CustomProperty
        {
            get
            {
                return _customProperty;
            }
        }
    }

    public interface IMESCustomPropertyInterface
    {
        CustomPropertyCollection CustomPropertyCollection
        {
            get;
            //set;
        }
    }

    /// <summary>
    /// 支持表达式接口
    /// </summary>
    public interface IExpressions
    {
        List<object> Expressions
        {
            get;
            
        }

        object Evaluator
        {
            get;
            set;
        }
    }

    /// <summary>
    /// UI层支持表达式接口
    /// </summary>
    public interface IUIDesignExpStruct
    {
        /// <summary>
        /// 控件的表达式层级树节点，树节点的Tag为对象Type
        /// </summary>
        TreeNode ExpStructNode
        {
            get;

        }


    }

    /// <summary>
    /// 类表达式接口
    /// </summary>
    public interface IClass
    {
        
    }

    /// <summary>
    /// 类成员表达式接口
    /// </summary>
    public interface IMember
    {

    }

    /// <summary>
    /// 对象表达式计算的xml数据基础节点集合
    /// </summary>
    public interface IDataXmlNodes
    {
        List<XmlNode> GetDataNodes(XmlDocument xdoc);
    }

    // 不删除设计时的基本控件 属性标签
    [System.AttributeUsage(AttributeTargets.Class)]
    public class MESBasicDesignerControlAttributeAttribute : Attribute
    {
        private string _strAtt;
        public string StrAtt
        {
            get { return _strAtt; }
            set { _strAtt = value; }
        }

        public  MESBasicDesignerControlAttributeAttribute()
        {

        }

        public MESBasicDesignerControlAttributeAttribute(string att)
        {
            this._strAtt = att;
        }

    }

    public class PublicFunction
    {
        private static readonly Attribute[] propertyAttributes = new Attribute[] 
			{
				DesignOnlyAttribute.No
			};

        public static CustomProperty GetCustomProperty(object o,string strPropName)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(o, propertyAttributes);
            PropertyDescriptor pd = props.Find(strPropName,false);
            if(pd != null)
            {
                return new CustomProperty(pd.DisplayName, strPropName, pd.Category, pd.Description, o);
            }
            return null;
        }

        public static CustomPropertyCollection GetCustomPropertyCollection(object o, List<string> PropNames)
        {
            CustomPropertyCollection collection = new CustomPropertyCollection();
            foreach (string strName in PropNames)
            {
                CustomProperty cp = GetCustomProperty(o, strName);
                if(cp != null)
                {
                    collection.Add(cp);
                }
            }
            return collection;
        }

        public static CustomPropertyCollection GetCustomPropertyCollection(object o)
        {
            CustomPropertyCollection collection = new CustomPropertyCollection();
            PropertyDescriptorCollection thisdescriptors = TypeDescriptor.GetProperties(o.GetType());
            foreach (PropertyDescriptor pd in thisdescriptors)
            {
                CustomProperty cp = GetCustomProperty(o, pd.Name);
                if (cp != null)
                {
                    collection.Add(cp);
                }
            }
            return collection;
        }

        public static CustomPropertyCollection PreFilterCustomPropertyCollection(object o, Type typeToIgnore)
        {
            CustomPropertyCollection collection = new CustomPropertyCollection();
            PropertyDescriptorCollection thisdescriptors = TypeDescriptor.GetProperties(o.GetType());
            PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(typeToIgnore);
            foreach (PropertyDescriptor pd in thisdescriptors)
            {
                if(descriptors.Contains(pd))
                    continue;
                CustomProperty cp = GetCustomProperty(o, pd.Name);
                if (cp != null)
                {
                    collection.Add(cp);
                }
            }
            return collection;
        }

        public static void SetPropertyVisibility(object obj, string propertyName, bool visible)
        {
            Type type = typeof(BrowsableAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("browsable", BindingFlags.Instance | BindingFlags.NonPublic);
            fld.SetValue(attrs[type], visible);
        }

        public static void SetPropertyReadOnly(object obj, string propertyName, bool readOnly)
        {
            Type type = typeof(System.ComponentModel.ReadOnlyAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);
            fld.SetValue(attrs[type], readOnly);
        }

    }

    public class MESDesignerService
    {
        private IDesignerHost _host = null;

        private object _hostSurfaceView = null;

        private List<Control> _cc = new List<Control>();
        
        public event MouseEventHandler MouseDoubleClick;

        public MESDesignerService()
        {
            
        }

        public IDesignerHost Host
        {
            set { _host = value; }
        }

        public object HostSurfaceView
        {
            set { _hostSurfaceView = value; }
        }

        public int XOffset
        {
            get { return GetXOffset(); }
        }

        public int YOffSet
        {
            get { return GetYOffset(); }
        }

        public List<Control> FocusControls
        {
            get { return _cc; }
        }

        private int GetXOffset()
        {
            if (_host == null)
                return 0;

            try
            {
                // Set the backcolor
                Type hostType = _host.RootComponent.GetType();

                if (hostType == typeof(Form))
                {
                    Form f = (Form)_host.RootComponent;
                    //改变视图尺寸时默认将滚动条设置至初始位置（0,0）
                    int x = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(f.Parent)).HorizontalScroll)).Value;
                    return x;
                }
            }
            catch
            {

            }
            return 0;
        }

        private int GetYOffset()
        {
            if (_host == null)
                return 0;

            try
            {
                // Set the backcolor
                Type hostType = _host.RootComponent.GetType();

                if (hostType == typeof(Form))
                {
                    Form f = (Form)_host.RootComponent;
                    //改变视图尺寸时默认将滚动条设置至初始位置（0,0）
                    int y = 0 - ((System.Windows.Forms.ScrollProperties)(((System.Windows.Forms.ScrollableControl)(f.Parent)).VerticalScroll)).Value;
                    return y;
                }
            }
            catch
            {

            }
            return 0;
        }

        public void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (this.MouseDoubleClick != null)
            {
                this.MouseDoubleClick(this, e);
            }
        }

        public void CreateControl(Control ct)
        {
            if (_hostSurfaceView == null)
                return;
            Control control = _hostSurfaceView as Control;

            control.Controls[0].Controls.Add(ct);

            ISelectionService _selectionService = (ISelectionService)(_host.GetService(typeof(ISelectionService)));
            if (_selectionService == null)
                return;
            Control selectControl = _selectionService.PrimarySelection as Control;
            Point ptScreen = selectControl.Parent.PointToScreen(selectControl.Location);
            Point ptClient = control.PointToClient(ptScreen);
            Point destpoint = new Point(ptClient.X + ct.Left, ptClient.Y + ct.Top);
            ct.Location = destpoint;
            ct.BringToFront();
            ct.Focus();

            _cc.Add(ct);
        }

        public void UpdateControl(Control ct)
        {
            if (_hostSurfaceView == null)
                return;
            Control control = _hostSurfaceView as Control;

            ISelectionService _selectionService = (ISelectionService)(_host.GetService(typeof(ISelectionService)));
            if (_selectionService == null)
                return;
            Control selectControl = _selectionService.PrimarySelection as Control;
            Point ptScreen = selectControl.Parent.PointToScreen(selectControl.Location);
            Point ptClient = control.PointToClient(ptScreen);
            Point destpoint = new Point(ptClient.X + ct.Left, ptClient.Y + ct.Top);
            ct.Location = destpoint;
            ct.BringToFront();
            ct.Focus();
        }

        public void RemoveControl(Control ct)
        {
            if (_hostSurfaceView == null)
                return;
            Control control = _hostSurfaceView as Control;
            if (control.Controls[0].Controls.Contains(ct))
                control.Controls[0].Controls.Remove(ct);
            if (_cc.Contains(ct))
                _cc.Remove(ct);
        }
    }
}
