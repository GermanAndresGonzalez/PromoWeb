using Dominio;
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
            lblCliente.Text = Session["voucher"] as string;

            Articulo seleccionado = new Articulo();
            seleccionado = Session["ArticuloSeleccionado"] as Articulo;
            //seleccionado.Imagenes

            lblID.Text = seleccionado.Nombre;
        }
    }
}