using System;

namespace Listener.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class TrackingEntityAttribute: Attribute
    {
        public TrackingEntityAttribute(bool hasEntity)
        {
            HasEntity = hasEntity;
        }
        public bool HasEntity { get; private set; }
    }
}
