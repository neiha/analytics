colors = ["#4CAF50", "#2196F3", "#9c27b0", "#ff9800", "#F44336"];
var chart;
$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "GetResumenActivity",
        success: function (result) {
            if (!result.IsSuccess)
                return;
            $(".resumenReporteData.userCount").html(result.Data.UserCount);
            $(".resumenReporteData.activeUsersCount").html(result.Data.ActiveUsersCount);
            $(".resumenReporteData.newUsersCount").html(result.Data.NewUsersCount);
            $(".resumenReporteData.returningUsersCount").html(result.Data.ReturningUsersCount);
            $(".resumenReporteData.sessionCount").html(result.Data.SessionCount);
            $(".resumenReporteData.userSessionRate").html(result.Data.UserSessionRate);
        }
    });

    $('#demo').daterangepicker({
        "ranges": {
            "Hoy": [
                moment(), moment()
            ],
            "Ayer": [
                moment().subtract(1, 'days'), moment().subtract(1, 'days')
            ],
            "Últimos 7 días": [
               moment().subtract(6, 'days'), moment()
            ],
            "Últimos 30 días": [
                moment().subtract(29, 'days'), moment()
            ],
            "Últimos 90 días": [
                moment().subtract(89, 'days'), moment()
            ],
            "Últimos 365 días": [
                moment().subtract(200, 'days'), moment()
            ]
        },
        "locale": {
            "format": "DD/MM/YYYY",
            "separator": " - ",
            "applyLabel": "Aplicar",
            "cancelLabel": "Cancelar",
            "fromLabel": "De",
            "toLabel": "A",
            "customRangeLabel": "Personalizado",
            "daysOfWeek": [
                "Do",
                "Lu",
                "Ma",
                "Mi",
                "Ju",
                "Vi",
                "Sa"
            ],
            "monthNames": [
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Septiembre",
                "Octubre",
                "Noviembre",
                "Diciembre"
            ],
            "firstDay": 1
        },
        "alwaysShowCalendars": true,
        "startDate": moment(),
        "endDate": moment().subtract(7, 'days')
    }, function (start, end, label) {
        console.log("New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')");
    });
    $('#tblexample')
   .addClass('nowrap')
   .dataTable({
       responsive: true,
       columnDefs: [
         { targets: [-1, -3], className: 'dt-body-right' }
       ]
   });

    $(".reportFilter").change(updateChart);
    $("#TipoGraficaReporte").change(function () {
        chart.transform($("#TipoGraficaReporte").val());
    });

    updateChart();

});
var ReporteConfig = [
    { "Tipo": "UsuariosNuevos", "Filtro": "Dia", "url": "JsonListNewUsersByDay", "xLabel":"Fecha", "yLabel":"Usuarios", "xType":"timeseries", "DataKey":"DateKey"},
    {"Tipo":"UsuariosNuevos", "Filtro":"Mes", "url":""},
    {"Tipo":"UsuariosNuevos", "Filtro":"Hora", "url":""},
    { "Tipo": "UsuariosNuevos", "Filtro": "DiaSemana", "url": "JsonListNewUsersByDayOfWeek", "xLabel": "Dia", "yLabel": "Usuarios", "xType": "categories", "DataKey": "StringKey" },
    {"Tipo":"UsuariosNuevos", "Filtro":"DiaSemanaHora", "url":""},
    {"Tipo":"UsuariosActivos", "Filtro":"Dia", "url":""},
    {"Tipo":"UsuariosActivos", "Filtro":"Mes", "url":""},
    {"Tipo":"UsuariosActivos", "Filtro":"Hora", "url":""},
    {"Tipo":"UsuariosActivos", "Filtro":"DiaSemana", "url":""},
    {"Tipo":"UsuariosActivos", "Filtro":"DiaSemanaHora", "url":""},
    { "Tipo": "Sesiones", "Filtro": "Dia", "url": "JsonListSessionsByDay", "xLabel": "Fecha", "yLabel": "Usuarios", "xType": "timeseries", "DataKey": "DateKey" },
    {"Tipo":"Sesiones", "Filtro":"Mes", "url":""},
    {"Tipo":"Sesiones", "Filtro":"Hora", "url":""},
    {"Tipo":"Sesiones", "Filtro":"DiaSemana", "url":""},
    {"Tipo":"Sesiones", "Filtro":"DiaSemanaHora", "url":""},
    {"Tipo":"Reinstalaciones", "Filtro":"Dia", "url":""},
    {"Tipo":"Reinstalaciones", "Filtro":"Mes", "url":""},
    {"Tipo":"Reinstalaciones", "Filtro":"Hora", "url":""},
    {"Tipo":"Reinstalaciones", "Filtro":"DiaSemana", "url":""},
    {"Tipo":"Reinstalaciones", "Filtro":"DiaSemanaHora", "url":""},
];

function getConfigByFiltros(tipo, filtro)
{
    for (var i = 0; i < ReporteConfig.length; i++)
    {
        config=ReporteConfig[i];
        if (config.Tipo == tipo && config.Filtro == filtro)
            return config;
    }
    return undefined;
}
function updateChart()
{
    var config = getConfigByFiltros($("#TipoReporte").val(), $("#FiltroReporte").val());
    console.log(config);
    $.ajax({
        type: "POST",
        url: config.url,
        success: function (result) {
            /*if (!result.IsSuccess)
                return;
                */
            columns = getColumnData(result,config.DataKey);
            generateChart(config, columns);
        }
    });

}

function getColumnData(data, dataKey)
{
    console.log(data);
    console.log(data.length);
    var x = new Array();
    var values = new Array();
    for (var i = 0; i < data.length; i++)
    {
        if(dataKey=="DateKey")
            x.push(new Date(parseInt(data[i].DateKey.substr(6))));
        else
            x.push(data[i].StringKey);
        values.push(data[i].Number);
    }
    return { "x": x, "values": values };
}
function generateChart(config,columnsToBind)
{
    console.log(columnsToBind.x);
    console.log(columnsToBind.values);
    columnsToBind.x.unshift("x");
    columnsToBind.values.unshift("UsuariosActivos");
    chart = c3.generate({
        bindto: '#area-chart',
        type: $("#TipoGraficaReporte").val(),
        data: {
            x: 'x',
            columns: [columnsToBind.x, columnsToBind.values]
        },
        axis: {
            x: {
                label: config.xLabel,
                type: config.xType
               
                
            },
            y: {
                label: config.yLabel
            }
        },
        color: { pattern: colors }

    });
}
