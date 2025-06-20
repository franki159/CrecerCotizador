
function renderTableNested(initParams) {

    data = initParams.dataTable;
    tableName = initParams.tableId;
    rowAfter = initParams.rowId;
    props = initParams.props;
    jHeader = initParams.tableHeader;
    colChk = initParams.colCheckAll;
    editButton = initParams.editButton;
    delButton = initParams.delButton;
    hasSequence = initParams.colSequence;
    ppagesize = initParams.pagesize;
    pcallerFunc = initParams.callerId;
    idInstance = initParams.instanceId;
    hasParams = initParams.hasParams;
    valDisable = initParams.valDisable;
    disabledByCol = initParams.disabledByCol;

    var countCol = 0;
    var rowtotal = 0;
    var pagesize = 0;

    var hTable = "";
    var rTable = "";
    var table = tableName + " table";
    var callerFunc = pcallerFunc;

    function executeFunctionByName(functionName, context /*, args */) {
        var args = Array.prototype.slice.call(arguments, 2);
        var namespaces = functionName.split(".");
        var func = namespaces.pop();
        for (var i = 0; i < namespaces.length; i++) {
            context = context[namespaces[i]];
        }
        return context[func].apply(context, args);
    }

    function getObjects(obj, key, val) {
        var objects = [];
        for (var i in obj) {
            if (!obj.hasOwnProperty(i)) continue;
            if (typeof obj[i] == 'object') {
                objects = objects.concat(getObjects(obj[i], key, val));
            } else if (i == key && obj[key] == val) {
                objects.push(obj);
            }
        }
        return objects;
    }

    function renderToggle(checked, idbool, idvalue) {

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

    function renderBtnDelete(ColumnClassNameDelete, idvalue, nId) {

        //Get property toggle
        var button = {
            style: 'data-delete ' + ColumnClassNameDelete,
            id: "btn_d_row_" + nId//idvalue
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

    function renderBtnEdit(ColumnClassNameEdit, idvalue, nId) {

        //Get property toggle
        var button = {
            style: 'data-edit ' + ColumnClassNameEdit,
            id: "btn_e_row_" + nId//idvalue
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

    function renderBarButtsons(type, value, nId) {
        var cell = "";

        if (type === 1)
            cell = "<td></td>"
        else {
            cell = "<td>" + ((editButton) ? renderBtnEdit('btnRowEdit', value, nId) : "") + ((delButton) ? renderBtnDelete('btnRowDelete', value, nId) : "") + "</td>";
        }
        return cell;
    }

    function renderCheckbox(wrap, val, i, j) {
        var classCss = (i === 0 && j === 0) ? 'class="selectAll"' : 'class="__isCssDisabled"';
        var onclick = (i === 0 && j === 0) ? "onclick=" + idInstance + ".selectAll('cont',this); " : '';
        var idElement = 'id="chk_' + i + '_' + j + '" ';
        var value = !(i === 0 && j === 0) ? "value='" + val + "'" : '';
        var isDisabled = (data.length === 0) ? " disabled " : " __isChkDisabled ";
        onclick = onclick.replace(/cont/g, wrap);

        //var chkbox = "<span class='check-st'><input type='checkbox' id='chk_" + idElement + "' " + classCss + " " + onclick + " " + value + "></input><span>";    
        var chkbox = '<span class="check-st"><input type="checkbox" ' + idElement + classCss + isDisabled + onclick + value + '></input><span>';
        return chkbox;
    }

    function resetTable(tableName) {
        var nRows = $(tableName + ' tr').length;

        if (nRows > 0) {
            $(tableName + ' tr').each(function () {
                $(this).remove();
            });
        }
    }

    function applyAddProps(tableName) {
        var j = 1;
        var i = 0;

        i = (colChk && hasSequence) ? 2 : ((hasSequence || colChk) ? 1 : i)

        $.each(jHeader, function (index, value) {

            var propsCell = getObjects(props, 'col', j);

            $.each(propsCell, function () {
                $.each(this, function (key, val) {

                    if (key === 'props') {
                        $.each(val, function () {
                            $.each(this, function (key, val) {
                                if (key === "visible" && val === false) {

                                    $(tableName + ' table td:nth-child(' + (j + i) + ')').hide();
                                }
                            })
                        });
                    }
                })
            })

            j++;
        });

        //$(tableName + ' tbody:nth-child(odd)').css("background-color", "#F4F4F8");
    }

    function setPropsCell(props, colName, colValue, colId, rowId, nId) {
        //var objCol = getObjects(propsCol, 'col', '1');
        var spanHtml = "<span class='label'></span>";
        var colTemp = "<td class='";
        var addHtml = colValue;
        var idElement = rowId + "_" + colId;
        //var cssCol = "col" + idElement + "_" + colName;
        var cssCol = colName + "-" + nId;

        //Get properties
        $.each(props, function () {
            $.each(this, function (name, value) {
                //console.log(name + '=' + value);

                if (name === 'props') {
                    $.each(value, function () {
                        $.each(this, function (key, val) {
                            switch (key) {
                                case "toggle":
                                    if (val) {
                                        cssCol += " toggle";
                                        //addHtml = "<span><input type='checkbox' checked = checked id='chk_" + idElement + "'/> </span>";
                                        addHtml = renderToggle((parseFloat(colValue) === 1) ? true : false, true, nId);
                                    }
                                    break;
                                case "button":
                                    if (val) {
                                        addHtml = "<span><button id='btn_" + idElement + "'>Edit</button> </span>";
                                    }
                                    break;
                                case "textbox":
                                    if (val) {
                                        addHtml = "<span><input type='text' id='txt_" + idElement + "'/></span>";
                                    }
                                    break;
                                    //label style                            
                                case "label":
                                    if (val) {
                                        addHtml = "<span class='label className classState'>colValue</span>";
                                    }
                                    break;
                                case "multidata":
                                    if (val) {
                                        try {
                                            var sValues = colValue.split('|');

                                            if (sValues.length > 1) {
                                                addHtml = "";

                                                $.each(sValues, function (key, val) {
                                                    var dat = val.split(',');
                                                    addHtml += "<span class='label className className_" + dat[0] + "'>" + dat[1] + "</span>";
                                                });
                                            } else {
                                                addHtml = "";

                                                $.each(sValues, function (key, val) {
                                                    var dat = val.split(',');
                                                    addHtml += "<span class='label className className_" + dat[0] + "'>" + dat[1] + "</span>";
                                                });
                                            }
                                        } catch (err) {
                                            console.log(err + " - " + colValue);
                                        }
                                    }
                                    break;
                                case "cssClass":
                                    addHtml = addHtml.replace(/className/g, val);
                                    break;
                                case "isState":
                                    if (val) {
                                        var nCode = colValue.split('|');

                                        addHtml = addHtml.replace('classState', 'l_state_job_' + nCode[0]);
                                        addHtml = addHtml.replace('colValue', nCode[1]);

                                    }
                                    else {
                                        addHtml = addHtml.replace('classState', '');
                                        addHtml = addHtml.replace('colValue', colValue);
                                    }
                                    break;
                                case "linkStyle":
                                    if (val) {
                                        addHtml = "<span class='link link_view' id='lnk-" + nId + "'>" + colValue + "</span>";
                                    }
                                    break;
                                case "showErrosObs":
                                    if (val) {
                                        addHtml = (parseInt(colValue) === 2) ? "<span class='link show_errors " + colValue + " ' id='lnk-" + nId + "'>Ver errores</span>" : "";
                                    }
                                    break;
                                default:
                                    break;
                            }
                        })
                    })
                }
            });
        });
        return colTemp += cssCol + "'>" + addHtml + "</td>";
    }

    function renderBody(data, props, colChk, tableName) {
        var i = 0;
        var j = 0;
        var rowTable = "";
        var chkDisabled = false;

        if (data.length === 0) {
            rowTable = renderTableEmpty();
        } else {

            //processing rows
            $.each(data, function (index, value) {
                var jsonRow = value;

                rowTable += "<tr class='tr-data " + (((i + 1) % 2 === 1) ? "even" : "odd") + " tr-nested '>";
                rowTable += colChk ? "<td>" + renderCheckbox(tableName, jsonRow.NID, (i + 1), j) + "</td>" : '';
                rowTable += hasSequence ? "<td>" + (i + 1) + "</td>" : '';

                j = 1;
                //processing columns
                $.each(jsonRow, function (key, val) {
                    //rowTable += "<td class='col" + j + "_" + index + "'>" + value +"</td>";   

                    if (j <= countCol) {
                        var propsCell = getObjects(props, 'col', j);

                        //Set properties
                        rowTable += setPropsCell(propsCell, key, val, j, (i + 1), jsonRow.NID);

                        //
                        if (disabledByCol === key && parseInt(valDisable) != parseInt(val)) {
                            chkDisabled = true;
                        }

                        j++;
                    }
                });

                rowTable = (chkDisabled) ? rowTable.replace(/__isChkDisabled/g, "disabled") : rowTable.replace(/__isChkDisabled/g, "");
                rowTable = (chkDisabled) ? rowTable.replace(/__isCssDisabled/g, "chk-disabled") : rowTable.replace(/__isCssDisabled/g, "");
                chkDisabled = false;
                rowTable += renderBarButtsons(2, (i + 1), jsonRow.NID);
                rowTable += "</tr>";
                i++;
            });
        }

        return rowTable;
    }

    function renderHeader(tableName, jHeader, colChk) {
        var htable = "";
        var tmpHeader = "<thead><tr class='theader tr-title'>";
        var count = 0;
        tmpHeader += colChk ? "<td>" + renderCheckbox(tableName, 0, 0, 0) + "</td>" : '';
        tmpHeader += hasSequence ? "<td>#</td>" : '';

        $.each(jHeader, function (index, value) {
            tmpHeader += "<td>" + value + "</td>";
            count++;
        });

        tmpHeader += renderBarButtsons(1, 0, 0);
        countCol = count;
        return htable += tmpHeader + "</tr></thead>";
    }

    //Render Pagination
    function renderPagination(JSonParameter) {

        var arrTools = ['<div class="border-left"><button class="preview" id="preview" __isDisabled onclick="__idInstance.getIDButtonPagination(this)">Previa</button></div>',
                    '<div><button class="next" id="next" __isDisabled onclick="__idInstance.getIDButtonPagination(this)">Siguiente</button></div>'];

        //Quantity for division
        var division = 10;

        if (JSonParameter === undefined || JSonParameter == null) {
            return false;
        }

        var number = JSonParameter.ROWTOTAL;
        var rownum = JSonParameter.ROWNUM;
        var n = 0;
        var r = 0;

        if (number > division) {

            n = number / rownum;
            r = number % rownum;

            if (r > 0) {
                n = parseInt(n) + 1;
            }

            if (rownum >= number) {
                n = 0;
            }

        }

        var strpage = '';

        arrTools[0] = arrTools[0].replace("__isDisabled", (parseInt(pagenum_nested) === 0 ? "disabled" : ""));

        if (n == 0) {
            arrTools[0] = arrTools[0] + '<div><button id="button-0" __isDisabled onclick="__idInstance.getIDButtonPagination(this)">1</button></div>' + arrTools[1];
            arrTools[0] = arrTools[0].replace(/__isDisabled/g, "disabled");
            strpage += arrTools[0]
        } else {

            //Previous button
            strpage += arrTools[0];

            var i = 0;
            var x = parseInt(pagenum_nested);
            
            //Initialize for
            for (i = 0; i < n; i++) {
                if (i >= x) {
                    if (i <= x + 4) {
                        strpage += '<div' + (parseInt(pagenum_nested) === (i) ? ' class="active"' : ' ') + ' "><button' + (parseInt(pagenum_nested) === (i) ? ' class="selected" ' : ' ') + ' id="button-' + i + '" ' + (parseInt(pagenum_nested) === (i) ? "disabled" : " ") + ' onclick="__idInstance.getIDButtonPagination(this)">' + (i + 1) + '</button></div>';
                    }
                }
            }

            arrTools[1] = arrTools[1].replace("__isDisabled", (parseInt(pagenum_nested) === (i - 1) ? "disabled" : ""));
            //Next button
            strpage += arrTools[1];

        }

        //Delete items div current
        $('#nav_' + idInstance).find('div').each(function () {
            $(this).remove();
        });

        strpage = strpage.replace(/__idInstance/g, idInstance);
        $('#nav_' + idInstance).append(strpage);

        var iniRange = (parseInt(pagenum_nested) * parseInt(rownum)) + 1;
        var endRange = (parseInt(n) === 0) ? parseInt(number) : (parseInt(pagenum_nested + 1) === parseInt(n)) ? parseInt(number) : (parseInt(iniRange) + parseInt(rownum)) - 1;

        //Modificado Jose Perez 29-11-2018

        $('#colLeng_' + idInstance + ' .info-pag span').text($('#colLeng_' + idInstance + ' .info-pag span').first().text().replace("XNUM", iniRange).replace("YNUM", endRange).replace("ZNUM", number));

    }

    function renderControlPagination() {

        var sl = "";
        var smsg = (data.length != 0) ? '<span class="info-pag">|<span>Mostrando registros del XNUM al YNUM de un total de ZNUM registros.</span></span>' : '';

        switch (ppagesize.toString()) {
            case "10":
                sl = '<div class="module-content-pagination" id="colLeng_' + idInstance + '"><div class="select-number-items"><span>Mostrar</span><select class="select-number-pagination" onChange="__idInstance.setControlPag(this)"><option value="10" selected>10</option><option value="15">15</option><option value="20">20</option></select><span>registros.</span>_msg_span</div><div class="content-number-pagination" id="nav_' + idInstance + '"></div></div>';
                break;
            case "15":
                sl = '<div class="module-content-pagination" id="colLeng_' + idInstance + '"><div class="select-number-items"><span>Mostrar</span><select class="select-number-pagination" onChange="__idInstance.setControlPag(this)"><option value="10">10</option><option value="15" selected>15</option><option value="20">20</option></select><span>registros.</span>_msg_span</div><div class="content-number-pagination" id="nav_' + idInstance + '"></div></div>';
                break;
            case "20":
                sl = '<div class="module-content-pagination" id="colLeng_' + idInstance + '"><div class="select-number-items"><span>Mostrar</span><select class="select-number-pagination" onChange="__idInstance.setControlPag(this)"><option value="10">10</option><option value="15">15</option><option value="20" selected>20</option></select><span>registros.</span>_msg_span</div><div class="content-number-pagination" id="nav_' + idInstance + '"></div></div>';
                break;
        }

        sl = sl.replace(/_msg_span/g, smsg);
        sl = sl.replace(/__idInstance/g, idInstance);
        return sl;//;'<div class="module-content-pagination"><div class="select-number-items"><span>Mostrar</span><select class="select-number-pagination" onChange="setControlPag(this)"><option value="10">10</option><option value="15">15</option><option value="20">20</option></select><span>registros.</span></div><div class="content-number-pagination"></div></div>';
    }

    function renderWraperGrid() {
        return '<div class="table-wrap"><div class="search-grid"><div class="grid-data"><table role="nested-grid" class="dTable nested-grid">_table</table></div></div>_pagination</div>';
    }

    function renderTableEmpty() {
        var colspan_number_empty = 0;

        $(jHeader).each(function (index, value) {
            for (property in value) {
                colspan_number_empty += 1;
            }
        });
        
        var i = (colChk && hasSequence) ? 3 : ((hasSequence || colChk) ? 2 : i)
        return "<tr class='tr-data even' style='text-align:center'><td colspan='" + colspan_number_empty + "'>No hay información disponible.</td></tr>";
    }

    //Public methods
    this.setControlPag = function setControlPag(field) {

        pagesize = $(field).val();
        pagesize_t_nested = pagesize;
        pagenum_nested = 0;

        JSonPagination = {
            ROWTOTAL: rowtotal,
            ROWNUM: pagesize
        }

        //Render pagination
        renderPagination(JSonPagination);

        //Invoke caller function        
        if (hasParams)
            executeFunctionByName(callerFunc, window, idRowSelected);
        else
            executeFunctionByName(callerFunc, window);
    }

    this.getIDButtonPagination = function GetIDButtonPagination(Object) {

        //Questions for rowtotal
        var rows = rowtotal / 10;
        rows = rowtotal % 10 == 0 ? rows : parseInt(rows) + 1;

        //Change background for default 
        $('.content-number-pagination').find('div span').each(function () {
            $(this).css('background', 'white');
        });

        //Change background container
        //$(this).css('background', 'rgb(28, 132, 198)');

        //Capture id of span
        var idspan = $(Object).attr('id');

        //Question value selected
        if (idspan != 'preview' && idspan != 'next') {
            //Get id for search in database
            pagenum_nested = $(Object).attr('id').replace('button-', '');
        } else {

            //Conditional for preview
            if (idspan == 'preview') {
                if (pagenum_nested == 0) {
                    return true;
                }
                else {
                    pagenum_nested -= 1;
                }
            }

            //Conditional for next
            if (idspan == 'next') {
                if (pagenum_nested == rows - 1) {
                    return true;
                }
                else {
                    pagenum_nested += 1;
                }
            }

        }

        //Invoke function
        if (hasParams)
            executeFunctionByName(callerFunc, window, idRowSelected);
        else
            executeFunctionByName(callerFunc, window);
    }

    this.selectAll = function selectAll(wrap, field) {
        $(wrap + " table input:checkbox").not(field && '.toggle-grid' && '.chk-disabled').prop('checked', field.checked);

        //tr-nested-table table-tr-200-5000-1
    }

    //return all checked inputs grid
    this.getAllChecked = function getAllChecked() {
        var selected = [];

        $(table + ' input[type=checkbox]').each(function () {
            if ($(this).is(":checked")) {
                if ($(this).attr('value') != undefined)
                    selected.push(parseInt($(this).attr('value')));
            }
        });

        return selected;
    }

    var renameTable = '.table-' + table.replace('.', '').replace('table', '').trim();

    //Render Header
    hTable = renderHeader(renameTable, jHeader, colChk);

    var wraper = renderWraperGrid();
    var controlPagination = renderControlPagination();

    //Rename for reset
    //var renameTable = '.table-' + table.replace('.', '').replace('table', '').trim();

    //Render Body
    resetTable(renameTable);
    rTable = renderBody(data, props, colChk, renameTable);

    //Se descartó porque se renderizará sobre un div y ya no sobre una table        

    wraper = wraper.replace("_table", hTable + rTable);
    wraper = wraper.replace("_pagination", controlPagination);

    var nameTableAlter = table.replace('.', '').replace('table', '').trim();
    var colspan_number = 0;

    if (data.length == 0) {
        $(jHeader).each(function (index, value) {
            for (property in value) {
                colspan_number += 1;
            }
        });
    } else {
        $(data).each(function (index, value) {
            for (property in value) {
                colspan_number += 1;
            }
        });
    }


    //After of content
    wraper = '<tr class="tr-nested-table table-' + nameTableAlter + '"><td class="tr-nested" colspan="' + colspan_number.toString() + '">' + wraper + '</td></tr>';

    //Delete elements not used
    $(renameTable).remove();

    //Render new table
    $(tableName).after(wraper);
    $(tableName).css('width', '100%');

    //Apply props
    applyAddProps(renameTable);

    //Pagination    
    pagesize = $('#colLeng_' + idInstance + ' .select-number-pagination').val();
    if (data.length != 0) {
        rowtotal = data[0].ROWTOTAL;
    }
    //console.log(data[0].ROWTOTAL + " - " + pagesize);
    JSonPagination = {
        ROWTOTAL: rowtotal,
        ROWNUM: pagesize
    }

    //Render pagination
    renderPagination(JSonPagination);

}