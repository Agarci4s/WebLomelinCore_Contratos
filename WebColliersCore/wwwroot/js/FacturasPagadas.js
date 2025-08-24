function changeRegion() {
    var newId = $("#selectRegion").val();
    var url = "/Facturas/getInmuebles";

    $.getJSON(url, { IdRegion: newId }, function (data) {
        var item = "";
        $("#datalistIdInmueble").empty();
        $.each(data, function (i, inmueble) {
            item += '<option data-value="' + inmueble.value + '">' + inmueble.text + '</option>'
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
                //changeInmueble(value);
                break;
            }
        }
    }
    else
        $("#inputIdInmueble").focus();
});

function getIdInmueble() {
    var IdInmueble = 0;
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

$(document).ready(function () {
    $("#frmBusqueda").submit(function (e) {
        e.preventDefault(); // Evita que el formulario recargue la página

        var IdInmueble = getIdInmueble();
        var formData = new FormData(this); // Create FormData object from the form

        // Add additional data to the FormData object
        formData.append('IdInmueble', IdInmueble);

        $.ajax({
            url: $(this).attr('action'), // Get action URL from the form
            type: 'POST',
            data: formData, // Serialize form data
            processData: false, // Important: Don't process the data
            contentType: false, // Important: Don't set content type
            success: function (response) {
                $("#tableData").html(response); // Renderiza la tabla en el div
            },
            error: function () {
                alert("Error al cargar los datos.");
            }
        });
    });

    // Acción del botón "Descargar"
    $('#btnDescargar').click(function () {
        var IdRegion = $('#selectRegion').val();
        var IdInmueble = getIdInmueble();
        var IdConcepto = $('#selectIdConcepto').val();

        // Construir y enviar un formulario temporal
        var form = $('<form>', {
            method: 'POST',
            action: $('#urlDescargar').val()
        });

        form.append($('<input>', { type: 'hidden', name: 'IdRegion', value: IdRegion }));
        form.append($('<input>', { type: 'hidden', name: 'IdInmueble', value: IdInmueble }));
        form.append($('<input>', { type: 'hidden', name: 'IdConcepto', value: IdConcepto }));

        $('body').append(form);
        form.submit();
        form.remove();
    });
});


//function Descargar() {
    
//    var IdInmueble = getIdInmueble();

//    let form = document.getElementById("frmBusqueda");
//    let formData = new FormData(form);

   
//    const urlInput = document.getElementById('urlDescargar');
//    const urlValue = urlInput.value;

//    $.ajax({
//        url: urlValue, // Get action URL from the form
//        type: 'POST',
//        data: formData, // Serialize form data
//        processData: false, // Important: Don't process the data
//        contentType: false, // Important: Don't set content type
//        success: function (response) {
//           // $("#tableData").html(response); // Renderiza la tabla en el div
//        },
//        error: function () {
//            alert("Error al cargar los datos.");
//        }
//    });
//}