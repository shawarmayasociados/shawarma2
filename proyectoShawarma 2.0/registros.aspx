<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registros.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!-- style del navegador! solo el navegador lateral, no de la estructura de la página-->
    <link href="Content/nav.css" rel="stylesheet" />
    <link href="Content/font.css" rel="stylesheet" />
    <link href="Content/filternav.css" rel="stylesheet" />
    <link href="Content/tabla_registros.css" rel="stylesheet" />
    <title></title>
</head>
<body>

     <nav> <!-- Creamos un navegador-->
        <ul> <!-- añadimos una lista que contendra nuestras opciones de navegación-->

            <!-- agregamos nuestro elemento de lista (li) con un a href (para reenviar a algún lugar tras presionar) y una imagen-->
            <li><a href="desk.aspx"><img src="Content/img/resources/icons/database.png" alt=""/></a></li>
           <li><a href="graficos.aspx"><img src="Content/img/resources/icons/stats.png" alt=""/></a></li>
            <li><a href="registros.aspx"><img src="Content/img/resources/icons/view-2.png" alt=""/></a></li>
            <li><a href="logout.aspx"><img src="Content/img/resources/icons/exit.png" alt=""/></a></li>
        </ul>
    </nav>
    
    
    <form  runat="server" class="contenedor_simulaciones" >
   
        <h3>Registro actividad de usuarios</h3>
        <div id="contenedor-planilla">
        <asp:GridView runat="server" ID="cargadedatos" CssClass="planilla" AllowPaging="true" 
 HeaderStyle-CssClass="header" RowStyle-CssClass="rows"
        PageSize="9" OnPageIndexChanging="cargar">
        <PagerStyle CssClass="page" HorizontalAlign="Center" />

        </asp:GridView>

        </div>
        
         <asp:Label ID="errorlabel" runat="server" Text="" CssClass="error"></asp:Label>
    </form>
</body>
</html>
