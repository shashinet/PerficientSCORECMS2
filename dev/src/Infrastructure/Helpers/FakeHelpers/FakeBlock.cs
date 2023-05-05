using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.DynamicProxy;
using EPiServer.Construction;
using EPiServer.Core;
using EPiServer.Core.Transfer;
using EPiServer.DataAbstraction;
using EPiServer.DataAbstraction.RuntimeModel.Internal;
using EPiServer.Security;
using Perficient.Infrastructure.Extensions;

namespace Perficient.Infrastructure.Helpers.FakeHelpers
{
    public class FakeBlock<T> where T : BlockData, new()
    {
        public T CurrentBlock { get; set; }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global", Justification = "BlockData can be cast as IContent")]
        public IContent CurrentBlockAsIContent => CurrentBlock as IContent;

        public ContentType ContentType => FakeContentType<T>.ContentType;

        public static FakeBlock<T> CreateFakeBlock(params Type[] additionalProxyClasses)
        {
            Type modelType = typeof(T);
            var defaultProxyClasses = new List<Type>
            {
                typeof(IContent),
                typeof(ILocalizable),
                typeof(IVersionable),
                typeof(IModifiedTrackable),
                typeof(IContentSecurable),
                typeof(ICategorizable),
                typeof(IResourceable),
                typeof(IChangeTrackable),
                typeof(IExportable)
            };

            defaultProxyClasses.AddRangeIfNotNull(additionalProxyClasses);

            ContentMixin instance = new();
            ProxyGenerationOptions proxyGenerationOptions = new();
            proxyGenerationOptions.AddMixinInstance(instance);
            object[] constructorArguments = new ConstructorParameterResolver().GetConstructorArgumentsAsList(modelType).ToArray();

            var fakedIContentBlock = new ProxyGenerator().CreateClassProxy(modelType, defaultProxyClasses.ToArray(), proxyGenerationOptions, constructorArguments, GetInterceptors()) as T;

            return new FakeBlock<T>
            {
                CurrentBlock = fakedIContentBlock
            };
        }

        private static IInterceptor[] GetInterceptors()
        {
            List<IInterceptor> list = new() { new SharedBlockInterceptor() };

            return list.ToArray();
        }
    }
}
