using AppCadConsClient.Core.Domain;
using Newtonsoft.Json;
using SPTC.Site;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppCadConsClient.Pages
{
    public partial class Consulta : System.Web.UI.Page
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
                (this.Page.Master as Principal).TituloPagina = "Pequena Aplicação de Cadastro e Consulta de Clientes";
                LoadEstadoCitys();
                grvResultados.DataSource = null;
                grvResultados.DataBind();

                string nome = Request.QueryString["n"];
                string DataD = Request.QueryString["d"];
                string DataA = Request.QueryString["a"];
                string CPF = Request.QueryString["c"];

                if (nome != null)
                {
                    txtNome.Text = Encoding.UTF8.GetString(Convert.FromBase64String(nome.Replace("||", "=")));
                }
                if (DataD != null)
                {
                    txtDateDe.Text = Encoding.UTF8.GetString(Convert.FromBase64String(DataD.Replace("||", "=")));
                }
                if (DataA != null)
                {
                    txtDateAte.Text = Encoding.UTF8.GetString(Convert.FromBase64String(DataA.Replace("||", "=")));
                }
                if (CPF != null)
                {
                    txtCPF.Text = Encoding.UTF8.GetString(Convert.FromBase64String(CPF.Replace("||", "=")));
                }
                if (nome != null || DataD != null || DataA != null || CPF != null)
                {
                    btnConsultar_Click(null, null);

                    Util.ShowMessage(this.Page, "Cliente atualizado com sucesso");
                }
            }
        }
        protected void grvResultados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cpff = e.CommandArgument.ToString();
            if (cpff.Length >= 10)
            {
                btnEditar.Text = "Editar";
                Cliente cliente = ConsultarCPF(cpff);

                nome.Text = cliente.Nome;
                cpf.Text = cliente.CPF;
                DataN.Text = cliente.DataNascimento.ToString("dd/MM/yyyy");
                email.Text = cliente.Email;
                rua.Text = cliente.Rua;
                numero.Text = cliente.Numero.ToString();
                Bairro.Text = cliente.Bairro;
                cep.Text = cliente.CEP;
                ddlEstado.SelectedValue = cliente.IdUF.ToString();
                ddlEstado_SelectedIndexChanged(null, null);
                ddlCity.SelectedValue = cliente.IdCity.ToString() + "|" + cliente.IdUF.ToString();
                CL = cliente;
                nome.Enabled = false;
                cpf.Enabled = false;
                DataN.Enabled = false;
                email.Enabled = false;
                rua.Enabled = false;
                numero.Enabled = false;
                Bairro.Enabled = false;
                cep.Enabled = false;
                ddlEstado.Enabled = false;
                ddlCity.Enabled = false;
                btnDeletar.Enabled = true;

                StringBuilder sbScript = new StringBuilder();
                sbScript.Append("$(document).ready(function (){");
                sbScript.Append("modalEdit();");
                sbScript.Append("});");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), sbScript.ToString(), true);

            }
        }
        protected void triggerLimpar_Click(object sender, EventArgs e)
        {
            grvResultados.DataSource = null;
            grvResultados.DataBind();
        }
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string nome = "";
            string datade = "";
            string dataate = "";
            string CPF = "";
            if (txtNome.Text.Length > 0)
            {
                nome = txtNome.Text.Trim();
            }
            if (txtDateDe.Text.Length > 0)
            {
                datade = Convert.ToDateTime(txtDateDe.Text.Trim()).ToString("yyyy-MM-dd");
            }
            if (txtDateAte.Text.Length > 0)
            {
                dataate = Convert.ToDateTime(txtDateAte.Text.Trim()).ToString("yyyy-MM-dd");
            }
            if (txtCPF.Text.Length > 0)
            {
                CPF = txtCPF.Text.ToString().Replace(".", "").Replace("-", "").Trim();
            }

            List<Cliente> clientes = ControllerCliente.ObterClientes(nome, datade, dataate, CPF);
            grvResultados.DataSource = clientes;
            grvResultados.DataBind();

        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {

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
        protected void ddlEstad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlEstado.SelectedValue, out int idEstado))
            {
                if (idEstado > 0)
                {
                    ddlCity.Enabled = true;
                    if (Citys.Count() > 0)
                    {
                        var cityEstado = Citys.Where(x => x.idUF == idEstado).ToList();
                        ddlCitys.DataTextField = "Cidade";
                        ddlCitys.DataValueField = "IdCityUF";
                        ddlCitys.DataSource = cityEstado;
                        ddlCitys.DataBind();

                    }
                }
                else
                {
                    var cityEstado = Citys.Where(x => x.idUF == idEstado).ToList();
                    ddlCitys.DataTextField = "Cidade";
                    ddlCitys.DataValueField = "IdCityUF";
                    ddlCitys.DataSource = cityEstado;
                    ddlCitys.DataBind();

                    ddlCitys.Items.Insert(0, "Selecione uma Cidade");
                    ddlCitys.Items[0].Value = "0";
                    ddlCitys.SelectedValue = "0";

                    ddlCitys.DataTextField = "Cidade";
                    ddlCitys.DataValueField = "IdCityUF";
                    ddlCitys.DataSource = Citys;
                    ddlCitys.DataBind();

                    ddlCitys.Items.Insert(0, "Selecione uma Cidade");
                    ddlCitys.Items[0].Value = "0";
                    ddlCitys.SelectedValue = "0";
                    ddlCitys.Enabled = false;
                }
            }
            else
            {
                ddlCitys.SelectedValue = "0";
                ddlCitys.Enabled = false;
            }
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            string nome = "";
            string DataD = "";
            string DataA = "";
            string CPF = "";
            string UltimaPesquisa = "";

            if (txtNome.Text != "")
            {
                nome = "n=" + txtNome.Text;
            }
            if (txtDateDe.Text != "")
            {
                DataD = "d=" + txtDateDe.Text;
            }
            if (txtDateAte.Text != "")
            {
                DataA = "a=" + txtDateAte.Text;
            }
            if (txtCPF.Text != "")
            {
                CPF = "c=" + txtCPF.Text;
            }
            if (nome != null)
            {
                UltimaPesquisa = nome;
            }
            if (DataD != "")
            {
                if (UltimaPesquisa.Length > 2)
                {
                    UltimaPesquisa += "&" + DataD;
                }
                else
                {
                    UltimaPesquisa = DataD;
                }
            }
            if (DataA != "")
            {
                if (UltimaPesquisa.Length > 4)
                {
                    UltimaPesquisa += "&" + DataA;
                }
                else
                {
                    UltimaPesquisa = DataA;
                }
            }
            if (CPF != "")
            {
                if (UltimaPesquisa.Length > 6)
                {
                    UltimaPesquisa += "&" + CPF;
                }
                else
                {
                    UltimaPesquisa = CPF;
                }
            }

            if (UltimaPesquisa.Length > 0)
            {
                CL.UltimaPesquisa = UltimaPesquisa;
            }

            Session["clEdt"] = CL;

            // Transferindo para a próxima página
            Server.Transfer("Edicao.aspx");
        }
        protected void btnDeletar_Click(object sender, EventArgs e)
        {

            if (Deletar())
            {
                Util.ShowMessage(this.Page, "Cliente deletado com sucesso");

                btnConsultar_Click(null, null);
            }


        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            if (Salvar())
            {
                Util.ShowMessage(this.Page, "Cliente cadastrador com sucesso");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirecionar", "window.location.href = '../#';", true);

            }

            Util.ShowMessage(this.Page, "CPF já consta na base de dados");


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
        private bool ValidaCad(string cpf)
        {
            if (ControllerCliente.ObterClientePorCPF(cpf))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool Deletar()
        {
            if (!ControllerCliente.DeletarCliente(CL.IdCliente))
            {
                return false;
            }
            else
            {
                CL = null;
                return true;
            }
        }
        private bool Salvar()
        {
            Cliente cliente = new Cliente
            {
                Nome = nom.Text.Trim(),
                DataNascimento = Convert.ToDateTime(Data.Text),
                CPF = cp.Text.ToString().Replace(".", "").Replace("-", "").Trim(),
                Email = emai.Text.Trim(),
                Rua = ru.Text.Trim(),
                Numero = int.Parse(numer.Text.Trim()),
                Bairro = Bairr.Text.Trim(),
                IdCity = int.Parse(ddlCitys.SelectedValue.Split('|')[0]),
                IdUF = int.Parse(ddlEstad.SelectedValue),
                CEP = ce.Text.Trim(),
            };
            //else
            //{

            //    if (nome.Text.Length <= 4)
            //    {
            //        Util.ShowMessage(this.Page, "Preencha o nome completo!");
            //    }
            //    else if (!ValidateCpf(cpf.Text.ToString().Replace(".", "").Replace("-", "").Trim()))
            //    {
            //        Util.ShowMessage(this.Page, "CPF invalido. Preencha o CPF novamente!");
            //    }
            //    else if (DataN.Text.ToString().Contains("/0000"))
            //    {
            //        Util.ShowMessage(this.Page, "Preecha a data de aniversário!");
            //    }
            //    else if (!email.Text.ToString().Contains("@"))
            //    {
            //        Util.ShowMessage(this.Page, "Preecha o email!");
            //    }
            //    else if (ddlEstado.SelectedValue == "0")
            //    {
            //        Util.ShowMessage(this.Page, "Selecione a cidade para limitar as opções de órgão!");
            //    }
            //    else if (ddlCity.SelectedValue == "0")
            //    {
            //        Util.ShowMessage(this.Page, "Selecione a cidade!");
            //    }
            //    else
            //    {
            //        if (ValidaCad(cpf.Text.ToString().Replace(".", "").Replace("-", "").Trim()))
            //        {
            //            if (Editar())
            //            {
            //                Util.ShowMessage(this.Page, "Cliente editado com sucesso");
            //            }
            //        }
            //        else
            //        {
            //            Util.ShowMessage(this.Page, "CPF já consta na base de dados");
            //        }
            //    }
            //}


            if (!ControllerCliente.InserirCliente(cliente))
            {
                return false;
            }
            else
            {
                return true;
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

                ddlEstad.DataTextField = "Estado";
                ddlEstad.DataValueField = "idUF";
                ddlEstad.DataSource = contextUF;
                ddlEstad.DataBind();

                ddlEstad.Items.Insert(0, "Selecione um Estado");
                ddlEstad.Items[0].Value = "0";
                ddlEstad.SelectedValue = "0";
            }
            #endregion DDL States

            #region DDL Citys


            Citys = ControllerEstadoCity.ListCitys();
            if (Citys.Count() > 0)
            {
                ddlCity.DataTextField = "Cidade";
                ddlCity.DataValueField = "IdCityUF";
                ddlCity.DataSource = Citys;
                ddlCity.DataBind();

                ddlCity.Items.Insert(0, "Selecione uma Cidade");
                ddlCity.Items[0].Value = "0";
                ddlCity.SelectedValue = "0";

                ddlCitys.DataTextField = "Cidade";
                ddlCitys.DataValueField = "IdCityUF";
                ddlCitys.DataSource = Citys;
                ddlCitys.DataBind();

                ddlCitys.Items.Insert(0, "Selecione uma Cidade");
                ddlCitys.Items[0].Value = "0";
                ddlCitys.SelectedValue = "0";
            }

            #endregion DDL Citys
        }
        private Cliente ConsultarCPF(string cpf)
        {
            return ControllerCliente.ClientePorCPF(cpf);
        }
    }
}