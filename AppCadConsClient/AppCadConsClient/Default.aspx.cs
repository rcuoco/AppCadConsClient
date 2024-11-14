using SPTC.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppCadConsClient
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                (this.Page.Master as Principal).TituloPagina = "Pequena Aplicação de Cadastro e Consulta de Clientes";
            }
        }
    }
}