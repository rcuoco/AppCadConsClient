using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace SPTC.Site
{
    public partial class Principal : MasterPage
    {
        //private Controller = new Controller();
        //private CustomUsers currentUser = null;
        public bool ShowAlertPanel
        {
            get
            {
                bool b = true;
                if (Session["ShowAlertPanel"] is bool)
                {
                    b = bool.Parse(Session["ShowAlertPanel"].ToString());
                }
                return b;
            }
            set => Session["ShowAlertPanel"] = value;
        }
        public string AlertTitle
        {
            get
            {
                string b = "";
                if (Session["AlertTitle"] is string)
                {
                    b = Session["AlertTitle"].ToString();
                }
                return b;
            }
            set => Session["AlertTitle"] = value;
        }
        public DateTime LastTimeUserHidAlert
        {
            get
            {
                DateTime b = DateTime.MinValue;
                if (Session["LastTimeUserHidAlert"] is DateTime)
                {
                    b = Convert.ToDateTime(Session["LastTimeUserHidAlert"].ToString());
                }
                return b;
            }
            set => Session["LastTimeUserHidAlert"] = value;
        }

        public string TituloPagina
        {
            get => this.lblTituloPagina.Text;
            set => this.lblTituloPagina.Text = value;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();

            menu_top.Visible = false;
            if (Request.Url.ToString().Contains("Consulta"))
            {
                HideControls();
            }
            else if (Request.Url.ToString().Contains("Cadastro"))
            {
                HideControls();
            }

        }

        public void HideControls()
        {
            menu_top.Visible = true;
        }

    }
}