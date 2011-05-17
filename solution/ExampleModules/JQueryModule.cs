using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    public class JQueryModule : AModule
    {
        new public static String group = "External Libraries";
        new public static String name = "JQuery";
        new public static List<String> WSLanguages = new List<String>(new String[] { "php", "aspx" });

        public JQueryModule(AModuleUserSetup setup) : base(setup) { }
    }
}
