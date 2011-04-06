using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using onlyconnect;
using System.Runtime.InteropServices;

namespace Frontend.HtmlEditorClasses
{
    [ComVisible(true)]
    class CRestrictedEditDesigner : IHTMLEditDesigner
    {

        private int isForbidden(IHTMLElement elem)
        {
            if (elem == null)
                return HRESULT.S_FALSE;
            if (elem.className == null)
                return isForbidden(elem.parentElement);
            if (elem.className == "module")
                return HRESULT.S_OK;

            return HRESULT.S_FALSE;
        }

        public int PostEditorEventNotify(int inEvtDispId, IHTMLEventObj pIEventObj)
        {
            return HRESULT.S_FALSE;
        }

        public int PostHandleEvent(int inEvtDispId, IHTMLEventObj pIEventObj)
        {
            return HRESULT.S_FALSE;
        }

        public int PreHandleEvent(int inEvtDispId, IHTMLEventObj pIEventObj)
        {
            ////pIEventObj.
            //if (pIEventObj.EventType != "mouseover")
            //    if (pIEventObj.EventType != "mouseout")
            //        if (pIEventObj.EventType != "mousemove")
            //            if (pIEventObj.EventType != "mousedown")
            //                if (pIEventObj.EventType != "mouseup")
            //        return HRESULT.S_OK;
            //if(pIEventObj.ToElement != null)
            //if (pIEventObj.ToElement.tagName != "BODY")
            //    return HRESULT.S_FALSE;
            //if (isForbidden(pIEventObj.ToElement) == HRESULT.S_OK)
            //    return HRESULT.S_OK;
            //if (pIEventObj.EventType == "mousedown")
            //{
                //if (pIEventObj.ToElement != null)
                //    return HRESULT.S_FALSE;
                return isForbidden(pIEventObj.SrcElement);
            //}
            //return HRESULT.S_FALSE;
        }

        public int TranslateAccelerator(int inEvtDispId, IHTMLEventObj pIEventObj)
        {
            return HRESULT.S_FALSE;
        }
    }
}
