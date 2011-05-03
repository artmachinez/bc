using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules
{
    public class ChatModule : AModule
    {
        new public static String group = "Examples";
        new public static String name = "Chat";
        new public static List<String> WSLanguages = new List<String>(new String[] { "php" });

        public ChatModule(AModuleUserSetup setup) : base(setup) { }
    }
}
