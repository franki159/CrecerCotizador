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

    $('.daterangeRegistro').daterangepicker({
        opens: 'left',
        locale: {
            format: 'DD/MMM/YYYY'
        }

    }, function (start, end, label) {
        DFECINIREG = start.format('DD/MM/YYYY');
        DFECFINREG = end.format('DD/MM/YYYY')
    });

    LoadEstadoSalud();
    LoadSexo();
    //loadHeaderSpn();

    $('#btnNuevo').on('click', function () {
        $('.popup').fadeIn(500);
        $("#BValue").removeClass();

        $('#txtCic').val('');
        $('#txtTasaVenta').val('');
        $('#txtAjuste').val('');
        $('#txtEdad').val('');
        $('#cboSexo').val('0');
        $('#cboCondSalud').val('0');
        $('#txtTiempo').val('');
        $('#txtTasaAjustada').val('');
        $('#txtPensionAnual').val('');
        $('#txtPensionMensual').val('');

        $('#txtCic').focus();

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

        if ($.trim($('#txtCic').val()) == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el CIC.', title: 'COTIZADOR' }).show();
            return false;
        }

        if ($.trim($('#txtTasaVenta').val()) == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el porcentaje de la tasa de venta.', title: 'COTIZADOR' }).show();
            return false;
        }

        if ($.trim($('#txtAjuste').val()) == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el porcentaje de ajuste.', title: 'COTIZADOR' }).show();
            return false;
        }

        if ($.trim($('#txtEdad').val()) == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la edad.', title: 'COTIZADOR' }).show();
            return false;
        }

        if ($('#cboSexo').val() == 0) {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar el sexo.', title: 'COTIZADOR' }).show();
            return false;
        }

        if ($('#cboCondSalud').val() == 0) {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar la condicion de salud.', title: 'COTIZADOR' }).show();
            return false;
        }

        $("#overlay").fadeIn(200);

        var vcic = parseFloat(Number($('#txtCic').val().replace(/,/g, '')));
        var vtasaventa = parseFloat(Number($('#txtTasaVenta').val().replace(/,/g, '')));
        var vajuste = parseFloat(Number($('#txtAjuste').val().replace(/,/g, '')));

        var objParametros = {
            cic: vcic,
            tasaventa: vtasaventa,
            ajuste: vajuste,
            edad: eval($('#txtEdad').val()),
            sexo: $('#cboSexo').val(),
            condsalud: $('#cboCondSalud').val()
        }

        $.ajax({
            url: '../../../Actorial/GetProcesoAnualidad',
            contentType: 'application/json',
            data: objParametros,
            success: function (data) {
                $("#overlay").fadeOut(300);
                var objData = data.entity[0];

                $('#txtTiempo').val(objData.ANIO);
                $('#txtTasaAjustada').val(objData.TASA_AJUSTADA);
                $('#txtPensionAnual').val(objData.PENSION_ANUAL);
                $('#txtPensionMensual').val(objData.PENSION_MENSUAL);
            },
            error: function () {
                $("#overlay").fadeOut(300);
                console.log('%cError: The information could not be obtained.', 'color:red');
            }
        });
    });

    $('#btnAccept').on('click', function () {

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

function LoadSexo() {

    var objParametros = {
        sDesc: 'SEXO'
    }

    $.ajax({
        url: '../../../Actorial/GetListaTablaMaestra',
        contentType: 'application/json',
        beforeSend: function () {
            $('#cboSexo').empty();
        },
        data: objParametros,
        success: function (data) {

            //First option with placeholder
            var optionPlaceHolder = '<option value="0">Seleccione</option>';

            //Add placeholder
            $('#cboSexo').append(optionPlaceHolder);

            //Drawing option
            $.each(data.entity, function (key, value) {

                //Create new option
                var newOption = '<option value="' + value.ID + '">' + value.SDESCVALOR + '</option>';

                //Add option in object select
                $('#cboSexo').append(newOption);

            });

        },
        error: function () {
            console.log('%cError: No se pudo obtener los Anios.', 'color:red');
        }
    });
}

function LoadEstadoSalud() {

    var objParametros = {
        sDesc: 'ESTADO SALUD'
    }

    $.ajax({
        url: '../../../Actorial/GetListaTablaMaestra',
        contentType: 'application/json',
        beforeSend: function () {
            $('#cboCondSalud').empty();
        },
        data: objParametros,
        success: function (data) {

            //First option with placeholder
            var optionPlaceHolder = '<option value="0">Seleccione</option>';

            //Add placeholder
            $('#cboCondSalud').append(optionPlaceHolder);

            //Drawing option
            $.each(data.entity, function (key, value) {

                //Create new option
                var newOption = '<option value="' + value.ID + '">' + value.SDESCVALOR + '</option>';

                //Add option in object select
                $('#cboCondSalud').append(newOption);

            });

        },
        error: function () {
            console.log('%cError: No se pudo obtener los Anios.', 'color:red');
        }
    });
}