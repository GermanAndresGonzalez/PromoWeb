<%@ Page Title="Voucher" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Voucher.aspx.cs" Inherits="PromoWeb.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your application description page.</h3>
        <p>Use this area to provide additional information.</p>
        <hr />
        <div class="container">
            <div class="row">
                <div class="col-3">
                    <div class="p-3">
                        <asp:Label
                            ID="lblVoucher"
                            Text="Código de voucher"
                            CssClass="form-label"
                            runat="server" />
                    </div>
                </div>
                <div class="col-5">
                    <div class="p-3">
                        <asp:TextBox
                            ID="txtVoucher"
                            CssClass="form-control"
                            runat="server" />


                    </div>
                </div>
                <div class="col-4">
                    <div class="p-3">
                        
                        <asp:Button
                            ID="btnVoucher"
                            Text="Buscar Código"
                            CssClass="btn btn-primary"
                            AutoPostBack="True"
                            OnClick="btnVoucher_Click"
                            runat="server"  />
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-4">
                    <div class="p-3"></div>
                </div>
                <div class="col-4">
                    <div class="p-3">
                        <asp:Label ID="lblErrorVoucher"
                            Color="Red"
                            CssClass="form-label"
                            style="font-weight:bold;color:red;"
                            runat="server" />
                    </div>
                </div>

                <div class="col-4">
                    <div class="p-3"></div>
                </div>
            </div>
        </div>
        <div class="mb-3">
        </div>
    </main>
</asp:Content>
