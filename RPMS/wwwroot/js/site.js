// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// When Hamburger menu button is toggled


$("#hamburger-menu").click(() => {
    console.log("clicked");
    $("#side-nav").toggleClass("show");
    $("#close-menu").addClass("show");
 })

$("#close-menu").click(() => {
    $("#side-nav").toggleClass("show");
})
