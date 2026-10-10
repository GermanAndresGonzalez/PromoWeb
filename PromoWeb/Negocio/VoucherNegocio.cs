using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Negocio;

namespace Negocio
{
    public class VoucherNegocio
    {
        // Trae el voucher junto con el cliente y el artículo (si ya fue canjeado).
        private const string SelectBase =
            "SELECT V.CodigoVoucher, V.FechaCanje, " +
            "V.IdCliente, C.Documento, C.Nombre AS ClienteNombre, C.Apellido, C.Email, C.Direccion, C.Ciudad, C.CP, " +
            "V.IdArticulo, A.Codigo AS ArticuloCodigo, A.Nombre AS ArticuloNombre, A.Descripcion AS ArticuloDescripcion, A.Precio " +
            "FROM Vouchers V " +
            "LEFT JOIN Clientes C ON C.Id = V.IdCliente " +
            "LEFT JOIN ARTICULOS A ON A.Id = V.IdArticulo";

        public List<Voucher> Listar()
        {
            List<Voucher> lista = new List<Voucher>();
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

        // Devuelve null si el código no existe.
        public Voucher BuscarPorCodigo(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(SelectBase + " WHERE V.CodigoVoucher = @codigo");
                datos.setearParametro("@codigo", codigo);
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

        // true si el voucher existe y todavía no fue canjeado.
        public bool EstaDisponible(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Vouchers WHERE CodigoVoucher = @codigo AND IdCliente IS NULL");
                datos.setearParametro("@codigo", codigo);

                return datos.obtenerId() > 0;
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

        // Crea un voucher nuevo sin canjear.
        public void Agregar(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Vouchers (CodigoVoucher) VALUES (@codigo);");
                datos.setearParametro("@codigo", codigo);
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

        // Canjea el voucher. Devuelve false si el código no existe o ya estaba canjeado.
        // La condición IdCliente IS NULL va en el propio UPDATE para que dos canjes
        // simultáneos del mismo código no puedan pasar los dos.
        // (Se usa obtenerId() porque ejecuta ExecuteScalar y devuelve el SELECT @@ROWCOUNT.)
        public bool Canjear(string codigo, int idCliente, int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Vouchers SET IdCliente = @idCliente, IdArticulo = @idArticulo, FechaCanje = @fecha " +
                                     "WHERE CodigoVoucher = @codigo AND IdCliente IS NULL; " +
                                     "SELECT @@ROWCOUNT;");

                datos.setearParametro("@idCliente", idCliente);
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearParametro("@fecha", DateTime.Today);
                datos.setearParametro("@codigo", codigo);

                return datos.obtenerId() > 0;
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

        // Solo borra vouchers que no fueron canjeados. Devuelve false si no se borró nada.
        public bool Eliminar(string codigo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM Vouchers WHERE CodigoVoucher = @codigo AND IdCliente IS NULL; " +
                                     "SELECT @@ROWCOUNT;");
                datos.setearParametro("@codigo", codigo);

                return datos.obtenerId() > 0;
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

        private Voucher Leer(AccesoDatos datos)
        {
            Voucher aux = new Voucher();

            aux.CodigoVoucher = (string)datos.Lector["CodigoVoucher"];

            aux.FechaCanje = datos.Lector["FechaCanje"] is DBNull
                ? (DateTime?)null
                : (DateTime)datos.Lector["FechaCanje"];

            if (!(datos.Lector["IdCliente"] is DBNull))
            {
                aux.Cliente = new Cliente();
                aux.Cliente.Id = (int)datos.Lector["IdCliente"];
                aux.Cliente.Documento = (string)datos.Lector["Documento"];
                aux.Cliente.Nombre = (string)datos.Lector["ClienteNombre"];
                aux.Cliente.Apellido = (string)datos.Lector["Apellido"];
                aux.Cliente.Email = (string)datos.Lector["Email"];
                aux.Cliente.Direccion = (string)datos.Lector["Direccion"];
                aux.Cliente.Ciudad = (string)datos.Lector["Ciudad"];
                aux.Cliente.CP = (int)datos.Lector["CP"];
            }

            // El artículo viene con sus datos básicos; Marca, Categoria e Imagenes no se cargan.
            if (!(datos.Lector["IdArticulo"] is DBNull))
            {
                aux.Articulo = new Articulo();
                aux.Articulo.Id = (int)datos.Lector["IdArticulo"];
                aux.Articulo.Codigo = datos.Lector["ArticuloCodigo"] is DBNull ? null : (string)datos.Lector["ArticuloCodigo"];
                aux.Articulo.Nombre = datos.Lector["ArticuloNombre"] is DBNull ? null : (string)datos.Lector["ArticuloNombre"];
                aux.Articulo.Descripcion = datos.Lector["ArticuloDescripcion"] is DBNull ? null : (string)datos.Lector["ArticuloDescripcion"];
                aux.Articulo.Precio = datos.Lector["Precio"] is DBNull ? 0 : (decimal)datos.Lector["Precio"];
            }

            return aux;
        }
    }
}
