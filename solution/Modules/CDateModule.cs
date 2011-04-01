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
    public class CDateModule:AModule
    {

        new public static String group = "advanced";

        new public static String templateName = "Date";

        new public static String name = "Date";

        new public static String tag = "date";

        new public static List<String> WSLanguages = new List<String>(new String[] { "php", "asp" });

        public CDateModule(AModuleUserSetup setup) : base(setup)
        {
        }

        //public override String toHTML()
        //{
        //    Stream resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Modules.Templates.Date_php.tpl");
        //    using (StreamReader reader = new StreamReader(resource))
        //    {
        //        String template = reader.ReadToEnd();
        //        return template;
        //    }

        //    //return (String)null;
        //    //ResourceManager rm = new System.Resources.ResourceManager("Resources.resx", System.Reflection.Assembly.GetExecutingAssembly());

        //    //return rm.GetObject("Date_asp.tpl").ToString();

        //    //String[] resources = this.GetType().Assembly.GetManifestResourceNames();
        //    //return template;// s.Length.ToString(); //Properties.Resources.Date_php.ToString();// resources[0]; //.Length.ToString();


        //    //ResourceManager rm = new ResourceManager("Date", Assembly.GetExecutingAssembly());

        //    //return rm.
        //}

    }
}
