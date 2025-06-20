var number = 0;

$(document).ready(function () {
    loadingReady();
});

function loadingReady() {

    //Time wait for presentation
    setInterval(function () {

        //Draw number
        if (number < 100) {
            number += 2;
            $('.counter').html(number + '%');
        }

        //Conditional for next page
        if (number === 100) {
            $.ajax({
                url: 'Security/Credentials',
                success: function (response) {
                    window.location.replace(response.objHome.MainUrl);
                    return;
                },
                error: function () {
                    console.log("No se ha podido obtener la información");
                }
            });
        }

    }, 100);

}