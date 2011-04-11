using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using onlyconnect;

namespace Frontend.HtmlEditorClasses
{
    /// <summary>
    /// EventArgs class for sending IHTMLElement
    /// </summary>
    public class ElementDataEventArgs : EventArgs
    {
        /// <summary>
        /// Data
        /// </summary>
        public IHTMLElement element = null;
        public IHTMLEventObj eventObj = null;

        public ElementDataEventArgs()
            : base()
        { }
    }
}
