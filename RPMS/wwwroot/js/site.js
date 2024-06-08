// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const mainNavLinks = document.querySelectorAll("#main-nav .nav-item");

const clearActiveNavLink = () => {
    mainNavLinks.forEach((navLinks) => {
        navLinks.childNodes[1].classList.remove("active");
    });
}

clearActiveNavLink();

if (window.location.href.toLowerCase().includes("resident")) {
    var link = mainNavLinks[0].childNodes[1];
    link.classList.add("active");

}

if (window.location.href.toLowerCase().includes("address") || window.location.href.toLowerCase().includes("street")) {
    var link = mainNavLinks[1].childNodes[1];
    link.classList.add("active");

}
if (window.location.href.toLowerCase().includes("settings")) {
    var link = mainNavLinks[2].childNodes[1];
    link.classList.add("active");

}
