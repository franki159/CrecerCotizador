$(document).ready(function () {

    //Menu Secundario
    $(".open-ul").on("click", function () {
        
        if ($('.' + $(this).attr('id') + ' .li-ref-sec a').hasClass('open-li-sec')) {

            //Remover clase
            $('.' + $(this).attr('id') + ' .li-ref-sec a').removeClass('open-li-sec');

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
            $(this).css('background', 'rgba(0,0,0,.2)');
            $(this).css('border-left', '3px solid rgb(25, 158, 216)');
            $(this).css('color', '#fff');

            //Mover flecha
            $(this).find('.img-arrow img').addClass('rotate');

            //Mostrar nuevo menú
            $('.' + $(this).attr('id') + ' .li-ref-sec a').addClass('open-li-sec');
            $('.' + $(this).attr('id') + ' .li-ref-sec .img-arrow-sec img').addClass('show-img');
        }
    });

    //Menu Terciario
    $(".open-ul-ter").on("click", function () {

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

});
