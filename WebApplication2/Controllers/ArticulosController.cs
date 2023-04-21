using Microsoft.AspNetCore.Mvc;
using CodigoComun.Entities;
using CodigoComun.Negocio;


namespace WebAppTarea11.Controllers
{
    public class ArticulosController : Controller
    {
        public IActionResult Index()
        {
            
            ArticuloServices articuloServices = new ArticuloServices();
            List<Articulo> articulos = articuloServices.GetAllArticulos();
            return View(articulos);
        }
    }
}
