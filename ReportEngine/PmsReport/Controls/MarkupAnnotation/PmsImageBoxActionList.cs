using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace PMS.Libraries.ToolControls.Report.MarkupAnnotation
{
    public class PmsImageBoxActionList : DesignerActionList
    {
        private PmsImageBox _imageBox = null;
        public PmsImageBoxActionList(IComponent component)
            : base(component)
        {
            _imageBox = component as PmsImageBox;
        }

        // Helper method to retrieve control properties. Use of 
        // GetProperties enables undo and menu updates to work properly.
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(Component)[propName];
            if (null == prop)
            {
                throw new ArgumentException(
                     "Matching ColorLabel property not found!",
                      propName);
            }
            else
            {
                return prop;
            }
        }

        [RefreshProperties(RefreshProperties.Repaint)]
        [Browsable(true)]
        public PictureBoxSizeMode Mode
        {
            get
            {
                if (null != _imageBox)
                {
                    return _imageBox.Mode;
                }
                return PictureBoxSizeMode.Normal;
            }
            set
            {
                if (null != _imageBox)
                {
                    PropertyDescriptor pd = GetPropertyByName("Mode");
                    if (null != pd)
                    {
                        pd.SetValue(_imageBox, value);
                    }
                }
            }
        }

        [Browsable(true)]
        public Image Image
        {
            get
            {
                if (null != _imageBox)
                {
                    return _imageBox.Image;
                }
                return null;
            }
            set
            {
                if (null != _imageBox)
                {
                    PropertyDescriptor pd = GetPropertyByName("Image");
                    if (null != pd)
                    {
                        pd.SetValue(_imageBox, value);
                    }
                }
            }
        }

        [Browsable(true)]
        public Image ErrorImage
        {
            get
            {
                if (null != _imageBox)
                {
                    return _imageBox.ErrorImage;
                }
                return null;
            }
            set
            {
                if (null != _imageBox)
                {
                    PropertyDescriptor pd = GetPropertyByName("ErrorImage");
                    if (null != pd)
                    {
                        pd.SetValue(_imageBox, value);
                    }
                }
            }
        }


        public void MessageBox()
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection dc = new DesignerActionItemCollection();
            dc.Add(new DesignerActionPropertyItem("Mode", "缩放模式", "Pms控件属性"));
            DesignerActionPropertyItem item = new DesignerActionPropertyItem("Image", "选择图像", "Pms控件属性", "背景图片");
            dc.Add(item);
            DesignerActionPropertyItem item2 = new DesignerActionPropertyItem("ErrorImage", "错误时显示的图像", "Pms控件属性", "错误图片");
            dc.Add(item2);
            return dc;
        }
    }
}
