using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Initialization;
using EPiServer.ServiceLocation;
using Perficient.Web.Features.Media;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    [InitializableModule]
    [ModuleDependency(typeof(CmsCoreInitialization))]
    public class PictureFieldCroppingFolderInitialization : IInitializableModule
    {

        protected Injected<IScorePictureFieldService> _pictureService;
        protected Injected<IAvailableSettingsRepository> _availableSettingsRepository;
        protected Injected<IContentTypeRepository> _contentTypeRepository;

        public void Initialize(InitializationEngine context)
        {
            var type = _contentTypeRepository.Service.Load(typeof(ImageMediaData));
            var setting = new AvailableSetting()
            {
                Availability = Availability.Specific,
                AllowedContentTypeNames =
                {
                    nameof(SystemCroppingFolder)
                }
            };

            _availableSettingsRepository.Service.RegisterSetting(type, setting);
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}
