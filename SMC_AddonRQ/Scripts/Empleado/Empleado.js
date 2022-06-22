let table = '';

window.onload = function () {
    var url = "ObtenerEmpleados";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let empleados = JSON.parse(data);
        let total_empleados = empleados.length;

        for (var i = 0; i < empleados.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + empleados[i].Documento.toUpperCase() + '</td>' +
                '<td>' + empleados[i].Nombres.toUpperCase() + '</td>' +
                '<td>' + empleados[i].Apellidos.toUpperCase() + '</td>' +
                '<td>' + empleados[i].Telefono.toUpperCase() + '</td>' +
                '<td>' + empleados[i].Direccion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + empleados[i].IdEmpleado + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + empleados[i].IdEmpleado + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Empleados").html(tr);
        $("#spnTotalRegistros").html(total_empleados);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Empleado");
    AbrirModal("modal-form");
}


function GuardarEmpleado() {

    let varIdEmpleado = $("#txtId").val();
    let varDocumento = $("#txtDocumento").val();
    let varNombres = $("#txtNombres").val();
    let varApellidos = $("#txtApellidos").val();
    let varDireccion = $("#txtDireccion").val();
    let varTelefono = $("#txtTelefono").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertEmpleado', {
        'IdEmpleado': varIdEmpleado,
        'Documento': varDocumento,
        'Nombres': varNombres,
        'Apellidos': varApellidos,
        'Telefono': varTelefono,
        'Direccion': varDireccion,
        'Tipo': 3,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerEmpleados");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdEmpleado) {
    $("#lblTituloModal").html("Editar Empleado");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdEmpleado': varIdEmpleado,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let empleados = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(empleados[0].IdEmpleado);
            $("#txtDocumento").val(empleados[0].Documento);
            $("#txtNombres").val(empleados[0].Nombres);
            $("#txtApellidos").val(empleados[0].Apellidos);
            $("#txtDireccion").val(empleados[0].Direccion);
            $("#txtTelefono").val(empleados[0].Telefono);
            if (empleados[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdEmpleado) {


    alertify.confirm('Confirmar', '¿Desea eliminar este empleado?', function () {
        $.post("EliminarEmpleado", { 'IdEmpleado': varIdEmpleado }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Empleado Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerEmpleados");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtDocumento").val("");
    $("#txtNombres").val("");
    $("#txtApellidos").val("");
    $("#txtDireccion").val("");
    $("#txtTelefono").val("");
    $("#chkActivo").prop('checked', false);
}




