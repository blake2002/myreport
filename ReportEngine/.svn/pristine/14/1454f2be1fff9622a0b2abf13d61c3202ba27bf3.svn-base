using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestControl
{
    public partial class UserControl1 : UserControl
    {
        private Dictionary<string, Object> _dic = new Dictionary<string, object>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Dictionary<string, Object> Dic
        {
            get
            {
                return _dic;
            }
            set
            {
                _dic = value;
            }
        }

        public UserControl1()
        {
            InitializeComponent();
            Dic.Add("abc", true);

        }
    }
}
