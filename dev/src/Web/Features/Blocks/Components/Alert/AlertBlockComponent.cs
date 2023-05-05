using EPiServer.Core;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Perficient.Infrastructure.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Perficient.Web.Features.Blocks.Components.Alert
{
    public class AlertBlockComponent : AsyncBlockComponent<AlertBlock>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly bool _isInEditMode;

        public AlertBlockComponent(IHttpContextAccessor httpContextAccessor, IsInEditModeAccessor isInEditModeAccessor)
        {
            _isInEditMode = isInEditModeAccessor();
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<IViewComponentResult> InvokeComponentAsync(AlertBlock currentBlock)
        {
            currentBlock.IsAlertDismissed = IsBlockDismissed(((IContent)currentBlock).ContentGuid.ToString());

            return await Task.FromResult(View("~/Features/Blocks/Components/Alert/AlertBlock.cshtml", currentBlock));
        }

        private bool IsBlockDismissed(string id)
        {
            if (_isInEditMode)
            {
                return false;
            }

            try
            {
                var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[$"alert-{id}"];
                if (cookieValue != null)
                {
                    var expireDate = DateTime.Parse(cookieValue);

                    return DateTime.Compare(expireDate, DateTime.Now) > 0;
                }
            }
            catch
            {
                //If this fails, dont cause a 5xx error, fail gracefully.
                return false;
            }

            return false;
        }
    }
}
