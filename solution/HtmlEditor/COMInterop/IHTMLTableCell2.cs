using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("3050F82D-98B5-11CF-BB82-00AA00BDCE0B")]
    public interface IHTMLTableCell2
    {
        string abbr 
        { 
        [DispId(tabledispids.IHTMLTABLECELL2_ABBR /*0x7d4*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECELL2_ABBR)]
        set; 
        }

        string axis 
        { 
        [DispId(tabledispids.IHTMLTABLECELL2_AXIS /*0x7d5*/)]
        get;
        [DispId(tabledispids.IHTMLTABLECELL2_AXIS)]
        set; 
        }

        string ch 
        { 
        [DispId(tabledispids.IHTMLTABLECELL2_CH /*0x7d6*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECELL2_CH)]
        set; 
        }

        string chOff 
        { 
        [DispId(tabledispids.IHTMLTABLECELL2_CHOFF /*0x7d7*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECELL2_CHOFF)]
        set; 
        }

        string headers 
        { 
        [DispId(tabledispids.IHTMLTABLECELL2_HEADERS /*0x7d8*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECELL2_HEADERS)]
        set; 
        }

        string scope 
        { 
        [DispId(tabledispids.IHTMLTABLECELL2_SCOPE /*0x7d9*/)]
        get; 
        [DispId(tabledispids.IHTMLTABLECELL2_SCOPE)]
        set; 
        }
    }
}