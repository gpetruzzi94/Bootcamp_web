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
    public class DepositoServices
    {
        DepositoRepository depositoRepository = new DepositoRepository();

        public bool VerificarDepositoDTO(int id)
        {

            StockService stckService = new StockService();
            List<StockDTO> listaAuxiliar = stckService.GetAllStocks();

            foreach (var item in listaAuxiliar)
            {
                if (item.IdDeposito == id)
                {
                    return true;
                }
            }

            return false;
        }

        public bool VerificarDepositoDTONombre(DepositoDTO deposito)
        {

            if (deposito.Id == null)
            {
                deposito.HuboError = true;
                deposito.Mensaje = "El deposito esta vacio,agregue datos";
                return true;
            }

            DepositoServices depositoServices = new DepositoServices();
            List<DepositoDTO> listaAuxiliar = depositoServices.GetTodosLosDepositos();

            foreach (var item in listaAuxiliar)
            {
                if (item.Id != deposito.Id && item.Nombre == deposito.Nombre)
                {
                    deposito.HuboError = true;
                    deposito.Mensaje = "No se puede agregar un deposito con un nombre que ya existente";
                    return true;
                }
            }

            return false;
        }


        public List<DepositoDTO> GetTodosLosDepositos()
        {
             
            return depositoRepository.GetAllDepositos();


        }




        public DepositoDTO GetDepositoPorId(int id)
        {
            DepositoDTO depositoAuxiliar = new DepositoDTO();
            depositoAuxiliar =depositoAuxiliar.GetDepositoDTO(depositoRepository.GetDepositoById(id));

            return depositoAuxiliar;
            

        }

        public DepositoDTO AddDeposito(DepositoDTO depositoAAgregar)
        {
            try
            {
                if (VerificarDepositoDTONombre(depositoAAgregar))
                {
                    
                    return depositoAAgregar;
                }
                Deposito deposito = depositoAAgregar.GetDeposito(depositoAAgregar);
                int r = depositoRepository.AddDepositoDb(deposito);


                if (r == 1)
                {
                    depositoAAgregar.Mensaje = "Deposito agregado";
                    return depositoAAgregar;
                }
                else
                {
                    depositoAAgregar.HuboError = true;
                    depositoAAgregar.Mensaje = "No se pudo agregar el deposito";
                    return depositoAAgregar;

                }
            }catch (Exception ex)
            {
                depositoAAgregar.HuboError = true;
                depositoAAgregar.Mensaje = $"Hubo una excepcion agregando al deposito {ex.Message}";
                return depositoAAgregar;

            }

        }

        public DepositoDTO EliminarDeposito(int id)
        {

            DepositoDTO depositoAEliminar = new DepositoDTO();
            try
            {


                if (VerificarDepositoDTO(id)) {
                    depositoAEliminar.HuboError=true;
                    depositoAEliminar.Mensaje = "Este deposito esta en uso en un stock,no se puede eliminar";
                    return depositoAEliminar;
                }

                int r = depositoRepository.EliminarDepositoDb(id);


                if (r == 1)
                {
                    depositoAEliminar.Mensaje = "Deposito eliminado";
                    return depositoAEliminar;
                }
                else
                {
                    depositoAEliminar.HuboError = true;
                    depositoAEliminar.Mensaje = "No se pudo eliminar el deposito";
                    return depositoAEliminar;

                }

            }catch (Exception ex)
            {
                depositoAEliminar.HuboError = true;
                depositoAEliminar.Mensaje = $"Hubo una excepcion eliminando al deposito {ex.Message}";
                return depositoAEliminar;

            }

        }


        public DepositoDTO ModificarDeposito(DepositoDTO depositoAModificar) {


            try
            {
                if (VerificarDepositoDTONombre(depositoAModificar))
                {
                    return depositoAModificar;
                }

                Deposito deposito = depositoAModificar.GetDeposito(depositoAModificar);
                int r = depositoRepository.UpdateDeposito(deposito);

                if (r == 1)
                {
                    depositoAModificar.Mensaje = "Deposito modificado";
                    return depositoAModificar;
                }
                else
                {
                    depositoAModificar.HuboError = true;
                    depositoAModificar.Mensaje = "No se pudo modificar el deposito";
                    return depositoAModificar;
                }
            }catch (Exception ex) {
                depositoAModificar.HuboError = true;
                depositoAModificar.Mensaje = $"Hubo una excepcion modificando al deposito {ex.Message}";
                return depositoAModificar;

            }



        
        }

        public List<StockDTO> GetDepositosStock(int id)
        {

            return depositoRepository.GetDepositosStock(id);


        }

    }
}
