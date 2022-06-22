let table = '';

window.onload = function () {
    var url = "ObtenerClientes";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let clientes = JSON.parse(data);
        let total_clientes = clientes.length;

        for (var i = 0; i < clientes.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + clientes[i].Documento.toUpperCase() + '</td>' +
                '<td>' + clientes[i].Nombres.toUpperCase() + '</td>' +
                '<td>' + clientes[i].Apellidos.toUpperCase() + '</td>' +
                '<td>' + clientes[i].Telefono.toUpperCase() + '</td>' +
                '<td>' + clientes[i].Direccion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + clientes[i].IdCliente + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + clientes[i].IdCliente + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Clientes").html(tr);
        $("#spnTotalRegistros").html(total_clientes);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Cliente");
    AbrirModal("modal-form");
}


function GuardarCliente() {

    let varIdCliente = $("#txtId").val();
    let varDocumento = $("#txtDocumento").val();
    let varNombres = $("#txtNombres").val();
    let varApellidos = $("#txtApellidos").val();
    let varDireccion = $("#txtDireccion").val();
    let varTelefono = $("#txtTelefono").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertCliente', {
        'IdCliente': varIdCliente,
        'Documento': varDocumento,
        'Nombres': varNombres,
        'Apellidos': varApellidos,
        'Telefono': varTelefono,
        'Direccion': varDireccion,
        'Tipo': 1,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerClientes");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdCliente) {
    $("#lblTituloModal").html("Editar Cliente");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdCliente': varIdCliente,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let clientes = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(clientes[0].IdCliente);
            $("#txtDocumento").val(clientes[0].Documento);
            $("#txtNombres").val(clientes[0].Nombres);
            $("#txtApellidos").val(clientes[0].Apellidos);
            $("#txtDireccion").val(clientes[0].Direccion);
            $("#txtTelefono").val(clientes[0].Telefono);
            if (clientes[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdCliente) {


    alertify.confirm('Confirmar', '¿Desea eliminar este cliente?', function () {
        $.post("EliminarCliente", { 'IdCliente': varIdCliente }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Cliente Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerClientes");
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




