using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PromoWeb
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                // Use HTML placeholder so the TextBox value remains empty until the user types.
                txtVoucher.Attributes["placeholder"] = "Ingrese el código del voucher";
        }

        protected void txtVoucher_TextChanged(object sender, EventArgs e)
        {
            string strVoucher = txtVoucher.Text;
            if (strVoucher == "")
            {
                lblErrorVoucher.Text = "Error";
                txtVoucher.Text = "Error";
            }
        }

        protected void btnVoucher_Click(object sender, EventArgs e)
        {
            lblErrorVoucher.Text = "";

            string codigo = txtVoucher.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                lblErrorVoucher.Text = "Debe completar el campo";
                return;
            }

            VoucherNegocio negocio = new VoucherNegocio();

            if (!negocio.EstaDisponible(codigo))
            {
                lblErrorVoucher.Text = "No está disponible";
                return;
            }

            Voucher voucherEncontrado = negocio.BuscarPorCodigo(codigo);

            lblErrorVoucher.Text = voucherEncontrado.Cliente == null
                ? "Nulo"
                : voucherEncontrado.Cliente.Id.ToString();
            
            Session["voucher"] = codigo;
            Response.Redirect("MostrarArticulos.aspx", false);


        }
        
    }

}


