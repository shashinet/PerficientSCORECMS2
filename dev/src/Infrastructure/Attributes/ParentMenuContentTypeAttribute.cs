using System;

namespace Perficient.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ParentMenuContentTypeAttribute : Attribute
    {
        private readonly Type _parentType;
        private readonly int _maxDepth;

        public ParentMenuContentTypeAttribute(Type parentType, int maxDepth)
        {
            _parentType = parentType;
            _maxDepth = maxDepth;
        }

        public Type ParentType
        {
            get { return _parentType; }
        }

        public int Depth
        {
            get { return _maxDepth; }
        }
    }
}
