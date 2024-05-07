// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Updates the first item of the carousel to be the active item of the carousel of document ready.
$(document).ready(function () {
    $('#carouselExampleCaptions').find('.carousel-item').first().addClass('active');
});