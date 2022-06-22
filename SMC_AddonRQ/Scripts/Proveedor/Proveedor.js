let table = '';

window.onload = function () {
    var url = "ObtenerProveedores";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let proveedores = JSON.parse(data);
        let total_proveedores = proveedores.length;

        for (var i = 0; i < proveedores.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + proveedores[i].Documento.toUpperCase() + '</td>' +
                '<td>' + proveedores[i].Nombres.toUpperCase() + '</td>' +
                '<td>' + proveedores[i].Apellidos.toUpperCase() + '</td>' +
                '<td>' + proveedores[i].Telefono.toUpperCase() + '</td>' +
                '<td>' + proveedores[i].Direccion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + proveedores[i].IdProveedor + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + proveedores[i].IdProveedor + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Proveedores").html(tr);
        $("#spnTotalRegistros").html(total_proveedores);

        table = $("#table_id").DataTable(lenguaje);

    });

}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Proveedor");
    AbrirModal("modal-form");
}


function GuardarProveedor() {

    let varIdProveedor = $("#txtId").val();
    let varDocumento = $("#txtDocumento").val();
    let varNombres = $("#txtNombres").val();
    let varApellidos = $("#txtApellidos").val();
    let varDireccion = $("#txtDireccion").val();
    let varTelefono = $("#txtTelefono").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertProveedor', {
        'IdProveedor': varIdProveedor,
        'Documento': varDocumento,
        'Nombres': varNombres,
        'Apellidos': varApellidos,
        'Telefono': varTelefono,
        'Direccion': varDireccion,
        'Tipo': 2,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerProveedores");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdProveedor) {
    $("#lblTituloModal").html("Editar Proveedor");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdProveedor': varIdProveedor,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let proveedores = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(proveedores[0].IdProveedor);
            $("#txtDocumento").val(proveedores[0].Documento);
            $("#txtNombres").val(proveedores[0].Nombres);
            $("#txtApellidos").val(proveedores[0].Apellidos);
            $("#txtDireccion").val(proveedores[0].Direccion);
            $("#txtTelefono").val(proveedores[0].Telefono);
            if (proveedores[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdProveedor) {


    alertify.confirm('Confirmar', '¿Desea eliminar este proveedor?', function () {
        $.post("EliminarProveedor", { 'IdProveedor': varIdProveedor }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Proveedor Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerProveedores");
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




