using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;

namespace Modules.Image
{
    public class CImageModule : AModule
    {
        new public static String group = "generic";
        new public static String name = "Image";
        new public static List<String> WSLanguages = new List<String>();

        public CImageModule(AModuleUserSetup setup) : base(setup) { }

    }
}
