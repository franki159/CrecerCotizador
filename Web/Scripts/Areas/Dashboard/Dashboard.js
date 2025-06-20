//window.chartColors = {
//red: 'rgb(237, 85, 101)',
//orange: 'rgb(248, 172, 89)',
//yellow: 'rgb(248, 172, 89)',
//green: 'rgb(26, 179, 148)',
//blue: 'rgb(28, 132, 198)',
//aqua: 'rgb(35, 198, 200)',
//grey: 'rgb(209, 218, 222)'
//};

//window.chartColors = {
//    bg_4: 'rgb(237, 85, 101)',
//    orange: 'rgb(248, 172, 89)',
//    yellow: 'rgb(248, 172, 89)',
//    bg_2: 'rgb(26, 179, 148)',
//    bg_1: 'rgb(28, 132, 198)',
//    bg_3: 'rgb(35, 198, 200)',
//    bg_5: 'rgb(209, 218, 222)'
//};


this.pagenum = 0;
this.pagesize_t = 10;
this.lstAutoJobs;
this.lstAutoJobsDet;
var chartUrl = '../../../Dashboard/getChartBar';
const aMonths = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosot", "Septiembre", "Octubre", "Noviembre", "Diciembre"
];

window.onload = function () {

    var strDate = new Date();
    $('#lMonth').text(aMonths[strDate.getMonth()]);

    $(".fecha").datepicker({
        language: "es", locale: "es", dateFormat: 'dd/mm/yy', changeYear: true
    });


    

    //Event close popup
    $('.btnCorredor').on('click', function () {    
        SolicitudXCorredor();        
    });

    $('.btnProducto').on('click', function () {
        SolicitudXProducto();        
    });

    $('.btnUbigeo').on('click', function () {
        SolicitudXUbigeo();        
    });

    $('.btnRangoPrima').on('click', function () {        
        SolicitudRangoPrima();   
    });
    $('.btnSumaAsegurada').on('click', function () {
        SolicitudXSumaAsegurada();
    });

    $('.fecha').on('changeDate', function (ev) {

        $(this).datepicker('hide');
    });

    SolicitudXCorredor();
    SolicitudXProducto();
    SolicitudXUbigeo();
    SolicitudXSumaAsegurada();
    SolicitudRangoPrima();    

    loadAutoJobs();    
}

function SolicitudXSumaAsegurada() {
    
    var fechaSumIni = $('.fechaSumIni').val() == undefined ? null : setFecha($('.fechaSumIni').val());
    var fechaSumFin = $('.fechaSumFin').val() == undefined ? null : setFecha($('.fechaSumFin').val());

    var optionsChart5 = {
        urlChart: chartUrl,
        paramsChart: { nChartData: 3, fechaIni: fechaSumIni, fechafin: fechaSumFin, nTipoReporte: 7 },
        typChart: 2,
        isVertical: true,
        viewAxis: { x: true, y: false ,labelX: 'Tipo Moneda', labelY: 'Póliza'  },
        container: 'chart_5',
        titleDisplay: false,
        titleChart: 'Rango de suma asegurada',
        titlePosition: 'bottom',
        legendPosition: 'bottom',
        ledendDisplay: true
    }

    renderChar(optionsChart5);
}

function SolicitudRangoPrima() {

    var fechaPrimaIni = $('.fechaPrimaIni').val() == undefined ? null : setFecha($('.fechaPrimaIni').val());
    var fechaPrimaFin = $('.fechaPrimaFin').val() == undefined ? null : setFecha($('.fechaPrimaFin').val());

    var optionsChart4 = {
        urlChart: chartUrl,
        paramsChart: { nChartData: 3, fechaIni: fechaPrimaIni, fechafin: fechaPrimaFin, nTipoReporte: 3 },
        typChart: 2,
        isVertical: true,
        viewAxis: { x: true, y: false, labelX:'Tipo Moneda' ,labelY:'Póliza' },
        container: 'chart_4',
        titleDisplay: false,
        titleChart: 'Rango de prima mensual',
        titlePosition: 'bottom',
        legendPosition: 'bottom',
        ledendDisplay: true
    }

    renderChar(optionsChart4);
}


function SolicitudXCorredor()
{
    var fechacanalInicio = $('.fechacanalInicio').val() == undefined ? null : setFecha($('.fechacanalInicio').val());
    var fechacanalFin = $('.fechacanalFin').val() == undefined ? null : setFecha($('.fechacanalFin').val());

    var optionsChart2 = {
        urlChart: chartUrl,
        paramsChart: { nChartData: 4, fechaIni: fechacanalInicio, fechafin: fechacanalFin, nTipoReporte: 1 },
        typChart: 4,
        container: 'chart_2',
        titleDisplay: false,
        titleChart: 'Solicitudes aprobadas',
        titlePosition: 'bottom',
        legendPosition: 'right',
        ledendDisplay: true
    }

    renderChar(optionsChart2);
}

function SolicitudXUbigeo()
{
    var fechaUbigIni = $('.fechaUbigIni').val() == undefined ? null : setFecha($('.fechaUbigIni').val());
    var fechaUbigFin = $('.fechaUbigFin').val() == undefined ? null : setFecha($('.fechaUbigFin').val());

    var optionsChart3 = {
        urlChart: chartUrl,
        paramsChart: { nChartData: 4, fechaIni: fechaUbigIni, fechafin: fechaUbigFin, nTipoReporte: 2 },
        typChart: 4,
        container: 'chart_3',
        titleDisplay: false,
        titleChart: 'Solicitudes por Ubigeo',
        titlePosition: 'bottom',
        legendPosition: 'right',
        ledendDisplay: true
    }

    renderChar(optionsChart3);
}

function SolicitudXProducto()
{
    var fechaTipIni = $('.fechaTipIni').val() == undefined ? null : setFecha($('.fechaTipIni').val());
    var fechaTipFin = $('.fechaTipFin').val() == undefined ? null : setFecha($('.fechaTipFin').val());

    var optionsChart6 = {
        urlChart: chartUrl,
        paramsChart: { nChartData: 5, fechaIni: fechaTipIni, fechafin: fechaTipFin, nTipoReporte: 6 },
        typChart: 5,
        //typChart:5,
        container: 'chart_6',
        titleDisplay: false,
        titleChart: 'Solicitud por Producto',
        titlePosition: '',
        legendPosition: 'bottom',
        ledendDisplay: true
    }

    renderChar(optionsChart6);
}

function statsNotify() {
    $.ajax({
        url: '../../../Dashboard/getChartBar',
        contentType: 'application/json',
        data: { nChartData: 6},
        success: function (data) {
            var dataChart = data.cb;            
            var i = 0;
            $.each(dataChart, function (key, val) {
                $('#statNotify').append('<li class="list-group-item ' + ((i == 0) ? 'fist-item' : '') + '"><span class="float-right">' + val.nValSerie[1] + '</span><span class="label label-' + val.nValSerie[0] + '">' + val.nIdSerie + '</span> ' + val.sSerie + '</li>');
                i++;
            });            
        },
        error: function () {
            console.log("No se ha podido obtener la información.");
        }
    });
}

function loadAutoJobs() {
    var objParameters = {
        PNVOUCHER: $('.txtDescription').val(),
        PSREFERENCE: $('.txtDescription').val(),
        PNPAGENUM: pagenum,
        PNPAGESIZE: pagesize_t
    };

    $.ajax({
        url: '../../../Dashboard/getAutoJobs',
        contentType: 'application/json',
        data: objParameters,
        success: function (data) {

            var JSonHeader = {
                Column1: 'ID',
                Column2: '',
                Column3: 'Voucher',
                Column4: 'Referencia',
                Column5: 'Ramo SBS',
                Column6: 'Producto',
                Column7: 'Póliza',
                Column8: 'Siniestro',
                Column9: 'Per&iacuteodo Contable',
                Column10: 'D&eacutebito soles',
                Column11: 'Cr&eacutedito soles',
                Column12: 'Balance Soles',
                Column13: 'D&eacutebito d&oacutelares',
                Column14: 'Cr&eacutedito d&oacutelares',
                Column15: 'Balance d&oacutelares',
                Column16: 'Estado',
                Column17: 'Observaciones',
            }

            var propsCol = {
                "Columns": [
                    {
                        "col": "1",
                        "name": "ID",
                        "props": [
                           {
                               "visible": false
                           }
                        ]
                    },
                    {
                        "col": "2",
                        "name": "Modulo",
                        "props": [
                           {
                               "visible": true,
                               "label": true,
                               "isState": false,
                               "multidata": true,
                               "cssClass": "l_module",
                           }
                        ]
                    },
                   {
                       "col": "3",
                       "name": "Voucher",
                       "props": [
                          {
                              "visible": true,
                              "linkStyle": true
                          }
                       ]
                   },
                   {
                       "col": "4",
                       "name": "Referencia",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "5",
                       "name": "Ramo SBS",
                       "props": [
                          {
                              "visible": true,
                          }
                       ]
                   },
                   {
                       "col": "6",
                       "name": "Producto",
                       "props": [
                          {
                              "visible": true,
                          }
                       ]
                   },
                   {
                       "col": "7",
                       "name": "Póliza",
                       "props": [
                          {
                              "visible": false,
                          }
                       ]
                   },
                   {
                       "col": "8",
                       "name": "Siniestro",
                       "props": [
                          {
                              "visible": false,
                          }
                       ]
                   },
                   {
                       "col": "9",
                       "name": "Per&iacuteodo Contable",
                       "props": [
                          {
                              "visible": true,
                          }
                       ]
                   },
                   {
                       "col": "10",
                       "name": "D&eacutebito soles",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "11",
                       "name": "Cr&eacutedito soles",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "12",
                       "name": "Balance Soles",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "13",
                       "name": "D&eacutebito d&oacutelares",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "14",
                       "name": "Cr&eacutedito d&oacutelares",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "15",
                       "name": "Balance d&oacutelares",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "16",
                       "name": "Estado",
                       "props": [
                          {
                              "visible": true,
                              "label": true,
                              "cssClass": "l_state_job",
                              "isState": true
                          }
                       ]
                   },
                   {
                       "col": "17",
                       "name": "Observaciones",
                       "props": [
                          {
                              "visible": true,
                              "showErrosObs": true
                          }
                       ]
                   }
                ]
            };

            var initTable = {
                dataTable: data.eJobs,
                tableId: '.data-autoSend',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: false,
                delButton: false,
                colSequence: false,
                pagesize: pagesize_t,
                callerId: 'loadAutoJobs',
                instanceId: 'lstAutoJobs',
                hasParams: false,
                valDisable: 0,
                disabledByCol: 'SSTATE_DESC'
            };

            //Render grid
            lstAutoJobs = new renderTable(initTable);

            //Show Details
            $('.link_view').on('click', function () {
                idValue = $(this).attr('id').replace('lnk-', '');
                sVoucher = $(this).text();
                idVoucher = sVoucher;
                showDetailsAcc(idValue, sVoucher);
            });

            //Show errors
            $('.show_errors').on('click', function () {
                idValue = $(this).attr('id').replace('lnk-', '');
                sVoucher = $('#lnk-' + idValue).text();
                showErrors(idValue, sVoucher);
            });


        },
        error: function () {
            console.log("No se ha podido obtener la información");
        }
    })
}
