using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Blazor.Entidades
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }
        public int ClienteId { get; set; }
        public string TipoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("VentaId")]

        public virtual List<VentasDetalles> Detalles { get; set; }

        public Ventas()
        {
            VentaId = 0;
            ClienteId = 0;
            TipoPago = string.Empty;
            Monto = 0;
            Fecha = DateTime.Now;

            Detalles = new List<VentasDetalles>();
        }
    }
}
