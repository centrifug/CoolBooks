﻿


    $(".ReportReview").click(function() {

        var id = event.target.id.split('_')[1];

        $.get("@Url.Content('~/Reviews/Report)/?id=" + id, function(data) {

            console.log("GET");
            $(".ReviewsContainer").find("#ReportReviewText_"+id).text(data);
            $(".ReviewsContainer").find("#ReportReview_"+id).toggleClass("btn-warning");
            $(".ReviewsContainer").find("#ReportReview_"+id).toggleClass("btn-primary");

        });  
    });




    $(".LikeReview").click(function() { 

        var id = event.target.id.split('_')[1];

        @*ajaxGet deprecated*@
        $.get("@Url.Content("~/Reviews/Like")/?id=" + id + "&status=" + $(this).data("status"), function(data) {  
            
            var counters = data.split('/');
            console.log("GET");
            $(".ReviewsContainer").find("#Reviewslikecount_"+id).text(counters[0]);  
            $(".ReviewsContainer").find("#Reviewsdislikecount_"+id).text(counters[1]);  
            $(".ReviewsContainer").find("#LikeReview_"+id).attr('disabled', 'disabled');  
            $(".ReviewsContainer").find("#DislikeReview_"+id).attr('disabled', false);

        });  
    });
    
    $(".DislikeReview").click(function() {  

        var id = event.target.id.split('_')[1];
        console.log("GET");
        @*ajaxGet deprecated*@
        $.get("@Url.Content("~/Reviews/Like")/?id=" + id + "&status=" + $(this).data("status"), function(data) {  
            
            var counters = data.split('/');  

            $(".ReviewsContainer").find("#Reviewslikecount_"+id).text(counters[0]);  
            $(".ReviewsContainer").find("#Reviewsdislikecount_"+id).text(counters[1]);   
            $(".ReviewsContainer").find("#DislikeReview_"+id).attr('disabled', 'disabled');  
            $(".ReviewsContainer").find("#LikeReview_"+id).attr('disabled', false);

        });  
    });  





    $(".like").click(function() { 

        var id = event.target.id.split('_')[1];

        @*ajaxGet deprecated*@
        $.get("@Url.Content("~/Comments/Like")/?id=" + id + "&status=" + $(this).data("status"), function(data) {  
            
            var counters = data.split('/');
            
            $(".CommentsContainer").find("#commentlikecount_"+id).text(counters[0]);  
            $(".CommentsContainer").find("#commentdislikecount_"+id).text(counters[1]);  
            $(".CommentsContainer").find("#like_"+id).attr('disabled', 'disabled');  
            $(".CommentsContainer").find("#dislike_"+id).attr('disabled', false);

        });  
    });
    
    $(".dislike").click(function() {  

        var id = event.target.id.split('_')[1];

        @*ajaxGet deprecated*@
        $.get("@Url.Content("~/Comments/Like")/?id=" + id + "&status=" + $(this).data("status"), function(data) {  
            
            var counters = data.split('/');  

            $(".CommentsContainer").find("#commentlikecount_"+id).text(counters[0]);  
            $(".CommentsContainer").find("#commentdislikecount_"+id).text(counters[1]);   
            $(".CommentsContainer").find("#dislike_"+id).attr('disabled', 'disabled');  
            $(".CommentsContainer").find("#like_"+id).attr('disabled', false);

        });  
    });  


        $(".ReportComment").click(function() {
         
            console.log("Funkar");
            var id = event.target.id.split('_')[1];
            console.log(id);

            $.get("@Url.Content("~/Comments/Report")/?id=" + id, function(data) {            
                console.log("GET SKER");
                $(".CommentsContainer").find("#ReportCommentText_"+id).text(data); 
                $(".CommentsContainer").find("#ReportComment_"+id).toggleClass("btn-warning");
                $(".CommentsContainer").find("#ReportComment_"+id).toggleClass("btn-primary");

            });  
        });     

