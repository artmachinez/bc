using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frontend.UserControls;
using Core.Project;

namespace Frontend.Helpers
{
    /// <summary>
    /// Handles creation of CanvasTabPages
    /// </summary>
    internal class CCanvasTabPageFactory
    {
        /// <summary>
        /// Creates new page with new project. Project info must be passed
        /// </summary>
        /// <param name="projectInfo"></param>
        /// <param name="url">Url of file (which project will be saved in)</param>
        /// <returns></returns>
        internal static CanvasTabPage createNewPage(CProjectInfo projectInfo, String url)
        {
            CanvasTabPage tabPage = new CanvasTabPage();
            tabPage.projectInfo = projectInfo;
            tabPage.activeProjectContent = ProjectResources.new_file.ToString();
            tabPage.Text = System.IO.Path.GetFileName(url);
            tabPage.url = url;
            return tabPage;
        }

        /// <summary>
        /// Creates new page with new project. Project info must be passed
        /// </summary>
        /// <param name="projectInfo"></param>
        /// <param name="url">Url of file (which project will be saved in)</param>
        /// <returns></returns>
        internal static CanvasTabPage createPage(CProjectInfo projectInfo, String url)
        {
            CanvasTabPage tabPage = new CanvasTabPage();
            tabPage.projectInfo = projectInfo;
            tabPage.activeProjectContent = projectInfo.projectXml;
            tabPage.Text = System.IO.Path.GetFileName(url);
            tabPage.url = url;
            return tabPage;
        }

        /// <summary>
        /// Creates page from existing content
        /// </summary>
        /// <param name="fileContent">Existing content</param>
        /// <param name="url">Url of file (which project will be saved in)</param>
        /// <returns>Page</returns>
        internal static CanvasTabPage createPageFromFile(String fileContent, String url)
        {
            CanvasTabPage tabPage = new CanvasTabPage();
            tabPage.activeProjectContent = fileContent;
            tabPage.Text = System.IO.Path.GetFileName(url);
            tabPage.url = url;
            return tabPage;
        }

    }
}
