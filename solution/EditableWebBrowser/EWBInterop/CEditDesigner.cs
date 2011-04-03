using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EditableWebBrowser.EWBInterop
{
    class CEditDesigner : EWBInterop.IHTMLEditDesigner
    {

        #region IHTMLEditDesigner Members

        public void PostEditorEventNotify(int disp, mshtml.IHTMLEventObj eventObj)
        {
            throw new NotImplementedException();
        }

        public void TranslateAccelerator(int disp, mshtml.IHTMLEventObj eventObj)
        {
            throw new NotImplementedException();
        }

        public void PostHandleEvent(int disp, mshtml.IHTMLEventObj eventObj)
        {
            throw new NotImplementedException();
        }

        public void PreHandleEvent(int disp, mshtml.IHTMLEventObj eventObj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
