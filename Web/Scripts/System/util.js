
$(".numero").keypress(

    function (e) {

        var charTyped = String.fromCharCode(e.which);
        var letterRegex = /[^0-9]/;

        if (charTyped.match(letterRegex)) {
            return false;
        }
        else {
            return true;
        }
    });

$(".letras").on("keypress", function (event) {
     
    var englishAlphabetAndWhiteSpace = /[A-Za-záéíóúÁÉÍÓÚ ]/g;
    var key = String.fromCharCode(event.which);
    console.log(event.keyCode);
    if (event.keyCode == 8 || event.keyCode == 37 || event.keyCode == 39 || englishAlphabetAndWhiteSpace.test(key)) {
        return true;
    }
    return false;
});

$('.numero').on("paste", function (e) {
    //  event.preventDefault();
    //if (event.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
    //    event.preventDefault();
    //}
});


$('.monto').keypress(function (event) {
    var $this = $(this);

    if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
    var text = $(this).val();
    if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {
            if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
            }
        }, 1);
    }
    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8) &&
        ($(this)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }
});

$('.monto').on('keyup', function (event) {

    if (this.value.length > 3) {
        this.value = this.value.replace(/,/g, '');
        var decimal = '';

        if (this.value.indexOf('.') > 0) {
            decimal = this.value.substr(this.value.indexOf('.'), 3);
            this.value = this.value.substring(0, this.value.indexOf('.'))
        }

        var longitud = this.value.length;
        var nuevoMonto = '';
        var contador = 1;
        var montoFinal = '';

        for (var i = longitud; i > 0; i--) {
            var dig = this.value.substr(i - 1, 1)
            if (contador == 3) {
                if (i == 1)
                    nuevoMonto = + dig + '' + nuevoMonto;
                else
                    nuevoMonto = ',' + dig + '' + nuevoMonto;

                contador = 1;
            }
            else {
                nuevoMonto = dig + '' + nuevoMonto;
                contador++;
            }

            console.log('digito' + dig)
            console.log('contador' + contador)
            console.log('nuevomonto' + nuevoMonto)

        }

        this.value = nuevoMonto + '' + decimal;
        this.focus();
    }
});

$('.monto_3').keypress(function (event) {
    var $this = $(this);

    if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
    var text = $(this).val();
    if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {
            if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
            }
        }, 1);
    }
    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 3) &&
        (event.which != 0 && event.which != 8) &&
        ($(this)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }
});

$('.monto_3').on('keyup', function (event) {

    if (this.value.length > 3) {
        this.value = this.value.replace(/,/g, '');
        var decimal = '';

        if (this.value.indexOf('.') > 0) {
            decimal = this.value.substr(this.value.indexOf('.'), 4);
            this.value = this.value.substring(0, this.value.indexOf('.'))
        }

        console.log("valor: " + this.value);
        console.log("decimales: " + decimal);

        var longitud = this.value.length;
        var nuevoMonto = '';
        var contador = 1;

        console.log("longitud: " + longitud)

        for (var i = longitud; i > 0; i--) {
            var dig = this.value.substr(i - 1, 1)
            if (contador == 3) {
                if (i == 1)
                    nuevoMonto = + dig + '' + nuevoMonto;
                else
                    nuevoMonto = ',' + dig + '' + nuevoMonto;

                contador = 1;
            }
            else {
                nuevoMonto = dig + '' + nuevoMonto;
                contador++;
            }

            console.log('digito' + dig)
            console.log('contador' + contador)
            console.log('nuevomonto' + nuevoMonto)
        }

        this.value = nuevoMonto + '' + decimal;
        this.focus();
    }
});

$('.monto_5').keypress(function (event) {
    var $this = $(this);

    if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }

    var text = $(this).val();

    if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {
            if ($this.val().substring($this.val().indexOf('.')).length > 5) {
                $this.val($this.val().substring(0, $this.val().indexOf('.') + 5));
            }
        }, 1);
    }

    if (text.length == 1) {
        if (event.which != 46) {
            event.preventDefault();
        }
    }

    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 5) &&
        (event.which != 0 && event.which != 8) &&
        ($(this)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }
});

$('.monto_5').on('keyup', function (event) {

    if (this.value.length > 0) {
        this.value = this.value.replace(/,/g, '');
        var decimal = '';

        if (this.value.indexOf('.') > 0) {
            decimal = this.value.substr(this.value.indexOf('.'), 6);
            this.value = this.value.substring(0, this.value.indexOf('.'))
        }

        console.log("valor: " + this.value);
        console.log("decimales: " + decimal);

        var longitud = this.value.length;
        var nuevoMonto = '';
        var contador = 1;

        console.log("longitud: " + longitud)

        for (var i = longitud; i > 0; i--) {
            var dig = this.value.substr(i - 1, 1)
            if (contador == 3) {
                if (i == 1)
                    nuevoMonto = + dig + '' + nuevoMonto;
                else
                    nuevoMonto = ',' + dig + '' + nuevoMonto;

                contador = 1;
            }
            else {
                nuevoMonto = dig + '' + nuevoMonto;
                contador++;
            }

            console.log('digito' + dig)
            console.log('contador' + contador)
            console.log('nuevomonto' + nuevoMonto)
        }

        this.value = nuevoMonto + '' + decimal;
        this.focus();
    }
});


//porcentaje Menor
/********************************************************/

$('.numeroTwo').on("paste", function (e) {
     
    //  event.preventDefault();
    //if (event.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
    //    event.preventDefault();
    //}

    var _this = $(this);
    setTimeout(function () {
        // access element value
        var value_input = _this.val();
        if (parseFloat(value_input) > 100) {
            e.preventDefault();
        }

    }, 1);

});


$('.numeroTwo').keypress(function (event) {
    var $this = $(this);
     

    if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
    var text = $(this).val();
    if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {
            if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
            }
        }, 1);
    }
    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8) &&
        ($(this)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }

    //var aaa = parseInt ((text + "" + event.key) );
    //text = ( text == "" || text == undefined || text == "" ? "" : text);
    if (parseFloat(text + "" + event.key) > 100) {
        event.preventDefault();
    }


});

$('.numeroTwo').on('keyup', function (e) {

    var text = $(this).val();

 
        //else
        //    decimal = '.00';
        if (parseFloat(text ) > 100) {
            event.preventDefault();
 
    
         
        this.focus();
    }
});


/********************************************************/





//function numberWithCommas(x) {
//    x = x.toString();
//    var pattern = /(-?\d+)(\d{3})/;
//    while (pattern.test(x))
//        x = x.replace(pattern, "$1,$2");
//    return x;
//}

//function numberWithCommas(x) {
//    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
//}

//$('.monto').digits();

//$.fn.digits = function () {
//    return this.each(function () {
//        $(this).text($(this).text().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
//    })
//}

function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}


//$('.monto').keypress(function (event) {
//    var $this = $(this);
//    if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
//        ((event.which < 48 || event.which > 57) &&
//            (event.which != 0 && event.which != 8))) {
//        event.preventDefault();
//    }

//    var text = $(this).val();
//    if ((event.which == 46) && (text.indexOf('.') == -1)) {
//        setTimeout(function () {
//            if ($this.val().substring($this.val().indexOf('.')).length > 3) {
//                $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
//            }
//        }, 1);
//    }

//    if ((text.indexOf('.') != -1) &&
//        (text.substring(text.indexOf('.')).length > 2) &&
//        (event.which != 0 && event.which != 8) &&
//        ($(this)[0].selectionStart >= text.length - 2)) {
//        event.preventDefault();
//    }
//});


//$('.monto').keypress(function (event) {
//    var $this = $(this);
//    if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
//        ((event.which < 48 || event.which > 57) &&
//            (event.which != 0 && event.which != 8))) {
//        event.preventDefault();
//    }

//    var text = $(this).val();
//    if ((event.which == 46) && (text.indexOf('.') == -1)) {
//        setTimeout(function () {
//            if ($this.val().substring($this.val().indexOf('.')).length > 3) {
//                $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
//            }
//        }, 1);
//    }

//    if ((text.indexOf('.') != -1) &&
//        (text.substring(text.indexOf('.')).length > 2) &&
//        (event.which != 0 && event.which != 8) &&
//        ($(this)[0].selectionStart >= text.length - 2)) {
//        event.preventDefault();
//    }
//});
//$(".letras").keypress(function (key) {

//    if ((key.charCode < 97 || key.charCode > 122)//letras mayusculas

//        && (key.charCode < 65 || key.charCode > 90) //letras minusculas

//        // && (key.charCode != 45) //retroceso

//        && (key.charCode != 241) //ñ

//        && (key.charCode != 209) //Ñ

//        && (key.charCode != 32) //espacio

//        && (key.charCode != 225) //á

//        && (key.charCode != 233) //é

//        && (key.charCode != 237) //í

//        && (key.charCode != 243) //ó

//        && (key.charCode != 250) //ú

//        && (key.charCode != 193) //Á

//        && (key.charCode != 201) //É

//        && (key.charCode != 205) //Í

//        && (key.charCode != 211) //Ó

//        && (key.charCode != 218) //Ú


//    )

//        return false;

//  });




$('.monto').on("paste", function (e) {
    var text = e.originalEvent.clipboardData.getData('Text');
    if ($.isNumeric(text)) {
        if ((text.substring(text.indexOf('.')).length > 3) && (text.indexOf('.') > -1)) {
            e.preventDefault();
            $(this).val(text.substring(0, text.indexOf('.') + 3));
        }
    }
    else {
        e.preventDefault();
    }
});



$('.collapse-section').on('click', function () {
    var className = "." + $(this).attr('id');
    console.log(className);
    if ($(className).is(":visible")) {
        //console.log('Hide');
        $(className).fadeOut(400);
    } else {

        $(className).fadeIn(400);
    }

});


$('.collapse').on('click', function () {

    if ($(this).hasClass("collapseactive")) {
        $(this).removeClass('collapseactive');
    } else {
        $(this).addClass('collapseactive');
    }


});


function isNumber(evt) {
    var char = evt.key;// String.fromCharCode(evt.char);
    var reg = /^\d+$/;
    if (!reg.test(char))
        evt.preventDefault();
}

function isDecimal(evt, control) {
    var char = $('.' + control).val() + '' + evt.key;
    if (char != "") {
        if (!(char.search(/^\$?[\d]+(\.\d*)?$/) >= 0))
            event.preventDefault();
    }
}


function rucValido(ruc) {
    //11 dígitos y empieza en 10,15,16,17 o 20
    if (!(ruc >= 1e10 && ruc < 11e9
        || ruc >= 15e9 && ruc < 18e9
        || ruc >= 2e10 && ruc < 21e9))
        return false;

    for (var suma = -(ruc % 10 < 2), i = 0; i < 11; i++, ruc = ruc / 10 | 0)
        suma += (ruc % 10) * (i % 7 + (i / 7 | 0) + 1);
    return suma % 11 === 0;

}


function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function LimpiarCampos() {
    $('.textcontrol').val('');
    $('.select').val('0');
}

function DeshabilitarCampos() {
    //$('.textcontrol').prop('disabled','disabled');
    //$('.select').prop('disabled', 'disabled');
}

//$('.numero').keyup(function () { this.value = (this.value + '').replace(/[^0-9]/g, ''); });

function LoadTablaMaestra(nombretabla, element, sel, bdisable) {
 
    var classname = '.' + element;
    var objParametros = {
        PSNOMBRE_TABLA: nombretabla
    }

    $.ajax({
        url: '../../../Tools/GetTablaMaestra',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var data = data.entityList;

            $(data).each(function (index, value) {

                var option = '<option value="' + value.NID + '">' + value.SDESC_VALOR + '</option>';
                //Add option in select
                $(classname).append(option);

            });

            if (sel != null && sel != undefined) {
                {
                    $(classname).val(sel).trigger('change');
                    if (bdisable)
                        $(classname).prop('disabled', 'disabled');
                }


            }
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}




function LoadTablaMaestraOnlyList(nombretabla, element, sel, bdisable, OnlyList = null) {

    var classname = '.' + element;
    var objParametros = {
        PSNOMBRE_TABLA: nombretabla
    }

    $.ajax({
        url: '../../../Tools/GetTablaMaestra',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var data = data.entityList;

            $(data).each(function (index, value) {
                var si = OnlyList.find(x => x == value.NID);
                if (!si) {
                    var option = '<option value="' + value.NID + '">' + value.SDESC_VALOR + '</option>';
                    //Add option in select
                    $(classname).append(option);
                }
            });

            if (sel != null && sel != undefined) {
                {
                    $(classname).val(sel).trigger('change');
                    if (bdisable)
                        $(classname).prop('disabled', 'disabled');
                }


            }
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}


function LoadProducto(element, sel, tipo_poliza) {

    var classname = '.' + element;

    if (tipo_poliza == null || typeof tipo_poliza == 'undefined') {
        tipo_poliza = 0
    }

    $(classname).empty();
    var option_seleccione = '<option value="0">Seleccione</option>';
    $(classname).append(option_seleccione);

    $.ajax({
        url: '../../../Contractor/GetProductos?nidtipo_poliza=' + tipo_poliza,
        contentType: 'application/json',
        success: function (data) {

            var data = data.entityList;

            $(data).each(function (index, value) {

                var option = '<option value="' + value.NID_PRODUCTO + '">' + value.SDESC_PRODUCTO + '</option>';
                //Add option in select
                $(classname).append(option);

            });

            if (sel != null && sel != undefined) {
                //$(classname).val(sel).trigger('change');
                $(classname).val(sel);//.trigger('change');
                //$(classname).prop('disabled', 'disabled');
            }
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}



function LoadCanal(sel) {

    var classname = '.selectcanal';
    var objParametros = {
        NCANAL: 0
    }

    $.ajax({
        url: '../../../Tools/GetCanal',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var data = data.entityList;

            $(data).each(function (index, value) {

                var option = '<option value="' + value.NID_CANAL + '">' + value.SDESC_CANAL + '</option>';
                //Add option in select
                $(classname).append(option);

            });

            if (sel != null && sel != undefined) {
                $(classname).val(sel).trigger('change');
                $(classname).prop('disabled', 'disabled');
            }
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
}


function validar(campo, nombreCampo) {
    //($("#ramosbs").val() === undefined || $("#ramosbs").val() == null || $("#ramosbs").val() == '') {
    if (campo == null || campo === undefined || campo == '') {
        return false;
    }
    return true;
}

function FormatoMonto(monto) {
    var newMonto = monto.toString().replace(/,/g, '');
    var newMonto1 = newMonto.toString().replace('.', ',');
    //var newMonto = monto.replace(/,/g, '');
    //var newMonto1 = newMonto.replace('.', ',');
    return newMonto1;
    //return monto.replace(',', '').replace(',', ''); //.replace('.', ',');
}


function Eliminar(data, id) {
    return $.grep(data, function (e) {
        return e.NID != id;
    });
}
