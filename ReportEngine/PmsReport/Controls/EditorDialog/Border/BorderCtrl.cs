using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PMS.Libraries.ToolControls.Report.Controls.EditorDialog
{
    public partial class BorderCtrl : UserControl
    {
        private bool _hasLeft = true;
        private bool _hasTop = true;
        private bool _hasRight = true;
        private bool _hasBottom = true;
        private int _numbers = 7;

        private float _borderWidth =1;
        public float BorderWidth
        {
            get
            {
                return _borderWidth;
            }
            set 
            {
                _borderWidth = value;
            }
        }

        private DashStyle _dashStyle = DashStyle.Solid;
        public DashStyle DashStyle
        {
            get
            {
                return _dashStyle;
            }
            set
            {
                _dashStyle = value;
                Invalidate();
            }
        }

        private Color _borderColor = Color.Black;

        public bool HasLeft
        {
            get
            {
                return _hasLeft;
            }
            internal set
            {
                _hasLeft = value;
            }
        }

        public bool HasTop
        {
            get
            {
                return _hasTop;
            }
            internal set
            {
                _hasTop = value;
            }
        }

        public bool HasRight
        {
            get
            {
                return _hasRight;
            }
            internal set
            {
                _hasRight = value;
            }
        }

        public bool HasBottom
        {
            get
            {
                return _hasBottom;
            }
            internal set
            {
                _hasBottom = value;
            }
        }

        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            internal set
            {
                _borderColor = value;
            }
        }

        public BorderCtrl()
        {
            InitializeComponent();
        }

        public void Bind(IBorder border)
        {
            if (null == border)
            {
                return;
            }
            _hasLeft = border.HasLeftBorder;
            _hasRight = border.HasRightBorder;
            _hasBottom = border.HasBottomBorder;
            _hasTop = border.HasTopBorder;
            _borderColor = border.BorderColor;
            _dashStyle = border.DashStyle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawCorner(e.Graphics);
            DrawBorder(e.Graphics);
            base.OnPaint(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int width = Width / _numbers;
            int height = Height / _numbers;

            if (width != 0 && height != 0)
            {
                if (e.X >= 0 && e.X <= 2 * width &&
                   e.Y >= height && e.Y <= Height - height)
                {
                    _hasLeft = !_hasLeft;
                    Invalidate();
                }
                else if (e.Y >= 0 && e.Y < 2 * height &&
                        e.X >= width && e.X < Width - width)
                {
                    _hasTop = !_hasTop;
                    Invalidate();
                }
                else if (e.X >= Width - 2*width && e.X <= Width &&
                         e.Y >= height && e.Y <= Height - height)
                {
                    _hasRight = !_hasRight;
                    Invalidate();
                }
                else if (e.Y >= Height - 2*height && e.Y <= Height &&
                    e.X >= width && e.X < Width - width)
                {
                    _hasBottom = !_hasBottom;
                    Invalidate();
                }
            }
            base.OnMouseClick(e);
        }

        private void DrawCorner(Graphics g)
        {
            if (null == g)
            {
                return;
            }

            int width = Width / _numbers;
            int height = Height / _numbers;

            if (width == 0 || height == 0)
            {
                return;
            }

            using (Pen pen = new Pen(Brushes.Gray))
            {
               ;
                //Top Left Corner
                g.DrawLine(pen, width, height/2,width, height );
                g.DrawLine(pen, width/2, height, width,height);

                //Top Right Corner
                g.DrawLine(pen, Width-width-1, height/2, Width-width-1, height);
                g.DrawLine(pen, Width - width-1, height, Width - width/2, height);

                // Bottom Left Corner
                g.DrawLine(pen, width/2, Height - height-1, width, Height - height-1);
                g.DrawLine(pen, width, Height - height-1, width, Height-height/2);

                // Bottom Right Corner
                g.DrawLine(pen, Width - width-1, Height - height-1, Width - width/2, Height - height-1);
                g.DrawLine(pen, Width - width-1, Height - height-1, Width - width-1, Height-height/2);
            }


        }

        private void DrawBorder(Graphics g)
        {
            if (null == g)
            {
                return;
            }

            int width = Width / _numbers;
            int height = Height / _numbers;

            if (width == 0 || height == 0)
            {
                return;
            }

            using (SolidBrush sb = new SolidBrush(BorderColor))
            {
                using (Pen pen = new Pen(sb))
                {
                    pen.DashStyle = _dashStyle;
                    pen.Width = BorderWidth;
                    if (_hasLeft)
                    {
                        int left = width, top = height;
                        g.DrawLine(pen, left + BorderWidth / 2, top, left+BorderWidth/2, Height - height);
                    }
                    if (_hasTop)
                    {
                        int left = width, top = height;
                        g.DrawLine(pen, left, top + BorderWidth / 2, Width - width, top + BorderWidth / 2);
                    }
                    if (_hasRight)
                    {
                        int left = Width - width, top = height;
                        g.DrawLine(pen, left - BorderWidth / 2, top, left - BorderWidth / 2, Height - height);
                    }
                    if (_hasBottom)
                    {
                        int left = width;
                        int top = Height - height;
                        g.DrawLine(pen, left, top - BorderWidth / 2, Width - width, top - BorderWidth / 2);
                    }
                }
            }
        }
    }
}
