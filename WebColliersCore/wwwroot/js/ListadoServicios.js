function changeRegion() {
    var newId = $("#IdRegion").val();
    var url = "/ListadoServicios/getInmuebles";

    $.getJSON(url, { IdRegion: newId }, function (data) {
        var item = "";
        $("#selectInmueble").empty();
        $.each(data, function (i, inmueble) {
            item += '<option value="' + inmueble.value + '">' + inmueble.text + '</option>'
        });
        $("#selectInmueble").html(item);
    });
}

function changeInmueble() {
    var newId = $("#IdInmueble").val();
    var url = "/ListadoServicios/getLocalidades";

    $.getJSON(url, { IdInmueble: newId }, function (data) {
        var item = "";
        $("#selectLocalidadAgua").empty();
        $.each(data, function (i, Localidades) {
            item += '<option value="' + Localidades.value + '">' + Localidades.text + '</option>'
        });
        $("#selectLocalidadAgua").html(item);
    });
}

function changeLocalidad() {
    var IdInmueble = $("#IdInmueble").val();
    var IdLocalidad = $("#selectLocalidad").val();
    var IdServicio = $("#selectIdTipoSerivicio").val();
    var url = "/ListadoServicios/getCuentas";

    $.getJSON(url, { IdInmueble: IdInmueble, IdLocalidad: IdLocalidad, IdServicio: IdServicio }, function (data) {
        var item = "";
        $("#selectCuenta").empty();
        $.each(data, function (i, cuentas) {
            item += '<option value="' + cuentas.value + '">' + cuentas.text + '</option>'
        });
        $("#selectCuenta").html(item);
    });
}