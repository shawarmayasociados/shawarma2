<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Comparación de Creditos Online</title>
    <!-- El style de login-->
    <link rel="stylesheet" href="Content/style.css"/>
    <link href="Content/font.css" rel="stylesheet" />
   

</head>
<body>
    <!-- Creamos secc8ón que contendra nuestro formualrio-->
    <section id="form-cont">
        <!-- Creamos formualrio que contrenda nuestro login-->
        <form  runat= "server">
            <!-- H3 Y su respectivo contenedor-->
           <div id="h3-cont">
           <h3>Bienvenido</h3> 
           </div> 
            <!-- Label que obtendra texto "ERROR DE USUARIO O CONTRASEÑA" desde c#-->
            <asp:Label ID="Label1" runat="server" Text="" CssClass="texterror"></asp:Label>
            <!-- Creamos caja contenedora de los campos nombre de usuario y contraseña-->
            <div id="box-cont">
            <!-- Campo textbox para recibir Nombre de usuario-->
            <asp:TextBox ID="textuser" runat="server" CssClass="input" placeholder="Nombre de usuario"></asp:TextBox>   
            <!-- Campo textbox para recibir Nombre de password-->
            <asp:TextBox ID="textpass" runat="server" CssClass="input" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
            </div>
            <!-- Boton para iniciar sesión que nos reenviara por c# a logout.aspx que contiene el codigo para eliminar sesión-->
            <asp:Button ID="buttlog" CssClass="button" runat="server" Text="Ingresar" OnClick="buttlog_Click" />
        </form>
    </section>
    
</body>
</html>
