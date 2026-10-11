using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PromoWeb
{
    public partial class Canjear : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                /*
                //lblCliente.Text = Session["voucher"] as string;
                //Articulo seleccionado = Session["ArticuloSeleccionado"] as Articulo;
                if (seleccionado == null)
                {
                    Response.Redirect("MostrarArticulos.aspx", false);
                    return;
                }

                CargarDatos(seleccionado);
                */


            }
        }
        protected void CargarDatos(Articulo seleccionado)
        {
            ImagenNegocio imaNegocio = new ImagenNegocio();
            string urlIma = imaNegocio.ObtenerPrimeraImagenUrl(seleccionado.Id) as string;
            imgArticulo.ImageUrl = urlIma;
            txtNombre.Text = seleccionado.Nombre;
            
        }


    }
}