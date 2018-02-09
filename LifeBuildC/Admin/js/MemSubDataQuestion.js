$(function () {

    if ($(window).width() > 800) {
        //$(".inputStyle").css('width', 300);
        $(".FieldStyle2").removeClass();
        $(".FieldStyle").css('display', 'none');
    }

    $('#chkCategoryName1, #chkCategoryName2').change(function () {

        $('#hfCategoryName').val('');

        var _CategoryName = "";

        if ($('#chkCategoryName1').is(":checked")) {
            _CategoryName += "C1 ";

        }

        if ($('#chkCategoryName2').is(":checked")) {
            _CategoryName += "C2 ";
        }

        $('#hfCategoryName').val(_CategoryName);

    });

    $('#btnSendQ').click(function () {

        $.ajax({
            url: '/Admin/Api/SendQuestion.aspx',
            type: 'POST',
            dataType: 'JSON',
            data: {
                GroupClass: $('#lblGroupClass').text(),
                GroupName: $('#lblGroupName').text(),
                //Ename: $('#lblEname').text(),
                CategoryName: $('#hfCategoryName').val(),
                Email: $('#txtEmail').val(),
                SubLine: $('#txtSubLine').val(),
                Question: $('#txtQuestion').val()
            },
            success: function (data) {
            }
        }).fail(function (data) {
        });

    });

});