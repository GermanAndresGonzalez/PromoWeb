<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MostrarArticulos.aspx.cs" Inherits="PromoWeb.Canje" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container my-4">
        <h2 class="mb-4">Artículos</h2>

        <asp:Panel ID="pnlSinResultados" runat="server" Visible="false"
            CssClass="alert alert-secondary" role="alert">
            No se encontraron artículos relacionados.
        </asp:Panel>

        <div class="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4">
            <asp:Repeater ID="rptArticulos" runat="server"
                OnItemCommand="rptArticulos_ItemCommand">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100 shadow-sm">

                            <img src='<%# ResolveUrl(Eval("ImagenUrl") as string ?? "~/Content/ImagenDefault.png") %>'
                                class="card-img-top"
                                style="height: 160px; object-fit: contain; background-color: #f8f9fa;"
                                alt='<%# Eval("Nombre") %>'
                                onerror="this.onerror=null;this.src='<%# ResolveUrl("~/Content/ImagenDefault.png") %>';" />

                            <div class="card-body d-flex flex-column">
                                <h6 class="card-title fw-bold mb-2">
                                    <%# Eval("MarcaDescripcion") %> <%# Eval("Nombre") %>
                                </h6>
                            </div>

                            <div class="card-footer bg-transparent border-0 pt-0">
                                <asp:LinkButton ID="btnVerMas" runat="server"
                                    CssClass="btn btn-primary w-100"
                                    CommandName="VerMas"
                                    CommandArgument='<%# Eval("Id") %>'>
                                    Canjear
                                </asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

</asp:Content>


