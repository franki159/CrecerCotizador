//Opciones grid Cabecera
this.pagenum = 0;
this.pagesize_t = 10;
this.lstData;
this.idRowSelected = 0;
var rowtotal = 0;
var nTotal = 0;
var idParametro = 0;
var DFECINIREG = '';
var DFECFINREG = '';
var criterio = 1;

$(document).ready(function () {

    loadLista();

    $("#txtFecLimHijo").datepicker({
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

    $('.select-number-pagination').change(function () {
        pagesize = $(this).val();
        loadHeaderSpn();
    });

    $('#btnNuevo').on('click', function () {

        criterio = 1;
        $('.popup').fadeIn(500);
        $('.title-span').text('Nuevo Parámetro');
        $('#txtFactorAjus').focus();
        LimpiarModal();

        ResizePopup(null, 200);
    });

    $('#btnSearch').on('click', function () {
        idParametro = 0;
        loadLista();
    });

    $('#btnCancel').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('.btnClosePopup').on('click', function () {
        $('.popup').fadeOut(500);
    });

    $('#btnAccept').on('click', function () {
        MantParametro();
    });
});

function loadLista() {

    $("#overlay").fadeIn(300);

    var objParameters = {
        P_RANGE_INI: setFecha(DFECINIREG),
        P_RANGE_FIN: setFecha(DFECFINREG),
        P_IDPARAMETRO: idParametro,
        P_NPAGESIZE: pagesize_t,
        P_NPAGENUM: pagenum
    };

    $.ajax({
        url: '../../../Mantenimiento/GetListaParametro',
        contentType: 'application/json',
        data: objParameters,
        success: function (data) {

            var JSonHeader = {
                Column1: 'NID',
                Column2: 'IDPARAMETRO',
                Column3: 'NFACTORAJUST',
                Column4: 'ESTADO',
                Column5: 'NESTADO',
                Column6: 'DFECLIMHJS',
                Column7: 'NGASTOEMIS',
                Column8: 'NGASTOMANT',
                Column9: 'NFACTORSEG',
                Column10: 'NMARGSOLV',
                Column11: 'FECHA_REGISTRO',
                Column12: 'ROWNUMBER',
                Column13: 'ROWTOTAL'
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
                        "name": "Factor Ajustado",
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
                        "name": "Fecha Lim. Hijos",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "7",
                        "name": "Gasto Emisión",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "8",
                        "name": "Gasto Mant.",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "9",
                        "name": "Factor Seg.",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "10",
                        "name": "Margen Solv.",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "11",
                        "name": "Fecha Registro",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    }
                ]
            };

            var initTable = {
                dataTable: data.entityList,
                tableId: '.data-Parametro',
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
                $('.data-Parametro tbody tr').each(function (index, value) {
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
                idParametro = $(this).attr('id').replace('btn_e_row_', '');
                criterio = 2;

                EditModal();
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {
                idParametro = $(this).attr('id').replace('btn_d_row_', '');
                criterio = 3;

                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'COTIZADOR',
                    labels: { ok: 'Ok', cancel: 'Cancelar' },
                    message: '¿Está seguro de eliminar el registro seleccionado?',
                    onok: function () {

                        var model = new Object();

                        model.P_CRITERIO = criterio;
                        model.P_IDPARAMETRO = idParametro;

                        $.ajax({
                            type: 'POST',
                            url: '../../../Mantenimiento/MantParametro',
                            contentType: 'application/json',
                            data: JSON.stringify(model),
                            success: function (data) {

                                //Using function of library
                                if (data.entityList) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'COTIZADOR' }).show();
                                    $('.btnClosePopup').click();
                                    idParametro = 0;
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

            $("#overlay").fadeOut(300);
        },
        error: function () {
            $("#overlay").fadeOut(300);
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
    loadHeaderSpn();
}

function MantParametro() {

    if ($.trim($('#txtFactorAjus').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el factor ajuste de pensión.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtFecLimHijo').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar una fecha.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtGastoEmis').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el gasto de emisión.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtGastoMant').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el gasto de mantenimiento.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtFactorSeg').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el factor de seguridad.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtMargenSolv').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el margen de solvencia.', title: 'COTIZADOR' }).show();
        return false;
    }

    var vfactorajus = parseFloat(Number($('#txtFactorAjus').val().replace(/,/g, '')));
    var vgastoemi = parseFloat(Number($('#txtGastoEmis').val().replace(/,/g, '')));
    var vgastomant = parseFloat(Number($('#txtGastoMant').val().replace(/,/g, '')));
    var vfactoseg = parseFloat(Number($('#txtFactorSeg').val().replace(/,/g, '')));
    var vmargensol = parseFloat(Number($('#txtMargenSolv').val().replace(/,/g, '')));
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
            model.P_IDPARAMETRO = idParametro;
            model.P_FACTORAJUST = vfactorajus;
            model.P_FECLIMHJS = $('#txtFecLimHijo').val();
            model.P_GASTOEMIS = vgastoemi
            model.P_GASTOMANT = vgastomant;
            model.P_FACTORSEG = vfactoseg;
            model.P_MARGSOLV = vmargensol;

            $.ajax({
                type: 'POST',
                url: '../../../Mantenimiento/MantParametro',
                contentType: 'application/json',
                data: JSON.stringify(model),
                success: function (data) {
                    if (data.entityList) {
                        alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'COTIZADOR' }).show();
                        idParametro = 0;
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
    $('.title-span').text('Editar Parámetro');

    var objParametros = {
        P_RANGE_INI: setFecha(DFECINIREG),
        P_RANGE_FIN: setFecha(DFECFINREG),
        P_IDPARAMETRO: idParametro,
        P_NPAGESIZE: pagesize_t,
        P_NPAGENUM: 0
    };

    $.ajax({
        url: '../../../Mantenimiento/GetListaParametro',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var objData = data.entityList;

            if (objData != null) {

                //Paint data in inputs
                $('#txtFactorAjus').val(objData[0].NFACTORAJUST);

                if (objData[0].FECHA_LIM_HIJO == "01/01/0001")
                    $('#txtFecLimHijo').val('');
                else
                    $('#txtFecLimHijo').val(objData[0].FECHA_LIM_HIJO);

                $('#txtGastoEmis').val(objData[0].NGASTOEMIS);
                $('#txtGastoMant').val(objData[0].NGASTOMANT);
                $('#txtFactorSeg').val(objData[0].NFACTORSEG);
                $('#txtMargenSolv').val(objData[0].NMARGSOLV);
            }
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

function LimpiarModal() {
    $('#txtFactorAjus').val('');
    $('#txtFecLimHijo').val('');
    $('#txtGastoEmis').val('');
    $('#txtGastoMant').val('');
    $('#txtFactorSeg').val('');
    $('#txtMargenSolv').val('');
}