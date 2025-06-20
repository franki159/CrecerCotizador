var codetreeview;
var treeviewchild;
var childclass;

function AssembleTreeView(JSonData) {

    //Validating JSonData
    if (JSonData === undefined) {
        console.log('%cError: You have not sent data as an argument', 'color:red');
        return false;
    }

    //Clean variable
    treeviewchild = '';
    codetreeview = '';
    childclass = '';
   

    $.each(JSonData, function (index, value) {

        //Initialize variable
        treeviewchild = '<tr>';

        //Received value
        var ObjectData = value;

        //Conditional for arrow rigth
        if (ObjectData.SARROW == '1') {

            //Conditional type children
            if (ObjectData.STYPE == 'F') treeviewchild += '<td>';
            else if (ObjectData.STYPE == 'S') treeviewchild += '<td class="son">';
            else if (ObjectData.STYPE == 'C') treeviewchild += '<td class="child">';

            treeviewchild += '<img class="arrow" id="' + ObjectData.STYPE + '-' + ObjectData.NIDRESOURCE + '" src="../Images/Areas/Modules/play-arrow_16.png" />';

            //Set variable
            childclass = ObjectData.STYPE + '-' + ObjectData.NIDRESOURCE;

        } else {

            if (childclass != '') {

                //Conditional type children
                if (ObjectData.STYPE == 'F') treeviewchild += '<td>';
                else if (ObjectData.STYPE == 'S') treeviewchild += '<td class="son ' + childclass + '">';
                else if (ObjectData.STYPE == 'C') treeviewchild += '<td class="child ' + childclass + '">';

            } else {

                //Conditional type children
                if (ObjectData.STYPE == 'F') treeviewchild += '<td>';
                else if (ObjectData.STYPE == 'S') treeviewchild += '<td class="son">';
                else if (ObjectData.STYPE == 'C') treeviewchild += '<td class="child">';

            }

            treeviewchild += '<img class="arrow" src="../Images/Areas/Modules/play-arrow_white_16.png" />';

        }

        //Add checkbox
        if (ObjectData.SARROW == '1')
            treeviewchild += '<input class="square" id="check-' + ObjectData.NIDRESOURCE + '" type="checkbox" />';
        else
            treeviewchild += '<input id="check-' + ObjectData.NIDRESOURCE + '" type="checkbox" />';

        //Icon for span
        if (ObjectData.STYPE == 'F') treeviewchild += '<img src="../' + ObjectData.SHTML + '" />';
        else treeviewchild += '<img src="../Images/Areas/Modules/radio_16.png" />';

        //Name for checbox
        treeviewchild += '<span>' + ObjectData.SNAME + '</span>';

        //Close row
        treeviewchild += '</td></tr>';

        //Concat variable
        codetreeview += treeviewchild;
    });




    //Return string 
    return codetreeview;

}

function CompareNodeInTreeView(JSonData, IdNode) {

    $(JSonData).each(function (index, value) {

        //Received data
        var ObjectData = value;

        //Prefix input
        var checkbox = 'td #check-' + ObjectData[IdNode];

        //Checked input
        $(checkbox).attr("checked", "checked");

    });

}

function DestroyTreeView(NameTable) {

    //Get name class table
    var classTable = '.' + NameTable;

    //Remove all rows table
    $(classTable + ' tr').each(function () {
        $(this).remove();
    });
}



/*---------------------------TREEVIEW-----------------------------*/

function FileTreeview(JSonData) {
    
    //Validating JSON Data
    if (JSonData === undefined) {
        Console.log('%cError: You have not sent data as an argument', 'color:red')
        return false;
    }
    var FilecodeTreeview = '';
    var Filetreeviewchild = '';
    var Filechildclass = '';
    var FilePath = '';
    var maxLevel = JSonData[0].MAXIMO_NIVEL;
    var levelold = '';

    var marginleft = '';
    // for (var i = 0; i < length; i++) {
    $.each(JSonData, function (index, value) {

        Filetreeviewchild = '<tr>';
        var ObjectData = value;//SL
        //if (i == ObjectData.NNIVEL_CARPETA) {

        marginleft = parseInt((ObjectData.NNIVEL_CARPETA - 1)) * 15;

        if (ObjectData.NIDESTRUCTURA_PADRE == 0) {
            //Conditional type children
            Filetreeviewchild += '<td>';
            //Filetreeviewchild += '<img class="" id="cla-' + ObjectData.NID +  '" src="../Images/Areas/Modules/play-arrow_16.png" />';
            //Filetreeviewchild += '<img class="arrow" id=cla-"' + ObjectData.NID + '-' + ObjectData.SNOMBRE_CARPETA + '" src="../Images/Areas/Modules/play-arrow_16.png" />';
            //Set variable
            Filechildclass = "cla-"+ ObjectData.NID;
        }
        else {
            if (ObjectData.NIDESTRUCTURA_PADRE > 0) {

                //if (ObjectData.NNIVEL_CARPETA != levelold)
                //{
                //}

                if (Filechildclass != '') {
                    //Conditional type children
                    //Filetreeviewchild += '<td class="son ' + Filechildclass + '" style='"margin-left:" + marginleft + " >';
                    Filetreeviewchild +='<img class="arrow"   src="../Images/Areas/Modules/play-arrow_white_16.png" />'; //regresar 
                    //Filetreeviewchild += '<img class="arrow" id="cla-' + ObjectData.NID +  '"   src="../Images/Areas/Modules/play-arrow_white_16.png" />';
                    Filetreeviewchild += '<td class="son ' + Filechildclass + ' ' + " cla-" + ObjectData.NIDESTRUCTURA_PADRE + '  " style="margin-left:' + marginleft + 'px"   >'; // regresar
                    //Filetreeviewchild += '<td class="son ' + "cla-" + ObjectData.NIDESTRUCTURA_PADRE;  + '" style="margin-left:' + marginleft + 'px"   >';
                    //treeviewchild += '<td class="child ' + childclass + '">';
                } else {
                    //Conditional type children
                    /*if (ObjectData.STYPE == 'F') treeviewchild += '<td>';
                    else if (ObjectData.STYPE == 'S') treeviewchild += '<td class="son">';
                    else if (ObjectData.STYPE == 'C') treeviewchild += '<td class="child">';*/
                    Filetreeviewchild += '<td class="child">';
                }
                Filetreeviewchild += '<img class="arrow"  src="../Images/Areas/Modules/play-arrow_white_16.png" />';
            }
        }

        //Add checkbox
        /*if (ObjectData.NIDESTRUCTURA_PADRE == 0)
            Filetreeviewchild += '<input class="square" id="check-' + ObjectData.NID + '" type="checkbox" />';
        else
            Filetreeviewchild += '<input id="check-' + ObjectData.NID + '" type="checkbox" />';*/
        Filetreeviewchild += '<img class="arrow" id="cla-' + ObjectData.NID + '"  src="../Images/Areas/SecurityUser/folder.jpg" style="width:18px!important;height:16px !important" />';
        Filetreeviewchild += '<input class="square subject-list" nid=' + ObjectData.NID + ' nlevel=' + ObjectData.NNIVEL_CARPETA + ' sruta=' + ObjectData.SRUTA_FILE + '   id="check-' + ObjectData.NID + '" type="checkbox" onclick="selectOnlyThis(this.id)"/>';

        //Icon for span
        //if (ObjectData.NIDESTRUCTURA_PADRE == 0)
        //Filetreeviewchild += '<img src="../' + ObjectData.SNOMBRE_CARPETA + '" />';
        //else
        //Filetreeviewchild += '<img src="../Images/Areas/Modules/radio_16.png" />';

        //Name for checbox
        Filetreeviewchild += '<span id=' + ObjectData.NID + '>' + ObjectData.SNOMBRE_CARPETA + '</span>';

        //Close row
        Filetreeviewchild += '</td></tr>';
        levelold = ObjectData.NNIVEL_CARPETA;
        //Concat variable
        FilecodeTreeview += Filetreeviewchild;
        //}
    });
     

    return FilecodeTreeview;
}


function FileTreeviewOLD(JSonData) {
    //Validating JSON Data
    if (JSonData === undefined) {
        Console.log('%cError: You have not sent data as an argument', 'color:red')
        return false;
    }
    var FilecodeTreeview = '';
    var Filetreeviewchild = '';
    var Filechildclass = '';
    var FilePath = '';
    var maxLevel = JSonData[0].MAXIMO_NIVEL;
    var levelold = '';

    var marginleft = '';
   // for (var i = 0; i < length; i++) {
    $.each(JSonData, function (index, value) {
        
        Filetreeviewchild = '<tr>';
            var ObjectData = value;//SL
            //if (i == ObjectData.NNIVEL_CARPETA) {

        marginleft = parseInt((ObjectData.NNIVEL_CARPETA-1)) * 20;

        if (ObjectData.NIDESTRUCTURA_PADRE==0) {
                //Conditional type children
                Filetreeviewchild += '<td>';
                Filetreeviewchild += '<img class="arrow" id="' + ObjectData.NID + '-' + ObjectData.SNOMBRE_CARPETA + '" src="../Images/Areas/Modules/play-arrow_16.png" />';
                //Set variable
                Filechildclass = ObjectData.NNIVEL_CARPETA + '-' + ObjectData.NID;
            }
            else {
                if (ObjectData.NIDESTRUCTURA_PADRE > 0) {
                    
                    //if (ObjectData.NNIVEL_CARPETA != levelold)
                    //{
                    //}

                    if (Filechildclass != '') { 
                        //Conditional type children
                        //Filetreeviewchild += '<td class="son ' + Filechildclass + '" style='"margin-left:" + marginleft + " >';
                        Filetreeviewchild += '<td class="son ' + Filechildclass + '" style="margin-left:' + marginleft +'px"   >';
                     //treeviewchild += '<td class="child ' + childclass + '">';
                    } else {
                        //Conditional type children
                        /*if (ObjectData.STYPE == 'F') treeviewchild += '<td>';
                        else if (ObjectData.STYPE == 'S') treeviewchild += '<td class="son">';
                        else if (ObjectData.STYPE == 'C') treeviewchild += '<td class="child">';*/
                        Filetreeviewchild += '<td class="child">';
                    }
                    Filetreeviewchild += '<img class="arrow" src="../Images/Areas/Modules/play-arrow_white_16.png" />';

                }
            }

            //Add checkbox
            /*if (ObjectData.NIDESTRUCTURA_PADRE == 0)
                Filetreeviewchild += '<input class="square" id="check-' + ObjectData.NID + '" type="checkbox" />';
            else
                Filetreeviewchild += '<input id="check-' + ObjectData.NID + '" type="checkbox" />';*/
        Filetreeviewchild += '<img class="" src="../Images/Areas/SecurityUser/folder.jpg" style="width:18px!important;height:16px !important" />';
        Filetreeviewchild += '<input class="square subject-list" nid=' + ObjectData.NID + ' nlevel=' + ObjectData.NNIVEL_CARPETA + ' sruta=' + ObjectData.SRUTA_FILE +'   id="check-' + ObjectData.NID + '" type="checkbox" onclick="selectOnlyThis(this.id)"/>';
        
            //Icon for span
        //if (ObjectData.NIDESTRUCTURA_PADRE == 0)
            //Filetreeviewchild += '<img src="../' + ObjectData.SNOMBRE_CARPETA + '" />';
        //else
            //Filetreeviewchild += '<img src="../Images/Areas/Modules/radio_16.png" />';

            //Name for checbox
        Filetreeviewchild += '<span id=' + ObjectData.NID +'>' + ObjectData.SNOMBRE_CARPETA + '</span>';

            //Close row
            Filetreeviewchild += '</td></tr>';
             levelold = ObjectData.NNIVEL_CARPETA;
            //Concat variable
            FilecodeTreeview += Filetreeviewchild;
            //}
        });

    return FilecodeTreeview;
}




function selectOnlyThis(id) {
    /*for (var i = 1; i <= 14; i++) {
        document.getElementsByClassName("subject-list").checked = false;
    }*/
    $(".subject-list").each(function () {
        if ($(this).prop('id')!=id)
            $(this).prop('checked', false);
    }); 

  //  document.getElementById(id).checked = true;
}



