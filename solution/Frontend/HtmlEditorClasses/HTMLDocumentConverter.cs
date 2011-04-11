using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.HtmlEditorClasses
{
    public static class HTMLDocumentConverter
    {
        public static HtmlAgilityPack.HtmlDocument mshtmlDocToAgilityPackDoc(onlyconnect.IHTMLDocument2 doc)
        {
            HtmlAgilityPack.HtmlDocument outDoc = new HtmlAgilityPack.HtmlDocument();
            string html = doc.GetBody().outerHTML;
            outDoc.LoadHtml(html);
            return outDoc;
        }
    }
}
