using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using Negocio;
using Utilitarios;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion AS Marca, A.IdCategoria, C.Descripcion AS Categoria, A.Precio FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria");
                datos.ejecutarLectura();

                ImagenNegocio imagenNegocio = new ImagenNegocio();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = datos.Lector.SafeInt("Id");
                    aux.Codigo = datos.Lector.SafeString("Codigo");
                    aux.Nombre = datos.Lector.SafeString("Nombre");
                    aux.Descripcion = datos.Lector.SafeString("Descripcion");

                    aux.Marca = new Marca();
                    aux.Marca.Id = datos.Lector.SafeInt("IdMarca");
                    aux.Marca.Descripcion = datos.Lector.SafeString("Marca");

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = datos.Lector.SafeInt("IdCategoria");
                    aux.Categoria.Descripcion = datos.Lector.SafeString("Categoria");

                    aux.Precio = datos.Lector.SafeDecimal("Precio");

                    aux.Imagenes = imagenNegocio.Listar(aux.Id);

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) " +
                                     "VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @precio); SELECT @@IDENTITY;");

                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca != null ? (object)nuevo.Marca.Id : DBNull.Value);
                datos.setearParametro("@idCategoria", nuevo.Categoria != null ? (object)nuevo.Categoria.Id : DBNull.Value);
                datos.setearParametro("@precio", nuevo.Precio);

                int idArticulo = datos.obtenerId();
                nuevo.Id = idArticulo;

                return idArticulo;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @id; DELETE FROM ARTICULOS WHERE Id = @id;");
                datos.setearParametro("@id", id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> FiltrarAvanzado(string nombre, int idMarca, int idCategoria, decimal precioMin, decimal precioMax)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.IdMarca, M.Descripcion AS Marca, A.IdCategoria, C.Descripcion AS Categoria FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria WHERE 1 = 1";

                if (!string.IsNullOrEmpty(nombre))
                {
                    consulta += " AND (A.Nombre LIKE @nombre OR A.Codigo LIKE @nombre)";
                }

                if (idMarca > 0)
                {
                    consulta += " AND A.IdMarca = " + idMarca;
                }

                if (idCategoria > 0)
                {
                    consulta += " AND A.IdCategoria = " + idCategoria;
                }

                consulta += " AND A.Precio >= " + precioMin;
                if (precioMax > 0)
                {
                    consulta += " AND A.Precio <= " + precioMax;
                }

                datos.setearConsulta(consulta);

                if (!string.IsNullOrEmpty(nombre))
                {
                    datos.setearParametro("@nombre", "%" + nombre + "%");
                }

                datos.ejecutarLectura();

                ImagenNegocio imagenNegocio = new ImagenNegocio();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = datos.Lector.SafeInt("Id");
                    aux.Codigo = datos.Lector.SafeString("Codigo");
                    aux.Nombre = datos.Lector.SafeString("Nombre");
                    aux.Descripcion = datos.Lector.SafeString("Descripcion");
                    aux.Precio = datos.Lector.SafeDecimal("Precio");

                    if (datos.Lector["IdMarca"] != DBNull.Value)
                    {
                        aux.Marca = new Marca();
                        aux.Marca.Id = datos.Lector.SafeInt("IdMarca");
                        aux.Marca.Descripcion = datos.Lector.SafeString("Marca");
                    }

                    if (datos.Lector["IdCategoria"] != DBNull.Value)
                    {
                        aux.Categoria = new Categoria();
                        aux.Categoria.Id = datos.Lector.SafeInt("IdCategoria");
                        aux.Categoria.Descripcion = datos.Lector.SafeString("Categoria");
                    }

                    aux.Imagenes = imagenNegocio.Listar(aux.Id);

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Articulo> Filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string columna = "";
                string condicion = "";

                if (campo == "Nombre")
                {
                    columna = "A.Nombre";
                }
                else if (campo == "Codigo")
                {
                    columna = "A.Codigo";
                }
                else if (campo == "Precio")
                {
                    columna = "A.Precio";
                }

                if (criterio == "Contiene")
                {
                    condicion = " LIKE ";
                    filtro = "%" + filtro + "%";
                }
                else if (criterio == "Comienza con")
                {
                    condicion = " LIKE ";
                    filtro = filtro + "%";
                }
                else if (criterio == "Termina con")
                {
                    condicion = " LIKE ";
                    filtro = "%" + filtro;
                }
                else if (criterio == "Igual a")
                {
                    condicion = " = ";
                }
                else if (criterio == "Mayor que")
                {
                    condicion = " > ";
                }
                else if (criterio == "Menor que")
                {
                    condicion = " < ";
                }

                string where = " WHERE " + columna + condicion + "@filtro";

                datos.setearConsulta(
                    "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, " +
                    "A.IdMarca, M.Descripcion AS Marca, " +
                    "A.IdCategoria, C.Descripcion AS Categoria, A.Precio " +
                    "FROM ARTICULOS A " +
                    "LEFT JOIN MARCAS M ON M.Id = A.IdMarca " +
                    "LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria" +
                    where
                );

                datos.setearParametro("@filtro", filtro);

                datos.ejecutarLectura();

                ImagenNegocio imagenNegocio = new ImagenNegocio();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = datos.Lector.SafeInt("Id");
                    aux.Codigo = datos.Lector.SafeString("Codigo");
                    aux.Nombre = datos.Lector.SafeString("Nombre");
                    aux.Descripcion = datos.Lector.SafeString("Descripcion");
                    aux.Precio = datos.Lector.SafeDecimal("Precio");

                    aux.Marca = new Marca();
                    aux.Marca.Id = datos.Lector.SafeInt("IdMarca");
                    aux.Marca.Descripcion = datos.Lector.SafeString("Marca");

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = datos.Lector.SafeInt("IdCategoria");
                    aux.Categoria.Descripcion = datos.Lector.SafeString("Categoria");

                    aux.Imagenes = imagenNegocio.Listar(aux.Id);

                    lista.Add(aux);
                }

                return lista;
            }
            catch
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Articulo Articulo)
        {
            AccesoDatos Base = new AccesoDatos();

            try
            {
                Base.setearConsulta("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, Precio = @precio WHERE Id = @id;");

                Base.setearParametro("@codigo", Articulo.Codigo);
                Base.setearParametro("@nombre", Articulo.Nombre);
                Base.setearParametro("@descripcion", Articulo.Descripcion);
                Base.setearParametro("@idMarca", Articulo.Marca != null ? (object)Articulo.Marca.Id : DBNull.Value);
                Base.setearParametro("@idCategoria", Articulo.Categoria != null ? (object)Articulo.Categoria.Id : DBNull.Value);
                Base.setearParametro("@precio", Articulo.Precio);
                Base.setearParametro("@id", Articulo.Id);

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
        /*
        public Articulo ObtenerArticuloPorId(int id)
        {
            // Opción rápida: filtrar sobre Listar()
            var lista = new ArticuloNegocio().Listar();
            return lista.Find(a => a.Id == id);
        }
        */
    }
}