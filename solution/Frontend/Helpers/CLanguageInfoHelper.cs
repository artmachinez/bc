using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.Helpers
{
    /// <summary>
    /// Class for manipulating with languages
    /// In modules, the language is set just be lang ID, more info for languages is set in
    /// LanguageInfoHelper.settings
    /// </summary>
    internal class CLanguageInfoHelper
    {
        /// <summary>
        /// Creates wrapper language class, tries to get info from 
        /// LanguageInfoHelper.settings
        /// </summary>
        /// <param name="langID"></param>
        /// <returns></returns>
        internal static CLanguageItem getLangItem(String langID)
        {
            CLanguageItem item = new CLanguageItem();

            // Set value
            item.Value = langID;

            // Try to set nice text
            try
            {
                item.Text = (String)LanguageInfoHelper.Default[langID];
            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
                item.Text = langID;
            }

            return item;
        }
    }
}
