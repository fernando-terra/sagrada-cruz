$(window).on('load', function () {
    $("#loader").fadeOut("slow");
});

function jsLoading(status) {
    if (status) {
        $('#loader').fadeIn("slow");
    }
    else {
        $('#loader').hide();
    }
}
