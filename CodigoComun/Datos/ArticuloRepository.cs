using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodigoComun.Entities;
using CodigoComun.DTO;

namespace CodigoComun.Datos
{
    public class ArticuloRepository
    {
        StocksAppContext db = new StocksAppContext();
        public List<ArticuloDTO> GetAllArticulos() { 
        
            ArticuloDTO articuloDTOAuxiliar = new ArticuloDTO();
            List<Articulo> articulos = new List<Articulo>();

            articulos = db.Articulos.ToList();

            List<ArticuloDTO> articulosDTO = new List<ArticuloDTO>(articulos.Count);

            articulos.ForEach((item) =>
            {
                articulosDTO.Add(articuloDTOAuxiliar.GetArticuloDTO(item));
            });
            return articulosDTO;
        
        }
        public List<StockDTO> GetArticulosStock(int id)
        {

            
            List<Stock> stocks = new List<Stock>();

            stocks = db.Stocks.ToList();
            StockDTO stockDTOAuxiliar = new StockDTO();
            List<StockDTO> stocksDTO = new List<StockDTO>();

            stocks.ForEach((item) =>
            {
                if(item.IdArticulo == id){
                    stocksDTO.Add(stockDTOAuxiliar.GetStockDTO(item));
                }
                
            });
            return stocksDTO;

        }

        public Articulo GetArticuloById(int id) { 
        
            Articulo articulo = new Articulo();
            articulo = db.Articulos.Find(id);
            return articulo;

        
        }

        public int AddArticulo(Articulo articuloAAgregar) {

            db.Articulos.Add(articuloAAgregar);
            int resultado = db.SaveChanges();  
            return resultado;

        }


        public int EliminarArticulo(int id) {

            db.Articulos.Remove(db.Articulos.Find(id));
            int resultado = db.SaveChanges();

            return resultado;


        }


        public int UpdateArticulo(Articulo articuloAActualizar) {


            db.Entry(articuloAActualizar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            int resultado = db.SaveChanges();

            return resultado;
        
        
        }



    }



}


