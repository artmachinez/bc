using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.HtmlEditorClasses
{
    /// <summary>
    /// Convert class 
    /// conversion needed between mshtml.HTMLDocument and HtmlAgilityPack.HtmlDocument
    /// </summary>
    public static class HTMLDocumentConverter
    {
        /// <summary>
        /// Converts onlyconnect.IHTMLDocument2 class to HtmlAgilityPack.HtmlDocument
        /// </summary>
        /// <param name="doc">IHTMLDocument2 document</param>
        /// <returns>Converted HtmlDocument</returns>
        public static HtmlAgilityPack.HtmlDocument mshtmlDocToAgilityPackDoc(onlyconnect.IHTMLDocument2 doc)
        {
            HtmlAgilityPack.HtmlDocument outDoc = new HtmlAgilityPack.HtmlDocument();
            string html = doc.GetBody().innerHTML;
            outDoc.LoadHtml(html);
            return outDoc;
        }
    }
}
