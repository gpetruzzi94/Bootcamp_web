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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Articulo articuloAEditar = articuloServices.BuscarId(id);
            return View(articuloAEditar);


        }
        [HttpPost]
        public IActionResult Edit(Articulo articuloAEditar)
        {
            string mensaje = articuloServices.ActualizarArticulo(articuloAEditar);
            if (mensaje == "Articulo modificado")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return View(articuloAEditar);
            }
        }
        public IActionResult Delete(int id)
        {
            string mensaje = articuloServices.BorrarArticulo(id);
            if (mensaje == "Articulo eliminado")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return View();
            }
        }


    }
}
