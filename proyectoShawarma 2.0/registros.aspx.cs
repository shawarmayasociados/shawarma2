using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {
            cargardatos();
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void logout(object sender,EventArgs e)
    {
        
        Response.Redirect("~/logout.aspx");


    }





    protected void cargardatos()
    {


        try
        {
            string Connect = ConfigurationManager.ConnectionStrings["connectuserinfo"].ConnectionString;
            SqlConnection con = new SqlConnection(Connect);

            //tablaCliente.Nombre as Cliente ,TablaBanco.nombre as Banco, monto as Monto, cuota as Cuota, tasa as Tasa, cae as MontoTotal, convert(char,fecha,103) as Fecha from ignacio.simulacion inner join ignacio.banco as TablaBanco on ignacio.simulacion.IdBanco = TablaBanco.IdBanco inner join ignacio.cliente as TablaCliente on ignacio.simulacion.IdCliente = TablaCliente.IdCliente where banco = @banco

            con.Open();
            // INGRESAMOS UNA QUERY PARA SOLICITAR LOS DATOS QUE QUEREMOS EN TABLA
            SqlCommand cmd = new SqlCommand("select  convert(char,fecha,103) as Fecha , banco, monto, cuota, tasa, tablaUsuario.Nombre as Usuario from ignacio.registro inner join ignacio.usuario as tablaUsuario on ignacio.registro.IdUsuario = tablaUsuario.IdUSuario ORDER BY CONVERT(datetime, fecha, 101) desc", con);




            ////

            // TRANSFORMAMOS NUESTRO COMANDO EN UNO PARA RECUPERAR TEXTO 
            cmd.CommandType = CommandType.Text;

            //ADAPTAMOS LOS DATOS
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //CREAMOS UN CONJUNTO DE DATOS
            DataSet ds = new DataSet();
            //LLENAMOS UN "DS" con los datos recuperados del sqldataadapter y lo nombramos datos
            da.Fill(ds, "DATOS");
            //traspasamos los datos a nuestra grilla llamada cargadedatos
            cargadedatos.DataSource = ds.Tables["DATOS"]; ;
            //confirmamos la transferencia total de los datos mediante databind
            cargadedatos.DataBind();



             string noexiste = Convert.ToString(cmd.ExecuteScalar()); //analizamos la respuesta del servidor 

            //errorlabel.Visible = false;
            
            if (noexiste != null)
            {

               
                errorlabel.Visible = false;
            }
            else
            {

                errorlabel.Text = "Oops algo salio mal";
                errorlabel.Visible = true;
            }

    }
        catch
        {
            cargadedatos.DataSource = null;
            cargadedatos.DataBind();
            errorlabel.Text = "Creo que no he encontrado datos por aquí";
            errorlabel.Visible = true;
        }


    }


    protected void cargar(object sender, GridViewPageEventArgs e)
    {

        cargadedatos.PageIndex = e.NewPageIndex;
        cargardatos();
    }
}