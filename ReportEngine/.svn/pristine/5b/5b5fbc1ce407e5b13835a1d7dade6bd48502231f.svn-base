using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MES.Controls.Design
{
    internal class AdornerPanel:IDisposable
    {
        public static readonly int IconHeight = 16;
        public static readonly int IconWidth = 16;
        public static readonly int Padding = 6;
        public static readonly int PanelHeight = 24;

        private List<SuspensionItem> _itemList = null;
        public List<SuspensionItem> ItemList
        {
            get { return _itemList; }
        }
        private int _currentIndex = 0;

        public Point Location
        {
            get;
            set;
        }

        private int _displayCountPerPage = 0;

        private Triangle _leftTriangle = null;
        private Triangle _rightTriangle = null;

        private Control _ctrl = null;

        public int PrevWidth
        {
            get;
            set;
        }

        public int Width
        {
            get
            {
                return _ctrl.Width;
            }
        }

        private int _width = 0;

        private int _triangleWidthIcon = 2;

        public AdornerPanel()
            : this(null)
        {

        }

        public AdornerPanel(Control ctrl)
        {
            _itemList = new List<SuspensionItem>();
            _ctrl = ctrl;
            if (null != _ctrl)
            {
                _displayCountPerPage = _ctrl.Width / (IconWidth + Padding);
                _ctrl.SizeChanged += new EventHandler(Ctrl_SizeChanged);
            }
        }

        private void Ctrl_SizeChanged(object sender, EventArgs e)
        {
            _displayCountPerPage = (_ctrl.Width - _triangleWidthIcon *2- Padding) / (IconWidth + Padding);
            _currentIndex = 0;
        }

        public void AddItem(SuspensionItem si)
        {
            if (null != si)
            {
                if (!_itemList.Contains(si))
                _itemList.Add(si);
            }
        }

        public void AddItems(SuspensionItem[] items)
        {
            if (null != items)
            {
                _itemList.AddRange(items);
            }
        }

        public bool RaiseMouseDown(MouseButtons button, Point point)
        {
            if (button == MouseButtons.Left)
            {
                if (point.X > Padding && point.X < Width - Padding)
                {
                    int x = point.X - Padding;
                    int number = x / (IconWidth + Padding);
                    int mode = x % (IconWidth + Padding);
                    if (mode > 0 && mode <= IconWidth)
                    {
                        number++;

                        int index = _currentIndex + number - 1;
                        if (index < _itemList.Count && number <= _displayCountPerPage)
                        {
                            SuspensionItem si = _itemList[index];
                            if (null != si && null != si.Handler)
                            {
                                si.Handler();
                                return true;
                            }
                        }
                    }
                }
                else if (null != _leftTriangle && point.X < Padding && point.X > 0)
                {
                    _currentIndex -= _displayCountPerPage;
                    if (_currentIndex == 0)
                    {
                        _leftTriangle = null;
                    }
                    return true;
                }
                else if (null != _rightTriangle && point.X > (Width - Padding) && point.X < Width)
                {
                    _currentIndex += _displayCountPerPage;
                    if (_currentIndex + _displayCountPerPage >= _itemList.Count)
                    {
                        _rightTriangle = null;
                    }
                    return true;
                }
            }
            return false;
        }

        public bool RaiseMouseMove(MouseButtons button, Point point)
        {
            if (button == MouseButtons.None)
            {
                if (point.X > Padding && point.X < Width - Padding && point.Y < PanelHeight)
                {
                    int x = point.X - Padding;
                    int number = x / (IconWidth + Padding);
                    int mode = x % (IconWidth + Padding);
                    if (mode > 0 && mode <= IconWidth)
                    {
                        number++;
                        int index = _currentIndex + number - 1;
                        if (index < _itemList.Count && number <= _displayCountPerPage)
                        {
                            int count = _itemList.Count - _currentIndex;
                            if (count > _displayCountPerPage)
                            {
                                count = _displayCountPerPage;
                            }
                            int tempIndex = 0;
                            for (int i = 0; i < count; i++)
                            {
                                tempIndex = _currentIndex + i;
                                SuspensionItem tmp = _itemList[tempIndex];
                                if (tempIndex == index)
                                {
                                    tmp.IsMouseOn = true;
                                }
                                else
                                {
                                    tmp.IsMouseOn = false;
                                }
                            }
                            return true;
                        }
                    }
                }
                else
                {
                    int count = _itemList.Count - _currentIndex;
                    if (count > _displayCountPerPage)
                    {
                        count = _displayCountPerPage;
                    }
                    int tempIndex = 0;
                    for (int i = 0; i < count; i++)
                    {
                        tempIndex = _currentIndex + i;
                        SuspensionItem tmp = _itemList[tempIndex];
                        tmp.IsMouseOn = false;
                    }
                }

                if (null != _leftTriangle)
                {
                    _leftTriangle.IsMouseOn = false;
                    if (point.X < Padding && point.X>0)
                    {
                        _leftTriangle.IsMouseOn = true;
                        return true;
                    }
                }
                if (null != _rightTriangle)
                {
                    _rightTriangle.IsMouseOn = false;
                    if (point.X > Width - Padding && point.X < Width)
                    {
                        _rightTriangle.IsMouseOn = true;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RaiseMouseUp(MouseButtons button, Point point)
        {
            if (button == MouseButtons.Left)
            {
                if (point.X > Padding && point.X < Width - Padding)
                {
                    int x = point.X - Padding;
                    int number = x / (IconWidth + Padding);
                    int mode = x % (IconWidth + Padding);
                    if (mode > 0 && mode <= IconWidth)
                    {
                        number++;

                        int index = _currentIndex + number - 1;
                        if (index < _itemList.Count && number <= _displayCountPerPage)
                        {
                            SuspensionItem si = _itemList[index];
                            if (null != si && null != si.Handler)
                            {
                                si.Handler();
                                return true;
                            }
                        }
                    }
                }
                else if (null != _leftTriangle && point.X < Padding && point.X > 0)
                {
                    _currentIndex -= _displayCountPerPage;
                    if (_currentIndex == 0)
                    {
                        _leftTriangle = null;
                    }
                    return true;
                }
                else if (null != _rightTriangle && point.X > (Width - Padding) && point.X < Width)
                {
                    _currentIndex += _displayCountPerPage;
                    if (_currentIndex + _displayCountPerPage >= _itemList.Count)
                    {
                        _rightTriangle = null;
                    }
                    return true;
                }
            }
            return false;
        }

        public bool RaiseMouseLeave()
        {
            int count = _itemList.Count - _currentIndex;
            if (count > _displayCountPerPage)
            {
                count = _displayCountPerPage;
            }
            int index = 0;
            for (int i = 0; i < count; i++)
            {
                index = _currentIndex + i;
                SuspensionItem tmp = _itemList[index];
                tmp.IsMouseOn = false;
            }
            if (null != _leftTriangle)
            {
                _leftTriangle.IsMouseOn = false;
            }
            if (null != _rightTriangle)
            {
                _rightTriangle.IsMouseOn = false;
            }
            return true;
        }

        public void Paint(Graphics g)
        {
            if (null == g)
            {
                return;
            }

            if (null == _itemList)
            {
                return;
            }
            if (null != _itemList)
            {
                if (null == _leftTriangle && _currentIndex > 0)
                {
                    _leftTriangle = new Triangle();
                }
                else if (null != _leftTriangle && _currentIndex == 0)
                {
                    _leftTriangle = null;
                }
                if (null == _rightTriangle &&
                    (_itemList.Count - _currentIndex) > _displayCountPerPage)
                {
                    _rightTriangle = new Triangle();
                }
                else if(null != _rightTriangle && 
                    (_itemList.Count - _currentIndex) <= _displayCountPerPage)
                {
                    _rightTriangle = null;
                }
            }

            if (_width == 0)
            {
                _width = Width;
              
            }
            PrevWidth = _width;
            using (Bitmap bmp = new Bitmap(Width, PanelHeight))
            {
                Graphics bmpGraphics = Graphics.FromImage(bmp);
                try
                {
                    if (null != _ctrl && null != _ctrl.Parent)
                    {
                        Color color = Color.FromArgb(255,247, 247, 247);
                        //Control parent = _ctrl.Parent;
                        // while(null != parent)
                        //{
                        //    if(parent.BackColor == Color.Transparent)
                        //    {
                        //        parent = parent.Parent;
                        //    }
                        //    else
                        //    {
                        //        color = parent.BackColor;
                        //        break;
                        //    }
                        //}
                        // if (Color.Empty == color)
                        // {
                        //     color = Color.White;
                        // }
                        bmpGraphics.Clear(color);
                    }
                    int count = _itemList.Count - _currentIndex;
                    if (count > _displayCountPerPage)
                    {
                        count = _displayCountPerPage;
                    }
                    int index = 0;
                    SuspensionItem moveItem = null;
                    int moveX = 0;
                    int moveY = 0;
                    for (int i = 0; i < count; i++)
                    {
                        index = _currentIndex + i;
                        SuspensionItem si = _itemList[index];
                        if (null != si)
                        {
                            int x = i * (IconWidth + Padding) + Padding + _triangleWidthIcon;
                            int y = (bmp.Height - IconHeight) / 2;
                            if (si.IsMouseOn && null != si.Img)
                            {
                                moveItem = si;
                                moveX = x;
                                moveY = y;
                                using(SolidBrush sb = new SolidBrush(Color.SkyBlue))
                                {
                                    bmpGraphics.FillRectangle(sb, x, y, IconWidth, IconHeight);
                                }
                            }
                            if (null == si.Img)
                            {
                                if (null != _ctrl)
                                    si.Text = si.TipText = _ctrl.Name;
                                DrawTipText(si, x, y*2, bmpGraphics);
                                continue;
                            }
                            bmpGraphics.DrawImage(si.Img, x, y, IconWidth, IconHeight);
                            if (si.IsMouseOn)
                            {
                                bmpGraphics.DrawRectangle(Pens.Red, x, y, IconWidth, IconHeight);
                            }
                        }
                    }

                    
                    if (null != moveItem)
                    {
                        if (null != moveItem.Img)
                            DrawTipText(moveItem, moveX, moveY, bmpGraphics);
                    }

                    int interval = (AdornerPanel.PanelHeight - IconHeight) / 2;
                    if (null != _leftTriangle)
                    {
                        Point pt1 = new Point(0, bmp.Height / 2);
                        Point pt2 = new Point(Padding, interval);
                        Point pt3 = new Point(Padding, bmp.Height - interval);
                        _leftTriangle.Rect = new Rectangle(0, interval, Padding, pt3.Y - pt2.Y);
                        _leftTriangle.Paint(bmpGraphics, pt1, pt2, pt3);
                    }
                    if (null != _rightTriangle)
                    {
                        Point pt1 = new Point(Width, bmp.Height / 2);
                        Point pt2 = new Point(Width - Padding, interval);
                        Point pt3 = new Point(Width - Padding, bmp.Height - interval);
                        _rightTriangle.Rect = new Rectangle(Width - Padding, interval, Padding, pt3.Y - pt2.Y);
                        _rightTriangle.Paint(bmpGraphics,pt1,pt2,pt3);
                    }
                    bmpGraphics.DrawRectangle(Pens.Black, 0, 0, bmp.Width-1, bmp.Height-1);
                    g.DrawImage(bmp, Location.X, Location.Y);
                }
                finally
                {
                    bmpGraphics.Dispose();
                }
                _width = _ctrl.Width;
            }
        }

        private void DrawTipText(SuspensionItem si, int x, int y,Graphics g)
        {
            if (null == g || null == si)
            {
                return;
            }

            if (string.IsNullOrEmpty(si.TipText))
            {
                return;
            }

            float left = x + IconWidth / 2 + 5;
            float top = y + IconHeight / 2 + 2;

            using (Font fnt = new Font("宋体", 9))
            {
                SizeF sf = g.MeasureString(si.TipText, fnt);
                if (left + sf.Width > Width)
                {
                    left = left - sf.Width;
                    if (left < 0)
                    {
                        sf.Width = Width;
                        left = 0;
                    }
                }
                if (top + sf.Height > PanelHeight)
                {
                    top = top - sf.Height;
                    if (top < 0)
                    {
                        top = 0;
                        sf.Height = PanelHeight / 2;
                    }
                }

                StringFormat formate = new StringFormat();
                formate.LineAlignment = StringAlignment.Center;
                formate.Alignment = StringAlignment.Center;
                RectangleF rect = new RectangleF(left, top, sf.Width, sf.Height);
                LinearGradientBrush sb = new LinearGradientBrush(rect, Color.Snow, Color.WhiteSmoke, 90);
                try
                {
                    g.FillRectangle(sb, rect);
                }
                finally
                {
                    sb.Dispose();
                }
                g.DrawString(si.TipText, fnt, Brushes.Black, rect);
            }
        }

        public void Dispose()
        { 
            if(null != _itemList)
            {
                foreach (SuspensionItem si in _itemList)
                {
                    si.Dispose();
                }
            }
        }

        public class Triangle
        {
            public Rectangle Rect
            {
                get;
                set;
            }   

            public bool IsMouseOn
            {
                get;
                set;
            }

            public void Paint(Graphics g, Point point1, Point point2, Point point3)
            {
                if (null == g)
                {
                    return;
                }

                if (IsMouseOn)
                {
                    using (SolidBrush sb = new SolidBrush(Color.SkyBlue))
                    {
                        g.FillRectangle(sb, Rect);
                    }
                }

                
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddPolygon(new Point[] { point1, point2, point3 });
                    g.FillPath(Brushes.Black, path);
                }
            }
        }

    }
}
