using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Modules;
using System.Reflection;
using System.IO;
using System.Windows.Threading;

namespace Core
{
    /// <summary>
    /// Delegates change on modules directory
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ModulesReloadedHandler(object sender, EventArgs e);

    public class CModuleReader
    {
        /// <summary>
        /// Event firing when modules are reloaded
        /// </summary>
        public event ModulesReloadedHandler ModulesReloadedEvent;

        private static CModuleReader instance = null;
        private CModuleReader() 
        {
        }
        /// <summary>
        /// Singleton instance getter
        /// </summary>
        public static CModuleReader Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CModuleReader();
                    instance.LoadModules();
                    instance.DirWatcherInit();
                }
                return instance;
            }
        }

        /// <summary>
        /// Dispatcher for reloading modules from filesystemwatcher thread
        /// </summary>
        Dispatcher disp = Dispatcher.CurrentDispatcher;

        /// <summary>
        /// All available languages
        /// </summary>
        public List<String> languages = new List<String>();

        /// <summary>
        /// All loaded modules
        /// </summary>
        public List<Type> modules = new List<Type>();

        /// <summary>
        /// Dictionary language->modules
        /// </summary>
        public Dictionary<String, List<Type>> langToModulesMap = new Dictionary<String, List<Type>>();

        /// <summary>
        /// Dictionary moduletypename->usersetuptype
        /// </summary>
        public Dictionary<String, Type> moduleToSetupMap = new Dictionary<String, Type>();

        /// <summary>
        /// Creates filesystemwatcher on modules directory
        /// </summary>
        private void DirWatcherInit()
        {
            FileSystemWatcher watch = new FileSystemWatcher();
            watch.Path = this.GetModulesDir();
            watch.Changed += new FileSystemEventHandler(ReloadModules);
            watch.Created += new FileSystemEventHandler(ReloadModules);
            watch.Deleted += new FileSystemEventHandler(ReloadModules);
            watch.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Reloads modules on filesystemwatcher events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadModules(object sender, FileSystemEventArgs e)
        {
            disp.BeginInvoke(
                DispatcherPriority.Normal,
                (Action)delegate
                {
                    this.LoadModules();
                    this.ModulesReloadedEvent(this, EventArgs.Empty);
                }
            );
        }

        /// <summary>
        /// Loads modules to inner dictionary (CModuleReader.modules)
        /// </summary>
        private void LoadModules()
        {
            langToModulesMap.Clear();
            modules.Clear();
            languages.Clear();
            moduleToSetupMap.Clear();

            String modulesDir = GetModulesDir();

            if (!Directory.Exists(modulesDir))
                return;

            // All assemblies
            foreach (String dll in Directory.GetFiles(modulesDir, "*.dll"))
            {
                // All classes in assembly
                foreach (Type type in Assembly.LoadFile(dll).GetTypes())
                {
                    // AModule children
                    if (type.IsSubclassOf(typeof(AModule)))
                    {
                        this.modules.Add(type);

                        if (CModuleReader.GetAvailableLanguages(type).Count == 0)
                        {
                            // Create 'languageless' in dictionary if there isnt
                            if (!langToModulesMap.ContainsKey(String.Empty))
                                langToModulesMap.Add(String.Empty, new List<Type>());

                            this.langToModulesMap[String.Empty].Add(type);
                        }
                        foreach (String lang in CModuleReader.GetAvailableLanguages(type))
                        {
                            // Save all languages
                            if (!this.languages.Contains(lang))
                                this.languages.Add(lang);

                            // Create the lang in dictionary if there isnt
                            if (!langToModulesMap.ContainsKey(lang))
                                    langToModulesMap.Add(lang, new List<Type>());

                            this.langToModulesMap[lang].Add(type);
                        }
                    }

                    // Make usersetup array
                    if (type.IsSubclassOf(typeof(AModuleUserSetup)))
                    {
                        String moduleName = (String)type.GetField("moduleName", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null);
                        if (!moduleToSetupMap.ContainsKey(moduleName))
                            moduleToSetupMap.Add(moduleName, type);
                        else
                            moduleToSetupMap[moduleName] = type;
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Gets instance of module (with usersetup)
        /// </summary>
        /// <param name="moduleName">Name of module</param>
        /// <returns>Instance of module</returns>
        public AModule GetModuleInstanceFromName(String moduleName)
        {
            foreach (Type type in this.modules)
            {
                String name = (String)type.GetField("name", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null);
                if (name == moduleName)
                {
                    return this.GetModuleInstanceFromType(type);
                }
            }
            throw new Exception("module not found");
        }

        /// <summary>
        /// Gets instance of module (with usersetup)
        /// </summary>
        /// <param name="moduleType">Module type</param>
        /// <returns>Instance of module</returns>
        public AModule GetModuleInstanceFromType(Type moduleType)
        {
            String moduleName = (String)moduleType.GetField("name", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null);
            AModuleUserSetup userSetup = (AModuleUserSetup)moduleToSetupMap[moduleName].InvokeMember(null, BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            Object[] args = new Object[] { userSetup };
            AModule module = (AModule)moduleType.InvokeMember(null, BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, null, null, args);
            return module;
        }

        /// <summary>
        /// Reads directory of modules from settings
        /// </summary>
        /// <returns>Full local path of modules directory</returns>
        public String GetModulesDir()
        {
            String currentDir = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            String modulesDir = currentDir + Path.DirectorySeparatorChar + Mod.Default.modulesDir;
            String completePath = new Uri(modulesDir).LocalPath;

            if (!Directory.Exists(completePath))
            {
                Directory.CreateDirectory(completePath);
            }

            return completePath;

        }

        /// <summary>
        /// Gets available languages for specific module
        /// </summary>
        /// <param name="type">Class type of module</param>
        /// <returns>Available languages</returns>
        public static List<String> GetAvailableLanguages(Type type)
        {
            return (List<String>)type.GetField("WSLanguages", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null);
        }

    }
}
