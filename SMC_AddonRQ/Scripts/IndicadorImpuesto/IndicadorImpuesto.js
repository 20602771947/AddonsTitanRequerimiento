let table = '';


window.onload = function () {
    var url = "ObtenerIndicadorImpuestos";
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

        let indicadorimpuestos = JSON.parse(data);
        let total_indicadorimpuestos = indicadorimpuestos.length;

        for (var i = 0; i < indicadorimpuestos.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + indicadorimpuestos[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + indicadorimpuestos[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + indicadorimpuestos[i].Porcentaje + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + indicadorimpuestos[i].IdIndicadorImpuesto + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + indicadorimpuestos[i].IdIndicadorImpuesto + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_IndicadorImpuestos").html(tr);
        $("#spnTotalRegistros").html(total_indicadorimpuestos);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Indicador de Impuestos");
    AbrirModal("modal-form");
}




function GuardarIndicadorImpuesto() {

    let varIdIndicadorImpuesto = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varPorcentaje = $("#txtPorcentaje").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertIndicadorImpuesto', {
        'IdIndicadorImpuesto': varIdIndicadorImpuesto,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Porcentaje': varPorcentaje,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerIndicadorImpuestos");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdIndicadorImpuesto) {
    $("#lblTituloModal").html("Editar Indicador de Impuesto");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdIndicadorImpuesto': varIdIndicadorImpuesto,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let indicadorimpuesto = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(indicadorimpuesto[0].IdIndicadorImpuesto);
            $("#txtCodigo").val(indicadorimpuesto[0].Codigo);
            $("#txtDescripcion").val(indicadorimpuesto[0].Descripcion);
            $("#txtPorcentaje").val(indicadorimpuesto[0].Porcentaje);
            if (indicadorimpuesto[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdIndicadorImpuesto) {


    alertify.confirm('Confirmar', '¿Desea eliminar este indicador de impuesto?', function () {
        $.post("EliminarIndicadorImpuesto", { 'IdIndicadorImpuesto': varIdIndicadorImpuesto }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Indicador de Impuesto Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerIndicadorImpuestos");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#txtPorcentaje").val("");
    $("#chkActivo").prop('checked', false);
}




