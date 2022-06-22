let table = '';


window.onload = function () {
    var url = "ObtenerCentroCostos";
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

        let centrocostos = JSON.parse(data);
        let total_centrocostos = centrocostos.length;

        for (var i = 0; i < centrocostos.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + centrocostos[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + centrocostos[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + centrocostos[i].IdCentroCosto + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + centrocostos[i].IdCentroCosto + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_CentroCostos").html(tr);
        $("#spnTotalRegistros").html(total_centrocostos);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Centro de Costo");
    AbrirModal("modal-form");
}




function GuardarCentroCosto() {

    let varIdCentroCosto = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertCentroCosto', {
        'IdCentroCosto': varIdCentroCosto,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerCentroCostos");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdCentroCosto) {
    $("#lblTituloModal").html("Editar Centro de Costo");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdCentroCosto': varIdCentroCosto,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let centrocosto = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(centrocosto[0].IdCentroCosto);
            $("#txtCodigo").val(centrocosto[0].Codigo);
            $("#txtDescripcion").val(centrocosto[0].Descripcion);
            if (centrocosto[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdCentroCosto) {


    alertify.confirm('Confirmar', '¿Desea eliminar este centro de costo?', function () {
        $.post("EliminarCentroCosto", { 'IdCentroCosto': varIdCentroCosto }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Centro de Costo Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerCentroCostos");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#chkActivo").prop('checked', false);
}




