$(document).ready(function(){   
    $('#enviaroracao').click(function () {
        var mail_from = $("#pray_name").val();
        var mail_message = $("#pray_message").val();
        var mail_city = $("#pray_city").val();

        if (mail_from == "") { mail_from = "Anônimo"; }
        if (mail_city == "") { mail_from = "Anônimo"; }

        var mail_body = "Nome: " + mail_from + " | Cidade: " + mail_city + " | Mensagem: " + mail_message;  

        sweetAlert("Já estamos intercedendo por você!");   
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