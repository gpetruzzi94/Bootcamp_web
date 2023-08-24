using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodigoComun.Negocio;
using CodigoComun.DTO;

namespace WebApiStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        ArticuloServices articuloServices = new ArticuloServices();

        [HttpGet("GetArticulo/{id}")]
        public IActionResult GetArticulo(int id)
        {
            ArticuloServices articuloServices = new ArticuloServices();
            var articuloDTO = articuloServices.BuscarId(id);

            return Ok(articuloDTO);
        }

        [HttpGet("GetAllArticulos")]
        public IEnumerable<ArticuloDTO> GetAllArticulos()
        {
            
            List<ArticuloDTO> articulosDTO = articuloServices.GetAllArticulos();

            return articulosDTO;
        }

        [HttpPost("AddArticulo")]
        public IActionResult AddArticulo(ArticuloDTO articuloDTO) { 

            articuloDTO = articuloServices.AgregarArticulo(articuloDTO);
            return Ok(articuloDTO);

        }


        [HttpPut("ModArticulo")]
        public IActionResult ModArticulo(ArticuloDTO articuloDTO)
        {

            articuloDTO = articuloServices.ActualizarArticulo(articuloDTO);
            return Ok(articuloDTO);

        }

        [HttpDelete("DropArticulo/{id}")]
        public IActionResult DropArticulo(int id)
        {
            ArticuloDTO articuloDTO = articuloServices.BorrarArticulo(id);
            return Ok(articuloDTO);

        }

        [HttpGet("GetArticulosStock/{id}")]
        public IEnumerable<StockDTO> GetArticulosStock(int id)
        {

            List<StockDTO> stockDTO = articuloServices.GetArticulosStock(id);

            return stockDTO;
        }



    }
}
