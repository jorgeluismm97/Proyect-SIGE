// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(function () {
    $("#id_categoria").change(function () {
        if ($(this).val() === "1") {
            $("#id_input").prop("disabled", true);
        } else {
            $("#id_input").prop("disabled", false);
        }
    });
});
