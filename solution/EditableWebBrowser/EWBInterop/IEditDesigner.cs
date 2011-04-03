using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using mshtml;

namespace EditableWebBrowser.EWBInterop
{
    [ 
    ComVisible(true),
    ComImport(),
    Guid("3050f662-98b5-11cf-bb82-00aa00bdce0b"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown),
    ]
    internal interface IHTMLEditDesigner
    {
        [return: MarshalAs(UnmanagedType.I4)]
        void PostEditorEventNotify(
            int disp,
            [MarshalAs(UnmanagedType.Interface)]
            IHTMLEventObj eventObj
            );

        [return: MarshalAs(UnmanagedType.I4)]
        void TranslateAccelerator(
            int disp,
            [MarshalAs(UnmanagedType.Interface)]
            IHTMLEventObj eventObj
            );

        [return: MarshalAs(UnmanagedType.I4)]
        void PostHandleEvent(
            int disp,
            [MarshalAs(UnmanagedType.Interface)]
            IHTMLEventObj eventObj
            );

        [return: MarshalAs(UnmanagedType.I4)]
        void PreHandleEvent(
            int disp,
            [MarshalAs(UnmanagedType.Interface)]
            IHTMLEventObj eventObj
            );


    }
}
