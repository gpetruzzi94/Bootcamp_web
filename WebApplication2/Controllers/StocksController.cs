using Microsoft.AspNetCore.Mvc;
using CodigoComun.Entities;
using CodigoComun.Negocio;

namespace WebAppTarea11.Controllers
{
    public class StocksController : Controller
    {
        StockService stockService = new StockService();
        public IActionResult Index()
        {
            DepositoServices depositoServices = new DepositoServices();
            ArticuloServices articuloServices = new ArticuloServices();
            List<Stock> listaStocs = stockService.GetAllStocks();

            listaStocs.ForEach(a => { a.IdArticuloNavigation = articuloServices.BuscarId((int)a.IdArticulo); });
            listaStocs.ForEach(a => { a.IdDepositoNavigation = depositoServices.GetDepositoPorId((int)a.IdDeposito); });

            return View(listaStocs);
        }
    }
}
