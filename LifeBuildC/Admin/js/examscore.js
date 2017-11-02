$(function () {

    //考試日期
    $('#examdate li a').click(function () {

        if ($(this).text() == "不分日期")
            $('#sCreateDate').val('');
        else
            $('#sCreateDate').val($(this).text());

        searchscore();
    });

    //科目
    $('#examec li a').click(function () {

        if ($(this).text() == "不分科")
            $('#sExamCategory').val('');
        else
            $('#sExamCategory').val($(this).attr('name'));

        searchscore();
    });

    $('.form-control').keyup(function () {
        searchscore();
    });

    // jQuery methods go here...
    //$('#sEgroup').keyup(function () {
    //    var searchword = $('#sEgroup').val();
    //    if (searchword !== "") {
    //        $('.tbscore').css("display", "none");
    //        $('.tbscore').find(":contains('" + searchword + "')").parent().css("display", ""); //全部搜尋
    //        //$('.tbscore').find(".tbEname:contains('" + searchword + "')").parent().css("display", ""); //單一搜尋姓名

    //    } else {
    //        $('.tbscore').css("display", "");
    //    }
    //});

});

function searchscore() {

    if ($('#sEgroup').val().trim() !== ""
        || $('#sExamCategory').val().trim() !== ""
        || $('#sCreateDate').val().trim() !== "") {

        //先全部清空
        $('.tbscore').css("display", "none");

        //each tr
        $(".table.table-hover tbody tr").each(function () {

            var _seript = 0;
            var _cnt = 0;

            if ($('#sCreateDate').val().trim() != "") {
                _seript++;
            }

            if ($('#sEgroup').val().trim() != "") {
                _seript++;
            }

            if ($('#sExamCategory').val().trim() != "") {
                _seript++;
            }

            $.each(this.cells, function () {

                //科目
                if ($(this).attr('class') == "tbExamCategory"
                    && $(this).html() == $('#sExamCategory').val().trim()) {
                    _cnt++;
                }

                //小組
                if ($(this).attr('class') == "tbEgroup"
                    && $(this).html() == $('#sEgroup').val().trim()) {
                    _cnt++;
                }

                //姓名
                if ($(this).attr('class') == "tbEname"
                    && $(this).html() == $('#sEgroup').val().trim()) {
                    _cnt++;
                }

                //手機
                if ($(this).attr('class') == "tbEmobile"
    && $(this).html() == $('#sEgroup').val().trim()) {
                    _cnt++;
                }

                //考試時間
                if ($(this).attr('class') == "tbCreateDate"
                    && $('#sCreateDate').val().trim() != ""
                     && $(this).text().search($('#sCreateDate').val().trim()) != -1) {
                    _cnt++;
                }

                if (_seript == _cnt)
                    $(this).parent().css("display", "");
                else
                    $(this).parent().css("display", "none");

            });



        });

    } else {
        $('.tbscore').css("display", "");
    }
}

//function searchscore() {

//    if ($('#sEgroup').val().trim() !== ""
//            || $('#sExamCategory').val().trim() !== "") {

//        //先全部清空
//        $('.tbscore').css("display", "none");

//        //each tr
//        $(".table.table-hover tbody tr").each(function () {

//            var _seript = 0;
//            var _cnt = 0;

//            if ($('#sEgroup').val().trim() != "") {
//                _seript++;
//            }

//            if ($('#sExamCategory').val().trim() != "") {
//                _seript++;
//            }

//            $.each(this.cells, function () {

//                //科目
//                if ($(this).attr('class') == "tbExamCategory"
//                    && $(this).html() == $('#sEgroup').val().trim()) {
//                    _cnt++;
//                }

//                //小組
//                if ($(this).attr('class') == "tbEgroup"
//                    && $(this).html() == $('#sEgroup').val().trim()) {
//                    _cnt++;
//                }

//                //姓名
//                if ($(this).attr('class') == "tbEname"
//                    && $(this).html() == $('#sEgroup').val().trim()) {
//                    _cnt++;
//                }

//                //考試時間
//                if ($(this).attr('class') == "tbCreateDate"
//                     && $(this).text().search($('#sExamCategory').val().trim()) != -1) {
//                    _cnt++;
//                }

//                if (_seript == _cnt)
//                    $(this).parent().css("display", "");
//                else
//                    $(this).parent().css("display", "none");

//            });



//        });

//    } else {
//        $('.tbscore').css("display", "");
//    }
//}