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
using mshtml;
using HtmlAgilityPack;

namespace Core
{
    public class CXMLParser
    {

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

        /// <summary>
        /// Creates instance of module from HTML node
        /// </summary>
        /// <param name="node">Module HTML node</param>
        /// <returns>Instance of AModule with UserSetup</returns>
        private AModule getModuleFromNode(HtmlNode node)
        {
            AModule module = CModuleReader.Instance.GetModuleInstanceFromTag(node.Name);

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
        public XmlNode getNodeFromModule(AModule module)
        {
            Type moduleType = module.GetType();
            Type setupType = module.setup.GetType();

            // Create root node
            String tagName = (String)moduleType.GetField("tag", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null);
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, App.Default.moduleNamespace, tagName, App.Default.moduleNamespaceURI); 

            // Save all setup_* members to attributes
            MemberInfo[] setupMembers = setupType.GetMember("setup_*", BindingFlags.Public | BindingFlags.Instance);
            foreach (MemberInfo setupMember in setupMembers)
            {
                XmlAttribute attribute = xmlDoc.CreateAttribute(setupMember.Name);
                attribute.Value = (String)setupType.InvokeMember(setupMember.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, null, module.setup, null);
                node.Attributes.Append(attribute);
            }

            // Save position as attr
            Rectangle location = (Rectangle)setupType.GetField("location", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).GetValue(module.setup);
            XmlAttribute locationAttr = xmlDoc.CreateAttribute("location");
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
                    HtmlNode newNode = doc.CreateElement("div");
                    newNode.SetAttributeValue("class", "modulecontainer");
                    newNode.AppendChild(moduleNode);
                    newNode.InnerHtml += this.getModuleFromNode(moduleNode).generatePreview();

                    moduleNode.ParentNode.ReplaceChild(newNode, moduleNode);
                }
            }
            return doc.DocumentNode.OuterHtml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="previewXML"></param>
        /// <returns></returns>
        public String getProjectXMLFromPreview(String previewXML)
        {
            HtmlDocument doc = new HtmlDocument();
            // Throws XmlException
            doc.LoadHtml(previewXML);

            HtmlNodeCollection moduleNodeList = doc.DocumentNode.SelectNodes("//div[@class='module']/*[1]"); //("//modules:*", nsmgr);

            if (moduleNodeList != null)
            {
                foreach (HtmlNode moduleNode in moduleNodeList)
                {
                    moduleNode.Attributes.Add("xmlns:modules", moduleNode.Attributes["modules"].Value);
                    moduleNode.Attributes["modules"].Remove();
                    moduleNode.Name = "modules:" + moduleNode.Name.Substring(App.Default.moduleNamespace.Length);
                    moduleNode.ParentNode.ParentNode.ReplaceChild(moduleNode, moduleNode.ParentNode);
                }
            }

            return doc.DocumentNode.OuterHtml;
        }


    }
}
