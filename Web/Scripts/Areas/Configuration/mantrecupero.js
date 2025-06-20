this.pagenum = 0;
this.pagesize_t = 10;
this.lstRecupero;
var rowtotal = 0;
var criterio = 1;
var idMantenedor = 0;

$(document).ready(function () {

    //Load list of mantenedor since database
    LoadRecupero();

    $("#txtFecLiberacion").datepicker({
        language: "es", locale: "es", dateFormat: 'dd/mm/yy', changeYear: true
    });

    $("#txtFecRecupero").datepicker({
        language: "es", locale: "es", dateFormat: 'dd/mm/yy', changeYear: true
    });

    //Lista tipo pago
    ListaTipoPago();

    //Event click for button
    $('.btnSearch').on('click', function () {
        LoadRecupero();
    });

    //Event close popup
    $('.btnClosePopup').on('click', function () {
        criterio = 1;
        $('.popup').fadeOut(500);
        DestroyTreeView('tree-resources');
    });

    //Event open popup
    $('#btnOpenPopup').on('click', function () {

        $('#txtPoliza').removeAttr("disabled");
        $('#txtPoliza').css("width", "83%");
        $('#div_busq').css("display", "block");
        $('#cboTipoRecupero').val('0');
        HabilitarControles(false);
        CleanModal();
        criterio = 1;

        //Open popup
        $('.popup').fadeIn(500);

        //Resize form register & edit user
        ResizePopup(null, 335);

        $('#btnAccept').css("display", "none");
        $('.title-span').text('Nuevo Mantenedor de Recupero');

        $('#txtPoliza').focus();
        $('#txtPoliza').select();
    });

    //Event cancel
    $('.btnCancel').on('click', function () {
        criterio = 1;
        $('.popup').fadeOut(500);
        DestroyTreeView('tree-resources');
    });

    $('.select-number-pagination').change(function () {
        pagesize = $(this).val();
        LoadRecupero();
    });

    //Buscar Poliza
    $('#btn_buscarPoliza').on('click', function () {
        ValidatePoliza();
    });

    //Mantenimiento
    $('#btnAccept').on('click', function () {
        ManteRecupero();
    });

});

function LoadRecupero() {

    var spoliza = $.trim($('#txtDescription').val());

    if (spoliza == "")
        spoliza = "*";
    else
        spoliza = $.trim($('#txtDescription').val().trim());

    var objParametros = {
        P_SPOLIZA: spoliza,
        P_NPAGESIZE: pagesize_t,
        P_NPAGENUM: pagenum
    };

    $.ajax({
        url: '../../../Configuration/GetListaMantenedorRecupero',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var JSonHeader = {
                Column1: 'ID',
                Column2: 'Póliza',
                Column3: 'Monto Lib.',
                Column4: 'Fecha Lib.',
                Column5: 'Monto Rec.',
                Column6: 'Fecha Rec.',
                Column7: 'Tipo Rec.',
                Column8: 'Temporalidad',
                Column9: 'Usuario Creación',
                Column10: 'Fecha Creacion'
            };

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
                        "name": "Póliza",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "3",
                        "name": "Monto Lib.",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "4",
                        "name": "Fecha Lib.",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "5",
                        "name": "Monto Rec.",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "6",
                        "name": "Fecha Rec.",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "7",
                        "name": "nTipo Rec.",
                        "props": [
                            {
                                "visible": false,
                            }
                        ]
                    },
                    {
                        "col": "8",
                        "name": "Tipo Rec.",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "9",
                        "name": "Temporalidad",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "10",
                        "name": "Usuario Creación",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "11",
                        "name": "Fecha Creación",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    }
                ]
            };

            var initTable = {
                dataTable: data.entityListRecupero,
                tableId: '.data-recupero',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: true,
                delButton: true,
                colSequence: false,
                pagesize: pagesize_t,
                callerId: 'LoadRecupero',
                instanceId: 'lstRecupero'
            }

            lstRecupero = new renderTable(initTable);

            //Add function a button edit row
            $('.btnRowEdit').on('click', function () {
                idMantenedor = $(this).attr('id').replace('btn_e_row_', '');
                criterio = 2;

                EditModal();
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {

                //Get id Mantenedor
                idMantenedor = $(this).attr('id').replace('btn_d_row_', '');
                criterio = 3;

                //Modal for confirmation
                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'SISPOC',
                    labels: { ok: 'Ok', cancel: 'Cancelar' },
                    message: '¿Está seguro de eliminar el registro seleccionado?',
                    onok: function () {

                        var model = new Object();

                        model.P_CRITERIO = criterio;
                        model.P_NID_MANTRECUPERO = idMantenedor;
                        model.P_SPOLIZA = "";
                        model.P_STEMPORALIDAD = "";

                        $.ajax({
                            type: 'POST',
                            url: '../../../Configuration/MantRecupero',
                            contentType: 'application/json',
                            data: JSON.stringify(model),
                            success: function (data) {

                                //Using function of library
                                if (data.entityList) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'SISPOC' }).show();
                                    $('.btnClosePopup').click();
                                    LoadRecupero();
                                    return true;
                                } else {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se presentaron errores al momento de eliminar el registro.', title: 'SISPOC' }).show();
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

        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

function ValidatePoliza() {

    var objParametros = {
        P_SPOLIZA: $('#txtPoliza').val()
    };

    $.ajax({
        url: '../../../Configuration/GetValidaMantenedorRecupero',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            if (data.entityList) {

                if (data.entityList[0].EXITO == 1) {
                    $('#btnAccept').css("display", "inline-flex");
                    HabilitarControles(true);
                } else {
                    HabilitarControles(false);

                    alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'SISPOC' }).show();
                }

                return true;
            }
        },
        error: function () {
            console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
            return false;
        }
    });
}

function HabilitarControles(validate) {

    if (validate == true) {
        $('#txtMontoLiberacion').removeAttr("disabled");
        $('#txtFecLiberacion').removeAttr("disabled");
        $('#txtRecupero').removeAttr("disabled");
        $('#txtFecRecupero').removeAttr("disabled");
        $('#cboTipoRecupero').removeAttr("disabled");
        $('#txtTemporalidad').removeAttr("disabled");
    } else {
        $('#txtMontoLiberacion').attr("disabled", "disabled");
        $('#txtFecLiberacion').attr("disabled", "disabled");
        $('#txtRecupero').attr("disabled", "disabled");
        $('#txtFecRecupero').attr("disabled", "disabled");
        $('#cboTipoRecupero').attr("disabled", "disabled");
        $('#txtTemporalidad').attr("disabled", "disabled");
    }
}

function ListaTipoPago() {

    var objParametros = {
        PNID_TIPO_TABLA_MAESTRA: 0,
        PSNOMBRE_TABLA: 'TIPO RECUPERO GARANTIA',
        PNORDER: 0
    }

    $.ajax({
        url: '../../../Tools/GetTablaMaestra',
        contentType: 'application/json',
        data: objParametros,
        beforeSend: function () {
            $('#cboTipoRecupero').empty();
        },
        success: function (data) {
            var data = data.entityList;
            var option = '<option value="0">Seleccione</option>';

            $(data).each(function (index, value) {
                option = option + '<option value="' + value.NID + '">' + value.SDESC_VALOR + '</option>';
            });

            $('#cboTipoRecupero').append(option);
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

function ManteRecupero() {

    var cadena = "";

    if (criterio == 1)
        cadena = '¿Está seguro de guardar el registro?';
    else if (criterio == 2)
        cadena = '¿Está seguro de modificar el registro?';

    if ($.trim($('#txtTemporalidad').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la temporalidad.', title: 'SISPOC' }).show();
        return false;
    }

    var montolib = parseFloat(Number($('#txtMontoLiberacion').val().replace(/,/g, '')));
    var montorec = parseFloat(Number($('#txtRecupero').val().replace(/,/g, '')));

    //Modal for confirmation
    alertify.dialog('confirm').set({
        transition: 'zoom',
        title: 'SISPOC',
        labels: { ok: 'Ok', cancel: 'Cancelar' },
        message: cadena,
        onok: function () {

            var model = new Object();

            model.P_CRITERIO = criterio;
            model.P_NID_MANTRECUPERO = idMantenedor;
            model.P_SPOLIZA = $('#txtPoliza').val();
            model.P_NMONTOLIBERACION = montolib;
            model.P_DFECLIBERACION = $('#txtFecLiberacion').val();
            model.P_NRECUPERO = montorec;
            model.P_DFECRECUPERO = $('#txtFecRecupero').val();
            model.P_NTIPORECUPERO = eval($('#cboTipoRecupero').val());
            model.P_STEMPORALIDAD = $('#txtTemporalidad').val();

            $.ajax({
                type: 'POST',
                url: '../../../Configuration/MantRecupero',
                contentType: 'application/json',
                data: JSON.stringify(model),
                success: function (data) {
                    if (data.entityList) {
                        alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'SISPOC' }).show();
                        LoadRecupero();
                        $('.popup').fadeOut(500);
                    }
                },
                error: function () {
                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se presentaron errores al momento de guardar el registro.', title: 'SISPOC' }).show();
                    console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
                    return false;
                }
            });
        },
        oncancel: function () {

        }
    }).show();
}

function CleanModal() {

    $('.popup .txtClean').each(function () {
        $(this).val('');
    });

}

function EditModal() {

    criterio = 2;
    $('#txtPoliza').css("width", "97%");
    $('#btnAccept').css("display", "inline-flex");

    //Open popup
    $('.popup').fadeIn(500);

    //Resize form register & edit user
    ResizePopup(null, 335);

    $('.title-span').text('Editar Mantenedor de Recupero');

    var objParametros = {
        P_NID_MANTRECUPERO: idMantenedor,
        P_SPOLIZA: "*",
        P_NPAGENUM: 0,
        P_NPAGESIZE: pagesize_t
    };

    $.ajax({
        url: '../../../Configuration/GetListaMantenedorRecupero',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var objData = data.entityListRecupero;

            if (objData != null) {

                $('#txtPoliza').attr("disabled", "disabled");
                HabilitarControles(true);

                //Resize popup
                ResizePopup(null, 335);

                //Hide filter
                $('#div_busq').css("display", "none");

                //Paint data in inputs
                $('#txtPoliza').val(objData[0].SPOLIZA);
                $('#txtMontoLiberacion').val(objData[0].NMONTOLIB);

                if (objData[0].SFECLIB == "01/01/0001")
                    $('#txtFecLiberacion').val("");
                else
                    $('#txtFecLiberacion').val(objData[0].SFECLIB);

                $('#txtRecupero').val(objData[0].NMONTOREC);

                if (objData[0].SFECREC == "01/01/0001")
                    $('#txtFecRecupero').val("");
                else
                    $('#txtFecRecupero').val(objData[0].SFECREC);

                $('#cboTipoRecupero').val(objData[0].NTIPOREC);
                $('#txtTemporalidad').val(objData[0].STEMP);
            }
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
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
    LoadRecupero();

}