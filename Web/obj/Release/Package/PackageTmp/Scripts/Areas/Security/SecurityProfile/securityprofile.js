this.pagenum = 0;
this.pagesize_t = 10;
this.lstProfiles;
var idprofile = null;
var flagEdit = false;
var showalert = true;
var rowtotal = 0;

$(document).ready(function () {

    //Load list of profiles since database
    LoadProfile();

    //Evente keypress for description
    $('.txtDescription').keyup(function (event) {
        LoadProfile();
    });

    //Event click for button
    $('.btnSearch').on('click', function () {
        LoadProfile();
    });

    //Event close popup
    $('.btnClosePopup').on('click', function () {
        $('.popup').fadeOut(500);
        flagEdit = false;
        idprofile = null;
        DestroyTreeView('tree-resources');
    });

    //Event open popup
    $('.btnOpenPopup').on('click', function () {

        //Open popup
        $('.popup').fadeIn(500);

        //Resize form register & edit user
        ResizePopup(null, 665);        

        //Set variables
        showalert = true;

        //Conditional edit row
        if (!flagEdit) {
            idprofile = null;
        } else {
            $('.title-span').text('Editar Perfil');
            // for example disbaled input 
            // $('.date').attr('disabled', 'disabled');
        }

        //Clean inputs
        CleanModal();

        //Call resources
        LoadResources();

        //Create function for button save
        $('.btnAccept').on('click', function () {

            if ($('.txtPopupName').val() === undefined || $('.txtPopupName').val() == null || $('.txtPopupName').val() == '') {
                alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar un nombre de perfil.', title: 'SIGMA' }).show();
                return false;
            }

            if ($('.txtPopupDescription').val() === undefined || $('.txtPopupDescription').val() == null || $('.txtPopupDescription').val() == '') {
                alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar una descripción para el perfil.', title: 'SIGMA' }).show();
                return false;
            }

            var acum = 0;
            var arrResource = [];

            $('.tree-resources input[type="checkbox"]').each(function () {

                if ((this).checked) {
                    var value = $(this).attr('id').replace('check-', '');
                    arrResource.push(value);
                    acum += 1;
                }

            });

            if (acum == 0) {
                alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar un recurso como mínimo.', title: 'SIGMA' }).show();
                return false;
            }

            var objParametros = {
                P_NIDPROFILE: idprofile == null ? 0 : idprofile,
                P_SNAME: $('.txtPopupName').val(),
                P_SDESCRIPTION: $('.txtPopupDescription').val(),
                P_NUSER: null,
                SCONSULTA: $('#chkConsulta').prop('checked') == true ?'1':'0' 
            }

            var JSonData = {
                objParametros: objParametros,
                lstResources: arrResource
            }

            if (showalert) {
                showalert = false;
                $.ajax({
                    type: 'POST',
                    url: '../../../SecurityProfile/InsProfile',
                    contentType: 'application/json',
                    data: JSON.stringify(JSonData),
                    success: function (data) {

                        //Using function of library
                        if (data.result) {
                            alertify.dialog('alert').set({ transition: 'zoom', message: 'Se guardo correctamente el perfil.', title: 'SIGMA' }).show();
                            $('.btnClosePopup').click();
                            $('.btnSearch').click();
                            return true;
                        } else {
                            alertify.dialog('alert').set({ transition: 'zoom', message: 'Se presentaron errores al momento de guardar el perfil.', title: 'SIGMA' }).show();
                            return false;
                        }

                    },
                    error: function () {
                        console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
                        return false;
                    }
                });
            }

        });

    });

    //Event for export data
    $('.btnExport').on('click', function () {
        exportExt();
    });

    //Event cancel
    $('.btnCancel').on('click', function () {
        $('.popup').fadeOut(500);
        flagEdit = false;
        idprofile = null;
        DestroyTreeView('tree-resources');
    });

    $('.select-number-pagination').change(function () {
        pagesize = $(this).val();
        LoadProfile();
    });

});

function LoadProfile() {

    var objParametros = {
        P_DESCRIPTION: $('.txtDescription').val(),
        P_NPAGENUM: pagenum,
        P_NPAGESIZE: pagesize_t
    };
  
    $.ajax({
        url: '../../../SecurityProfile/GetProfile',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var JSonParameters = {
                NameTable: 'data-profile',
                Entities: 'entityProfile',
                Id: 'NIDPROFILE'
            };

            var JSonHeader = {
                Column1: 'ID',
                Column2: 'Nombre',
                Column3: 'Descripción',
                Column4: 'Tipo',
                Column5: 'Usuario creación',
                Column6: 'Fecha creación',
                Column7: 'Usuario Act.',
                Column8: 'Fecha Act.',
                Column9: 'Activar'                
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
                        "name": "Nombre",
                        "props": [
                           {
                               "visible": true,
                           }
                        ]
                    },                 
                   {
                       "col": "3",
                       "name": "Descripción",
                       "props": [
                          {
                              "visible": true
                          }
                       ]
                    },
                    {
                        "col": "4",
                        "name": "Tipo",
                        "props": [
                            {
                                "visible": true,
                            }
                        ]
                    },
                   {
                       "col": "5",
                       "name": "Usuario creación",
                       "props": [
                          {
                              "visible": true,
                          }
                       ]
                   },
                   {
                       "col": "6",
                       "name": "Fecha creación",
                       "props": [
                          {
                              "visible": true,
                          }
                       ]
                   },
                   {
                       "col": "7",
                       "name": "Usuario Act.",
                       "props": [
                          {
                              "visible": true,
                          }
                       ]
                   },
                   {
                       "col": "8",
                       "name": "Fecha Act.",
                       "props": [
                          {
                              "visible": true,
                          }
                       ]
                   },
                   {
                       "col": "9",
                       "name": "Activar",
                       "props": [
                          {
                              "visible": true,
                              "toggle":true
                          }
                       ]
                   }
                ]
            };

            var initTable = {
                dataTable: data.entityProfile,
                tableId: '.data-profile',
                props: propsCol,
                tableHeader: JSonHeader,
                colCheckAll: false,
                editButton: true,
                delButton: true,
                colSequence: true,
                pagesize: pagesize_t,
                callerId: 'LoadProfile',
                instanceId: 'lstProfiles'
            }

            //AssembleGrid(JSonParameters, JSonHeader, data.entityProfile, ParamsColumnsStyle, JSonPagination);
            
            lstProfiles = new renderTable(initTable);
            ////Set value
            //rowtotal = JSonPagination.ROWTOTAL;

            //JSonPagination = {
            //    ROWTOTAL: rowtotal,
            //    ROWNUM: pagesize
            //}

            ////Create pagination
            //AssemblePagination(JSonPagination);

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
                var nidprofile = $(this).attr('id').toString().replace('toggle-','');;

                //JSon for send
                var JSonSend = {
                    P_NIDPROFILE: nidprofile,
                    P_SSTATE: sstate
                }

                //Send and update profile
                $.ajax({
                    url: '../../../SecurityProfile/UpdProfileState',
                    contentType: "application/json",
                    data: JSonSend,
                    success: function(response){
                        console.log('%cThe profile was updated correctly.', 'color:green');
                    },
                    error: function () {
                        console.log('%cError: Unable to update profile','color:red');
                    }
                });
            });

            //Add function a button edit row
            $('.btnRowEdit').on('click', function () {
                idprofile = $(this).attr('id').replace('btn_e_row_', '');                
                flagEdit = true;
                $('.btnOpenPopup').click();
            });

            //Add function a button delete row
            $('.btnRowDelete').on('click', function () {

                //Get id profile
                idprofile = $(this).attr('id').replace('btn_d_row_', '');

                //Modal for confirmation
                alertify.dialog('confirm').set({
                    transition: 'zoom',
                    title: 'SIGMA',
                    labels: {ok:'Ok', cancel:'Cancelar'},
                    message: '¿Está seguro de eliminar el perfil seleccionado?',
                    onok: function () {

                        //Object for send
                        var JSonSend = {
                            P_NIDPROFILE: idprofile
                        }

                        //Send and delete DelProfile
                        $.ajax({
                            url: '../../../SecurityProfile/DelProfile',
                            contentType: "application/json",
                            data: JSonSend,
                            success: function (response) {

                                //Get request answer
                                if (response.entityDelProfile.P_MESSAGE != null) {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: response.entityDelProfile.P_MESSAGE, title: 'SIGMA' }).show();
                                } else {
                                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se ha eliminado el perfil correctamente.', title: 'SIGMA' }).show();
                                    $('.btnSearch').click();
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
            console.log('%cError: The information could not be obtained.','color:red');
        }
    });
}

function LoadResources() {

    $.ajax({
        url: '../../../SecurityProfile/GetResourceProfile',
        contentType: 'application/json',
        success: function (data) {

            //Assemble treeview
            //inserta el contenido
            $('.tree-resources').append(AssembleTreeView(data.entityResourceProfile));

            if (flagEdit) {

                //Get value cells for input text
                var sname = $('.SNAME-' + idprofile).text();
                var sdescription = $('.SDESCRIPTION-' + idprofile).text();
                var suserregister = $('.SUSERREG-' + idprofile).text();
                var dfecregister = $('.DFECREG-' + idprofile).text();
                var sconsulta = $('.SCONSULTA-' + idprofile).text();

                $('.txtPopupName').val(sname);
                $('.txtPopupDescription').val(sdescription);
                $('.txtPopupUser').val(suserregister);
                $('#chkConsulta').prop('checked',sconsulta == 'CONSULTA' ? true : false);
                //Convert to string a to date
                var fromDate = dfecregister.split("/");

                //Get to date since fromDate
                $('.txtPopupDateRegister').val(fromDate[2].substring(0, 4) + '-' + fromDate[1] + '-' + fromDate[0]);

                //Conditional edit
                var idnode = {
                    P_NIDPROFILE: idprofile
                };

                //Compare resource since database
                $.ajax({
                    url: '../../../SecurityProfile/GetResourceProfileByID',
                    contentType: 'application/json',
                    data: idnode,
                    success: function (data) {

                        //Using function of library
                        CompareNodeInTreeView(data.entityResourcesProfiles, 'NIDRESOURCE');

                    },
                    error: function () {

                    }
                });

            } else {

                //Get date today
                var dteToday = new Date();
                var dteMonth = '00';
                var strToday = dteToday.getFullYear() + '-' + formatted_string('00',dteToday.getMonth() + 1,'l') + '-' + dteToday.getDate();

                $('.txtPopupDateRegister').val(strToday);

                //Get user current
                var suserregister = $('.session-username').text();

                $('.txtPopupUser').val(suserregister);

                //Function for action arrow
                $('.arrow').on('click', function () {
                    
                    var className = '.' + $(this).attr('id');

                    if($(className).is(":visible")){
                        console.log('Hide');
                        $(className).fadeOut(50);
                    } else {
                        console.log('Show');
                        $(className).fadeIn(50);
                    }

                });

            }

        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}

function CleanModal() {

    $('.popup .txtClean').each(function () {
        $(this).val('');
    });

}

function exportExt() {

    //Crear objeto de parametros para el filtro de la informacion anterior
    var objParametros = {
        P_DESCRIPTION: $('.txtDescription').val(),
        P_NPAGENUM: pagenum,
        P_NPAGESIZE: pagesize_t
    };

    window.location.href = '../Reports/SecurityProfile?' + $.param(objParametros);
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
    LoadProfile();

}