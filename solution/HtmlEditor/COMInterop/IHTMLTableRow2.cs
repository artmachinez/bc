using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("3050F4A1-98B5-11CF-BB82-00AA00BDCE0B")]
    public interface IHTMLTableRow2
    {
        object height 
        { 
        [DispId(tabledispids.IHTMLTABLEROW2_HEIGHT /*-2147418106*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLEROW2_HEIGHT)]
        set; 
        }
    }
}