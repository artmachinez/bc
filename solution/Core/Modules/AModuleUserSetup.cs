using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Drawing;
using System.Reflection;

namespace Core.Modules
{
    public abstract class AModuleUserSetup
    {

        #region Properties required to inherit in module

        /// <summary>
        /// Needed to set to get reference
        /// (usersetup and module classes can be named however)
        /// </summary>
        public static String moduleName;

        #endregion

        #region Inner properties

        /// <summary>
        /// Not really used since mshtml somehow handles positioning.
        /// Its more like placeholder for possible implementation
        /// </summary>
        public Rectangle location = new Rectangle(1, 1, 50, 50);

        /// <summary>
        /// Unique id used to identify element in html
        /// </summary>
        public String id = System.Guid.NewGuid().ToString();

        #endregion

        #region Own variables

        // Own module variables can be defined and used in templates.
        // Example of defined variable:
        //
        //      private String _setup_variable = "not set";
        //      public String setup_variable
        //      {
        //          get
        //          { 
        //              return this._setup_variable; 
        //          }
        //          set 
        //          {
        //              if(this._setup_variable.Length > 5)
        //                  throw new Excpetion('variable must be max 5 chars long');
        //              this._setup_variable = value; 
        //          }
        //      }
        //
        // Variable must be defined as String and as a class member 
        // - with setter and getter
        //
        // Example of usage in templates:
        //
        //      <div>dynamic text: {{_setup.setup_variable}}</div>

        #endregion

        /// <summary>
        /// Creates dictionary from this class
        /// </summary>
        /// <returns>Dictionary of setup_ properties + id</returns>
        public Dictionary<String, String> getDictionary()
        {
            Dictionary<String, String> dict = new Dictionary<String, String>();

            // Save all setup_* members
            MemberInfo[] setupMembers = this.GetType().GetMember("setup_*", MemberTypes.Property, BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            foreach (MemberInfo setupMember in setupMembers)
            {
                dict[setupMember.Name] = (String)this.GetType().InvokeMember(setupMember.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, null, this, null);
            }

            // ID is not property but field
            // Fields can be used internally in modules, so rather include needed fields
            // manually here
            dict["id"] = this.id;

            return dict;
        }


    }
}
