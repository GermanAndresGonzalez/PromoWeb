using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PromoWeb
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtVoucher_TextChanged(object sender, EventArgs e)
        {
            if (txtVoucher.Text != null)
            {
                lblErrorVoucher.Text = "";
            }
        }

        protected void btnVoucher_Click(object sender, EventArgs e)
        {
            if (txtVoucher.Text == "")
            {
                lblErrorVoucher.Text = "Error";
            }
        }
    }
}