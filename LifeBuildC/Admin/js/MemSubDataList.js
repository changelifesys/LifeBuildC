$(function () {

    if ($(window).width() > 800) {
        $(".inputStyle").css('width', 300);
        $(".FieldStyle2").removeClass();
        $(".FieldStyle").css('display', 'none');
    }

    $('#blkQ').click(function () {
        $('#hidGroupClass').val($('#dropGroupClass').find(':selected').text());
        $('#hidGroupName').val($('#dropGroupName').find(':selected').text());
        $('#hidEname').val($('#txtEname').val());
    });

});