//Opciones grid Cabecera
this.pagenum = 0;
this.pagesize_t = 10;
this.lstReinsurance;
this.lstReinsurance_nested;
this.lstDetailReinsurance
this.idRowSelected = 0;
var rowtotal = 0;
var nTotal = 0;
var idMeler = 0;
var DFECINIREG = '';
var DFECFINREG = '';
var DFECINICIE = '';
var DFECFINCIE = '';

$(document).ready(function () {

    $("#txtFechaCierre").datepicker({
        language: "es", locale: "es", dateFormat: 'dd/mm/yy', changeYear: true
    });

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

    /*
    $('.select-number-pagination').change(function () {
        pagesize = $(this).val();
        loadHeaderSpn();
    });
    */

    $('#btnNuevo').on('click', function () {
        $('.popup').fadeIn(500);
        $("#BValue").removeClass();

        $("#txtFechaCierre").val('');
        $('#SValue').text("Proceso de Carga Pendiente...");
        $("#BValue").addClass("colorMostaza");
        $("#fileInputId").val(null);

        ResizePopup(null, 200);
    });

    $('#btnSearch').on('click', function () {
        loadHeaderSpn();
    });

    $('#btnCancel').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('.btnClosePopup').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('#btnProcesar').on('click', function () {

        $("#BValue").removeClass();
        $('#SValue').text("Proceso de Carga Pendiente...");
        $("#BValue").addClass("colorMostaza");

        var files = $("#fileInput").get(0).files;
        var fileData = new FormData();

        if (files.length == 0) {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar un archivo', title: 'COTIZADOR' }).show();
            return false;
        }

        var extFile = files[0].name.substring(files[0].name.lastIndexOf(".") + 1, files[0].name.length);
        if (extFile != 'XML' && extFile != 'xml') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar un archivo con extensión XML.', title: 'COTIZADOR' }).show();
            return false;
        }

        $("#overlay").fadeIn(200);

        fileData.append("fileInput", files[0]);
        fileData.append('name', 'Archivos');

        var objParametros = {
            P_SNOMBRE_ARCHIVO: files[0].name
        }

        $.ajax({
            url: '../../../Comercial/GetVerificarNombreArchivoMealer',
            contentType: 'application/json',
            data: objParametros,
            success: function (data) {
                var objData = data.entity;
                if (objData != null) {
                    $("#BValue").removeClass();
                    $("#overlay").fadeOut(300);
                    $('#SValue').text("El archivo ya fue procesado.");
                    $("#BValue").addClass("colorMostaza");
                } else {
                    //RECIEN CARGA EL ARCHIVO
                    $.ajax({
                        type: "POST",
                        url: "../../../Comercial/UploadFilesMealer",
                        contentType: false,
                        processData: false,
                        data: fileData,
                        success: function (data) {
                            $("#BValue").removeClass();

                            if (data.ID == 0) {
                                $('#SValue').text(data.Result);
                                $("#BValue").addClass("colorRojo");
                            } else {
                                var objData = data.entityList;

                                if (objData != null) {
                                    $('#SValue').text("Cantidad de solicitudes cargados: " + objData[0].CANTIDAD);
                                    $("#BValue").addClass("colorAzul");

                                    loadHeaderSpn();
                                } else {
                                    $('#SValue').text("Se ha encontrado un error en la carga.");
                                    $("#BValue").addClass("colorRojo");
                                }
                            }

                            $("#overlay").fadeOut(300);
                        },
                        error: function () {
                            $("#BValue").removeClass();
                            $("#overlay").fadeOut(300);
                            $('#SValue').text("Se ha encontrado un error en la carga.");
                            $("#BValue").addClass("colorRojo");
                        }
                    });
                }
            },
            error: function () {
                $("#BValue").removeClass();
                $("#overlay").fadeOut(300);
                $('#SValue').text("Se ha encontrado un error en la carga.");
                $("#BValue").addClass("colorRojo");
                console.log('%cError: The information could not be obtained.', 'color:red');
            }
        });
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
        url: '../../../Comercial/GetListaComercialMealer',
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
                Column7: 'FECHA2',
                Column8: 'CORRECTOS',
                Column9: 'ERROR',
                Column10: 'TOTAL',
                Column11: 'DESCARGA EXCEL'
            }

            var propsCol = {
                "Columns": [
                    {
                        "col": "1",
                        "name": "SELECT",
                        "props": [
                            {
                                "visible": false
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
                        "name": "Cant. Sol.",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "11",
                        "name": "Descarga Excel",
                        "props": [
                            {
                                "visible": true,
                                "linkdownload": true,
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
                delButton: true,
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

                    if (nidEstado == "2" || nidEstado == "3") {
                        $('#btn_d_row_' + (id).toString()).hide();

                        if (nidEstado == "3") {
                            $('.DESCARGA-' + (id).toString()).css("display", "none");
                        }
                    }
                        
                });
            });

            $('.link_view').on('click', function () {
                var sname_file = $(this).attr('id').replace('lnk-', '');

                var objParametros = {
                    P_IDLOTE: sname_file
                };

                window.location.href = '../../../Report/ReporteComercialMealer?' + $.param(objParametros);
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {
                //Get id profile
                idMeler = $(this).attr('id').replace('btn_d_row_', '');

                //Modal for confirmation
                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'COTIZADOR',
                    labels: { ok: 'Ok', cancel: 'Cancelar' },
                    message: '¿Está seguro de eliminar el registro seleccionado?',
                    onok: function () {

                        var JSonSend = {
                            P_IDLOTE: idMeler
                        }

                        $.ajax({
                            url: '../../../Comercial/GetDeleteSolicitud',
                            contentType: "application/json",
                            data: JSonSend,
                            success: function (response) {
                                //Get request answer
                                if (response.entity == false) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: response.entityDel.P_MESSAGE, title: 'COTIZADOR' }).show();
                                } else {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se ha eliminado el registro correctamente.', title: 'COTIZADOR' }).show();
                                    $('#btnSearch').click();
                                }

                                return true;
                            },
                            error: function () {
                                console.log('%cError: Unable to delete profile', 'color:red');
                            }
                        });
                    },
                    oncancel: function () {

                    }
                }).show();
            });

        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

/*
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
*/

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

