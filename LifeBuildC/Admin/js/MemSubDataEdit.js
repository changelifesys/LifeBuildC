$(function () {

    var _json = $('#hfChcGroup').val();
    //console.log(_json);

    var _obj = JSON.parse(_json);
    //console.log(_obj.DataInfo[0]);
    //console.log(_obj.DataInfo.length);

    for (var i = 0; i < _obj.DataInfo.length; i++) {
        //console.log(_obj.DataInfo[i]);
        $('#dropGroupClass').append($('<option></option>').attr('value', i).text(_obj.DataInfo[i].group));
    }

    //var _GroupClassID = $('#GroupClassID').val();
    //$('#dropGroupClass option[value=' + _GroupClassID + ']').attr('selected', 'selected');

    $('#dropGroupClass').change(function () {

        $('#dropGroupName option').remove();

        var _i = $('#dropGroupClass').find(':selected').val();
        console.log(_i);

        for (var i = 0; i < _obj.DataInfo[_i].list.length; i++) {

            console.log(_obj.DataInfo[_i].list[i]);
            $('#dropGroupName').append($('<option></option>').attr('value', i).text(_obj.DataInfo[_i].list[i]));

        }

    });

    var a = $('#hfGroupClass').val();
    $('#dropGroupClass').val(2);
});