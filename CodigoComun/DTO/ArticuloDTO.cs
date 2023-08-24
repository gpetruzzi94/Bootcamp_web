using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using CodigoComun.Entities;

namespace CodigoComun.DTO
{
    public class ArticuloDTO:BaseDTO
    {

        //public Articulo articulo { set; get; }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public decimal? MinimoStock { get; set; }
        public string Proveedor { get; set; }
        public decimal? Precio { get; set; }
        public string Codigo { get; set; }


        public Articulo GetArticulo(ArticuloDTO articuloDTO) {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ArticuloDTO, Articulo>());
            var mapper = new Mapper(config);

            Articulo articuloADevolver = mapper.Map<Articulo>(articuloDTO);

            return articuloADevolver;
        }
        public ArticuloDTO GetArticuloDTO(Articulo articulo)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Articulo, ArticuloDTO>());
            var mapper = new Mapper(config);

            ArticuloDTO articuloADevolver = mapper.Map<ArticuloDTO>(articulo);

            return articuloADevolver;
        }


        public string MostrarError() {

            return this.Mensaje;
        }

    }
}
