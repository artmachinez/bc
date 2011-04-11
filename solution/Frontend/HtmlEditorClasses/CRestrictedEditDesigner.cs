using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using onlyconnect;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Frontend.HtmlEditorClasses
{
    public class ElementDataEventArgs : EventArgs
    {
        public IHTMLElement element;

        public ElementDataEventArgs(IHTMLElement e)
        {
            this.element = e;
        }
        public IHTMLElement e;
    }

    /// <summary>
    /// Fires when module in htmleditor clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ModuleClickedEventHandler(object sender, ElementDataEventArgs e);

    [ComVisible(true)]
    class CRestrictedEditDesigner : IHTMLEditDesigner
    {
        public event ModuleClickedEventHandler moduleClicked;

        private bool isModule(IHTMLElement elem, out IHTMLElement foundModule)
        {
            foundModule = null;
            if (elem == null)
                return false;
            if (elem.className == null)
                return isModule(elem.parentElement, out foundModule);
            if (elem.className == "modulecontainer")
            {
                foundModule = elem;
                return true;
            }

            return false;
        }

        #region IHTMLEditDesigner Members

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
            // EventType [mouseover, mouseout, mousemove, mouseup]

            // When clicked something, check if it is module or not
            if (pIEventObj.EventType == "mousedown")
            {
                IHTMLElement module;
                if (isModule(pIEventObj.SrcElement, out module))
                {
                    // Fire event
                    ElementDataEventArgs args = new ElementDataEventArgs(module);
                    this.moduleClicked(this, args);
                    // And deny the rest
                    return HRESULT.S_OK;
                }
            }
            
            return HRESULT.S_FALSE;
        }

        public int TranslateAccelerator(int inEvtDispId, IHTMLEventObj pIEventObj)
        {
            return HRESULT.S_FALSE;
        }

        #endregion
    }
}
