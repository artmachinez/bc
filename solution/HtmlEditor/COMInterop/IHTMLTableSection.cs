using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("3050f23b-98b5-11cf-bb82-00aa00bdce0b")]
    public interface IHTMLTableSection
    {
        [DispId(0x3e9)]
        object insertRow(int index);

        [DispId(0x3ea)]
        void deleteRow(int index);

        string vAlign 
        { 
        [DispId(-2147413081)]
        get; 
        [DispId(-2147413081)]
        set; 
        }

        object bgColor 
        { 
        [DispId(-501)]
        get; 
        [DispId(-501)]
        set; 
        }

        string align 
        { 
        [DispId(-2147418040)]
        get; 
        [DispId(-2147418040)]
        set; 
        }

        IHTMLElementCollection rows 
        { 
        [DispId(0x3e8)]
        get; 
        }
    }
}