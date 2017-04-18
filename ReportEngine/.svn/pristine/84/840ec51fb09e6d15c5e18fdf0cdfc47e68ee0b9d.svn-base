using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class BorderAndBackEditor : Form
    {
        public BorderAndBackEditor()
        {
            InitializeComponent();
            cboBorderDashStyleInit();
            cboBackHatchStyleInit();
            cboBackGradientStyleInit();
            ccboBackColor.SelectedItem = Color.Empty;
            ccboBackSecondColor.SelectedItem = Color.Empty;
        }

        public Color BorderColor;
        public ChartDashStyle BorderDashStyle;
        public int BorderWidth;
        public new Color BackColor;
        public Color BackSecondColor;
        public ChartHatchStyle BackHatchStyle;
        public GradientStyle BackGradientStyle;

        private void BorderAndBackEditor_Load(object sender, EventArgs e)
        {
            ccboBorderColor.SelectedItem = BorderColor;
            cboBorderDashStyle.SelectedItem = BorderDashStyle;
            nudBorderWidth.Value = BorderWidth;
            ccboBackColor.SelectedItem = BackColor;
            ccboBackSecondColor.SelectedItem = BackSecondColor;
            cboBackHatchStyle.SelectedItem = BackHatchStyle;
            cboBackGradientStyle.SelectedItem = BackGradientStyle;

            chartPreview.BorderColor = BorderColor;
            chartPreview.BorderDashStyle = BorderDashStyle;
            chartPreview.BorderWidth = BorderWidth;
            chartPreview.BackColor = BackColor;
            chartPreview.BackSecondaryColor = BackSecondColor;
            chartPreview.BackHatchStyle = BackHatchStyle;
            chartPreview.BackGradientStyle = BackGradientStyle;
        }

        private void cboBackGradientStyleInit()
        {
            cboBackGradientStyle.Items.Clear();
            cboBackGradientStyle.Items.Add(GradientStyle.None);
            cboBackGradientStyle.Items.Add(GradientStyle.LeftRight);
            cboBackGradientStyle.Items.Add(GradientStyle.TopBottom);
            cboBackGradientStyle.Items.Add(GradientStyle.Center);
            cboBackGradientStyle.Items.Add(GradientStyle.DiagonalLeft);
            cboBackGradientStyle.Items.Add(GradientStyle.DiagonalRight);
            cboBackGradientStyle.Items.Add(GradientStyle.HorizontalCenter);
            cboBackGradientStyle.Items.Add(GradientStyle.VerticalCenter);
            cboBackGradientStyle.SelectedIndex = 0;
        }

        private void cboBackGradientStyle_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color black = Color.Black;
            Color white = Color.White;
            if (ccboBackColor.SelectedItem != null)
            {
                black = ccboBackColor.SelectedItem;
            }
            if (ccboBackSecondColor.SelectedItem != null)
            {
                white = ccboBackSecondColor.SelectedItem;
            }
            if (black == Color.Empty)
            {
                black = Color.Black;
            }
            if (white == Color.Empty)
            {
                white = Color.White;
            }
            if (black == white)
            {
                white = Color.FromArgb(black.B, black.R, black.G);
            }
            if ((GradientStyle)(cboBackHatchStyle.Items[e.Index]) != GradientStyle.None)
            {
                Brush brush = GetGradientBrush(GetGraphRect(e), black, white, (GradientStyle)(cboBackGradientStyle.Items[e.Index]));
                e.Graphics.FillRectangle(brush, GetGraphRect(e));
                brush.Dispose();
            }
            DrawComBoBoxText(cboBackGradientStyle, e);
        }

        Brush GetGradientBrush(RectangleF rectangle, Color firstColor, Color secondColor, GradientStyle type)
        {
            rectangle.Inflate(1f, 1f);
            Brush brush = null;
            float angle = 0f;
            if ((rectangle.Height == 0f) || (rectangle.Width == 0f))
            {
                return new SolidBrush(Color.Black);
            }
            if ((type == GradientStyle.LeftRight) || (type == GradientStyle.VerticalCenter))
            {
                angle = 0f;
            }
            else if ((type == GradientStyle.TopBottom) || (type == GradientStyle.HorizontalCenter))
            {
                angle = 90f;
            }
            else if (type == GradientStyle.DiagonalLeft)
            {
                angle = (float)((Math.Atan((double)(rectangle.Width / rectangle.Height)) * 180.0) / 3.1415926535897931);
            }
            else if (type == GradientStyle.DiagonalRight)
            {
                angle = (float)(180.0 - ((Math.Atan((double)(rectangle.Width / rectangle.Height)) * 180.0) / 3.1415926535897931));
            }
            if ((((type == GradientStyle.TopBottom) || (type == GradientStyle.LeftRight)) || ((type == GradientStyle.DiagonalLeft) || (type == GradientStyle.DiagonalRight))) || ((type == GradientStyle.HorizontalCenter) || (type == GradientStyle.VerticalCenter)))
            {
                RectangleF rect = new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                if (type == GradientStyle.HorizontalCenter)
                {
                    rect.Height /= 2f;
                    LinearGradientBrush brush2 = new LinearGradientBrush(rect, firstColor, secondColor, angle);
                    brush = brush2;
                    brush2.WrapMode = WrapMode.TileFlipX;
                    return brush;
                }
                if (type == GradientStyle.VerticalCenter)
                {
                    rect.Width /= 2f;
                    LinearGradientBrush brush3 = new LinearGradientBrush(rect, firstColor, secondColor, angle);
                    brush = brush3;
                    brush3.WrapMode = WrapMode.TileFlipX;
                    return brush;
                }
                return new LinearGradientBrush(rectangle, firstColor, secondColor, angle);
            }
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rectangle);
            PathGradientBrush brush4 = new PathGradientBrush(path);
            brush = brush4;
            brush4.CenterColor = firstColor;
            Color[] colorArray = new Color[] { secondColor };
            brush4.SurroundColors = colorArray;
            if (path != null)
            {
                path.Dispose();
            }
            return brush;
        }

        private void cboBackHatchStyleInit()
        {
            cboBackHatchStyle.Items.Clear();
            cboBackHatchStyle.Items.Add(ChartHatchStyle.None);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.BackwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Cross);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DarkDownwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DarkHorizontal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DarkUpwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DarkVertical);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DashedDownwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DashedHorizontal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DashedUpwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DashedVertical);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DiagonalBrick);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DiagonalCross);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Divot);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DottedDiamond);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.DottedGrid);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.ForwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Horizontal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.HorizontalBrick);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.LargeCheckerBoard);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.LargeConfetti);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.LargeGrid);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.LightDownwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.LightHorizontal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.LightUpwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.LightVertical);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.NarrowHorizontal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.NarrowVertical);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.OutlinedDiamond);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent05);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent10);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent20);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent25);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent30);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent40);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent50);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent60);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent70);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent75);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent80);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Percent90);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Plaid);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Shingle);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.SmallCheckerBoard);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.SmallConfetti);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.SmallGrid);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.SolidDiamond);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Sphere);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Trellis);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Vertical);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Wave);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.Weave);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.WideDownwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.WideUpwardDiagonal);
            cboBackHatchStyle.Items.Add(ChartHatchStyle.ZigZag);
            cboBackHatchStyle.SelectedIndex = 0;
        }

        private void cboBackHatchStyle_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color black = Color.Black;
            Color white = Color.White;
            if (ccboBackColor.SelectedItem != null)
            {
                black = ccboBackColor.SelectedItem;
            }
            if (ccboBackSecondColor.SelectedItem != null)
            {
                white = ccboBackSecondColor.SelectedItem;
            }
            if (black == Color.Empty)
            {
                black = Color.Black;
            }
            if (white == Color.Empty)
            {
                white = Color.White;
            }
            if (black == white)
            {
                white = Color.FromArgb(black.B, black.R, black.G);
            }
            if ((ChartHatchStyle)(cboBackHatchStyle.Items[e.Index]) != ChartHatchStyle.None)
            {
                Brush brush = GetHatchBrush((ChartHatchStyle)(cboBackHatchStyle.Items[e.Index]), black, white);
                e.Graphics.FillRectangle(brush, GetGraphRect(e));
                brush.Dispose();
            }
            DrawComBoBoxText(cboBackHatchStyle, e);
        }

        private Brush GetHatchBrush(ChartHatchStyle hatchStyle, Color backColor, Color foreColor)
        {
            return new HatchBrush((HatchStyle)Enum.Parse(typeof(HatchStyle), hatchStyle.ToString()), foreColor, backColor);
        }

        private void cboBorderDashStyleInit()
        {
            cboBorderDashStyle.Items.Clear();
            cboBorderDashStyle.Items.Add(ChartDashStyle.NotSet);
            cboBorderDashStyle.Items.Add(ChartDashStyle.Solid);
            cboBorderDashStyle.Items.Add(ChartDashStyle.Dash);
            cboBorderDashStyle.Items.Add(ChartDashStyle.Dot);
            cboBorderDashStyle.Items.Add(ChartDashStyle.DashDot);
            cboBorderDashStyle.Items.Add(ChartDashStyle.DashDotDot);
            cboBorderDashStyle.SelectedIndex = 0;
        }

        private void cboBorderDashStyle_DrawItem(object sender, DrawItemEventArgs e)
        {
            Color black = Color.Black;
            if (ccboBorderColor.SelectedItem != null)
            {
                black = ccboBorderColor.SelectedItem;
            }
            if (black == Color.Empty)
            {
                black = Color.Black;
            }
            if ((ChartDashStyle)(cboBorderDashStyle.Items[e.Index]) != ChartDashStyle.NotSet)
            {
                Pen MyPen = new Pen(black);
                MyPen.DashStyle = (DashStyle)(cboBorderDashStyle.Items[e.Index]);
                e.Graphics.DrawLine(MyPen, e.Bounds.Left + 3, e.Bounds.Top + e.Bounds.Height / 2, e.Bounds.Left + e.Bounds.Width / 4, e.Bounds.Top + e.Bounds.Height / 2);
                MyPen.Dispose();
            }
            DrawComBoBoxText(cboBorderDashStyle, e);
        }

        private static Rectangle GetGraphRect(DrawItemEventArgs e)
        {
            Rectangle rect = new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, e.Bounds.X + e.Bounds.Width / 4 - 2, e.Bounds.Height - 2);
            return rect;
        }

        private void DrawComBoBoxText(ComboBox comboBox, DrawItemEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), Font, new SolidBrush(Color.Black),
                new Rectangle(e.Bounds.X + e.Bounds.Width / 4 + 3, e.Bounds.Y, e.Bounds.Width * 3 / 4 - 3, e.Bounds.Height), sf);
        }

        private void cboBorderDashStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartPreview.BorderlineDashStyle = (ChartDashStyle)cboBorderDashStyle.SelectedItem;
        }

        private void ccboBorderColor_SelectColorChanged(object sender, Color OldColor, Color NewColor)
        {
            chartPreview.BorderlineColor = ccboBorderColor.SelectedItem;
        }

        private void nudBorderWidth_ValueChanged(object sender, EventArgs e)
        {
            chartPreview.BorderlineWidth = (int)nudBorderWidth.Value;
        }

        private void cboBackHatchStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartPreview.BackHatchStyle = (ChartHatchStyle)cboBackHatchStyle.SelectedItem;
        }

        private void ccboBackColor_SelectColorChanged(object sender, Color OldColor, Color NewColor)
        {
            chartPreview.BackColor = ccboBackColor.SelectedItem;
        }

        private void ccboBackSecondColor_SelectColorChanged(object sender, Color OldColor, Color NewColor)
        {
            chartPreview.BackSecondaryColor = ccboBackSecondColor.SelectedItem;
        }

        private void cboBackGradientStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            chartPreview.BackGradientStyle = (GradientStyle)cboBackGradientStyle.SelectedItem;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            BorderColor = ccboBorderColor.SelectedItem;
            BorderDashStyle = (ChartDashStyle)cboBorderDashStyle.SelectedItem;
            BorderWidth = (int)nudBorderWidth.Value;
            BackColor = ccboBackColor.SelectedItem;
            BackSecondColor = ccboBackSecondColor.SelectedItem;
            BackHatchStyle = (ChartHatchStyle)cboBackHatchStyle.SelectedItem;
            BackGradientStyle = (GradientStyle)cboBackGradientStyle.SelectedItem;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
