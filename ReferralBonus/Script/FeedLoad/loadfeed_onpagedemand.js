/// <reference path="../jquery-3.0.0.js" />


$(document).ready(function () {
    $("body").append('<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation"><div class="container"><div class="navbar-header"><button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1"><span class="sr-only">Toggle navigation</span><span class="icon-bar"></span> <span class="icon-bar"></span><span class="icon-bar"></span></button><a href="http://spawebcrawler.somee.com/Home/index"><img src="http://spawebcrawler.somee.com/img/logowhite.png" style="width: 185px;padding-top: 7px;" /></a> </div><div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1"><ul class="nav navbar-nav navbar-right"><li class="active"><a style="background-color: #5CB85C;" href="~/home/Index">Home</a></li><li><a href="http://spawebcrawler.somee.com/home/Subscribe">Subscribe</a></li><li><a id="Id_ContactUs" href="#">ContactUs</a></li></ul></div> </div></nav>');
    $("#Id_ContactUs").click(function () {
        $("#mainContent").html('');
    });
    var currentpageNO = 1;
    loaddata(currentpageNO);


    $(window).scroll(function () {
        if($(window).scrollTop()==$(document).height()-$(window).height())
        {
            
            $("#loading-image").show();

            setTimeout(
                function () {
                    currentpageNO += 1;
                    loaddata(currentpageNO);
        }, 1);

          
        } 

    });





    function loaddata(currentpage) {
        $.ajax({
            url: 'http://spawebcrawler.somee.com/Home/GetFeed',
            datatype: 'json',
            method: 'GET',
            data: { pageNumber: currentpage, pageSize: 5 },
            beforeSend: function () {
                $("#loading-image").show();
            },
            success: function (data) {


                $("#loading-image").hide();
                data = JSON.parse(data);

                var FeedTable = $('#mainContent');
               

                $(data).each(function (index, Feeds) {

                    

                    FeedTable.append(' <div itemscope itemtype="http://schema.org/LocalBusiness" class="row"><div class="col-md-3"><a href=' + Feeds.link + ' target="_blank"><img src="http://spawebcrawler.somee.com/img/' + Feeds.image + '" alt="" class="img-responsive imgageGetshare"></a></div><div class="col-md-9"><h3 itemprop="name">' + Feeds.Title + '</h3><p style="white-space: pre-line;">' + Feeds.Description + '</p><a itemprop="url" class="btn btn-success" href=' + Feeds.link + ' target="_blank">Get the deal<span class="glyphicon glyphicon-chevron-right"></span></a></div></div><hr>');




                });

            },
            error: function (err) {
                alert(err);
            }


        });
    }

    window.onerror = function (message, filename, linenumber) {

        alert("JS error: " + message + " on line " + linenumber + " for " + filename);

    }



});