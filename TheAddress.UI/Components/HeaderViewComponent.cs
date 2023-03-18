using Microsoft.AspNetCore.Mvc;

namespace TheAddress.UI.Components
{
    public class HeaderViewComponent:ViewComponent
    {
        public HeaderViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
          
            return View();
        }
    }
}
