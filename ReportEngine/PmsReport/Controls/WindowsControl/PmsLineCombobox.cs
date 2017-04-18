using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using PMS.Libraries.ToolControls.Report.Win32;
using System.Windows;

namespace PMS.Libraries.ToolControls.Report.Controls
{
    public class PmsLineCombobox : ComboBox
    {
        private IntPtr _editPtr = IntPtr.Zero;
        public PmsLineCombobox()
            : base()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
        }



        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            try
            {
                DashStyle ds = (DashStyle)Convert.ToInt32(this.Items[e.Index]);
                if (e.State == DrawItemState.Selected)
                {
                    g.FillRectangle(Brushes.Bisque, e.Bounds.Left, e.Bounds.Left + e.Index * this.ItemHeight, this.Width, this.ItemHeight);
                }
                else
                {
                    g.FillRectangle(Brushes.White, e.Bounds.Left, e.Bounds.Left + e.Index * this.ItemHeight, this.Width, this.ItemHeight);
                }
                Brush brush = new SolidBrush(Color.Black);
                Pen pen = new Pen(brush, 2);
                pen.DashStyle = ds;
                g.DrawLine(pen, e.Bounds.Left, e.Bounds.Left + e.Bounds.Height / 2 - pen.Width / 2 + e.Index * this.ItemHeight,
                                e.Bounds.Right + e.Index * this.ItemHeight, e.Bounds.Left + e.Bounds.Height / 2 - pen.Width / 2 + e.Index * this.ItemHeight);
                brush.Dispose();
                pen.Dispose();
            }
            catch
            {

            }
            base.OnDrawItem(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextUpdate(e);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32Message.WM_ERASEBKGND:
                    
                    break;
                case Win32Message.WM_PAINT:
                    if (this.DropDownStyle == ComboBoxStyle.DropDown)
                    {
                        if (this._editPtr == IntPtr.Zero)
                        {
                            IntPtr ptr = new IntPtr();
                            ptr = IntPtr.Zero;
                            Win32API.EnumChildWindows(this.Handle, new FunctionPointer.EnumChildProc(this.EnumChildProc), ref ptr);
                            this._editPtr = ptr;
                        }
                        else
                        {
                            if (null != this.SelectedItem)
                            {
                                //Rect rect = new Rect();
                                //if (Win32API.GetClientRect(_editPtr, ref rect))
                                //{
                                //    Rectangle area = new Rectangle((int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height);
                                //    Graphics g = Graphics.FromHwnd(_editPtr);
                                //    g.FillRectangle(Brushes.White, area);
                                //    Pen pen = new Pen(Brushes.Black, 2);
                                //    pen.DashStyle = (DashStyle)Convert.ToInt32(this.SelectedItem);
                                //    g.DrawLine(pen, area.Left, area.Height / 2 - pen.Width / 2, area.Right, area.Height / 2 - pen.Width / 2);
                                //    pen.Dispose();
                                //    g.Dispose();
                                //}
                                Graphics g = Graphics.FromHwnd(_editPtr);
                                g.Clear(Color.White);
                                Pen pen = new Pen(Brushes.Black, 2);
                                pen.DashStyle = (DashStyle)Convert.ToInt32(this.SelectedItem);
                                g.DrawLine(pen, 0, this.ItemHeight / 2 - pen.Width / 2, this.Width, this.ItemHeight / 2 - pen.Width / 2);
                                pen.Dispose();
                                g.Dispose();
                            }
                        }
                    }
                    else
                    {

                        Rectangle rect = this.ClientRectangle;
                        Graphics g = this.CreateGraphics();
                        g.FillRectangle(Brushes.White, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
                        Pen pen = new Pen(Brushes.Black, 2);
                        pen.DashStyle = (DashStyle)Convert.ToInt32(this.SelectedItem);
                        g.DrawLine(pen, (float)rect.X, rect.Height / 2 - pen.Width / 2,
                                        (float)rect.Width, rect.Height / 2 - pen.Width / 2);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private bool EnumChildProc(IntPtr hWnd, ref IntPtr lParam)
        {
            lParam = hWnd;
            return false;
        }
    }
}
