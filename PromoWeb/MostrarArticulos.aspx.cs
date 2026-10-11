using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PromoWeb
{
    public partial class Canje : System.Web.UI.Page
    {
        private readonly ImagenNegocio _imagenNegocio = new ImagenNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarArticulos();
            }
        }

        private void CargarArticulos()
        {
            // Reemplazá esto por tu fuente real (ArticuloNegocio.Listar(), filtros, etc.)
            List<Articulo> lista = new ArticuloNegocio().Listar();
            MostrarArticulosEnPantalla(lista);
        }

        private void MostrarArticulosEnPantalla(List<Articulo> lista)
        {
            if (lista == null || lista.Count == 0)
            {
                rptArticulos.Visible = false;
                pnlSinResultados.Visible = true;
                return;
            }

            rptArticulos.Visible = true;
            pnlSinResultados.Visible = false;

            // Proyectamos a un objeto plano para el Repeater (evita lógica en el markup)
            var items = new List<object>();
            foreach (var art in lista)
            {
                string url = _imagenNegocio.ObtenerPrimeraImagenUrl(art.Id);
                items.Add(new
                {
                    Id = art.Id,
                    Nombre = art.Nombre,
                    MarcaDescripcion = art.Marca != null ? art.Marca.Descripcion : "",
                    ImagenUrl = url
                });
            }

            rptArticulos.DataSource = items;
            rptArticulos.DataBind();
        }



        protected void rptArticulos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerMas")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                // Buscamos el artículo completo (con Marca, Categoria e Imágenes)
                Articulo seleccionado = ObtenerArticuloPorId(id);

                if (seleccionado == null)
                    return;

                // Guardamos en Session
                Session["ArticuloSeleccionado"] = seleccionado;

                // Y disparamos el modal
                //MostrarModalArticulo(seleccionado);
                Response.Redirect("IngDatosCliente.aspx", false);
            }
        }
        private Articulo ObtenerArticuloPorId(int id)
        {
            // Opción rápida: filtrar sobre Listar()
            var lista = new ArticuloNegocio().Listar();
            return lista.Find(a => a.Id == id);
        }
    }
}