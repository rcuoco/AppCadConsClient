<%@ Page Title="Consulta" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="AppCadConsClient.Pages.Consulta" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript" src="../Scripts/Cadastro.js"></script>
    <main>
        <table width="100%" style="text-align: initial">
            <tr>
                <td class="row" width="100%">
                    <section aria-labelledby="gettingStartedTitle">
                        <h2 id="gettingStartedTitle">Consulta (Listagem de Clientes)</h2>
                        <p>
                            <table width="70%">

                                <tr class="trTitulo">
                                    <td width="300px">Nome:</td>
                                    <td width="100px">Data de</td>
                                    <td width="100px">Data Até</td>
                                    <td width="250px">CPF</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNome" Style="font-size: 11px; width: 300px;" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateDe" placeholder="DD/MM/AAAA" class="nascimento" Width="100px" Style="font-size: 11px;" runat="server" Columns="8" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateAte" placeholder="DD/MM/AAAA" class="nascimento" Width="100px" Style="font-size: 11px;" runat="server" Columns="8" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCPF" Style="font-size: 11px;" runat="server" class="cpf" placeholder="000.000.000-00"/>
                                    </td>
                                </tr>
                            </table>
                        </p>
                        <p>
                            <asp:Button ID="btnConsultar" class="btn btn-primary btn-md" Text="Consultar" runat="server" OnClick="btnConsultar_Click" />
                        </p>
                        <p>
                                <asp:GridView ID="grvResultados" runat="server"
                                    AutoGenerateColumns="False"
                                    GridLines="None"
                                    EmptyDataText="Nenhum cadastro encontrado." Width="100%"
                                    OnRowCommand="grvResultados_RowCommand">
                                    <columns>
                                        <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="Left" HeaderText="Nome">
                                            <itemstyle horizontalalign="Left" />
                                            <itemtemplate><%# Eval("Nome")%></itemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="Left" HeaderText="E-mail">
                                            <itemstyle horizontalalign="Left" />
                                            <itemtemplate><%# Eval("Email")%></itemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="Left" HeaderText="Data de Aniversário">
                                            <itemstyle horizontalalign="Left" />
                                            <itemtemplate><%# Eval("DataNascimento")%></itemtemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="Left" HeaderText="Ações">
                                            <headerstyle width="10%" />
                                            <itemstyle horizontalalign="Center" />
                                            <itemtemplate>
                                                <asp:Button ID="btn_view" class="btn btn-primary btn-md" Text="Visualizar" runat="server"  CommandArgument='<%# Eval("CPF")%>' OnClick="btnVisualizar_Click" />
                                            </itemtemplate>
                                        </asp:TemplateField>
                                    </columns>
                                </asp:GridView>
                        </p>
                    </section>
                </td>
            </tr>
        </table>
    </main>
</asp:Content>
