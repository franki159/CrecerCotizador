this.pagenum = 0;
this.pagesize_t = 10;
this.pagenum_nested = 0;
this.pagesize_t_nested = 10;
this.lstResources;
this.lstResource_Nested;
var nidresource = null;
var flagEdit = false;
var showalert = true;
var rowtotal = 0;
var nameNested = '';

$(document).ready(function () {

    //Load grid
    LoadResource();

    //Adjust popup
    ResizePopup(null, 420);

    //Evente keypress for description
    $('.txtDescription').keyup(function (event) {
        LoadResource();
    });

    //Event for pagination
    $('.select-number-pagination').change(function () {
        pagesize = $(this).val();
        LoadResource();
    });

    //Event open popup
    $('.btnOpenPopup').on('click', function () {
        $('.txtClean').val('');
        $('.popup').fadeIn(500);
        LoadResourceFather();
        LoadResourceImage();

        if (!flagEdit) {
            nidresource = null;
        } else {

            $('.title-span').text('Editar Módulo');

            //Get data for edit
            var objParametros = {
                P_NIDRESOURCE: nidresource
            }

            $.ajax({
                url: '../../../SecurityModule/GetResourceEdit',
                contentType: 'application/json',
                data: objParametros,
                success: function (data) {

                    var objData = data.entityResource;

                    if (objData != null) {

                        //Paint data in inputs
                        $('.txtPopupName').val(objData.SNAME);
                        $('.txtPopupDescription').val(objData.SDESCRIPTION);
                        $('.txtPopupUser').val(objData.SUSER);
                        $('.txtPopupDateRegister').val(objData.DFECREG);
                        $('.select-father').val(objData.NIDPARENT);
                        
                    }

                },
                error: function () {
                    console.log('%cError: The information could not be obtained.', 'color:red');
                }
            });

        }

    });

    //Event close popup
    $('.btnClosePopup').on('click', function () {
        $('.popup').fadeOut(500);
    });

    //Event for export data
    $('.btnExport').on('click', function () {
        exportExt();
    });

    //Event for button save
    $('.btnAccept').on('click', function () {

        if ($('.txtPopupName').val() === undefined || $('.txtPopupName').val() == null || $('.txtPopupName').val() == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el nombre del módulo.', title: 'SIGMA' }).show();
            return false;
        }

        if ($('.txtPopupDescription').val() === undefined || $('.txtPopupDescription').val() == null || $('.txtPopupDescription').val() == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la descripción del módulo.', title: 'SIGMA' }).show();
            return false;
        }

        if ($('.select-father').val() === undefined || $('.select-father').val() === null || $('.select-father').val() == '' || $('.select-father').val() == '0') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar el módulo padre.', title: 'SIGMA' }).show();
            return false;
        }

        var JSonData = {
            P_NIDRESOURCE: nidresource != null ? nidresource : 0,
            P_NIDPARENT: $('.select-father').val(),
            P_SNAME: $('.txtPopupName').val(),
            P_SDESCRIPTION: $('.txtPopupDescription').val(),
            P_SHTML: $('.selecteditem').attr('value-data') != 0 ? $('.selecteditem').attr('value-data') : 0,
            P_NUSERREG: 0
        }

        $.ajax({
            type: 'POST',
            url: '../../../SecurityModule/InsResources',
            contentType: 'application/json',
            data: JSON.stringify(JSonData),
            success: function (data) {
                if (!flagEdit) alertify.dialog('alert').set({ transition: 'zoom', message: 'Se registró el módulo correctamente.', title: 'SIGMA' }).show();
                else alertify.dialog('alert').set({ transition: 'zoom', message: 'Se modificó el módulo correctamente.', title: 'SIGMA' }).show();
                
                LoadResource();
                $('.btnClosePopup').click();
            },
            error: function () {
                alertify.dialog('alert').set({ transition: 'zoom', message: 'Se presentaron errores al momento de guardar al usuario.', title: 'SIGMA' }).show();
                console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
                return false;
            }
        });

    });

});

function LoadResource() {

    var objParametros = {
        P_DESCRIPTION: $('.txtDescription').val(),
        P_NPAGENUM: pagenum,
        P_NPAGESIZE: pagesize_t
    };

    $.ajax({
        url: '../../../SecurityModule/GetResources',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var JSonParameters = {
                NameTable: 'data-resources',
                Entities: 'entityResource',
                Id: 'NIDRESOURCE'
            };

            var JSonHeader = {
                Column1: 'ID',
                Column2: 'Módulo',
                Column3: 'Módulo Padre',
                Column4: 'Usuario Creación',
                Column5: 'Fecha Creación',
                Column6: 'Activar'
            };

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
                        "name": "Módulo",
                        "props": [
                           {
                               "visible": true
                           }
                        ]
                    },
                    {
                        "col": "3",
                        "name": "Módulo Padre",
                        "props": [
                           {
                               "visible": true
                           }
                        ]
                    },
                   {
                       "col": "4",
                       "name": "Usuario creación",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "5",
                       "name": "Fecha creación",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "6",
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

            var initTable = {
                dataTable: data.entityResource,
                tableId: '.data-resources',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: true,
                delButton: true,
                colSequence: false,
                pagesize: pagesize_t,
                callerId: 'LoadResource',
                instanceId: 'lstResources'
                //disableNested: true,
                //functionNested: LoadResourceNested
            }
         
            //AssembleGrid(JSonParameters, JSonHeader, data.entityResource, ParamsColumnsStyle, JSonPagination);
            lstResources = new renderTable(initTable);

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
                var nidresource = $(this).attr('id').toString().replace('toggle-', '');;

                //JSon for send
                var JSonSend = {
                    P_NIDRESOURCE: nidresource,
                    P_SSTATE: sstate
                }

                //Send and update profile
                $.ajax({
                    url: '../../../SecurityModule/UpdResourceState',
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
                nidresource = $(this).attr('id').replace('btn_e_row_', '');
                flagEdit = true;
                $('.btnOpenPopup').click();
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {

                nidresource = $(this).attr('id').replace('btn_d_row_', '');

                //Modal for confirmation
                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'SIGMA',
                    labels: { ok: 'Ok', cancel: 'Cancelar' },
                    message: '¿Está seguro de eliminar el módulo seleccionado?',
                    onok: function () {

                        //Object for send
                        var JSonSend = {
                            P_NIDRESOURCE: nidresource
                        }

                        //Send and delete DelProfile
                        $.ajax({
                            url: '../../../SecurityModule/DelResource',
                            contentType: "application/json",
                            data: JSonSend,
                            success: function (response) {

                                //Get request answer
                                if (response.entityDelResource.P_MESSAGE != null) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: response.entityDelResource.P_MESSAGE, title: 'SIGMA' }).show();
                                } else {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se ha eliminado el módulo correctamente.', title: 'SIGMA' }).show();
                                    LoadResource();
                                }

                                return true;
                            },
                            error: function () {
                                console.log('%cError: Unable to delete module', 'color:red');
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

function LoadResourceFather() {

    $.ajax({
        url: '../../../SecurityModule/GetResourcesFather',
        contentType: 'application/json',
        success: function (data) {

            var JSonData = data.entityResource;
            
            $(JSonData).each(function (index, value) {
                var option = value;
                var optionCode = "<option value='" + option.NIDPARENT + "'>" + option.SNAME + "</option>";
                $('.select-father').append(optionCode);
            });

        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
        

}

function LoadResourceImage() {

    var JSonParameter = {
        NameSelect: 'select-image'
    }

    $.ajax({
        url: '../../../SecurityModule/GetResourcesImage',
        contentType: 'application/json',
        success: function (data) {

            var JSonData = data.entityResource;
            addOption(JSonParameter, JSonData);

            //Add function option
            $('.option').on('click', function () {
                $('.selecteditem').attr('value-data', $(this).attr('value-data'));
                $('.selecteditem').find('span').text($(this).attr('alt'))
                $('.arrowcheck').click();
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

    window.location.href = '../Reports/SecurityModule?' + $.param(objParametros);
}

//Test 
/*
function LoadResourceNested() {

    var objParametros = {
        P_DESCRIPTION: $('.txtDescription').val(),
        P_NPAGENUM: pagenum_nested,
        P_NPAGESIZE: pagesize_t_nested
    };

    $.ajax({
        url: '../../../SecurityModule/GetResources',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var JSonParameters = {
                NameTable: 'data-resources',
                Entities: 'entityResource',
                Id: 'NIDRESOURCE'
            };

            var JSonHeader = {
                Column1: 'ID',
                Column2: 'Módulo',
                Column3: 'Módulo Padre',
                Column4: 'Usuario Creación',
                Column5: 'Fecha Creación',
                Column6: 'Activar'
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
                        "name": "Módulo",
                        "props": [
                           {
                               "visible": true
                           }
                        ]
                    },
                    {
                        "col": "3",
                        "name": "Módulo Padre",
                        "props": [
                           {
                               "visible": true
                           }
                        ]
                    },
                   {
                       "col": "4",
                       "name": "Usuario creación",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "5",
                       "name": "Fecha creación",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                   },
                   {
                       "col": "6",
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

            var initTable = {
                dataTable: data.entityResource,
                tableId: '.data-resources',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: true,
                delButton: true,
                colSequence: false,
                pagesize: pagesize_t_nested,
                callerId: 'LoadResourceNested',
                instanceId: 'lstResource_Nested',
                disableNested: false
            }

            //AssembleGrid(JSonParameters, JSonHeader, data.entityResource, ParamsColumnsStyle, JSonPagination);
            lstResource_Nested = new renderTable(initTable);

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
                var nidresource = $(this).attr('id').toString().replace('toggle-', '');;

                //JSon for send
                var JSonSend = {
                    P_NIDRESOURCE: nidresource,
                    P_SSTATE: sstate
                }

                //Send and update profile
                $.ajax({
                    url: '../../../SecurityModule/UpdResourceState',
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
                nidresource = $(this).attr('id').replace('btn_e_row_', '');
                flagEdit = true;
                $('.btnOpenPopup').click();
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {

                nidresource = $(this).attr('id').replace('btn_d_row_', '');

                //Modal for confirmation
                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'SIGMA',
                    labels: { ok: 'Ok', cancel: 'Cancelar' },
                    message: '¿Está seguro de eliminar el módulo seleccionado?',
                    onok: function () {

                        //Object for send
                        var JSonSend = {
                            P_NIDRESOURCE: nidresource
                        }

                        //Send and delete DelProfile
                        $.ajax({
                            url: '../../../SecurityModule/DelResource',
                            contentType: "application/json",
                            data: JSonSend,
                            success: function (response) {

                                //Get request answer
                                if (response.entityDelResource.P_MESSAGE != null) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: response.entityDelResource.P_MESSAGE, title: 'SIGMA' }).show();
                                } else {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se ha eliminado el módulo correctamente.', title: 'SIGMA' }).show();
                                    LoadResource();
                                }

                                return true;
                            },
                            error: function () {
                                console.log('%cError: Unable to delete module', 'color:red');
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


*/