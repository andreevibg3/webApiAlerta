﻿<%@ Page Title="" Language="C#" MasterPageFile="~/paginaMaestra.Master" AutoEventWireup="true" CodeBehind="paginaAlerta.aspx.cs" Inherits="ClienteAlerta.paginaAlerta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <script>
        function verMapa(strLocation) {
            var partsOfStr = strLocation.replace("Ver", "").trim().split('|');
            mymap.setView([partsOfStr[0], partsOfStr[1]], 18);
            //alert("" + strLocation);
        }
    </script>
    <div class="row">
        <div class="col-md-6">
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="Label1" runat="server" Text="Asignar A:"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false" Style="margin-bottom: 10px; float: right; width: calc(100% - 78px);">
                            <asp:ListItem Value="-1">Sin asignar</asp:ListItem>
                            <asp:ListItem Value="POLICIA">POLICIA</asp:ListItem>
                            <asp:ListItem Value="ABOGADO">ABOGADO</asp:ListItem>
                            <asp:ListItem Value="TRABAJADOR_SOCIAL">TRABAJADOR_SOCIAL</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="table-responsive-sm" style="margin-bottom: 15px;">
                        <asp:GridView Width="100%" DataKeyNames="CODIGO" ID="GridView1" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="false">
                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                            <Columns>

                                <%--<asp:TemplateField HeaderText="Asignado a" SortExpression="UsuarioAsignado">
                        <ItemTemplate>
                            <asp:DropDownList ID="DropDownList2" Text='<%# Bind("UsuarioAsignado") %>' AppendDataBoundItems="true" runat="server">
                                <asp:ListItem Value="">Sin asignar</asp:ListItem>
                                <asp:ListItem Value="POLICIA">POLICIA</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                                <asp:BoundField HeaderText="Usuario" DataField="usuario" />
                                <asp:BoundField HeaderText="Estado" DataField="estado" />
                                <asp:BoundField HeaderText="Fecha" DataField="fechaCreacion" />
                                <asp:BoundField HeaderText="Asignado A" DataField="UsuarioAsignado" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" CommandName="editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Asignar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnVer" runat="server" CommandName="ver" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="verMapa($(event.target).text())" Text='<%# "Ver " + Eval("localizacion") %>' Width="35px" Height="20px" style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis; word-break: break-all; word-wrap: break-word;">Ver</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="Tan" />
                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                            <SortedAscendingCellStyle BackColor="#FAFAE7" />
                            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
                            <SortedDescendingCellStyle BackColor="#E1DB9C" />
                            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-md-6">

            <div id="mapid" style="width: 100%; height: 500px;">
            </div>
            <style>
                .resetzoom {
                    cursor: pointer;
                    background-color: #fff;
                    padding: 4px;
                    border-radius: 3px;
                    border: 2px solid #6B95D1;
                }

                    .resetzoom:hover {
                        background-color: #f1f1f1;
                    }
            </style>
            <script>

                var mymap = L.map('mapid').setView([-1.8, -78.9], 7);

                L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
                    maxZoom: 18,
                    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, ' +
                        '<a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, ' +
                        'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
                    id: 'mapbox.streets'
                }).addTo(mymap);

                (function () {
                    var control = new L.Control({ position: 'topleft' });
                    control.onAdd = function (map) {
                        var azoom = L.DomUtil.create('div', 'resetzoom');
                        azoom.innerHTML = "Zoom";
                        L.DomEvent
                            .disableClickPropagation(azoom)
                            .addListener(azoom, 'click', function () {
                                mymap.setView([-1.8, -78.9], 7);
                            }, azoom);
                        return azoom;
                    };
                    return control;
                }())
                    .addTo(mymap);

                var markerClusters = L.markerClusterGroup();



            </script>
            <div class="card-columns" id="contenedorAgentes" runat="server"></div>

        </div>
    </div>
</asp:Content>
