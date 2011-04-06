using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("3050F5C7-98B5-11CF-BB82-00AA00BDCE0B")]
    public interface IHTMLTableSection2
    {
        [DispId(tabledispids.IHTMLTABLESECTION2_MOVEROW /*0x3eb*/)]
        object moveRow(
            [In] [Optional] [DefaultParameterValue(-1)] int indexFrom,
            [In] [Optional] [DefaultParameterValue(-1)] int indexTo
            );
    }
}