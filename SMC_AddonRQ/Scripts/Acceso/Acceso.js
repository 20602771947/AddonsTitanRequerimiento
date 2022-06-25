let table = '';

window.onload = function () {


    //$.post("ObtenerAccesosPerfil", { 'IdPerfil': 0}
    //    , function (data, status) {

    //    if (data == "error") {
    //        return;
    //    }
    //    let tr = '';
    //    let accesos = JSON.parse(data);

    //    $.post("ObtenerMenus", function (data, status) {

    //        let menus = JSON.parse(data);

    //        for (var i = 0; i < menus.length; i++) {
    //            tr += '<tr>' +
    //                '<td>' + menus[i].Menu.toUpperCase() + '</td>';

    //            for (var j = 0;j < accesos.length; j++) {
    //                if (menus[i].IdMenu == accesos[j].IdMenu) {
    //                    tr += '<td><input type="checkbox" id="permiso" name="permiso" checked/></td>';
    //                } else {
    //                    tr += '<td><input type="checkbox" id="permiso" name="permiso"/></td>';
    //                }
    //            }
                        
    //            tr +='</tr>';
    //        }

    //        $("#tbRol").html(tr);
    //    });

    //});


    CargarPerfiles();

};


function BuscarAccesos() {

    let varcboPerfil = $("#cboPerfil").val();
    //console.log(varcboPerfil);

    $.post("ObtenerMenus"
        , function (data, status) {

            if (data == "error") {return;}
           
            let menus = JSON.parse(data);
            let tr = '';
           
            $.post("ObtenerAccesosPerfil", { 'IdPerfil': varcboPerfil }, function (data, status) {

                if (data == "error") {
                    $("input:checkbox[name='permiso[]']:checked").each(function (indice, elemento) {
                        $(elemento).prop('checked', false);
                    }); return; }

                let accesos = JSON.parse(data);
                
                for (var i = 0; i < menus.length; i++) {
                    let checked = false;
                    let idvalor;
                    let idvalornuevos;
                    tr += '<tr>';

                    if (menus[i].Posicion == 2) {
                        tr += '<td style="padding-left:20px">' + menus[i].Menu.toUpperCase() + '</td>';
                    } else if (menus[i].Posicion == 3) {
                        tr += '<td style="padding-left:40px">' + menus[i].Menu.toUpperCase() + '</td>';
                    } else {
                        tr += '<td><strong>' + menus[i].Menu.toUpperCase() + '</strong></td>';
                    }


                    for (var j = 0; j < accesos.length; j++) {
                        if (menus[i].IdMenu == accesos[j].IdMenu) {
                            checked = true;
                            idvalor = menus[i].IdMenu;
                        } else {
                            idvalornuevos = menus[i].IdMenu;
                        }
                    }

                    //console.log(idvalornuevos);

                    if (checked) {
                        tr += '<td><input type="checkbox" id="permiso" name="permiso[]" value="'+idvalor+'" checked/></td>';
                    } else {
                        tr += '<td><input type="checkbox" id="permiso" name="permiso[]" value="'+idvalornuevos+'"/></td>';
                    }
                   

                    tr += '</tr>';

               

                }   

                $("#tbRol").html(tr);

            }); 
                
               
          

        });

}

function GuardarAcceso() {

    let arrayacceso = new Array();
    let varcboPerfil = $("#cboPerfil").val();

    $("input:checkbox[name='permiso[]']:checked").each(function (indice, elemento) {
        arrayacceso.push($(elemento).val());
    });


    $.post("EliminarAccesoxPerfil", { 'IdPerfil': varcboPerfil}, function (data, status) {
        if (data == 1) {
            $.post("GuardarAcceso", { 'IdPerfil': varcboPerfil, 'ArrayAccesos': arrayacceso }, function (data, status) {
                if (data == 1) {
                    swal("Exito!", "Proceso Realizado Correctamente", "success")
                } else {
                    swal("Error!", "Ocurrio un Error")
                }
            });
        } else {
            swal("Error!", "Ocurrio un Error")
        }
    });

   
   

}


function CargarPerfiles() {

    $.post("/Perfil/ObtenerPerfiles", function (data, status) {

        let perfiles = JSON.parse(data);
        llenarComboPerfil(perfiles, "cboPerfil", "Seleccione")

    });

}


function llenarComboPerfil(lista, idCombo, primerItem) {
    var contenido = "";
    if (primerItem != null) contenido = "<option value=''>" + primerItem + "</option>";
    var nRegistros = lista.length;
    var nCampos;
    var campos;
    for (var i = 0; i < nRegistros; i++) {

        if (lista.length > 0) { contenido += "<option value='" + lista[i].IdPerfil + "'>" + lista[i].Perfil + "</option>"; }
        else { }
    }
    var cbo = document.getElementById(idCombo);
    if (cbo != null) cbo.innerHTML = contenido;
}