<%@ Page Language="C#" AutoEventWireup="true" CodeFile="graficos.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <!-- Cargamos jquery y chart.js desde servidores externos más rapidos que localhost-->
 
    <link href="Content/nav.css" rel="stylesheet" />
   <link href="Content/graficos.css" rel="stylesheet" />
    <!-- Fuentes-->
    <link href="Content/font.css" rel="stylesheet" />
     <title>gráficos</title>
      
     <!--Load the AJAX API-->
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">

        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
        

        // Callback that creates and populates a data table,
        // instantiates the pie chart, passes in the data and
        // draws it.
        function drawChart() {

            // Create the data table.
            var data = google.visualization.arrayToDataTable(<%=obtenerDatos()%>);


            var options =
                {
                   
                    "backgroundColor": { "fill": "#FFFFFF" }, //le damos un color al fondo
                    
                    "fontSize": 14, //tamñao de letra 14
                    "pieSliceTextStyle": { "color": "#FFFFFF" }, //ocultamos el texto poniendo en blanco su contenido para que desaparezca con el fondo
                    
                    "legend": //leyenda
                    {
                        "position": "none", //lo ocultamos
                        

                    },
                    "tooltip": //el efecto
                    {
                        "textStyle": { "color": "#000000" },
                        "showColorCode": true
                    },
                    "colors": //color del grafico
                    ["#e74c3c"],
               
                    chartArea: { //css para el grafico
                        widht: "90%", height: "90%", left: "34%"
                    }
                };
            
            // creamos una variable con los datos de la misma y la dibujamos en el div de html
                    var barchart = new google.visualization.BarChart(document.getElementById('chart_div'));
            barchart.draw(data, options);
        }
    </script>
    <!-- style del navegador! solo el navegador lateral, no de la estructura de la página-->
    <script>

        // Load the Visualization API and the corechart package.
        google.charts.load('current', { 'packages': ['corechart'] });

        // Set a callback to run when the Google Visualization API is loaded.
        google.charts.setOnLoadCallback(drawChart);

        // Callback that creates and populates a data table,
        // instantiates the pie chart, passes in the data and
        // draws it.
        function drawChart() {

            // Create the data table.
            
         
            var data = google.visualization.arrayToDataTable([
                ['Rango', 'Solicitudes'],
                ['0 - 499.999', <%=rango1()%>],
                ['2.000.000 - 4.999.999', <%=rango2()%>],
                ['5.000.000 o mas', <%=rango3()%>]

            ]);
            // Set chart options
            var options = {
                
                
                
                
                "legend": {
                    "position": "bottom",
                    "textStyle":
                    {
                        "color": "#000000",
                        "fontSize": 14
                    }
                },
                "backgroundColor": { "fill": "#FFFFFF" }, //le damos un color al fondo

                "fontSize": 17.161388653683318, //tamñao de letra 14
                "pieSliceTextStyle": { "color": "#FFFFFF" }, //ocultamos el texto poniendo en blanco su contenido para que desaparezca con el fondo

                "legend": //leyenda
                {
                    "position": "none"

                },
                "tooltip": //el efecto
                {
                    "textStyle": { "color": "#000000" },
                    "showColorCode": true
                },
                

                chartArea: { //css para el grafico
                    top: "15%", width: "80%", height: "80%"
                }
                
            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.PieChart(document.getElementById('chart_div-2'));
            chart.draw(data, options);
        }
    </script>
   
  
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
  
    <form id="form1"  runat="server" >

     

        

        <div id="contenedor-contadores">
         <div id="contenedor-simulaciones">
            <asp:Label id="contadorsimulaciones" CssClass="numeros" runat="server" />
            <h4>Simulaciones</h4>
        </div>
        <div id="contador-bancos">
            <asp:Label id="banco" CssClass="numeros" Text="" runat="server" />
            <h4>Bancos</h4>
            
        </div>
        <div id="contador-usuarios">
            <asp:Label id="usuarios" CssClass="numeros" Text="" runat="server" />
            <h4>Usuarios</h4>
             <h4>registrados</h4>
            
        </div>
        </div>
        <div id="barcont" >
        <h3>Simulaciones realizadas por Banco</h3>
        
        <div id="chart_div"></div>
        </div>    
        
        <div id="barcont-2" >
        <h3>Rango de monto de prestamos solicitados</h3>
        
        <div id="chart_div-2"></div>
        </div>    

    </form>
    
    
</body>
  
</html>
