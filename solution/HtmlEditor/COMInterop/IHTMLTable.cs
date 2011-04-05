using System.Runtime.InteropServices;

namespace onlyconnect
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [Guid("3050f21e-98b5-11cf-bb82-00aa00bdce0b")]
    public interface IHTMLTable
    {
        [DispId(tabledispids.IHTMLTABLE_REFRESH)]
        void refresh();

        [DispId(tabledispids.IHTMLTABLE_NEXTPAGE)]
        void nextPage();

        [DispId(tabledispids.IHTMLTABLE_PREVIOUSPAGE)]
        void previousPage();

        [DispId(tabledispids.IHTMLTABLE_CREATETHEAD)]
        object createTHead();

        [DispId(tabledispids.IHTMLTABLE_DELETETHEAD)]
        void deleteTHead();

        [DispId(tabledispids.IHTMLTABLE_CREATETFOOT)]
        object createTFoot();

        [DispId(tabledispids.IHTMLTABLE_DELETETFOOT)]
        void deleteTFoot();

        [DispId(tabledispids.IHTMLTABLE_CREATECAPTION)]
        IHTMLTableCaption createCaption();

        [DispId(tabledispids.IHTMLTABLE_DELETECAPTION)]
        void deleteCaption();

        [DispId(tabledispids.IHTMLTABLE_INSERTROW)]
        object insertRow(int index);

        [DispId(tabledispids.IHTMLTABLE_DELETEROW)]
        void deleteRow(int index);

        object cellPadding 
        { 
        [DispId(tabledispids.IHTMLTABLE_CELLPADDING)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_CELLPADDING)]
        set; 
        }

        int dataPageSize 
        { 
        [DispId(tabledispids.IHTMLTABLE_DATAPAGESIZE)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_DATAPAGESIZE)]
        set; 
        }

        IHTMLTableCaption caption 
        { 
        [DispId(tabledispids.IHTMLTABLE_CAPTION)]
        get; 
        }

        IHTMLElementCollection rows 
        { 
        [DispId(tabledispids.IHTMLTABLE_ROWS)]
        get; 
        }

        IHTMLTableSection tFoot 
        { 
        [DispId(tabledispids.IHTMLTABLE_TFOOT)]
        get; 
        }

        string rules 
        { 
        [DispId(tabledispids.IHTMLTABLE_RULES)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_RULES)]
        set; 
        }

        object height 
        { 
        [DispId(tabledispids.IHTMLTABLE_HEIGHT)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_HEIGHT)]
        set; 
        }

        object onreadystatechange 
        { 
        [DispId(tabledispids.IHTMLTABLE_ONREADYSTATECHANGE)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_ONREADYSTATECHANGE)]
        set; 
        }

        object border 
        { 
        [DispId(tabledispids.IHTMLTABLE_BORDER)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_BORDER)]
        set; 
        }

        IHTMLTableSection tHead 
        { 
        [DispId(tabledispids.IHTMLTABLE_THEAD)]
        get; 
        }

        string align 
        { 
        [DispId(tabledispids.IHTMLTABLE_ALIGN)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_ALIGN)]
        set; 
        }

        object bgColor 
        { 
        [DispId(tabledispids.IHTMLTABLE_BGCOLOR)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_BGCOLOR)]
        set; 
        }

        object borderColorLight 
        { 
        [DispId(tabledispids.IHTMLTABLE_BORDERCOLORLIGHT)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_BORDERCOLORLIGHT)]
        set; 
        }

        IHTMLElementCollection tBodies 
        { 
        [DispId(tabledispids.IHTMLTABLE_TBODIES)]
        get; 
        }

        string readyState 
        { 
        [DispId(tabledispids.IHTMLTABLE_READYSTATE)]
        get; 
        }

        string background 
        { 
        [DispId(tabledispids.IHTMLTABLE_BACKGROUND)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_BACKGROUND)]
        set; 
        }

        object borderColorDark 
        { 
        [DispId(tabledispids.IHTMLTABLE_BORDERCOLORDARK)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_BORDERCOLORDARK)]
        set; 
        }

        object width 
        { 
        [DispId(tabledispids.IHTMLTABLE_WIDTH)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_WIDTH)]
        set; 
        }

        string frame 
        { 
        [DispId(tabledispids.IHTMLTABLE_FRAME)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_FRAME)]
        set; 
        }

        int cols 
        { 
        [DispId(tabledispids.IHTMLTABLE_COLS)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_COLS)]
        set; 
        }

        object cellSpacing 
        { 
        [DispId(tabledispids.IHTMLTABLE_CELLSPACING)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_CELLSPACING)]
        set; 
        }

        object borderColor 
        { 
        [DispId(tabledispids.IHTMLTABLE_BORDERCOLOR)]
        get; 
        [DispId(tabledispids.IHTMLTABLE_BORDERCOLOR)]
        set; 
        }
    }
}