$(document).ready(function () {
    // Oculta todos los formularios al cargar
    $("#CargaAgua").hide();
    $("#CargaLuz").hide();
    $("#CargaPredial").hide();
    $("#CargaArchivos").hide();
    $("#CargaManual").hide();
    $("#Filters").hide();
    

    // Evento cambio del combo
    $('#cmbTipoServicio').on('change', function () {
        var typeService = parseInt($(this).val(), 10);
        LoadInmueble();
        if (typeService === 1) {
            $("#Filters").show();
            $("#CargaAgua").show();
            $("#CargaLuz").hide();
            $("#CargaPredial").hide();
        } else if (typeService === 2) {
            $("#Filters").show();
            $("#CargaManual").show(); 
            $("#CargaLuz").show();
            $("#CargaAgua").hide();
            $("#CargaPredial").hide();
        } else if (typeService === 3) {
            $("#Filters").show();
            $("#CargaPredial").show();
            $("#CargaLuz").hide();
            $("#CargaAgua").hide();
        } else {
            $("#Filters").hide();
            $("#CargaAgua").hide();
            $("#CargaLuz").hide();
            $("#CargaPredial").hide();
        }
    });

    $('#cbTipoCarga').change(function () {
        if ($(this).is(':checked')) {
            $("#Filters").hide();
            $("#CargaLuz").hide();
            $("#CargaArchivos").show();
        } else {
            $("#Filters").show();
            $("#CargaLuz").show();
            $("#CargaArchivos").hide();
        }
    });

});

function changeInmueble(IdInmueble)
{
   // var IdInmueble = getIdInmueble();
    var url = "/ComprobanteServicios/getLocalidades";

    $.getJSON(url, { IdInmueble: IdInmueble }, function (data) {
        var item = "";
        $("#datalistIdLocalidad").empty();
        $.each(data, function (i, Localidades) {
            item += '<option data-value="' + Localidades.value + '" value="' + Localidades.text + '">';
        });
        $("#datalistIdLocalidad").html(item);
    });
}

function changeLocalidad(IdLocalidad)
{
    var IdInmueble = getIdInmueble();
    var IdLocalidad = getIdLocalidad();
    var IdServicio = parseInt($("#cmbTipoServicio").val(), 10);

    var url = "/ComprobanteServicios/getCuentas";

    $.getJSON(url, { IdInmueble: IdInmueble, IdLocalidad: IdLocalidad, IdTipoServicio: IdServicio  }, function (data) {
        var item = "";
        $("#datalistIdCuenta").empty();
        $.each(data, function (i, cuentas) {
            item += '<option data-value="' + cuentas.value + '" value="' + cuentas.text + '">';
        });
        $("#datalistIdCuenta").html(item);
    });
}

function getIdLocalidad() {
    var IdLocalidad = null;
    var inputValue = document.getElementById("inputDataListLoc").value;
    var options = document.querySelectorAll('#datalistIdLocalidad option');

    if (options.length > 0) {
        for (var i = 0; i < options.length; i++) {
            var option = options[i];
            if (option.getAttribute('value') === inputValue) {
                IdLocalidad = option.getAttribute('data-value');
                break;
            }
        }
    }

    return parseInt(IdLocalidad);
}

function getIdInmueble()
{
    var IdInmueble = null;
    var inputValue = document.getElementById("inputDataListInm").value;
    var options = document.querySelectorAll('#datalistIdInmueble option');

    if (options.length > 0) {
        for (var i = 0; i < options.length; i++) {
            var option = options[i];
            if (option.getAttribute('value') === inputValue) {
                IdInmueble = option.getAttribute('data-value');
                break;
            }
        }
    }
    return parseInt(IdInmueble);
}

function getIdCuenta() {
    var IdCuenta = null;
    var inputValue = document.getElementById("inputDataListCuentas").value;
    var options = document.querySelectorAll('#datalistIdCuenta option');

    if (options.length > 0) {
        for (var i = 0; i < options.length; i++) {
            var option = options[i];
            if (option.getAttribute('value') === inputValue) {
                IdCuenta = option.getAttribute('data-value');
                break;
            }
        }
    }
    return parseInt(IdCuenta);
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
    $('#formXml').submit(function (e) {
        e.preventDefault(); // Prevent default form submission

        var form = $(this);

        const formElement = document.getElementById('formXml');
        const formData = new FormData(formElement);

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: formData, //$(this).serialize(), // Serialize form data
            processData: false,
            contentType: false,
            //dataType: "json",
            success: function (response) {
                if (response != null) {
                    $("#Filters").show();
                    $("#CargaLuz").show();
                    $("#CargaArchivos").hide();
                    $("#CargaManual").hide();

                    
                    $("#PagosLuz_LecturaActual").val(response.lecturaActual);
                    $("#PagosLuz_LecturaAnterior").val(response.lecturaAnterior);
                    $("#PagosLuz_periodoPago").val(response.periodoPago);
                    $("#PagosLuz_conceptoPago").val(response.conceptoPago);
                    $("#PagosLuz_LineaCaptura").val(response.lineaCaptura);
                    $("#PagosLuz_importe").val(response.importe);
                    $("#PagosLuz_iva").val(response.iva);

                    
                    // var formattedDate = moment(response.fechaPago).format("YYYY-MM-DD");
                    

                    $("#PagosLuz_fechaPago").val(getDateFormat(response.fechaPago));
                    $("#PagosLuz_FechaLimitePago").val(getDateFormat(response.fechaLimitePago));
                    $("#PagosLuz_FechaCorte").val(getDateFormat(response.fechaCorte));
                }
                else {
                    alert("Ocurrio un error al cargar el archivo. Intente nuevamente");
                }
            },
            error: function (error) {
                alert("Ocurrio un error, intente nuevamente");
            }
        });
    });
});

function getDateFormat(date) {
    var anio1 = date.substring(0, 4);
    var month1 = date.substring(5, 7);
    var day1 = date.substring(8, 10);
    var newDate = `${anio1}-${month1}-${day1}`;
    return newDate;
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

function LoadInmueble() {
    var url = "/ComprobanteServicios/getInmuebles";

    $.getJSON(url, { IdRegion: 1 }, function (data) {
        var item = "";

        $("#inputDataListInm").empty();
        $("#datalistIdInmueble").empty();
        $("#inputIdInmueble").empty();
        $.each(data, function (i, inmueble) {
            item += '<option data-value="' + inmueble.value + '" value="' + inmueble.text + '">';
        });
        $("#datalistIdInmueble").html(item);
    });
}

$("#inputDataListInm").on("change paste keyup", function () {

    $("#inputIdInmueble").val('');
    var inputValue = document.getElementById("inputDataListInm").value;
    var options = document.querySelectorAll('#datalistIdInmueble option');

    if (options.length > 0) {
        for (var i = 0; i < options.length; i++) {
            var option = options[i];
            if (option.getAttribute('value') === inputValue) {
                var value = option.getAttribute('data-value');
                $("#inputIdInmueble").val(value);
                changeInmueble(value);
                break;
            }
        }
    }
    else
        $("#inputIdInmueble").focus();
});

$("#inputDataListLoc").on("change paste keyup", function () {

    $("#inputIdLocalidad").val('');
    var inputValue = document.getElementById("inputDataListLoc").value;
    var options = document.querySelectorAll('#datalistIdLocalidad option');

    if (options.length > 0) {
        for (var i = 0; i < options.length; i++) {
            var option = options[i];
            if (option.getAttribute('value') === inputValue) {
                var value = option.getAttribute('data-value');
                $("#inputIdLocalidad").val(value);
                changeLocalidad(value);
                break;
            }
        }
    }
    else
        $("#inputIdLocalidad").focus();
});

$("#inputDataListCuentas").on("change paste keyup", function () {
    $("#inputIdCuenta").val('');
    var inputValue = document.getElementById("inputDataListCuentas").value;
    var options = document.querySelectorAll('#datalistIdCuenta option');

    if (options.length > 0) {
        for (var i = 0; i < options.length; i++) {
            var option = options[i];
            if (option.getAttribute('value') === inputValue) {
                var value = option.getAttribute('data-value');
                $("#inputIdCuenta").val(value);
                break;
            }
        }
    }
    else
        $("#inputIdCuenta").focus();
});


$(document).ready(function () {
    $('#CargaAgua').submit(function (e) {
        e.preventDefault(); // Prevent default form submission
        
        var IdInmueble = getIdInmueble();
        var IdLocalidad = getIdLocalidad();
        var IdCuenta = getIdCuenta();

        var formData = new FormData(this); // Create FormData object from the form

        // Add additional data to the FormData object
        formData.append('PagosAgua.IdInmueble', IdInmueble);
        formData.append('PagosAgua.IdLocalidad', IdLocalidad);
        formData.append('PagosAgua.idCuentaAgua', IdCuenta);

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: formData, // Serialize form data
            processData: false, // Important: Don't process the data
            contentType: false, // Important: Don't set content type
            success: function (response) {
                alert("Se inserto correctamente");
            },
            error: function (error) {
                console.error("Error submitting form:", error);
            }
        });
    });
});

$(document).ready(function () {
    $('#CargaLuz').submit(function (e) {
        e.preventDefault(); // Prevent default form submission

        var IdInmueble = getIdInmueble();
        var IdLocalidad = getIdLocalidad();
        var IdCuenta = getIdCuenta();

        var formData = new FormData(this); // Create FormData object from the form

        // Add additional data to the FormData object
        formData.append('PagosLuz.IdInmueble', IdInmueble);
        formData.append('PagosLuz.IdLocalidad', IdLocalidad);
        formData.append('PagosLuz.idCuentaLuz', IdCuenta);

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: formData, // Serialize form data
            processData: false, // Important: Don't process the data
            contentType: false, // Important: Don't set content type
            success: function (response) {
                alert("Se inserto correctamente");
            },
            error: function (error) {
                console.error("Error submitting form:", error);
            }
        });
    });
});

$(document).ready(function () {
    $('#CargaPredial').submit(function (e) {
        e.preventDefault(); // Prevent default form submission

        var IdInmueble = getIdInmueble();
        var IdLocalidad = getIdLocalidad();
        var IdCuenta = getIdCuenta();

        var formData = new FormData(this); // Create FormData object from the form

        // Add additional data to the FormData object
        formData.append('PagosPredial.IdInmueble', IdInmueble);
        formData.append('PagosPredial.IdLocalidad', IdLocalidad);
        formData.append('PagosPredial.idCuentaPredial', IdCuenta);

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: formData, // Serialize form data
            processData: false, // Important: Don't process the data
            contentType: false, // Important: Don't set content type
            success: function (response) {
                alert("Se inserto correctamente");
            },
            error: function (error) {
                console.error("Error submitting form:", error);
            }
        });
    });
});

    


