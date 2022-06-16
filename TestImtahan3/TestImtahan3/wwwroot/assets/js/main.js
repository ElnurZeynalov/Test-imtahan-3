$(".menu-btn").click(function() {
    $(".nav-bar").css({
        display: "flex"
    })
    $(".menu-x-btn").css({
        display: "block"
    })
    $(".menu-btn").css({
        display: "none"
    })
    console.log("qewfqef")
});
$(".menu-x-btn").click(function() {
    $(".nav-bar").css({
        display: "none"
    })
    $(".menu-x-btn").css({
        display: "none"
    })
    $(".menu-btn").css({
        display: "block"
    })
    console.log("qewfqef")
});