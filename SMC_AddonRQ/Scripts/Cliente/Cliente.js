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
    CargarTipoPersona();
    CargarTipoDocumento();
    CargarCondicionPago();
    CargarPaises();
    CargarDepartamentos();
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



function CargarTipoPersona() {
    $.post("/TipoPersona/ObtenerTipoPersonas", function (data, status) {
        let tipopersona = JSON.parse(data);
        llenarComboTipoPersona(tipopersona, "cboTipoPersona", "Seleccione")
    });
}

function CargarTipoDocumento() {
    $.post("/TipoDocumento/ObtenerTipoDocumentos", function (data, status) {
        let tipodocumento = JSON.parse(data);
        llenarComboTipoDocumento(tipodocumento, "cboTipoDocumento", "Seleccione")
    });
}

function CargarCondicionPago() {
    $.post("/CondicionPago/ObtenerCondicionPagos", function (data, status) {
        let condicionpago = JSON.parse(data);
        llenarComboCondicionPago(condicionpago, "cboCondicionPago", "Seleccione")
    });
}

function CargarPaises() {
    $.post("/Pais/ObtenerPaises", function (data, status) {
        let pais = JSON.parse(data);
        llenarComboPais(pais, "cboPais", "Seleccione")
    });
}

function CargarDepartamentos() {
    $.post("/Ubigeo/ObtenerDepartamentos", function (data, status) {
        let departamento = JSON.parse(data);
        llenarComboDepartamento(departamento, "cboDepartamento", "Seleccione")
    });
}

function CargarProvincias() {
    let varDepartamento = $("#cboDepartamento").val();
    $.post("/Ubigeo/ObtenerProvincias", { 'Departamento': varDepartamento.slice(0, 2)}, function (data, status) {
        let provincia = JSON.parse(data);
        llenarComboProvincia(provincia, "cboProvincia", "Seleccione")
    });
}

function CargarDistritos() {
    let varProvincia = $("#cboProvincia").val();
    console.log(varProvincia.slice(0, 4));
    $.post("/Ubigeo/ObtenerDistritos", { 'Provincia': varProvincia.slice(0, 4) }, function (data, status) {
        let distrito = JSON.parse(data);
        llenarComboDistrito(distrito, "cboDistrito", "Seleccione")
    });
}

function Buscar() {
    let varTipoDocumento = $("#cboTipoDocumento").val();
    let varDocumento = $("#txtNroDocumento").val();
    $.post("ConsultarDocumento", { 'Tipo': varTipoDocumento, 'Documento': varDocumento  }, function (data, status) {

        if (data == "error") {
            swal("Error!", "Ocurrio un al buscar este documento")
        }

        let datos = JSON.parse(data);
        let CantidadDatos = Object.keys(datos.persona).length;

        if (CantidadDatos > 3) {

            $("#txtRazonSocial").val(datos.persona.razonSocial);
            $("#txtEstadoContribuyente").val(datos.persona.estado);
            $("#txtCondicionContribuyente").val(datos.persona.condicion);
            $("#txtDireccionFiscal").val(datos.persona.direccion);

            let ubigeo = datos.persona.ubigeo;
            let obtenerdosdigitos = ubigeo.slice(0, 2);
            let obtenercuatrodigitos = ubigeo.slice(0, 4);
            let varDepartamento = obtenerdosdigitos + '0000';
            let varProvincia = obtenercuatrodigitos + '00';
            //let varDistrito = ubigeo;

            console.log(varDepartamento);
            console.log(varProvincia);
            $("#cboDepartamento").val(varDepartamento);
            CargarProvincias();
            $("#cboProvincia").val(varProvincia);
            //CargarDistritos();
            //$("#cboDistrito").val(varDistrito);

        } else {
            $("#txtRazonSocial").val(datos.persona.razonSocial);
        }

        

    });
}

function soloEnteros(e, obj) {
    var charCode = (e.which) ? e.which : e.keyCode;
    if (charCode == 13) {
        var tidx = parseInt(obj.getAttribute('tabindex')) + 1;
        elems = document.getElementsByClassName('input-sm');
        for (var i = elems.length; i--;) {
            var tidx2 = elems[i].getAttribute('tabindex');
            if (tidx2 == tidx) { elems[i].focus(); break; }
        }
    } else if (charCode == 46 || charCode > 31 && (charCode < 48 || charCode > 57)) {
        e.preventDefault();
        return false;
    }
    return true;
}


function ValidarBotonBuscar() {

    $("#btnBuscarReniec").show();
    let varTipoDocumento = $("#cboTipoDocumento").val();
    if (varTipoDocumento == 0) {
        $("#btnBuscarReniec").hide();
    }

}


function llenarComboTipoPersona(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdTipoPersona + "'>" + lista[i].TipoPersona + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function llenarComboTipoDocumento(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].Codigo + "'>" + lista[i].TipoDocumento + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function llenarComboCondicionPago(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdCondicionPago + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}


function llenarComboPais(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdPais + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboDepartamento(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].CodUbigeo + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboProvincia(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].CodUbigeo + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboDistrito(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].CodUbigeo + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}