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
function calculateTotalPredial(typeService) {
    if (typeService == 1) {
        var importe = parseFloat($("#PagosPredial_importe").val()) || 0;
        var recargos = parseFloat($("#PagosPredial_Recargos").val()) || 0;
        var multas = parseFloat($("#PagosPredial_Multas").val()) || 0;
        var actualizacion = parseFloat($("#PagosPredial_Actualizacion").val()) || 0;
        var diferencia = parseFloat($("#PagosPredial_Diferencia").val()) || 0;
        var honorarios = parseFloat($("#PagosPredial_Honorarios").val()) || 0;
        var notificacion = parseFloat($("#PagosPredial_Notificacion").val()) || 0;
        var gastoEjecucion = parseFloat($("#PagosPredial_GastoEjecucion").val()) || 0;
        var descuento = parseFloat($("#PagosPredial_Descuento").val()) || 0;

        var suma = importe + recargos + multas + actualizacion + diferencia + honorarios + notificacion + gastoEjecucion;
        var total = suma - descuento;

        $("#TotalPagoPredial").val(total.toFixed(2));
    }
}

$(document).ready(function () {
    $("#PagosAgua_ImporteHabitacional, #PagosAgua_ImporteComercial, #PagosAgua_GastosEjecucion, #PagosAgua_Multas, #PagosAgua_Recargos, #PagosAgua_IvaComercial,  #PagosAgua_Actualizacion")
            
        .on('input', function () {
            calculateTotalImport(1);
        });

    $("#PagosPredial_importe, #PagosPredial_Recargos, #PagosPredial_Multas, #PagosPredial_Actualizacion, #PagosPredial_Diferencia, #PagosPredial_Honorarios, #PagosPredial_Notificacion, #PagosPredial_GastoEjecucion, #PagosPredial_Descuento")
        .on('input', function () {
            calculateTotalPredial(1);
        });
});

//Calculo de diferencia de consumo y consumo diario SERVICIO DE AGUA
const lectura1Input = document.querySelector("#PagosAgua_Lectura1");
const lectura2Input = document.querySelector("#PagosAgua_Lectura2");
const fecha1Input = document.querySelector("#PagosAgua_FechaLectura1");
const fecha2Input = document.querySelector("#PagosAgua_FechaLectura2");

const diferenciaConsumo = document.querySelector("#diferenciaConsumo");
const consumoDiario = document.querySelector("#consumoDiario");

const consumoBimestral = document.querySelector("#PagosAgua_ConsumoBimestral");

function calcularDiasDiferencia() {
    const fecha1 = new Date(fecha1Input.value);
    const fecha2 = new Date(fecha2Input.value);

    if (fecha1Input.value && fecha2Input.value && fecha2 > fecha1) {
        const difTime = Math.abs(fecha2 - fecha1);
        const difDays = Math.ceil(difTime / (1000 * 60 * 60 * 24));
        return difDays;
    }
    return null;
}

function calcularConsumo() {
    const lectura1 = parseFloat(lectura1Input.value);
    const lectura2 = parseFloat(lectura2Input.value);

    const dias = calcularDiasDiferencia();

    if (!isNaN(lectura1) && !isNaN(lectura2) && dias !== null && dias > 0) {
        const diferencia = lectura2 - lectura1;
        const consumoDiarioValor = dias / diferencia;
        //consumoBimestral
       const consumoBimestralValor = consumoDiarioValor * 60; 

        diferenciaConsumo.value = diferencia.toFixed(2);
        consumoDiario.value = consumoDiarioValor.toFixed(2);
        consumoBimestral.value = consumoBimestralValor.toFixed(2);
       

    } else {
        diferenciaConsumo.value = "";
        consumoDiario.value = "";
        consumoBimestral.value = "";
    }
}
[lectura1Input, lectura2Input, fecha1Input, fecha2Input].forEach(input => {
    if (!input)  return;
    input.addEventListener("input", calcularConsumo);
    input.addEventListener("change", calcularConsumo);
});

//DIFERENCIA DE CONSUMO SERVICIO LUZ
const lecturaActualInput = document.querySelector("#PagosLuz_LecturaActual");
const lecturaAnteriorInput = document.querySelector("#PagosLuz_LecturaAnterior");

const diferenciaCosumoLuz = document.querySelector("#diferenciaConsumoLuz");

function calcularConsumoLuz() {
    const lecturaActual = parseFloat(lecturaActualInput.value);
    const lecturaAnterior = parseFloat(lecturaAnteriorInput.value);

    if (!isNaN(lecturaActual) && !isNaN(lecturaAnterior)) {
        const diferenciaLuz = lecturaActual - lecturaAnterior;

        diferenciaCosumoLuz.value = diferenciaLuz.toFixed(2);
    } else {
        diferenciaCosumoLuz.value = "";
    }
}
[lecturaActualInput, lecturaAnteriorInput].forEach(input => {
    if (!input) return;
    input.addEventListener("input", calcularConsumoLuz);
    input.addEventListener("change", calcularConsumoLuz);
});



    
