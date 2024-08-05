using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class TableAttribute : Attribute
    {
        public string Name { get; }

        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}
