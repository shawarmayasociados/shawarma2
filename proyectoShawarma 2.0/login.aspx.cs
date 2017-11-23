using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    //una vez cargue la página nos aseguramos
    protected void Page_Load(object sender, EventArgs e)
    {
        //si se logra iniciar sesión y almacenar los datos en user correctamente se reenvia a desk.aspx
        if (Session["user"] != null)
        {
            Response.Redirect("~/desk.aspx");
        }

    }

    //evento trás hacer click en el boton para enviar datos
    protected void buttlog_Click(object sender, EventArgs e)
    {
        //creamos variable para iniciar sesión con los datos suministrados en web.config
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectuserinfo"].ToString());
        //abrimos una conexión previamente
        try
        {
            con.Open();
            //creamos una query para enviar a SQL solicitando todos los datos de usuario y comparando con los que haya suministado el usuario.
            string query = "SELECT count(*) FROM ignacio.Usuario WHERE Correo = '" + textuser.Text + "' and Password = '" + textpass.Text + "' ";
            //creamos un sql commando con los datos de query y conexión.
            SqlCommand cmd = new SqlCommand(query, con);
            //recuperamos el dato de respuesta del servidor execute scalar intepreta nuestro valor en un numero binario 0 o 1, 1 es que la query solicitada no es nula
            string output = cmd.ExecuteScalar().ToString();

            //si nuestro valor devuelto es 1 ejecutar
            if (output == "1")
            {
                //Creamos la sesión con el correo ingresado por el usuario y redireccionamos a desk.aspx
                Session["user"] = textuser.Text;
                Response.Redirect("~/desk.aspx");
            }
            else
            {
                //si el resultado es diferente a 1 añadimos texto a label1 en html e inmediatamente aparecera el mensaje añadido en pantalla.
                Label1.Text = "Error en usuario o contraseña";
            }
        }
        catch
        {
            Label1.Text = "Ocurrio un error inesperado, Intentelo nuevamente";
        }
        
       
    }
}