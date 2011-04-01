using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules.Date
{
    public class CDateUserSetup: AModuleUserSetup
    {
        new public static String moduleName = "Date";

        private String _setup_format = "DD:MM:YYYY";
        public String setup_format
        {
            get
            {
                return _setup_format;
            }
            set
            {
                switch (value)
                {
                    case "DD:MM:YYYY":
                    case "MM:DD:YYYY":
                        _setup_format = value;
                        break;
                    default:
                        throw new Exception("invalid format");
                }
            }
        }
    }
}
