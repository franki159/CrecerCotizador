
var arrTools = ['<div class="border-left"><button class="preview" id="preview" onclick="GetIDButtonPagination(this)">Previa</button></div>',
                '<div><button class="next" id="next" onclick="GetIDButtonPagination(this)">Siguiente</button></div>'];

//Quantity for division
var division = 10;

function AssemblePagination(JSonParameter) {

    if (JSonParameter === undefined || JSonParameter == null) {
        return false;
    }

    var number = JSonParameter.ROWTOTAL;
    var rownum = JSonParameter.ROWNUM;
    var n = 0;
    var r = 0;

    if (number > division) {
        n = number / division;
        r = number % division;

        if (r > 0) {
            n = parseInt(n) + 1;
        }

        if (rownum >= number) {
            n = 0;
        }

    }

    var strpage = '';

    if (n == 0) {

        strpage += arrTools[0] + '<div><button id="button-0" onclick="GetIDButtonPagination(this)">1</button></div>' + arrTools[1];

    } else {

        //Previous button
        strpage += arrTools[0];

        var i = 0;

        //Initialize for
        for (i = 0; i < n; i++) {
            strpage += '<div><button id="button-' + i + '" onclick="GetIDButtonPagination(this)">' + (i + 1) + '</button></div>';
        }

        //Next button
        strpage += arrTools[1];

    }

    //Delete items div current
    $('.content-number-pagination').find('div').each(function () {
        $(this).remove();
    });

    $('.content-number-pagination').append(strpage);

}