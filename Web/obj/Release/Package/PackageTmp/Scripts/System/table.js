
var codeRow = '';
var codeTable = '';

function AssembleTable(JSonData, JSonProperties, JSonInputs) {

    if (JSonProperties === undefined) {
        console.log('%cError: Not entered the name of the table.', 'color:red');
        return false;
    }

    if (JSonProperties.NameTable === undefined || JSonProperties.NameTable === null || JSonProperties.NameTable === '') {
        console.log('%cError: You have entered an erroneous value in the table name property.', 'color:red');
        return false;
    }

    if (JSonData == undefined) {
        console.lo('%cError: You must enter information to assemble the table.', 'color:red');
        return false;
    }

    var classNameTable = '.' + JSonProperties.NameTable;

    //Set variable code html
    codeTable = '';
    codeRow = '';

    $.each(JSonData, function (index, value) {
        
        //Received data
        var data = value;

        if (JSonProperties.FunctionAssigned === undefined) {
            //Assemble td for table
            codeRow = '<tr><td id="profile-' + data[JSonInputs["id"]] + '">' + data[JSonInputs["label"]] + '</td></tr>';
        } else {
            //Assemble td for table
            codeRow = '<tr class="' + JSonProperties.ClassAssignedSN + '" onClick="' + JSonProperties.FunctionAssigned + '(this)" id="' + JSonProperties.ClassForId + '-' + data[JSonInputs["id"]] + '"><td>' + data[JSonInputs["label"]] + '</td></tr>';
        }
        

        //Concat a table
        codeTable += codeRow;

    });

    //Assign code a table
    $(classNameTable).append(codeTable);

}