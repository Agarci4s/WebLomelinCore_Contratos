$('#cmbTipoSerivicio').on('change', function () {
    $('#TipoCarga').text('Carga manual');
    $("#CargaArchivos").show();
    $("#CargaLuz").hide();
    $("#CargaPredial").hide();
    $("#CargaAgua").hide();
    $("#CargaAgua").hide();
    $('#cbTipoCarga').prop('checked', false);
});

$('#TipoCarga').text('Carga manual');
$("#CargaArchivos").show();
$("#CargaLuz").hide();
$("#CargaPredial").hide();
$("#CargaAgua").hide();
const checkbox = document.getElementById('cbTipoCarga');

checkbox.addEventListener('change', function () {
    if (this.checked) {
        $('#TipoCarga').text('Carga de archivo');
        var typeService = parseInt($('#cmbTipoSerivicio').val(), 10);
        console.log(typeService);

        $("#CargaArchivos").hide();
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
    }
    else {
        $('#TipoCarga').text('Carga manual');
        $("#CargaArchivos").show();
        $("#CargaLuz").hide();
        $("#CargaPredial").hide();
        $("#CargaAgua").hide();
    }
});