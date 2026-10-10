using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;

namespace Negocio
{
    public class ClienteNegocio
    {
        private const string SelectBase = "SELECT Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP FROM Clientes";

        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(SelectBase);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    lista.Add(Leer(datos));
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

        public Cliente BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(SelectBase + " WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return Leer(datos);

                return null;
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

        // Devuelve null si no existe un cliente con ese documento.
        public Cliente BuscarPorDocumento(string documento)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(SelectBase + " WHERE Documento = @documento");
                datos.setearParametro("@documento", documento);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return Leer(datos);

                return null;
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

        // Inserta el cliente, le asigna el Id generado y lo devuelve.
        public int Agregar(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) " +
                                     "VALUES (@documento, @nombre, @apellido, @email, @direccion, @ciudad, @cp); " +
                                     "SELECT SCOPE_IDENTITY();");

                datos.setearParametro("@documento", nuevo.Documento);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@direccion", nuevo.Direccion);
                datos.setearParametro("@ciudad", nuevo.Ciudad);
                datos.setearParametro("@cp", nuevo.CP);

                int id = datos.obtenerId();
                nuevo.Id = id;

                return id;
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

        public void Modificar(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Clientes SET Documento = @documento, Nombre = @nombre, Apellido = @apellido, " +
                                     "Email = @email, Direccion = @direccion, Ciudad = @ciudad, CP = @cp WHERE Id = @id;");

                datos.setearParametro("@documento", cliente.Documento);
                datos.setearParametro("@nombre", cliente.Nombre);
                datos.setearParametro("@apellido", cliente.Apellido);
                datos.setearParametro("@email", cliente.Email);
                datos.setearParametro("@direccion", cliente.Direccion);
                datos.setearParametro("@ciudad", cliente.Ciudad);
                datos.setearParametro("@cp", cliente.CP);
                datos.setearParametro("@id", cliente.Id);

                datos.ejecutarAccion();
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

        // Falla (SqlException 547) si el cliente tiene vouchers canjeados, por la FK FK_Vouchers_Clientes.
        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Clientes WHERE Id = @id;");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
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

        private Cliente Leer(AccesoDatos datos)
        {
            Cliente aux = new Cliente();

            aux.Id = (int)datos.Lector["Id"];
            aux.Documento = (string)datos.Lector["Documento"];
            aux.Nombre = (string)datos.Lector["Nombre"];
            aux.Apellido = (string)datos.Lector["Apellido"];
            aux.Email = (string)datos.Lector["Email"];
            aux.Direccion = (string)datos.Lector["Direccion"];
            aux.Ciudad = (string)datos.Lector["Ciudad"];
            aux.CP = (int)datos.Lector["CP"];

            return aux;
        }
    }
}
