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
                '<td>' + solicitudes[i].DetalleEstado + '</td>' +
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
    AbrirModal("modal-form");
    //setearValor_ComboRenderizado("cboCodigoArticulo");
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



let contador = 0;
function AgregarLinea() {


    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); 
    var yyyy = today.getFullYear();

    today = yyyy + '-' + mm + '-' + dd;
  

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

    contador++;
    let tr = '';
    
    //<select class="form-control select2" id="cboCodigoArticulo" name="cboCodigoArticulo[]">
    //    <option value="0">Seleccione</option>
    //</select>
    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="0" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td>
            <input class="form-control" type="text" id="txtCodigoArticulo" name="txtCodigoArticulo[]" />
            </td>
            <td><input class="form-control" type="text" id="txtDescripcionArticulo" name="txtDescripcionArticulo[]"/></td>
            <td>
            <select class="form-control" id="cboUnidadMedida" name="cboUnidadMedida[]">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < UnidadMedida.length; i++) {
                tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida+`">` + UnidadMedida[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="date" value="`+ today +`" id="txtFechaNecesaria`+contador+`" name="txtFechaNecesaria[]"></td>
            <td>
            <select class="form-control MonedaDeCabecera" style="width:100px" name="cboMoneda[]" id="cboMonedaDetalle`+contador+`" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Moneda.length; i++) {
                tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control TipoCambioDeCabecera" type="number" name="txtTipoCambio[]" id="txtTipoCambioDetalle`+contador+`" disabled></td>
            <td><input class="form-control" type="number" name="txtCantidadNecesaria[]" value="0" id="txtCantidadNecesaria`+contador+`" onkeyup="CalcularTotalDetalle(`+contador+`)"></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="0" id="txtPrecioInfo`+ contador + `" onkeyup="CalcularTotalDetalle(` + contador +`)"></td>
            <td>
            <select class="form-control" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+contador+`" onchange="CalcularTotalDetalle(`+contador+`)">`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
            for (var i = 0; i < IndicadorImpuesto.length; i++) {
                tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje+`" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" name="txtItemTotal[]" id="txtItemTotal`+contador+`" onchange="CalcularTotales()"></td>
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


    let varMoneda = $("#cboMoneda").val();
    let varTipoCambio = $("#txtTipoCambio").val();

    if (varMoneda) {
        $(".MonedaDeCabecera").val(varMoneda);
    }
    if (varTipoCambio) {
        $(".TipoCambioDeCabecera").val(varTipoCambio);
    }
    
    

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
    $(".MonedaDeCabecera").val(varMoneda).change();
    $("#txtTipoCambio").val('0');
    let varTipoCambio = $("#txtTipoCambio").val();
    $(".TipoCambioDeCabecera").val(varTipoCambio).change();
}

//function ValidarMonedaBaseDetalle(contador) {

//    let varMoneda = $("#cboMonedaDetalle"+contador).val();
//    $.post("/Moneda/ValidarMonedaBase", { 'IdMoneda': varMoneda }, function (data, status) {

//        if (data == "error") {
//            swal("Error!", "Ocurrio un Error")
//            return;
//        } else {

//            let datos = JSON.parse(data);
//            if (datos[0].Base) {
//                $("#txtTipoCambioDetalle"+contador).prop('disabled', true);
//            } else {
//                $("#txtTipoCambioDetalle" + contador).prop('disabled', false);
//            }

//        }


//    });

//}

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
    let arrayIdSolicitudDetalle = new Array();

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
    $("input[name='txtIdSolicitudRQDetalle[]']").each(function (indice, elemento) {
        arrayIdSolicitudDetalle.push($(elemento).val());
    });
    //ArrayGeneral.push({ 'IdArticulo': arrayIdArticulo });

    //console.log(ArrayGeneral[0]);
    //return;


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
        'IdArticulo': arrayIdArticulo,
        'IdUnidadMedida': arrayIdUnidadMedida,
        'FechaNecesaria': arrayFechaNecesaria,
        'IdItemMoneda': arrayIdMoneda,
        'ItemTipoCambio': arrayTipoCambio,
        'CantidadNecesaria': arrayCantidadNecesaria,
        'PrecioInfo': arrayPrecioInfo,
        'IdIndicadorImpuesto': arrayIndicadorImpuesto,
        'ItemTotal': arrayTotal,
        'IdAlmacen': arrayAlmacen,
        'IdProveedor': arrayProveedor,
        'NumeroFabricacion': arrayNumeroFabricacion,
        'NumeroSerie': arrayNumeroSerie,
        'IdLineaNegocio': arrayLineaNegocio,
        'IdCentroCostos': arrayCentroCosto,
        'IdProyecto': arrayProyecto,
        'IdSolicitudRQDetalle': arrayIdSolicitudDetalle
        
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



            //agrega detalle
            let tr = '';

            let Detalle = solicitudes[0].Detalle;
            console.log(Detalle);
            for (var i = 0; i < Detalle.length; i++) {

                AgregarLineaDetalle(i, Detalle[i].IdSolicitudRQDetalle, Detalle[i].IdUnidadMedida, Detalle[i].IdIndicadorImpuesto, Detalle[i].IdAlmacen, Detalle[i].IdProveedor, Detalle[i].IdLineaNegocio, Detalle[i].IdCentroCostos, Detalle[i].IdProyecto, Detalle[i].IdItemMoneda, Detalle[i].ItemTipoCambio, Detalle[i].CantidadNecesaria, Detalle[i].PrecioInfo, Detalle[i].ItemTotal, Detalle[i].NumeroFabricacion, Detalle[i].NumeroSerie, Detalle[i].FechaNecesaria);

            }





        }

    });

}


function CalcularTotalDetalle(contador) {

    let varIndicadorImppuesto = $("#cboIndicadorImpuestoDetalle" + contador).val();
    let varPorcentaje = $('option:selected', "#cboIndicadorImpuestoDetalle"+contador).attr("impuesto");

    let varCantidadNecesaria = $("#txtCantidadNecesaria" + contador).val();
    let varPrecioInfo = $("#txtPrecioInfo" + contador).val();

    let subtotal = varCantidadNecesaria * varPrecioInfo;
     
    let varTotal = 0;
    let impuesto = 0;

    if (Number(varPorcentaje) == 0) {
        varTotal = subtotal;
    } else {
        impuesto = (subtotal * varPorcentaje);
        varTotal = subtotal + impuesto;
    }
    $("#txtItemTotal" + contador).val(varTotal.toFixed(2)).change();


}

function CalcularTotales() {

    let arrayCantidadNecesaria = new Array();
    let arrayPrecioInfo = new Array();
    let arrayIndicadorImpuesto = new Array();
    let arrayTotal = new Array();

    $("input[name='txtCantidadNecesaria[]']").each(function (indice, elemento) {
        arrayCantidadNecesaria.push($(elemento).val());
    });
    $("input[name='txtPrecioInfo[]']").each(function (indice, elemento) {
        arrayPrecioInfo.push($(elemento).val());
    });
    $("select[name='cboIndicadorImpuesto[]']").each(function (indice, elemento) {
        arrayIndicadorImpuesto.push($('option:selected',elemento).attr("impuesto"));
    });
    $("input[name='txtItemTotal[]']").each(function (indice, elemento) {
        arrayTotal.push(Number($(elemento).val()));
    });

    //console.log(arrayTotal);
    //console.log(arrayIndicadorImpuesto);

    let subtotal = 0;
    let impuesto = 0;
    let total = 0;

    for (var i = 0; i < arrayPrecioInfo.length; i++) {
        subtotal += (arrayCantidadNecesaria[i] * arrayPrecioInfo[i]);
        total += arrayTotal[i];
    }

    impuesto = total - subtotal;

    $("#txtTotalAntesDescuento").val(subtotal.toFixed(2));
    $("#txtImpuesto").val(impuesto.toFixed(2));
    $("#txtTotal").val(total.toFixed(2));

}


function AgregarLineaDetalle(contador,IdSolicitudRQDetalle,IdUnidadMedida, IdIndicadorImpuesto, IdAlmacen, IdProveedor, IdLineaNegocio, IdCentroCosto, IdProyecto, IdMoneda, ItemTipoCambio, CantidadNecesaria, PrecioInfo, ItemTotal, NumeroFabricacion, NumeroSerie, FechaNecesaria) {

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

    var fechaSplit = (FechaNecesaria.substring(0, 10)).split("-");
    var fecha = fechaSplit[0] + "-" + fechaSplit[1] + "-" + fechaSplit[2];

    let tr = '';

    tr += `<tr>
            <td><input style="display:none;" class="form-control" type="text" value="`+ IdSolicitudRQDetalle+`" id="txtIdSolicitudRQDetalle" name="txtIdSolicitudRQDetalle[]"/></td>
            <td><input class="form-control" type="text" id="txtCodigoArticulo" name="txtCodigoArticulo[]"/></td>
            <td><input class="form-control" type="text" id="txtDescripcionArticulo" name="txtDescripcionArticulo[]"/></td>
            <td>
            <select class="form-control" name="cboUnidadMedida[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < UnidadMedida.length; i++) {
        if (UnidadMedida[i].IdUnidadMedida == IdUnidadMedida) {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `" selected>` + UnidadMedida[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida + `">` + UnidadMedida[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td><input class="form-control" type="date" value="`+ fecha + `" id="txtFechaNecesaria` + contador + `" name="txtFechaNecesaria[]"></td>
            <td>
            <select class="form-control MonedaDeCabecera" style="width:100px" name="cboMoneda[]" id="cboMonedaDetalle`+ contador + `" disabled>`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Moneda.length; i++) {
        if (Moneda[i].IdMoneda == IdMoneda) {
            tr += `  <option value="` + Moneda[i].IdMoneda + `" selected>` + Moneda[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td><input class="form-control TipoCambioDeCabecera" type="number" name="txtTipoCambio[]" value="`+ ItemTipoCambio+`" id="txtTipoCambioDetalle`+ contador + `" disabled></td>
            <td><input class="form-control" type="number" name="txtCantidadNecesaria[]" value="`+ CantidadNecesaria+`" id="txtCantidadNecesaria`+ contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)"></td>
            <td><input class="form-control" type="number" name="txtPrecioInfo[]" value="`+ PrecioInfo+`" id="txtPrecioInfo`+ contador + `" onkeyup="CalcularTotalDetalle(` + contador + `)"></td>
            <td>
            <select class="form-control" name="cboIndicadorImpuesto[]" id="cboIndicadorImpuestoDetalle`+ contador + `" onchange="CalcularTotalDetalle(` + contador + `)">`;
    tr += `  <option impuesto="0" value="0">Seleccione</option>`;
    for (var i = 0; i < IndicadorImpuesto.length; i++) {
        if (IndicadorImpuesto[i].IdIndicadorImpuesto == IdIndicadorImpuesto) {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `" selected>` + IndicadorImpuesto[i].Descripcion + `</option>`;
        } else {
            tr += `  <option impuesto="` + IndicadorImpuesto[i].Porcentaje + `" value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td><input class="form-control changeTotal" type="number" style="width:100px" value="`+ ItemTotal+`" name="txtItemTotal[]" id="txtItemTotal`+ contador + `" onchange="CalcularTotales()"></td>
            <td>
            <select class="form-control" style="width:100px" name="cboAlmacen[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Almacen.length; i++) {
        if (Almacen[i].IdAlmacen == IdAlmacen) {
            tr += `  <option value="` + Almacen[i].IdAlmacen + `" selected>` + Almacen[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" style="width:100px" name="cboProveedor[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Proveedor.length; i++) {
        if (Proveedor[i].IdProveedor == IdProveedor) {
            tr += `  <option value="` + Proveedor[i].IdProveedor + `" selected>` + Proveedor[i].RazonSocial + `</option>`;
        } else {
            tr += `  <option value="` + Proveedor[i].IdProveedor + `">` + Proveedor[i].RazonSocial + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td><input class="form-control" type="text" value="`+ NumeroFabricacion+`" name="txtNumeroFacbricacion[]"></td>
            <td><input class="form-control" type="text" value="`+ NumeroSerie+`" name="txtNumeroSerie[]"></td>
            <td>
            <select class="form-control" name="cboLineaNegocio[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < LineaNegocio.length; i++) {
        if (LineaNegocio[i].IdLineaNegocio == IdLineaNegocio) {
            tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `" selected>` + LineaNegocio[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" name="cboCentroCostos[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < CentroCosto.length; i++) {
        if (CentroCosto[i].IdCentroCosto == IdCentroCosto) {
            tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `" selected>` + CentroCosto[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" style="width:100px" name="cboProyecto[]">`;
    tr += `  <option value="0">Seleccione</option>`;
    for (var i = 0; i < Proyecto.length; i++) {
        if (Proyecto[i].IdProyecto == IdProyecto) {
            tr += `  <option value="` + Proyecto[i].IdProyecto + `" selected>` + Proyecto[i].Descripcion + `</option>`;
        } else {
            tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
        }
    }
    tr += `</select>
            </td>
            <td><input class="form-control" type="text" value="0" disabled></td>
            <td><input class="form-control" type="text" value="0" disabled></td>
            <td><button class="btn btn-xs btn-danger"  onclick="EliminarDetalle(`+ IdSolicitudRQDetalle+`,this)">-</button></td>
          <tr>`;

    $("#tabla").find('tbody').append(tr);

}

function EliminarDetalle(IdSolicitudRQDetalle,dato) {

    alertify.confirm('Confirmar', '¿Desea eliminar este item?', function () {

        $.post("EliminarDetalleSolicitud", { 'IdSolicitudRQDetalle': IdSolicitudRQDetalle }, function (data, status) {

            if (data == 0) {
                swal("Error!", "Ocurrio un Error")
               
            } else {
                swal("Exito!", "Item Eliminado", "success")
                $(dato).closest('tr').remove();
            }

        });

    }, function () {});

   

}

function EnviarTipoCambioDetalle(){
    let TpCambio = $("#txtTipoCambio").val();
    $(".TipoCambioDeCabecera").val(TpCambio);
}

//function setearValor_ComboRenderizado(combo) {


//    $("#"+combo).select2(
//        {
//            ajax: {
//                url: "/Articulo/ObtenerArticulos",
//                dataType: 'json',
//                processResults: function (data) {
//                    let ArrayProductos = new Array();
//                    for (var i = 0; i < data.length; i++) {
//                        ArrayProductos.push({ "id": data[i].IdArticulo, "text": data[i].Descripcion1 });
//                    }
//                    return {
//                        results: [
//                            {
//                                id: 0,
//                                text: 'enhancement'
//                            },
//                            {
//                                id: 1,
//                                text: 'bug'
//                            },
//                            {
//                                id: 2,
//                                text: 'duplicate'
//                            },
//                            {
//                                id: 3,
//                                text: 'invalid'
//                            },
//                            {
//                                id: 4,
//                                text: 'wontfix'
//                            }
//                        ]
//                    };
//                }
//            }
//        }
//    );


//}