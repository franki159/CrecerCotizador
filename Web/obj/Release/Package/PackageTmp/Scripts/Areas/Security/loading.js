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

        var uri = window.location.href;
        if (uri.indexOf('Security/Process') !== -1) {
            window.location.replace(uri.replace("Security/Process", ""));
        }


        if (number === 100) {
            $.ajax({
                url: 'Security/Credentials',
                success: function (response) {
                    
                    localStorage.removeItem('areauser');
                    localStorage.setItem('areauser', response.objHome.NArea)
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