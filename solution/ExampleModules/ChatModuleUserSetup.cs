using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    class ChatModuleUserSetup : AModuleUserSetup
    {
        new public static String moduleName = "Chat";

        #region User Variables

        private String _setup_height = "450px";
        public String setup_height
        {
            get { return this._setup_height; }
            set { this._setup_height = value; }
        }

        private String _setup_width = "800px";
        public String setup_width
        {
            get { return this._setup_width; }
            set { this._setup_width = value; }
        }

        private String _setup_bgcolor = "#EEEEEE";
        public String setup_bgcolor
        {
            get { return this._setup_bgcolor; }
            set { this._setup_bgcolor = value; }
        }

        #endregion
    }
}
