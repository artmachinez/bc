using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules.Link
{
    public class CLinkUserSetup: AModuleUserSetup
    {
        new public static String moduleName = "Link";

        private String _setup_href = "not defined";
        public String setup_href 
        { 
            get { return this._setup_href; }
            set { this._setup_href = value; }
        }

        private String _setup_title = "link";
        public String setup_title
        {
            get { return this._setup_title; }
            set { this._setup_title = value; }
        }
    }
}
