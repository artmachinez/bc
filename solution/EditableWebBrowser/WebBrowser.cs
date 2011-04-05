using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mshtml;
using EditableWebBrowser.EWBInterop;
//using Windows.w

namespace EditableWebBrowser
{
    public class WebBrowser : IOleClientSite
    {
        /// <summary>
        /// WebBrowser class holder
        /// </summary>
        private System.Windows.Forms.WebBrowser _wb;
        public System.Windows.Forms.WebBrowser wb
        {
            get 
            {
                if (_wb == null)
                {
                    _wb = new System.Windows.Forms.WebBrowser();
                }
                return _wb; 
            }
        }

        /// <summary>
        /// Unmanaged HTMLDocument of WebBrowser
        /// </summary>
        private IHTMLDocument2 _doc2wb;
        public mshtml.IHTMLDocument2 doc2wb
        {
            get
            {
                if (_doc2wb == null)
                {
                    _doc2wb = (IHTMLDocument2)wb.Document.DomDocument;
                }
                return _doc2wb; 
            }
        }


        EWBInterop.IOleObject m_document;

        /// <summary>
        /// 
        /// </summary>
        public WebBrowser()
        {
            //EWBInterop.IServiceProvider sp = (EWBInterop.IServiceProvider)wb;
            //Boolean created = false;
            //try
            //{
            //    EWBInterop.IOleObject m_document = (EWBInterop.IOleObject)new HTMLDocument();

            //    int iRetval;
            //    iRetval = win32.OleRun(m_document);
            //    iRetval = m_document.SetClientSite(this);

            //    // Lock the object in memory
            //    iRetval = win32.OleLockRunning(m_document, true, false);

            //    m_document.SetHostNames("HtmlEditor", "HtmlEditor");
            //    m_document.Advise(this, out iAdviseCookie);

            //    //hook up HTMLDocumentEvents2
            //    Guid guid = new Guid("3050f613-98b5-11cf-bb82-00aa00bdce0b");
            //    IConnectionPointContainer icpc = (IConnectionPointContainer)m_document;

            //    icpc.FindConnectionPoint(ref guid, out icp);
            //    icp.Advise(this, out iEventsCookie);

            //    created = true;
            //}
            //finally
            //{
            //    if (created == false)
            //        m_document = null;
            //}


            //return;
        }

        public int SaveObject()
        {
            throw new NotImplementedException();
        }

        public int GetMoniker(uint dwAssign, uint dwWhichMoniker, out object ppmk)
        {
            throw new NotImplementedException();
        }

        public int GetContainer(out IOleContainer ppContainer)
        {
            throw new NotImplementedException();
        }

        public int ShowObject()
        {
            throw new NotImplementedException();
        }

        public int OnShowWindow(int fShow)
        {
            throw new NotImplementedException();
        }

        public int RequestNewObjectLayout()
        {
            throw new NotImplementedException();
        }
    }
}
