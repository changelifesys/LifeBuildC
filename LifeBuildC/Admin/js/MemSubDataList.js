$(function () {

    //if ($(window).width() > 800) {
    //    $(".inputStyle").css('width', 300);
    //    $(".FieldStyle2").removeClass();
    //    $(".FieldStyle").css('display', 'none');
    //}

    $("#btnQuery").css('width', 300);
    $("#btnQuery").css('background-color', 'rgb(204,204,204)');
    $("#blkQ").css('width', 300);
    $("#blkQ").css('background-color', 'white');

    $('#blkQ').click(function () {
        $('#hidGroupClass').val($('#dropGroupClass').find(':selected').text());
        $('#hidGroupName').val($('#dropGroupName').find(':selected').text());
        $('#hidEname').val($('#txtEname').val());
    });

});