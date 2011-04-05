using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using onlyconnect;
using System.Runtime.InteropServices;

namespace Frontend.Editor
{
    [ComVisible(true)]
    class CRestrictedEditDesigner : IHTMLEditDesigner
    {

        public int PostEditorEventNotify(int inEvtDispId, IHTMLEventObj pIEventObj)
        {
            return HRESULT.S_FALSE;
            throw new NotImplementedException();
        }

        public int PostHandleEvent(int inEvtDispId, IHTMLEventObj pIEventObj)
        {
            return HRESULT.S_FALSE;
            throw new NotImplementedException();
        }

        public int PreHandleEvent(int inEvtDispId, IHTMLEventObj pIEventObj)
        {
            return HRESULT.S_OK;
            throw new NotImplementedException();
        }

        public int TranslateAccelerator(int inEvtDispId, IHTMLEventObj pIEventObj)
        {
            return HRESULT.S_FALSE;
            throw new NotImplementedException();
        }
    }
}
