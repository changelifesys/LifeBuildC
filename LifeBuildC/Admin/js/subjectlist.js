//Load Page
//ExamAdd.aspx
//ExamEdit.aspx
$(function () {

    if (getCookie("CategoryID") == "C1") {
        $('#rdoC1List').prop('checked', true);
    } else if (getCookie("CategoryID") == "C2") {
        $('#rdoC2List').prop('checked', true);
    } else {
        $('#rdoC1List').prop('checked', true);
    }

    $('#rdoC1List').click(function () {

        /*第一段*/
        //判斷是否已經有相同的cookie值。有的話，使舊的cookie過期
        if (document.cookie.indexOf('CategoryID') >= 0) {
            var expD = new Date();
            expD.setTime(expD.getTime() + (-1 * 24 * 60 * 60 * 1000));
            var uexpires = "expires=" + expD.toUTCString();
            document.cookie = "CategoryID=C1; " + uexpires;
        }

        setCookie('CategoryID', 'C1', 1);
        javascript: window.location.reload();
    });

    $('#rdoC2List').click(function () {

        /*第一段*/
        //判斷是否已經有相同的cookie值。有的話，使舊的cookie過期
        if (document.cookie.indexOf('CategoryID') >= 0) {
            var expD = new Date();
            expD.setTime(expD.getTime() + (-1 * 24 * 60 * 60 * 1000));
            var uexpires = "expires=" + expD.toUTCString();
            document.cookie = "CategoryID=C2; " + uexpires;
        }

        setCookie('CategoryID', 'C2', 1);
        javascript: window.location.reload();
    });

    $('#btnAddC1').click(function () {

        /*第一段*/
        //判斷是否已經有相同的cookie值。有的話，使舊的cookie過期
        if (document.cookie.indexOf('CategoryID') >= 0) {
            var expD = new Date();
            expD.setTime(expD.getTime() + (-1 * 24 * 60 * 60 * 1000));
            var uexpires = "expires=" + expD.toUTCString();
            document.cookie = "CategoryID=C1; " + uexpires;
        }

        setCookie('CategoryID', 'C1', 1);
    });

    $('#btnAddC2').click(function () {

        /*第一段*/
        //判斷是否已經有相同的cookie值。有的話，使舊的cookie過期
        if (document.cookie.indexOf('CategoryID') >= 0) {
            var expD = new Date();
            expD.setTime(expD.getTime() + (-1 * 24 * 60 * 60 * 1000));
            var uexpires = "expires=" + expD.toUTCString();
            document.cookie = "CategoryID=C2; " + uexpires;
        }

        setCookie('CategoryID', 'C2', 1);
    });

    //alert(getCookie("CategoryID"));

    function getCookie(cookieName) {
        var name = cookieName + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1);
            if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
        }
        return "";
    }

    function setCookie(cookieName, cookieValue, exdays) {
        if (document.cookie.indexOf(cookieName) >= 0) {
            var expD = new Date();
            expD.setTime(expD.getTime() + (-1 * 24 * 60 * 60 * 1000));
            var uexpires = "expires=" + expD.toUTCString();
            document.cookie = cookieName + "=" + cookieValue + "; " + uexpires;
        }
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cookieName + "=" + cookieValue + "; " + expires;
    }

});

