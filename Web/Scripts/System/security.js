
var flagCollapse = false;

$(document).ready(function () {
    ContentMain();
});

function ContentMain() {
    $.ajax({
        url: '../../../Security/MainContent',
        success: function (response) {

            //Add main
            $('.ul-main-gnr').append(response.objMain);
            debugger;
            //Main secondary
            $(".open-ul").on("click", function () {
                //localStorage.removeItem('idClienteEdit');

                ClearStorage();
                console.log('controlando el click');
                var idMenu = $(this).attr('id');
                var idActivo = $('.active-menu').attr('id');
                console.log('idmenu');
                console.log(idMenu);
                console.log('idactivo');
                console.log(idActivo);                                        

                $('.active-menu').each(function () {
                       $(this).removeClass('active-menu');
                });                                               

                if (idMenu == idActivo) {
                    $('.' + $(this).attr('id') + ' .li-ref-sec a').removeClass('open-li-sec');

                    //Cerrar menús terciarios
                    $('#' + $(this).attr('id') + '.li-ref-ter a').each(function () {
                        $(this).removeClass('open-li-ter');
                    });

                    $('#' + $(this).attr('id') + ' .img-arrow img').each(function () {
                       
                        $(this).removeClass('rotate');
                    });

                    $('#' + $(this).attr('id') + ' .img-arrow-sec img').each(function () {
                        $(this).removeClass('show-img');
                    });

                    $('#' + $(this).attr('id') + ' .img-arrow-sec img').each(function () {
                        $(this).removeClass('rotate-sec');
                    });

                    $('#' + $(this).attr('id') + ' .open-ul-ter').each(function () {
                        $(this).css('background', 'rgb(47, 64, 80)');
                    });

                    return;
                } else
                    $(this).addClass('active-menu');               

                if ($('.' + $(this).attr('id') + ' .li-ref-sec a').hasClass('.')) {

                    //Remover clase
                    $('.' + $(this).attr('id') + ' .li-ref-sec a').removeClass('open-li-sec');

                    //if (flagCollapse) $('.' + $(this).attr('id')).removeClass('ul-sec-main-colapse');

                    //Cerrar menús terciarios
                    $('.li-ref-ter a').each(function () {
                        $(this).removeClass('open-li-ter');
                    });

                    $(this).find('.img-arrow img').each(function () {
                        $(this).removeClass('rotate');
                    });

                    $('.img-arrow-sec img').each(function () {
                        $(this).removeClass('show-img');
                    });

                    $('.img-arrow-sec img').each(function () {
                        $(this).removeClass('rotate-sec');
                    });

                    $('.open-ul-ter').each(function () {
                        $(this).css('background', 'rgb(47, 64, 80)');
                    });

                } else {

                    localStorage.setItem("IDAsi", $(this).attr('alt'));
                    //localStorage.removeItem('idClienteEdit');
                    ClearStorage();
                    //Send a item main                    
                    var data = {
                        MainItem: $(this).attr('id'),
                        idPage: $(this).attr('alt')
                    };

                    $.ajax({
                        type: 'POST',
                        url: '../../../Security/MainItemSelect',
                        contentType: 'application/json',
                        data: JSON.stringify(data),
                        success: function (response) { },
                        error: function () {
                            console.log("No se ha podido obtener guardar la información");
                        }
                    });

                    //Cerrar menús secundarios
                    $('.li-ref-sec a').each(function () {
                        $(this).removeClass('open-li-sec');
                    });

                    //Cerrar menús terciarios
                    $('.li-ref-ter a').each(function () {
                        $(this).removeClass('open-li-ter');
                    });

                    //Eliminar selección
                    $('.open-ul').each(function () {
                        $(this).css('background', 'rgb(47, 64, 80)');
                        $(this).css('border-left', 'none');
                        $(this).css('color', '#a7b1c2');
                    });

                    $('.img-arrow img').each(function () {
                        $(this).removeClass('rotate');
                    });

                    $('.img-arrow-sec img').each(function () {
                        $(this).removeClass('show-img');
                        $(this).removeClass('rotate-sec');
                    });

                    $('.open-ul-ter').each(function () {
                        $(this).css('background', 'rgb(47, 64, 80)');
                    });

                    //Activar item seleccionado
                    $(this).css('background', 'rgb(41, 56, 70)');
                    $(this).css('border-left', '3px solid rgb(25, 158, 216)');
                    $(this).css('color', '#fff');
                    
                    //Mover flecha
                    $(this).find('.img-arrow img').addClass('rotate');
                    $(this).find('.img-arrow img').css('margin-right','28px');

                    //Mostrar nuevo menú
                    $('.' + $(this).attr('id') + ' .li-ref-sec a').addClass('open-li-sec');
                    $('.' + $(this).attr('id') + ' .li-ref-sec .img-arrow-sec img').addClass('show-img');

                    //Add css
                    if (!flagCollapse) $('.' + $(this).attr('id') + ' .li-ref-sec .open-li-sec').css('border-left', '3px solid rgb(25, 158, 216)');

                    if (flagCollapse)
                        $('.' + $(this).attr('id')).addClass('ul-sec-main-colapse');
                    else
                        $('.' + $(this).attr('id')).removeClass('ul-sec-main-colapse');
                }
            });

            //Main tertiary
            $(".open-ul-ter").on("click", function () {
                //localStorage.removeItem('idClienteEdit');
                ClearStorage();
                if ($('.' + $(this).attr('id') + ' .li-ref-ter a').hasClass('open-li-ter')) {

                    //Remover clases
                    $('.' + $(this).attr('id') + ' .li-ref-ter a').removeClass('open-li-ter');
                    $(this).find('.img-arrow-sec img').removeClass('rotate-sec');
                    $(this).css('background', 'rgb(47, 64, 80)');

                } else {

                    //Remover clases
                    $('.li-ref-ter a').each(function () {
                        $(this).removeClass('open-li-ter');
                    });

                    $('.img-arrow-sec img').each(function () {
                        $(this).removeClass('rotate-sec');
                    });

                    localStorage.setItem("IDAsi", $(this).attr('alt'));

                    //Send a item main                    
                    var data = {
                        MainItem: $(this).attr('id'),
                        idPage: $(this).attr('alt')
                    };

                    $.ajax({
                        type: 'POST',
                        url: '../../../Security/MainItemChildSelect',
                        contentType: 'application/json',
                        data: JSON.stringify(data),
                        success: function (response) {
                            $('.' + $(this).attr('id')).css('color','#00ff00');
                        },
                        error: function () {
                            console.log("No se ha podido obtener guardar la información");
                        }
                    });

                    $('.open-ul-ter').each(function () {
                        $(this).css('background', 'rgb(47, 64, 80)');
                    });

                    //Abrir sub-menu
                    $('.' + $(this).attr('id') + ' .li-ref-ter a').addClass('open-li-ter');

                    //Rotar flecha
                    $(this).find('.img-arrow-sec img').addClass('rotate-sec');

                    //Dar estilo al menú activo
                    $(this).css('background', 'rgba(0,0,0,.5)');

                }
            });

            $(".li-ref-ter").on("click", function () {              
                //localStorage.removeItem('idClienteEdit');
                ClearStorage();
                localStorage.setItem("IDAsi", $(this).attr('alt'));

                var data = {
                    MainItem: $(this).attr('id'),
                    idPage: $(this).attr('alt')
                };

                $.ajax({
                    type: 'POST',
                    url: '../../../Security/MainItemChildSelect',
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    success: function (response) { },
                    error: function () {
                        console.log("No se ha podido obtener guardar la información");
                    }
                });
            });

            //Function button collapse
            $('.toogle-main-img').on('click', function () {
                //localStorage.removeItem('idClienteEdit');
                ClearStorage();

                if (!flagCollapse) {
                    $('.content-global .aside-left').css('width', '70px');
                    $('.content-global .aside-left .profile-user').css('height', '10px');
                    $('.content-global .aside-left .main-system ul li a .textLi').css('color', 'rgba(0,0,0,0)');
                    $('.content-global .aside-left .main-system ul li a img').css('margin-left', '5px');
                    $('.content-global .aside-left .profile-user .user-info').css('display', 'none');
                    //$('.content-global .aside-left .main-system ul .ul-sec-main').addClass('collapse-main');
                    $('.li-ref-sec a').removeClass('open-li-sec');
                    $('.li-ref-ter a').removeClass('open-li-ter');              

                    //$('.content-global .aside-left .main-system ul .ul-ter-main li a').css('margin-left', '0');
                    $('.content-global .aside-left .main-system ul .ul-sec-main li a').css('border', 'none');
                    $('.content-global .content-bottom .made').css('margin-right', '62px');
                    //$('.content-global .content-bottom .made').css('transition-delay', '0.5s');
                    
                    
                   // $('.user-logo').css('display', 'none');
                    //$('.crecer-logo').css('display', 'inline-block');
                    
                    //$('.user-logo').fadeIn(500);
                    //$('.crecer-logo').fadeIn(500);
                    
                    //$('.user-logo').addClass(".tran-logo");
                    //$('.crecer-logo').css('display', 'inline-block');

                    //$('.user-logo').css('transition', 'opacity 600ms, visibility 600ms');
                    //$('.user-logo').css('visibility', 'hidden');
                    //$('.user-logo').css('opacity', '0');
                    //$('.crecer-logo').css('transition', 'opacity 600ms, visibility 600ms');
                    //$('.crecer-logo').css('visibility', 'visible');
                    //$('.crecer-logo').css('opacity', '1');
                

                    $('.crecer-logo').removeClass('hidden');
                    $('.crecer-logo').removeClass('box');
                    $('.user-logo').removeClass('box');
                    $('.user-logo').removeClass('hidden');

                    $('.crecer-logo').addClass('box');
                    $('.user-logo').addClass('hidden');


                   // $('.user-logo').css('transition', 'opacity 900ms, visibility 900ms');
                   // //$('.user-logo').css('display', 'none');
                   // $('.user-logo').css('opacity', '0');
                   // $('.crecer-logo').css('transition', 'opacity 900ms, visibility 900ms');
                   //// $('.crecer-logo').css('display', 'inline-block');
                   // $('.crecer-logo').css('opacity', '1');


                    $('.content-global .aside-left .main-system').css('min-height', '700px');
                    $('body').addClass('collapse');
                    
                    flagCollapse = true;
                } else {
                    $('.content-global .aside-left').css('width', '270px');
                    $('.content-global .aside-left .profile-user').css('height', '105px');
                    $('.content-global .aside-left .main-system ul li a .textLi').css('color', 'rgb(167, 177, 193)');
                    $('.content-global .aside-left .main-system ul li a img').css('margin-left', '0');
                    $('.content-global .aside-left .profile-user .user-info').fadeIn(500);
                    $('.content-global .aside-left .main-system ul .ul-sec-main').removeClass('collapse-main');
                    $('.content-global .aside-left .main-system ul .ul-sec-main li .open-li-sec').css('border-left', '3px solid rgb(25, 158, 216)');
                    //////$('.crecer-logo').css('display', 'none');
                    //////$('.user-logo').css('display', 'inline-block');
                    $('.content-global .aside-left .main-system').css('min-height', '573px');
                    $('.content-global .content-bottom .made').css('margin-right', '240px');
                    //$('.content-global .content-bottom .made').css('transition-delay', '0.5s');
                    //////$('.crecer-logo').addClass(".tran-logo");
                    //////$('.user-logo').css('display', 'inline-block');   

                    $('.crecer-logo').removeClass('hidden');
                    $('.crecer-logo').removeClass('box');
                    $('.user-logo').removeClass('box');
                    $('.user-logo').removeClass('hidden');


                    $('.crecer-logo').addClass('hidden');
                    $('.user-logo').addClass('box');
                    //$('.crecer-logo').css('transition', 'opacity 600ms, visibility 600ms');
                    //$('.crecer-logo').css('visibility', 'hidden');
                    //$('.crecer-logo').css('opacity', '0');
                    //$('.user-logo').css('transition', 'opacity 600ms, visibility 600ms');
                    //$('.user-logo').css('visibility', 'visible');
                    //$('.user-logo').css('opacity', '1');



                    

                    //$('.crecer-logo').css('display', 'none');
                    //$('.user-logo').fadeIn(500);
                    
                    flagCollapse = false;
                    $('body').removeClass('collapse');
                }

            });

            //Get value item selected of session
            $.ajax({
                url: '../../../Security/ReadMainItemSelected',
                success: function (response) {

                    if (response.objMainItem !== undefined && response.objMainItem != null && response.objMainItem != '') {

                        $('.open-ul-ter').each(function () {
                            $(this).css('background', 'rgb(47, 64, 80)');
                        });

                        var idItem = '#' + response.objMainItem;
                        var id = response.objMainItem;

                        //Activar item seleccionado
                        $(idItem).css('background', 'rgb(41, 56, 70)');
                        $(idItem).css('border-left', '3px solid rgb(25, 158, 216)');
                        $(idItem).css('color', '#fff');

                        //Mover flecha
                        $(idItem).find('.img-arrow img').addClass('rotate');
                        $(idItem).find('.img-arrow img').css('margin-right', '28px');

                        //Mostrar nuevo menú
                        $('.' + id + ' .li-ref-sec a').addClass('open-li-sec');
                        $('.' + id + ' .li-ref-sec .img-arrow-sec img').addClass('show-img');

                        //Add css
                        if (!flagCollapse) $('.' + id + ' .li-ref-sec .open-li-sec').css('border-left', '3px solid rgb(25, 158, 216)');

                        //Validate child selected
                        $.ajax({
                            url: '../../../Security/ReadMainItemChildSelected',
                            success: function (response) {

                                if (response.objMainChildItem !== undefined && response.objMainChildItem != null && response.objMainChildItem != '') {


                                    var idItem = '#' + response.objMainChildItem;
                                    var id = response.objMainChildItem;

                                    $('.open-ul-ter').each(function () {
                                        $(this).css('background', 'rgb(47, 64, 80)');
                                    });

                                    //Abrir sub-menu
                                    $('.' + id + ' .li-ref-ter a').addClass('open-li-ter');

                                    //Rotar flecha
                                    $(idItem).find('.img-arrow-sec img').addClass('rotate-sec');

                                    //Dar estilo al menú activo
                                    $(idItem).css('background', 'rgba(0,0,0,.5)');

                                }

                            },
                            error: function () {
                                console.log("No se ha podido obtener la información");
                            }
                        });
                    }

                },
                error: function () {
                    console.log("No se ha podido obtener la información");
                }
            });
        },
        error: function () {
            console.log("No se ha podido obtener la información");
        }
    });
}