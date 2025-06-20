
function  loadSelect(element, codelist) {

    var classname = '.' + element;
    var objParametros = {
        P_SCODE: codelist
    }

    $.ajax({
        url: '../../../Tools/GetList',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {

            var data = data.entityList;

            $(data).each(function (index, value) {

                var option = '<option value="' + value.SITEM + '">' + value.SDESCRIPTION + '</option>';

                //Add option in select
                $(classname).append(option);

            });
        },
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });

}



function getValorParametro(sValor, nValor, nType) {
    var sReturn = '';
    var objParametros = {
        PSVALOR: sValor,
        PNVALOR: nValor,
        PNTYPE: nType
    }

    $.ajax({
        url: '../../../Tools/GetValueParametro',
        contentType: 'application/json',
        data: objParametros,
        success: function (data) {
            sReturn = data;
        },
        async: false,
        error: function () {
            console.log('%cError: The information could not be obtained.', 'color:red');
        }
    });
    return sReturn;
}