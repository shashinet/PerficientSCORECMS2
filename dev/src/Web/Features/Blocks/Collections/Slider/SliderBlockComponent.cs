using AutoMapper;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Collections.Slider
{
    public class SliderBlockComponent : AsyncPartialContentComponent<SliderBlock>
    {
        private readonly IMapper _mapper;

        public SliderBlockComponent(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(SliderBlock currentBlock)
        {
            var viewModel = _mapper.Map<SliderViewModel>(currentBlock);


            return await Task.FromResult(View("~/Features/Blocks/Collections/Slider/_SliderBlock.cshtml", viewModel));
        }
    }
}
