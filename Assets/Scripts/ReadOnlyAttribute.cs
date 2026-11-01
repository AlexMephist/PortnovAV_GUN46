using System;

namespace DefaultNamespace

{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : Attribute { }
}