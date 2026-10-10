using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> ListaDeCategorias = new List<Categoria>();
            AccesoDatos Datos = new AccesoDatos();
            try
            {
                Datos.setearConsulta("SELECT Id, Descripcion FROM CATEGORIAS");
                Datos.ejecutarLectura();

                while (Datos.Lector.Read())
                {
                    Categoria auxiliar = new Categoria();
                    auxiliar.Id = (int)Datos.Lector["Id"];
                    auxiliar.Descripcion = (string)Datos.Lector["Descripcion"];
                    ListaDeCategorias.Add(auxiliar);
                }

                return ListaDeCategorias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Datos.cerrarConexion();
            }
        }

        public void Agregar(Categoria CategoriaNueva)
        {

            AccesoDatos Base = new AccesoDatos();

            Base.setearConsulta("INSERT INTO CATEGORIAS (Descripcion) Values(@Descripcion)");

            Base.setearParametro("@Descripcion", CategoriaNueva.Descripcion);

            Base.ejecutarAccion();

            Base.cerrarConexion();


        }


        public void Modificar(Categoria Categoria)
        {
            AccesoDatos Base = new AccesoDatos();
            try
            {
                Base.setearConsulta("UPDATE CATEGORIAS SET Descripcion = @descripcion WHERE Id = @id;");

                Base.setearParametro("@descripcion", Categoria.Descripcion);
                Base.setearParametro("@id", Categoria.Id);

                Base.ejecutarAccion();
            }
            catch
            {
                throw;
            }
            finally
            {
                Base.cerrarConexion();
            }
        }
        public void Eliminar(int id)
        {
            AccesoDatos Base = new AccesoDatos();
            try
            {
                Base.setearConsulta("DELETE FROM CATEGORIAS WHERE Id = @id;");
                Base.setearParametro("@id", id);
                Base.ejecutarAccion();
            }
            catch
            {
                throw;
            }
            finally
            {
                Base.cerrarConexion();
            }
        }

    }
}