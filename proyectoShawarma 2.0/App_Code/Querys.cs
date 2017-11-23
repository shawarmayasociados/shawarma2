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

/// <summary>
/// Summary description for Querys
/// </summary>
public class Querys
{
    protected void cargardatosfiltros()
    {

        string Connect = ConfigurationManager.ConnectionStrings["connectuserinfo"].ConnectionString;
        SqlConnection con = new SqlConnection(Connect);
       
    }
}