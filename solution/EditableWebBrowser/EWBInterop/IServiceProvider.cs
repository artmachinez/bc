using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EditableWebBrowser.EWBInterop
{
    [
    ComVisible(true),
    ComImport(),
    Guid("6d5140c1-7436-11ce-8034-00aa006009fa"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown),
    ]
    public interface IServiceProvider : System.IServiceProvider
    {
        [
        return: MarshalAs(UnmanagedType.I4)
        ]
        int QueryService(
            ref System.Guid guidService,
            ref System.Guid riid,
            out IntPtr ppvObject
            );
    }
}
