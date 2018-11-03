$(function () {
    $("#btnNewStatement").click(function () {
        $("#newStatementModal").modal();
    });
});

function jsNewStatement() {
    var author = $('#nameStatement').val();
    var content = $('#textStatement').val();
    var city = $('#cityStatement').val();

    var payload = {
        "Author": author,
        "Content": content,
        "City": city
    }

    $.ajax({
        type: "POST",
        url: "http://localhost:57541/Admin/Statement/New",
        data: payload,
        success: function () {
            jsLoading(false);
            $('#newStatementModal').modal('toggle');
            swal({
                title: " ",
                text: "Obrigado pelo seu depoimento. Em breve estará em nossa página :)",
                type: "success",
                showCancelButton: false
            }, function () {
                window.location.href = "#statement";
            });
        },
        error: function () {
            $('#newStatementModal').modal('toggle');
            swal({
                title: " ",
                text: "Infelizmente não foi possível enviar seu depoimento :(",
                type: "error",
                showCancelButton: false
            });
        }
    });
}

function jsFlowStatement(id, approve) {
    console.log("id: " + id + " approve: " + approve);
   
    var payload = {
        "id": id,
        "approved": approve
    }

    $.ajax({
        type: "POST",
        url: "http://localhost:57541/Admin/Statement/Flow",
        data: payload,
        success: function () {
            swal({
                title: " ",
                text: "Depoimento atualizado com sucesso! :)",
                type: "success",
                showCancelButton: false
            }, function () {
                window.location.href = "/Admin/Statement/Index";
            });
        },
        error: function () {
            swal({
                title: " ",
                text: "Não foi possível atualizar o status do depoimento :(",
                type: "error",
                showCancelButton: false
            });
        }
    });
}