let table = '';


window.onload = function () {
    var url = "ObtenerSucursales";
    ConsultaServidor(url);
};


function ConsultaServidor(url) {

    $.post(url, function (data, status) {

        console.log(data);
        if (data == "error") {
            table = $("#table_id").DataTable(lenguaje);
            return;
        }

        let tr = '';

        let sucursales = JSON.parse(data);
        let total_sucursales = sucursales.length;

        for (var i = 0; i < sucursales.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + sucursales[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + sucursales[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + sucursales[i].Direccion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + sucursales[i].IdSucursal + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + sucursales[i].IdSucursal + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Sucursales").html(tr);
        $("#spnTotalRegistros").html(total_sucursales);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Sucursal");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true);
}




function GuardarSucursal() {

    let varIdSucursal = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varDireccion = $("#txtDireccion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertSucursal', {
        'IdSucursal': varIdSucursal,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Direccion': varDireccion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerSucursales");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdSucursal) {
    $("#lblTituloModal").html("Editar Almacen");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdSucursal': varIdSucursal,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let sucursales = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(sucursales[0].IdSucursal);
            $("#txtCodigo").val(sucursales[0].Codigo);
            $("#txtDescripcion").val(sucursales[0].Descripcion);
            $("#txtDireccion").val(sucursales[0].Direccion);
            if (sucursales[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdSucursal) {


    alertify.confirm('Confirmar', '¿Desea eliminar esta sucursal?', function () {
        $.post("EliminarSucursal", { 'IdSucursal': varIdSucursal }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Sucursal Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerSucursales");
                limpiarDatos();
            }

        });

    }, function () { });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion").val("");
    $("#txtDireccion").val("");
    $("#chkActivo").prop('checked', false);
}




