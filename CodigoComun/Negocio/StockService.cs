using CodigoComun.Datos;
using CodigoComun.Entities;
using CodigoComun.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodigoComun.Negocio
{
    public class StockService
    {

        private StockRepository StockRepository = new StockRepository();


        public StockDTO VerificarStockDTO(StockDTO stockDTO) {

            StockRepository = new StockRepository();

            if (stockDTO == null)
            {
                stockDTO.HuboError = true;
                stockDTO.Mensaje = "El stock esta vacio,agregue datos";
                return stockDTO;
            }

            StockService stockService = new StockService();
            List<StockDTO> listaAuxiliar = new List<StockDTO>();
            listaAuxiliar = stockService.GetAllStocks();

            foreach (var item in listaAuxiliar) {
                if (item.IdArticulo == stockDTO.IdArticulo && item.IdDeposito == stockDTO.IdDeposito && stockDTO.Id != item.Id) {
                    stockDTO = item;
                    stockDTO.HuboError = true;
                    stockDTO.Mensaje = "Existe";
                    return stockDTO;
                }
            }

            stockDTO.HuboError = false;
            stockDTO.Mensaje = "No existe";
            return stockDTO;
        }
        


        public StockDTO AgregarStock(StockDTO stockAAgregar)
        {
            try
            {
                StockRepository = new StockRepository();
                stockAAgregar = VerificarStockDTO(stockAAgregar);
                if (stockAAgregar.HuboError == true) {
                        stockAAgregar.Mensaje = "ya existe un stock con esos datos";
                        return stockAAgregar;

                }
                stockAAgregar.HuboError = false;
                stockAAgregar.Mensaje = "";
                StockRepository = new StockRepository();

                Stock stock = stockAAgregar.GetStock(stockAAgregar);

                int r = StockRepository.AddStockDb(stock);

                if (r == 1)
                {
                    stockAAgregar.Mensaje = "Stock agregado";
                    return stockAAgregar;
                }
                else
                {
                    stockAAgregar.HuboError = true;
                    stockAAgregar.Mensaje = "No se pudo agregar el Stock";
                    return stockAAgregar;

                }
            }catch (Exception ex)
            {
                stockAAgregar.HuboError = true;
                stockAAgregar.Mensaje = $"Hubo una excepcion dando de alta al stock {ex.Message}";
                return stockAAgregar;
            }


        }

        public StockDTO ActualizarStock(StockDTO stockAActualizar)
        {

            try
            {
                StockRepository = new StockRepository();
                StockDTO stockAuxiliar = new StockDTO();

   
                

                stockAuxiliar = VerificarStockDTO(stockAActualizar);
                if (stockAuxiliar.HuboError ==true)
                {

                    stockAActualizar.Mensaje= "Ya existe el stock con esos Id de articulo y deposito";
                    return stockAActualizar;

                }
                 
                if (stockAActualizar.Cantidad == 0) {
                    stockAuxiliar = BorrarStock(stockAActualizar.Id);
                    return stockAActualizar;

                }
                

                Stock stock = stockAActualizar.GetStock(stockAActualizar);
                int r = StockRepository.UpdateStock(stock);

                if (r == 1)
                {
                    stockAActualizar.Mensaje = "Stock modificado";
                    return stockAActualizar;
                }
                else
                {
                    stockAActualizar.HuboError = true;
                    stockAActualizar.Mensaje = "No se pudo modificar el Stock";
                    return stockAActualizar;

                }

            }catch(Exception ex)
            {
                stockAActualizar.HuboError = true;
                stockAActualizar.Mensaje = $"Hubo una excepcion modificando al stock {ex.Message}";
                return stockAActualizar;

            }

        }

        public StockDTO BorrarStock(int itemId)
        {
            StockRepository = new StockRepository();
            StockDTO stockDTO = new StockDTO();
            try
            {

                int r = StockRepository.EliminarStockDb(itemId);
                

                if (r == 1)
                {
                    stockDTO.Mensaje = "Stock eliminado";
                    return stockDTO;
                }
                else
                {
                    stockDTO.HuboError = true;
                    stockDTO.Mensaje = "No se pudo eliminar el Stock";
                    return stockDTO;

                }
            }catch (Exception ex)
            {
                stockDTO.HuboError = true;
                stockDTO.Mensaje = $"Hubo una excepcion al eliminar el stock {ex.Message}";
                return stockDTO;
            }


        }

        public StockDTO BuscarId(int itemId)
        {

            StockRepository = new StockRepository();
            StockDTO stockAuxiliar = new StockDTO();
            
            stockAuxiliar = stockAuxiliar.GetStockDTO(StockRepository.GetStockById(itemId));

            return stockAuxiliar;



        }

        public List<StockDTO> GetAllStocks()
        {
            StockRepository = new StockRepository();
            return StockRepository.GetAllStocks();



        }



    }
}
