using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050F23A-98B5-11CF-BB82-00AA00BDCE0B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLTableCol
    {
        int span 
        { 
        [DispId(tabledispids.IHTMLTABLECOL_SPAN /*0x3e9*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECOL_SPAN)]
        set; 
        }

        object width 
        { 
        [DispId(tabledispids.IHTMLTABLECOL_WIDTH /*-2147418107*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECOL_WIDTH)]
        set; 
        }

        string align 
        { 
        [DispId(tabledispids.IHTMLTABLECOL_ALIGN /*-2147418040*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECOL_ALIGN /*-2147418040*/)]
        set; 
        }

        string vAlign 
        { 
        [DispId(tabledispids.IHTMLTABLECOL_VALIGN /*-2147413081*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECOL_VALIGN /*-2147413081*/)]
        set; 
        }
    }
}