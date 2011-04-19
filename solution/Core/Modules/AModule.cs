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
        #region Properties required to inherit in module

        /// <summary>
        /// Unique name of module
        /// </summary>
        public static String name = "generic";

        /// <summary>
        /// Group name - can be left like this
        /// </summary>
        public static String group = "generic";

        /// <summary>
        /// List of strings of languages available
        /// </summary>
        public static List<String> WSLanguages = new List<String>();

        #endregion

        public AModuleUserSetup setup;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="setup">Needed instance of user setup</param>
        public AModule(AModuleUserSetup setup)
        {
            this.setup = setup;
        }

        #region Methods for generating output from templates

        /// <summary>
        /// Generates HTML part of module
        /// </summary>
        /// <returns>HTML String</returns>
        public virtual String generateHTML()
        {
            return this.renderTemplate("html.tpl");
        }

        /// <summary>
        /// Generates pure HTML preview of module set in preview.tpl template
        /// </summary>
        /// <returns>HTML String</returns>
        public String generatePreview()
        {
            return this.renderTemplate("preview.tpl");
        }

        /// <summary>
        /// Gets string of template from its name.
        /// Templates must be in folder [modulename]_Templates, in namespace Modules
        /// and has to be set as embedded resources.
        /// </summary>
        /// <param name="name">Name of template</param>
        /// <returns>Template string</returns>
        internal virtual String getTemplate(String name)
        {
            // Get namespace path to templates of this module
            String templatePath = "Modules.";
            templatePath += (String)this.GetType().GetField("name", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty).GetValue(null);
            templatePath += "_Templates.";

            // And read it
            Stream resource = this.GetType().Assembly.GetManifestResourceStream(templatePath + name);
            using (StreamReader reader = new StreamReader(resource))
            {
                String template = reader.ReadToEnd();
                return template;
            }
        }

        /// <summary>
        /// Passes this.userSetup to template and renders its contents
        /// Templates must be in folder [modulename]_Templates, in namespace Modules
        /// and has to be set as embedded resources.
        /// </summary>
        /// <param name="name">Name of template</param>
        /// <returns>Rendered template string</returns>
        internal virtual String renderTemplate(String name)
        {
            Template template = Template.Parse(this.getTemplate(name));
            return template.Render(Hash.FromAnonymousObject(new { _setup = this.setup.getDictionary() }));
        }

        #endregion
    }
}
