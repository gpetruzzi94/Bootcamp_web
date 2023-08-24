using CodigoComun.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodigoComun.Entities;
using CodigoComun.DTO;

namespace CodigoComun.Negocio
{

    public class ArticuloServices
    {
        private ArticuloRepository articuloRepository =new ArticuloRepository();


        public bool VerificarArticuloDTO(int id) {
            
            StockService stckService = new StockService();
            List<StockDTO> listaAuxiliar = stckService.GetAllStocks();

            foreach(var item in listaAuxiliar)
            {
                if(item.IdArticulo == id)
                {
                    return true;
                }
            }

            return false;
        }

        public bool VerificarArticuloDTOCodigo(ArticuloDTO articulo)
        {

            ArticuloServices articuloServices = new ArticuloServices();
            List<ArticuloDTO> listaAuxiliar = articuloServices.GetAllArticulos();

            foreach (var item in listaAuxiliar)
            {
                if (item.Id != articulo.Id && item.Codigo == articulo.Codigo)
                {
                    articulo.HuboError = true;
                    articulo.Mensaje = "No se puede agregar un articulo con un codigo ya existente";
                    return true;
                }
            }

            return false;
        }

        public ArticuloDTO AgregarArticulo(ArticuloDTO articuloAAGregar) {
            try
            {
                if (VerificarArticuloDTOCodigo(articuloAAGregar)) { 
                    return articuloAAGregar;
                }

                Articulo articulo = articuloAAGregar.GetArticulo(articuloAAGregar);
                int r = articuloRepository.AddArticulo(articulo);

                if (r == 1)
                {
                    articuloAAGregar.Mensaje = "Articulo agregado";
                    return articuloAAGregar;
                }
                else
                {
                    articuloAAGregar.HuboError = true;
                    articuloAAGregar.Mensaje = "No se pudo agregar el articulo";
                    return articuloAAGregar;

                }

            }
            catch (Exception ex) { 
                articuloAAGregar.HuboError=true;
                articuloAAGregar.Mensaje = $"Hubo una excepcion dando de alta al articulo {ex.Message }";
                return articuloAAGregar;
            
            }
        
        
        }

        public ArticuloDTO ActualizarArticulo(ArticuloDTO articuloAActualizar)
        {
            try {

                if (VerificarArticuloDTOCodigo(articuloAActualizar))
                {
                    return articuloAActualizar;
                }

                Articulo articuloAuxiliar = articuloAActualizar.GetArticulo(articuloAActualizar);
                int r = articuloRepository.UpdateArticulo(articuloAuxiliar);

                if (r == 1)
                {
                    articuloAActualizar.Mensaje = "Articulo modificado";
                    return articuloAActualizar;
                }
                else
                {
                    articuloAActualizar.HuboError = true;
                    articuloAActualizar.Mensaje = "No se pudo modificar el articulo";
                    return articuloAActualizar;

                }
        
            }catch (Exception ex) { 
                articuloAActualizar.HuboError=true;
                articuloAActualizar.Mensaje = $"Hubo una excepcion modificando al articulo {ex.Message }";
                return articuloAActualizar;
            
            }


}

        public ArticuloDTO BorrarArticulo(int id)
        {
            ArticuloDTO articuloAEliminar = new ArticuloDTO();
            try {

                if (VerificarArticuloDTO(id)) {

                    articuloAEliminar.HuboError = true;
                    articuloAEliminar.Mensaje = "Este articulo esta en uso en un stock,no se puede eliminar";
                    return articuloAEliminar;
                }

                int r = articuloRepository.EliminarArticulo(id);

                if (r == 1)
                {
                    articuloAEliminar.Mensaje = "Articulo eliminado";
                    return articuloAEliminar;
                }
                else
                {
                    articuloAEliminar.HuboError = true;
                    articuloAEliminar.Mensaje = "No se pudo eliminar el articulo";
                    return articuloAEliminar;

                }

            }
            catch (Exception ex)
            {
                articuloAEliminar.HuboError = true;
                articuloAEliminar.Mensaje = $"Hubo una excepcion eliminando al articulo {ex.Message}";
                return articuloAEliminar;

            }

            

        }

        public ArticuloDTO BuscarId(int itemId) { 
        

            ArticuloDTO articuloAuxiliar = new ArticuloDTO();
            
            articuloAuxiliar = articuloAuxiliar.GetArticuloDTO(articuloRepository.GetArticuloById(itemId));

            return articuloAuxiliar;

   
        
        }

        public List<ArticuloDTO> GetAllArticulos() {

            return articuloRepository.GetAllArticulos();
        
        
        }
        public List<StockDTO> GetArticulosStock(int id)
        {

            return articuloRepository.GetArticulosStock(id);


        }


    }
}
