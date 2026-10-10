using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            AccesoDatos Datos = new AccesoDatos();
            List<Marca> ListaDeMarcas = new List<Marca>();
            try
            {
                Datos.setearConsulta("SELECT Id,Descripcion FROM MARCAS");

                Datos.ejecutarLectura();

                while (Datos.Lector.Read())
                {
                    Marca Auxiliar = new Marca();

                    Auxiliar.Id = (int)Datos.Lector["Id"];
                    Auxiliar.Descripcion = (string)Datos.Lector["Descripcion"];
                    ListaDeMarcas.Add(Auxiliar);
                }

                return ListaDeMarcas;
            }
            catch
            {
                throw;
            }
            finally
            {
                Datos.cerrarConexion();
            }
        }


        public void Agregar(Marca nueva)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.setearConsulta("INSERT INTO MARCAS (Descripcion) VALUES (@descripcion);");

                Datos.setearParametro("@descripcion", nueva.Descripcion);

                Datos.ejecutarAccion();
            }
            catch
            {
                throw;
            }
            finally
            {
                Datos.cerrarConexion();
            }
        }

        public void Modificar(Marca marca)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.setearConsulta("UPDATE MARCAS SET Descripcion = @descripcion WHERE Id = @id;");

                Datos.setearParametro("@descripcion", marca.Descripcion);
                Datos.setearParametro("@id", marca.Id);

                Datos.ejecutarAccion();
            }
            catch
            {
                throw;
            }
            finally
            {
                Datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @id;");

                Datos.setearParametro("@id", id);

                Datos.ejecutarAccion();
            }
            catch
            {
                throw;
            }
            finally
            {
                Datos.cerrarConexion();
            }
     }
  }
}
