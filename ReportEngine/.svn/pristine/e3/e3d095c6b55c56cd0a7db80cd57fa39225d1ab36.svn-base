using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PMS.Libraries.ToolControls.Report.Win32;
using System.Windows.Forms;
//using System.Windows.Controls;
namespace PMS.Libraries.ToolControls.ReportControls
{
    public class LineCombobox:System.Windows.Forms.ComboBox
    {
        public LineCombobox()
            : base()
        {
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            //this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            IComboboxItem item = this.Items[e.Index] as IComboboxItem;
            if (null != item)
            {
                //if (e.Index == 5)
                //{
                //    e.DrawBackground();
                //    int startX = e.Bounds.Left + 5;
                //    int startY = (e.Bounds.Y);
                //    Point p1 = new Point(startX, startY);
                //    int endX = e.Bounds.Right - 5;
                //    int endY = (e.Bounds.Y);
                //    ComboBoxItem cbitem = (ComboBoxItem)this.Items[e.Index];
                //    Point p2 = new Point(endX, endY);
                //    base.OnDrawItem(e);
                //    Pen SolidmyPen = new Pen(item.foreColor, 1);
                //    Pen DashedPen = new Pen(item.foreColor, 1);
                //    DashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                //    Pen DashDot = new Pen(item.foreColor, 1);
                //    DashDot.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                //    // Pen DashedPen = new Pen(item.foreColor, (Int32)this.Items[e.Index]);

                //    Bitmap myBitmap = new Bitmap(cbitem.);
                //    Graphics graphicsObj;
                //    graphicsObj = Graphics.FromImage(myBitmap);
                //}
                //else
                    item.DrawItem(e);
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == Win32Message.WM_PAINT && m.HWnd == Handle)
            {
                if (this.DroppedDown)
                {
                    return;
                }
                IntPtr ptr = IntPtr.Zero;
                Win32API.EnumChildWindows(Handle, new FunctionPointer.EnumChildProc(EnumChildProc), ref ptr);
            }
        }

        private bool EnumChildProc(IntPtr hWnd, ref IntPtr lParam)
        {
            if (null == this.SelectedItem)
            {
                return false;
            }
            using (Graphics g = Graphics.FromHwnd(hWnd))
            {
                IComboboxItem item = this.SelectedItem as IComboboxItem;
                DrawItemEventArgs e = new DrawItemEventArgs(g, Font, new Rectangle(0, Height / 2, Width, Height / 2), 0, DrawItemState.NoFocusRect);
                item.DrawItem(e);
            }
            return false;
        }
    }
}
