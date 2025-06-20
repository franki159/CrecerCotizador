this.pagenum = 0;
this.pagesize_t = 10;
var lstUsers;
var niduser = null;
var flagEdit = false;
var showalert = true;
var suser = '';
var idNotAssigned = '';
var txtNotAssigned = '';
var side = 0;
var rowtotal = 0;
var objet =  new Array();
var dataform = new FormData();

$(document).ready(function () {
 

    //Load list of profiles since database ResizePopup(x,y)
    LoadUsers();

    //Evente keypress for description
    $('.txtDescription').keyup(function (event) {
        LoadUsers();
    });

    //Event click for button
    $('.btnSearch').on('click', function () {
        LoadUsers();
    });

    //Event close popup
    $('.btnClosePopup').on('click', function () {

        //Close popup
        $('.popup').fadeOut(500);
        flagEdit = false;
        niduser = null;

        //Hide filter
        $('.filter-show').fadeIn(200);

        //Show && Hide Containers
        $('.data-show').css('height', '0px');
        $('.profile-show').css('height', '0px');
        $('.banner-show').css('height', '280px');

        //Delete items
        $('.tbProfileNotAssigned').find('tr').each(function () {
            $(this).remove();
        });

        //Resize container
        $('.search-answer').css('height', '0px');
        $('.search-answer').css('padding', '0');

        //Clear input
        $('.txtUserSearchAD').val('');

        //Clear table assigned
        $('.tbProfileAssigned').find('tr').each(function () {
            $(this).remove();
        });
    });

    //Event open popup
    $('.btnOpenPopup').on('click', function () {

        //Open popup
        $('.popup').fadeIn(500);

        //Resize form register & edit user
        ResizePopup(845, 500);

        //Set variables
        showalert = true;

        //Clear input
        $('.clean').val('');
        $('.selectuser').val('0');

        //Conditional edit row
        if (!flagEdit) {

            //Reset variable
            niduser = null;

            //Hide filter
            $('.filter-show').fadeIn(200);

        } else {


            console.log('usuario edit');
            $('.title-span').text('Editar Usuario');
            
            //Get data for edit
            var objParametros = {
                P_NIDUSER: niduser
            }

            $.ajax({
                url: '../../../SecurityUser/GetUserEdit',
                contentType: 'application/json',
                data: objParametros,
                success: function (data) {

                    var objData = data.entityUser;

                    if (objData != null) {

                        //Resize popup
                        ResizePopup(845, 660);

                        //Hide filter
                        $('.filter-show').fadeOut(200);

                        //Show && Hide Containers
                        $('.data-show').css('height', '250px');
                        $('.profile-show').css('height', '280px');
                        $('.banner-show').css('height', '0px');
                        $('.banner-show').css('border', 'none');

                        //Paint data in inputs
                        $('.txtNameUser').val(objData.SNAME);
                        $('.txtLastname1').val(objData.SLASTNAME);
                        $('.txtLastname2').val(objData.SLASTNAME2);
                        $('.txtEmail').val(objData.SEMAIL);
                        $('.txtTelephone').val(objData.SPHONE1);
                        $('.txtAddress').val(objData.SADDRESS);
                        $('.txtPhone').val(objData.SPHONE1);
                        $('.selectCargo').val(parseInt(objData.NIDCHARGE));
                        $('.selectArea').val(parseInt(objData.NAREA));

                        suser = objData.SUSER;

                        LoadProfileAssigned();
                    }

                },
                error: function () {
                    console.log('%cError: The information could not be obtained.', 'color:red');
                }
            });

            //$('#preview_image1').attr('src', '../../../SecurityUser/GetUserFoto?id=' + niduser);

          

        }

        //Load profiles in table
        LoadProfileNotAssigned();

    });

    //Event for export data
    $('.btnExport').on('click', function () {
        exportExt();
    });

    //Event for searc users since AD
    $('.txtUserSearchAD').keyup(function () {

        if ($('.txtUserSearchAD').val() == '')
        {
            //Delete items
            $('.tr-ad').each(function () {
                $(this).remove();
            });

            //Resize container
            $('.search-answer').css('height', '0px');
            $('.search-answer').css('padding', '0');

            //Exit function
            return false;
        }

        var objParametros = {
            P_SUSER: $('.txtUserSearchAD').val()
        }

        $.ajax({
            url: '../../../SecurityUser/GetUserAD',
            contentType: 'application/json',
            data: objParametros,
            success: function (data) {

                var arrayData = data.entityUser;
                var codetable = '';

                if (arrayData.length > 0) {

                    //Delete items
                    $('.tr-ad').each(function () {
                        $(this).remove();
                    });

                    //Initialize container
                    $('.search-answer').css('height', '120px');
                    $('.search-answer').css('padding', ': 5px 0');

                    $.each(arrayData, function (index, value) {

                        //Received data json
                        var objData = value;

                        //Code dinamic for table of users AD
                        codetable = '<tr class="tr-ad"><td class="td-ad" id="' + objData.NIDUSER + '" user="' + objData.SUSER + '">' + objData.SNAME+ ' '+ objData.SLASTNAME + ' '+objData.SLASTNAME2 +'</td></tr>';

                        //Add tr a table
                        $('.tbUserAD').append(codetable);

                    });

                    $('.td-ad').on('click', function () {
                        //$(this).attr('id')
                        /*Search user*/
                        var user = $(this).attr('user');
                        console.log(arrayData);
                        var objData = arrayData.find(({SUSER}) => SUSER === user);
                            //data.entityUser;
                        console.log(objData);
                        if (objData != null) {
                            //Resize popup
                            ResizePopup(845, 660);

                            //Hide filter
                            $('.filter-show').fadeOut(200);

                            //Show && Hide Containers
                            $('.data-show').css('height', '250px');
                            $('.profile-show').css('height', '280px');
                            $('.banner-show').css('height', '0px');
                            $('.banner-show').css('border', 'none');

                            //Paint data in inputs
                            $('.txtNameUser').val(objData.SNAME);
                            $('.txtLastname1').val(objData.SLASTNAME);
                            $('.txtLastname2').val(objData.SLASTNAME2);
                            $('.txtEmail').val(objData.SEMAIL);
                            $('.txtTelephone').val(objData.SPHONE1);

                            suser = objData.SUSER;
                        }

                    });

                } else {

                    //Delete items
                    $('.tr-ad').each(function () {
                        $(this).remove();
                    });

                    //Resize container
                    $('.search-answer').css('height', '0px');
                    $('.search-answer').css('padding', '0');
                }

            },
            error: function () {
                console.log('%cError: The information could not be obtained.', 'color:red');
            }
        });


    });

    //Event for button cancel
    $('.btnCancel').on('click', function () {

        //Close popup
        $('.popup').fadeOut(500);
        flagEdit = false;
        niduser = null;

        //Show && Hide Containers
        $('.data-show').css('height', '0px');
        $('.profile-show').css('height', '0px');
        $('.banner-show').css('height', '280px');

        //Delete items
        //Delete items
        $('.tbProfileNotAssigned').find('tr').each(function () {
            $(this).remove();
        });

        //Resize container
        $('.search-answer').css('height', '0px');
        $('.search-answer').css('padding', '0');

        //Clear input
        $('.txtUserSearchAD').val('');

        $('.tbProfileAssigned').find('tr').each(function () {
            $(this).remove();
        });

    });

    //Event add data table assigned 
    $('.btnAssigned').on('click', function () {

        var exists = 0;

        //Validate list
        $('.tbProfileAssigned').find('tr').each(function () {
            exists += 1;
        });

        if (exists > 0) {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Ya ha asignado un perfil al usuario.', title: 'SIGMA' }).show();
            return false;
        }

        //Assemble name cell
        var idCell = '#profile-' + idNotAssigned;

        //Remove cell
        $(idCell).remove();

        //Add cell a table assigned
        var codetr = '<tr class="assigntr" onClick="GetIdProfileAssign(this)" id="assign-' + idNotAssigned + '"><td>' + txtNotAssigned + '<td></tr>';

        //Add a table
        $('.tbProfileAssigned').append(codetr);

        $('.notassigntr').hover(
              function () {
                  $(this).css('background', 'rgb(187, 211, 227)');
              },
              function () {
                  $(this).css('background', '#fff');
              }
        );

    });

    //Event add data table assigned 
    $('.btnNotAssigned').on('click', function () {

        //Assemble name cell
        var idCell = '#assign-' + idNotAssigned;

        //Remove cell
        $(idCell).remove();

        //Add cell a table assigned
        var codetr = '<tr class="notassigntr" id="profile-' + idNotAssigned + '"><td>' + txtNotAssigned + '<td></tr>';

        //Add a table
        $('.tbProfileNotAssigned').append(codetr);

        //Add function 
        $('#profile-' + idNotAssigned).on('click', function () {

            idNotAssigned = $(this).attr('id').replace('profile-', '');
            txtNotAssigned = $(this).find('td').html();

            var idhtml = '#profile-' + idNotAssigned;

            $('.notassigntr').each(function () {
                if ('#' + $(this).attr('id') == idhtml) {
                    $(idhtml).css('background', 'rgb(187, 211, 227)');
                } else {
                    $(this).css('background', '#fff');
                }
            });

        });

        $('.notassigntr').hover(
              function () {
                  $(this).css('background', 'rgb(187, 211, 227)');
              },
              function () {
                  $(this).css('background', '#fff');
              }
        );

    });

    //Pagination
    $('.select-number-pagination').change(function () {
        pagesize = $(this).val();
        LoadUsers();
    });

    //Event for button save
    $('.btnAccept').on('click', function () {

        if ($('.txtNameUser').val() === undefined || $('.txtNameUser').val() == null || $('.txtNameUser').val() == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el nombre del usuario.', title: 'SIGMA' }).show();
            return false;
        }

        if ($('.txtLastname1').val() === undefined || $('.txtLastname1').val() == null || $('.txtLastname1').val() == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el apellido paterno del usuario.', title: 'SIGMA' }).show();
            return false;
        }

        if ($('.txtLastname2').val() === undefined || $('.txtLastname2').val() == null || $('.txtLastname2').val() == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el apellido materno del usuario.', title: 'SIGMA' }).show();
            return false;
        }

        if ($('.txtAddress').val() === undefined || $('.txtAddress').val() == null || $('.txtAddress').val() == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar la dirección del usuario.', title: 'SIGMA' }).show();
            return false;
        }

        if ($('.txtEmail').val() === undefined || $('.txtEmail').val() == null || $('.txtEmail').val() == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el correo electrónico del usuario.', title: 'SIGMA' }).show();
            return false;
        }

        if ($('.txtPhone').val() === undefined || $('.txtPhone').val() == null || $('.txtPhone').val() == '') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe ingresar el teléfono del usuario.', title: 'SIGMA' }).show();
            return false;
        }

        if ($('.selectCargo').val() === undefined || $('.selectCargo').val() == null || $('.selectCargo').val() == '' || $('.selectCargo').val() == '0') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar el cargo del usuario.', title: 'SIGMA' }).show();
            return false;
        }

        if ($('.selectArea').val() === undefined || $('.selectArea').val() == null || $('.selectArea').val() == '' || $('.selectArea').val() == '0') {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe seleccionar el área del usuario.', title: 'SIGMA' }).show();
            return false;
        }

        var listprofileassigned = 0
        var idprofileuser = 0;

        $('.tbProfileAssigned').find('tr').each(function () {

            //Increment variable
            listprofileassigned += 1;

            //Get id profile
            idprofileuser = $(this).attr('id').replace('assign-', '');

        });
        
        if (listprofileassigned == 0) {
            alertify.dialog('alert').set({ transition: 'zoom', message: 'Debe asignar como minímo un perfil al usuario', title: 'SIGMA' }).show();
            return false;
        } else {

        }

        var JSonData = {
            P_NIDUSER: niduser != null ? niduser : 0,
            P_SUSER: suser,
            P_SSTATE: 1,
            P_NUSERREG: 0,
            P_SNAME: $('.txtNameUser').val(),
            P_SLASTNAME: $('.txtLastname1').val(),
            P_SLASTNAME2: $('.txtLastname2').val(),
            P_SSEX: $('.gender').val() == 1 ? 1 : 2,
            P_SADDRESS: $('.txtAddress').val(),
            P_SEMAIL: $('.txtEmail').val(),
            P_SPHONE1: $('.txtPhone').val(),
            P_NIDPROFILE: idprofileuser,
            P_NAREA: $('.selectArea').val(),
            P_NIDCHARGE: $('.selectCargo').val()
        }


        var url = "";
        url = '';
      /*  dataform.append('P_NIDUSER', JSonData.P_NIDUSER);
        dataform.append('P_SUSER', JSonData.P_SUSER);
        dataform.append('P_SSTATE', JSonData.P_SSTATE);
        dataform.append('P_NUSERREG', JSonData.P_NUSERREG);
        dataform.append('P_SNAME', JSonData.P_SNAME);
        dataform.append('P_SLASTNAME', JSonData.P_SLASTNAME);
        dataform.append('P_SLASTNAME2', JSonData.P_SLASTNAME2);
        dataform.append('P_SSEX', JSonData.P_SSEX);        
        dataform.append('P_SADDRESS', JSonData.P_SADDRESS);
        dataform.append('P_SEMAIL', JSonData.P_SEMAIL);
        dataform.append('P_SPHONE1', JSonData.P_SPHONE1);
        dataform.append('P_NIDPROFILE', JSonData.P_NIDPROFILE);
        dataform.append('P_NAREA', JSonData.P_NAREA);
        dataform.append('P_NIDCHARGE', JSonData.P_NIDCHARGE);   
        dataform.append('FilesToUpload1', objet[0].data.files[0]);
        console.log(dataform);
        console.log(objet);
        //data.append('FilesToUpload2', objet[1].data.files[0]);
        debugger;
        $.ajax({
            url: '../../../SecurityUser/InsUser',
            type: 'POST',
            contentType: false,
            data: dataform,
            processData: false,
            cache: false,
            async: true,
            success: function (response) {
                alert('ok');
            },
            failure: function (msg) {
                alert(msg);
            },
            error: function (xhr, status, error) {
                alert(error);
            }
        });
        */

       $.ajax({
            type: 'POST',
            url: '../../../SecurityUser/InsUser',
            contentType: 'application/json',
            data: JSON.stringify(JSonData),
            success: function (data) {

                //Using function of library
                if (data.result) {

                    //Message of confirmation
                    if (flagEdit) alertify.dialog('alert').set({ transition: 'zoom', message: 'Se modificó correctamente el usuario.', title: 'SIGMA' }).show();
                    else alertify.dialog('alert').set({ transition: 'zoom', message: 'Se guardo correctamente el usuario.', title: 'SIGMA' }).show();
                    
                    //Destroy popup
                    $('.btnClosePopup').click();
                    $('.btnSearch').click();

                    //Reload users
                    LoadUsers();

                } else {
                    alertify.dialog('alert').set({ transition: 'zoom', message: 'Se presentaron errores al momento de guardar al usuario.', title: 'SIGMA' }).show();
                    return false;
                }

            },
            error: function () {
                alertify.dialog('alert').set({ transition: 'zoom', message: 'Se presentaron errores al momento de guardar al usuario.', title: 'SIGMA' }).show();
                console.log('%cÉrror: Could not establish a connection with the controller.', 'color:red');
                return false;
            }
        });

    });

    //Load charges && areas
    loadSelect('selectCargo', 'L001');    
    loadAreas();

});

function LoadUsers() {

    var objParametros = {
        P_DESCRIPTION: $('.txtDescription').val(),
        P_NPAGENUM: pagenum,
        P_NPAGESIZE: pagesize_t
    };

    $.ajax({
        url: '../../../SecurityUser/GetUser',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var JSonParameters = {
                NameTable: 'data-user',
                Entities: 'entityUser',
                Id: 'NIDUSER'
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

function GetIdProfile(row) {

    idNotAssigned = $(row).attr('id').replace('profile-', '');
    txtNotAssigned = $(row).find('td').html();

    var idhtml = '#profile-' + idNotAssigned;
    
    $('.notassigntr').each(function () {

        if ('#' + $(this).attr('id') == idhtml) {
            $(idhtml).css('background', 'rgb(187, 211, 227)');
        } else {
            $(this).css('background', '#fff');
        }
    });

}

function GetIdProfileAssign(row) {

    idNotAssigned = $(row).attr('id').replace('assign-', '');
    txtNotAssigned = $(row).find('td').html();

    var idhtml = '#assign-' + idNotAssigned;

    $('.assigntr').each(function () {

        if ('#' + $(this).attr('id') == idhtml) {
            $(idhtml).css('background', 'rgb(187, 211, 227)');
        } else {
            $(this).css('background', '#fff');
        }
    });

}

function LoadProfileNotAssigned() {

    var JSonSend = {
        P_NIDUSER: niduser
    };

    $.ajax({
        url: '../../../SecurityUser/GetProfileUserNotAssigned',
        contentType: "application/json",
        data: JSonSend,
        success: function (response) {
            
            var JSonProperties = {
                NameTable: 'tbProfileNotAssigned',
                FunctionAssigned: 'GetIdProfile',
                ClassForId: 'profile',
                ClassAssignedSN: 'notassigntr'
            };

            var JSonInputs = {
                id: 'NIDPROFILE',
                label: 'SNAME'
            };

            //Send table.js
            AssembleTable(response.entityProfile, JSonProperties, JSonInputs);

        },
        error: function () {
            console.log('%cError: Unable to update profile', 'color:red');
        }
    });
}

function LoadProfileAssigned() {

    var JSonSend = {
        P_NIDUSER: niduser
    };

    $.ajax({
        url: '../../../SecurityUser/GetProfileUserAssigned',
        contentType: "application/json",
        data: JSonSend,
        success: function (response) {

            var JSonProperties = {
                NameTable: 'tbProfileAssigned',
                FunctionAssigned: 'GetIdProfileAssign',
                ClassForId: 'assign',
                ClassAssignedSN: 'assigntr'
            };

            var JSonInputs = {
                id: 'NIDPROFILE',
                label: 'SNAME'
            };

            //Send table.js
            AssembleTable(response.entityProfile, JSonProperties, JSonInputs);
        },
        error: function () {
            console.log('%cError: Unable to update profile', 'color:red');
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
    LoadUsers();

}

function expand(element) {

    //Get id element select
    var id = $(element).attr('id');

    //Remove class
    $('.sections').each(function () {
        $(this).addClass('section-collapse');
    });

    $('.arrow-expand').find('img').each(function () {
        $(this).removeClass('rotate');
    });

    //Question class exists
    if (!$('.' + id).hasClass('true')) {

        //Add new class
        $(element).find('img').addClass('rotate');
        $('.' + id).removeClass('section-collapse');
        $('.' + id).addClass('true')

    } else {
        $('.' + id).removeClass('true')
    }

}


function loadAreas() {
    var JSonSend = {
        PNIDCONFASI: null
    }

    var rselec = 0;
    $.ajax({
        url: '../../../SecurityUser/getAreas',
        contentType: 'application/json',
        data: JSonSend,
        success: function (data) {            
            $.each(data.eAreas, function (key, value) {
                $(".selectArea").append("<option value=" + value.NIDAREA + ">" + value.DESCAREA + "</option>");                
            });            
        },
        error: function () {
            console.log('%cError: No se pudo obtener los datos de las areas.', 'color:red');
        }
    });
}




$("#FilesToUpload1").change("change", function () {
    debugger;
    readURL(this, "#preview_image1");

    var filename = $("#FilesToUpload1").val().split("\\");

    var rules = new Object();
    rules.field = filename[2];
    rules.data = this;
    objet.push(rules);
    if (estado1.length == 3) {
        for (var i = objet.length - 1; i >= 0; i--) {
            if (estado1[2] == objet[i].field) {
                objet.splice(i, 1);
            }
        };
    }
});


function readURL(input, target) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        var image_target = $(target);
        reader.onload = function (e) {
            image_target.attr('src', e.target.result).show();
        };
        reader.readAsDataURL(input.files[0]);
    }
}