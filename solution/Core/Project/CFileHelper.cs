using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Core.Project;
using HtmlAgilityPack;

namespace Core.Project
{
    /// <summary>
    /// Class for reading from/writing to project files
    /// </summary>
    public class CFileHelper
    {
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

    }
}
