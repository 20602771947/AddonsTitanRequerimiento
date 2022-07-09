let table = '';


window.onload = function () {
    var url = "ObtenerDepartamentos";
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

        let departamentos = JSON.parse(data);
        let total_departamentos = departamentos.length;

        for (var i = 0; i < departamentos.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + departamentos[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + departamentos[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + departamentos[i].IdDepartamento + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + departamentos[i].IdDepartamento + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Departamentos").html(tr);
        $("#spnTotalRegistros").html(total_departamentos);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Departamento");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true);
}




function GuardarDepartamento() {

    let varIdDepartamento = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertDepartamento', {
        'IdDepartamento': varIdDepartamento,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerDepartamentos");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdDepartamento) {
    $("#lblTituloModal").html("Editar Departamento");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdDepartamento': varIdDepartamento,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let departamentos = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(departamentos[0].IdDepartamento);
            $("#txtCodigo").val(departamentos[0].Codigo);
            $("#txtDescripcion").val(departamentos[0].Descripcion);
            if (departamentos[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdDepartamento) {


    alertify.confirm('Confirmar', '¿Desea eliminar este departamento?', function () {
        $.post("EliminarDepartamento", { 'IdDepartamento': varIdDepartamento }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Departamento Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerDepartamentos");
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




