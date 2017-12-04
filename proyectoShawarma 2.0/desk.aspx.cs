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
        if (Session["user"] != null)
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
    protected void logout(object sender, EventArgs e) //si el usuario no esta logueado te redirecciona en logout aspx que contiene el codigo para reenviar al inciio de sesión
    {

        Response.Redirect("~/logout.aspx");
    }
    //

    protected void llenarseleccion() //accion para llenar dropdown lisst
    {
        try { 
        string Connect = ConfigurationManager.ConnectionStrings["connectuserinfo"].ConnectionString; //declaramos estring de conexión
        SqlConnection con = new SqlConnection(Connect); //transformamos nuestro string en una conexión con sql
        // lista seleccón dropdown 1 que contendra los nombres de los bancos
        string Bancos = "select nombre from ignacio.banco"; //creamos una query para llamar al nombre de los bancos que mostraremos
        SqlCommand cmdBancos = new SqlCommand(Bancos, con); //creamos un nuevo comando que leera la base de datos, añadimos los 2 parametros "query" y "conexión"
        con.Open();
        SqlDataAdapter datosbanco = new SqlDataAdapter(cmdBancos);
        DataTable tablabanco = new DataTable(); //creamos una tabla que alamcenara los datos
        datosbanco.Fill(tablabanco); //llenamos tabla banco con los datos recibidos "datosbanco"
        DropDownList1.DataTextField = "nombre"; // data text - texto de datos = Nombre
        DropDownList1.DataValueField = "nombre"; // valor asignado nombre
        DropDownList1.DataSource = tablabanco; //le decimos que el origen de los datos de nuestro dropdown es tablabanco
        DropDownList1.DataBind(); //damos ejecución para cargar los datos 

        DropDownList1.Items.Insert(0, "Banco"); //añadimos por opción por defecto "Banco" que será un todos

        // lista seleccón dropdown 2
        //creamos una lista de string que contendra los valores que queremos asignar a los rangos
        string[] valores = new string[5];
        valores[0] = "500000";
        valores[1] = "1999999";
        valores[2] = "20000000";
        valores[3] = "4999999";
        valores[4] = "5000000";

        int i = 0; // creamos un contador para ir añadiendo la informacíón

        while (true) //cremos un while que permita añadir los datos de forma cronologíca
        {
            if (i <= 1)
            {    // el dropdown se mostrara así  VALOR1 - VALOR2
                //añadimos items de forma que cree rangos de VALORPAR - VALORIMPAR
                DropDownList2.Items.Add(valores[2 * i] + "-" + valores[2 * i + 1]);
            }

            else
            {   // dropdownlist final tomara el ultimo valor asignado y agregara una cadena de string
                DropDownList2.Items.Add(valores[4] + " " + "o" + " " + "Más");
                break;
            }
            i++;
        }

        DropDownList2.Items.Insert(0, "Monto"); //añadimos por opción por defecto "Monto" que será un todos

        // lista seleccón dropdown 3
        //creamos lista de cuotas
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
                //añadimos items de forma que cree rangos de VALORPAR - VALORIMPAR
                DropDownList3.Items.Add(cuotas[2 * b] + "-" + cuotas[2 * b + 1]);
            }

            else
            {

                break;
            }
            b++;
        }

        DropDownList3.Items.Insert(0, "Cuotas"); //añadimos por opción por defecto "Cuotas" que será un todos

        //creamos una lista de string donde ponemos intereses 
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
            {   //añadimos items de forma  Par - Impar
                DropDownList4.Items.Add(tasa[2 * c] + "-" + tasa[2 * c + 1]);
            }

            else
            {

                break;
            }
            c++;
        }
        //añadimos por opción por defecto "Tasa" que será un todos
        DropDownList4.Items.Insert(0, "Tasa");
        }
        catch
        {
            errorlabel.Text = "¡Oops algo salio mal!";
        }
    }



    protected void cargardatos() //creamos un procedimiento que solicitara datos y obtendra datos 
    {


        try
        {

            string Connect = ConfigurationManager.ConnectionStrings["connectuserinfo"].ConnectionString; //creamos string de conexion 
            SqlConnection con = new SqlConnection(Connect);
            con.Open();
            // INGRESAMOS UNA QUERY PARA SOLICITAR LOS DATOS QUE QUEREMOS EN TABLA
            SqlCommand cmd = new SqlCommand("select   convert(char,fecha,103) as Fecha,  TablaBanco.nombre as Banco,  monto as Monto, cuota as Cuota, tasa as Tasa, cae as MontoTotal, rut as Rut , tablaCliente.Nombre as Cliente from ignacio.simulacion inner join ignacio.banco as TablaBanco on ignacio.simulacion.IdBanco = TablaBanco.IdBanco inner join ignacio.cliente as TablaCliente on ignacio.simulacion.IdCliente = TablaCliente.IdCliente where Monto between @MontoMenor and @MontoMayor and Cuota between @CuotaMenor and @CuotaMayor and Tasa  between @TasaMenor and @TasaMayor and tablabanco.nombre = @banco order by CONVERT(DateTime, Fecha,101)  DESC", con);

            string banco = DropDownList1.SelectedItem.Text.ToString(); //transformamos la opcion seleccionada a texto

            if (banco == "Banco") //si la opcion es banco, la query cambia para que este recupere todos los bancos mas los parametros ingresados
            {
                cmd = new SqlCommand("select  convert(char,fecha,103) as Fecha, TablaBanco.nombre as Banco, monto as Monto, cuota as Cuota, tasa as Tasa, cae as MontoTotal , rut as Rut , tablaCliente.Nombre as Cliente from ignacio.simulacion inner join ignacio.banco as TablaBanco on ignacio.simulacion.IdBanco = TablaBanco.IdBanco inner join ignacio.cliente as TablaCliente on ignacio.simulacion.IdCliente = TablaCliente.IdCliente where Monto between @MontoMenor and @MontoMayor and Cuota between @CuotaMenor and @CuotaMayor and Tasa  between @TasaMenor and @TasaMayor order by CONVERT(DateTime, Fecha,101)  DESC", con);
            }

            // Selección Monto   

            //si el parametro seleccionado es identico al valor de la opcion [x] toma esos valores
            //donde DropDownList2.Items[x] donde x corresponde a la ubicaciòn de la selecciòn en el dropdown list
            //de esta forma podemos transformar su selecciòn para posteriormente hacer una consulta

            var montomayor = 0;
            var montomenor = 0;

            if (DropDownList2.SelectedItem.ToString() == DropDownList2.Items[1].ToString())
            {
                montomayor = 1999999;
                montomenor = 499999;
            }
            if (DropDownList2.SelectedItem.ToString() == DropDownList2.Items[2].ToString())
            {
                montomayor = 4999999;
                montomenor = 1999999;
            }
            if (DropDownList2.SelectedItem.ToString() == DropDownList2.Items[3].ToString())
            {
                montomayor = 999999999;
                montomenor = 4999999;
            }
            if (DropDownList2.SelectedItem.ToString() == DropDownList2.Items[0].ToString())
            {
                montomayor = 999999999;
                montomenor = 499999;
            }

            //si el parametro seleccionado es identico al valor de la opcion [x] toma esos valores
            //donde DropDownList3.Items[x] donde x corresponde a la ubicaciòn de la selecciòn en el dropdown list
            //de esta forma podemos transformar su selecciòn para posteriormente hacer una consulta

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

            // Selección de Tasa

            //si el parametro seleccionado es identico al valor de la opcion [x] toma esos valores
            //donde DropDownList4.Items[x] donde x corresponde a la ubicaciòn de la selecciòn en el dropdown list
            //de esta forma podemos transformar su selecciòn para posteriormente hacer una consulta

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

            //// agregamos la definiciòn de cada parametro

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


            string noexiste = Convert.ToString(cmd.ExecuteScalar()); //analizamos la respuesta del servidor 

            //errorlabel.Visible = false;
            
            if (noexiste != null)
            {

                errorlabel.Text = "existe";
                errorlabel.Visible = false;
            }
            else
            {

                errorlabel.Text = "no hay datos con los parametros ingresados";
                errorlabel.Visible = true;
            }

    }
        catch
        {
            cargadedatos.DataSource = null;
            cargadedatos.DataBind();
            errorlabel.Text = "No Existen Simulaciones Con Los Parametros Ingresados";
            errorlabel.Visible = true;
        }


    }

    protected void enviarregistro()
    {
        try
        {
            //creamos un nuevo registro tras hacer click en boton filtrar
            string SqlString = "Insert Into ignacio.registro (idusuario,banco, monto,cuota,tasa,fecha) Values (@sesion,@banco,@monto,@cuota,@tasa,getdate())"; //la query con los datos
            string Connect2 = ConfigurationManager.ConnectionStrings["connectuserinfo"].ConnectionString; //el string de conexiòn
            SqlConnection con2 = new SqlConnection(Connect2); //sql conecction para conectar sql y c#

            {
                using (SqlCommand cmduser = new SqlCommand(SqlString, con2)) //usamos la conexiòn vigente para hacer lo que viene ahora
                {

                    SqlCommand idusuario1 = new SqlCommand("select idusuario from ignacio.usuario where correo = @correo"); //obtenemos idusuario con el correo ingresado por nuestro usuairo en el login
                    idusuario1.CommandType = CommandType.Text; //comando de tipo texto
                    idusuario1.Connection = con2; //usamos el string de conexion sql 
                    con2.Open(); //abrimos la conexion
                    idusuario1.Parameters.AddWithValue("@correo", Session["user"].ToString()); //declaramos lo que significa @correo
                    string usuarioactual = idusuario1.ExecuteScalar().ToString(); //recuperamos el id del usuario y lo pasamos a string
                    string idusuario = idusuario1.ExecuteScalar().ToString(); //recuperamos el id del usuario y lo pasamos a string

                    string banco = DropDownList1.Text.ToString(); //recuperamos el id del usuario y lo pasamos a string

                    if (DropDownList1.SelectedItem.Text.ToString() == "Banco") //si el item seleccionado es igual a la opcion por defecto
                    {
                        banco = "Todos"; //el usuairo abrà seleccionado todos los del tip    
                    }

                    //mismo caso que el anterior
                    string monto = DropDownList2.Text.ToString();
                    if (DropDownList2.SelectedItem.Text.ToString() == "Monto")
                    {
                        monto = "Todos";
                    }
                    //mismo caso que el anterior
                    string cuota = DropDownList3.Text.ToString();
                    if (DropDownList3.SelectedItem.Text.ToString() == "Cuotas")
                    {
                        cuota = "Todos";
                    }
                    //mismo caso que el anterior
                    string tasa = DropDownList4.Text.ToString();
                    if (DropDownList4.SelectedItem.Text.ToString() == "Tasa")
                    {
                        tasa = "Todos";
                    }

                    cmduser.CommandType = CommandType.Text; //decimos que el comandos era de tipo texto
                    cmduser.Parameters.AddWithValue("@sesion", usuarioactual); //añadimos la definicio a los parametros
                    cmduser.Parameters.AddWithValue("@banco", banco);
                    cmduser.Parameters.AddWithValue("@monto", monto);
                    cmduser.Parameters.AddWithValue("@cuota", cuota);
                    cmduser.Parameters.AddWithValue("@tasa", tasa);

                    cmduser.ExecuteNonQuery(); //ejecutamos el comando

                }
            }
        }
        catch
        {
            cargadedatos.DataSource = null; //si no se llega a realizar lo anterior la grilla estarà vacia
            cargadedatos.DataBind(); //cargamos una grilla vacia para que no aparezca 
            errorlabel.Text = "Error en conexión de BD"; //definimos un mensaje de error.
            errorlabel.Visible = true; //lo mostramos
        }
    
    }
    //ejecutamos cargardatos cuando este evento se ejecuta tras presionar en un boton en aspx
    protected void filtrar_Click(object sender, EventArgs e)
    {
        
      
            cargardatos(); //ejecutamos la tabla con parametros 
            enviarregistro(); //ejecutamos el envio de informaciòn a registro
       
     }



    //con este definimos que sucedera al presionar los botoncitos de cabio de pagina
    protected void cargar(object sender, GridViewPageEventArgs e)
    {

        cargadedatos.PageIndex = e.NewPageIndex; //decimos que pageindex, el elemto para cambiar las paginas, siempre serà una nueva divisiòn del documento original.
        cargardatos(); //cargamos los datos
    }
}