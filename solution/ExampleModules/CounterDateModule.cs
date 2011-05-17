using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    public class CounterModule : AModule
    {
        new public static String group = "Advanced Examples";
        new public static String name = "Counter";
        new public static List<String> WSLanguages = new List<String>(new String[] { "aspx", "php" });

        public CounterModule(AModuleUserSetup setup) : base(setup) { }
    }
}
