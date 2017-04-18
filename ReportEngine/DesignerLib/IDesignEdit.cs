using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.Controls.Design
{
    public interface IDesignEdit
    {
        bool IsEdit
        {
            get;
        }

        void EndEdit();
        void CancleEdit();
        void BeginEdit();
    }
}
