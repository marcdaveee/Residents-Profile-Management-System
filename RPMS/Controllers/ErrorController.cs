using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RPMS.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.StatusCode = "404";
                    ViewBag.Title = "Not Found";
                    ViewBag.ErrorMessage = "Requested resource not found.";
                    break;

                case 500:
                    ViewBag.StatusCode = "500";
                    ViewBag.Title = "Internal Server Error";
                    ViewBag.ErrorMessage = "Error occured while processing your request. Please try again. If not resolved, please contact the developer.";
                    break;
            }

            return View("Error");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionDetails.Path;

            return View("Error");
        }
    }
}
