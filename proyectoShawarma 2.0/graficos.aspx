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
                   
                    "backgroundColor": { "fill": "#FFFFFF" },
                    
                    "fontSize": 12,
                    "pieSliceTextStyle": { "color": "#FFFFFF" },
                    "sliceVisibilityThreshold": true,
                    "legend":
                    {
                        "position": "none",
                        "textStyle": {
                            "color": "#000000", "fontSize": 13
                        }

                    },
                    "tooltip":
                    {
                        "textStyle": { "color": "#000000" },
                        "showColorCode": true
                    },
                    "colors":
                    ["#2574A9"],
                //    "colors":
                //    ["#D9B611", "#F3C13A", "#F4D03F", "#F5D76E", "#F9690E",
                //        "#8F1D21", "#F44336", "#C91F37", "#D24D57",
                //        "#003171", "#4B77BE", "#19B5FE","#89C4F4",
                //        "#006442", "#407A52", "#6B9362", "#26C281",
                //        "#049372", "#2ABB9B", "#F5D76E",
                //        "#F1A9A0", "#336E7B", "#59ABE3",
                //        "#013243", "#DCC6E0", "#16A085", "#F7CA18",
                //        "#F2784B", "#913D88","#26A69A"],
                //    chartArea: {
                //        left: 10, top: 10, bottom:10, width: "100%", height: "100%"
                //    }
                   chartArea: {
                       width: "60%", height: "90%", left: "35%", top:" 1%"
                    }
                };
            // Set chart options
          

            // Instantiate and draw our chart, passing in some options.
                    var barchart = new google.visualization.BarChart(document.getElementById('chart_div'));
            barchart.draw(data, options);
        }
    </script>
    <!-- style del navegador! solo el navegador lateral, no de la estructura de la página-->

   
  
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
  
    <form id="form1" runat="server" >

        <div>
            <asp:Label id="contador" text="" runat="server" />
        </div>

        <div id="barcont" >
        <h3>Simulaciones realizadas por Banco</h3>
        
        <div id="chart_div"></div>
        </div>    

    </form>
    
    
</body>
  
</html>
