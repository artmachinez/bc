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

        private String _setup_headercolor = "black";
        public String setup_headercolor
        {
            get { return this._setup_headercolor; }
            set { this._setup_headercolor = value; }
        }

        private String _setup_headertext = "My chat";
        public String setup_headertext
        {
            get { return this._setup_headertext; }
            set { this._setup_headertext = value; }
        }

        private String _setup_width = "300px";
        public String setup_width
        {
            get { return this._setup_width; }
            set { this._setup_width = value; }
        }

        private String _setup_bgcolor = "white";
        public String setup_bgcolor
        {
            get { return this._setup_bgcolor; }
            set { this._setup_bgcolor = value; }
        }

        #endregion
    }
}
