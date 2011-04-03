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
    Guid("3050f663-98b5-11cf-bb82-00aa00bdce0b"),
    InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown),
    ]
    internal interface IHTMLEditServices
    {

        [return: MarshalAs(UnmanagedType.I4)]
        int AddDesigner(
            [MarshalAs(UnmanagedType.Interface)]
            IHTMLEditDesigner designer
            );

        [return: MarshalAs(UnmanagedType.Interface)]
        Object GetSelectionServices(
            [MarshalAs(UnmanagedType.Interface)]
            Object markupContainer,
            [Out, MarshalAs(UnmanagedType.Interface)] 
            out ISelectionServices ppSelSvc
            );

        [return: MarshalAs(UnmanagedType.I4)]
        int MoveToSelectionAnchor(
            [MarshalAs(UnmanagedType.Interface)]
            Object markupPointer
            );

        [return: MarshalAs(UnmanagedType.I4)]
        int MoveToSelectionEnd(
            [MarshalAs(UnmanagedType.Interface)]
            Object markupPointer
            );

        [return: MarshalAs(UnmanagedType.I4)]
        int RemoveDesigner(
            [MarshalAs(UnmanagedType.Interface)]
            IHTMLEditDesigner designer
            );
    }
}
