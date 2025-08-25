$(document).ready(function () {
    $('#frmIcoi').submit(function (e) {
        e.preventDefault(); // Prevent default form submission

        var tableData = [];

        $("#tableData tbody tr").each(function () {
            try {
                var row = $(this);

                const isChecked = row.find('.form-check-input').is(':checked');
                const id = row.find('.form-id').val();

                var rowObject = {
                    IdRegistro: parseInt(id),
                    EnvioIcoi: isChecked
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
                alert("Se enviaron los registros correctamente");
            },
            error: function (error) {
                alert("Ocurrio un error, intente nuevamente");
            }
        });
    });
});