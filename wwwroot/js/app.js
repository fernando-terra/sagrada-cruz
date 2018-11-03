$(document).ready(function(){   
    $('#enviaroracao').click(function () {
        var txt_from = $("#pray_name").val();
        var txt_message = $("#pray_message").val();
        var txt_city = $("#pray_city").val();
        
        var payload = { "Author": txt_from, "City": txt_city, "Content": txt_message };

        $.ajax({
            type: "POST",
            url: "http://sagradacruz.com.br/Home/NewPray",            
            data: payload,
            success: function () {
                jsLoading(false);
                swal({
                    title: " ",
                    text: "Já estamos intercedendo por você :)",
                    type: "info",
                    showCancelButton: false
                }, function () {
                    window.location.href = "#pray";
                });
            },
            error: function () {
                swal({
                    title: " ",
                    text: "Infelizmente não foi possível enviar seu pedido de oração :(",
                    type: "error",
                    showCancelButton: false
                });
            }
        })       
    });

    jsCheckDevice();

});

function jsCheckDevice() {
    var device = clientInformation.userAgent.match("/*Win/*");
    if (device == null) {
        /* MOBILE */
        $('#welcomeBannerDesktop').hide();
        $('#welcomeBannerMobile').show();
    }
    else {
        /* DESKTOP */
        $('#welcomeBannerDesktop').show();
        $('#welcomeBannerMobile').hide();
    }
}