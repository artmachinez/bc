using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules.Image
{
    public class CImageUserSetup: AModuleUserSetup
    {
        new public static String moduleName = "Image";

        private String _setup_source = "not defined";
        public String setup_source 
        {
            get { return this._setup_source; }
            set { this._setup_source = value; }
        }

        private String _setup_alt = "not defined";
        public String setup_alt
        {
            get { return this._setup_alt; }
            set { this._setup_alt = value; }
        }

    }
}
