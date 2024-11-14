using AppCadConsClient.Core.Domain;
using SPTC.Site;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppCadConsClient.Pages
{
    public partial class Consulta : System.Web.UI.Page
    {
        private ClienteRepository ControllerCliente = new ClienteRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                (this.Page.Master as Principal).TituloPagina = "Pequena Aplicação de Cadastro e Consulta de Clientes";

            }
        }
        protected void grvResultados_RowCommand(object sender, EventArgs e)
        {

        }
        protected void btnVisualizar_Click(object sender, EventArgs e)
        {

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
    }
}