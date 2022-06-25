let table = '';


window.onload = function () {
    var url = "ObtenerProyectos";
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

        let proyectos = JSON.parse(data);
        let total_proyectos = proyectos.length;

        for (var i = 0; i < proyectos.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + proyectos[i].Codigo.toUpperCase() + '</td>' +
                '<td>' + proyectos[i].Descripcion.toUpperCase() + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + proyectos[i].IdProyecto + ')"></button>' +
                '<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + proyectos[i].IdProyecto + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Proyectos").html(tr);
        $("#spnTotalRegistros").html(total_proyectos);

        table = $("#table_id").DataTable(lenguaje);

    });

}




function ModalNuevo() {
    $("#lblTituloModal").html("Nuevo Proyecto");
    AbrirModal("modal-form");
}




function GuardarProyecto() {

    let varIdProyecto = $("#txtId").val();
    let varCodigo = $("#txtCodigo").val();
    let varDescripcion = $("#txtDescripcion").val();
    let varEstado = false;

    if ($('#chkActivo')[0].checked) {
        varEstado = true;
    }

    $.post('UpdateInsertProyecto', {
        'IdProyecto': varIdProyecto,
        'Codigo': varCodigo,
        'Descripcion': varDescripcion,
        'Estado': varEstado
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerProyectos");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });
}

function ObtenerDatosxID(varIdProyecto) {
    $("#lblTituloModal").html("Editar Proyecto");
    AbrirModal("modal-form");

    //console.log(varIdUsuario);

    $.post('ObtenerDatosxID', {
        'IdProyecto': varIdProyecto,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let proyecto = JSON.parse(data);
            //console.log(usuarios);
            $("#txtId").val(proyecto[0].IdProyecto);
            $("#txtCodigo").val(proyecto[0].Codigo);
            $("#txtDescripcion").val(proyecto[0].Descripcion);
            if (proyecto[0].Estado) {
                $("#chkActivo").prop('checked', true);
            }

        }

    });

}

function eliminar(varIdProyecto) {


    alertify.confirm('Confirmar', '¿Desea eliminar este proyecto?', function () {
        $.post("EliminarProyecto", { 'IdProyecto': varIdProyecto }, function (data) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
                limpiarDatos();
            } else {
                swal("Exito!", "Proyecto Eliminado", "success")
                table.destroy();
                ConsultaServidor("ObtenerProyectos");
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




