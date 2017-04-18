using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DesignerLib.ShapeControl
{
    public interface IShape
    {
        void Draw(PaintEventArgs e);
    }
}
