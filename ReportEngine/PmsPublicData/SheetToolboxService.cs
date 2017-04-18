using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace PMS.Libraries.ToolControls.PmsSheet.PmsPublicData
{
    internal class SheetToolboxService : ToolboxService
    {
        private DesignSurface _surface;
        private GListBox toolBox;
        private int selectedIndex = 0;

        public SheetToolboxService(DesignSurface surface,int Query)
        {
            _surface = surface;
            toolBox = new GListBox();
            ImageList imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.point);
            imageList.Images.Add(Properties.Resources.label);
            imageList.Images.Add(Properties.Resources.textbox);
            imageList.Images.Add(Properties.Resources.combobox);
            imageList.Images.Add(Properties.Resources.check);
            imageList.Images.Add(Properties.Resources.picture);
            imageList.Images.Add(Properties.Resources.foreignKey);
            toolBox.ImageList = imageList;

            GListBoxItem pointer = new GListBoxItem(0, null);
            pointer.toolBoxItem.DisplayName = "Pointer";
            toolBox.Items.Add(pointer);

            pointer = new GListBoxItem(1, typeof(LabelEx));
            pointer.toolBoxItem.DisplayName = "Label";
            toolBox.Items.Add(pointer);

            pointer = new GListBoxItem(2, typeof(TextBoxEx));
            pointer.toolBoxItem.DisplayName = "TextBox";
            toolBox.Items.Add(pointer);

            pointer = new GListBoxItem(3, typeof(ComboBoxEx));
            pointer.toolBoxItem.DisplayName = "ComboBox";
            toolBox.Items.Add(pointer);

            pointer = new GListBoxItem(4, typeof(CheckBoxEx));
            pointer.toolBoxItem.DisplayName = "CheckBox";
            toolBox.Items.Add(pointer);

            if (Query == 0)//不是查询条件设计器
            {
                pointer = new GListBoxItem(5, typeof(FileDisplay));
                pointer.toolBoxItem.DisplayName = "FileDisplay";//
                toolBox.Items.Add(pointer);

                pointer = new GListBoxItem(6, typeof(ForeignKeyCtrlEx));
                pointer.toolBoxItem.DisplayName = "ForeignKeyBox";
                toolBox.Items.Add(pointer);

                pointer = new GListBoxItem(7, typeof(GroupBoxEx));
                pointer.toolBoxItem.DisplayName = "GroupBoxEx";
                toolBox.Items.Add(pointer);

                pointer = new GListBoxItem(8, typeof(RadioButtonEx));
                pointer.toolBoxItem.DisplayName = "RadioButtonEx";
                toolBox.Items.Add(pointer);

                pointer = new GListBoxItem(9, typeof(NumericUpDownEx));
                pointer.toolBoxItem.DisplayName = "NumericUpDownEx";
                toolBox.Items.Add(pointer);
            }

            toolBox.MouseDown += new MouseEventHandler(toolBox_MouseDown);
        }

        void toolBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                GListBox lbSender = sender as GListBox;
                Rectangle lastSelectedBounds = lbSender.GetItemRectangle(0);
                
                try
                {
                    lastSelectedBounds = lbSender.GetItemRectangle(selectedIndex);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

                selectedIndex = lbSender.IndexFromPoint(e.X, e.Y); // change our selection
                lbSender.SelectedIndex = selectedIndex;
                lbSender.Invalidate(lastSelectedBounds); // clear highlight from last selection
                lbSender.Invalidate(lbSender.GetItemRectangle(selectedIndex)); // highlight new one

                if (selectedIndex != 0)
                {
                    if (e.Clicks == 2)
                    {
                        IDesignerHost idh = (IDesignerHost)this._surface.GetService(typeof(IDesignerHost));
                        IToolboxUser tbu = idh.GetDesigner(idh.RootComponent as IComponent) as IToolboxUser;

                        if (tbu != null)
                        {
                            GListBoxItem gbi = lbSender.Items[selectedIndex] as GListBoxItem;
                            tbu.ToolPicked((System.Drawing.Design.ToolboxItem)(gbi.toolBoxItem));
                        }
                    }
                    else if (e.Clicks < 2)
                    {
                        GListBoxItem gbi = lbSender.Items[selectedIndex] as GListBoxItem;
                        System.Drawing.Design.ToolboxItem tbi = gbi.toolBoxItem;
                        IToolboxService tbs = this;

                        // The IToolboxService serializes ToolboxItems by packaging them in DataObjects.
                        DataObject d = tbs.SerializeToolboxItem(tbi) as DataObject;

                        try
                        {
                            lbSender.DoDragDrop(d, DragDropEffects.Copy);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public GListBox ToolBox
        {
            get { return toolBox; }
            set { toolBox = value; }
        }

        protected override CategoryNameCollection CategoryNames
        {
            get
            {
                return null;
            }
        }

        //实现带分类的工具列表，由于目前不分类，所以即为全部工具
        protected override System.Collections.IList GetItemContainers(string categoryName)
        {
            ToolboxItem[] t = new ToolboxItem[this.toolBox.Items.Count];
            this.toolBox.Items.CopyTo(t, 0);

            return t;
        }

        //实现工具列表
        protected override System.Collections.IList GetItemContainers()
        {
            ToolboxItem[] t = new ToolboxItem[this.toolBox.Items.Count];
            this.toolBox.Items.CopyTo(t, 0);

            return t;
        }

        protected override void Refresh()
        {
        }

        protected override string SelectedCategory
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        //实现工具选择
        protected override ToolboxItemContainer SelectedItemContainer
        {
            get
            {
                if (toolBox.SelectedIndex > 0)
                {
                    ToolboxItem t = ((GListBoxItem)toolBox.SelectedItem).toolBoxItem;
                    return new ToolboxItemContainer(t);
                }
                return null;
            }
            set
            {
                if (value == null)
                {
                    toolBox.SelectedIndex = -1;
                }
            }
        }
    }
    // GListBoxItem 类 
    internal class GListBoxItem
    {
        private ToolboxItem _toolBoxItem;
        private int _myImageIndex;
        public int ImageIndex
        {
            get { return _myImageIndex; }
            set { _myImageIndex = value; }
        }
        public ToolboxItem toolBoxItem
        {
            get { return _toolBoxItem; }
            set { _toolBoxItem = value; }
        }
        //构造函数 
        public GListBoxItem(int index)
        {
            _myImageIndex = index;
            _toolBoxItem = new ToolboxItem();
        }
        public GListBoxItem(int index, Type t)
        {
            _myImageIndex = index;
            if (t == null)
                _toolBoxItem = new ToolboxItem();
            else
                _toolBoxItem = new ToolboxItem(t);
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
    // GListBox 类 
    internal class GListBox : ListBox
    {
        private ImageList _myImageList;
        public ImageList ImageList
        {
            get { return _myImageList; }
            set { _myImageList = value; }
        }
        public GListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.DrawItem += new DrawItemEventHandler(GListBox_DrawItem);
            this.MeasureItem += new MeasureItemEventHandler(GListBox_MeasureItem);
            //this.Bounds.Height = 100;
        }
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {

            e.DrawBackground();
            e.DrawFocusRectangle();
            Rectangle bounds = e.Bounds;
            try
            {
                GListBoxItem item;
                Size imageSize = _myImageList.ImageSize;

                item = (GListBoxItem)Items[e.Index];
                if (item.ImageIndex != -1)
                {
                    ImageList.Draw(e.Graphics, bounds.Left, bounds.Top, 16, 16, item.ImageIndex);
                    e.Graphics.DrawString(item.toolBoxItem.DisplayName, e.Font, new SolidBrush(e.ForeColor), bounds.Left + imageSize.Width, bounds.Top);
                }
                else
                {
                    e.Graphics.DrawString(item.toolBoxItem.DisplayName, e.Font, new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
                }
            }
            catch
            {
                if (e.Index != -1)
                {
                    e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
                }
                else
                {
                    e.Graphics.DrawString(Text, e.Font, new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
                }
            }
            base.OnDrawItem(e);
        }
        private void GListBox_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
        {
            //if (e.Index == 1)//设置第二行的高度为30  
            {
                e.ItemHeight = 20;
            }
        }

        private void GListBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            if ((e.State & DrawItemState.Selected) > 0)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(225, 230, 232)), e.Bounds);

            GListBoxItem item;
            Rectangle bounds = e.Bounds;
            Size imageSize = _myImageList.ImageSize;
            try
            {
                item = (GListBoxItem)Items[e.Index];
                if (item.ImageIndex != -1)
                {
                    ImageList.Draw(e.Graphics, bounds.Left, bounds.Top + 2, item.ImageIndex);
                    e.Graphics.DrawString(item.toolBoxItem.DisplayName, e.Font, new SolidBrush(e.ForeColor), bounds.Left + imageSize.Width, bounds.Top + 2);
                }
                else
                {
                    e.Graphics.DrawString(item.toolBoxItem.DisplayName, e.Font, new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
                }
            }
            catch
            {
            }
        }
    }
}
