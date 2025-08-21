function changeRegion() {
    var newId = $("#selectRegion").val();
    var url = "/Facturas/getInmuebles";

    $.getJSON(url, { IdRegion: newId }, function (data) {
        var item = "";
        $("#selectInmueble").empty();
        $.each(data, function (i, inmueble) {
            item += '<option value="' + inmueble.value + '">' + inmueble.text + '</option>'
        });
        $("#selectInmueble").html(item);
    });
}


$(document).ready(function () {
    $("#frmBusqueda").submit(function (e) {
        e.preventDefault(); // Evita que el formulario recargue la página

        $.ajax({
            url: $(this).attr('action'),
            method: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                $("#tableData").html(response); // Renderiza la tabla en el div
            },
            error: function () {
                alert("Error al cargar los datos.");
            }
        });
    });
});