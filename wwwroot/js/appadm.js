$(document).ready(function () {
    $('#btnSave').prop("disabled", true);    
    $('#tblUsers').DataTable();
});

/****************************************************************/

$('#btnClear').on('click', function () {  
    $('#btnSave').prop("disabled", true);
});

/****************************************************************/

$('#txtName').focusout(function () { checkInputs(); })
$('#txtUser').focusout(function () { checkInputs(); })
$('#txtEmail').focusout(function () { checkInputs(); })
$('#txtPassword').focusout(function () { checkInputs(); })
$('#txtTip').focusout(function () { checkInputs(); })

function checkInputs() {
    if ($('#txtName').val().trim() != ""
        && $('#txtUser').val().trim() != ""
        && $('#txtEmail').val().trim() != ""
        && $('#txtPassword').val().trim() != "") {
        $('#btnSave').prop("disabled", false);
    }
    else {
        $('#btnSave').prop("disabled", true);
    }
}

/****************************************************************/

$('#formNewUser').on('submit', function () {
    event.preventDefault();
    jsLoading(true);

    var name = $('#txtName').val();
    var username = $('#txtUser').val();
    var email = $('#txtEmail').val();
    var password = $('#txtPassword').val();
    var tip = $('#txtTip').val();

    var payload = '{"Name": "' + name + '", "Username": "' + username + '", "Email": "' + email + '", "Password": "' + password + '", "Tip": "' + tip +'"}';    

    $.ajax({
        type: "POST",
        url: "http://sagradacruz.com.br/Admin/User/Save",
        data: JSON.parse(payload),
        success: function () {
            jsLoading(false);
            swal({
                title: "Sucesso",
                text: "Usuário cadastrado com sucesso!",
                type: "success",
                showCancelButton: false
            }, function () {
                window.location.href = "/Admin/User/List";
            });
        }
    })
});

$('#formEditUser').on('submit', function () {
    event.preventDefault();
    jsLoading(true);

    var id = $('#txtId').val();
    var name = $('#txtName').val();
    var username = $('#txtUser').val();
    var email = $('#txtEmail').val();
    var password = $('#txtPassword').val();
    var tip = $('#txtTip').val();

    var payload = '{"Id": ' + id + ', "Name": "' + name + '", "Username": "' + username + '", "Email": "' + email + '", "Password": "' + password + '", "Tip": "' + tip + '"}';

    $.ajax({
        type: "POST",
        url: "http://sagradacruz.com.br/Admin/User/Save/",
        data: JSON.parse(payload),
        success: function () {
            jsLoading(false);
            swal({
                title: "Sucesso",
                text: "Usuário atualizado com sucesso!",
                type: "success",
                showCancelButton: false
            }, function () {
                window.location.href = "/Admin/User/List";
            });
        }
    })
});

function jsDeleteUser(id) {    
    jsLoading(false);

    swal({
        title: "Atenção",
        text: "Deseja realmente excluir este usuário?",
        type: "warning",
        showCancelButton: true
    }, function () {
        window.location.href = "/Admin/User/Delete/" + id;
    });
}
/****************************************************************/