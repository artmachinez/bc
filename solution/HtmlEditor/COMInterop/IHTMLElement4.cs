using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("3050f80f-98b5-11cf-bb82-00aa00bdce0b")]
    public interface IHTMLElement4
    {
        [DispId(tabledispids.IHTMLELEMENT4_NORMALIZE /*-2147417000*/)]
        void normalize();

        [DispId(tabledispids.IHTMLELEMENT4_GETATTRIBUTENODE /*-2147417003*/)]
        IHTMLDOMAttribute getAttributeNode(string bstrname);

        [DispId(tabledispids.IHTMLELEMENT4_SETATTRIBUTENODE /*-2147417002*/)]
        IHTMLDOMAttribute setAttributeNode(IHTMLDOMAttribute pattr);

        [DispId(tabledispids.IHTMLELEMENT4_REMOVEATTRIBUTENODE /*-2147417001*/)]
        IHTMLDOMAttribute removeAttributeNode(IHTMLDOMAttribute pattr);

        object onmousewheel
        {
            [DispId(tabledispids.IHTMLELEMENT4_ONMOUSEWHEEL /*-2147412036*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT4_ONMOUSEWHEEL)]
            set;
        }

        object onfocusin
        {
            [DispId(tabledispids.IHTMLELEMENT4_ONFOCUSIN /*-2147412021*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT4_ONFOCUSIN)]
            set;
        }

        object onbeforeactivate
        {
            [DispId(tabledispids.IHTMLELEMENT4_ONBEFOREACTIVATE /*-2147412022*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT4_ONBEFOREACTIVATE)]
            set;
        }

        object onfocusout
        {
            [DispId(tabledispids.IHTMLELEMENT4_ONFOCUSOUT /*-2147412020*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT4_ONFOCUSOUT)]
            set;
        }
    }
}