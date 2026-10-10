using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Voucher
    {
        // PK de la tabla (varchar), no es autoincremental
        public string CodigoVoucher { get; set; }

        // IdCliente, FechaCanje e IdArticulo son NULL en la base
        // mientras el voucher no fue canjeado.
        public Cliente Cliente { get; set; }
        public DateTime? FechaCanje { get; set; }
        public Articulo Articulo { get; set; }

        public bool Canjeado
        {
            get { return Cliente != null && FechaCanje.HasValue && Articulo != null; }
        }

        public Voucher()
        {
        }

        public Voucher(string codigoVoucher)
        {
            CodigoVoucher = codigoVoucher;
        }

        public Voucher(string codigoVoucher, Cliente cliente, DateTime? fechaCanje, Articulo articulo)
            : this(codigoVoucher)
        {
            Cliente = cliente;
            FechaCanje = fechaCanje;
            Articulo = articulo;
        }

        public override string ToString()
        {
            return CodigoVoucher;
        }
    }
}
