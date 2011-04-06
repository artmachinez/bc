using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050F82A-98B5-11CF-BB82-00AA00BDCE0B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLTableCol2
    {
        string ch 
        { 
        [DispId(tabledispids.IHTMLTABLECOL2_CH /*0x3ea*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECOL2_CH)]
        set; 
        }

        string chOff 
        { 
        [DispId(tabledispids.IHTMLTABLECOL2_CHOFF /*0x3eb*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECOL2_CHOFF)]
        set; 
        }
    }
}