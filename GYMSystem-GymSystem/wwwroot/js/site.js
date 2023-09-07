

// hide effect after start 
$(document).ready(
    function () {
        $("#texthide1").hide(1000);
        $(".services-de").hide(1000);
        $("#texthide2").hide(1000);
        $("#texthide3").hide(1000);
        $(".special-type").hide(1000);
        $(".img-effect").hide(1000);
        $(".info p").hide(1000);
        $(".info #couche-name").hide(1000);
        $(".portfolio p:first").hide(1000);
    }
)




// details
$(document).ready(
    function () {
        $("#texthide1").click(
            function () {
                $("#texthide1").hide(1000);
            }
        )
        $("#hideAnim1").click(
            function () {
                $("#texthide1").show(1000);
            }
        )
    }
)
$(document).ready(
    function () {
        $("#texthide2").click(
            function () {
                $("#texthide2").hide(1000);
            }
        )
        $("#hideAnim2").click(
            function () {
                $("#texthide2").show(1000);
            }
        )
    }
)
$(document).ready(
    function () {
        $("#texthide3").click(
            function () {
                $("#texthide3").hide(1000);
            }
        )
        $("#hideAnim3").click(
            function () {
                $("#texthide3").show(1000);
            }
        )
    }
)




// Get the button:

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        $("#myBtn").show(500);
    } else {
        $("#myBtn").hide(500);
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
}




// animation with scroll

$(function () {
    const navLinks = document.querySelector(".navbar")
    "use strict";
    $(window).on("scroll", function () {
        var sc = $(window).scrollTop();
        if (sc > 290) {
            $("#texthide1").show(1000);
            $("#texthide2").show(1000);
            $("#texthide3").show(1000);
        } else {
            $("#texthide1").hide(500);
            $("#texthide2").hide(500);
            $("#texthide3").hide(500);
        }
        if (sc > 1380) {
            $(".portfolio p:first").show(1000);

        } else {
            $(".portfolio p:first").hide(1000);
        }
        if (sc > 1920) {
            $(".info p").show(500);
            $(".info #couche-name").show(250);
        } else {
            $(".info p").hide(1000);
            $(".info #couche-name").hide(1000);
        }
        if (sc > 900) {
            $(".services-de").show(1000);

        }
        if (sc > 600) {
            $(".img-effect").show(1000);
            $(".special-type").show(500);
        } else {

            $(".img-effect").hide(1000);
            $(".services-de").hide(1000);
            $(".special-type").hide(500);
        }
        //if (sc > 0) {
        //    navLinks.classList.remove('bg-transparent')
        //} else {
        //    navLinks.classList.toggle('bg-transparent')
        //}

    })
})

