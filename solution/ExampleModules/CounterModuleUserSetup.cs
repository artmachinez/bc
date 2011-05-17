using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    class CounterModuleUserSetup : AModuleUserSetup
    {
        new public static String moduleName = "Counter";

        #region User Variables

        private String _setup_showip = "1";
        public String setup_showip
        {
            get { return this._setup_showip; }
            set { this._setup_showip = value; }
        }

        #endregion
    }
}
