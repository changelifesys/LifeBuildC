$(function () {

    //Url 取得參數

    var strUrl = location.search; //取得:  ?ec=C234
    var getPara, ParaVal;
    var aryPara = [];

    if (strUrl.indexOf("?") != -1) {

        var getSearch = strUrl.split("?");
        getPara = getSearch[1].split("&");

        for (i = 0; i < getPara.length; i++) {
            ParaVal = getPara[i].split("=");
            aryPara.push(ParaVal[0]);
            aryPara[ParaVal[0]] = ParaVal[1];
        }

        //alert(aryPara["ec"]);

    }

    $('#li_1').removeClass();
    $('#li_2').removeClass();
    $('#li_3').removeClass();
    $('#li_4').removeClass();
    $('#li_5').removeClass();


    if (aryPara["ec"] == "C1")
        $('#li_1').addClass('active');
    if (aryPara["ec"] == "C212")
        $('#li_2').addClass('active');
    if (aryPara["ec"] == "C234")
        $('#li_3').addClass('active');
    if (aryPara["ec"] == "Sys")
        $('#li_4').addClass('active');
    if (aryPara["ec"] == "Score")
        $('#li_5').addClass('active');

    /*
    var id = -1; //起始值

    $('li').click(function () {

        var bb = $(this).attr('id');
        var aa = $(this).attr('id').split('_')[1];

        if (id != $(this).attr('id').split('_')[1]) {
            //點選不同列才執行

            $(this).addClass('active');

            //移除先前的 class
            //$('#li_' + id).removeClass();
            //id = $(this).attr('id').split('_')[1];

        }
    });*/

});

