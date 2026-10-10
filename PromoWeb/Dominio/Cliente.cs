using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public int CP { get; set; }

        public Cliente()
        {
        }

        public Cliente(string documento, string nombre, string apellido, string email, string direccion, string ciudad, int cp)
        {
            Documento = documento;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Direccion = direccion;
            Ciudad = ciudad;
            CP = cp;
        }

        public Cliente(int id, string documento, string nombre, string apellido, string email, string direccion, string ciudad, int cp)
            : this(documento, nombre, apellido, email, direccion, ciudad, cp)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"{Apellido}, {Nombre}";
        }
    }
}
