using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Runtime.InteropServices;

namespace Frontend.UserControls
{
    public partial class PreviewWebBrowser : WebBrowser, IServiceProvider
    {
        //private IHTMLDocument2 doc;
        public PreviewWebBrowser()
        {
            //InitializeComponent();
            //this.DocumentText = "<html>yea</html>";
            //doc = (IHTMLDocument2)this.Document.DomDocument;
            //doc.designMode = "On";


            //this.OnVisibleChanged += 

            //this.OnStatusTextChanged();
            //this.OnDragEnter += new HandledEventArgs(what);
            //this.OnDragEnter += new 
        }

        public Object GetService(Type t)
        {
            CEditHost snapper = new CEditHost();
            return Marshal.GetComInterfaceForObject(snapper, typeof(IHTMLEditHost));
        }

        public int QueryService(ref System.Guid guidservice, ref System.Guid interfacerequested, out IntPtr ppserviceinterface)
        {

            int hr = HRESULT.E_NOINTERFACE;
            System.Guid iid_htmledithost = new System.Guid("3050f6a0-98b5-11cf-bb82-00aa00bdce0b");
            System.Guid sid_shtmledithost = new System.Guid("3050F6A0-98B5-11CF-BB82-00AA00BDCE0B");

            if ((guidservice == sid_shtmledithost) & (interfacerequested == iid_htmledithost))
            {
                CEditHost snapper = new CEditHost();
                ppserviceinterface = Marshal.GetComInterfaceForObject(snapper, typeof(IHTMLEditHost));
                if (ppserviceinterface != IntPtr.Zero)
                {
                    hr = HRESULT.S_OK;
                }

            }
            else
            {
                ppserviceinterface = IntPtr.Zero;
            }

            return hr;
        }

        //#region IDropTarget Members

        //void IDropTarget.OnDragDrop(DragEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void IDropTarget.OnDragEnter(DragEventArgs e)
        //{
        //    //e.Effect = DragDropEffects.Copy;


        //    throw new NotImplementedException();
        //}

        //void IDropTarget.OnDragLeave(EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void IDropTarget.OnDragOver(DragEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion
    }
}
