using Microsoft.AspNetCore.Mvc;
using CodigoComun.Entities;
using CodigoComun.Negocio;

namespace WebAppTarea11.Controllers
{
    public class DepositosController : Controller
    {
        public IActionResult Index()
        {
            DepositoServices depositoServices = new DepositoServices();
            List<Deposito> depositos= depositoServices.GetTodosLosDepositos();
            return View(depositos);
        }
    }
}
