$(document).ready(function () {

    // Login field
    $("#submit").click(function (e) {
        e.preventDefault();
        username = $("#username").val();
        password = $("#password").val();
        var credentials = btoa(username + ":" + password); // 
        $.ajax("/api/teacher", { // send a request to api/teacher so we can authenticate user. We have set [BasicAuthentication] attribute.
            method: "GET",
            headers: { // basic auth
                Authorization: "Basic " + credentials //base 64
            },
            success: function () {
                alert("Done.");
                $(".modal").modal('hide');
                $.ajaxSetup({ // set this global header for all ajax calls from now on. We will send the right credentials in every header.
                    beforeSend: function (xhr) { 
                        xhr.setRequestHeader("Authorization", "Basic " + credentials);
                    }});
                
            },
            error: function(){
                alert("Wrong credentials. Please try again.") 
            }
        })
    })


    // Hide fields and form
    $(".post-form-container").hide();
    $(".get-hidden").hide(); // show the id field and hide the others
    $(".delete-hidden").hide();

    // This is GET button in Home/Teacher
    $('#getall-operation').click(function () {
        $("#result").text(""); // clear the result box
        $(".post-form-container").hide();
        $(".get-hidden").hide(); 
        $(".delete-hidden").hide();
        // Determine which format should be used.
        var chosenFormat = $("#formatter").find(":selected").text();
        var format = "json"; // default
        if (chosenFormat == "XML")
            format = "xml";
        // AJAX call
        $.ajax("/api/teacher/", {
            method: "GET",
            dataType: format,
            success: function (data) {
                if (chosenFormat == "XML") {
                    console.log(data);
                    $("#result").append("Please check your console for more information. (F12) \n \n")
                    $(data).find('Teacher').each(function (index, value) {
                        $("#result").append("&nbsp;[ " + $(this).text() + " ] &nbsp; ")
                    })
                    }
                     else
                    var res = (JSON.stringify(data));
                $("#result").text(res);
            },
             error: function (jqXHR) {
                 $("#result").text("Status Code: " + jqXHR.status + "\nStatus Text: " + jqXHR.statusText + "\n" + jqXHR.responseText);
                 if (jqXHR.status=="401") // if status code is 401 - Unauthorized show the form.
                 $(".modal").modal(); 
             }
        })
    })
    // GET #2
    $('#get-operation').click(function () {
        $("#result").text(""); // clear the result box
        $(".get-hidden").show(); // show the id field and hide the others
        $(".delete-hidden").hide();
        $(".post-form-container").hide();
    })// this is the hidden button from above
    $('#get-id-button').click(function () {
        // Determine which format should be used.
        var chosenFormat = $("#formatter").find(":selected").text();
        var format = "json"; // default
        if (chosenFormat == "XML")
            format = "xml";
        // AJAX call
        $.ajax("/api/teacher/" + $('#get-id-operation').val(), {
            method: "GET",
            dataType: format,
            success: function (data) {
                if (chosenFormat == "XML") {
                    console.log(data);
                    $("#result").text(" "); // clear it
                    $("#result").append("Please check your console for more information. (F12) \n \n")
                    $(data).find('Teacher').each(function (index, value) {
                        $("#result").append("&nbsp;[ " + $(this).text() + " ] &nbsp; ")
                    })
                }
                else
                $("#result").text((JSON.stringify(data)));
            },
            error: function (jqXHR) {
                $("#result").text("Status Code: " + jqXHR.status + "\nStatus Text: " + jqXHR.statusText + "\n" + jqXHR.responseText);
                if (jqXHR.status == "401") // if status code is 401 - Unauthorized show the form.
                    $(".modal").modal();             }
        })
    })
    // POST 
    $("#post-operation").click(function () {
        $("#result").text(""); // clear the result box
        $(".get-hidden").hide();
        $(".delete-hidden").hide();
        $(".post-form-container").show();
    })

    // DELETE
    $("#delete-operation").click(function () {
        $("#result").text(""); // clear the result box
        $(".get-hidden").hide();
        $(".delete-hidden").show();
        $(".post-form-container").hide();
        $('#delete-id-button').click(function () {
            $.ajax("/api/teacher/" + $('#delete-id-operation').val(), {
                method: "DELETE",
                success: function (data) {
                    $("#result").text("Record deleted.");
                },
                error: function (jqXHR) {
                    $("#result").text("Status Code: " + jqXHR.status + "\nStatus Text: " + jqXHR.statusText + "\n" + jqXHR.responseText);
                    if (jqXHR.status == "401") // if status code is 401 - Unauthorized show the form.
                        $(".modal").modal();                 }
            })
        })
    })
    // Form submit
    $('#submit-form-btn').click(function (e) {
        e.preventDefault();
        $.ajax("/api/teacher", {
            method: "POST",
            contentType: "application/x-www-form-urlencoded",
            data: $("#post-form").serialize(),
            success: function () {
                $(".post-form-container").hide();
                alert("Done.")
            },
            error: function (jqXHR) {
                $("#result").text("Status Code: " + jqXHR.status + "\nStatus Text: " + jqXHR.statusText + "\n" + jqXHR.responseText);
                if (jqXHR.status == "401") // if status code is 401 - Unauthorized show the form.
                    $(".modal").modal();             }
        })
    })
})