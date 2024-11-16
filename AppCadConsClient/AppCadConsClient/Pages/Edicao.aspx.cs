using AppCadConsClient.Core.Domain;
using SPTC.Site;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppCadConsClient.Pages
{
    public partial class Edicao : System.Web.UI.Page
    {

        private EstadoCityRepository ControllerEstadoCity = new EstadoCityRepository();
        private ClienteRepository ControllerCliente = new ClienteRepository();
        private Util Util = new Util();
        public List<Cidades> Citys
        {
            get
            {
                List<Cidades> o = (List<Cidades>)this.ViewState["citys"];

                if (o == null || (List<Cidades>)o == null)
                    return null;
                else
                    return (List<Cidades>)o;
            }
            set
            {
                this.ViewState["citys"] = value;
            }
        }
        public Cliente CL
        {
            get
            {
                Cliente o = (Cliente)this.ViewState["cl"];

                if (o == null || (Cliente)o == null)
                    return null;
                else
                    return (Cliente)o;
            }
            set
            {
                this.ViewState["cl"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cliente cl = (Cliente)Session["clEdt"];
                CL = cl;
                (this.Page.Master as Principal).TituloPagina = "Pequena Aplicação de Cadastro e Consulta de Clientes";
                LoadEstadoCitys();
            }
        }
        private void LoadEstadoCitys()
        {
            #region DDL States

            List<Estados> contextUF = null;

            contextUF = ControllerEstadoCity.ListStates();
            if (contextUF.Count() > 0)
            {
                ddlEstado.DataTextField = "Estado";
                ddlEstado.DataValueField = "idUF";
                ddlEstado.DataSource = contextUF;
                ddlEstado.DataBind();

                ddlEstado.Items.Insert(0, "Selecione um Estado");
                ddlEstado.Items[0].Value = "0";
                ddlEstado.SelectedValue = "0";
            }
            #endregion DDL States

            #region DDL Citys

            Citys = ControllerEstadoCity.ListCitys();
            if (Citys.Count() > 0)
            {
                var cityEstado = Citys.Where(x => x.idUF == CL.IdUF).ToList();
                ddlCity.DataTextField = "Cidade";
                ddlCity.DataValueField = "IdCityUF";
                ddlCity.DataSource = cityEstado;
                ddlCity.DataBind();

                ddlCity.Items.Insert(0, "Selecione uma Cidade");
                ddlCity.Items[0].Value = "0";
                ddlCity.SelectedValue = "0";
            }
            #endregion DDL Citys

            nome.Text = CL.Nome;
            cpf.Text = CL.CPF;
            DataN.Text = CL.DataNascimento.ToString("dd/MM/yyyy");
            email.Text = CL.Email;
            rua.Text = CL.Rua;
            numero.Text = CL.Numero.ToString();
            Bairro.Text = CL.Bairro;
            cep.Text = CL.CEP;
            ddlEstado.SelectedValue = CL.IdUF.ToString();
            ddlCity.SelectedValue = CL.IdCity.ToString() + "|" + CL.IdUF.ToString();
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (nome.Text.Length <= 4)
            {
                Util.ShowMessage(this.Page, "Preencha o nome completo!");
            }
            else if (!ValidateCpf(cpf.Text.ToString().Replace(".", "").Replace("-", "").Trim()))
            {
                Util.ShowMessage(this.Page, "CPF invalido. Preencha o CPF novamente!");
            }
            else if (DataN.Text.ToString().Contains("/0000"))
            {
                Util.ShowMessage(this.Page, "Preecha a data de aniversário!");
            }
            else if (!email.Text.ToString().Contains("@"))
            {
                Util.ShowMessage(this.Page, "Preecha o email!");
            }
            else if (ddlEstado.SelectedValue == "0")
            {
                Util.ShowMessage(this.Page, "Selecione a cidade para limitar as opções de órgão!");
            }
            else if (ddlCity.SelectedValue == "0")
            {
                Util.ShowMessage(this.Page, "Selecione a cidade!");
            }
            else
            {
                if (ValidaCad(CL.IdCliente))
                {
                    if (Salvar())
                    {
                        if (CL.UltimaPesquisa!= null)
                        {
                        string retorno = "";
                        var lista = CL.UltimaPesquisa.Split('&');
                        // Transferindo para a próxima página
                        for (int n = 0; n < lista.Count(); n++)
                        {
                            retorno += lista[n].Split('=')[0] + "=" + Convert.ToBase64String(Encoding.UTF8.GetBytes(lista[n].Split('=')[1])).Replace("=","||");
                            if (lista.Count() - 1 > n )
                            {
                                retorno += "&";
                            }
                        }
                        Server.Transfer("Consulta.aspx?" + retorno);
                        }
                        else
                        {
                            Server.Transfer("Consulta.aspx");
                        }
                    }

                }
                else
                {
                    Util.ShowMessage(this.Page, "CPF já consta na base de dados");
                }

            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlEstado.SelectedValue, out int idEstado))
            {
                if (idEstado > 0)
                {
                    ddlCity.Enabled = true;
                    if (Citys.Count() > 0)
                    {
                        var cityEstado = Citys.Where(x => x.idUF == idEstado).ToList();
                        ddlCity.DataTextField = "Cidade";
                        ddlCity.DataValueField = "IdCityUF";
                        ddlCity.DataSource = cityEstado;
                        ddlCity.DataBind();

                        ddlCity.Items.Insert(0, "Selecione uma Cidade");
                        ddlCity.Items[0].Value = "0";
                        ddlCity.SelectedValue = "0";
                    }
                }
                else
                {
                    ddlCity.DataTextField = "Cidade";
                    ddlCity.DataValueField = "IdCityUF";
                    ddlCity.DataSource = Citys;
                    ddlCity.DataBind();

                    ddlCity.Items.Insert(0, "Selecione uma Cidade");
                    ddlCity.Items[0].Value = "0";
                    ddlCity.SelectedValue = "0";
                    ddlCity.Enabled = false;
                }
            }
            else
            {
                ddlCity.SelectedValue = "0";
                ddlCity.Enabled = false;
            }
        }
        private static bool ValidateCpf(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            bool allDigitsEqual = cpf.Distinct().Count() == 1;
            if (allDigitsEqual)
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digitoVerificador1 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpf[9].ToString()) != digitoVerificador1)
                return false;

            soma = 0;
            tempCpf += digitoVerificador1;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digitoVerificador2 = resto < 2 ? 0 : 11 - resto;

            if (int.Parse(cpf[10].ToString()) != digitoVerificador2)
                return false;

            return true;
        }
        private bool ValidaCad(int id)
        {
            return ControllerCliente.ObterClientePorId(id);
        }
        private bool Salvar()
        {
            Cliente cliente = new Cliente
            {
                IdCliente = CL.IdCliente,
                Nome = nome.Text.Trim(),
                DataNascimento = Convert.ToDateTime(DataN.Text),
                CPF = cpf.Text.ToString().Replace(".", "").Replace("-", "").Trim(),
                Email = email.Text.Trim(),
                Rua = rua.Text.Trim(),
                Numero = int.Parse(numero.Text.Trim()),
                Bairro = Bairro.Text.Trim(),
                IdCity = int.Parse(ddlCity.SelectedValue.Split('|')[0]),
                IdUF = int.Parse(ddlEstado.SelectedValue),
                CEP = cep.Text.Trim(),
            };

            if (!ControllerCliente.AtualizarCliente(cliente))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}