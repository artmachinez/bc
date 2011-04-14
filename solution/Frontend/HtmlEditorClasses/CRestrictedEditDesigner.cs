using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using onlyconnect;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Frontend.HtmlEditorClasses
{
    /// <summary>
    /// Fires when module in htmleditor clicked
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ModuleClickedEventHandler(object sender, ElementDataEventArgs e);
    /// <summary>
    /// Fires when canvas clicked (not module on it though)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CanvasClickedEventHandler(object sender, ElementDataEventArgs e);

    /// <summary>
    /// Disables mouse events on module div in view mode
    /// </summary>
    [ComVisible(true)]
    class CRestrictedEditDesigner : IHTMLEditDesigner
    {
        public event ModuleClickedEventHandler moduleClicked;

        public event CanvasClickedEventHandler canvasClicked;

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
                    ElementDataEventArgs args = new ElementDataEventArgs();
                    args.element = module;
                    args.eventObj = pIEventObj;
                    this.moduleClicked(this, args);
                    // And deny the rest
                    return HRESULT.S_OK;
                }
                else
                {
                    ElementDataEventArgs args = new ElementDataEventArgs();
                    args.element = pIEventObj.SrcElement;
                    args.eventObj = pIEventObj;
                    this.canvasClicked(this, args);
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
