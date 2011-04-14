using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Frontend.UserControls;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Core.Project;
using HtmlAgilityPack;

namespace Frontend.Helpers
{
    /// <summary>
    /// Class for reading from/writing to project files
    /// </summary>
    internal class CFileHelper
    {
        /// <summary>
        /// Saves project in String to file
        /// (deprecated)
        /// </summary>
        /// <param name="url">Url of file</param>
        /// <param name="what">content</param>
        /// <returns>Boolean of success</returns>
        public static bool saveProject(String url, String what)
        {
            try
            {
                StreamWriter sw = new StreamWriter(url);
                sw.Write(what);
                sw.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Saves project to file set in page
        /// </summary>
        /// <param name="page">CanvasTabPage to save project from</param>
        /// <returns>Boolean of success</returns>
        public static bool saveProject(CProjectInfo projectInfo, String url)
        {
            try
            {
                // Init serializer
                DataContractSerializer serializer = new DataContractSerializer(typeof(CProjectInfo));
                
                // And stream to file
                StreamWriter sw = new StreamWriter(url);

                // Serialize projectInfo to file
                serializer.WriteObject(sw.BaseStream, projectInfo);
                sw.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Reads file
        /// </summary>
        /// <param name="fileUrl">File url</param>
        /// <returns>Content of file</returns>
        public static String readProject(String fileUrl)
        {
            return System.IO.File.ReadAllText(fileUrl);
        }

        public static CProjectInfo getProject(String fileUrl)
        {
            // Init serializer
            DataContractSerializer serializer = new DataContractSerializer(typeof(CProjectInfo));

            // And stream from file
            StreamReader sr = new StreamReader(fileUrl);
            CProjectInfo projectInfo = (CProjectInfo)serializer.ReadObject(sr.BaseStream);
            sr.Close();

            return projectInfo;
        }

    }
}
