using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Core.Modules;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;
using HtmlAgilityPack;

namespace Core.Helpers
{
    /// <summary>
    /// Class for handling module tags in project XML, 
    /// transforming it to preview XML and others transformations
    /// </summary>
    public class CXMLParser
    {
        #region Singleton biz

        private static CXMLParser instance = null;
        private CXMLParser() 
        {
        }
        /// <summary>
        /// Singleton instance getter
        /// </summary>
        public static CXMLParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CXMLParser();
                }
                return instance;
            }
        }

        #endregion

        /// <summary>
        /// Cached preview
        /// </summary>
        private Hashtable projectXMLToPreviewCache = new Hashtable();

        /// <summary>
        /// Creates instance of module from HTML node
        /// </summary>
        /// <param name="node">Module HTML node</param>
        /// <returns>Instance of AModule with UserSetup</returns>
        public AModule GetModuleFromNode(HtmlNode node)
        {
            if (node.Attributes["name"] == null)
            {
                return null;
            }
            
            AModule module = CModuleReader.Instance.GetModuleInstanceFromName(node.Attributes["name"].Value);

            // Set attributes
            foreach (HtmlAttribute attribute in node.Attributes)
            {
                try
                {
                    Object[] args = new Object[] { attribute.Value };
                    module.setup.GetType().InvokeMember(attribute.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty, null, module.setup, args);
                }
                catch (Exception)
                {
                    // Attribute not found, ignoring
                }
            }
            // And location
            RectangleConverter converter = new RectangleConverter();
            module.setup.location = (Rectangle)converter.ConvertFromString(node.Attributes["location"].Value);

            return module;
        }
        /// <summary>
        /// Creates XML string from instance of module
        /// </summary>
        /// <param name="module">Module</param>
        /// <returns>XML Node of module</returns>
        public HtmlNode GetNodeFromModule(AModule module)
        {
            Type moduleType = module.GetType();
            Type setupType = module.setup.GetType();

            // Create root node
            String moduleName = (String)moduleType.GetField("name", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null);


            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNode node = htmlDoc.CreateElement("module");

            // Save all setup_* members to attributes
            MemberInfo[] setupMembers = setupType.GetMember("setup_*", BindingFlags.Public | BindingFlags.Instance);
            foreach (MemberInfo setupMember in setupMembers)
            {
                HtmlAttribute attribute = htmlDoc.CreateAttribute(setupMember.Name);
                attribute.Value = (String)setupType.InvokeMember(setupMember.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, null, module.setup, null);
                node.Attributes.Append(attribute);
            }

            // Save name(type) of module
            HtmlAttribute nameAttr = htmlDoc.CreateAttribute("name");
            nameAttr.Value = moduleName;
            node.Attributes.Append(nameAttr);

            // Save position as attr
            Rectangle location = (Rectangle)setupType.GetField("location", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).GetValue(module.setup);
            HtmlAttribute locationAttr = htmlDoc.CreateAttribute("location");
            RectangleConverter converter = new RectangleConverter();
            locationAttr.Value = converter.ConvertToString(location);
            node.Attributes.Append(locationAttr);

            return node;
        }
        /// <summary>
        /// Takes whole HTML document and replaces modules tag with their preview templates
        /// </summary>
        /// <param name="inputHTML">Complete project HTML</param>
        /// <returns>Complete preview</returns>
        public String GetPreviewFromProjectXML(String inputHTML)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(inputHTML);

            // For checking/saving output to cache
            int hash = inputHTML.GetHashCode();

            // Check cache
            if (this.projectXMLToPreviewCache.ContainsKey(hash))
            {
                return (String)projectXMLToPreviewCache[hash];
            }

            HtmlNodeCollection moduleNodeList = doc.DocumentNode.SelectNodes("//module");

            if (moduleNodeList != null)
            {
                foreach (HtmlNode moduleNode in moduleNodeList)
                {
                    AModule module = this.GetModuleFromNode(moduleNode);

                    if (module != null)
                    {
                        // Because nodetag is not html, HtmlNode is not in dom structure (parent is null).
                        // Therefore HtmlNode.ReplaceChild method cannot by applied asi in this.getProjectXMLFromPreview
                        HtmlNode newNode = moduleNode.Clone();
                        moduleNode.Name = "div";
                        moduleNode.Attributes.RemoveAll();
                        moduleNode.Attributes.Add("class", "modulecontainer");
                        moduleNode.Attributes.Add("id", module.setup.id.ToString());
                        moduleNode.AppendChild(newNode);

                        moduleNode.InnerHtml += module.generatePreview();
                    }
                }
            }

            // Clear cache and add output of this
            projectXMLToPreviewCache.Clear();
            projectXMLToPreviewCache.Add(hash, doc.DocumentNode.OuterHtml);

            return doc.DocumentNode.OuterHtml;
        }
        /// <summary>
        /// Since module tags are stored even in preview, this code
        /// just deletes the previews of modules
        /// Therefore it doesnt need to be cached
        /// </summary>
        /// <param name="previewHTML">Preview of project</param>
        /// <returns>Project XML</returns>
        public String GetProjectXMLFromPreview(String previewHTML)
        {
            if (previewHTML == null)
                return String.Empty;

            HtmlDocument doc = new HtmlDocument();
            // Throws XmlException
            doc.LoadHtml(previewHTML);

            // First child of <div class='modulecontainer' /> is saved node 
            HtmlNodeCollection moduleNodeList = doc.DocumentNode.SelectNodes("//div[@class='modulecontainer']/*[1]");

            if (moduleNodeList != null)
            {
                foreach (HtmlNode moduleNode in moduleNodeList)
                {
                    moduleNode.ParentNode.ParentNode.ReplaceChild(moduleNode, moduleNode.ParentNode);
                }
            }

            return doc.DocumentNode.OuterHtml;
        }
        /// <summary>
        /// Generates final HTML from project XML
        /// </summary>
        /// <param name="projectXML"></param>
        /// <param name="moduleList">List of modules generated from project code</param>
        /// <returns>HTML part of final output</returns>
        public String GetHTMLFromProjectXML(String projectXML, out List<AModule> moduleList)
        {
            // List of modules, to have module ID saved
            moduleList = new List<AModule>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(projectXML);

            // And generate modules' html
            HtmlNodeCollection moduleNodeList = doc.DocumentNode.SelectNodes("//module");
            if (moduleNodeList != null)
            {
                foreach (HtmlNode moduleNode in moduleNodeList)
                {
                    // Change project modulenode for proper html output
                    AModule module = this.GetModuleFromNode(moduleNode);
                    moduleNode.Name = "div";
                    moduleNode.Attributes.RemoveAll();
                    moduleNode.InnerHtml = module.generateHTML();
                    moduleList.Add(module);
                }
            }

            return doc.DocumentNode.OuterHtml;
        }

        /// <summary>
        /// Changes language of project XML - removes all modules that are not in new language
        /// </summary>
        /// <param name="projectXML"></param>
        /// <param name="newLang"></param>
        /// <returns></returns>
        public String ChangeProjectLanguage(String projectXML, String newLang)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(projectXML);

            HtmlNodeCollection moduleNodeList = doc.DocumentNode.SelectNodes("//module");

            if (moduleNodeList != null)
            {
                foreach (HtmlNode moduleNode in moduleNodeList)
                {
                    AModule module = this.GetModuleFromNode(moduleNode);
                    List<String> availableLangs = CModuleReader.GetAvailableLanguages(module.GetType());
                    if (!availableLangs.Contains(newLang) && availableLangs.Count != 0)
                    {
                        moduleNode.Remove();
                    }
                }
            }
            return doc.DocumentNode.OuterHtml;
        }
    }
}
