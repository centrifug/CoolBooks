// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Sökfunktion för authors i create-book
$(document).ready(function () {
    $('#filterAuthor').keyup(function () {
        var query = this.value;

        $(".author").each(function () {

            $(this).hide();
            if ($(this).text().toLowerCase().indexOf(query.toLowerCase()) != -1) {
                $(this).show();
            }
        });

    });
});

// Sökfunktion för Genre i create-book
$(document).ready(function () {
    $('#filterGenre').keyup(function () {
        var query = this.value;

        $(".genre").each(function () {

            $(this).hide();
            if ($(this).text().toLowerCase().indexOf(query.toLowerCase()) != -1) {
                $(this).show();
            }
        });

    });
});