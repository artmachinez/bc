using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frontend.UserControls;

namespace Frontend.Helpers
{
    /// <summary>
    /// Handles creation of CanvasTabPages
    /// </summary>
    internal class CCanvasTabPageFactory
    {
        /// <summary>
        /// Creates new empty page
        /// </summary>
        /// <param name="language">Language project is in</param>
        /// <param name="title">Title of page</param>
        /// <returns>New page</returns>
        internal static CanvasTabPage createNewPage(CLanguageItem language, String title)
        {
            CanvasTabPage tabPage = new CanvasTabPage();
            tabPage.XMLProjectContent = ImageResources.new_file.ToString();
            tabPage.Text = title;
            return tabPage;
        }

        /// <summary>
        /// Creates new empty page
        /// </summary>
        /// <param name="language">Language project is in</param>
        /// <param name="title">Title of page</param>
        /// <param name="url">Url of file (which project will be saved in)</param>
        /// <returns>New page</returns>
        internal static CanvasTabPage createNewPage(CLanguageItem language, String title, String url)
        {
            CanvasTabPage tabPage = new CanvasTabPage();
            tabPage.XMLProjectContent = ImageResources.new_file.ToString();
            tabPage.Text = title;
            tabPage.url = url;
            return tabPage;
        }

        /// <summary>
        /// Creates page from existing content
        /// </summary>
        /// <param name="fileContent">Existing content</param>
        /// <param name="title">Title of page</param>
        /// <param name="url">Url of file (which project will be saved in)</param>
        /// <returns>Page</returns>
        internal static CanvasTabPage createPageFromFile(String fileContent, String title, String url)
        {
            CanvasTabPage tabPage = new CanvasTabPage();
            tabPage.XMLProjectContent = fileContent;
            tabPage.Text = title;
            tabPage.url = url;
            return tabPage;
        }

    }
}
