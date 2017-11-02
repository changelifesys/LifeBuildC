<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamScore.aspx.cs" Inherits="LifeBuildC.Admin.ExamScore" %>

<%@ Register Src="~/Admin/UserControl/menu.ascx" TagPrefix="uc1" TagName="menu" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <title>考試成績</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/login.css" rel="stylesheet" />
    <style type="text/css">
        .dropdown {
            width: 94px;
            float: left;
        }

        .searchipt {
            margin: 0 95px;
        }
    </style>
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/examscore.js"></script>

</head>
<body>
    <form role="form" runat="server">
        <input id="Hidden1" type="hidden" value='<%=tbscore %>' />
        <div class="container-fluid">
            <div class="row content">
                <!--menu-->
                <uc1:menu runat="server" ID="menu" />
                <!--content-->
                <div class="col-sm-9">

                    <h2>成績查詢</h2>
                    <!--考試日期-->
                    <div class="dropdown">
                        <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown" style="width: 100%">
                            考試日期
    <span class="caret"></span>
                        </button>
                        <ul id="examdate" class="dropdown-menu">
                            <li><a href="#">不分日期</a></li>
                            <script>
                                var shdate = JSON.parse($('#Hidden1').val());
                                var shhtml = '';
                                for (var i = 0; i < shdate.ExamDate.length; i++) {
                                    shhtml += '<li><a href="#">';
                                    shhtml += shdate.ExamDate[i].Edate;
                                    shhtml += '</a></li>';
                                }

                                $('#examdate').append(shhtml);
                            </script>
                            <%--<li><a href="#">2017/03/16</a></li>--%>
                        </ul>
                    </div>
                    <div class="searchipt">
                        <input id="sCreateDate" type="text" class="form-control" name="msg" placeholder="ex：2017/01/01"  style="width: 100%">
                    </div>
                    <!--科目-->
                    <div class="dropdown">
                                <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown" style="width: 100%">
                                    科目
    <span class="caret"></span>
                                </button>
                                <ul id="examec" class="dropdown-menu">
                                    <li><a href="#">不分科</a></li>
                                    <li ><a href="#" name="C1">C1</a></li>
                                    <li ><a href="#" name="C212">C2 一、二課</a></li>
                                    <li ><a href="#" name="C234">C2 三、四課</a></li>
                                </ul>
                            </div>
                    <div class="searchipt">
                        <input id="sExamCategory" type="text" class="form-control" name="msg" placeholder="ex：C1、C212、C234" style="width: 100%">
                    </div>
                    <!--關鍵字-->
                    <div class="input-group">
                        <span class="input-group-addon">關鍵字</span>
                        <input id="sEgroup" type="text" class="form-control" name="msg" placeholder="可搜尋小組、姓名、手機">
                    </div>
                    
                    <!--成績列表-->
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>科目</th>
                                <th>小組</th>
                                <th>姓名</th>
                                <th>手機</th>
                                <th>分數</th>
                                <th>考試時間</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <script>
                                //var aaa = $('#Hidden1').val();
                                var tbscore = JSON.parse($('#Hidden1').val());
                                var tbhtml = '';
                                for (var i = 0; i < tbscore.DataInfo.length; i++) {

                                    tbhtml += '<tr id="tr_' + tbscore.DataInfo[i].USID + '" class="tbscore">';

                                    //科目
                                    tbhtml += '<td class="tbExamCategory">';
                                    tbhtml += tbscore.DataInfo[i].ExamCategory;
                                    tbhtml += '</td>';

                                    //小組
                                    tbhtml += '<td class="tbEgroup">';
                                    tbhtml += tbscore.DataInfo[i].Egroup;
                                    tbhtml += '</td>';

                                    //姓名
                                    tbhtml += '<td class="tbEname">';
                                    tbhtml += tbscore.DataInfo[i].Ename;
                                    tbhtml += '</td>';

                                    //手機
                                    tbhtml += '<td class="tbEmobile">';
                                    tbhtml += tbscore.DataInfo[i].Emobile;
                                    tbhtml += '</td>';

                                    //分數
                                    tbhtml += '<td>';
                                    tbhtml += tbscore.DataInfo[i].EScore;
                                    tbhtml += '</td>';

                                    //考試時間
                                    tbhtml += '<td class="tbCreateDate">';
                                    tbhtml += tbscore.DataInfo[i].CreateDate;
                                    tbhtml += '</td>';

                                    tbhtml += '<td>';
                                    tbhtml += '<input USID="' + tbscore.DataInfo[i].USID + '" class="btn btn-danger" type="button" value="刪除" />';
                                    tbhtml += '</td>';

                                    tbhtml += '</tr>';

                                }

                                $('tbody').append(tbhtml);
                                
                            </script>
                            
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        
        <script>

            $('.btn.btn-danger').click(function () {
                var _usid = $(this).attr("USID");
                var _chk = prompt("請輸入1234後確定刪除", "");
                if (_chk == "1234") {

                    $.ajax({
                        url: 'Api/DelUserScore.aspx',
                        type: 'post',
                        dataType: 'json',
                        data: {
                            id: _usid
                        },
                        success: function (data) {
                            console.log(data);

                            //success content
                            if (data.msg == "success")
                            {
                                $("#tr_" + _usid).remove();
                                alert("刪除成功");
                            }
                                

                        }
                    }).fail(function () {

                    });

                } else if (_chk == "") {
                    alert("必需輸入1234確定刪除");
                } else if (_chk.length > 0) {
                    alert("輸入錯誤");
                }
                
            });
        </script>
        <!--footer-->
        <footer class="container-fluid">
            <p>Copyright© 2017 Change Life Church</p>
        </footer>
    </form>
</body>
</html>
