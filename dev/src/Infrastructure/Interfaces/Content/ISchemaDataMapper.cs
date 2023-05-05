using EPiServer.Core;
using Schema.NET;

namespace Perficient.Infrastructure.Interfaces.Content
{
    public interface ISchemaDataMapper<in T> where T : IContent
    {
        Thing Map(T content);
    }
}
