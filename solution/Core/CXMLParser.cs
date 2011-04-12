using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;
using HtmlAgilityPack;

namespace Core
{
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
        /// Creates instance of module from HTML node
        /// </summary>
        /// <param name="node">Module HTML node</param>
        /// <returns>Instance of AModule with UserSetup</returns>
        public AModule getModuleFromNode(HtmlNode node)
        {
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
        public HtmlNode getNodeFromModule(AModule module)
        {
            Type moduleType = module.GetType();
            Type setupType = module.setup.GetType();

            // Create root node
            String moduleName = (String)moduleType.GetField("name", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null);


            HtmlDocument htmlDoc = new HtmlDocument();
            HtmlNode node = htmlDoc.CreateElement("module"); //XmlNodeType.Element, App.Default.moduleNamespace, moduleName, App.Default.moduleNamespaceURI); 

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
        public String getPreviewFromProjectXML(String inputHTML)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(inputHTML);

            HtmlNodeCollection moduleNodeList = doc.DocumentNode.SelectNodes("//module");

            if (moduleNodeList != null)
            {
                foreach (HtmlNode moduleNode in moduleNodeList)
                {

                    AModule module = this.getModuleFromNode(moduleNode);

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

            return doc.DocumentNode.OuterHtml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="previewHTML"></param>
        /// <returns></returns>
        public String getProjectXMLFromPreview(String previewHTML)
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
        /// <returns></returns>
        public String getHTMLFromProjectXML(String projectXML)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(projectXML);

            // Need to set script paths

            // And generate modules' html
            HtmlNodeCollection moduleNodeList = doc.DocumentNode.SelectNodes("//module");
            if (moduleNodeList != null)
            {
                foreach (HtmlNode moduleNode in moduleNodeList)
                {
                    AModule module = this.getModuleFromNode(moduleNode);
                    // OuterHtml plx
                    moduleNode.InnerHtml += module.generateHTML();
                }
            }

            return doc.DocumentNode.OuterHtml;
        }
    }
}
