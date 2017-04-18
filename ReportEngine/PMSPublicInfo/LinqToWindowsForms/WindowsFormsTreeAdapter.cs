using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.LinqToWindowsForms
{
    /// <summary>
    /// Defines an interface that must be implemented to generate the LinqToTree methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILinqTree<T>
    {
        IEnumerable<T> Children();

        T Parent { get; }
    }

    /// <summary>
    /// Adapts a Control to provide methods required for generate
    /// a Linq To Tree API
    /// </summary>
    public class WindowsFormsTreeAdapter : ILinqTree<Control>
    {
        private Control _item;

        public WindowsFormsTreeAdapter(Control item)
        {
            _item = item;
        }

        public IEnumerable<Control> Children()
        {
            foreach (var item in _item.Controls)
            {
                yield return (Control)item;
            }
        }

        public Control Parent
        {
            get
            {
                return _item.Parent;
            }
        }
    }
}
