$(function () {

    if ($(window).width() > 800) {


    } else {
        $('#tableMem').css('width', 350);


    }

    $("#btnQuery").css('width', 300);
    $("#btnQuery").css('background-color', 'rgb(204,204,204)');
    $("#blkQ").css('width', 300);
    $("#blkQ").css('background-color', 'white');
    $("#btnExcel").css('width', 300);
    $("#btnExcel").css('background-color', 'white');
    $("#btnAllExcel").css('width', 300);
    $("#btnAllExcel").css('background-color', 'white');


    $('#blkQ').click(function () {
        $('#hidGroupClass').val($('#dropGroupClass').find(':selected').text());
        $('#hidGroupName').val($('#dropGroupName').find(':selected').text());
        $('#hidEname').val($('#txtEname').val());
    });

    // Initial call of function
    KeepSessionAlive();

});

function KeepSessionAlive() {
    // 1. Make request to server
    $.post("http://changelifesys.org/admin/MemSubData/MemSubDataList.aspx");

    // 2. Schedule new request after 60000 miliseconds (1 minute)
    setInterval(KeepSessionAlive, 60000);
}
