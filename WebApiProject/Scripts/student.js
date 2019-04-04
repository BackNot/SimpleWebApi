$(document).ready(function () {
    var studentObj = ["FirstName", "LastName", "Discipline"];
    
    // Hide fields and form
    $(".post-form-container").hide();
    $(".get-hidden").hide(); // show the id field and hide the others
    $(".delete-hidden").hide();

    // This is GET button in Home/Student
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
        $.ajax("/api/student/", {
            method: "GET",
            dataType: format,
            success: function (data) {
                if (chosenFormat == "XML") {
                    console.log(data);
                    $("#result").append("Please check your console for more information. (F12) \n \n")
                    $(data).find('Student').each(function (index, value) {
                        $("#result").append("&nbsp;[ " + $(this).text() + " ] &nbsp; ")
                    })
                    }
                     else
                    var res = (JSON.stringify(data));
                $("#result").text(res);
            },
             error: function (jqXHR) {
                $("#result").text("Status Code: " + jqXHR.status + "\nStatus Text: " + jqXHR.statusText + "\n" + jqXHR.responseText);
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
        $.ajax("/api/student/" + $('#get-id-operation').val(), {
            method: "GET",
            dataType: format,
            success: function (data) { 
                $("#result").text(" "); // clear it
                if (chosenFormat == "XML") {
                    console.log(data);
                    $("#result").append("Please check your console for more information. (F12) \n \n")
                    $(data).find('Student').each(function (index, value) {
                            $("#result").append("&nbsp;[ " + $(this).text() + " ] &nbsp; ")
                    })
                }
                else
                $("#result").text((JSON.stringify(data)));
            },
            error: function (jqXHR) {
                $("#result").text("Status Code: " + jqXHR.status + "\nStatus Text: " + jqXHR.statusText + "\n" + jqXHR.responseText);
            }
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
            $.ajax("/api/student/" + $('#delete-id-operation').val(), {
                method: "DELETE",
                success: function (data) {
                    $("#result").text("Record deleted.");
                },
                error: function (jqXHR) {
                    $("#result").text("Status Code: " + jqXHR.status + "\nStatus Text: " + jqXHR.statusText + "\n" + jqXHR.responseText);
                }
            })
        })
    })
    // Form submit
    $('#submit-form-btn').click(function (e) {
        e.preventDefault();
        $.ajax("/api/student", {
            method: "POST",
            contentType: "application/x-www-form-urlencoded",
            data: $("#post-form").serialize(),
            success: function () {
                $(".post-form-container").hide();
                alert("Done.")

            },
            error: function (jqXHR) {
                $("#result").text("Status Code: " + jqXHR.status + "\nStatus Text: " + jqXHR.statusText + "\n" + jqXHR.responseText);
            }
        })
    })
})