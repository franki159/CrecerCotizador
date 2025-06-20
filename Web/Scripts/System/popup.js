function ResizePopup(x, y) {

    var width = '';
    var height = '';

    if (x != null && x !== undefined) {
        width = x + 'px';
    }

    if (y != null && y !== undefined) {
        height = y + 'px';
    }

    if (x != '') {
        $('.popup-content').css('width', width );
    }

    if (y != '') {
        $('.popup-content').css('height', height);
    }

}