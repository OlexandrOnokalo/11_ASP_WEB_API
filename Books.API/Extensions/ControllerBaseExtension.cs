using Microsoft.AspNetCore.Mvc;
using Books.BLL.Services;

namespace Books.API.Extensions
{
    public static class ControllerBaseExtension
    {
        public static IActionResult GetAction(this ControllerBase controller, ServiceResponse response)
        {
            if (response.Success)
            {
                return controller.Ok(response);
            }
            else
            {
                return controller.BadRequest(response);
            }
        }
    }
}