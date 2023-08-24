using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodigoComun.Negocio;
using CodigoComun.DTO;
using CodigoComun.Entities;

namespace WebApiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        StockService stockServices = new StockService();

        [HttpPost]
        public IActionResult Post([FromBody]StockDTO stockAGuardar) {

            stockAGuardar = stockServices.AgregarStock(stockAGuardar);

            return Ok(stockAGuardar);
        

        }


        [HttpGet("GetStock/{id}")]
        public IActionResult GetStock(int id)
        {
            StockService stockServices = new StockService();
            var stockDTO = stockServices.BuscarId(id);

            return Ok(stockDTO);
        }

        [HttpGet("GetAllStocks")]
        public IEnumerable<StockDTO> GetAllStocks()
        {

            List<StockDTO> stocksDTO = stockServices.GetAllStocks();

            return stocksDTO;
        }



        [HttpPut("ModStock")]
        public IActionResult ModStock(StockDTO stockDTO)
        {

            stockDTO = stockServices.ActualizarStock(stockDTO);
            return Ok(stockDTO);

        }

        [HttpDelete("DropStock/{id}")]
        public IActionResult DropStock(int id)
        {
            StockDTO stockDTO = stockServices.BorrarStock(id);
            return Ok(stockDTO);

        }
    }
}
