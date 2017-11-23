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


public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] != null )
        {
            if (!IsPostBack)
            { //limpiampos el dropdown para evitar la clonacion de elementos
                DropDownList1.Items.Clear();
                DropDownList2.Items.Clear();
                DropDownList3.Items.Clear();
                DropDownList4.Items.Clear();
                llenarseleccion();
                cargardatos();
            }
        
           
        }
        else
        {
            Response.Redirect("~/login.aspx");
        }


    }
    protected void logout(object sender, EventArgs e) //si el usuario no esta logueado te redirecciona en logout aspx
    {

        Response.Redirect("~/logout.aspx");
    }
    //

    protected void llenarseleccion() //accion para llenar dropdown lisst
    {
        string Connect = ConfigurationManager.ConnectionStrings["connectuserinfo"].ConnectionString;
        SqlConnection con = new SqlConnection(Connect);
        // lista seleccón dropdown 1
        string Bancos = "select nombre from ignacio.banco";
        SqlCommand cmdBancos = new SqlCommand(Bancos, con);
        con.Open();
        SqlDataAdapter datosbanco = new SqlDataAdapter(cmdBancos);
        DataTable tablabanco = new DataTable();
        datosbanco.Fill(tablabanco);
        DropDownList1.DataTextField = "nombre";
        DropDownList1.DataValueField = "nombre";
        DropDownList1.DataSource = tablabanco;
        DropDownList1.DataBind();

        DropDownList1.Items.Insert(0, "Banco");
        // lista seleccón dropdown 2
        string[] valores = new string[5];
        valores[0] = "500000";
        valores[1] = "1999999";
        valores[2] = "20000000";
        valores[3] = "4999999";
        valores[4] = "5000000";

        int i = 0;

        while (true)
        {
            if (i <= 1)
            {
                DropDownList2.Items.Add(valores[2 * i] + "-" + valores[2 * i + 1]);
            }

            else
            {
                DropDownList2.Items.Add(valores[4] + " " + "o" + " " + "Más");
                break;
            }
            i++;
        }
        DropDownList2.Items.Insert(0, "Monto");

        // lista seleccón dropdown 3
        string[] cuotas = new string[10];
        cuotas[0] = "12";
        cuotas[1] = "19";
        cuotas[2] = "20";
        cuotas[3] = "29";
        cuotas[4] = "30";
        cuotas[5] = "39";
        cuotas[6] = "40";
        cuotas[7] = "49";
        cuotas[8] = "50";
        cuotas[9] = "60";

        int b = 0;

        while (true)
        {
            if (b <= 4)
            {
                DropDownList3.Items.Add(cuotas[2 * b] + "-" + cuotas[2 * b + 1]);
            }

            else
            {

                break;
            }
            b++;
        }

        DropDownList3.Items.Insert(0, "Cuotas");

        string[] tasa = new string[6];
        tasa[0] = "1.0";
        tasa[1] = "1.4";
        tasa[2] = "1.5";
        tasa[3] = "1.9";
        tasa[4] = "2.0";
        tasa[5] = "2.5";

        int c = 0;

        while (true)
        {
            if (c <= 2)
            {
                DropDownList4.Items.Add(tasa[2 * c] + "-" + tasa[2 * c + 1]);
            }

            else
            {

                break;
            }
            c++;
        }
        DropDownList4.Items.Insert(0, "Tasa");

    }



    protected void cargardatos()
    {



        try
        {

            string Connect = ConfigurationManager.ConnectionStrings["connectuserinfo"].ConnectionString;
            SqlConnection con = new SqlConnection(Connect);

            con.Open();
                // INGRESAMOS UNA QUERY PARA SOLICITAR LOS DATOS QUE QUEREMOS EN TABLA
            SqlCommand cmd = new SqlCommand("select tablaCliente.Nombre as Cliente , rut as Rut , TablaBanco.nombre as Banco, monto as Monto, cuota as Cuota, tasa as Tasa, cae as MontoTotal, convert(char,fecha,103) as Fecha from ignacio.simulacion inner join ignacio.banco as TablaBanco on ignacio.simulacion.IdBanco = TablaBanco.IdBanco inner join ignacio.cliente as TablaCliente on ignacio.simulacion.IdCliente = TablaCliente.IdCliente order  by CONVERT(DateTime, Fecha,101)  DESC", con);

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

         

        }

        catch

        {
            //si no se recuperan los registros, el errorlabel en aspx se mostrara con un catch

            errorlabel.Text = "no existen registros";
            errorlabel.Visible = true;

        }
    }

       //ejecutamos cargardatos cuando este evento se ejecuta tras presionar en un boton en aspx
    protected void filtrar_Click(object sender, EventArgs e)
    {

        try
        {
            string Connect = ConfigurationManager.ConnectionStrings["connectuserinfo"].ConnectionString;
            SqlConnection con = new SqlConnection(Connect);

            //tablaCliente.Nombre as Cliente ,TablaBanco.nombre as Banco, monto as Monto, cuota as Cuota, tasa as Tasa, cae as MontoTotal, convert(char,fecha,103) as Fecha from ignacio.simulacion inner join ignacio.banco as TablaBanco on ignacio.simulacion.IdBanco = TablaBanco.IdBanco inner join ignacio.cliente as TablaCliente on ignacio.simulacion.IdCliente = TablaCliente.IdCliente where banco = @banco

            con.Open();
            // INGRESAMOS UNA QUERY PARA SOLICITAR LOS DATOS QUE QUEREMOS EN TABLA
            SqlCommand cmd = new SqlCommand("select tablaCliente.Nombre as Cliente, rut as Rut, TablaBanco.nombre as Banco, monto as Monto, cuota as Cuota, tasa as Tasa, cae as MontoTotal, convert(char,fecha,103) as Fecha from ignacio.simulacion inner join ignacio.banco as TablaBanco on ignacio.simulacion.IdBanco = TablaBanco.IdBanco inner join ignacio.cliente as TablaCliente on ignacio.simulacion.IdCliente = TablaCliente.IdCliente where Monto between @MontoMenor and @MontoMayor and Cuota between @CuotaMenor and @CuotaMayor and Tasa  between @TasaMenor and @TasaMayor and tablabanco.nombre = @banco order by CONVERT(DateTime, Fecha,101)  DESC", con);




            ///////
            string banco = DropDownList1.SelectedItem.Text.ToString();

            if (banco == "Banco")
            {
                cmd = new SqlCommand("select tablaCliente.Nombre as Cliente, rut as Rut, TablaBanco.nombre as Banco, monto as Monto, cuota as Cuota, tasa as Tasa, cae as MontoTotal, convert(char,fecha,103) as Fecha from ignacio.simulacion inner join ignacio.banco as TablaBanco on ignacio.simulacion.IdBanco = TablaBanco.IdBanco inner join ignacio.cliente as TablaCliente on ignacio.simulacion.IdCliente = TablaCliente.IdCliente where Monto between @MontoMenor and @MontoMayor and Cuota between @CuotaMenor and @CuotaMayor and Tasa  between @TasaMenor and @TasaMayor order by CONVERT(DateTime, Fecha,101)  DESC", con);
            }

            //        //// Selección Monto
            var montomayor = 0;
            var montomenor = 0;
            if (DropDownList2.SelectedItem.ToString() == DropDownList2.Items[1].ToString())
            {
                montomayor = 1999999;
                montomenor = 499999;
            }
            if (DropDownList2.SelectedItem.ToString() == DropDownList2.Items[2].ToString())
            {
                montomayor = 5000000;
                montomenor = 1999999;
            }
            if (DropDownList2.SelectedItem.ToString() == DropDownList2.Items[3].ToString())
            {
                montomayor = 999999999;
                montomenor = 1999999;
            }
            if (DropDownList2.SelectedItem.ToString() == DropDownList2.Items[0].ToString())
            {
                montomayor = 999999999;
                montomenor = 499999;
            }
            //        //// Selección cuotas
            var cuotamayor = 0;
            var cuotamenor = 0;
            if (DropDownList3.SelectedItem.ToString() == DropDownList3.Items[1].ToString())
            {
                cuotamayor = 20;
                cuotamenor = 11;
            }
            if (DropDownList3.SelectedItem.ToString() == DropDownList3.Items[2].ToString())
            {
                cuotamayor = 30;
                cuotamenor = 19;
            }
            if (DropDownList3.SelectedItem.ToString() == DropDownList3.Items[3].ToString())
            {
                cuotamayor = 40;
                cuotamenor = 29;
            }
            if (DropDownList3.SelectedItem.ToString() == DropDownList3.Items[4].ToString())
            {
                cuotamayor = 50;
                cuotamenor = 39;
            }
            if (DropDownList3.SelectedItem.ToString() == DropDownList3.Items[5].ToString())
            {
                cuotamayor = 60;
                cuotamenor = 49;
            }
            if (DropDownList3.SelectedItem.ToString() == DropDownList3.Items[0].ToString())
            {
                cuotamayor = 60;
                cuotamenor = 1;
            }
            //        //// Selección de Tasa
            var tasamayor = 0.2;
            var tasamenor = 0.2;

            if (DropDownList4.SelectedItem.ToString() == DropDownList4.Items[1].ToString())
            {
                tasamayor = 1.4;
                tasamenor = 1.0;
            }
            if (DropDownList4.SelectedItem.ToString() == DropDownList4.Items[2].ToString())
            {
                tasamayor = 1.9;
                tasamenor = 1.5;
            }
            if (DropDownList4.SelectedItem.ToString() == DropDownList4.Items[3].ToString())
            {
                tasamayor = 2.5;
                tasamenor = 2.0;
            }

            if (DropDownList4.SelectedItem.ToString() == DropDownList4.Items[0].ToString())
            {
                tasamayor = 3.0;
                tasamenor = 0.0;
            }

            ////

            cmd.Parameters.AddWithValue("@MontoMayor", montomayor);
            cmd.Parameters.AddWithValue("@MontoMenor", montomenor);
            cmd.Parameters.AddWithValue("@CuotaMayor", cuotamayor);
            cmd.Parameters.AddWithValue("@CuotaMenor", cuotamenor);
            cmd.Parameters.AddWithValue("@TasaMayor", tasamayor);
            cmd.Parameters.AddWithValue("@TasaMenor", tasamenor);
            cmd.Parameters.AddWithValue("@banco", banco);

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

            string output = cmd.ExecuteScalar().ToString();
            if (output != "1")
            {
                errorlabel.Visible = false;   

            }
            
        }
        catch
        {
            errorlabel.Text = "No Existen Simulaciones Con Los Parametros ingresados";
            errorlabel.Visible = true;
        }
    }
    //con este definimos que sucedera al presionar los botoncitos de cabio de pagina
    protected void cargar(object sender, GridViewPageEventArgs e)
    {
        cargadedatos.PageIndex = e.NewPageIndex;
        cargardatos();
    }
}