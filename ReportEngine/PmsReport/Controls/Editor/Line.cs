using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace PMS.Libraries.ToolControls.ReportControls
{
    public class Line : IComboboxItem
    {
        public string Description
        {
            get;
            set;
        }

        public DashStyle DashStyle
        {
            get;
            set;
        }

        public Line(string description, DashStyle ds)
        {
            Description = description;
            DashStyle = ds;
        }

        public void DrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            using (Pen pen = new Pen(new SolidBrush(Color.Black)))
            {
                pen.Width = 2;
                pen.DashStyle = DashStyle;
                if (e.State == System.Windows.Forms.DrawItemState.HotLight)
                {
                    g.FillRectangle(new SolidBrush(Color.Blue), e.Bounds);
                    g.DrawLine(pen, new PointF(e.Bounds.Left, e.Bounds.Top + e.Bounds.Height / 2 - Pens.Red.Width / 2),
                                    new PointF(e.Bounds.Left + e.Bounds.Width, e.Bounds.Top + e.Bounds.Height / 2 - Pens.Red.Width / 2));
                }
                else if (((int)(e.State & System.Windows.Forms.DrawItemState.Selected) ) != 0)
                {
                    Color clr = Color.FromArgb(255,195,255,255);
                    g.FillRectangle(new SolidBrush(clr), e.Bounds);
                    g.DrawLine(pen, new PointF(e.Bounds.Left, e.Bounds.Top + e.Bounds.Height / 2 - Pens.Red.Width / 2),
                                          new PointF(e.Bounds.Left + e.Bounds.Width, e.Bounds.Top + e.Bounds.Height / 2 - Pens.Red.Width / 2));
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                    g.DrawLine(pen, new PointF(e.Bounds.Left, e.Bounds.Top + e.Bounds.Height / 2 - Pens.Red.Width / 2),
                                    new PointF(e.Bounds.Left + e.Bounds.Width, e.Bounds.Top + e.Bounds.Height / 2 - Pens.Red.Width / 2));
                }

            }
        }
    }

    public interface IComboboxItem
    {
        void DrawItem(System.Windows.Forms.DrawItemEventArgs e);
    }
}
