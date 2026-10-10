using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }

        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();

        public Articulo()
        {
        }

        public Articulo(string codigo, string nombre, string descripcion, decimal precio, Marca marca, Categoria categoria)
        {
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Marca = marca;
            Categoria = categoria;
        }

        public Articulo(int id, string codigo, string nombre, string descripcion, decimal precio, Marca marca, Categoria categoria, List<Imagen> imagenes)
            : this(codigo, nombre, descripcion, precio, marca, categoria)
        {
            Id = id;
            Imagenes = imagenes ?? new List<Imagen>();
        }

        public override string ToString()
        {
            return $"{Codigo} - {Nombre}";
        }
    }
}
