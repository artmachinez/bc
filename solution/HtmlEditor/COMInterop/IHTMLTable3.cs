using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050F829-98B5-11CF-BB82-00AA00BDCE0B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLTable3
    {
        string summary 
        {
        [DispId(tabledispids.IHTMLTABLE3_SUMMARY /*0x40f*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLE3_SUMMARY)]
        set; 
        }
    }
}