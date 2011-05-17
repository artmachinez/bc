using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    public class LoginModule : AModule
    {
        new public static String group = "Advanced Examples";
        new public static String name = "Login";
        new public static List<String> WSLanguages = new List<String>(new String[] { "php" });

        public LoginModule(AModuleUserSetup setup) : base(setup) {}
    }
}
