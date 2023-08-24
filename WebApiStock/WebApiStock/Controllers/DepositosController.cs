using CodigoComun.DTO;
using CodigoComun.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositosController : ControllerBase
    {

        DepositoServices depositoServices = new DepositoServices();

        [HttpGet("GetDeposito/{id}")]
        public IActionResult GetDeposito(int id)
        {
            DepositoServices depositoServices = new DepositoServices();
            var depositoDTO = depositoServices.GetDepositoPorId(id);

            return Ok(depositoDTO);
        }

        [HttpGet("GetAllDepositos")]
        public IEnumerable<DepositoDTO> GetAllDepositos()
        {

            List<DepositoDTO> depositosDTO = depositoServices.GetTodosLosDepositos();

            return depositosDTO;
        }

        [HttpPost("AddDeposito")]
        public IActionResult AddDeposito(DepositoDTO depositoDTO)
        {

            depositoDTO = depositoServices.AddDeposito(depositoDTO);
            return Ok(depositoDTO);

        }


        [HttpPut("ModDeposito")]
        public IActionResult ModDeposito(DepositoDTO depositoDTO)
        {

            depositoDTO = depositoServices.ModificarDeposito(depositoDTO);
            return Ok(depositoDTO);

        }

        [HttpDelete("DropDeposito/{id}")]
        public IActionResult DropDeposito(int id)
        {
            DepositoDTO depositoDTO = depositoServices.EliminarDeposito(id);
            return Ok(depositoDTO);

        }

        [HttpGet("GetDepositosStock/{id}")]
        public IEnumerable<StockDTO> GetDepositosStock(int id)
        {

            List<StockDTO> stockDTO = depositoServices.GetDepositosStock(id);

            return stockDTO;
        }



    }
}
