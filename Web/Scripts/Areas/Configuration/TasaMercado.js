//Opciones grid Cabecera
this.pagenum = 0;
this.pagesize_t = 10;
this.lstData;
this.idRowSelected = 0;
var rowtotal = 0;
var nTotal = 0;
var idTasaMercado = 0;
var DFECINIREG = '';
var DFECFINREG = '';
var criterio = 1;

$(document).ready(function () {

    loadLista();
    
    $('.daterangeRegistro').daterangepicker({
        opens: 'left',
        locale: {
            format: 'DD/MMM/YYYY'
        }

    }, function (start, end, label) {
        DFECINIREG = start.format('DD/MM/YYYY');
        DFECFINREG = end.format('DD/MM/YYYY')
    });

    $('#btnNuevo').on('click', function () {

        criterio = 1;
        $('.popup').fadeIn(500);
        $('.title-span').text('Nuevo Tasa Mercado');
        $('#txtJubSA').focus();
        LimpiarModal();

        ResizePopup(null, 200);
    });

    $('#btnSearch').on('click', function () {
        idTasaMercado = 0;
        loadLista();
    });

    $('#btnCancel').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('.btnClosePopup').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('#btnAccept').on('click', function () {
        MantSepelio();
    });
});

function loadLista() {

    $("#overlay").fadeIn(300);

    var objParameters = {
        P_SRANGEREGINI: setFecha(DFECINIREG),
        P_SRANGEREGFIN: setFecha(DFECFINREG),
        P_IDTASAMERCADO: idTasaMercado,
        P_NPAGESIZE: pagesize_t,
        P_NPAGENUM: pagenum
    };

    $.ajax({
        url: '../../../Mantenimiento/GetListaTasaMercado',
        contentType: 'application/json',
        data: objParameters,
        success: function (data) {

            var JSonHeader = {
                Column1: 'NID',
                Column2: 'IDTASAMERCADO',
                Column3: 'TASAMERCJUBSA',
                Column4: 'TASAMERCJUBDA',
                Column5: 'TASAMERCJUBSI',
                Column6: 'TASAMERCINVSA',
                Column7: 'TASAMERCINVDA',
                Column8: 'TASAMERCINVSI',
                Column9: 'TASAMERCSOBSA',
                Column10: 'TASAMERCSOBDA',
                Column11: 'TASAMERCSOBSI',
                Column12: 'ESTADO',
                Column13: 'NVIGENCIA',
                Column14: 'NESTADO',
                Column15: 'FECHA_REGISTRO'
            }

            var propsCol = {
                "Columns": [
                    {
                        "col": "1",
                        "name": "NID",
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
                        "name": "Jubilación Soles Ajustados",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "4",
                        "name": "Jubilación  Dólares Ajustados",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "5",
                        "name": "Jubilación Soles Indexados",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "6",
                        "name": "Invalidez Soles Ajustados",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "7",
                        "name": "Invalidez  Dólares Ajustados",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "8",
                        "name": "Invalidez Soles Indexados",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "9",
                        "name": "Sobrevivencia Soles Ajustados",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "10",
                        "name": "Sobrevivencia  Dólares Ajustados",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "11",
                        "name": "Sobrevivencia Soles Indexados",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "12",
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
                        "col": "13",
                        "name": "Nestado",
                        "props": [
                            {
                                "visible": false
                            }
                        ]
                    },
                    {
                        "col": "14",
                        "name": "Vigencia",
                        "props": [
                            {
                                "toggle": true
                            }
                        ]
                    },
                    {
                        "col": "15",
                        "name": "Fecha Registro",
                        "props": [
                            {
                                "visible": false
                            }
                        ]
                    }
                ]
            };

            var initTable = {
                dataTable: data.entityList,
                tableId: '.data-Tasa-Mercado',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: true,
                delButton: true,
                colSequence: false,
                pagesize: pagesize_t,
                valDisable: 0,
                callerId: 'loadLista',
                instanceId: 'lstData'
            }

            $.when(
                lstData = new renderTable(initTable)
            ).done(function () {
                $('.data-Tasa-Mercado tbody tr').each(function (index, value) {
                    var nidEstado = "";
                    var id = 0;
                    $(this).children("td").each(function (i, val) {
                        switch (i) {
                            case 4:
                                nidEstado = data.entityList[index].NESTADO;
                                id = data.entityList[index].NID;
                                break;
                        }
                    });

                    if (nidEstado == "3") {
                        $('#btn_e_row_' + (id).toString()).hide();
                        $('#btn_d_row_' + (id).toString()).hide();
                    }

                });
            });

            //Add function a button edit row
            $('.btnRowEdit').on('click', function () {
                idTasaMercado = $(this).attr('id').replace('btn_e_row_', '');
                criterio = 2;

                EditModal();
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {
                idTasaMercado = $(this).attr('id').replace('btn_d_row_', '');
                criterio = 3;

                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'COTIZADOR',
                    labels: { ok: 'Ok', cancel: 'Cancelar' },
                    message: '¿Está seguro de eliminar el registro seleccionado?',
                    onok: function () {

                        var model = new Object();

                        model.P_CRITERIO = criterio;
                        model.P_IDTASAMERCADO = idTasaMercado;

                        $.ajax({
                            type: 'POST',
                            url: '../../../Mantenimiento/MantTasaMercado',
                            contentType: 'application/json',
                            data: JSON.stringify(model),
                            success: function (data) {

                                //Using function of library
                                if (data.entityList) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'COTIZADOR' }).show();
                                    $('.btnClosePopup').click();
                                    idTasaMercado = 0;
                                    loadLista();
                                    return true;
                                } else {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se presentaron errores al momento de eliminar el registro.', title: 'COTIZADOR' }).show();
                                    return false;
                                }
                            },
                            error: function () {
                                console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
                                return false;
                            }
                        });

                    },
                    oncancel: function () {

                    }
                }).show();

            });

            $(".toggle-grid").on('click', function () {
                debugger;
                if ($(this).is(":checked")) {
                    idTasaMercado = $(this).attr('id').replace('toggle-', '');
                    criterio = 4;

                    alertify.dialog('confirm').set({
                        transition: 'zoom',
                        title: 'COTIZADOR',
                        labels: { ok: 'Ok', cancel: 'Cancelar' },
                        message: '¿Está seguro de activar el registro seleccionado?',
                        onok: function () {

                            var model = new Object();

                            model.P_CRITERIO = criterio;
                            model.P_IDTASAMERCADO = idTasaMercado;

                            $.ajax({
                                type: 'POST',
                                url: '../../../Mantenimiento/MantTasaMercado',
                                contentType: 'application/json',
                                data: JSON.stringify(model),
                                success: function (data) {

                                    //Using function of library
                                    if (data.entityList) {
                                        alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'COTIZADOR' }).show();
                                        $('.btnClosePopup').click();
                                        idTasaMercado = 0;
                                        loadLista();
                                        return true;
                                    } else {
                                        alertify.dialog('alert').set({ transition: 'zoom', message: 'Se presentaron errores al momento de activar el registro.', title: 'COTIZADOR' }).show();
                                        return false;
                                    }
                                },
                                error: function () {
                                    console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
                                    return false;
                                }
                            });

                        },
                        oncancel: function () {

                        }
                    }).show();
                } else {
                    $(this).prop('checked', true);
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

function MantSepelio() {

    if ($.trim($('#txtJubSA').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la Jubilación Soles Ajustados.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtJubDA').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la Jubilación Dólares Ajustados.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtJubSI').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la Jubilación Soles Indexados.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtInvSA').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la Invalidez Soles Ajustados.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtInvDA').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la Invalidez Dólares Ajustados.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtInvSI').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la Invalidez Soles Indexados.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtSobSA').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la Sobrevivencia Soles Ajustados.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtSobDA').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la Sobrevivencia Dólares Ajustados.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtSobSI').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la Sobrevivencia Soles Indexados.', title: 'COTIZADOR' }).show();
        return false;
    }

    var vMontoJubSA = parseFloat(Number($('#txtJubSA').val().replace(/,/g, '')));
    var vMontoJubDA = parseFloat(Number($('#txtJubDA').val().replace(/,/g, '')));
    var vMontoJubSI = parseFloat(Number($('#txtJubSI').val().replace(/,/g, '')));
    var vMontoInvSA = parseFloat(Number($('#txtInvSA').val().replace(/,/g, '')));
    var vMontoInvDA = parseFloat(Number($('#txtInvDA').val().replace(/,/g, '')));
    var vMontoInvSI = parseFloat(Number($('#txtInvSI').val().replace(/,/g, '')));
    var vMontoSobSA = parseFloat(Number($('#txtSobSA').val().replace(/,/g, '')));
    var vMontoSobDA = parseFloat(Number($('#txtSobDA').val().replace(/,/g, '')));
    var vMontoSobSI = parseFloat(Number($('#txtSobSI').val().replace(/,/g, '')));

    var cadena = "";

    if (criterio == 1)
        cadena = '¿Está seguro de guardar el registro?';
    else if (criterio == 2)
        cadena = '¿Está seguro de modificar el registro?';

    //Modal for confirmation
    alertify.dialog('confirm').set({
        transition: 'zoom',
        title: 'COTIZADOR',
        labels: { ok: 'Ok', cancel: 'Cancelar' },
        message: cadena,
        onok: function () {

            var model = new Object();

            model.P_CRITERIO = criterio;
            model.P_IDTASAMERCADO = idTasaMercado;
            model.P_TASAMERCJUBSA = vMontoJubSA;
            model.P_TASAMERCJUBDA = vMontoJubDA;
            model.P_TASAMERCJUBSI = vMontoJubSI;
            model.P_TASAMERCINVSA = vMontoInvSA;
            model.P_TASAMERCINVDA = vMontoInvDA;
            model.P_TASAMERCINVSI = vMontoInvSI;
            model.P_TASAMERCSOBSA = vMontoSobSA;
            model.P_TASAMERCSOBDA = vMontoSobDA;
            model.P_TASAMERCSOBSI = vMontoSobSI;

            $.ajax({
                type: 'POST',
                url: '../../../Mantenimiento/MantTasaMercado',
                contentType: 'application/json',
                data: JSON.stringify(model),
                success: function (data) {

                    if (data.entityList) {
                        alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'COTIZADOR' }).show();
                        idTasaMercado = 0;
                        loadLista();
                        $('.popup').fadeOut(500);
                    }
                    
                },
                error: function () {
                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se presentaron errores al momento de guardar el registro.', title: 'COTIZADOR' }).show();
                    console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
                    return false;
                }
            });
        },
        oncancel: function () {

        }
    }).show();
}

function EditModal() {

    //Open popup
    $('.popup').fadeIn(500);

    //Resize form register & edit user
    ResizePopup(null, 200);

    LimpiarModal();
    $('.title-span').text('Editar Tasa Mercado');

    var objParametros = {
        P_RANGE_INI: setFecha(DFECINIREG),
        P_RANGE_FIN: setFecha(DFECFINREG),
        P_IDTASAMERCADO: idTasaMercado,
        P_NPAGESIZE: -1,
        P_NPAGENUM: 0
    };

    $.ajax({
        url: '../../../Mantenimiento/GetListaTasaMercado',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var objData = data.entityList;

            if (objData != null) {

                //Paint data in inputs
                $('#txtJubSA').val(objData[0].NTASAMERCJUBSA);
                $('#txtJubDA').val(objData[0].NTASAMERCJUBDA);
                $('#txtJubSI').val(objData[0].NTASAMERCJUBSI);
                $('#txtInvSA').val(objData[0].NTASAMERCINVSA);
                $('#txtInvDA').val(objData[0].NTASAMERCINVDA);
                $('#txtInvSI').val(objData[0].NTASAMERCINVSI);
                $('#txtSobSA').val(objData[0].NTASAMERCSOBSA);
                $('#txtSobDA').val(objData[0].NTASAMERCSOBDA);
                $('#txtSobSI').val(objData[0].NTASAMERCSOBSI);
            }
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

function LimpiarModal() {
    $('#txtJubSA').val('');
    $('#txtJubDA').val('');
    $('#txtJubSI').val('');
    $('#txtInvSA').val('');
    $('#txtInvDA').val('');
    $('#txtInvSI').val('');
    $('#txtSobSA').val('');
    $('#txtSobDA').val('');
    $('#txtSobSI').val('');
}
