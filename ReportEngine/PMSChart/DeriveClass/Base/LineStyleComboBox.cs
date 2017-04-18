using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PMS.Libraries.ToolControls.PMSChart
{
    /// <summary>
    /// LineStyleComboBox控件
    /// “线型”下拉选择控件，使用时需先调用Init()方法进行初始化
    /// </summary>
    public class LineStyleComboBox : ComboBox
    {
        public LineStyleComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void Init()
        {
            this.Items.Clear();
            this.Items.Add(ChartDashStyle.Solid);
            this.Items.Add(ChartDashStyle.Dash);
            this.Items.Add(ChartDashStyle.Dot);
            this.Items.Add(ChartDashStyle.DashDot);
            this.Items.Add(ChartDashStyle.DashDotDot);
            this.SelectedIndex = 0;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)//判断是否需要重绘
            {
                //鼠标选中在这个项上
                if ((e.State & DrawItemState.Selected) != 0)
                {
                    //渐变画刷
                    LinearGradientBrush brush = new LinearGradientBrush(e.Bounds, Color.FromArgb(255, 251, 237),
                                                     Color.FromArgb(255, 236, 181), LinearGradientMode.Vertical);
                    //填充区域
                    Rectangle borderRect = new Rectangle(3, e.Bounds.Y, e.Bounds.Width - 5, e.Bounds.Height - 2);

                    e.Graphics.FillRectangle(brush, borderRect);

                    //画边框
                    Pen pen = new Pen(Color.FromArgb(229, 195, 101));
                    e.Graphics.DrawRectangle(pen, borderRect);
                }
                else
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }


                Pen MyPen = new Pen(new SolidBrush(Color.Black), 1);
                MyPen.DashStyle = (DashStyle)(this.Items[e.Index]);
                e.Graphics.DrawLine(MyPen, e.Bounds.Left + 3, e.Bounds.Top + 8, e.Bounds.Right - 4, e.Bounds.Top + 8);
            }
        }
    }


}
