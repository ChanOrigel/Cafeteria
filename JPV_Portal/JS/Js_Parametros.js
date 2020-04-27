/*====================================== VARIABLES =====================================*/
var oTable;
var $Correo = '';
var dataSet = [{ Menu: "Usuarios", Etiqueta: "Usuarios" },
               { Menu: "Proveedores", Etiqueta: "Proveedores" },
               { Menu: "Reporte Facturas", Etiqueta: "Reporte" }];

$.extend(true, $.fn.dataTable.defaults, {
    "language": {
        "sEmptyTable": "No hay datos en la tabla",
        "lengthMenu": "Mostando _MENU_ Registro(s) por página",
        "zeroRecords": "Nada Encontrado",
        "info": "Mostrando página _PAGE_ de _PAGES_",
        "infoEmpty": "No hay registros disponibles",
        "infoFiltered": "(filtered from _MAX_ total records)",
        "sLoadingRecords": "Cargando ...",
        "sProcessing": "Por favor espere...",
        "oPaginate": {
            "sFirst": "Primero",
            "sPrevious": "Atras",
            "sNext": "Siguiente",
            "sLast": "Ultimo"
        }
    }
});

/*====================================== INICIO-CARGA =====================================*/

$(document).on('ready', function () {
    inicializar_pagina();
});

/// <summary>
/// FUNCION PARA ESTABLECER EL ESTADO INICIAL DE LA PAGINA 
/// </summary>
function inicializar_pagina() {
    try {
        eventos();
        cargar_tabla();
        estado_inicial();
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION QUE INICIALIZA LOS MANEJADORES DE EVENTOS.
/// </summary>
function eventos() {
    $('#btn_nuevo').on('click', function (e) {
        e.preventDefault();
        limpiar_controles();
        habilitar_controles('Nuevo');
    });
    $('#btn_modificar').on('click', function (e) {
        e.preventDefault();
        habilitar_controles('Modificar');
    });


    $('#btn_cancelar').on('click', function (e) {
        e.preventDefault();
        limpiar_controles();
        cargar_tabla();
        habilitar_controles('Inicio');
    });
    $('#btn_guardar').on('click', function (e) {
        e.preventDefault();

        var output = validar_datos();
        if (output.Estatus) {
            if ($('#txt_id').val() != null && $('#txt_id').val() != undefined && $('#txt_id').val() != '') {
                Ope_Modificar();
            } else {
                Ope_Alta();
            }
        } else {
            $('#btn_guardar').popModal({
                html: "<h6> Datos requeridos </h6> <hr /> " + output.Mensaje + "<div class='popModal_footer'><button type='button' class='btn btn-primary btn-block' data-popmodal-but='ok'>ok</button></div>",
                placement: 'bottomLeft',
                showCloseBut: true,
                onDocumentClickClose: true,
                onDocumentClickClosePrevent: '',
                overflowContent: false,
                inline: true,
                asMenu: false,
                size: '',
                onOkBut: function (event, el) { },
                onCancelBut: function (event, el) { },
                onLoad: function (el) { },
                onClose: function (el) { }
            });
        }
    });
}

/*====================================== OPERACIONES =====================================*/
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_tabla() {
    var registros = "{}";
    try {

        $.ajax({
            url: 'Controllers/Ctrl_Parametros.asmx/Consultar',
            method: 'POST',
            cache: false,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.d != undefined && data.d != null) {
                    var res = eval("(" + data.d + ")");
                    row = JSON.parse(res.data);
                    if (row != null) {
                        if (row.length > 0) {
                            $('#txt_puerto').val(row[0].Puerto);
                            $('#txt_contrasenia_correo').val(row[0].Contrasena);
                            $('#txt_servidor_correo').val(row[0].Servidor);
                            $('#txt_correo_saliente').val(row[0].Correo);
                            $('#txt_id').val(row[0].Parametro_ID);
                            $('#cmb_cifrar_correo').val(row[0].Cifrar_Conexion);
                            $('#txt_correo_destino').val(row[0].Correo_Destino);
                            $('#txt_impresora').val(row[0].Impresora);
                            //$('#txt_correo_empleado').val(row[0].Correo_Empleado);

                        }
                    }
                }
            }
        });
    } catch (e) {
        mostrar_mensaje('Informe Técnico', e);
    }
}

/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Ope_Alta() {
    var Obj_usuario = new Object();

    try {
        Obj_usuario.Puerto = $('#txt_puerto').val();
        Obj_usuario.Cifrar_Conexion = $('#cmb_cifrar_correo :selected').val();
        Obj_usuario.Contrasena = encodeURI($('#txt_contrasenia_correo').val());
        Obj_usuario.Servidor = $('#txt_servidor_correo').val();
        Obj_usuario.Correo = $('#txt_correo_saliente').val();
        Obj_usuario.Correo_Destino = $('#txt_correo_destino').val();
        Obj_usuario.Impresora = $('#txt_impresora').val();
        //Obj_usuario.Correo_Empleado = $('#txt_correo_empleado').val();


        $.ajax({
            url: 'Controllers/Ctrl_Parametros.asmx/Ope_Alta',
            data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "'}",
            type: 'POST',
            cache: false,
            async: true,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {

                    asignar_modal("Correcto", res.Mensaje);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    estado_inicial();
                    //lis.modal('success', 'Success Alert & Notification');
                }
                else {
                    asignar_modal("Advertencia", res.Mensaje);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Ope_Modificar() {
    var Obj_usuario = new Object();
    try {

        Obj_usuario.Parametro_ID = $('#txt_id').val();

        Obj_usuario.Puerto = $('#txt_puerto').val();
        Obj_usuario.Cifrar_Conexion = $('#cmb_cifrar_correo :selected').val();
        Obj_usuario.Contrasena = encodeURI($('#txt_contrasenia_correo').val());
        Obj_usuario.Servidor = $('#txt_servidor_correo').val();
        Obj_usuario.Correo = $('#txt_correo_saliente').val();
        Obj_usuario.Correo_Destino = $('#txt_correo_destino').val();
        Obj_usuario.Impresora = $('#txt_impresora').val();
        //Obj_usuario.Correo_Empleado = $('#txt_correo_empleado').val();

        $.ajax({
            url: 'Controllers/Ctrl_Parametros.asmx/Ope_Modificar',
            data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "'}",
            type: 'POST',
            cache: false,
            async: true,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {
                    asignar_modal("Correcto", res.Mensaje);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                    estado_inicial();
                }
                else {
                    asignar_modal("Advertencia", res.Mensaje);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}


/*====================================== GENERALES =====================================*/
/// <summary>
/// FUNCION PARA ESTABLECER LA PAGINA CON LA CONFIGURACION INICIAL
/// </summary>
function estado_inicial() {
    try {
        habilitar_controles('Inicio');
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION PARA VALIDAR LOS DATOS REQUERIDOS
/// </summary>
function validar_datos() {
    var output = new Object();
    output.Estatus = true;
    output.Mensaje = '';

    try {
        if ($('#txt_correo_saliente').val() == '' || $('#txt_correo_saliente').val() == undefined || $('#txt_correo_saliente').val() == null) {
            output.Estatus = false;
            output.Mensaje += '&nbsp;-&nbsp;El correo es un dato requerido.<br />';
        }

        if ($('#txt_servidor_correo').val() == '' || $('#txt_servidor_correo').val() == undefined || $('#txt_servidor_correo').val() == null) {
            output.Estatus = false;
            output.Mensaje += '&nbsp;-&nbsp;El servidor es un dato requerido.<br />';
        }

        if ($('#txt_contrasenia_correo').val() == '' || $('#txt_contrasenia_correo').val() == undefined || $('#txt_contrasenia_correo').val() == null) {
            output.Estatus = false;
            output.Mensaje += '&nbsp;-&nbsp;La contraseña es un dato requerido.<br />';
        }

        if ($('#txt_puerto').val() == '' || $('#txt_puerto').val() == undefined || $('#txt_puerto').val() == null) {
            output.Estatus = false;
            output.Mensaje += '&nbsp;-&nbsp;El puerto es un dato requerido.<br />';
        }
        if ($('#txt_correo_destino').val() == '' || $('#txt_correo_destino').val() == undefined || $('#txt_correo_destino').val() == null) {
            output.Estatus = false;
            output.Mensaje += '&nbsp;-&nbsp;El correo de destino es obligatorio.<br />';
        }

        if ($('#cmb_cifrar_correo :selected').val() == '' || $('#cmb_cifrar_correo :selected').val() == undefined || $('#cmb_cifrar_correo :selected').val() == null) {
            output.Estatus = false;
            output.Mensaje += '&nbsp;-&nbsp;El cifrar correo es un dato requerido.<br />';
        }
        if ($('#txt_impresora').val() == '' || $('#txt_impresora').val() == undefined || $('#txt_impresora').val() == null) {
            output.Estatus = false;
            output.Mensaje += '&nbsp;-&nbsp;La Impresora es un dato requerido.<br />';
        }
        //if ($('#txt_correo_empleado').val() == '' || $('#txt_correo_empleado').val() == undefined || $('#txt_correo_empleado').val() == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '&nbsp;-&nbsp;La Impresora es un dato requerido.<br />';
        //}


    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    } finally {
        return output;
    }
}

/// <summary>
/// FUNCION QUE HABILITA LOS CONTROLES DE LA PAGINA DE ACUERDO A LA OPERACION A REALIZAR.
/// </summary>
function habilitar_controles(opcion) {
    var Estatus = false;
    switch (opcion) {
        case "Inicio":
            Estatus = false;
            $('#li-nuevo').css({ display: 'inline-block' });
            $('#li-guardar').css({ display: 'none' });
            $('#li-cancelar').css({ display: 'none' });
            $('#cont-alta').css({ display: 'Block' });

            if ($('#txt_id').val() == '' || $('#txt_id').val() == undefined || $('#txt_id').val() == null)
            { 
                $('#li-nuevo').css({ display: 'inline-block' });
                $('#li-editar').css({ display: 'none' });
            }
            else {
                $('#li-nuevo').css({ display: 'none' });
                $('#li-editar').css({ display: 'inline' });
            }
            
            break;
        case "Nuevo":
            Estatus = true;
            $('#li-nuevo').css({ display: 'none' });
            $('#li-editar').css({ display: 'none' });
            $('#li-guardar').css({ display: 'inline-block' });
            $('#li-cancelar').css({ display: 'inline-block' });
            $('#cont-alta').css({ display: 'Block' });

            break;
        case "Modificar":
            Estatus = true;
            $('#li-nuevo').css({ display: 'none' });
            $('#li-editar').css({ display: 'none' });
            $('#li-guardar').css({ display: 'inline-block' });
            $('#li-cancelar').css({ display: 'inline-block' });
            $('#cont-alta').css({ display: 'Block' });

            break;
    }

    $('#txt_ruta_xml').attr({ disabled: !Estatus });
    $('#txt_puerto').attr({ disabled: !Estatus });
    $('#txt_contrasenia_correo').attr({ disabled: !Estatus });
    $('#txt_servidor_correo').attr({ disabled: !Estatus });
    $('#txt_correo_saliente').attr({ disabled: !Estatus });
    $('#cmb_cifrar_correo').attr({ disabled: !Estatus });
    $('#txt_correo_destino').attr({ disabled: !Estatus });
    $('#txt_impresora').attr({ disabled: !Estatus });
    //$('#txt_correo_empleado').attr({ disabled: !Estatus });

}
/// <summary>
/// FUNCION PARA LIMPIAR LOS CONTROLES
/// </summary>
function limpiar_controles() {
    $('input[type=text]').each(function () { $(this).val(''); });
    $('input[type=password]').each(function () { $(this).val(''); });
    $('input[type=hidden]').each(function () { $(this).val(''); });
    $('select').each(function () { $(this).val(''); });
}

/// <summary>
/// CREAR MODAL MENSAJE
/// </summary>
function asignar_modal(titulo, mensaje) {
    $('#title').text('');
    $('#Ml_boby').text('');
    $('#title').append('<span class="glyphicon glyphicon-option-vertical"></span> ' + titulo);
    $('#Ml_boby').append(mensaje);
}
