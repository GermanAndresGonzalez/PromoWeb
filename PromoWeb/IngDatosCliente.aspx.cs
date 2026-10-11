using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PromoWeb
{
    public partial class IngDatosCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnDatosCliente_Click(object sender, EventArgs e)
        {

        }
        protected void txtNros_changed(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            resetearEstilo(txt);
            string texto = txt.Text;
            if (!(soloNumeros(texto)))
            {
                cambiarEstilo(txt);
            }
        }
        protected void txtNombre_changed(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            resetearEstilo(txt);
            string texto = txt.Text;
            if (!(soloLetras(texto)))
            {
                cambiarEstilo(txt);
            }
        }
        
        protected void txtLetrasyNumeros_changed(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            resetearEstilo(txt);
            string texto = txt.Text;
            if (!(soloLetrasYNumeros(texto)))
            {
                cambiarEstilo(txt);
            }
        }

        


        private bool soloLetras(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsLetter(caracter)) && (!char.IsWhiteSpace(caracter)))
                {
                    return false;
                }
            }
            return true;
        }

        private bool soloLetrasYNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsLetter(caracter)) && (!char.IsWhiteSpace(caracter)) && (!char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }




        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
                {
                if (!(char.IsNumber(caracter)))
                {
                    return false;
                }
            }
            return true;
        }

        private void cambiarEstilo(TextBox recuadro)
        {
            recuadro.BorderColor = (recuadro.BorderColor == System.Drawing.Color.Empty)
                ? System.Drawing.Color.Red
                : System.Drawing.Color.Empty;

            recuadro.BorderWidth = (recuadro.BorderWidth == Unit.Pixel(1))
                ? Unit.Pixel(4)
                : Unit.Pixel(1);              
        }

        private void resetearEstilo(TextBox recuadro)
        {
            recuadro.BorderColor = System.Drawing.Color.Empty;
            recuadro.BorderWidth = Unit.Pixel(1);
        }
            
    }
}