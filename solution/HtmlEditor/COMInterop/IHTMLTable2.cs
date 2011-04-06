using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050F4AD-98B5-11CF-BB82-00AA00BDCE0B")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLTable2
    {
        [DispId(tabledispids.IHTMLTABLE2_FIRSTPAGE /*0x40b*/)]
        void firstPage();

        [DispId(tabledispids.IHTMLTABLE2_LASTPAGE /*0x40c*/)]
        void lastPage();

        IHTMLElementCollection cells 
        {
        [DispId(tabledispids.IHTMLTABLE2_CELLS /*0x40d*/)]
        get; 
        }

        [return : MarshalAs(UnmanagedType.IDispatch)]
        [DispId(tabledispids.IHTMLTABLE2_MOVEROW /*0x40e*/)]
        object moveRow(
            [In] [Optional] [DefaultParameterValue(-1)] int indexFrom,
            [In] [Optional] [DefaultParameterValue(-1)] int indexTo);
    }
}