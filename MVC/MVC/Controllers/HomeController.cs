using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using MVC.Service;
namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeService homeService;

        public HomeController(ILogger<HomeController> logger,HomeService homeService)
        {
            _logger = logger;
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateForm([FromBody] FormReq req)
        {
            try
            {
                if (req != null)
                {
                    var result = await homeService.CreateForm(req);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                var aa = ex;
            }
           

            return null;
         
        }
        [HttpPost]
        public async Task<JsonResult> DeleteForm([FromBody] FormReq req)
        {
            try
            {
                if (req != null)
                {
                    var result = await homeService.DeleteForm(req);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                var aa = ex;
            }


            return null;

        }
        [HttpPost]
        public async Task<JsonResult> UpdateForm([FromBody] FormReq req)
        {
            try
            {
                if (req != null)
                {
                    var result = await homeService.UpdateForm(req);
                    return Json(result);
                }
            }
            catch (Exception ex)
            {
                var aa = ex;
            }


            return null;

        }
    }
}