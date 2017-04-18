using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Design;

namespace PMS.Libraries.ToolControls.PMSChart
{
    public partial class LabelFit : UserControl
    {
        private IWindowsFormsEditorService service = null;
        public LabelFit(IWindowsFormsEditorService service1)
        {
            InitializeComponent();
            service = service1;
        }
        private LabelAutoFitStyles labelAutoFitStyle;
        public LabelAutoFitStyles LabelAutoFitStyle
        {
            get
            {
                labelAutoFitStyle = LabelAutoFitStyles.None;
                //for (int i = 0; i < 7; i++)
                {
                    //if (checkedListBox1.GetItemChecked(i))
                        //labelAutoFitStyle |= (LabelAutoFitStyles)(2 ^ i);
                }

                if (checkedListBox1.GetItemChecked(0))
                    labelAutoFitStyle |= LabelAutoFitStyles.IncreaseFont;
                if (checkedListBox1.GetItemChecked(1))
                    labelAutoFitStyle |= LabelAutoFitStyles.DecreaseFont;
                if (checkedListBox1.GetItemChecked(2))
                    labelAutoFitStyle |= LabelAutoFitStyles.StaggeredLabels;
                if (checkedListBox1.GetItemChecked(3))
                    labelAutoFitStyle |= LabelAutoFitStyles.LabelsAngleStep30;
                if (checkedListBox1.GetItemChecked(4))
                    labelAutoFitStyle |= LabelAutoFitStyles.LabelsAngleStep45;
                if (checkedListBox1.GetItemChecked(5))
                    labelAutoFitStyle |= LabelAutoFitStyles.LabelsAngleStep90;
                if (checkedListBox1.GetItemChecked(6))
                    labelAutoFitStyle |= LabelAutoFitStyles.WordWrap;
                return labelAutoFitStyle;
            }
            set
            {
                labelAutoFitStyle = value;
            }
        }
        private void LabelFit_Load(object sender, EventArgs e)
        {
            //for (int i = 0; i < 7; i++)
            {
                //if ((labelAutoFitStyle & ((LabelAutoFitStyles)(2 ^ i))) != 0)
                 
                if ((labelAutoFitStyle & LabelAutoFitStyles.IncreaseFont) != 0)
                    checkedListBox1.SetItemChecked(0, true);

                if ((labelAutoFitStyle & LabelAutoFitStyles.DecreaseFont) != 0)
                    checkedListBox1.SetItemChecked(1, true);
                if ((labelAutoFitStyle & LabelAutoFitStyles.StaggeredLabels) != 0)
                    checkedListBox1.SetItemChecked(2, true);
                if ((labelAutoFitStyle & LabelAutoFitStyles.LabelsAngleStep30) != 0)
                    checkedListBox1.SetItemChecked(3, true);
                if ((labelAutoFitStyle & LabelAutoFitStyles.LabelsAngleStep45) != 0)
                    checkedListBox1.SetItemChecked(4, true);
                if ((labelAutoFitStyle & LabelAutoFitStyles.LabelsAngleStep90) != 0)
                    checkedListBox1.SetItemChecked(5, true);
                if ((labelAutoFitStyle & LabelAutoFitStyles.WordWrap) != 0)
                    checkedListBox1.SetItemChecked(6, true);
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                service.CloseDropDown();
                return true; ;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void LabelFit_Leave(object sender, EventArgs e)
        {
            service.CloseDropDown();
        }
    }
}
