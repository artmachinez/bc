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
        /// Creates instance of module from XML node
        /// </summary>
        /// <param name="node">Module XML node</param>
        /// <returns>Instance of AModule with UserSetup</returns>
        private AModule getModuleFromXMLNode(XmlNode node)
        {
            AModule module = CModuleReader.Instance.GetModuleInstanceFromTag(node.LocalName);

            // Set attributes
            foreach (XmlAttribute attribute in node.Attributes)
            {
                try
                {
                    Object[] args = new Object[] { attribute.Value };
                    module.setup.GetType().InvokeMember(attribute.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty, null, module.setup, args);
                }
                catch (Exception e)
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
        /// Creates instance of module from XML node
        /// </summary>
        /// <param name="node">Module XML node</param>
        /// <returns>Instance of AModule with UserSetup</returns>
        //private AModule getModuleFromXMLNode(HtmlNode node)
        //{
        //    AModule module = CModuleReader.Instance.GetModuleInstanceFromTag(node.LocalName);

        //    // Set attributes
        //    foreach (XmlAttribute attribute in node.Attributes)
        //    {
        //        try
        //        {
        //            Object[] args = new Object[] { attribute.Value };
        //            module.setup.GetType().InvokeMember(attribute.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty, null, module.setup, args);
        //        }
        //        catch (Exception e)
        //        {
        //            // Attribute not found, ignoring
        //        }
        //    }
        //    // And location
        //    RectangleConverter converter = new RectangleConverter();
        //    module.setup.location = (Rectangle)converter.ConvertFromString(node.Attributes["location"].Value);

        //    return module;
        //}

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
        /// Takes whole XML document and replaces modules tag with their preview templates
        /// </summary>
        /// <param name="inputXML">Complete project XML</param>
        /// <returns>Complete preview</returns>
        public String getPreviewFromProjectXML(String inputXML)
        {
            XmlDocument doc = new XmlDocument();
            // Throws XmlException
            doc.LoadXml(inputXML);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("modules", "~");

            XmlNodeList moduleNodeList = doc.SelectNodes("//modules:*", nsmgr);

            foreach (XmlNode moduleNode in moduleNodeList)
            {
                XmlDocument containerNode = new XmlDocument();

                // Save setup to a xmlnode called <module[modulename]>
                XmlElement setupNode = containerNode.CreateElement(App.Default.moduleNamespace + moduleNode.LocalName);
                foreach (XmlAttribute attr in moduleNode.Attributes)
                {
                    setupNode.SetAttribute(attr.LocalName, attr.Value);
                    //setupNode.Attributes.Append(containerNode.ImportNode(attr, true));
                }

                XmlElement rootNode = containerNode.CreateElement("div");
                rootNode.SetAttribute("class", "module");
                rootNode.AppendChild(setupNode);

                //rootNode.AppendChild(containerNode.CreateE(moduleNode.OuterXml));
                rootNode.InnerXml += this.getModuleFromXMLNode(moduleNode).generatePreview();

                containerNode.AppendChild(rootNode);
                
                // Switch 
                moduleNode.ParentNode.ReplaceChild(doc.ImportNode(containerNode.DocumentElement, true), moduleNode);
            }

            return doc.OuterXml;
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
