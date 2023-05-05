using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer.DataAbstraction;

namespace Perficient.Infrastructure.Helpers.FakeHelpers
{
    public class FakeContentType<T> : ContentType
    {
        private FakeContentType()
        {
            Name = "FakeContentType";
            ModelType = typeof(T);
        }

        public static FakeContentType<T> ContentType { get; } = new();
    }
}
