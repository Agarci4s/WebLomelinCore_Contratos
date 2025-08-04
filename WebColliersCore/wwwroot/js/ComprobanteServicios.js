$(document).ready(function () {
    // Oculta todos los formularios al cargar
    $("#CargaAgua").hide();
    $("#CargaLuz").hide();
    $("#CargaPredial").hide();

    // Evento cambio del combo
    $('#cmbTipoServicio').on('change', function () {
        var typeService = parseInt($(this).val(), 10);

        if (typeService === 1) {
            $("#CargaAgua").show();
            $("#CargaLuz").hide();
            $("#CargaPredial").hide();
        } else if (typeService === 2) {
            $("#CargaLuz").show();
            $("#CargaAgua").hide();
            $("#CargaPredial").hide();
        } else if (typeService === 3) {
            $("#CargaPredial").show();
            $("#CargaLuz").hide();
            $("#CargaAgua").hide();
        } else {
            $("#CargaAgua").hide();
            $("#CargaLuz").hide();
            $("#CargaPredial").hide();
        }
    });
});

function changeInmuebleAgua()
{
    var newId = $("#selectInmuebleAgua").val();
    var url = "/ComprobanteServicios/getLocalidades";

    $.getJSON(url, { IdInmueble: newId }, function (data) {
        var item = "";
        $("#selectLocalidadAgua").empty();
        $.each(data, function (i, Localidades) {
            item += '<option value="' + Localidades.value + '">' + Localidades.text + '</option>'
        });
        $("#selectLocalidadAgua").html(item);
    });

}

function changeLocalidadAgua()
{
    var newId = $("#selectCuentaAgua").val();
    var url = "/ComprobanteServicios/getCuentasAgua";

    $.getJSON(url, { IdLocalidad: newId }, function (data) {
        var item = "";
        $("#selectCuentaAgua").empty();
        $.each(data, function (i, cuentasAgua) {
            item += '<option value="' + cuentasAgua.value + '">' + cuentasAgua.text + '</option>'
        });
        $("#selectCuentaAgua").html(item);
    });
}

 function changeInmuebleLuz()
    {
        var newId = $("#selectInmuebleLuz").val();
        var url = "/ComprobanteServicios/getLocalidades";

        $.getJSON(url, { IdInmueble: newId }, function (data) {
            var item = "";
            $("#selectLocalidadLuz").empty();
            $.each(data, function (i, Localidades) {
                item += '<option value="' + Localidades.value + '">' + Localidades.text + '</option>'
            });
            $("#selectLocalidadLuz").html(item);
        });

    }

function changeLocalidadLuz()
{
    var newId = $("#selectCuentaLuz").val();
    var url = "/ComprobanteServicios/getCuentasLuz";

    $.getJSON(url, { IdLocalidad: newId }, function (data) {
        var item = "";
        $("#selectCuentaLuz").empty();
        $.each(data, function (i, cuentasLuz) {
            item += '<option value="' + cuentasLuz.value + '">' + cuentasLuz.text + '</option>'
        });
        $("#selectCuentaLuz").html(item);
    });
}

function changeInmueblePredial() {
    var newId = $("#selectInmuebleLuz").val();
    var url = "/ComprobanteServicios/getLocalidades";

    $.getJSON(url, { IdInmueble: newId }, function (data) {
        var item = "";
        $("#selectLocalidadPredial").empty();
        $.each(data, function (i, Localidades) {
            item += '<option value="' + Localidades.value + '">' + Localidades.text + '</option>'
        });
        $("#selectLocalidadPredial").html(item);
    });

}

function changeLocalidadPredial() {
    var newId = $("#selectCuentaPredial").val();
    var url = "/ComprobanteServicios/getCuentasPredial";

    $.getJSON(url, { IdLocalidad: newId }, function (data) {
        var item = "";
        $("#selectCuentaPredial").empty();
        $.each(data, function (i, cuentasPredial) {
            item += '<option value="' + cuentasPredial.value + '">' + cuentasPredial.text + '</option>'
        });
        $("#selectCuentaPredial").html(item);
    });
}

function calculateTotalImport(typeService) {
    if (typeService == 1) {
        var importeHabitacional = parseFloat($("#PagosAgua_ImporteHabitacional").val()) || 0;
        var importeComercial = parseFloat($("#PagosAgua_ImporteComercial").val()) || 0;
        var gastosEjecucion = parseFloat($("#PagosAgua_GastosEjecucion").val()) || 0;
        var multas = parseFloat($("#PagosAgua_Multas").val()) || 0;
        var recargos = parseFloat($("#PagosAgua_Recargos").val()) || 0;
        var ivaComercial = parseFloat($("#PagosAgua_IvaComercial").val()) || 0;
        var actualizacion = parseFloat($("#PagosAgua_Actualizacion").val()) || 0;

        var total = importeHabitacional + importeComercial + gastosEjecucion + multas + recargos + ivaComercial + actualizacion;

        $("#TotalPago").val(total.toFixed(2));
    }
}

$(document).ready(function () {
    $("#PagosAgua_ImporteHabitacional, #PagosAgua_ImporteComercial, #PagosAgua_GastosEjecucion, #PagosAgua_Multas, #PagosAgua_Recargos, #PagosAgua_IvaComercial, #PagosAgua_Actualizacion")
        .on('input', function () {
            calculateTotalImport(1);
        });
});

function calculateTotalPredial(typeService) {
    if (typeService == 1) {
        var importe = parseFloat($("#PagosPredial_importe").val()) || 0;
        var iva = parseFloat($("#PagosPredial_iva").val()) || 0;
        var recargos = parseFloat($("#PagosPredial_Recargos").val()) || 0;
        var multas = parseFloat($("#PagosPredial_Multas").val()) || 0;
        var actualizacion = parseFloat($("#PagosPredial_Actualizacion").val()) || 0;


        var total = importe + iva + recargos + multas + actualizacion;

        $("#TotalPagoPredial").val(total.toFixed(2));
    }
}

$(document).ready(function () {
    $("#PagosPredial_importe, #PagosPredial_iva, #PagosPredial_Recargos, #PagosPredial_Multas, #PagosPredial_Actualizacion")
        .on('input', function () {
            calculateTotalPredial(1);
        });
});

    
