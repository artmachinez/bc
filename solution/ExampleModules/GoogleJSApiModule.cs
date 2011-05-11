using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    public class GoogleJSApiModule : AModule
    {
        new public static String group = "Online";
        new public static String name = "GoogleJSApi";
        new public static List<String> WSLanguages = new List<String>();

        public GoogleJSApiModule(AModuleUserSetup setup) : base(setup) { }
    }
}
