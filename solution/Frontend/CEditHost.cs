using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mshtml;

namespace Frontend
{
    //class to implement IHtmlEditHost
    /// <summary>
    /// enables you to customize the way elements are resized and moved
    /// </summary>
    internal class CEditHost : IHTMLEditHost
    {

        public void SnapRect(IHTMLElement pIElement,
                ref tagRECT rect,
                _ELEMENT_CORNER ehandle
                )
        {
            Console.WriteLine("SnapRect called");
            return;
        }

    }
}
