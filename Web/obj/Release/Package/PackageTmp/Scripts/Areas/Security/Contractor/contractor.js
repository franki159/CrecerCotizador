this.pagenum = 0;
this.pagesize_t = 10;
var lstContractor;
var nidcontractor = null;
var flagEdit = false;
var showalert = true;
var scontractor = '';
var idNotAssigned = '';
var txtNotAssigned = '';
var side = 0;
var rowtotal = 0;
var objet = new Array();
var dataform = new FormData();



$(document).ready(function () {


});

function LoadUsers() {

    var objParametros = {
        P_DESCRIPTION: $('.txtDescription').val(),
        P_NPAGENUM: pagenum,
        P_NPAGESIZE: pagesize_t
    };

    $.ajax({
        url: '../../../Contractor/Index',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var JSonParameters = {
                NameTable: 'data-contractor',
                Entities: 'entity',
                Id: 'NIDCONTRACT'
            };

            var JSonHeader = {
                Column1: 'ID',
                Column2: 'Usuario',
                Column3: 'Perfil',
                Column4: 'Nombre',
                Column5: 'Dirección',
                Column6: 'Cargo',
                Column7: 'Fecha Creación',
                Column8: 'Activar'
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
                        "name": "Usuario",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "3",
                        "name": "Perfil",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "4",
                        "name": "Nombre",
                        "props": [
                            {
                                "visible": true
                            }
                        ]
                    },
                    {
                        "col": "5",
                        "name": "Dirección",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "6",
                        "name": "Cargo",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "7",
                        "name": "Fecha de creación",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                    {
                        "col": "8",
                        "name": "Activar",
                        "props": [
                            {
                                "visible": true,
                                "toggle": true
                            }
                        ]
                    }
                ]
            };
            console.log(data.entityUser);

            var initTable = {
                dataTable: data.entityUser,
                tableId: '.data-user',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: true,
                delButton: false,
                colSequence: true,
                pagesize: pagesize_t,
                callerId: 'LoadUsers',
                instanceId: 'lstUsers'
            }

            lstUsers = new renderTable(initTable);

            //Add function a toggle
            $('.toggle-grid').change(function () {

                //Variable for status
                var sstate;

                //Conditional checked
                if (this.checked) {
                    sstate = 1;
                } else {
                    sstate = 0;
                }

                //Get id of toggle
                var niduser = $(this).attr('id').toString().replace('toggle-', '');;

                //JSon for send
                var JSonSend = {
                    P_NIDUSER: niduser,
                    P_SSTATE: sstate
                }

                //Send and update profile
                $.ajax({
                    url: '../../../SecurityUser/UpdUserState',
                    contentType: "application/json",
                    data: JSonSend,
                    success: function (response) {
                        console.log('%cThe profile was updated correctly.', 'color:green');
                    },
                    error: function () {
                        console.log('%cError: Unable to update profile', 'color:red');
                    }
                });
            });

            //Add function a button edit row
            $('.btnRowEdit').on('click', function () {
                niduser = $(this).attr('id').replace('btn_e_row_', '');
                flagEdit = true;
                $('.btnOpenPopup').click();
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {

                //Get id profile
                niduser = $(this).attr('id').replace('btn_d_row_', '');

                //Modal for confirmation
                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'SIGMA',
                    labels: { ok: 'Ok', cancel: 'Cancelar' },
                    message: '¿Está seguro de eliminar el usuario seleccionado?',
                    onok: function () {

                        //Object for send
                        var JSonSend = {
                            P_NIDUSER: niduser
                        }

                        //Send and delete DelProfile
                        $.ajax({
                            url: '../../../SecurityUser/DelUser',
                            contentType: "application/json",
                            data: JSonSend,
                            success: function (response) {

                                //Get request answer
                                if (response.entityDelUser.P_MESSAGE != null) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: response.entityDelUser.P_MESSAGE, title: 'SIGMA' }).show();
                                } else {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se ha eliminado el usuario correctamente.', title: 'SIGMA' }).show();
                                    $('.btnSearch').click();
                                }

                                return true;
                            },
                            error: function () {
                                console.log('%cError: Unable to delete user', 'color:red');
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



function exportExt() {

    //Crear objeto de parametros para el filtro de la informacion anterior
    var objParametros = {
        P_DESCRIPTION: $('.txtDescription').val(),
        P_NPAGENUM: this.pagenum,
        P_NPAGESIZE: this.pagesize_t
    };

    window.location.href = '../Reports/SecurityUser?' + $.param(objParametros);
}