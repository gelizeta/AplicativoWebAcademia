using AplicativoWebAcademia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AplicativoWebAcademia.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public EmpresaController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public ActionResult Save([Bind("Codigo, Nome, NomeFantasia, CNPJ")] EmpresaModel empresaModel)
        {
            return View("~/Views/Home/Index.cshtml");
        }
        public ActionResult Cadastrar()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}