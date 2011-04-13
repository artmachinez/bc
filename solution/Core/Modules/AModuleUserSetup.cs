using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using DotLiquid;
using System.Xml.Serialization;
using System.Drawing;

namespace Core.Modules
{
    public abstract class AModuleUserSetup : Drop
    {
        /// <summary>
        /// Needed to set to get reference
        /// (usersetup and module classes can be named however)
        /// </summary>
        public static String moduleName;

        #region Inner needed variables

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


    }
}
