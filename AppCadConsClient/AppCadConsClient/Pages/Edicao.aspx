<%@ Page Title="Edicao" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Edicao.aspx.cs" Inherits="AppCadConsClient.Pages.Edicao" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!-- Carrega o arquivo CSS corretamente -->
    <link rel="stylesheet" href="../Styles/jquery-ui.css" />
    <link rel="stylesheet" href="../Styles/select2.min.css" />

    <!-- Carrega os scripts JavaScript corretamente -->
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../Scripts/select2.min.js"></script>
    <script type="text/javascript" src="../Scripts/Cadastro.js"></script>

    <main>
        <table width="100%" style="text-align: initial">
            <tr>
                <td class="row" width="100%">
                    <section aria-labelledby="gettingStartedTitle">
                        <h2 id="gettingStartedTitle" runat="server">Cadastro de Clientes</h2>
                        <p>
                            Todos os campos são obrigatórios    para o cadastro.
                        </p>
                        <div id="painelCadastro" runat="server">
                            <div class="bod">
                                <div class="contai">
                                    <div class="registration-form">
                                        <div id="divNome" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="nome" Text="Nome:" />
                                            <asp:TextBox runat="server" ID="nome" CssClass="textbox" />
                                        </div>

                                        <div id="divCPF" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="cpf" Text="CPF:" />
                                            <asp:TextBox runat="server" ID="cpf" CssClass="textbox cpf" MaxLength="14" placeholder="000.000.000-00" />
                                        </div>

                                        <div id="divNacimento" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="DataN" Text="Data de nascimento :" />
                                            <asp:TextBox runat="server" ID="DataN" CssClass="textbox nascimento " MaxLength="10" placeholder="00/00/0000" />
                                        </div>

                                        <div id="divEmail" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="email" Text="E-mail:" />
                                            <asp:TextBox runat="server" ID="email" CssClass="textbox" placeholder="cliente@cliente.com.br" />
                                        </div>

                                        <div id="divRua" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="rua" Text="Rua:" />
                                            <asp:TextBox runat="server" ID="rua" CssClass="textbox" />
                                        </div>

                                        <div id="divNumero" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="numero" Text="Numero:" />
                                            <asp:TextBox runat="server" ID="numero" CssClass="textbox" />
                                        </div>

                                        <div id="divBairro" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="Bairro" Text="Bairro:" />
                                            <asp:TextBox runat="server" ID="Bairro" CssClass="textbox" />
                                        </div>

                                        <div id="divCEP" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="cep" Text="CEP:" />
                                            <asp:TextBox runat="server" ID="cep" CssClass="textbox cep" MaxLength="9" placeholder="00000-000" />
                                        </div>

                                        <div id="divEstado" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="ddlEstado" Text="UF:" />
                                            <asp:DropDownList runat="server" ID="ddlEstado" class="dropdownlist estado" AutoPostBack="True" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>

                                        <div id="divCidade" class="form-group" runat="server">
                                            <asp:Label runat="server" AssociatedControlID="ddlCity" Text="Cidade:" />
                                            <asp:DropDownList runat="server" ID="ddlCity" class="dropdownlist cidade" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <asp:Button ID="btnSalvar" class="btn btn-cad btn-md validarCPF" Text="Salvar" runat="server" OnClientClick="bloquearBotao('triggerSalvar|btnSalvar');" />
                                            <asp:Button ID="triggerSalvar" Style="display: none" runat="server" OnClick="btnSalvar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                </td>
            </tr>
        </table>
    </main>
</asp:Content>
