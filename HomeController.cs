using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sumukha.Models;
using Sumukha.Data;

namespace Sumukha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;



        public HomeController(ILogger<HomeController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View( new Contact());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [HttpPost]
        public async Task<IActionResult> Submit(Contact model)
        {
            if (ModelState.IsValid)
            {
                await _emailService.SendNotificationAsync(model.Name, model.Email, model.Subject,model.Message);
                ViewBag.Message = "Your Message has been sent successfully!";
                return View("Success");
            }
            return View("Success", model);
        }


        // Success Page
        public IActionResult Success()
        {
            return View();
        }
    }
}
