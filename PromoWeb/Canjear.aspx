<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Canjear.aspx.cs" Inherits="PromoWeb.Canjear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <hr />

    <p>Canje</p>
    <div class="container">

        <div class="row">
            <div class="col-3">
                <div class="p-3">
                    <div class="col">
                        <div class="card h-100 shadow-sm">
                            <asp:Image ID="imgArticulo" runat="server" />
                            <div class="card-body d-flex flex-column">
                                <h6 class="card-title fw-bold mb-2">
                                    <asp:Label ID="txtNombre" runat="server" />                                
                                </h6>
                            </div>
                        </div>

                </div>
                <div class="col-5">
                    <div class="p-3">
                        <asp:Label
                            ID="lblID"
                            Text="Código de ID"
                            CssClass="form-label"
                            runat="server" />
                    </div>
                </div>
                <div class="col-4">
                    <div class="p-3">
                        <asp:Label
                            ID="lblError"
                            Text="Error"
                            CssClass="form-label"
                            runat="server" />
                    </div>
                </div>

            </div>
        </div>
</asp:Content>
