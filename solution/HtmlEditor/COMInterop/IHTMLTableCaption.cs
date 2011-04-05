using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050f2eb-98b5-11cf-bb82-00aa00bdce0b")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLTableCaption
    {
        string align 
        { 
        [DispId(-2147418040)]
        get; 
        [DispId(-2147418040)]
        set; 
        }

        string vAlign 
        { 
        [DispId(-2147413081)]
        get; 
        [DispId(-2147413081)]
        set; 
        }
    }
}