using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class MESList : UserControl
    {
        private HScrollBar _hScroll = null;
        private VScrollBar _vScroll = null;
        private CustomList _list = null;
        private bool _hVisible = false;
        private bool _vVisible = false;

        public int ItemWidth
        {
            get
            {
                if (null != _list)
                {
                    return _list.ItemWidth;
                }
                return 0;
            }
            set
            {
                if (null != _list)
                {
                    _list.ItemWidth = value;
                }
            }
        }

        public int ItemHeight
        {
            get
            {
                if (null != _list)
                {
                    return _list.ItemHeight;
                }
                return 0;
            }
            set
            {
                if (null != _list)
                {
                    _list.ItemHeight = value;
                }
            }
        }

        public int SelectedIndex
        {
            get
            {
                if (null != _list)
                {
                    return _list.SelectedtIndex;
                }
                return -1;
            }
            set
            {
                _list.SelectedtIndex = value;
            }
        }

        public object SelectedItem
        {
            get
            {
                if (null != _list)
                {
                    return _list.SelectedItem;
                }
                return null;
            }
        }

        public event OnSelectedItemChanged OnSelectedItemChanged;

        public MESList()
        {
            InitializeComponent();
            _list = new CustomList();
            _hScroll = new HScrollBar();
            _vScroll = new VScrollBar();
            _hScroll.Visible = true;
            _vScroll.Visible = true;
            Controls.Add(_hScroll);
            Controls.Add(_vScroll);
            _hScroll.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            _vScroll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Top;
            Controls.Add(_list);
            _list.Location = new Point(1, 1);
            _list.Width = Width ;
            _list.Height = Height;
            _list.OnScrollBarShow += new OnScrollBarShow(List_OnScrollBarShow);
            _vScroll.Visible = false;
            _hScroll.Visible = false;
            _hScroll.Scroll += new ScrollEventHandler(HScroll_Scroll);
            _vScroll.Scroll += new ScrollEventHandler(VScroll_Scroll);
            _list.OnSelectedItemChanged += new EditorDialog.OnSelectedItemChanged(List_OnSelectedItemChanged);
        }

        void List_OnSelectedItemChanged(object o, ItemEventArgs e)
        {
            if (null != OnSelectedItemChanged)
            {
                OnSelectedItemChanged(this, e);
            }
        }

        private void List_OnScrollBarShow(object o, ScrollBarShowEventArgs e)
        {
            if (0 != (int)(e.Direction & Direction.Vetical) )
            {
                this._vScroll.Visible = e.Visible;
                _vVisible = e.Visible;
                if (e.Visible)
                {
                    this._vScroll.Minimum = e.Min;
                    this._vScroll.Maximum = e.Max;
                    this._vScroll.SmallChange = e.SmallChange;
                    this._vScroll.LargeChange = e.LargeChange;
                    this._vScroll.Value = 0;
                    _vScroll.Location = new Point(Width - _vScroll.Width - 1, 1);
                    if (!_hVisible)
                    {
                        _vScroll.Height = Height-2;
                    }
                    else
                    {
                        _vScroll.Height = Height - _hScroll.Height;
                    }
                }
                if (!e.Visible)
                {
                    if (_hScroll.Visible)
                    {
                        _hScroll.Width = Width-2;
                    }
                }
            }
            if (0 != (int)(e.Direction & Direction.Horizontal))
            {
                this._hScroll.Visible = e.Visible;
                this._hVisible = e.Visible;
                if (e.Visible)
                {
                    this._hScroll.Minimum = e.Min;
                    this._hScroll.Maximum = e.Max;
                    this._hScroll.SmallChange = e.SmallChange;
                    this._hScroll.LargeChange = e.LargeChange;
                    this._hScroll.Value = 0;
                    _hScroll.Location = new Point(1, Height - _hScroll.Height - 1);
                    if (_vVisible)
                    {
                        _hScroll.Width = Width - _vScroll.Width;
                    }
                    else
                    {
                        _hScroll.Width = Width-2;
                    }
                }
                if (!e.Visible)
                {
                    if (_vScroll.Visible)
                    {
                        _vScroll.Height = Height-2;
                    }
                }
            }

            int width = Width;
            if (_vVisible)
            {
                width -= _vScroll.Width;
            }
            int height = Height;
            if (_hVisible)
            {
                height -= _hScroll.Height;
            }
            _list.Size = new Size(width-2, height-2);
        }

        private  void VScroll_Scroll(object sender, ScrollEventArgs e)
        {
            _list.Scroll(Direction.Vetical, e.NewValue);
        }

        private void HScroll_Scroll(object sender, ScrollEventArgs e)
        {
            _list.Scroll(Direction.Horizontal, e.NewValue);
        }

        public void AddItem(ICustomDrawItem item)
        {
            _list.AddItem(item);
        }

        public void RemoveItem(ICustomDrawItem item)
        {
            _list.RemoveItem(item);
        }

        public void RemoveItem(int index)
        {
            _list.RemoveItem(index);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, Width-1 , Height-1 );
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (_vVisible)
            {
                _vScroll.Location = new Point(Width - _vScroll.Width - 1, 1);
                if (!_hVisible)
                {
                    _vScroll.Height = Height - 2;
                }
                else
                {
                    _vScroll.Height = Height - _hScroll.Height;
                }
            }
            if (_hVisible)
            {
                _hScroll.Location = new Point(1, Height - _hScroll.Height - 1);
                if (_vVisible)
                {
                    _hScroll.Width = Width - _vScroll.Width;
                }
                else
                {
                    _hScroll.Width = Width - 2;
                }
            }
            int width = Width;
            if (_vVisible)
            {
                width -= _vScroll.Width;
            }
            int height = Height;
            if (_hVisible)
            {
                height -= _hScroll.Height;
            }
            if (null != _list)
            {
                _list.Size = new Size(width - 2, height - 2);
            }
            base.OnSizeChanged(e);
        }

    }
}
