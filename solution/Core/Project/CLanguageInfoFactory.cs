﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Project
{
    /// <summary>
    /// Class for manipulating with languages
    /// In modules, the language is set just be lang ID, more info for languages is set in
    /// LanguageInfoHelper.settings
    /// </summary>
    public class CLanguageInfoFactory
    {
        /// <summary>
        /// Creates wrapper language class, tries to get info from 
        /// LanguageInfoHelper.settings
        /// </summary>
        /// <param name="langID"></param>
        /// <returns></returns>
        public static CLanguageInfo getLangItem(String langID)
        {
            CLanguageInfo item = new CLanguageInfo();

            // Set value
            item.Value = langID;

            // Try to set nice text
            try
            {
                item.Text = (String)LanguagesInfos.Default[langID];
            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
                item.Text = langID;
            }

            return item;
        }
    }
}
