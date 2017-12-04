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
            cantidadsimulaciones();
            bancosactuales();
            usuariosactuales();
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
            strDatos = strDatos + "'" + dr[0] + "'" + "," + dr[1];
            strDatos = strDatos + "],";
        }
        strDatos = strDatos + "]";
        return strDatos;
    }


    protected void cantidadsimulaciones()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectuserinfo"].ToString());
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.CommandText = "SELECT count(nombre) 'nombre' from ignacio.simulacion inner join ignacio.banco On ignacio.simulacion.IdBanco = ignacio.banco.Idbanco";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;


            contadorsimulaciones.Text = cmd.ExecuteScalar().ToString();

        }
        catch
        {
            contadorsimulaciones.Text = "Error, recargue";
        }



    }

    protected void bancosactuales()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectuserinfo"].ToString());
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.CommandText = "SELECT count(nombre) from ignacio.banco";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;


            banco.Text = cmd.ExecuteScalar().ToString();
        }
        catch
        {
            banco.Text = "Error, recargue";
        }
    }
    protected void usuariosactuales()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectuserinfo"].ToString());
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.CommandText = "SELECT count(nombre) from ignacio.usuario";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;


            usuarios.Text = cmd.ExecuteScalar().ToString();
        }
        catch
        {
            usuarios.Text = "Error, recargue";
        }
    }



    protected int rango1()
     {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectuserinfo"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT COUNT(CASE when monto between 0 and 1999999 then '0-20' ELSE NULL END) as [0 - 20] from simulacion";
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        con.Open();
        

       string nparse = cmd.ExecuteScalar().ToString();
        int numero = Convert.ToInt32(nparse);
          

        return numero;
    }
    protected int rango2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectuserinfo"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select COUNT(CASE when monto between 2000000 and 4999999 then '20-40' ELSE NULL END) as [20 - 40] from simulacion";

        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        con.Open();


        string nparse = cmd.ExecuteScalar().ToString();
        int numero = Convert.ToInt32(nparse);


        return numero;
    }
    protected int rango3()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectuserinfo"].ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select COUNT(CASE when  monto between 5000000 and 99999999 then '40-60' else null end) as [40 - 60] from simulacion";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();


            string nparse = cmd.ExecuteScalar().ToString();
            int numero = Convert.ToInt32(nparse);


            return numero;
        }
        catch
        {
            int numero = 0;


            return numero;
        }
    }

}