using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050f23c-98b5-11cf-bb82-00aa00bdce0b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLTableRow
    {
        [DispId(0x3eb)]
        object insertCell(int index);

        [DispId(0x3ec)]
        void deleteCell(int index);

        string vAlign 
        { 
        [DispId(-2147413081)]
        get; 
        [DispId(-2147413081)]
        set; 
        }

        object borderColorDark 
        { 
        [DispId(-2147413082)]
        get; 
        [DispId(-2147413082)]
        set; 
        }

        object borderColor 
        { 
        [DispId(-2147413084)]
        get; 
        [DispId(-2147413084)]
        set; 
        }

        int rowIndex 
        { 
        [DispId(0x3e8)]
        get; 
        }

        IHTMLElementCollection cells 
        { 
        [DispId(0x3ea)]
        get; 
        }

        string align 
        { 
        [DispId(-2147418040)]
        get; 
        [DispId(-2147418040)]
        set; 
        }

        object borderColorLight 
        { 
        [DispId(-2147413083)]
        get; 
        [DispId(-2147413083)]
        set; 
        }

        int sectionRowIndex 
        { 
        [DispId(0x3e9)]
        get; 
        }

        object bgColor 
        { 
        [DispId(-501)]
        get; 
        [DispId(-501)]
        set; 
        }
    }
}