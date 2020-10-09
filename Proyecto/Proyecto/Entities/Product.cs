using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.Entities
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Descripcion { get; set; }
        public string Detalle { get; set; } 
        public decimal Descuento { get; set; }
        public decimal Iva { get; set; }
        public decimal Valor { get; set; }
        public string Referencia { get; set; }
        public int Cantidad { get; set; }
        public string FotoPrincipal { get; set; }
        public string TiempoGarantia { get; set; }
        public DateTime Garantia { get; set; }
        public DateTime FechaCreacion { get; set; }
        /*
         	Descripcion
	Descuento
	Detalle
	Iva
	Referencia
	Cantidad
	Valor
	FotoPrincipal
	TiempoGarantia
	Garantia
         */
    }
}
