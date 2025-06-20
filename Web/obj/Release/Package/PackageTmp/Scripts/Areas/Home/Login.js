    var checkedRemember = false;

    $(document).ready(function () {
        checkedRemember = false;
    });

    function rememberUser() {
        if (!checkedRemember) {
            $('.box-checked').css('background', 'rgb(0, 176, 224)');
            checkedRemember = true;
        } else {
            $('.box-checked').css('background', '#fff');
            checkedRemember = false;
        }
    }

    function GetCredentials() {
        $.ajax({
            url: 'Home/Credentials',
            success: function (response) {
                window.location.replace(response.objHome.MainUrl);
            },
            error: function () {
                console.log("No se ha podido obtener la información");
            }
        });
    }
