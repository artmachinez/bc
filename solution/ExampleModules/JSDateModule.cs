using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    public class JSDateModule : AModule
    {
        new public static String group = "Simple Examples";
        new public static String name = "JSDate";
        new public static List<String> WSLanguages = new List<String>();

        public JSDateModule(AModuleUserSetup setup) : base(setup) { }
    }
}
