<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="LifeBuildC.Admin.Demo" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Bootstrap Example</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style>
        /* Set height of the grid so .sidenav can be 100% (adjust if needed) */
        .row.content {
            height: 1500px;
        }

        /* Set gray background color and 100% height */
        .sidenav {
            background-color: #f1f1f1;
            height: 100%;
        }

        /* Set black background color, white text and some padding */
        footer {
            background-color: #555;
            color: white;
            padding: 15px;
        }

        /* On small screens, set height to 'auto' for sidenav and grid */
        @media screen and (max-width: 767px) {
            .sidenav {
                height: auto;
                padding: 15px;
            }

            .row.content {
                height: auto;
            }
        }
    </style>
</head>
<body>

    <div class="container-fluid">
        <div class="row content">
            <!--menu-->
            <div class="col-sm-3 sidenav">
                <h4>生命建造程序</h4>
                <ul class="nav nav-pills nav-stacked">
                    <li class="active"><a href="#section1">考試題庫</a></li>
                    <li><a href="#section2">系統設定</a></li>
                </ul>
                <br>
                
            </div>
            <!--content-->
            <div class="col-sm-9">

                <h4><small>RECENT POSTS</small></h4>
                <hr>
                <h2>Officially Blogging</h2>
                <h5><span class="glyphicon glyphicon-time"></span>Post by John Doe, Sep 24, 2015.</h5>
                <h5><span class="label label-success">Lorem</span> <span class="label label-danger">Food</span> <span class="label label-primary">Ipsum</span></h5>
                <br>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
                <hr>

                <h4>Leave a Comment:</h4>
                <form role="form">
                    <div class="form-group">
                        <textarea class="form-control" rows="3" required></textarea>
                    </div>
                    <button type="submit" class="btn btn-success">Submit</button>
                </form>
                <br>
                <br>

            </div>
        </div>
    </div>

    <!--footer-->
    <footer class="container-fluid">
        <p>Footer Text</p>
    </footer>

</body>
</html>
