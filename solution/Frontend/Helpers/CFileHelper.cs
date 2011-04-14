using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Frontend.UserControls;

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
        public static bool saveProject(CanvasTabPage page)
        {
            try
            {
                StreamWriter sw = new StreamWriter(page.url);
                sw.Write(page.XMLProjectContent);
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

    }
}
