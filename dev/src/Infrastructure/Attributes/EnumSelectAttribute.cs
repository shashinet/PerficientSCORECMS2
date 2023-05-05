using EPiServer.Shell.ObjectEditing;
using Perficient.Infrastructure.SelectionFactories;
using System;

namespace Perficient.Infrastructure.Attributes
{
    public class EnumSelectAttribute : SelectOneAttribute
    {

        public EnumSelectAttribute(Type enumType)
        {
            EnumType = enumType;
        }

        public Type EnumType { get; set; }


        public override Type SelectionFactoryType
        {
            get => typeof(EnumSelectionFactory<>).MakeGenericType(EnumType);
            set => base.SelectionFactoryType = typeof(EnumSelectionFactory<>).MakeGenericType(value);
        }
    }
}
