using Microsoft.AspNetCore.Mvc;

namespace TheAddress.UI.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public FooterViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
