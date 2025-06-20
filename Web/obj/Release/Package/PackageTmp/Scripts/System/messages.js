
var arrErrors = null;

$(document).ready(function () {

    //Array errors
    arrErrors = [
        '%cError: You are trying to create a grid without having assigned the name of the table.',
        '%cError: It is mandatory to send as a parameter the data to be displayed in the grid.',
        '%cWarning: The table will only be informative, since no custom fields have been entered.',
        '%cError: There is no element of type table with the name sent as argument in the function.',
        '%cError: The headers of the grids are not being sent as parameters of the function.'
    ];

});

function SendErrors() {
    return arrErrors;
}
