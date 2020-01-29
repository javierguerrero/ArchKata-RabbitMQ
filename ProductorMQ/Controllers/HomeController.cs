using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductorMQ.Models;
using RabbitMQ.Support;

namespace ProductorMQ.Controllers
{
    public class HomeController : Controller
    {
        private readonly AmqpService amqpService;

        public HomeController(AmqpService amqpService)
        {
            this.amqpService = amqpService ?? throw new ArgumentNullException(nameof(amqpService));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EnviarMensaje(MensajeInput input)
        {
            amqpService.PublishMessage(input.Mensaje, input.Cola);
            return View("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class MensajeInput
    {
        public string Cola { get; set; }
        public string Mensaje { get; set; }
    }
}
