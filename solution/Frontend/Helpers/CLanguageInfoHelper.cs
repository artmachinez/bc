using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frontend.Helpers
{
    public class LanguageDropDownItem
    {
        public String Value;
        public String Text;

        public override String ToString()
        {
            return Text;
        }
    }

    public class CLanguageInfoHelper
    {
        public static LanguageDropDownItem getLangItem(String langID)
        {
            LanguageDropDownItem item = new LanguageDropDownItem();

            // Set value
            item.Value = langID;

            // Set nice text
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
