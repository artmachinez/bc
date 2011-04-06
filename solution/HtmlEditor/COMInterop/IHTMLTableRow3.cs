using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("3050F82C-98B5-11CF-BB82-00AA00BDCE0B")]
    public interface IHTMLTableRow3
    {
        string ch 
        { 
        [DispId(tabledispids.IHTMLTABLEROW3_CH /*0x3f1*/)]
        get;
        [DispId(tabledispids.IHTMLTABLEROW3_CH)]
        set; 
        }

        string chOff 
        { 
        [DispId(tabledispids.IHTMLTABLEROW3_CHOFF /*0x3f2*/)]
        get;
        [DispId(tabledispids.IHTMLTABLEROW3_CHOFF)]
        set; 
        }
    }
}