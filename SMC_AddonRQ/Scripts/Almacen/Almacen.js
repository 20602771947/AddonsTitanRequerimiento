let table = '';


window.onload = function () {
    var url = "ObtenerAlmacenes";
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

        let almacenes = JSON.parse(data);
        let total_almacenes= almacenes.length;

        for (var i = 0; i < almacenes.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + almacenes[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + almacenes[i].Descripcion.toUpperCase() + '</td>' +
                '<td>' + almacenes[i].Sucursal.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + almacenes[i].IdAlmacen + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + almacenes[i].IdAlmacen + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Almacenes").html(tr);
        $("#spnTotalRegistros").html(total_almacenes);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Almacen");
    AbrirModal("modal-form");
    CargarSucursales();
}



function CargarSucursales() {

    $.post("/Sucursal/ObtenerSucursales", function (data, status) {

        let sucursales = JSON.parse(data);
        llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")

    });

}

function llenarComboSucursal(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSucursal + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function GuardarAlmacen() {

    let varIdAlmacen = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varSucursal = $("#cboSucursal").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertAlmacen', {
        'IdAlmacen': varIdAlmacen,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado,
        'IdSucursal': varSucursal
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerAlmacenes");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdAlmacen) {
    $("#lblTituloModal").html("Editar Almacen");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdAlmacen': varIdAlmacen,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let almacenes = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(almacenes[0].IdAlmacen);
            $("#txtCodigo").val(almacenes[0].Codigo);
            $("#txtDescripcion").val(almacenes[0].Descripcion);
            if (almacenes[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }



            $.post("/Sucursal/ObtenerSucursales", function (data, status) {
                let contenido;
                let sucursales = JSON.parse(data);
                for (var i = 0; i < sucursales.length; i++) {
                    if (sucursales[i].IdSucursal == almacenes[0].IdSucursal) {
                        contenido += "<option selected value='" + sucursales[i].IdSucursal + "'>" + sucursales[i].Descripcion + "</option>";
                    } else {
                        contenido += "<option value='" + sucursales[i].IdSucursal + "'>" + sucursales[i].Descripcion + "</option>";
                    }
                }
                let cbo = document.getElementById("cboSucursal");
                if (cbo != null) cbo.innerHTML = contenido;
            });




        }

    });

}

function eliminar(varIdAlmacen) {


    alertify.confirm('Confirmar', '¿Desea eliminar este almacen?', function () {
        $.post("EliminarAlmacen", { 'IdAlmacen': varIdAlmacen }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Almacen Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerAlmacenes");
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




