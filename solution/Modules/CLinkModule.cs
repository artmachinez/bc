using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules.Link
{
    public class CLinkModule:AModule
    {
        new public static String group = "generic";

        new public static String templateName = "Link";

        new public static String name = "Link";

        new public static String tag = "link";

        new public static List<String> WSLanguages = new List<String>(new String[] { "php" });

        public CLinkModule(AModuleUserSetup setup) : base(setup)
        {
        }

    }
}
