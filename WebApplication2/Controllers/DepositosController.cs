using Microsoft.AspNetCore.Mvc;
using CodigoComun.Entities;
using CodigoComun.Negocio;

namespace WebAppTarea11.Controllers
{
    public class DepositosController : Controller
    {
        DepositoServices depositoServices = new DepositoServices();
        public IActionResult Index()
        {
            
            List<Deposito> depositos= depositoServices.GetTodosLosDepositos();
            return View(depositos);
        }


        [HttpGet]
        public IActionResult Create()
        {
            Deposito articuloACrear = new Deposito();
            return View(articuloACrear);


        }
        [HttpPost]
        public IActionResult Create(Deposito depositoAGuardar)
        {
            string mensaje = depositoServices.AddDeposito(depositoAGuardar);
            if (mensaje == "Deposito agregado")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return View(depositoAGuardar);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Deposito depositoAEditar = depositoServices.GetDepositoPorId(id);
            return View(depositoAEditar);


        }
        [HttpPost]
        public IActionResult Edit(Deposito depositoAEditar)
        {
            string mensaje = depositoServices.ModificarDeposito(depositoAEditar);
            if (mensaje == "Deposito modificado")
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return View(depositoAEditar);
            }
        }
        public IActionResult Delete(int id)
        {
            string mensaje = depositoServices.EliminarDeposito(id);
            if (mensaje == "Deposito eliminado")
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
