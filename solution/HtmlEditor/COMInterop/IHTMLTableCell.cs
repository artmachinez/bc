using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050f23d-98b5-11cf-bb82-00aa00bdce0b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLTableCell
    {
        string vAlign 
        { 
        [DispId(-2147413081)]
        get; 
        [DispId(-2147413081)]
        set; 
        }

        string background 
        { 
        [DispId(-2147413111)]
        get; 
        [DispId(-2147413111)]
        set; 
        }

        int colSpan 
        { 
        [DispId(0x7d2)]
        get; 
        [DispId(0x7d2)]
        set; 
        }

        int cellIndex 
        { 
        [DispId(0x7d3)]
        get; 
        }

        int rowSpan 
        { 
        [DispId(0x7d1)]
        get; 
        [DispId(0x7d1)]
        set; 
        }

        bool noWrap 
        { 
        [DispId(-2147413107)]
        get; 
        [DispId(-2147413107)]
        set; 
        }

        object bgColor 
        { 
        [DispId(-501)]
        get; 
        [DispId(-501)]
        set; 
        }

        object borderColor 
        { 
        [DispId(-2147413084)]
        get; 
        [DispId(-2147413084)]
        set; 
        }

        object height 
        { 
        [DispId(-2147418106)]
        get; 
        [DispId(-2147418106)]
        set; 
        }

        object borderColorLight 
        { 
        [DispId(-2147413083)]
        get; 
        [DispId(-2147413083)]
        set; 
        }

        object width 
        { 
        [DispId(-2147418107)]
        get; 
        [DispId(-2147418107)]
        set; 
        }

        string align 
        { 
        [DispId(-2147418040)]
        get; 
        [DispId(-2147418040)]
        set; 
        }

        object borderColorDark 
        { 
        [DispId(-2147413082)]
        get; 
        [DispId(-2147413082)]
        set; 
        }
    }
}