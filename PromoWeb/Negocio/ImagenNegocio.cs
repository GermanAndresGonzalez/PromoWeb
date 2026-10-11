using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> Listar(int idArticulo)
        {
            List<Imagen> imagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, IdArticulo, ImagenUrl FROM IMAGENES WHERE IdArticulo = @idArticulo");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen imagen = new Imagen(
                        (int)datos.Lector["Id"],
                        (int)datos.Lector["IdArticulo"],
                        datos.Lector["ImagenUrl"] is DBNull
                    ? null
                    : (string)datos.Lector["ImagenUrl"]
            );
                    imagenes.Add(imagen);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return imagenes;
        }

        public void Agregar(Imagen imagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@idArticulo, @imagenUrl)");
                datos.setearParametro("@idArticulo", imagen.IdArticulo);
                datos.setearParametro("@imagenUrl", imagen.ImagenUrl);
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

        public void EliminarPorArticulo(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @idArticulo");
                datos.setearParametro("@idArticulo", idArticulo);
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

        public void Modificar(Imagen imagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE IMAGENES SET ImagenUrl = @imagenUrl WHERE Id = @id");
                datos.setearParametro("@imagenUrl", imagen.ImagenUrl);
                datos.setearParametro("@id", imagen.Id);

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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM IMAGENES WHERE Id = @id");
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

        public string ObtenerPrimeraImagenUrl(int idArticulo)
        {
            try
            {
                var imagenes = Listar(idArticulo);

                if (imagenes != null && imagenes.Count > 0 &&
                    !string.IsNullOrWhiteSpace(imagenes[0].ImagenUrl))
                {
                    return imagenes[0].ImagenUrl;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }




    }
}