$(function () {

    if ($(window).width() > 800) {
        $(".inputStyle").css('width', 300);
        $(".FieldStyle2").removeClass();
        $(".FieldStyle").css('display', 'none');
    }

    var _json = $('#hfChcGroup').val();
    //console.log(_json);

    var _obj = JSON.parse(_json);
    //console.log(_obj.DataInfo[0]);
    //console.log(_obj.DataInfo.length);

    //組別
    for (var i = 0; i < _obj.DataInfo.length; i++) {
        //console.log(_obj.DataInfo[i]);
        $('#dropGroupClass').append($('<option></option>').attr('value', i).text(_obj.DataInfo[i].group));

        if (_obj.DataInfo[i].group == $('#hfGroupClass').val())
        {
            $("#dropGroupClass").val(i);
            getGroupName();

            $('#hfGroupClassValue').val($('#dropGroupClass :selected').text());
            $('#hfGroupNameValue').val($('#dropGroupName :selected').text());

        }

    }

    //組別
    $('#dropGroupClass').change(function () {
        getGroupName();
        $('#hfGroupClassValue').val($('#dropGroupClass :selected').text());
        $('#hfGroupNameValue').val($('#dropGroupName :selected').text());
    });

    //小組
    $('#dropGroupName').change(function () {
        $('#hfGroupNameValue').val($('#dropGroupName :selected').text());
    });

    function getGroupName()
    {
        //清空 option
        $('#dropGroupName option').remove();

        var _i = $('#dropGroupClass').find(':selected').val();
        //console.log(_i);

        for (var i = 0; i < _obj.DataInfo[_i].list.length; i++) {
            //console.log(_obj.DataInfo[_i].list[i]);
            $('#dropGroupName').append($('<option></option>').attr('value', i).text(_obj.DataInfo[_i].list[i]));

            if (_obj.DataInfo[_i].list[i] == $('#hfGroupName').val())
            {
                $('#dropGroupName').val(i);
            }

        }
    }

});