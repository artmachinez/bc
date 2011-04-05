using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using onlyconnect;

namespace Frontend.Editor.HtmlEditor
{
    class HtmlEditor : onlyconnect.HtmlEditor
    {
        internal HTMLDocument mHtmlDoc;

        public void SetEditDesigner(onlyconnect.IHTMLEditDesigner ds)
        {
            //this.m_htmldoc.designMode = "On";
            onlyconnect.IServiceProvider isp = (onlyconnect.IServiceProvider)this.mHtmlDoc;
            onlyconnect.IHTMLEditServices es;
            System.Guid IHtmlEditServicesGuid = new System.Guid("3050f663-98b5-11cf-bb82-00aa00bdce0b");
            System.Guid SHtmlEditServicesGuid = new System.Guid(0x3050f7f9, 0x98b5, 0x11cf, 0xbb, 0x82, 0x00, 0xaa, 0x00, 0xbd, 0xce, 0x0b);
            IntPtr ppv;
            if (isp != null)
            {
                isp.QueryService(ref SHtmlEditServicesGuid, ref IHtmlEditServicesGuid, out ppv);
                es = (onlyconnect.IHTMLEditServices)Marshal.GetObjectForIUnknown(ppv);
                int retval = es.AddDesigner(ds);
                Marshal.Release(ppv);
            }
            return;
        }
    }
}
