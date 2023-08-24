using Microsoft.AspNetCore.Mvc;
using CodigoComun.Entities;
using CodigoComun.Negocio;
using CodigoComun.DTO;
using WebAppTarea11.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppTarea11.Controllers
{
    public class StocksController : Controller
    {
        StockService stockService = new StockService();
        public IActionResult Index()
        {
            DepositoServices depositoServices = new DepositoServices();
            ArticuloServices articuloServices = new ArticuloServices();
            DepositoDTO depositoDTO = new DepositoDTO();
            ArticuloDTO articuloDTO = new ArticuloDTO();

            List<StockDTO> listaStocs = stockService.GetAllStocks();

            listaStocs.ForEach(a => { a.IdArticuloNavigation = articuloServices.BuscarId((int)a.IdArticulo); });
            listaStocs.ForEach(a => { a.IdDepositoNavigation = depositoServices.GetDepositoPorId((int)a.IdDeposito); });

            return View(listaStocs);
        }


        [HttpGet]
        public IActionResult Create()
        {
            StockViewModel stock = new StockViewModel();
            stock.stockDTO = new StockDTO();

            DepositoServices depositoServices = new DepositoServices();
            ArticuloServices articuloServices = new ArticuloServices();
            stock.depositos = depositoServices.GetTodosLosDepositos();
            stock.articulos=articuloServices.GetAllArticulos();

            stock.selectDepositosList = new SelectList(stock.depositos, "Id","Id");
            stock.selectArticulosList = new SelectList(stock.articulos, "Id", "Id");

            return View(stock);


        }
        [HttpPost]
        public IActionResult Create(StockViewModel stockAGuardar)
        {

       
            stockAGuardar.stockDTO = stockService.AgregarStock(stockAGuardar.stockDTO);
            TempData["AlertMessage"] = stockAGuardar.stockDTO.Mensaje;
            if (stockAGuardar.stockDTO.Mensaje == "Stock agregado")
            {
                return RedirectToAction("Index");
            }
            else
            {

                return View(stockAGuardar);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            StockViewModel stock = new StockViewModel();
            stock.stockDTO = new StockDTO();

            DepositoServices depositoServices = new DepositoServices();
            ArticuloServices articuloServices = new ArticuloServices();
            stock.depositos = depositoServices.GetTodosLosDepositos();
            stock.articulos = articuloServices.GetAllArticulos();

            stock.selectDepositosList = new SelectList(stock.depositos, "Id", "Id");
            stock.selectArticulosList = new SelectList(stock.articulos, "Id", "Id");

            stock.stockDTO = stockService.BuscarId(id);

            return View(stock);


        }
        [HttpPost]
        public IActionResult Edit(StockViewModel stockAEditar)
        {
            
            stockAEditar.stockDTO = stockService.ActualizarStock(stockAEditar.stockDTO);
            TempData["AlertMessage"] = stockAEditar.stockDTO.Mensaje;
            if (stockAEditar.stockDTO.Mensaje == "Stock modificado")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(stockAEditar);
            }
        }
        [HttpGet,ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            StockViewModel stockAEliminar = new StockViewModel();
            stockAEliminar.stockDTO= stockService.BorrarStock(id);

            TempData["AlertMessage"] = stockAEliminar.stockDTO.Mensaje;
            return RedirectToAction("Index");
            
        }


    }
}
