using System;

namespace Listener.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TrackingPropertyAttribute:Attribute
    {
        public TrackingPropertyAttribute(string name, bool isProperty)
        {
            Name = name;
            IsProperty = isProperty;
        }
        public string Name { get; private set; }
        public bool IsProperty { get; private set; }
    }
}
