var table = '.module-content .module-content-data .data-contains .contains-search .search-grid .grid-data table tr';

$(window).resize(function () {

    if ($(window).width() <= 520) {
        return false;
    }

    if ($(window).width() <= 720) {
        //window.alert('720px');
        return false;
    }

    if ($(window).width() <= 920) {
        
        $(table).find('td').each(function () {

            $(this).removeClass('');

        });

        return false;
    }

});