using CodigoComun.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppTarea11.ViewModel
{
    public class StockViewModel
    {

        public StockDTO stockDTO { get; set; }

        public List<DepositoDTO> depositos { get; set; }
        public List<ArticuloDTO> articulos { get; set; }

        public SelectList selectDepositosList { get; set; }
        public SelectList selectArticulosList { get; set; }
    }
}
