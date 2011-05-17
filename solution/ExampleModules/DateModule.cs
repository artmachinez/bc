using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    public class DateModule : AModule
    {
        new public static String group = "Simple Examples";
        new public static String name = "Date";
        new public static List<String> WSLanguages = new List<String>();

        public DateModule(AModuleUserSetup setup) : base(setup) { }
    }
}
