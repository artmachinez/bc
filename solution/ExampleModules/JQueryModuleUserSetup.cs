using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    class JQueryModuleUserSetup : AModuleUserSetup
    {
        new public static String moduleName = "JQuery";

        #region User Variables

        private String _setup_library = "jquery";
        public String setup_library
        {
            get { return this._setup_library; }
            set { this._setup_library = value; }
        }

        private String _setup_version = "1";
        public String setup_version
        {
            get { return this._setup_version; }
            set { this._setup_version = value; }
        }

        #endregion
    }
}
