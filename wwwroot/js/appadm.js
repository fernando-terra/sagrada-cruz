$(document).ready(function () {
    $('#btnSave').prop("disabled", true);    
});

$('#btnClear').on('click', function () {  
    $('#btnSave').prop("disabled", true);
});

$('#txtName').focusout(function () { checkInputs(); })
$('#txtUser').focusout(function () { checkInputs(); })
$('#txtEmail').focusout(function () { checkInputs(); })
$('#txtPassword').focusout(function () { checkInputs(); })
$('#txtTip').focusout(function () { checkInputs(); })

function checkInputs() {
    if ($('#txtName').val().trim() != ""
        && $('#txtUser').val().trim() != ""
        && $('#txtEmail').val().trim() != ""
        && $('#txtPassword').val().trim() != ""
        && $('#txtTip').val().trim() != "") {
        $('#btnSave').prop("disabled", false);
    }
    else {
        $('#btnSave').prop("disabled", true);
    }
}

$('#formNewUser').on('submit', function () {
    event.preventDefault();

    var name = $('#txtName').val();
    var username = $('#txtUser').val();
    var email = $('#txtEmail').val();
    var password = $('#txtPassword').val();
    var tip = $('#txtTip').val();

    var payload = '{"Name": "' + name + '", "Username": "' + username + '", "Email": "' + email + '", "Password": "' + password + '", "Tip": "' + tip +'"}';    

    $.ajax({
        type: "POST",
        url: "http://sagradacruz.com.br/Admin/User/New",
        data: JSON.parse(payload),
        success: function () {
            sweetAlert("Usuário cadastrado com sucesso");
        }
    })
});

/****************************************************************/