using Microsoft.AspNetCore.Mvc;
using CodigoComun.Entities;
using CodigoComun.Negocio;
using CodigoComun.DTO;


namespace WebAppTarea11.Controllers
{
    public class ArticulosController : Controller
    {
        ArticuloServices articuloServices = new ArticuloServices();
        public IActionResult Index()
        {

            
            List<ArticuloDTO> articulos = articuloServices.GetAllArticulos();
            return View(articulos);
        }

        [HttpGet]
        public IActionResult Create() { 
            ArticuloDTO articuloACrear = new ArticuloDTO();
            return View(articuloACrear);

        
        }
        [HttpPost]
        public IActionResult Create(ArticuloDTO articuloAGuardar)
        {
            articuloAGuardar =articuloServices.AgregarArticulo(articuloAGuardar);
            TempData["AlertMessage"] = articuloAGuardar.Mensaje;
            if (articuloAGuardar.Mensaje == "Articulo agregado")
            {
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(articuloAGuardar);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ArticuloDTO articuloAEditar = articuloServices.BuscarId(id);
            return View(articuloAEditar);


        }
        [HttpPost]
        public IActionResult Edit(ArticuloDTO articuloAEditar)
        {
            articuloAEditar = articuloServices.ActualizarArticulo(articuloAEditar);
            TempData["AlertMessage"] = articuloAEditar.Mensaje;
            if (articuloAEditar.Mensaje == "Articulo modificado")
            {
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(articuloAEditar);
            }
        }
        [HttpGet,ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            ArticuloDTO articuloAEliminar = articuloServices.BorrarArticulo(id);

            TempData["AlertMessage"] = articuloAEliminar.Mensaje;
  
            return RedirectToAction("Index");
            
            
            
        }


    }
}
