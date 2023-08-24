using AutoMapper;
using CodigoComun.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoComun.DTO
{
    public class StockDTO : BaseDTO
    {
        //public Stock stock { set; get; }

        public int Id { get; set; }
        public int? IdArticulo { get; set; }
        public int? IdDeposito { get; set; }
        public decimal? Cantidad { get; set; }

        public virtual ArticuloDTO IdArticuloNavigation { get; set; }
        public virtual DepositoDTO IdDepositoNavigation { get; set; }


        public StockDTO(){
            Id = 0;
            IdArticulo = 0;
            IdDeposito = 0;
            Cantidad = 0;
            HuboError = false;
            Mensaje = "";
        }

        public Stock GetStock(StockDTO stockDTO)
        {

            Stock stockADevolver = new Stock();

            stockADevolver.Id = stockDTO.Id;
            stockADevolver.IdArticulo = stockDTO.IdArticulo;
            stockADevolver.IdDeposito = stockDTO.IdDeposito;
            stockADevolver.Cantidad = stockDTO.Cantidad;

            /*
            var config = new MapperConfiguration(cfg => cfg.CreateMap<StockDTO, Stock>());
            var mapper = new Mapper(config);

            Stock stockADevolver = mapper.Map<Stock>(stockDTO);
            */

            return stockADevolver;
        }

        public StockDTO GetStockDTO(Stock stock)
        {


            StockDTO stockADevolver = new StockDTO();
            stockADevolver.Id=stock.Id;
            stockADevolver.IdArticulo=stock.IdArticulo;
            stockADevolver.IdDeposito=stock.IdDeposito;
            stockADevolver.Cantidad=stock.Cantidad;

            /*
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Stock, StockDTO>());
            var mapper = new Mapper(config);

            StockDTO stockADevolver = mapper.Map<StockDTO>(stock);
            */

            return stockADevolver;
        }


        public string MostrarError()
        {

            return this.Mensaje;
        }

    }
}
