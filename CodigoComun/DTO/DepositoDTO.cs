using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CodigoComun.Entities;

namespace CodigoComun.DTO
{
    public class DepositoDTO:BaseDTO
    {
        //public Deposito deposito { set; get; }

        public int Id { get; set; }
        public decimal? Capacidad { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }


        public Deposito GetDeposito(DepositoDTO depositoDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<DepositoDTO, Deposito>());
            var mapper = new Mapper(config);

            Deposito depositoADevolver = mapper.Map<Deposito>(depositoDTO);

            return depositoADevolver;
        }

        public DepositoDTO GetDepositoDTO(Deposito deposito)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Deposito, DepositoDTO>());
            var mapper = new Mapper(config);

            DepositoDTO depositoADevolver = mapper.Map<DepositoDTO>(deposito);

            return depositoADevolver;
        }

        public string MostrarError()
        {

            return this.Mensaje;
        }

    }
}
