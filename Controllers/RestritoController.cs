using Microsoft.AspNetCore.Mvc;
using PrimeiraCrudMVC.Filters;

namespace PrimeiraCrudMVC.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
