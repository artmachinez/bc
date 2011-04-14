using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Frontend.Helpers
{
    /// <summary>
    /// Handling open/create/.. file dialogs
    /// </summary>
    internal class CFileDialogFactory
    {
        /// <summary>
        /// Creates new file dialog - has to be created as SaveFileDialog
        /// </summary>
        /// <returns>Dialog for new file</returns>
        internal static SaveFileDialog createNewFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Project File (*." + Core.App.Default.projectExtension + ")|*." + Core.App.Default.projectExtension;
            saveFileDialog.Title = "Create New Project";

            return saveFileDialog;
        }

        /// <summary>
        /// Creates open file dialog
        /// </summary>
        /// <returns>Dialog for opening a file</returns>
        internal static OpenFileDialog createOpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Project File (*." + Core.App.Default.projectExtension + ")|*." + Core.App.Default.projectExtension;
            openFileDialog.Title = "Open Project";

            return openFileDialog;
        }

        /// <summary>
        /// Creates save file dialog
        /// </summary>
        /// <returns>Dialog for saving a file</returns>
        internal static SaveFileDialog createSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Project File (*." + Core.App.Default.projectExtension + ")|*." + Core.App.Default.projectExtension;
            saveFileDialog.Title = "Save Project As";

            return saveFileDialog;
        }

    }
}
