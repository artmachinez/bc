using System;
using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050F673-98B5-11CF-BB82-00AA00BDCE0B")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLElement3
    {
        [DispId(tabledispids.IHTMLELEMENT3_MERGEATTRIBUTES /*0x80010448*/)]
        void mergeAttributes(IHTMLElement mergeThis, ref object pvarFlags);

        bool isMultiLine
        {
            [DispId(tabledispids.IHTMLELEMENT3_ISMULTILINE /*0x80010449*/)]
            get;
        }

        bool canHaveHTML
        {
            [DispId(tabledispids.IHTMLELEMENT3_CANHAVEHTML /*0x8001044A*/)]
            get;
        }

        object onlayoutcomplete
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONLAYOUTCOMPLETE /*0x800117B9*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONLAYOUTCOMPLETE)]
            set;
        }

        object onpage
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONPAGE /*0x800117BA*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONPAGE)]
            set;
        }

        bool inflateBlock
        {
            [DispId(tabledispids.IHTMLELEMENT3_INFLATEBLOCK /*0x8001044C*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_INFLATEBLOCK)]
            set;
        }

        object onbeforedeactivate
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONBEFOREDEACTIVATE /*0x800117BD*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONBEFOREDEACTIVATE)]
            set;
        }

        [DispId(tabledispids.IHTMLELEMENT3_SETACTIVE /*0x8001044D*/)]
        void setActive();

        string contentEditable
        {
            [DispId(tabledispids.IHTMLELEMENT3_CONTENTEDITABLE /*0x8001142A*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_CONTENTEDITABLE)]
            set;
        }

        bool isContentEditable
        {
            [DispId(tabledispids.IHTMLELEMENT3_ISCONTENTEDITABLE /*0x8001044E*/)]
            get;
        }

        bool hideFocus
        {
            [DispId(tabledispids.IHTMLELEMENT3_HIDEFOCUS /*0x8001142B*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_HIDEFOCUS)]
            set;
        }

        bool disabled
        {
            [DispId(tabledispids.IHTMLELEMENT3_DISABLED /*0x8001004C*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_DISABLED)]
            set;
        }

        bool isDisabled
        {
            [DispId(tabledispids.IHTMLELEMENT3_ISDISABLED /*0x80010451*/)]
            get;
        }

        object onmove
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONMOVE /*0x800117BE*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONMOVE)]
            set;
        }

        object oncontrolselect
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONCONTROLSELECT /*0x800117BF*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONCONTROLSELECT)]
            set;
        }

        [DispId(tabledispids.IHTMLELEMENT3_FIREEVENT /*0x80010452*/)]
        bool FireEvent(string bstrEventName, ref Object pvarEventObject);

        object onresizestart
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONRESIZESTART /*0x800117C3*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONRESIZESTART)]
            set;
        }

        object onresizeend
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONRESIZEEND /*0x800117C4*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONRESIZEEND)]
            set;
        }

        object onmovestart
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONMOVESTART /*0x800117C1*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONMOVESTART)]
            set;
        }

        object onmoveend
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONMOVEEND /*0x800117C2*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONMOVEEND /*0x800117C2*/)]
            set;
        }

        object onmouseenter
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONMOUSEENTER /*0x800117C5*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONMOUSEENTER)]
            set;
        }

        object onmouseleave
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONMOUSELEAVE /*0x800117C6*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONMOUSELEAVE)]
            set;
        }

        object onactivate
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONACTIVATE /*0x800117C7*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONACTIVATE)]
            set;
        }

        object ondeactivate
        {
            [DispId(tabledispids.IHTMLELEMENT3_ONDEACTIVATE /*0x800117C8*/)]
            get;
            [DispId(tabledispids.IHTMLELEMENT3_ONDEACTIVATE)]
            set;
        }

        [DispId(tabledispids.IHTMLELEMENT3_DRAGDROP /*0x80010453*/)]
        bool dragDrop();

        int glyphMode
        {
            [DispId(tabledispids.IHTMLELEMENT3_GLYPHMODE /*0x80010454*/)]
            get;
        }
    }
}