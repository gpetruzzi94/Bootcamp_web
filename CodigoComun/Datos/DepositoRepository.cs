using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodigoComun.DTO;
using CodigoComun.Entities;

namespace CodigoComun.Datos
{
    public class DepositoRepository
    {
        StocksAppContext db = new StocksAppContext();


            
        public List<DepositoDTO> GetAllDepositos() {


            DepositoDTO depositoDTOAuxiliar = new DepositoDTO();
            List<Deposito> depositos = new List<Deposito>();

            depositos = db.Depositos.ToList();

            List<DepositoDTO> depositosDTO = new List<DepositoDTO>(depositos.Count);

            depositos.ForEach((item) =>
            {
                depositosDTO.Add(depositoDTOAuxiliar.GetDepositoDTO(item));
            });
            return depositosDTO;


        }


        public List<StockDTO> GetDepositosStock(int id)
        {


            List<Stock> stocks = new List<Stock>();

            stocks = db.Stocks.ToList();
            StockDTO stockDTOAuxiliar = new StockDTO();
            List<StockDTO> stocksDTO = new List<StockDTO>();

            stocks.ForEach((item) =>
            {
                if (item.IdDeposito == id)
                {
                    stocksDTO.Add(stockDTOAuxiliar.GetStockDTO(item));
                }

            });
            return stocksDTO;

        }

        public Deposito GetDepositoById(int id)
        {


            Deposito deposito = new Deposito();
            deposito = db.Depositos.Find(id);

            return deposito;

        }

        public int AddDepositoDb(Deposito depositoAAgregar) { 
        

            db.Depositos.Add(depositoAAgregar);
            int resultado = db.SaveChanges();


            return resultado;
        
        }

        public int EliminarDepositoDb(int id)
        {


            db.Depositos.Remove(db.Depositos.Find(id));

            int resultado = db.SaveChanges();


            return resultado;

        }

        public int UpdateDeposito(Deposito depositoAActualizar ) {


            db.Entry(depositoAActualizar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            
            int resultado = db.SaveChanges();


            return resultado;



        }

    }
}
