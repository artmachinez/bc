using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Core.Project;
using Core.Modules;
using HtmlAgilityPack;
using System.Reflection;

namespace Core.Helpers
{
    /// <summary>
    /// Class for reading from/writing to project files
    /// </summary>
    public class CFileHelper
    {
        /// <summary>
        /// Generates complete page to specified path
        /// Project content of the page is in projectInfo.projectXml, language
        /// which to generate to is in projectInfo.languageID
        /// </summary>
        /// <param name="projectInfo">ProjectInfo instance</param>
        /// <param name="path">Path of HTML index</param>
        /// <returns>Boolean of success</returns>
        public static bool generatePage(CProjectInfo projectInfo, String path)
        {
            List<AModule> moduleList;
            String index = CXMLParser.Instance.GetHTMLFromProjectXML(projectInfo.projectXml, out moduleList);

            // Save html file
            if (!saveFile(index, path))
                return false;

            // Generate all the templates in module directory
            foreach (AModule module in moduleList)
            {
                // Get module name
                String moduleName = (String)module.GetType().GetField("name", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty).GetValue(null);

                // Create directory for each module
                String newDirPath = Path.GetDirectoryName(path) + Path.DirectorySeparatorChar + "modules" + Path.DirectorySeparatorChar + module.setup.id;
                DirectoryInfo di = Directory.CreateDirectory(newDirPath);
                String[] moduleResources = module.GetType().Assembly.GetManifestResourceNames();
                foreach (String resource in moduleResources)
                {
                    // There are more resources - preview one, html one, 
                    // but just those in given language are needed
                    String nspace = "Modules." + moduleName  + "_Templates." + projectInfo.languageID;
                    if(resource.StartsWith(nspace))
                    {
                        String name = resource.Substring(nspace.Length + 1);
                        String renderedTemplate = module.renderTemplate(projectInfo.languageID + "." + name);

                        if (!saveFile(renderedTemplate, newDirPath + Path.DirectorySeparatorChar + name))
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Saves serialized ProjectInfo to file
        /// </summary>
        /// <param name="projectInfo">ProjectInfo to get serialized</param>
        /// <param name="url">Url of file to save to</param>
        /// <returns>Boolean of success</returns>
        public static bool saveProject(CProjectInfo projectInfo, String url)
        {
            // Init serializer
            DataContractSerializer serializer = new DataContractSerializer(typeof(CProjectInfo));
            StreamWriter sw = null;
            try
            {
                // Init stream to file
                sw = new StreamWriter(url);

                // Serialize projectInfo to file
                serializer.WriteObject(sw.BaseStream, projectInfo);
            }
            catch (IOException)
            {
                return false;
            }
            finally
            {
                // Whatever happened, try to close the file
                if (sw != null)
                    sw.Close();
            }
            return true;
        }

        /// <summary>
        /// Tries to deserialize filecontent to ProjectInfo
        /// </summary>
        /// <param name="fileUrl">Url of file of project</param>
        /// <returns>CProjectInfo of deserialized object</returns>
        public static CProjectInfo getProject(String fileUrl)
        {
            // Init serializer
            DataContractSerializer serializer = new DataContractSerializer(typeof(CProjectInfo));
            StreamReader sr = null;
            CProjectInfo projectInfo;
            try
            {
                // Init stream from file
                sr = new StreamReader(fileUrl);

                // Deserialize file to ProjectInfo
                projectInfo = (CProjectInfo)serializer.ReadObject(sr.BaseStream);
            }
            catch (SerializationException)
            {
                projectInfo = null;
            }
            catch (IOException)
            {
                projectInfo = null;
            }
            finally
            {
                // Whatever happened, try to close the file
                if(sr!=null)
                    sr.Close();
            }

            return projectInfo;
        }

        /// <summary>
        /// Saving contents to file, not firing exceptions
        /// </summary>
        /// <param name="content"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool saveFile(String content, String url)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(url);
                sw.Write(content);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
            return true;
        }
    }
}
