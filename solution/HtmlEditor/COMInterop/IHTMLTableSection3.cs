using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050F82B-98B5-11CF-BB82-00AA00BDCE0B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLTableSection3
    {
        string ch 
        { 
        [DispId(tabledispids.IHTMLTABLESECTION3_CH /*0x3ec*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLESECTION3_CH)]
        set; 
        }

        string chOff 
        { 
        [DispId(tabledispids.IHTMLTABLESECTION3_CHOFF /*0x3ed*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLESECTION3_CHOFF)]
        set; 
        }
    }
}