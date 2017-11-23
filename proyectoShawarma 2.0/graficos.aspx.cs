using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;



public partial class Default3 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null)
        {

        }
        else
        {
            Response.Redirect("~/login.aspx");
        }
    }
    protected void logout(object sender, EventArgs e)
    {

        Response.Redirect("~/logout.aspx");
    }


    //var sql = new SqlCommand("SELECT nombre 'bancos', count(nombre) 'cantidad' from ignacio.simulacion inner join ignacio.banco On ignacio.simulacion.IdBanco = ignacio.banco.Idbanco group by nombre", con);

    protected string obtenerDatos()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectuserinfo"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT nombre 'bancos', count(nombre) 'cantidad' from ignacio.simulacion inner join ignacio.banco On ignacio.simulacion.IdBanco = ignacio.banco.Idbanco group by nombre";
        cmd.CommandType = CommandType.Text;

        cmd.Connection = con;
        con.Open();
        DataTable Datos = new DataTable();
        Datos.Load(cmd.ExecuteReader());
       

        //DataTable Datos = new DataTable();
        //Datos.Columns.Add(new DataColumn("Task", typeof(string)));
        //Datos.Columns.Add(new DataColumn("Hours per day", typeof(string)));

        //Datos.Rows.Add(new Object[] { "Work", 11 });
        //Datos.Rows.Add(new Object[] { "Eat", 2 });
        //Datos.Rows.Add(new Object[] { "Commute", 2 });
        //Datos.Rows.Add(new Object[] { "Watch tv", 2 });
        //Datos.Rows.Add(new Object[] { "Sleep", 7 });

        string strDatos;

        strDatos = "[['Bancos','Cantidad de simulaciones por banco'],";

        foreach (DataRow dr in Datos.Rows)
        {
            strDatos = strDatos + "[";
            strDatos = strDatos + "'"+dr[0]+"'"+","+dr[1];
            strDatos = strDatos + "],";
        }
        strDatos = strDatos + "]";
        return strDatos;
    }


    //protected string cantidadsimulaciones(object sender, EventArgs e )
    //{
    //    try
    //    {
    //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectuserinfo"].ToString());
    //        SqlCommand cmd = new SqlCommand();
    //        con.Open();
    //        cmd.CommandText = "SELECT count(nombre) 'cantidad' from ignacio.simulacion inner join ignacio.banco On ignacio.simulacion.IdBanco = ignacio.banco.Idbanco";
    //        cmd.CommandType = CommandType.Text;
    //        cmd.Connection = con;
    //        con.Open();
            

    //        contador.Text = cmd.ExecuteReader.ToString();
    //        contador.Visible = true;

    //    }
    //    catch
    //    {
    //        contador.Text = "0";
    //    }

    //}
}