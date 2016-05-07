$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "GetResumenActivity",
        success: function (result) {
            console
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

});

