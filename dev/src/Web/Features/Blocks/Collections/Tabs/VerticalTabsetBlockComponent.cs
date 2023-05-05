using AutoMapper;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Collections.Tabs
{
    public class VerticalTabsetBlockComponent : AsyncPartialContentComponent<VerticalTabsetBlock>
    {
        private readonly IMapper _mapper;

        public VerticalTabsetBlockComponent(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(VerticalTabsetBlock currentBlock)
        {
            var viewModel = _mapper.Map<VerticalTabsetViewModel>(currentBlock);


            return await Task.FromResult(View("~/Features/Blocks/Collections/Tabs/_VerticalTabsetBlock.cshtml", viewModel));
        }
    }
}
