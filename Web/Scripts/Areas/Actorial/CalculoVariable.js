//Opciones grid Cabecera
this.pagenum = 0;
this.pagesize_t = 200;
this.lstDataInputMeler;
this.lstDataVariableCab;
this.lstDataDetails;
this.idRowSelected = 0;
var rowtotal = 0;
var nTotal = 0;
var idMeler = 0;
var DFECINIREG = '';
var DFECFINREG = '';
var DFECINICIE = '';
var DFECFINCIE = '';

$(document).ready(function () {

    loadListaVariable();

    $('.daterangeRegistro').daterangepicker({
        opens: 'left',
        locale: {
            format: 'DD/MMM/YYYY'
        }

    }, function (start, end, label) {
        DFECINIREG = start.format('DD/MM/YYYY');
        DFECFINREG = end.format('DD/MM/YYYY')
    });

    $('.daterangeCierre').daterangepicker({
        opens: 'left',
        locale: {
            format: 'DD/MMM/YYYY'
        }

    }, function (start, end, label) {
        DFECINICIE = start.format('DD/MM/YYYY');
        DFECFINCIE = end.format('DD/MM/YYYY')
    });

    $('.select-number-pagination').change(function () {
        pagesize = $(this).val();
        loadHeaderSpn();
    });

    $('#btnNuevo').on('click', function () {

        $('.content-pill').hide();
        $('#nav-icb').removeClass('c-active');
        $($('#nav-icg').data('target')).fadeIn();
        $('#nav-icg').addClass('c-active');

        $('.data-archivos-input-meler').empty();
    });

    $('#btnBuscarInput').on('click', function () {
        loadInputMeler();
    });

    $('#btnCancel').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('.btnClosePopup').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('#btnGuardar').on('click', function () {
        
        var itemsSelected = lstDataDetails.getAllChecked();

        if (itemsSelected != 0) {

            alertify.dialog('confirm').set({
                transition: 'zoom',
                title: 'COTIZADOR',
                labels: { ok: 'Ok', cancel: 'Cancelar' },
                message: '¿Está seguro de generar el proceso?',
                onok: function () {
                    MarcarEnvio();
                },
                oncancel: function () {
                }
            }).show();

        } else {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar un item para poder procesar el registro.', title: 'COTIZADOR' }).show();
        }
    });

    $('#btnVolver').on('click', function () {
        $('.content-pill').hide();
        $('#nav-icg').removeClass('c-active');
        $($('#nav-icb').data('target')).fadeIn();
        $('#nav-icb').addClass('c-active');
    });
});

function MarcarEnvio() {

    $("#overlay").fadeIn(200);

    var itemsSelected = lstDataDetails.getAllChecked();

    var objParametros = {
        idLote: idMeler,
        lstSelectedItems: itemsSelected
    }

    //Marco los registros que seran enviados
    $.ajax({
        type: 'POST',
        url: '../../../Actorial/GuardarVariableAfiliado',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(objParametros),
        success: function (response) {

            $("#overlay").fadeOut(300);

            if (response.result === false) {

                if (response.entity.length > 0) {
                   
                    var listaValidacion = "No se puede realizar la operación.<br />La lista de Nro. de Operaciones superan el máximo de cotización.<br />";
                    for (var i = 0; i < response.entity.length; i++) {
                        listaValidacion = listaValidacion + response.entity[i].NROOPERACION + ", ";
                    }
                    
                    listaValidacion = listaValidacion.substr(0, listaValidacion.length - 1);

                    $('#SValue').html(listaValidacion);
                    $("#BValue").addClass("colorRojo");
                    $("#BValue").css("display", "flex");
                } else {
                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Hubo un inconveniente al guardar el registro.', title: 'COTIZADOR' }).show();
                }
            }
            else {
                alertify.dialog('alert').set({ transition: 'zoom', message: 'Se guardo el registro correctamente.', title: 'COTIZADOR' }).show();

                $('.content-pill').hide();
                $('#nav-icg').removeClass('c-active');
                $($('#nav-icb').data('target')).fadeIn();
                $('#nav-icb').addClass('c-active');

                loadListaVariable();
                $('.popup').fadeOut(500);
            }
        },
        error: function () {
            $("#overlay").fadeOut(300);
            $('.popup').fadeOut(500);
            console.log('%cError: La Carga de Asientos no se realizó.', 'color:red');
        }
    });
}

function loadListaVariable() {

    $("#overlay").fadeIn(300);

    var objParameters = {
        P_NPAGENUM: pagenum,
        P_NPAGESIZE: pagesize_t,
        P_SRANGEREG_INI: setFecha(DFECINIREG),
        P_SRANGEREG_FIN: setFecha(DFECFINREG)
    };

    $.ajax({
        url: '../../../Actorial/GetListaVariableCab',
        contentType: 'application/json',
        data: objParameters,
        success: function (data) {

            var JSonHeader = {
                Column1: 'NID',
                Column2: 'SARCH',
                Column3: 'NROREGISTRO',
                Column4: 'ESTADO',
                Column5: 'NESTADO',
                Column6: 'FECHAREG',
                Column7: 'DESCARGA',
                Column8: 'ROWNUMBER',
                Column9: 'ROWTOTAL'
            }

            var propsCol = {
                "Columns": [
                    {
                        "col": "1",
                        "name": "ID",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "2",
                        "name": "Archivo",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "3",
                        "name": "Cant. Operación",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "4",
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
                        "col": "5",
                        "name": "Nestado",
                        "props": [
                            {
                                "visible": false
                            }
                        ]
                    },
                    {
                        "col": "6",
                        "name": "Fecha Registro",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "7",
                        "name": "Descarga",
                        "props": [
                            {
                                "visible": true,
                                "linkStyle": true
                            }
                        ]
                    }
                ]
            };

            var initTable = {
                dataTable: data.entity,
                tableId: '.data-archivosunat',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: false,
                delButton: false,
                colSequence: false,
                pagesize: pagesize_t,
                valDisable: 0,
                callerId: 'loadListaVariable',
                instanceId: 'lstDataVariableCab'
            }

            lstDataVariableCab = new renderTable(initTable)

            $('.link_view').on('click', function () {
                var sname_file = $(this).attr('id').replace('lnk-', '');
                console.log("consola: " + sname_file);
                var cadena = $(this).text();

                if (cadena == "Descarga Excel") {
                    var objParametros = {
                        P_IDLOTE: sname_file
                    };
                    window.location.href = '../../../Report/ReporteVariableDet?' + $.param(objParametros);
                    //window.location.href = '../../../Report/ReporteVariableMor?' + $.param(objParametros);
                }
            });

            $("#overlay").fadeOut(300);
        },
        error: function () {
            $("#overlay").fadeOut(300);
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

function loadInputMeler() {

    $("#overlay").fadeIn(300);

    var objParameters = {
        P_NPAGENUM: pagenum,
        P_NPAGESIZE: pagesize_t,
        P_SRANGEREG_INI: setFecha(DFECINIREG),
        P_SRANGEREG_FIN: setFecha(DFECFINREG),
        P_SRANGECIE_INI: setFecha(DFECINICIE),
        P_SRANGECIE_FIN: setFecha(DFECFINCIE)
    };

    $.ajax({
        url: '../../../Actorial/GetListaActorialMealer',
        contentType: 'application/json',
        data: objParameters,
        success: function (data) {

            var JSonHeader = {
                Column1: 'SELECT',
                Column2: 'ID',
                Column3: 'NOMBRE ARCHIVO',
                Column4: 'ESTADO',
                Column5: 'NESTADO',
                Column6: 'FECHA',
                Column7: 'FECHACIERRE',
                Column8: 'CORRECTOS',
                Column9: 'ERROR',
                Column10: 'TOTAL',
                Column11: 'DESCARGA EXCEL'
            }

            var propsCol = {
                "Columns": [
                    {
                        "col": "1",
                        "name": "Seleccionar",
                        "props": [
                            {
                                "visible": true,
                                "linkStyle": true,
                            }
                        ]
                    },
                    {
                        "col": "2",
                        "name": "ID",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "3",
                        "name": "Nombre de Archivo",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "4",
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
                        "col": "5",
                        "name": "Nestado",
                        "props": [
                            {
                                "visible": false,
                            }
                        ]
                    },
                    {
                        "col": "6",
                        "name": "Fecha Registro",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "7",
                        "name": "Fecha Cierre",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    }
                ]
            };

            var initTable = {
                dataTable: data.entity,
                tableId: '.data-archivos-input-meler',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: false,
                delButton: false,
                colSequence: false,
                pagesize: pagesize_t,
                valDisable: 0,
                callerId: 'loadInputMeler',
                instanceId: 'lstDataInputMeler'
            }

            lstDataInputMeler = new renderTable(initTable)

            $('.link_view').on('click', function () {
                var sname_file = $(this).attr('id').replace('lnk-', '');
                console.log("consola: " + sname_file);
                idMeler = sname_file;
                var cadena = $(this).text();

                if (cadena == "Select") {
                    $('.data-det-input-meler').empty();
                    $('.popup').fadeIn(500);

                    $("#BValue").removeClass();
                    $("#BValue").css("display", "none");

                    loadDetailsSpn();
                }

               
            });

            $("#overlay").fadeOut(300);
        },
        error: function () {
            $("#overlay").fadeOut(300);
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

function loadDetailsSpn() {

    $("#overlay").fadeIn(300);

    var objParameters = {
        P_IDLOTE: idMeler
    };

    $.ajax({
        url: '../../../Actorial/GetListaNroOpercionAfiliado',
        contentType: 'application/json',
        data: objParameters,
        success: function (data) {

            var JSonHeader = {
                Column1: 'NLINEA',
                Column2: 'SNROOPERACION',
                Column3: 'SFECHADEVENGUE',
                Column4: 'SFECHAENVIO',
                Column5: 'SFECHACIERRE'
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
                        "name": "Nro. Operación",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "3",
                        "name": "Fecha Devengue",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "4",
                        "name": "Fecha Envío",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "5",
                        "name": "Fecha Cierre",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    }
                ]
            };

            var initTable = {
                dataTable: data.entity,
                tableId: '.data-det-input-meler',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: true,
                editButton: false,
                delButton: false,
                colSequence: false,
                pagesize: pagesize_t,
                callerId: 'loadDetailsSpn',
                instanceId: 'lstDataDetails'
            }

            lstDataDetails = new renderTable(initTable)

            $("#overlay").fadeOut(300);
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');

            $("#overlay").fadeOut(300);
        }
    });
}

function GetIDButtonPagination(Object) {

    //Questions for rowtotal
    var rows = rowtotal / 10;
    rows = rowtotal % 10 == 0 ? rows : parseInt(rows) + 1;

    //Change background for default 
    $('.content-number-pagination').find('div span').each(function () {
        $(this).css('background', 'white');
    });

    //Change background container
    $(this).css('background', 'rgb(28, 132, 198)');

    //Capture id of span
    var idspan = $(Object).attr('id');

    //Question value selected
    if (idspan != 'preview' && idspan != 'next') {

        //Get id for search in database
        pagenum = $(Object).attr('id').replace('button-', '');

    } else {

        //Conditional for preview
        if (idspan == 'preview') {
            if (pagenum == 0) {
                return true;
            }
            else {
                pagenum -= 1;
            }
        }

        //Conditional for next
        if (idspan == 'next') {
            if (pagenum == rows - 1) {
                return true;
            }
            else {
                pagenum += 1;
            }
        }

    }

    //Invoke function
    loadHeaderSpn();
}

function LoadAnios() {
    $.ajax({
        url: '../../../Comercial/GetAnio',
        contentType: 'application/json',
        success: function (data) {

            //Clear select
            $('.cbxAnio1 option').remove();

            //First option with placeholder
            var optionPlaceHolder = '<option value="0">Año</option>';

            //Add placeholder
            $('.cbxAnio1').append(optionPlaceHolder);

            //Drawing option
            $.each(data.entity, function (key, value) {

                //Create new option
                var newOption = '<option value="' + value.ANIO + '">' + value.ANIO + '</option>';

                //Add option in object select
                $('.cbxAnio1').append(newOption);

            });

        },
        error: function () {
            console.log('%cError: No se pudo obtener los Anios.', 'color:red');
        }
    });
}

function LoadMeses() {

    //Clear select
    $('.cbxMes1 option').remove();

    //First option with placeholder
    var optionPlaceHolder = '<option value="0">Mes</option>';

    //Add placeholder
    $('.cbxMes1').append(optionPlaceHolder);

    //Create new option
    var newOption = '<option value="01">Enero</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="02">Febrero</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="03">Marzo</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="04">Abril</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="05">Mayo</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="06">Junio</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="07">Julio</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="08">Agosto</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="09">Setiembre</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="10">Octubre</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="11">Noviembre</option>'; $('.cbxMes1').append(newOption);
    var newOption = '<option value="12">Diciembre</option>'; $('.cbxMes1').append(newOption);
}

