/// <reference path="../jquery-3.0.0.js" />


$(document).ready(function () {
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
            url: 'http://localhost:26788/Home/GetFeedOnDemand.asmx/GetFeed',
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

                    
                    FeedTable.append(' <div itemscope itemtype="http://schema.org/LocalBusiness" class="row"><div class="col-md-3"><a href=' + Feeds.link + ' target="_blank"><img src="/img/' + Feeds.image + '" alt="" class="img-responsive imgageGetshare"></a></div><div class="col-md-9"><h3 itemprop="name">' + Feeds.Title + '</h3><p style="white-space: pre-line;">' + Feeds.Description + '</p><a itemprop="url" class="btn btn-success" href=' + Feeds.link + ' target="_blank">Get the deal<span class="glyphicon glyphicon-chevron-right"></span></a></div></div><hr>');
                



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