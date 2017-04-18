using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class CustomList : Control
    {
        private static readonly int ItemRowPadding = 5;
        private static readonly int ItemColPadding = 5;

        private int _itemWidth = 80;
        public int ItemWidth
        {
            get
            {
                return _itemWidth;
            }
            set
            {
                _itemWidth = value;
            }
        }


        private int _itemHeight = 20;
        public int ItemHeight
        {
            get
            {
                return _itemHeight;
            }
            set
            {
                _itemHeight = value;
            }
        }

        private int _pageIndex = 0;
        private int _colIndex = 0;

        private int _selectIndex = -1;
        public int SelectedtIndex
        {
            get
            {
                
                return _selectIndex;
            }
            set
            {
                 
                _selectIndex = value;
            }
        }

        public object SelectedItem
        {
            get
            {
                if (-1 < SelectedtIndex && SelectedtIndex < Items.Count)
                {
                    return Items[SelectedtIndex];
                }
                return null;
            }
        }

        public event OnScrollBarShow OnScrollBarShow;

        public event OnSelectedItemChanged OnSelectedItemChanged;

        [Browsable(false)]
        public List<ICustomDrawItem> Items
        {
            get;
            private set;
        }

        private Range _range;

        public CustomList()
        {
            InitializeComponent();
            Items = new List<ICustomDrawItem>();
            _range = new Range();
            _range.Row = 0;
            _range.Col = 0;
            this.DoubleBuffered = true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            _range = RecalcuateRangeOfPage();
            SetVScrollBarValue();
            SetHScrollBarValue();
            base.OnSizeChanged(e);
            Invalidate();
        }

        public void AddItem(ICustomDrawItem item)
        {
            Items.Add(item);
            SetVScrollBarValue();
            SetHScrollBarValue();
        }

        public void RemoveItem(ICustomDrawItem item)
        {
            Items.Remove(item);
            SetVScrollBarValue();
            SetHScrollBarValue();
        }

        public void RemoveItem(int index)
        {
            if (0 <= index && index < Items.Count)
            {
                Items.RemoveAt(index);
                if (_selectIndex >= Items.Count)
                {
                    _selectIndex = -1;
                }
            }
        }

        public void Scroll(Direction direction, int newValue)
        {
            if (0 != (int)(direction & Direction.Vetical))
            {
                _pageIndex = newValue;
            }
            if (0 != (int)(direction & Direction.Horizontal))
            {
                _colIndex = newValue;
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics);
            base.OnPaint(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int startIndex = _pageIndex * _range.Row * _range.Col + +_colIndex * _range.Row;
            int col = e.Location.X / (ItemWidth + ItemColPadding) -1;
            if (e.Location.X % (ItemWidth + ItemColPadding) > 0)
            {
                if (col < _range.Col - 1)
                {
                    col++;
                }
            }

            int row = e.Location.Y / (ItemHeight + ItemRowPadding) -1;
            if (e.Location.Y % (ItemHeight + ItemRowPadding) > 0)
            {
                if (row < _range.Row - 1)
                {
                    row++;
                }
            }
            startIndex += col * _range.Row + row;
            if (startIndex>=0 &&  startIndex < Items.Count)
            {
                if (SelectedtIndex != startIndex)
                {
                    SelectedtIndex = startIndex;
                    if (null != OnSelectedItemChanged)
                    {
                        OnSelectedItemChanged(this, new ItemEventArgs(SelectedItem));
                    }
                }
                Invalidate();
            }
            base.OnMouseClick(e);
        }

        private Range RecalcuateRangeOfPage()
        {
            //x*ItemHeight + (x-1)*ItemRowPadding = Height
            int row = (this.ClientRectangle.Height + ItemRowPadding) / (ItemHeight+ItemRowPadding);
            int col = (this.ClientRectangle.Width + ItemColPadding) / (ItemWidth + ItemColPadding);
            
            //控件客户区域的宽度剩余大于ItemWidth的一半时候增加一列
            int rest = (this.ClientRectangle.Width + ItemColPadding) % (ItemWidth + ItemColPadding);
            if (rest > ItemWidth / 2)
            {
                if (row * col < Items.Count)
                {
                    col++;
                }
            }
            if (col == 0)
            {
                col = 1;
            }
            Range range = new Range();
            range.Row = row;
            range.Col = col;
            return range;
        }

        private void SetHScrollBarValue()
        {
            if (0 == _range.Col)
            {
                return;
            }

            int width = _range.Col * ItemWidth + (_range.Col - 1) * ItemColPadding;
            int itemCountPerPage = _range.Row * _range.Col;
            int prevPageItemCount = itemCountPerPage * _pageIndex;

            if (width > ClientRectangle.Width && _range.Col > 1)
            {
                int change  = ItemWidth + ItemColPadding;
                if (null != OnScrollBarShow && Items.Count > 0 && Items.Count - prevPageItemCount>_range.Row)
                {
                     int rest = (this.ClientRectangle.Width + ItemColPadding) % (ItemWidth + ItemColPadding);
                     if (rest > ItemWidth / 2)
                     {
                         OnScrollBarShow(this, new ScrollBarShowEventArgs(Direction.Horizontal, true,
                                         0,
                                         1,
                                         1,
                                         1));
                     }

                }
              
            }
            else
            {
                if (null != OnScrollBarShow)
                {
                    OnScrollBarShow(this, new ScrollBarShowEventArgs(Direction.Horizontal, false));
                }
            }
        }

        private void SetVScrollBarValue()
        {
            if (0 == _range.Row)
            {
                return;
            }

            int itemCountPerPage = _range.Row * _range.Col;
            if (Items.Count > itemCountPerPage)
            {
                if (null != OnScrollBarShow && Items.Count>0 && Items.Count > itemCountPerPage)
                {
                    OnScrollBarShow(this, new ScrollBarShowEventArgs(Direction.Vetical, true,
                                    0,
                                    Items.Count / (itemCountPerPage) - 1 + Convert.ToInt32((Items.Count % (itemCountPerPage) > 0)),
                                    1,
                                    1));

                }
                
            }
            else
            {
                if (null != OnScrollBarShow)
                {
                    OnScrollBarShow(this, new ScrollBarShowEventArgs(Direction.Vetical, false));
                }
            }
        }

        private void Draw(Graphics g)
        {
            if (null == g)
            {
                return;
            }

            g.Clear(this.BackColor);
            int startIndex = _pageIndex * _range.Row * _range.Col + _colIndex * _range.Row;
            int endIndex = (_pageIndex+1) * _range.Row * _range.Col-1;
            if (endIndex >= Items.Count)
            {
                endIndex = Items.Count - 1;
            }

            int x = 0, y = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                x = (i - startIndex) / _range.Row;
                y = (i - startIndex) % _range.Row;
                ICustomDrawItem item = Items[i];
                if (null != item)
                {
                    int width = ItemWidth;
                    if (width > Width)
                    {
                        width = Width;
                    }
                    int height = ItemHeight;
                    if (height > Height)
                    {
                        height = Height;
                    }
                    if (i == SelectedtIndex)
                    {
                        using (SolidBrush brush = new SolidBrush(Color.SkyBlue))
                        {
                            g.FillRectangle(brush, x * (ItemWidth + ItemColPadding)+1,
                                              y * (ItemHeight + ItemRowPadding) + 1, width - 2, height - 2);
                        }
                    }
                    item.Draw(g, x * (ItemWidth + ItemColPadding),
                                    y * (ItemHeight + ItemRowPadding), width - 2, height-2);
                }
            }
        }

        /// <summary>
        /// 显示的行列数
        /// </summary>
        private struct Range
        {
            public int Row
            {
                get;
                set;
            }

            public int Col
            {
                get;
                set;
            }
        }
    }

    public delegate void OnScrollBarShow(object o,ScrollBarShowEventArgs e);

    public delegate void OnSelectedItemChanged(object o,ItemEventArgs e);

    public class ScrollBarShowEventArgs : EventArgs 
    {
        public Direction Direction
        {
            get;
            private set;
        }

        public int Min
        {
            get;
            private set;
        }

        public int Max
        {
            get;
            private set;
        }

        public bool Visible
        {
            get;
            private set;
        }

        public int SmallChange
        {
            get;
            private set;
        }


        public int LargeChange
        {
            get;
            private set;
        }

        public ScrollBarShowEventArgs(Direction direction,bool visible, int min=0, int max=0,int smallChange=0,int largeChange=0)
        {
            Direction = direction;
            Min = min;
            Max = max;
            SmallChange = smallChange;
            LargeChange = largeChange;
            Visible = visible;
        }
    }

    public class ItemEventArgs : EventArgs
    {
        public object Item
        {
            get;
            set;
        }

        public ItemEventArgs(object selectedItem)
            : base()
        {
            Item = selectedItem;
        }
    }

    [Flags]
    public enum Direction
    {
        Horizontal = 1,
        Vetical
    }
    
}
