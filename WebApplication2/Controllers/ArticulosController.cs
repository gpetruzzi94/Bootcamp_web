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

        
        public IActionResult Create() { 
            Articulo articuloACrear = new Articulo();
            return View(articuloACrear);

        
        }
        [HttpPost]
        public IActionResult Create(Articulo articuloAGuardar)
        {
            return View();
        }

    }
}
