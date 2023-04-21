using Microsoft.AspNetCore.Mvc;
using CodigoComun.Entities;
using CodigoComun.Negocio;


namespace WebAppTarea11.Controllers
{
    public class ArticulosController : Controller
    {
        ArticuloServices articuloServices = new ArticuloServices();
        public IActionResult Index()
        {

            
            List<Articulo> articulos = articuloServices.GetAllArticulos();
            return View(articulos);
        }

        [HttpGet]
        public IActionResult Create() { 
            Articulo articuloACrear = new Articulo();
            return View(articuloACrear);

        
        }
        [HttpPost]
        public IActionResult Create(Articulo articuloAGuardar)
        {
            string mensaje =articuloServices.AgregarArticulo(articuloAGuardar);
            if (mensaje == "Articulo agregado")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return View(articuloAGuardar);
            }
        }

    }
}
