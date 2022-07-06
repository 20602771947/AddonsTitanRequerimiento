let table = '';


window.onload = function () {
    var url = "ObtenerArticulos";
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

        let articulo = JSON.parse(data);
        let total_articulo = articulo.length;

        for (var i = 0; i < articulo.length; i++) {

            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + articulo[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + articulo[i].Descripcion1.toUpperCase() + '</td>' +
                '<td>' + articulo[i].Descripcion2.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + articulo[i].IdArticulo + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + articulo[i].IdArticulo + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Articulos").html(tr);
        $("#spnTotalRegistros").html(total_articulo);

        table = $("#table_id").DataTable(lenguaje);

    });

}



function CargarUnidadMedida() {
    $.ajaxSetup({ async: false }); 
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        let unidadmedida = JSON.parse(data);
        llenarComboUnidadMedida(unidadmedida, "cboUnidadMedida", "Seleccione")
    });

}

function CargarCodigoUbso() {
    $.ajaxSetup({ async: false }); 
    $.post("/CodigoUbso/ObtenerCodigoUbso", function (data, status) {
        let codigoubso = JSON.parse(data);
        llenarComboCodigoUbso(codigoubso, "cboCodigoUbso", "Seleccione")
    });

}


function llenarComboCodigoUbso(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCodigoUbso + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function llenarComboUnidadMedida(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdUnidadMedida + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Articulo");
    AbrirModal("modal-form");
    $('#chkActivo').prop('checked', true);
    CargarUnidadMedida();
    CargarCodigoUbso();
}


function GuardarArticulo() {

    let varIdArticulo = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion1 = $("#txtDescripcion1").val();
    let varDescripcion2 = $("#txtDescripcion2").val();
    let varIdUnidadMedida = $("#cboUnidadMedida").val();
    let IdCodigoUbso = $("#cboCodigoUbso").val();
    let varEstadoActivoFijo = false;
    let varEstadoActivoCatalogo = false;
    let varEstado = false;

    if ($('#chkActivoFijo')[0].checked) {
        varEstadoActivoFijo = true;
    }
    if ($('#chkActivoCatalogo')[0].checked) {
        varEstadoActivoCatalogo = true;
    }
    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertArticulo', {
        'IdArticulo': varIdArticulo,
        'Codigo': varCodigo,
        'Descripcion1': varDescripcion1,
        'Descripcion2': varDescripcion2,
        'IdUnidadMedida': varIdUnidadMedida,
        'IdCodigoUbso': IdCodigoUbso,
        'ActivoFijo': varEstadoActivoFijo,
        'ActivoCatalogo': varEstadoActivoCatalogo,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerArticulos");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}


function ObtenerDatosxID(varIdArticulo) {
    $("#lblTituloModal").html("Editar Articulo");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdArticulo': varIdArticulo,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let articulos = JSON.parse(data);
            console.log(articulos);
            $("#txtId").val(articulos[0].IdArticulo);
            $("#txtCodigo").val(articulos[0].Codigo);
            $("#txtDescripcion1").val(articulos[0].Descripcion1);
            $("#txtDescripcion2").val(articulos[0].Descripcion2);

            CargarUnidadMedida();
            CargarCodigoUbso();


            $("#cboUnidadMedida").val(articulos[0].IdUnidadMedida);
            $("#cboCodigoUbso").val(articulos[0].IdCodigoUbso);

            if (articulos[0].ActivoFijo) {
                $("#chkActivoFijo").prop('checked', true);
            }

            if (articulos[0].ActivoCatalogo) {
                $("#chkActivoCatalogo").prop('checked', true);
            }

            if (articulos[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }


        }

    });

}


function limpiarDatos() {
    $("#txtId").val("");
    $("#txtCodigo").val("");
    $("#txtDescripcion1").val("");
    $("#txtDescripcion2").val("");
    $("#chkActivoFijo").prop('checked', false);
    $("#chkActivoCatalogo").prop('checked', false);
}


function eliminar(varIdArticulo) {

    alertify.confirm('Confirmar', '¿Desea eliminar este articulo?', function () {
        $.post("EliminarArticulo", { 'IdArticulo': varIdArticulo }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Articulo Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerArticulos");
                limpiarDatos();
            }

        });

    }, function () { });

}
