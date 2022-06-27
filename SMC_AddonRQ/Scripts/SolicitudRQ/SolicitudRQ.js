

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
            <td><input class="form-control" type="text" id="txtCodigoArticulo" /></td>
            <td><input class="form-control" type="text" id="txtDescripcionArticulo" /></td>
            <td>
            <select class="form-control">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < UnidadMedida.length; i++) {
                tr += `  <option value="` + UnidadMedida[i].IdUnidadMedida+`">` + UnidadMedida[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="date" value=""></td>
            <td>
            <select class="form-control" style="width:100px">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Moneda.length; i++) {
                tr += `  <option value="` + Moneda[i].IdMoneda + `">` + Moneda[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="number"></td>
            <td><input class="form-control" type="number"></td>
            <td><input class="form-control" type="number"></td>
            <td>
            <select class="form-control">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < IndicadorImpuesto.length; i++) {
                tr += `  <option value="` + IndicadorImpuesto[i].IdIndicadorImpuesto + `">` + IndicadorImpuesto[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="number" style="width:100px"></td>
            <td>
            <select class="form-control" style="width:100px">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Almacen.length; i++) {
                tr += `  <option value="` + Almacen[i].IdAlmacen + `">` + Almacen[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" style="width:100px">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Proveedor.length; i++) {
                tr += `  <option value="` + Proveedor[i].IdProveedor + `">` + Proveedor[i].RazonSocial + `</option>`;
            }
    tr += `</select>
            </td>
            <td><input class="form-control" type="text"></td>
            <td><input class="form-control" type="text"></td>
            <td>
            <select class="form-control">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < LineaNegocio.length; i++) {
                tr += `  <option value="` + LineaNegocio[i].IdLineaNegocio + `">` + LineaNegocio[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td>
            <select class="form-control">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < CentroCosto.length; i++) {
                tr += `  <option value="` + CentroCosto[i].IdCentroCosto + `">` + CentroCosto[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td>
            <select class="form-control" style="width:100px">`;
    tr += `  <option value="0">Seleccione</option>`;
            for (var i = 0; i < Proyecto.length; i++) {
                tr += `  <option value="` + Proyecto[i].IdProyecto + `">` + Proyecto[i].Descripcion + `</option>`;
            }
    tr += `</select>
            </td>
            <td>0</td>
            <td>0</td>
            <td><button class="btn btn-xs btn-danger borrar">-</button></td>
          <tr>`;

    $("#tabla").find('tbody').append(tr);

}


function CargarSeries() {
    $.post("/Serie/ObtenerSeries", function (data, status) {
        let series = JSON.parse(data);
        llenarComboSerie(series, "cboSerie", "Seleccione")
    });
}

function CargarSolicitante() {
    $.post("/Empleado/ObtenerEmpleados", function (data, status) {
        let empleados = JSON.parse(data);
        llenarComboEmpleado(empleados, "cboEmpleado", "Seleccione")
        llenarComboEmpleado(empleados, "cboTitular", "Seleccione")
    });
}

function CargarSucursales() {
    $.post("/Sucursal/ObtenerSucursales", function (data, status) {
        let sucursales = JSON.parse(data);
        llenarComboSucursal(sucursales, "cboSucursal", "Seleccione")
    });
}

function CargarDepartamentos() {
    $.post("/Departamento/ObtenerDepartamentos", function (data, status) {
        let departamentos = JSON.parse(data);
        llenarComboDepartamento(departamentos, "cboDepartamento", "Seleccione")
    });
}

function CargarMoneda() {
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

    $("#cboMoneda").val();
    //$.post("/Serie/ObtenerSeries", function (data, status) {
    //    let series = JSON.parse(data);
    //    llenarComboSerie(series, "cboSerie", "Seleccione")
    //});

}