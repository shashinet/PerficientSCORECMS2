using EPiServer;

namespace Perficient.Infrastructure.Interfaces.Content
{
    public interface IContentSaving
    {
        void SavingContent(object sender, ContentEventArgs e);
    }

    public interface IContentCheckingIn
    {
        void CheckingInContent(object sender, ContentEventArgs e);
    }

    public interface IContentCreating
    {
        void CreatingContent(object sender, ContentEventArgs e);
    }

    public interface IContentPublishing
    {
        void PublishingContent(object sender, ContentEventArgs e);
    }

    public interface IContentPublished
    {
        void PublishedContent(object sender, ContentEventArgs e);
    }
}
