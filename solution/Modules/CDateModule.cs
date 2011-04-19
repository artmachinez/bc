using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;
using System.Reflection;
using System.IO;
using System.Resources;

namespace Modules.Date
{
    public class CDateModule : AModule
    {

        new public static String group = "advanced";
        new public static String name = "Date";
        new public static List<String> WSLanguages = new List<String>(new String[] { "php", "asp" });

        public CDateModule(AModuleUserSetup setup) : base(setup) { }
    }
}
