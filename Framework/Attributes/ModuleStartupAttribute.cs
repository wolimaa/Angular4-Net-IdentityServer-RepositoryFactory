using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Attributes
{
    public class ModuleStartupAttribute : Attribute
    {
        public Type StartupType { get; set; }
        public string MethodName { get; set; }

        public ModuleStartupAttribute(Type startupType)
        {
            StartupType = startupType;
            MethodName = "Configuration";
        }

        public ModuleStartupAttribute(Type startupType, string methodName)
        {
            StartupType = startupType;
            MethodName = methodName;
        }
    }
}
