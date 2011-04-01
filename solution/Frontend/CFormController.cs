using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frontend.Forms;
using System.Windows.Forms;

namespace Frontend
{
    public class CFormController
    {
        internal MainForm mainForm;

        internal ToolBoxForm toolbox;

        internal ComboBox languageBox;

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
