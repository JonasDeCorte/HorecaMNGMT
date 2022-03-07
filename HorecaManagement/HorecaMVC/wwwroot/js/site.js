// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function toggleView() {
    var table = document.getElementById("tableView");
    var cards = document.getElementById("cardsView");
    if (table.style.display !== "none") {
        table.style.display = "none";
        cards.style.display = "flex";
    } else {
        table.style.display = "table";
        cards.style.display = "none";
    }
}