let table = '';


window.onload = function () {
    var url = "ObtenerSeries";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        //console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let series = JSON.parse(data);
        let total_series = series.length;

        for (var i = 0; i < series.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + series[i].Serie.toUpperCase() + '</td>' +
                '<td>' + series[i].NumeroInicial + '</td>' +
                '<td>' + series[i].NumeroFinal + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + series[i].IdSerie + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + series[i].IdSerie + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Series").html(tr);
        $("#spnTotalRegistros").html(total_series);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Serie");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true);
}




function GuardarSerie() {

    let varIdSerie = $("#txtId").val();
    let varSerie = $("#txtSerie").val();
    let varNumeroInicial = $("#txtNumeroInicial").val();
    let varNumeroFinal = $("#txtNumeroFinal").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertSerie', {
        'IdSerie': varIdSerie,
        'Serie': varSerie,
        'NumeroInicial': varNumeroInicial,
        'NumeroFinal': varNumeroFinal,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerSeries");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdSerie) {
    $("#lblTituloModal").html("Editar Serie");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdSerie': varIdSerie,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let serie = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(serie[0].IdSerie);
            $("#txtSerie").val(serie[0].Serie);
            $("#txtNumeroInicial").val(serie[0].NumeroInicial);
            $("#txtNumeroFinal").val(serie[0].NumeroFinal);
            if (serie[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdSerie) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta serie?', function () {
        $.post("EliminarSerie", { 'IdSerie': varIdSerie }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Serie Eliminada", "success")
                table.destroy();
                ConsultaServidor("ObtenerSeries");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtSerie").val("");
    $("#txtNumeroInicial").val("");
    $("#txtNumeroFinal").val("");
    $("#chkActivo").prop('checked', false);
}




