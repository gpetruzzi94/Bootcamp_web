using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodigoComun.DTO;
using CodigoComun.Entities;


namespace CodigoComun.Datos
{
    public class StockRepository
    {
        StocksAppContext db = new StocksAppContext();


        public List<StockDTO> GetAllStocks()
        {
            StockDTO stockDTOAuxiliar = new StockDTO();
            List<Stock> stocks = new List<Stock>();

            stocks = db.Stocks.ToList();

            List<StockDTO> stockssDTO = new List<StockDTO>(stocks.Count);

            stocks.ForEach((item) =>
            {
                stockssDTO.Add(stockDTOAuxiliar.GetStockDTO(item));
            });
            return stockssDTO;



        }


        public Stock GetStockById(int id)
        {


            Stock Stock = new Stock();
            Stock = db.Stocks.Find(id);

            return Stock;

        }

        public int AddStockDb(Stock StockAAgregar)
        {


            db.Stocks.Add(StockAAgregar);
            int resultado = db.SaveChanges();


            return resultado;

        }

        public int EliminarStockDb(int id)
        {


            db.Stocks.Remove(db.Stocks.Find(id));

            int resultado = db.SaveChanges();


            return resultado;

        }

        public int UpdateStock(Stock StockAActualizar)
        {


            db.Entry(StockAActualizar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            int resultado = db.SaveChanges();


            return resultado;



        }


    }
}
