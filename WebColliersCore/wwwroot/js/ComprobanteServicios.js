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

function calculateTotalImport(typeService)
{
    if (typeService == 1) {
        
        var importeHabitacional = $("#PagosAgua_ImporteHabitacional").val();
        var ImporteComercial = $("#PagosAgua_ImporteComercial").val();
        var gastosEjecucion = $("#PagosAgua_GastosEjecucion").val();
        var multas = ("#PagosAgua_Multas").val();
        var recargos = $("#PagosAgua_Recargos").val();
        var ivaComercial = $("#PagosAgua_IvaComercial").val();

        var total = gastosEjecucion + multas + recargos + ivaComercial + importeComercial + importeHabitacional;
        $("#TotalPago").val(total.toFixed(2)); // 2 decimales

        //function onfocus in input text with jqeury
        //disable un input with jquery
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

    

    function changeInmueblePredial()
    {
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

    function changeLocalidadPredial()
    {
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
    
}