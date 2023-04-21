using Microsoft.AspNetCore.Mvc;
using CodigoComun.Entities;
using CodigoComun.Negocio;


namespace WebAppTarea11.Controllers
{
    public class ArticulosController : Controller
    {
        public IActionResult Index()
        {
            //test
            ArticuloServices articuloServices = new ArticuloServices();
            List<Articulo> articulos = articuloServices.GetAllArticulos();
            return View(articulos);
        }
    }
}
