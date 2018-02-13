$(function () {

    $("#btnSendQ").css('width', 300);
    $("#btnSendQ").css('background-color', 'rgb(204,204,204)');
    $("#btnCel").css('width', 300);
    $("#btnCel").css('background-color', 'white');


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

        //$("#btnCel").click();

        var IsSend = true;
        var Msg;

        if ($('#txtQuestion').val() == '') {
            IsSend = false;
            Msg = '請描述您的問題';
        }

        if ($('#txtSubLine').val() == '') {
            IsSend = false;
            Msg = '請填寫主旨';
        }

        if ($('#txtEmail').val() == '') {
            IsSend = false;
            Msg = '請填寫您的Mail';
        }

        if ($('#hfCategoryName').val() == '') {
            IsSend = false;
            Msg = '請勾選課程';
        }

        if (IsSend) {

            $('#btnSendQ').css('display', 'none');
            $('#btnCel').css('display', 'none');
            $('#imgloading').removeAttr('style');

            $.ajax({
                url: '/Admin/Api/SendQuestion.aspx',
                type: 'POST',
                dataType: 'JSON',
                data: {
                    GroupClass: $('#lblGroupClass').text(),
                    GroupName: $('#lblGroupName').text(),
                    CategoryName: $('#hfCategoryName').val(),
                    Email: $('#txtEmail').val(),
                    SubLine: $('#txtSubLine').val(),
                    Question: $('#txtQuestion').val()
                },
                success: function (data) {

                    //alert(data.IsPageError);

                    if (!data.IsPageError)
                    {
                        alert('問題已反應給中央同工');
                        $("#btnCel").click();
                    }
                    else
                    {
                        alert('請檢查資料格式是否有填錯');
                    }

                }
            }).fail(function (data) {
            });

        } else {
            alert(Msg);
        }



    });

});