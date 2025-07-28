$("#CargaAgua").hide();
$("#CargaLuz").hide();
$("#CargaPredial").hide();

$('#cmbTipoSerivicio').on('change', function () {    
    var typeService = parseInt($('#cmbTipoSerivicio').val(), 10);
    console.log(typeService);

    if (typeService === 1) {
        $("#CargaAgua").show();
        $("#CargaLuz").hide();
        $("#CargaPredial").hide();
    }
    else if (typeService === 2) {
        $("#CargaLuz").show();
        $("#CargaAgua").hide();
        $("#CargaPredial").hide();
    }
    else if (typeService === 3) {
        $("#CargaPredial").show();
        $("#CargaLuz").hide();
        $("#CargaAgua").hide();
    }
});

function changeInmuebleAgua() {
    var newId = $("#selectInmuebleAgua").val();
    var url = "/ComprobanteServicios/getLocalidades";

    $.getJSON(url, { id: newId }, function (data) {
        var item = "";
        $("#selectLocalidadAgua").empty();
        $.each(data, function (i, Localidades) {
            item += '<option value="' + Localidades.value + '">' + Localidades.text + '</option>'
        });
        $("#selectLocalidadAgua").html(item);
    });

}


function changeLocalidadAgua() {
    var newId = $("#selectCuentaAgua").val();
    var url = "/ComprobanteServicios/getCuentasAgua";

    $.getJSON(url, { id: newId }, function (data) {
        var item = "";
        $("#selectCuentaAgua").empty();
        $.each(data, function (i, cuentasAgua) {
            item += '<option value="' + cuentasAgua.value + '">' + cuentasAgua.text + '</option>'
        });
        $("#selectCuentaAgua").html(item);
    });

}