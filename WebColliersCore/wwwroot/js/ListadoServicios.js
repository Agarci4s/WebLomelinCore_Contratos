$("#div_Agua").hide();
$("#div_Luz").hide();
$("#div_Predial").hide();

$("#formAccionAgua").hide();
$("#formAccionLuz").hide();
$("#formAccionPredial").hide();

$(document).ready(function () {
    $('#frmBusqueda').submit(function (e) {
        e.preventDefault(); // Prevent default form submission
        var tipoServicio = parseInt($('#selectIdTipoSerivicio').val(), 10);

        $("#div_Agua").empty();
        $("#div_Luz").empty();
        $("#div_Predial").empty();

        $("#div_Agua").hide();
        $("#div_Luz").hide();
        $("#div_Predial").hide();

        $("#formAccionAgua").hide();
        $("#formAccionLuz").hide();
        $("#formAccionPredial").hide();

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: $(this).serialize(), // Serialize form data
            success: function (response) {
                if (tipoServicio == 1) {
                    $('#div_Agua').html(response);
                    $("#div_Agua").show();
                    $("#formAccionAgua").show();
                }
                else if (tipoServicio == 2) {
                    $('#div_Luz').html(response);
                    $("#div_Luz").show();
                    $("#formAccionLuz").show();
                }
                else if (tipoServicio == 3) {                    
                    $('#div_Predial').html(response);
                    $("#div_Predial").show();
                    $("#formAccionPredial").show();
                }
            },
            error: function (error) {
                console.error("Error submitting form:", error);
            }
        });
    });
});

function changeRegion() {
    var newId = $("#selectRegion").val();
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
    var newId = $("#selectInmueble").val();
    var url = "/ListadoServicios/getLocalidades";

    $.getJSON(url, { IdInmueble: newId }, function (data) {
        var item = "";
        $("#selectLocalidad").empty();
        $.each(data, function (i, Localidades) {
            item += '<option value="' + Localidades.value + '">' + Localidades.text + '</option>'
        });
        $("#selectLocalidad").html(item);
    });
}

function changeLocalidad() {
    var IdInmueble = $("#selectInmueble").val();
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


$(document).ready(function () {
    $('#formAccionAgua').submit(function (e) {
        e.preventDefault(); // Prevent default form submission

        var tableData = [];

        $("#tableData tbody tr").each(function () {
            try {
                var row = $(this);

                const isChecked = row.find('.form-check-input').is(':checked');
                const id = row.find('.form-id').val();
                var NewStatus = $("#selectNewIdEstatusAgua").val();

                var rowObject = {
                    idPagoAgua: parseInt(id),
                    EsSeleccionado: isChecked,
                    StatusProceso: NewStatus
                };
                tableData.push(rowObject);

            } catch (e) {
                console.log(e);
            }
        });

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: JSON.stringify(tableData),// $(this).serialize(), // Serialize form data
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert("Los estatus se actualizaron correctamente");
            },
            error: function (error) {
                alert("Ocurrio un error, intente nuevamente");
            }
        });
    });
});


$(document).ready(function () {
    $('#formAccionLuz').submit(function (e) {
        e.preventDefault(); // Prevent default form submission

        var tableData = [];

        $("#tableData tbody tr").each(function () {
            try {
                var row = $(this);

                const isChecked = row.find('.form-check-input').is(':checked');
                const id = row.find('.form-id').val();
                var NewStatus = $("#selectNewIdEstatusLuz").val();

                var rowObject = {
                    idPagoLuz: parseInt(id),
                    EsSeleccionado: isChecked,
                    StatusProceso: NewStatus
                };
                tableData.push(rowObject);

            } catch (e) {
                console.log(e);
            }
        });

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: JSON.stringify(tableData),// $(this).serialize(), // Serialize form data
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert("Los estatus se actualizaron correctamente");
            },
            error: function (error) {
                alert("Ocurrio un error, intente nuevamente");
            }
        });
    });
});

$(document).ready(function () {
    $('#formAccionPredial').submit(function (e) {
        e.preventDefault(); // Prevent default form submission

        var tableData = [];

        $("#tableData tbody tr").each(function () {
            try {
                var row = $(this);

                const isChecked = row.find('.form-check-input').is(':checked');
                const id = row.find('.form-id').val();
                var NewStatus = $("#selectNewIdEstatusPredial").val();

                var rowObject = {
                    idPagoPredial: parseInt(id),
                    EsSeleccionado: isChecked,
                    StatusProceso: NewStatus
                };
                tableData.push(rowObject);

            } catch (e) {
                console.log(e);
            }
        });

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: JSON.stringify(tableData),// $(this).serialize(), // Serialize form data
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                alert("Los estatus se actualizaron correctamente");
            },
            error: function (error) {
                alert("Ocurrio un error, intente nuevamente");
            }
        });
    });
});
