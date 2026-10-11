<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IngDatosCliente.aspx.cs" Inherits="PromoWeb.IngDatosCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <hr />
    <h1><b>Ingresá tus datos</b></h1>


    <div style="grid: 1fr 1fr;" class="d-grid gap-1">
        <div class="row">
            <div class="col">
                <label for="txtNroDni" class="form-label">DNI</label>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:TextBox ID="txtNroDni" AutoPostBack="true" placeholder="99888777" CssClass="form-control" OnTextChanged="txtNros_changed" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <label for="txtNombre" class="form-label">Nombre</label>
            </div>
            <div class="col">
                <label for="txtApellido" class="form-label">Apellido</label>
            </div>
            <div class="col">
                <label for="txtEmail" class="form-label">Email</label>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:TextBox ID="txtNombre" AutoPostBack="true" placeholder="Juanito" OnTextChanged="txtNombre_changed" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox ID="txtApellido" AutoPostBack="true" placeholder="Argento" OnTextChanged="txtNombre_changed" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox ID="txtEmail" AutoPostBack="true" placeholder="email@email.com" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator
                    ID="revEmail"
                    runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="El formato del correo electrónico no es válido."
                    ForeColor="Red"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <label for="lblDireccion" class="form-label">Dirección</label>
            </div>
            <div class="col">
                <label for="lblCiudad" class="form-label">Ciudad</label>
            </div>
            <div class="col">
                <label for="lblCP" class="form-label">CP</label>
            </div>
        </div>
        <div class="row">
            <div class="col">
                <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" OnTextChanged="txtLetrasyNumeros_changed" AutoPostBack="true" placeholder="Dirección"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" OnTextChanged="txtNombre_changed" AutoPostBack="true" placeholder="Ciudad"></asp:TextBox>
            </div>
            <div class="col">
                <asp:TextBox ID="txtCP" CssClass="form-control" AutoPostBack="true" placeholder="CP" runat="server"></asp:TextBox>
            </div>
        </div>


        <div class="mt-3">
            <asp:CheckBox ID="chkAcepto" CssClass="custom-control-input" Text="" runat="server" />
            <label for="chkAcepto" class="custom-control-input">Acepto los términos y condiciones.</label>
        </div>
        <div class="mt-3">
            <asp:Button ID="btnDatosCliente" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnDatosCliente_Click" runat="server" />
        </div>

    </div>
</asp:Content>
