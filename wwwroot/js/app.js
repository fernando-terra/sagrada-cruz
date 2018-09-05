$(document).ready(function(){   
    $('#enviaroracao').click(function () {
        var mail_from = $("#pray_name").val();
        var mail_message = $("#pray_message").val();
        var mail_city = $("#pray_city").val();

        if (mail_from == "") { mail_from = "Anônimo"; }
        if (mail_city == "") { mail_from = "Anônimo"; }

        var mail_body = "Nome: " + mail_from + " | Cidade: " + mail_city + " | Mensagem: " + mail_message;  

        sweetAlert("Já estamos intercedendo por você!");

        //$.ajax({
        //    type: "POST",
        //    url: "http://34.220.216.121/php/mail.php",
        //    data: "body=" + mail_body,
        //    success: function () {
        //        sweetAlert("Já estamos intercedendo por você!");
        //    }
        //});        
    });
});