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

        var IdLocalidad = null;
        inputValue = document.getElementById("inputDataListLoc").value;
        options = document.querySelectorAll('#datalistIdLocalidad option');

        if (options.length > 0) {
            for (var i = 0; i < options.length; i++) {
                var option = options[i];
                if (option.getAttribute('value') === inputValue) {
                    IdLocalidad = option.getAttribute('data-value');
                    break;
                }
            }
        }

        var IdCuenta = null;
        inputValue = document.getElementById("inputDataListCuentas").value;
        options = document.querySelectorAll('#datalistIdCuenta option');

        if (options.length > 0) {
            for (var i = 0; i < options.length; i++) {
                var option = options[i];
                if (option.getAttribute('value') === inputValue) {
                    IdCuenta = option.getAttribute('data-value');
                    break;
                }
            }
        }

        var rowObject = {
            IdInmueble: IdInmueble,
            IdLocalidad: IdLocalidad,
            IdTipoServicio: tipoServicio,
            IdStatusProceso: parseInt($("#selectIdEstatus").val()),
            IdCuentaServicio: IdCuenta
        };

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: rowObject, // Serialize form data
            //contentType: "application/json; charset=utf-8",
            //dataType: "json",
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
        
        $("#inputDataListInm").empty();
        $("#datalistIdInmueble").empty();
        $("#inputIdInmueble").empty();
        $.each(data, function (i, inmueble) {
            item += '<option data-value="' + inmueble.value + '" value="' + inmueble.text + '">';
        });
        $("#datalistIdInmueble").html(item);
    });
}

function changeInmueble(idInmueble) {

    var url = "/ListadoServicios/getLocalidades";

    $.getJSON(url, { IdInmueble: idInmueble }, function (data) {
        var item = "";
        $("#datalistIdLocalidad").empty();
        $.each(data, function (i, Localidades) {           
            item += '<option data-value="' + Localidades.value + '" value="' + Localidades.text + '">';
        });
        $("#datalistIdLocalidad").html(item);
    });
}

function changeLocalidad(idLocalidad) {
    var IdInmueble = $("#inputIdInmueble").val();
    IdInmueble = $("#datalistIdInmueble").val();

    var inputValue = document.getElementById("inputDataListInm").value;
    var options = document.querySelectorAll('#datalistIdInmueble option');

    if (options.length > 0) {
        for (var i = 0; i < options.length; i++) {
            var option = options[i];
            if (option.getAttribute('value') === inputValue) {
                IdInmueble = option.getAttribute('data-value');
               // $("#inputIdLocalidad").val(value);
               // changeLocalidad(value);
                break;
            }
        }
    }
   

    var IdLocalidad = idLocalidad;// $("#selectLocalidad").val();
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