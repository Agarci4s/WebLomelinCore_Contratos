function changeArrendatario() {
    var newId = $("#cmbArrendatarios").val();
    var url = "/Localidades/GetNombresComercialesList";

    $.getJSON(url, { id: newId }, function (data) {
        var item = "";
        $("#cmbNombreComercial").empty();
        $.each(data, function (i, Inquilinos) {
            item += '<option value="' + Inquilinos.value + '">' + Inquilinos.text + '</option>';
        });
        $("#cmbNombreComercial").html(item);
    });
}

function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return (key >= 48 && key <= 57);
}