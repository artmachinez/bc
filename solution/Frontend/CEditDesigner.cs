using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mshtml;

namespace Frontend
{
    class CEditDesigner : IHTMLEditDesigner
    {
        public void PostEditorEventNotify(int disp, IHTMLEventObj eventObj){ }
        public void TranslateAccelerator(int disp, IHTMLEventObj eventObj) { }
        public void PostHandleEvent(int disp, IHTMLEventObj eventObj) { }

        public void PreHandleEvent(int disp, IHTMLEventObj eventObj)
        {
            throw new Exception("awey2");
        }


    }
}
