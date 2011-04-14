using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.Helpers
{
    /// <summary>
    /// Object file for module
    /// In modules, the language is set just be lang ID, more info for languages is set in
    /// LanguageInfoHelper.settings
    /// </summary>
    public class CLanguageItem
    {
        /// <summary>
        /// Value set in modules - without spaces and stuff
        /// </summary>
        public String Value;
        /// <summary>
        /// Nice name of language
        /// </summary>
        public String Text;

        public override String ToString()
        {
            return Text;
        }
    }
}
