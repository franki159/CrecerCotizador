//Opciones grid Cabecera
this.pagenum = 0;
this.pagesize_t = 10;
this.lstData;
this.idRowSelected = 0;
var rowtotal = 0;
var nTotal = 0;
var idSepelio = 0;
var DFECINIREG = '';
var DFECFINREG = '';
var criterio = 1;

$(document).ready(function () {

    loadLista();
    
    $("#txtFecCierre").datepicker({
        language: "es", locale: "es", dateFormat: 'dd/mm/yy', changeYear: true
    });
    
    $('#txtFecCierre').on('changeDate', function (ev) {
        $(this).datepicker('hide');
        var valor = $('#cboTipo').val();
        MostrarMeses(valor);
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
        $('.title-span').text('Nuevo Sepelio');
        $('#txtFecCierre').removeAttr("disabled");
        $('#txtFecCierre').focus();
        LimpiarModal();

        ResizePopup(null, 200);
    });

    $('#btnSearch').on('click', function () {
        idSepelio = 0;
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

    $('#cboTipo').on('change', function () {

        var valor = this.value;
        MostrarMeses(valor);
    });
});

function loadLista() {

    $("#overlay").fadeIn(300);

    var objParameters = {
        P_RANGE_INI: setFecha(DFECINIREG),
        P_RANGE_FIN: setFecha(DFECFINREG),
        P_IDSEPELIO: idSepelio,
        P_NPAGESIZE: pagesize_t,
        P_NPAGENUM: pagenum
    };

    $.ajax({
        url: '../../../Mantenimiento/GetListaSepelio',
        contentType: 'application/json',
        data: objParameters,
        success: function (data) {

            var JSonHeader = {
                Column1: 'NID',
                Column2: 'IDSEPELIO',
                Column3: 'FECHACIERRE',
                Column4: 'NMONTO',
                Column5: 'ESTADO',
                Column6: 'NESTADO',
                Column7: 'ROWNUMBER',
                Column8: 'ROWTOTAL'
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
                        "name": "Fecha Cierre",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "4",
                        "name": "Monto",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "5",
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
                        "col": "6",
                        "name": "Nestado",
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
                tableId: '.data-Sepelio',
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
                $('.data-Sepelio tbody tr').each(function (index, value) {
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
                idSepelio = $(this).attr('id').replace('btn_e_row_', '');
                criterio = 2;

                EditModal();
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {
                idSepelio = $(this).attr('id').replace('btn_d_row_', '');
                criterio = 3;

                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'COTIZADOR',
                    labels: { ok: 'Ok', cancel: 'Cancelar' },
                    message: '¿Está seguro de eliminar el registro seleccionado?',
                    onok: function () {

                        var model = new Object();

                        model.P_CRITERIO = criterio;
                        model.P_IDSEPELIO = idSepelio;

                        $.ajax({
                            type: 'POST',
                            url: '../../../Mantenimiento/MantSepelio',
                            contentType: 'application/json',
                            data: JSON.stringify(model),
                            success: function (data) {

                                //Using function of library
                                if (data.entityList) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'COTIZADOR' }).show();
                                    $('.btnClosePopup').click();
                                    idSepelio = 0;
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

function MantSepelio() {

    if ($.trim($('#txtFecCierre').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar fecha de cierre.', title: 'COTIZADOR' }).show();
        return false;
    }

    if ($.trim($('#txtMonto').val()) == '') {
        alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el monto.', title: 'COTIZADOR' }).show();
        return false;
    }

    var vMonto = parseFloat(Number($('#txtMonto').val().replace(/,/g, '')));
    var vTipo = $('#cboTipo').val();
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
            model.P_IDSEPELIO = idSepelio;
            model.P_DFECCIERRE = $('#txtFecCierre').val();
            model.P_NMONTO = vMonto;
            model.P_TIPO = vTipo;

            $.ajax({
                type: 'POST',
                url: '../../../Mantenimiento/MantSepelio',
                contentType: 'application/json',
                data: JSON.stringify(model),
                success: function (data) {

                    if (data.valida == 1) {

                        var msjfecha = "";
                        for (var i = 0; i < data.entityList.length; i++) {
                            msjfecha = msjfecha + data.entityList[i].FECHACIERRE + ' - ';
                        }

                        msjfecha = "No se puede realizacion la acción<br />Lista de fechas ingresadas que ya existen en el sistema:<br />" +  msjfecha.substr(0, msjfecha.length - 2);
                        alertify.dialog('alert').set({ transition: 'zoom', message: msjfecha, title: 'COTIZADOR' }).show();
                    } else {
                        if (data.entityList) {
                            alertify.dialog('alert').set({ transition: 'zoom', message: data.entityList[0].MENSAJE, title: 'COTIZADOR' }).show();
                            idSepelio = 0;
                            loadLista();
                            $('.popup').fadeOut(500);
                        }
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
    $('#div_tipo').css("display", "none");
    $('#div_meses').css("display", "none");
    $('#txtFecCierre').attr("disabled", "disabled");
    $('.title-span').text('Editar Sepelio');

    var objParametros = {
        P_RANGE_INI: setFecha(DFECINIREG),
        P_RANGE_FIN: setFecha(DFECFINREG),
        P_IDSEPELIO: idSepelio,
        P_NPAGESIZE: -1,
        P_NPAGENUM: 0
    };

    $.ajax({
        url: '../../../Mantenimiento/GetListaSepelio',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var objData = data.entityList;

            if (objData != null) {

                //Paint data in inputs
                $('#txtMonto').val(objData[0].NMONTO);

                if (objData[0].FECHACIERRE == "01/01/0001")
                    $('#txtFecCierre').val('');
                else
                    $('#txtFecCierre').val(objData[0].FECHACIERRE);
            }
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

function LimpiarModal() {
    $('#txtFecCierre').val('');
    $('#txtMonto').val('');
    $('#cboTipo').val('1');
    $('#div_tipo').css("display", "block");
}

function MostrarMeses(valor) {
    if (valor == "2") {

        $('#div_meses').css("display", "block");

        if ($.trim($('#txtFecCierre').val()) == '') {
            $('.lista-det-mes tbody').empty();
            return false;
        }

        var objParametros = {
            fecha: setFecha($('#txtFecCierre').val())
        };

        $.ajax({
            url: '../../../Mantenimiento/GetFechaTrimestral',
            contentType: 'application/json',
            data: objParametros,
            beforeSend: function () {
                $('.lista-det-mes tbody').empty();
            },
            success: function (data) {

                var html = "";
                for (var i = 0; i < data.entityList.length; i++) {
                    html = "<tr><td style='border: 1px solid silver'>" + data.entityList[i].FECHACIERRE + "</td></tr>";
                    $('.lista-det-mes tbody').append(html);
                }
            },
            error: function () {
                console.log('%cError: The information could not be obtained.', 'color:red');
            }
        });
    } else {
        $('#div_meses').css("display", "none");
    }
}