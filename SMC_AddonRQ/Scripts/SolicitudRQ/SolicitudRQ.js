let table = '';


window.onload = function () {
    var url = "ObtenerSolicitudesRQ";
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

        let solicitudes = JSON.parse(data);
        let total_solicitudes = solicitudes.length;

        //console.log(solicitudes);

        for (var i = 0; i < solicitudes.length; i++) {


            tr += '<tr>' +
                '<td>' + (i + 1) + '</td>' +
                '<td>' + solicitudes[i].Solicitante + '</td>' +
                '<td>' + solicitudes[i].Serie + '</td>' +
                '<td>' + solicitudes[i].Numero + '</td>' +
                '<td>' + solicitudes[i].TotalAntesDescuento + '</td>' +
                '<td>' + solicitudes[i].Impuesto + '</td>' +
                '<td>' + solicitudes[i].Total + '</td>' +
                '<td><button class="btn btn-primary fa fa-pencil btn-xs" onclick="ObtenerDatosxID(' + solicitudes[i].IdSolicitudRQ + ')"></button>' +
                //'<button class="btn btn-danger btn-xs  fa fa-trash" onclick="eliminar(' + solicitudes[i].IdSolicitudRQ + ')"></button></td >' +
                '</tr>';
        }

        $("#tbody_Solicitudes").html(tr);
        $("#spnTotalRegistros").html(total_solicitudes);

        table = $("#table_id").DataTable(lenguaje);

    });

}



function ModalNuevo() {
    $("#lblTituloModal").html("Nueva Solicitud");
    AgregarLinea();
    CargarSeries();
    CargarSolicitante();
    CargarSucursales();
    CargarDepartamentos();
    CargarMoneda();
    //configurarAutocompletar("txtCodigoArticulo");
    AbrirModal("modal-form");
   
}

function openContenido(evt, Name) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(Name).style.display = "block";
    evt.currentTarget.className += " active";
}



function AgregarLinea() {

    let UnidadMedida;
    let IndicadorImpuesto;
    let Almacen;
    let Proveedor;
    let LineaNegocio;
    let CentroCosto;
    let Proyecto;
    let Moneda;

    $.ajaxSetup({ async: false }); 
    $.post("/UnidadMedida/ObtenerUnidadMedidas", function (data, status) {
        UnidadMedida = JSON.parse(data);
    });

    $.post("/IndicadorImpuesto/ObtenerIndicadorImpuestos", function (data, status) {
        IndicadorImpuesto = JSON.parse(data);
    });

    $.post("/Almacen/ObtenerAlmacenes", function (data, status) {
        Almacen = JSON.parse(data);
    });

    $.post("/Proveedor/ObtenerProveedores", function (data, status) {
        Proveedor = JSON.parse(data);
    });

    $.post("/LineaNegocio/ObtenerLineaNegocios", function (data, status) {
        LineaNegocio = JSON.parse(data);
    });

    $.post("/CentroCosto/ObtenerCentroCostos", function (data, status) {
        CentroCosto = JSON.parse(data);
    });

    $.post("/Proyecto/ObtenerProyectos", function (data, status) {
        Proyecto = JSON.parse(data);
    });

    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        Moneda = JSON.parse(data);
    });

  
    let tr = '';

    tr += `<tr>
            <td></td>
            <td><input class="form-control" type="text" id="txtCodigoArticulo" name="txtCodigoArticulo[]"/></td>
            <td><input class="form-control" type="text" id="txtDescripcionArticulo" name="txtDescripcionArticulo[]"/></td>
            <td>
            <select class="form-control" name="cboUnidadMedida[]">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < UnidadMedida.length; i++) {
                tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida+`">` + UnidadMedida[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="date" value="" name="txtFechaNecesaria[]"></td>
            <td>
            <select class="form-control" style="width:100px" name="cboMoneda[]">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Moneda.length; i++) {
                tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="number" name="txtTipoCambio[]"></td>
            <td><input class="form-control" type="number" name="txtCantidadNecesaria[]"></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]"></td>
            <td>
            <select class="form-control" name="cboIndicadorImpuesto[]">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < IndicadorImpuesto.length; i++) {
                tr += `  <option value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="number" style="width:100px" name="txtItemTotal[]"></td>
            <td>
            <select class="form-control" style="width:100px" name="cboAlmacen[]">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Almacen.length; i++) {
                tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" style="width:100px" name="cboProveedor[]">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Proveedor.length; i++) {
                tr += `  <option value="` + Proveedor[i].IdProveedor + `">` + Proveedor[i].RazonSocial + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="text" name="txtNumeroFacbricacion[]"></td>
            <td><input class="form-control" type="text" name="txtNumeroSerie[]"></td>
            <td>
            <select class="form-control" name="cboLineaNegocio[]">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < LineaNegocio.length; i++) {
                tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" name="cboCentroCostos[]">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < CentroCosto.length; i++) {
                tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" style="width:100px" name="cboProyecto[]">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Proyecto.length; i++) {
                tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="text" value="0" disabled></td>
            <td><input class="form-control" type="text" value="0" disabled></td>
            <td><button class="btn btn-xs btn-danger borrar">-</button></td>
          <tr>`;

    $("#tabla").find('tbody').append(tr);

}


function CargarSeries() {
    $.ajaxSetup({ async: false }); 
    $.post("/Serie/ObtenerSeries", function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerie(series, "cboSerie", "Seleccione")
    });
}

function CargarSolicitante() {
    $.ajaxSetup({ async: false }); 
    $.post("/Empleado/ObtenerEmpleados", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleado(empleados, "cboEmpleado", "Seleccione")
        llenarComboEmpleado(empleados, "cboTitular", "Seleccione")
    });
}

function CargarSucursales() {
    $.ajaxSetup({ async: false }); 
    $.post("/Sucursal/ObtenerSucursales", function (data, status) {
        let sucursales = JSON.parse(data);
        llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")
    });
}

function CargarDepartamentos() {
    $.ajaxSetup({ async: false }); 
    $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
        let departamentos = JSON.parse(data);
        llenarComboDepartamento(departamentos, "cboDepartamento", "Seleccione")
    });
}

function CargarMoneda() {
    $.ajaxSetup({ async: false }); 
    $.post("/Moneda/ObtenerMonedas", function (data, status) {
        let monedas = JSON.parse(data);
        llenarComboMoneda(monedas, "cboMoneda", "Seleccione")
    });
}

function llenarComboSerie(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdSerie + "'>" + lista[i].Serie + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboEmpleado(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdEmpleado + "'>" + lista[i].RazonSocial + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
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

function llenarComboDepartamento(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdDepartamento + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

function llenarComboMoneda(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdMoneda + "'>" + lista[i].Descripcion + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}

$(document).on('click', '.borrar', function (event) {
    event.preventDefault();
    $(this).closest('tr').remove();
});


//function configurarAutocompletar(nombre) {
//    var availableTags = [
//        "ActionScript",
//        "AppleScript",
//        "Ruby",
//        "Scala",
//        "Scheme"
//    ];

//    $("#" + nombre).autocomplete({
//        source: availableTags
//    });
//}

function CerrarModal() {
    $("#tabla").find('tbody').empty();
    $.magnificPopup.close();
    limpiarDatos();
}

function ValidarMonedaBase() {

    let varMoneda = $("#cboMoneda").val();
    $.post("/Moneda/ValidarMonedaBase", { 'IdMoneda': varMoneda }, function (data, status) {

        if (data == "error") {
            swal("Error!", "Ocurrio un Error")
            return;
        } else {

            let datos = JSON.parse(data);
            if (datos[0].Base) {
                $("#txtTipoCambio").prop('disabled', true);
            } else {
                $("#txtTipoCambio").prop('disabled', false);
            }

        }


    });

}

function ObtenerNumeracion() {

    let varSerie = $("#cboSerie").val();

    

    $.post("/Serie/ValidarNumeracionSerieSolicitudRQ", { 'IdSerie': varSerie }, function (data, status) {
        if (data == 'sin datos') {
            $.post("/Serie/ObtenerDatosxID", { 'IdSerie': varSerie }, function (data, status) {
                let valores = JSON.parse(data);
                $("#txtNumeracion").val(valores[0].NumeroInicial);
            });
        } else {
            let values = JSON.parse(data);
            let Numero = Number(values[0].NumeroInicial);
            $("#txtNumeracion").val(Numero+1);

        }
    });

}


function GuardarSolicitud() {
    let ArrayGeneral = new Array();
    let arrayIdArticulo = new Array();
    let arrayIdUnidadMedida = new Array();
    let arrayFechaNecesaria = new Array();
    let arrayIdMoneda = new Array();
    let arrayTipoCambio = new Array();
    let arrayCantidadNecesaria = new Array();
    let arrayPrecioInfo = new Array();
    let arrayIndicadorImpuesto = new Array();
    let arrayTotal = new Array();
    let arrayAlmacen = new Array();
    let arrayProveedor = new Array();
    let arrayNumeroFabricacion = new Array();
    let arrayNumeroSerie = new Array();
    let arrayLineaNegocio = new Array();
    let arrayCentroCosto = new Array();
    let arrayProyecto = new Array();
    $("input[name='txtCodigoArticulo[]']").each(function (indice, elemento) {
        arrayIdArticulo.push($(elemento).val());
    });
    $("select[name='cboUnidadMedida[]']").each(function (indice, elemento) {
        arrayIdUnidadMedida.push($(elemento).val());
    });
    $("input[name='txtFechaNecesaria[]']").each(function (indice, elemento) {
        arrayFechaNecesaria.push($(elemento).val());
    });
    $("select[name='cboMoneda[]']").each(function (indice, elemento) {
        arrayIdMoneda.push($(elemento).val());
    });
    $("input[name='txtTipoCambio[]']").each(function (indice, elemento) {
        arrayTipoCambio.push($(elemento).val());
    });
    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        arrayCantidadNecesaria.push($(elemento).val());
    });
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push($(elemento).val());
    });
    $("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
        arrayIndicadorImpuesto.push($(elemento).val());
    });
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push($(elemento).val());
    });
    $("select[name='cboAlmacen[]']").each(function (indice, elemento) {
        arrayAlmacen.push($(elemento).val());
    });
    $("select[name='cboProveedor[]']").each(function (indice, elemento) {
        arrayProveedor.push($(elemento).val());
    });
    $("input[name='txtNumeroFacbricacion[]']").each(function (indice, elemento) {
        arrayNumeroFabricacion.push($(elemento).val());
    });
    $("input[name='txtNumeroSerie[]']").each(function (indice, elemento) {
        arrayNumeroSerie.push($(elemento).val());
    });
    $("select[name='cboLineaNegocio[]']").each(function (indice, elemento) {
        arrayLineaNegocio.push($(elemento).val());
    });
    $("select[name='cboCentroCostos[]']").each(function (indice, elemento) {
        arrayCentroCosto.push($(elemento).val());
    });
    $("select[name='cboProyecto[]']").each(function (indice, elemento) {
        arrayProyecto.push($(elemento).val());
    });

    //ArrayGeneral.push({
    //    'IdArticulo': arrayIdArticulo,
    //    'IdUnidadMedida': arrayIdUnidadMedida,
    //    'FechaNecesaria': arrayFechaNecesaria,
    //    'IdMoneda': arrayIdMoneda,
    //    'TipoCambio': arrayTipoCambio,
    //    'CantidadNecesaria': arrayCantidadNecesaria,
    //    'PrecioInfo': arrayPrecioInfo,
    //    'IdIndicadorImpuesto': arrayIndicadorImpuesto,
    //    'Total': arrayTotal,
    //    'IdAlmacen': arrayAlmacen,
    //    'IdProveedor': arrayProveedor,
    //    'NumeroFabricacion': arrayNumeroFabricacion,
    //    'NumeroSerie': arrayNumeroSerie,
    //    'IdLineaNegocio': arrayLineaNegocio,
    //    'IdCentroCosto': arrayCentroCosto,
    //    'IdProyecto': arrayProyecto
    //});


    if (!$("#cboSerie").val().length > 0) {
        swal("Info!", "Debe Seleccionar Serie")
        return;
    }
    if (!$("#cboEmpleado").val().length > 0) {
        swal("Info!", "Debe Seleccionar Solicitante")
        return;
    }
    if (!$("#cboSucursal").val().length > 0) {
        swal("Info!", "Debe Seleccionar Sucursal")
        return;
    }
    if (!$("#cboDepartamento").val().length > 0) {
        swal("Info!", "Debe Seleccionar Departamento")
        return;
    }
    if (!$("#cboMoneda").val().length > 0) {
        swal("Info!", "Debe Seleccionar Moneda")
        return;
    }
    if (!$("#cboTitular").val().length > 0) {
        swal("Info!", "Debe Seleccionar Titular")
        return;
    }

    let varIdSolicitud = $("#txtId").val();
    let varSerie = $("#cboSerie").val();
    let varSerieDescripcion = $("#cboSerie").find('option:selected').text();
    let varEstado = 1; //Abierto
    let varNumero = $("#txtNumeracion").val();
    let varMoneda = $("#cboMoneda").val();
    let varTipoCambio = $("#txtTipoCambio").val();
    let varClaseArticulo = $("#cboClaseArticulo").val();
    let varSolicitante = $("#cboEmpleado").val();
    let varFechaContabilizacion = $("#txtFechaContabilizacion").val();
    let varSucursal = $("#cboSucursal").val();
    let varFechaValidoHasta = $("#txtFechaValidoHasta").val();
    let varDepartamento = $("#cboDepartamento").val();
    let varFechaDocumento = $("#txtFechaDocumento").val();
    let varTitular = $("#cboTitular").val();
    let varTotalAntesDescuento = $("#txtTotalAntesDescuento").val();
    let varComentarios = $("#txtComentarios").val();
    let varImpuesto = $("#txtImpuesto").val();
    let varTotal = $("#txtTotal").val();


    $.post('UpdateInsertSolicitud', {
        'IdSolicitudRQ': varIdSolicitud,
        'IdSerie': varSerie,
        'Serie': varSerieDescripcion,
        'Numero': varNumero,
        'IdSolicitante': varSolicitante,
        'IdSucursal': varSucursal,
        'IdDepartamento': varDepartamento,
        'IdClaseArticulo': varClaseArticulo,
        'IdTitular': varTitular,
        'IdMoneda': varMoneda,
        'TipoCambio': varTipoCambio,
        'IndicadorImpuesto': 0,
        'TotalAntesDescuento': varTotalAntesDescuento,
        'Impuesto': varImpuesto,
        'Total': varTotal,
        'FechaContabilizacion': varFechaContabilizacion,
        'FechaValidoHasta': varFechaValidoHasta,
        'FechaDocumento': varFechaDocumento,
        'Comentarios': varComentarios,
        'Estado': varEstado,
        'ArrayItem': ArrayGeneral[0]
    }, function (data, status) {

        if (data == 1) {
            swal("Exito!", "Proceso Realizado Correctamente", "success")
            table.destroy();
            ConsultaServidor("ObtenerSolicitudesRQ");
            limpiarDatos();
        } else {
            swal("Error!", "Ocurrio un Error")
            limpiarDatos();
        }

    });

}

function limpiarDatos() {

    $("#txtId").val('');
    $("#cboSerie").val('');
    $("#txtNumeracion").val('');
    $("#cboMoneda").val('');
    $("#txtTipoCambio").val('');
    //$("#cboClaseArticulo").val('');
    $("#cboEmpleado").val('');
    //$("#txtFechaContabilizacion").val(strDate);
    $("#cboSucursal").val('');
    //$("#txtFechaValidoHasta").val(strDate);
    $("#cboDepartamento").val('');
    //$("#txtFechaDocumento").val(strDate);
    $("#cboTitular").val('');
    $("#txtTotalAntesDescuento").val('');
    $("#txtComentarios").val('');
    $("#txtImpuesto").val('');
    $("#txtTotal").val('');
}


function ObtenerDatosxID(IdSolicitudRQ) {
    $("#lblTituloModal").html("Editar Solicitud");
    AbrirModal("modal-form");

    $.post('ObtenerDatosxID', {
        'IdSolicitudRQ': IdSolicitudRQ,
    }, function (data, status) {

        if (data == "Error") {
            swal("Error!", "Ocurrio un error")
            limpiarDatos();
        } else {
            let solicitudes = JSON.parse(data);
            //console.log(solicitudes);

            CargarSeries();
            CargarSolicitante();
            CargarSucursales();
            CargarDepartamentos();
            CargarMoneda();

            $("#txtId").val(solicitudes[0].IdSolicitudRQ);
            $("#cboSerie").val(solicitudes[0].IdSerie);
            $("#txtNumeracion").val(solicitudes[0].Numero);
            $("#cboMoneda").val(solicitudes[0].IdMoneda);
            $("#txtTipoCambio").val(solicitudes[0].TipoCambio);
            $("#cboClaseArticulo").val(solicitudes[0].IdClaseArticulo);
            $("#cboEmpleado").val(solicitudes[0].IdSolicitante);

            var fechaSplit = (solicitudes[0].FechaContabilizacion.substring(0, 10)).split("-");
            var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];
            $("#txtFechaContabilizacion").val(fecha);

            $("#cboSucursal").val(solicitudes[0].IdSucursal);

            var fechaSplit1 = (solicitudes[0].FechaValidoHasta.substring(0, 10)).split("-");
            var fecha1 = fechaSplit1[0] + "-" + fechaSplit1[1] + "-" + fechaSplit1[2];
            $("#txtFechaValidoHasta").val(fecha1);

            $("#cboDepartamento").val(solicitudes[0].IdDepartamento);

            var fechaSplit2 = (solicitudes[0].FechaDocumento.substring(0, 10)).split("-");
            var fecha2 = fechaSplit2[0] + "-" + fechaSplit2[1] + "-" + fechaSplit2[2];
            $("#txtFechaDocumento").val(fecha2);

            $("#cboTitular").val(solicitudes[0].IdTitular);
            $("#txtTotalAntesDescuento").val(solicitudes[0].TotalAntesDescuento);
            $("#txtComentarios").val(solicitudes[0].Comentarios);
            $("#txtImpuesto").val(solicitudes[0].Impuesto);
            $("#txtTotal").val(solicitudes[0].Total);
        }

    });

}