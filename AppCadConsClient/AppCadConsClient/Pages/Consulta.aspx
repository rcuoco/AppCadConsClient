<%@ Page Title="Consulta" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="AppCadConsClient.Pages.Consulta" %>

<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!-- Carrega o arquivo CSS corretamente -->
    <link rel="stylesheet" href="../Styles/jquery-ui.css" />
    <link rel="stylesheet" href="../Styles/select2.min.css" />

    <!-- Carrega os scripts JavaScript corretamente -->
    <script type="text/javascript" src="../Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../Scripts/select2.min.js"></script>
    <script type="text/javascript" src="../Scripts/Consulta.js"></script>

    <main>
        <table width="100%" style="text-align: initial">
            <tr>
                <td class="row" width="100%">
                    <section aria-labelledby="gettingStartedTitle">
                        <h2 id="gettingStartedTitles">Consulta (Listagem de Clientes)</h2>

                        <table width="70%">

                            <tr class="trTitulo">
                                <td width="300px">Nome:</td>
                                <td width="100px">Data Aniversário de</td>
                                <td width="100px">Data Aniversário Até</td>
                                <td width="250px">CPF</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtNome" class="nome" Style="font-size: 11px; width: 300px;" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateDe" placeholder="DD/MM/AAAA" class="nascimento" Width="100px" Style="font-size: 11px;" runat="server" Columns="8" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDateAte" placeholder="DD/MM/AAAA" class="nascimento" Width="100px" Style="font-size: 11px;" runat="server" Columns="8" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCPF" Style="font-size: 11px;" runat="server" class="cpf" placeholder="000.000.000-00" />
                                </td>
                            </tr>
                        </table>

                        <p>
                            <asp:Button ID="btnConsultar" class="btn btn-primary btn-md" Text="Consultar" runat="server" OnClick="btnConsultar_Click" />
                            <asp:Button ID="btnLimpar" class="btn btn-default btn-md" Text="Limpar" runat="server" OnClientClick="Limpar();" OnClick="triggerLimpar_Click" />
                            <asp:Button ID="triggerLimpar" Style="display: none" runat="server" OnClick="triggerLimpar_Click" />
                        </p>
                    </section>

                </td>
            </tr>
        </table>
        <table width="100%" style="text-align: initial">
            <tr>
                <td class="row" width="100%">
                    <section aria-labelledby="gettingStartedTitle">
                        <h2 id="gettingStartedTitle">Resultados</h2>
                        <p>
                            <asp:GridView ID="grvResultados" runat="server"
                                AutoGenerateColumns="False"
                                GridLines="None"
                                EmptyDataText="Nenhum cadastro encontrado." Width="100%"
                                OnRowCommand="grvResultados_RowCommand">
                                <Columns>
                                    <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="Left" HeaderText="Nome">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate><%# Eval("Nome")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="Left" HeaderText="E-mail">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate><%# Eval("Email")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="Left" HeaderText="Data de Aniversário">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate><%# Convert.ToDateTime(Eval("DataNascimento")).ToString("dd/MM/yyyy")%></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="true" HeaderStyle-HorizontalAlign="Center" HeaderText="Ações">
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Button ID="btn_view" class="btn btn-primary btn-md" Text="Visualizar" runat="server" CommandArgument='<%# Eval("CPF")%>' OnClick="btnVisualizar_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BackColor="#FFFFFF" />
                                <AlternatingRowStyle BackColor="#F0F0F0" />
                            </asp:GridView>
                        </p>
                    </section>
                </td>
            </tr>
        </table>

        <div id="modalEditExclui" style="display: none" class="ui-dialog">
            <table width="100%" style="text-align: initial">
                <tr>
                    <td class="row" width="100%">
                        <section aria-labelledby="gettingStartedTitle">
                            <h2 id="H1" runat="server">Cadastro de Clientes</h2>
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
                                                <asp:DropDownList runat="server" ID="ddlCity" class="dropdownlist cidade" AutoPostBack="True" Enabled="false">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <asp:Button ID="btnEditar" class="btn btn-primary btn-md beditar" Text="Editar" runat="server" OnClientClick="bloquearBotao('triggerEditar|btnEditar|btnDeletar');" />
                                                <asp:Button ID="triggerEditar" Style="display: none" runat="server" OnClick="btnEditar_Click" />
                                                <asp:Button ID="btnDeletar" class="btn btn-default btn-md" Text="Deletar" runat="server" OnClientClick="bloquearBotao('triggerDeletar|btnDeletar|btnEditar');" />
                                                <asp:Button ID="triggerDeletar" Style="display: none" runat="server" OnClick="btnDeletar_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </td>
                </tr>
            </table>
        </div>
        <div id="modalSalvarEdit" style="display: none" class="ui-dialog">
            <table width="100%" style="text-align: initial">
                <tr>
                    <td class="row" width="100%">
                        <section aria-labelledby="gettingStartedTitle">
                            <h2 id="H2" runat="server">Edição do Cliente</h2>
                            <p>
                                Todos os campos são obrigatórios.
                            </p>
                            <div id="Div1" runat="server">
                                <div class="bod">
                                    <div class="contai">
                                        <div class="registration-form">
                                            <div id="div2" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="nome" Text="Nome:" />
                                                <asp:TextBox runat="server" ID="nom" CssClass="textbox" />
                                            </div>

                                            <div id="div3" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="cpf" Text="CPF:" />
                                                <asp:TextBox runat="server" ID="cp" CssClass="textbox cpf" MaxLength="14" placeholder="000.000.000-00" />
                                            </div>

                                            <div id="div4" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="DataN" Text="Data de nascimento :" />
                                                <asp:TextBox runat="server" ID="Data" CssClass="textbox nascimento " MaxLength="10" placeholder="00/00/0000" />
                                            </div>

                                            <div id="div5" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="email" Text="E-mail:" />
                                                <asp:TextBox runat="server" ID="emai" CssClass="textbox" placeholder="cliente@cliente.com.br" />
                                            </div>

                                            <div id="div6" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="rua" Text="Rua:" />
                                                <asp:TextBox runat="server" ID="ru" CssClass="textbox" />
                                            </div>

                                            <div id="div7" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="numero" Text="Numero:" />
                                                <asp:TextBox runat="server" ID="numer" CssClass="textbox" />
                                            </div>

                                            <div id="div8" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="Bairro" Text="Bairro:" />
                                                <asp:TextBox runat="server" ID="Bairr" CssClass="textbox" />
                                            </div>

                                            <div id="div9" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="cep" Text="CEP:" />
                                                <asp:TextBox runat="server" ID="ce" CssClass="textbox cep" MaxLength="9" placeholder="00000-000" />
                                            </div>

                                            <div id="div10" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="ddlEstado" Text="UF:" />
                                                <asp:DropDownList runat="server" ID="ddlEstad" class="dropdownlist estado" AutoPostBack="True" OnSelectedIndexChanged="ddlEstad_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>

                                            <div id="div11" class="form-group" runat="server">
                                                <asp:Label runat="server" AssociatedControlID="ddlCity" Text="Cidade:" />
                                                <asp:DropDownList runat="server" ID="ddlCitys" class="dropdownlist cidade" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <asp:Button ID="Button1" class="btn btn-cad btn-md" Text="Salvar" runat="server" OnClientClick="bloquearBotao('Button2|Button1');" />
                                                <asp:Button ID="Button2" Style="display: none" runat="server" OnClick="btnSalvar_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </td>
                </tr>
            </table>
        </div>
    </main>
</asp:Content>
