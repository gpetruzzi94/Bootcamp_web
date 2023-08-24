using Microsoft.AspNetCore.Mvc;
using CodigoComun.Entities;
using CodigoComun.Negocio;
using CodigoComun.DTO;

namespace WebAppTarea11.Controllers
{
    public class DepositosController : Controller
    {
        DepositoServices depositoServices = new DepositoServices();
        public IActionResult Index()
        {
            
            List<DepositoDTO> depositos= depositoServices.GetTodosLosDepositos();
            return View(depositos);
        }


        [HttpGet]
        public IActionResult Create()
        {
            Deposito articuloACrear = new Deposito();
            return View(articuloACrear);


        }
        [HttpPost]
        public IActionResult Create(DepositoDTO depositoAGuardar)
        {
            depositoAGuardar = depositoServices.AddDeposito(depositoAGuardar);
            TempData["AlertMessage"] = depositoAGuardar.Mensaje;
            if (depositoAGuardar.Mensaje == "Deposito agregado")
            {
                
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(depositoAGuardar);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            DepositoDTO depositoAEditar = depositoServices.GetDepositoPorId(id);
            return View(depositoAEditar);


        }
        [HttpPost]
        public IActionResult Edit(DepositoDTO depositoAEditar)
        {
            depositoAEditar = depositoServices.ModificarDeposito(depositoAEditar);
            TempData["AlertMessage"] = depositoAEditar.Mensaje;
            if (depositoAEditar.Mensaje == "Deposito modificado")
            {
                
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(depositoAEditar);
            }
        }
        [HttpGet, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            DepositoDTO depositoAEliminar = depositoServices.EliminarDeposito(id);
            TempData["AlertMessage"] = depositoAEliminar.Mensaje;
            if (depositoAEliminar.Mensaje == "Deposito eliminado")
            {
                
                return RedirectToAction("Index");
            }
            else
            {
                
                return RedirectToAction("Index");
            }
        }



    }
}
