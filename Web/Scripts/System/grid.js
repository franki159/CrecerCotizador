
//Array of images
var arrImages = null;

//Array errors
var arrErrors = null;

//Array columns nulls
var arrColumn = ['ROWNUMBER', 'ROWTOTAL', 'STATUS', 'TAG'];
var arrColumnChecked = ['SSTATE'];

//Variables for style
var color = false;
var rownum = false;
var toggle = false;
var optionedit = false;
var optiondelete = false;
var checked = false;
var selected = false;
var idbool = false;
var idvalue = '';

//Variables for count
var acumrow = 1;

//Code html
var codehtml = '';
var pathimages = '';

//Booleans
var flagTotal = false;

//Initialize library of convertion JSon to Grid
$(document).ready(function () {

    //Get errors
    arrErrors = SendErrors();

    //Get path images
    GetRouteImages()
});

function AssembleGrid(JSonParameters,JSonHeader,JSonData,ParamsColumnsStyle,JSonPagination) {

    //Update flag
    flagTotal = false;

    //Clear variables
    codehtml = '';
    acumrow = 1;

    //Validate parameters
    if (ValidationParams(JSonParameters, JSonHeader, JSonData, ParamsColumnsStyle, JSonPagination)) {

        //Validate exists header in table
        var nFilas = $('.' + JSonParameters.NameTable + ' tr').length;

        //Assemble code html
        var RowHeader = '<tr class="tr-title">';

        //Ask if there is a correlative number
        if (ParamsColumnsStyle !== undefined) {
            if (!ParamsColumnsStyle.ColumnCorrelative !== undefined) {
                if (ParamsColumnsStyle.ColumnCorrelative) {
                    RowHeader += '<td class="td-center">#</td>';
                    rownum = true;
                }
            }
        }

        //Ask if there is a toggle animation
        if (ParamsColumnsStyle !== undefined) {
            if (!ParamsColumnsStyle.ToggleAnimation !== undefined) {
                if (ParamsColumnsStyle.ToggleAnimation) {
                    toggle = true;
                }
            }
        }

        //Ask if there is a edit button
        if (ParamsColumnsStyle !== undefined) {
            if (!ParamsColumnsStyle.ColumnEdit !== undefined) {
                if (ParamsColumnsStyle.ColumnEdit) {
                    optionedit = true;
                }
            }
        }

        //Ask if there is a delete button
        if (ParamsColumnsStyle !== undefined) {
            if (!ParamsColumnsStyle.ColumnDelete !== undefined) {
                if (ParamsColumnsStyle.ColumnDelete) {
                    optiondelete = true;
                }
            }
        }

        //Assemble header table
        $.each(JSonHeader, function (index, value) {
            RowHeader += '<td>' + value + '</td>';
        });

        //Ask if there is a button options crud
        if (ParamsColumnsStyle !== undefined) {
            if (!ParamsColumnsStyle.ColumnEdit !== undefined || !ParamsColumnsStyle.ColumnDelete !== undefined) {
                if (ParamsColumnsStyle.ColumnEdit || ParamsColumnsStyle.ColumnDelete) {
                    RowHeader += '<td></td>';
                    option = true;
                }
            }
        }

        //Close row of header
        RowHeader += '</tr>';

        //Pass data
        if (nFilas == 0) {
            codehtml += RowHeader;
        } else {
            $('.tr-data').each(function () {
                $(this).remove();
            });
        }

        //Follow data
        $.each(JSonData, function (index, value) {

            //Received value
            var ObjectData = value;

            //String for cells with color a without color
            var RowCell = '';

            //Conditional color
            if (!color) {
                RowCell += '<tr class="tr-data color-silver">';
                color = true;
            } else {
                RowCell += '<tr class="tr-data">';
                color = false;
            }

            //Td expand for responsive
            RowCell += '<td class="td-expand"><button class="btnExpand"></button></td>';

            //Sequence grid
            if (rownum) RowCell += '<td class="td-center">' + acumrow + '</td>';

            $.each(ObjectData, function (index, value) {

                //Get Total
                if (!flagTotal) {
                    if(index == arrColumn[1]){
                        JSonPagination.ROWTOTAL = value;
                        flagTotal = true;
                    }
                }

                //Validate columns
                if (index != arrColumn[0] && index != arrColumn[1] && index != arrColumn[2] && index != arrColumn[3]) {
                    if (index == arrColumnChecked[0] && !selected) {
                        if (value == 0) {
                            checked = false;
                        }
                        else {
                            checked = true;
                            selected = true;
                        }
                    } else if (JSonParameters.Id !== undefined && JSonParameters.Id !== null && JSonParameters.Id !== '' && JSonParameters.Id == index) {
                        idbool = true;
                        idvalue = value;
                    } else {
                        RowCell += '<td class="td-responsive ' + index + '-' + idvalue + '">' + value + '</td>';
                    }
                }
            });

            //Toggle style
            if (toggle) RowCell += '<td>' + AddToggle(checked, idbool, idvalue) + '</td>';

            //Button option
            if (option) {

                //Open label
                RowCell += '<td colspan="2">';

                if (optionedit) {
                    RowCell += AddEdit(ParamsColumnsStyle.ColumnClassNameEdit, idvalue);
                }

                if (optiondelete) {
                    RowCell += AddDelete(ParamsColumnsStyle.ColumnClassNameDelete, idvalue);
                }

                //Close label
                RowCell += '</button>';
            }

            //Close row
            RowCell += '</tr>';

            //Pass data
            codehtml += RowCell;

            //Increment variable
            acumrow += 1;
            selected = false;
        });

        //Name table for class
        var nameTableClass = '.' + JSonParameters.NameTable;
        $(nameTableClass).append(codehtml);
    }
}

function AddToggle(checked,idbool,idvalue) {

    var Toggle = {
        style: 'toggle-grid'
    };

    if (checked) {
        if (idbool) var ToggleAssemble = '<input type="checkbox" id="toggle-' + idvalue + '" class="' + Toggle.style + '" checked />';
            else var ToggleAssemble = '<input type="checkbox" class="' + Toggle.style + '" checked />';
    } else {
        if (idbool) var ToggleAssemble = '<input type="checkbox" id="toggle-' + idvalue + '" class="' + Toggle.style + '" />';
            else var ToggleAssemble = '<input type="checkbox" class="' + Toggle.style + '" />';
        
    }

    //Checkbox with style toggle
    return ToggleAssemble;
}

function AddEdit(ColumnClassNameEdit, idvalue) {

    //Get property toggle
    var button = {
        style: 'data-edit ' + ColumnClassNameEdit,
        id: ColumnClassNameEdit + idvalue
    }

    //Assemble button
    var buttonAssemble = '<button id="' + button.id + '" class="tooltip ' + button.style + '" alt="Editar">';

    //Get images since array
    var imgAssemble = '<a><img src="../Images/Areas/Modules/edit_grid_16.png" /></a>';

    //Finally button edit
    buttonAssemble += imgAssemble + '</button>';

    //Return element for DOM
    return buttonAssemble;

}

function AddDelete(ColumnClassNameDelete, idvalue) {

    //Get property toggle
    var button = {
        style: 'data-delete ' + ColumnClassNameDelete,
        id: ColumnClassNameDelete + idvalue
    }

    //Assemble button
    var buttonAssemble = '<button id="' + button.id + '" class="tooltip ' + button.style + '" alt="Eliminar">';

    //Get images since array
    var imgAssemble = '<a><img src="../Images/Areas/Modules/delete_grid_16.png" /></a>';

    //Finally button edit
    buttonAssemble += imgAssemble + '</button>';

    //Return element for DOM
    return buttonAssemble;

}

function GetRouteImages() {

    //Go a controller for get path
    $.ajax({
        url: '../../../Common/GetPath',
        success: function (response) {
            pathimages = response.pathImage;
        },
        error: function () {
            console.log("No se ha podido obtener el path solicitado");
        }
    });

}

function AssemblePagination() {

}

function ValidationParams(JSonParameters, JSonHeader, JSonData, ParamsColumnsStyle, JSonPagination) {

    if (JSonParameters.NameTable == '' || JSonParameters.NameTable == null || JSonParameters.NameTable === undefined) {
        console.log(arrErrors[0], "color: red");
        return false;
    }

    var nameForId = '#' + JSonParameters.NameTable;
    var nameForClass = '.' + JSonParameters.NameTable;

    if ($(nameForId).length == 0 && $(nameForClass).length == 0) {
        console.log(arrErrors[3], "color: red");
        return false;
    }

    if (JSonHeader == null || JSonHeader === undefined) {
        console.log(arrErrors[4], "color: red");
        return false;
    }

    if (JSonData == null || JSonData === undefined) {
        console.log(arrErrors[1], "color: red");
        return false;
    }

    if (ParamsColumnsStyle == null || ParamsColumnsStyle === undefined) {
        console.log(arrErrors[2], "color: yellow");
        return true;
    } else {
        return true;
    }

    if (JSonPagination === undefined || JSonPagination == null) {
        return true;
    }

}