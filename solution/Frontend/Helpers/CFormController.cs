using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frontend.Forms;
using System.Windows.Forms;
using Frontend.UserControls;

namespace Frontend.Helpers
{
    /// <summary>
    /// Stores pointers to important parts of Frontend, 
    /// so they don't have to be called like '.parent.parent.firstchild.whatever'
    /// later on.
    /// </summary>
    public class CFormController
    {
        internal MainForm mainForm;
        internal ToolBoxForm toolbox;
        internal ToolStripComboBox languageBox;
        internal PropertiesForm propertyBox;
        internal ClosableTabControl mainTabControl;

        private static CFormController instance = null;
        private CFormController() 
        {
        }
        /// <summary>
        /// Singleton instance getter
        /// </summary>
        public static CFormController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CFormController();
                }
                return instance;
            }
        }
    }
}
