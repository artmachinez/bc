using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;
using DotLiquid;
using System.Drawing;

namespace Core.Modules
{
    public abstract class AModule
    {
        public AModuleUserSetup setup;

        public static String name = "generic";

        public static String group = "generic";

        public static String tag = "generic";

        public static String templateName = "generic";

        public static List<String> WSLanguages = new List<String>(new String[] { "php","asp" });

        private String _WSOutputLanguage;
        public String WSOutputLanguage
        {
            get
            {
                return this._WSOutputLanguage;
            }
            set
            {
                this._WSOutputLanguage = value;
            }
        }

        public AModule(AModuleUserSetup setup)
        {
            this.setup = setup;
        }

        public virtual String generateHTML()
        {
            Template template = Template.Parse(this.getTemplate(this.GetType().GetMember("templateName") + "_" + this.WSOutputLanguage + ".tpl"));
            return template.Render(Hash.FromAnonymousObject(new { _setup = this.setup }));
        }

        public virtual String generateScripts()
        {
            return String.Empty;
        }

        public virtual String generateWS()
        {
            return String.Empty;
        }

        public String generatePreview()
        {
            Template template = Template.Parse(this.getTemplate(this.GetType().GetField("name", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty).GetValue(null) + "_preview.tpl"));
            String xml = template.Render(Hash.FromAnonymousObject(new { _setup = (Object)this.setup }));
            return xml;
        }

        protected virtual String getTemplate(String name)
        {
            Stream resource = this.GetType().Assembly.GetManifestResourceStream("Modules.Templates." + name);
            using (StreamReader reader = new StreamReader(resource))
            {
                String template = reader.ReadToEnd();
                return template;
            }
        }
    }
}
