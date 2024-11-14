<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppCadConsClient._Default" %>


<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">

    <main style="text-align: center;">
        <table width="100%" style="text-align: center; flex-wrap: nowrap;">
            <tr>
                <td class="row" width="40%" >
                    <section aria-labelledby="gettingStartedTitle">
                        <h2 id="gettingStartedTitle">Consulta (Listagem de Clientes)</h2>
                        <p>
                            <a class="btn btn-primary btn-md" href="Pages/Consulta.aspx">Consultar &raquo;</a>
                        </p>
                    </section>
                </td>
                <td class="row" width="40%">
                    <section aria-labelledby="librariesTitle">
                        <h2 id="librariesTitle">Cadastro de Cliente</h2>
                        <p>
                            <a class="btn btn-cad btn-md" href="Pages/Cadastro.aspx">Cadastrar &raquo;</a>
                        </p>

                    </section>
                </td>
            </tr>
        </table>
        <div class="row">
        </div>
    </main>
</asp:Content>
