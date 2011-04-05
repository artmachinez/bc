using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [Guid("3050F413-98B5-11CF-BB82-00AA00BDCE0B")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IHTMLTableRowMetrics
    {
        int clientHeight 
        { 
        [DispId(tabledispids.IHTMLTABLEROWMETRICS_CLIENTHEIGHT /*-2147416093*/)]
        get; 
        }

        int clientWidth 
        { 
        [DispId(tabledispids.IHTMLTABLEROWMETRICS_CLIENTWIDTH /*-2147416092*/)]
        get; 
        }

        int clientTop 
        { 
        [DispId(tabledispids.IHTMLTABLEROWMETRICS_CLIENTTOP /*-2147416091*/)]
        get; 
        }

        int clientLeft 
        { 
        [DispId(tabledispids.IHTMLTABLEROWMETRICS_CLIENTLEFT /*-2147416090*/)]
        get; 
        }
    }
}