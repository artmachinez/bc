using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    class LoginModuleUserSetup : AModuleUserSetup
    {
        new public static String moduleName = "Login";

        #region User Variables

        private String _setup_background = "white";
        public String setup_background
        {
            get { return this._setup_background; }
            set { this._setup_background = value; }
        }

        private String _setup_inputcolor = "gray";
        public String setup_inputcolor
        {
            get { return this._setup_inputcolor; }
            set { this._setup_inputcolor = value; }
        }

        #endregion
    }
}
