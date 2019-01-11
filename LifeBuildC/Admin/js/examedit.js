//Load Page
//ExamAdd.aspx
//ExamEdit.aspx
$(function () {

    //預覽
    $('#btnView').click(function () {
        ckbField();
        for (var i = 1; i <= 10; i++) {
            getValue(i);
        }
    });

    $('#btnView').trigger('click');

    //返回
    $('#btnCel').click(function () {
        //if (confirm("取消編輯?")) {
        //    $("#txtField_1").text("取消");
        //    $("#txtField_2").text("取消");
        //}

        $("#txtField_1").text("取消");
        $("#txtField_2").text("取消");

    });

    //儲存
    $('#btnSave').click(function () {
        ckbField();
    });

    function ckbField() {
        for (var i = 1; i <= 9 ; i++) {
            for (var j = i + 1; j <= 10; j++) {

                //檢查 textarea 的值
                if ($('#txtField_' + i).val() == ""
                    && $('#txtField_' + j).val() != "") {

                    $('#txtField_' + i).val($('#txtField_' + j).val());
                    $('#txtField_' + j).val('');

                    //檢查 CheckBox 的值
                    if (!$('#ckbIsField_' + i).prop('checked')
                        && $('#ckbIsField_' + j).prop('checked')) {
                        $('#ckbIsField_' + i).prop('checked', true);
                        $('#ckbIsField_' + j).prop('checked', false);
                    }

                }

                //檢查 CheckBox 是否需要勾選
                if ($('#ckbIsField_' + i).prop('checked')
                    && $('#txtField_' + i).val() == "") {
                    $('#ckbIsField_' + i).prop('checked', false);
                }

            }
        }

        //檢查 CheckBox 是否需要勾選
        if ($('#ckbIsField_10').prop('checked')
            && $('#txtField_10').val() == "") {
            $('#ckbIsField_10').prop('checked', false);
        }
    }

    function getValue(id) {

        if ($('#ckbIsField_' + id).prop("checked")) {
            $('#pshow_' + id).attr('style', 'color:red;text-decoration:underline;');
        } else {
            $('#pshow_' + id).removeAttr('style');
        }

        $('#pshow_' + id).text($('#txtField_' + id).val());

    }

    $("#txtSDate12,#txtSDate34,#txtSubStrDate,#txtSubEndDate,#txtSDate5,#txtSDate").datepicker({
        dayNames: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"],
        dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
        monthNames: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        monthNamesShort: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
        prevText: "上月",
        nextText: "次月",
        weekHeader: "週",
        dateFormat: 'yy/mm/dd',
        minDate: 0
    });

});

