using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    class LinkModuleUserSetup : AModuleUserSetup
    {
        new public static String moduleName = "Link";

        #region User Variables

        private String _setup_target = "";
        public String setup_target
        {
            get { return this._setup_target; }
            set { this._setup_target = value; }
        }

        private String _setup_text = "new link";
        public String setup_text
        {
            get { return this._setup_text; }
            set { this._setup_text = value; }
        }


        #endregion
    }
}
