let requestVerificationToken = $("input[name='__RequestVerificationToken']").val();

$("#btnIncluirParcela").click(function () {
    $('#formParcela')[0].reset();
    var today = new Date().toISOString().slice(0, 10);
    $("#dataVencimento").val(today);
    $("#modalParcela").modal("show");
});

$("#formParcela").submit(function (e) {
    e.preventDefault();
    var formAction = $(this).attr("action");
    var parcela = $('form#formParcela').serialize();
    if ($("#formParcela").valid()) {
        $.ajax({
            type: "POST",
            data: parcela,
            url: formAction,
            headers: {
                "RequestVerificationToken": requestVerificationToken
            }
        }).done(function (data, statusText, xhdr) {
            $("#tBodyParcelas").append(data);
            $("#modalParcela").modal("hide");
        }).fail(function (xhdr, statusText, errorText) {
            console.log("Failed: " + errorText);
        });
    }
});

function removerParcela(linha) {
    var i = linha.parentNode.parentNode.rowIndex;
    document.getElementById("tabelaParcela").deleteRow(i);
}

