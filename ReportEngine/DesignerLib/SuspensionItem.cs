using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MES.Controls.Design
{
    public class SuspensionItem:IDisposable
    {
        public Image Img
        {
            get;
            set;
        }

        public string Text
        {
            get;
            set;
        }

        public string TipText
        {
            get;
            set;
        }

        public Action Handler
        {
            get;
            set;
        }

        internal bool IsMouseOn
        {
            get;
            set;
        }

        public SuspensionItem(Image img, string txt, string tipText)
            : this(img, txt, tipText,null)
        { 
        }

        public SuspensionItem(Image img, string txt, string tipText, Action handler)
        {
            Img = img;
            Text = txt;
            TipText = tipText;
            Handler = handler;
        }

        public void Dispose()
        {
            if (null != Img)
            {
                Img.Dispose();
                Img = null;
                Handler = null;
            }
        }
          
    }
}
