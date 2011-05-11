using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    public class LinkModule : AModule
    {
        new public static String group = "Simple Examples";
        new public static String name = "Link";
        new public static List<String> WSLanguages = new List<String>();

        public LinkModule(AModuleUserSetup setup) : base(setup) { }
    }
}
