using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Project
{
    /// <summary>
    /// Stores informations about project.
    /// This class will be serialized as saved project, so if there's
    /// something important to save with project, add it here
    /// </summary>
    public class CProjectInfo
    {
        /// <summary>
        /// ID of language set in project
        /// </summary>
        public String languageID;
        /// <summary>
        /// All xml content of a project
        /// </summary>
        public String projectXml;
    }
}
