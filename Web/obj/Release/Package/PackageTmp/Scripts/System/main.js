$(document).ready(function () {
    $('.content-global').addClass('income-animation');

    //Show time & date
    appTimer();

});

function appTimer() {
    setInterval(function () {
        var fecha = new Date();
        //var options = { year: 'numeric', month: 'long', day: 'numeric' };        
        var options = { year: 'string', month: 'string', day: 'string' };        

        var dt = new Date();
        var mm = dt.getMonth() + 1;
        var dd = dt.getDate();
        var yyyy = dt.getFullYear();
        var format = dd + '/' + pad(mm,'2') + '/' + yyyy;
        

        //var fechastring = fecha.toLocaleDateString("es-ES", options);
        var strDate = (new Date()).toLocaleDateString();
        var strTime = (new Date()).toLocaleTimeString();
        $('#dateApp').html(format); //fecha.toLocaleDateString("es-ES", options)); 
        $('#timeApp').html(strTime);
        $('#lblChartDate').html('Al ' + strDate);
    }, 1000);
};

function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
};

function setFecha(vFecha ){
    
    var fechaSeteada = '';
    if (vFecha === undefined || vFecha === null || vFecha === '') {
        return null;
    }
    fechaSeteada = vFecha.toString().substr(6, 4) + '-' + vFecha.toString().substr(3, 2) + '-' + vFecha.toString().substr(0, 2);

    return fechaSeteada;
};


function setFechaFormat(vFecha, format = "-") {

    var fechaSeteada = '';
    if (vFecha === undefined || vFecha === null || vFecha === '') {
        return null;
    }
    //fechaSeteada = vFecha.toString().substr(6, 4) + '-' + vFecha.toString().substr(3, 2) + '-' + vFecha.toString().substr(0, 2);
    fechaSeteada = vFecha.toString().substr(0, 2) + format + vFecha.toString().substr(3, 2) + format + vFecha.toString().substr(6, 4) ;

    return fechaSeteada;
};

function ClearStorage() {
    console.log('ClearStorage');
    localStorage.removeItem('idClienteEdit');
    localStorage.removeItem('viewcontratantante');
}

$('.correo').on('keyup', function () {
    return validateEmail();
});


//$(".letra").on('keypress', function (event) { var regex = new RegExp("^[a-zA-Z ]+$"); var key = String.fromCharCode(!event.charCode ? event.which : event.charCode); if (!regex.test(key)) { event.preventDefault(); return false; } });

//RQ-CAU1
function TienePolizasEjecutadas(nrosolicitud, nidcliente, ntipodoc, snrodoc) {

    var objParametros = {
        PNID_CLIENTE: nidcliente,
        PNNUMERO_SOLICITUD: nrosolicitud,
        PNTIPO_DOCUMENTO: ntipodoc,
        PSNUMERO_DOCUMENTO: snrodoc
    };
    var vReturn = false;
    $.ajax({
        url: '../../../Tools/TienePolizasEjecutadas',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {
            var entidad = data.entityList[0];
            if (entidad.NCANTIDAD_PE > 0)
                vReturn = true;
        },
        async: false,
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });

    return vReturn;
}