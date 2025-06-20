$(document).ready(function () {

    $('.arrowcheck').change(function () {
        if (!this.checked) {
            $('.items').height(95);
        }
        else {
            $('.items').height(0);
        }
    });

});

function addOption(JSonParameter, JSonData) {

    if (JSonParameter === undefined || JSonParameter === null) {
        return false;
    }

    if (JSonData === undefined || JSonData === null) {
        return false;
    }

    var className = '.' + JSonParameter.NameSelect;

    $(JSonData).each(function (index, value) {

        var objData = value;
        var optionCode = '';

        optionCode += '<div class="option" value-data="' + objData.STAG + '" alt="' + objData.SNAME + '"><span><img src="../' + objData.STAG + '" />' + objData.SNAME + '</span></div>';

        $(className).append(optionCode);

    });

}