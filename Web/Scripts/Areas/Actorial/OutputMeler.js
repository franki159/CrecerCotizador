//Opciones grid Cabecera
this.pagenum = 0;
this.pagesize_t = 10;
this.lstReinsurance;
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

    loadHeaderSpn();

    $('.select-number-pagination').change(function () {
        pagesize = $(this).val();
        loadHeaderSpn();
    });

    $('#btnNuevo').on('click', function () {
        $("#BValue").removeClass()
        $('.popup').fadeIn(500);

        $("#tblConsulta").css("display", "none");
        $("#fileInput").val("");
        $('#SValue').text("Inicio de Proceso de Carga.");
        $("#BValue").addClass("colorMostaza");
        $("#fileInputId").val(null);

        $('#btn_procesar').prop('disabled', true);
        ResizePopup(null, 200);
    });

    $('#btnSearch').on('click', function () {
        pagenum = 0;
        pagesize_t = 10;
        loadHeaderSpn();
    });

    $('#btnCancel').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('.btnClosePopup').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('#btnProcesar').on('click', function () {

        $("#BValue").removeClass()
        $("#tblConsulta").css("display", "none");
        $('#btnProcesar').prop('disabled', false);

        var files = $("#fileInput").get(0).files;
        var fileData = new FormData();

        if (files.length == 0) {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar un archivo', title: 'COTIZADOR' }).show();
            $('#SValue').text("Inicio de Proceso de Carga.");
            $("#BValue").addClass("colorMostaza");
            return false;
        }

        var extFile = files[0].name.substring(files[0].name.lastIndexOf(".") + 1, files[0].name.length);
        if (extFile != 'xls' && extFile != 'xlsx') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar un archivo excel.', title: 'COTIZADOR' }).show();
            $('#SValue').text("Inicio de Proceso de Carga.");
            $("#BValue").addClass("colorMostaza");
            return false;
        }

        $("#overlay").fadeIn(200);

        fileData.append("fileInput", files[0]);
        fileData.append('name', idMeler.toString());

        var objParametros = {
            P_SNOMBRE_ARCHIVO: files[0].name
        }

        $.ajax({
            url: '../../../Actorial/GetVerificarNombreArchivoOutputMealer',
            contentType: 'application/json',
            data: objParametros,
            success: function (data) {
                var objData = data.entity;
                if (objData != null) {
                    $("#overlay").fadeOut(300);
                    $('#SValue').text("El archivo ya fue procesado.");
                    $("#BValue").addClass("colorMostaza");
                } else {
                    //RECIEN CARGA EL ARCHIVO
                    $.ajax({
                        type: "POST",
                        url: "../../../Actorial/UploadFiles",
                        contentType: false,
                        processData: false,
                        data: fileData,
                        success: function (data) {
                            var objData = data.entityList;

                            if (objData != null) {

                                if (objData[0].EXITO == "1") {
                                    $('#SValue').text("Cantidad de solicitudes cargados: " + objData[0].CANTIDAD);
                                    $("#BValue").addClass("colorAzul");

                                    //loadHeaderSpn();
                                    loadDetailsSpn();
                                } else if (objData[0].EXITO == "-1") {
                                    $('#SValue').text(objData[0].MENSAJE);
                                    $("#BValue").addClass("colorRojo");
                                    $("#overlay").fadeOut(300);
                                    $("#fileInput").val("");
                                } else if (objData[0].EXITO == "-2") {
                                    $("#tblConsulta").css("display", "table");
                                    $("#tblConsulta tbody").empty();
                                    $('#SValue').text("Errores");
                                    $("#BValue").addClass("colorRojo");
                                    $("#fileInput").val("");

                                    var objDataExcel = data.entityList[0].DATA;
                                    var tr = "";
                                    var styleFila = "";
                                    for (var i = 0; i < objDataExcel.length; i++) {
                                        if (i % 2 == 0)
                                            styleFila = "filaPrincipal";
                                        else
                                            styleFila = "filaSecundario";

                                        tr += '<tr>';
                                        tr += '<td class="' + styleFila +'">' + objDataExcel[i].CANTIDAD + '</td>';
                                        tr += '<td class="' + styleFila +'">' + objDataExcel[i].MENSAJE + '</td>';
                                        tr += '</tr>';
                                    }

                                    $("#tblConsulta tbody").append(tr);
                                    $("#overlay").fadeOut(300);
                                } else if (objData[0].EXITO == "-3") {
                                    $('#SValue').text(objData[0].MENSAJE);
                                    $("#BValue").addClass("colorRojo");
                                    $("#overlay").fadeOut(300);
                                    $("#fileInput").val("");
                                }
                                
                            } else {
                                $('#SValue').text("Se ha encontrado un error en la carga.");
                                $("#BValue").addClass("colorRojo");

                                $("#overlay").fadeOut(300);
                                $("#fileInput").val("");
                            }
                        },
                        error: function () {
                            $("#overlay").fadeOut(300);
                            $('#SValue').text("Se ha encontrado un error en la carga.");
                            $("#BValue").addClass("colorRojo");
                            $("#fileInput").val("");
                        }
                    });
                }
            },
            error: function () {
                $("#overlay").fadeOut(300);
                $('#SValue').text("Se ha encontrado un error en la carga.");
                $("#BValue").addClass("colorRojo");
                console.log('%cError: The information could not be obtained.', 'color:red');
                $("#fileInput").val("");
            }
        });
    });

    $('#btnVolver').on('click', function () {
        $('.content-pill').hide();
        $('#nav-icg').removeClass('c-active');
        $($('#nav-icb').data('target')).fadeIn();
        $('#nav-icb').addClass('c-active');
    });

    $('#btnPlantilla').on('click', function () {
        window.location.href = '../../../Actorial/DownloadPlantilla';
    });
});

function loadHeaderSpn() {

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
                    },
                    {
                        "col": "8",
                        "name": "Correctos",
                        "props": [
                            {
                                "visible": false,
                            }
                        ]
                    },
                    {
                        "col": "9",
                        "name": "Errores",
                        "props": [
                            {
                                "visible": false,
                            }
                        ]
                    },
                    {
                        "col": "10",
                        "name": "Cant. Sol. Comercial",
                        "props": [
                            {
                                "visible": false,
                            }
                        ]
                    },
                    {
                        "col": "11",
                        "name": "Descarga Excel Comercial",
                        "props": [
                            {
                                "visible": true,
                                "linkStyle": true,
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
                callerId: 'loadHeaderSpn',
                instanceId: 'lstReinsurance'
            }

            $.when(
                lstReinsurance = new renderTable(initTable)
            ).done(function () {
                $('.data-archivosunat tbody tr').each(function (index, value) {
                    var nidEstado = "";
                    var id = 0;
                    $(this).children("td").each(function (i, val) {
                        switch (i) {
                            case 4:
                                nidEstado = data.entity[index].NESTADO;
                                id = data.entity[index].NID;
                                break;
                        }
                    });

                    if (nidEstado == "2" || nidEstado == "3")
                        $('#btn_d_row_' + (id).toString()).hide();
                });
            });

            $('.link_view').on('click', function () {

                var sname_file = $(this).attr('id').replace('lnk-', '');
                var cadena = $(this).text();

                if (cadena == "Select") {
                    $('.content-pill').hide();
                    $('#nav-icb').removeClass('c-active');
                    $($('#nav-icg').data('target')).fadeIn();
                    $('#nav-icg').addClass('c-active');
                    idMeler = sname_file;

                    loadDetailsSpn();
                } else {
                    var objParametros = {
                        P_IDLOTE: sname_file
                    };

                    window.location.href = '../../../Report/ReporteComercialMealer?' + $.param(objParametros);
                }
            });

        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

function loadDetailsSpn() {

    var objParameters = {
        P_PERIODO: idMeler,
        P_NPAGENUM: pagenum,
        P_NPAGESIZE: pagesize_t
    };

    $.ajax({
        url: '../../../Actorial/GetListaActorialOutputMealer',
        contentType: 'application/json',
        data: objParameters,
        success: function (data) {

            var JSonHeader = {
                Column1: 'ID',
                Column2: 'IDINPUT',
                Column3: 'NOMBRE ARCHIVO',
                Column4: 'ESTADO',
                Column5: 'NESTADO',
                Column6: 'FECHA',
                Column7: 'CORRECTOS',
                Column8: 'ERROR',
                Column9: 'TOTAL',
                Column10: 'DESCARGA XML'
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
                        "name": "ID INPUT",
                        "props": [
                            {
                                "visible": false
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
                        "name": "Correctos",
                        "props": [
                            {
                                "visible": false,
                            }
                        ]
                    },
                    {
                        "col": "8",
                        "name": "Errores",
                        "props": [
                            {
                                "visible": false,
                            }
                        ]
                    },
                    {
                        "col": "9",
                        "name": "Cant. Sol. Comercial",
                        "props": [
                            {
                                "visible": false,
                            }
                        ]
                    },
                    {
                        "col": "10",
                        "name": "Descarga XML",
                        "props": [
                            {
                                "visible": true,
                                "fileDescargar": true,
                            }
                        ]
                    }
                ]
            };

            var initTable = {
                dataTable: data.entity,
                tableId: '.data-archivosDetails',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: false,
                delButton: true,
                colSequence: false,
                pagesize: pagesize_t,
                callerId: 'loadDetailsSpn',
                instanceId: 'lstDataDetails'
            }

            $.when(
                lstDataDetails = new renderTable(initTable)
            ).done(function () {
                $('.data-archivosDetails tbody tr').each(function (index, value) {
                    var nidEstado = "";
                    var id = 0;
                    $(this).children("td").each(function (i, val) {
                        switch (i) {
                            case 5:
                                nidEstado = data.entity[index].NESTADO;
                                id = data.entity[index].NID;
                                break;
                        }
                    });

                    if (nidEstado == "2" || nidEstado == "3") {
                        $('#btn_d_row_' + (id).toString()).hide();

                        if (nidEstado == "3") {
                            $('#btn_w_row' + (id).toString()).css("display", "none");
                        }
                    }
                        
                });
            });

            $('.btnRowDownload').on('click', function () {
                var sname_file = $(this).attr('id').replace('btn_w_row', '');

                var objParametros = {
                    P_IDLOTE: sname_file
                };
                
                window.location.href = '../../../Report/DownloadXml?' + $.param(objParametros);
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {
                //Get id profile
                var idMelerOutput = $(this).attr('id').replace('btn_d_row_', '');

                //Modal for confirmation
                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'COTIZADOR',
                    labels: { ok: 'Ok', cancel: 'Cancelar' },
                    message: '¿Está seguro de eliminar el registro seleccionado?',
                    onok: function () {

                        var JSonSend = {
                            P_IDLOTE: idMelerOutput
                        }

                        $.ajax({
                            url: '../../../Actorial/GetDeleteSolicitud',
                            contentType: "application/json",
                            data: JSonSend,
                            success: function (response) {
                                //Get request answer
                                if (response.entity == false) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: response.entityDel.P_MESSAGE, title: 'COTIZADOR' }).show();
                                } else {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se ha eliminado el registro correctamente.', title: 'COTIZADOR' }).show();
                                    loadDetailsSpn();
                                }

                                return true;
                            },
                            error: function () {
                                console.log('%cError: Unable to delete Output Meler', 'color:red');
                            }
                        });
                    },
                    oncancel: function () {

                    }
                }).show();
            });

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

