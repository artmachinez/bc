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
    //[XmlIgnore]
    public abstract class AModuleUserSetup : Drop
    {
        public static String moduleName;

        public Rectangle location = new Rectangle(1, 1, 50, 50);

        private String _id = System.Guid.NewGuid().ToString();
        public String id
        {
            get { return this._id; }
            set { this._id = value; }
        }

    }
}
