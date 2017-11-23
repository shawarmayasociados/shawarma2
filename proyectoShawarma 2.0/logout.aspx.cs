using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Validamos que exista una sesión que cerrar
        if (Session["user"] != null)
        {
            Session.Remove("user");//removemos la sesión y reenviamos al login
            Response.Redirect("~/login.aspx");
        }
        else //Si no existe sesión reenviamos al login para evitar cualquier amenaza
        {
            Response.Redirect("~/login.aspx");
        }
        
    }
    
}