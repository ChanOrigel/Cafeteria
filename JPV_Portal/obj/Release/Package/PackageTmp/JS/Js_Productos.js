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
        $('#txt_showing').val("p");//starts in productos
        Cargar_Cmb();
        cargar_tabla_Prod();
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION QUE INICIALIZA LOS MANEJADORES DE EVENTOS.
/// </summary>
function eventos() {
    $('#btn_Categ').on('click', function (e) {
        e.preventDefault();
        limpiar_controles();
        habilitar_controles('IrCateg');
    });
    $('#btn_Prod').on('click', function (e) {
        e.preventDefault();
        limpiar_controles();
        habilitar_controles('IrProd');
    });
    $('#btn_nuevo').on('click', function (e) {
        e.preventDefault();
        limpiar_controles();
        habilitar_controles('Nuevo');
    });
    $('#btn_cancelar').on('click', function (e) {
        e.preventDefault();
        limpiar_controles();

        if ($('#txt_showing').val() == "c")
            habilitar_controles('IrCateg');
        else
            habilitar_controles('IrProd');

    });
    $('#btn_guardar').on('click', function (e) {
        e.preventDefault();

        var output = validar_datos();
        if (output.Estatus) {
            if ($('#txt_showing').val() == "c") {
                if ($('#txt_categ_id').val() != null && $('#txt_categ_id').val() != undefined && $('#txt_categ_id').val() != '') {
                    Ope_Modificar();
                } else {
                    Ope_Alta();
                }
            } else {
                if ($('#txt_id').val() != null && $('#txt_id').val() != undefined && $('#txt_id').val() != '') {
                    Ope_Modificar();
                } else {
                    Ope_Alta();
                }
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

/*====================================== TABLAS =====================================*/
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_tabla_Prod() {
    var registros = "{}";

    //var Obj_Param = Obtener_Parametros();

    $.ajax({
        url: 'Controllers/Ctrl_Productos.asmx/Consultar_Productos',
        //data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                registros = res.Registros;
                llenar_grid_prod(registros);
            }
            //else {
            //    llenar_grid_prod();
            //}
        }
    });
}
/// </summary>
///Llenar tabla
/// </summary>
function llenar_grid_prod(registros) {
    try {
        //PRODUCTOS TABLE///////////////////////////////////////////////////////

        $('#Tbl_RegistrosProd').bootstrapTable('destroy');
        $('#Tbl_RegistrosProd').bootstrapTable({
            data: JSON.parse(registros),
            method: 'POST',
            height: 400,
            striped: true,
            pagination: true,
            pageSize: 50,
            pageList: [10, 25, 50, 100, 200],
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [
                { field: 'Categoria', title: 'Categoria', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Producto', title: 'Producto', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Precio', title: 'Precio', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                {
                    field: 'Modificar', title: 'Modificar', align: 'center', formatter: function (index, value, row) {
                        return '<button type="button" class="btn-warning btn-lg" title="Modificar"><i class="glyphicon glyphicon-edit"></i></button>'
                        //return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="glyphicon glyphicon-remove"></i></a></div>';
                    }
                },
                {
                    field: 'Eliminar', title: 'Eliminar', align: 'center', formatter: function (index, value, row) {
                        //return '<button type="button" class="btn-primary btn-sm" title="Modificar"><i class="glyphicon glyphicon-edit"></i></button>'
                        return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"> <button type="button" class="btn-warning btn-lg" title="Eliminar"><i class="glyphicon glyphicon-remove"></i></button></a></div>';
                    }
                },
                { title: "Producto_ID", visible: false },
                { title: "Categoria_ID", visible: false },


            ],
            onClickCell: function (field, value, row, $element) {
                if (field == 'Modificar') {

                    $('#txt_id').val(row.Producto_ID);
                    $('#txt_descProd').val(row.Producto);
                    $('#txt_precio').val(row.Precio);

                    $('#txt_categ_id').val(row.Categoria_ID);
                    $('#txt_categ').val(row.Categoria);

                    //esto funciona para setear informacion en un combo 
                    $("#cmb_Categ").select2({
                        data: [{ id: row.Categoria_ID, text: row.Categoria }]
                    })
                    $('#cmb_Categ').val(row.Categoria_ID).trigger('change');
                    Cargar_Cmb();
                    /////////////////////////////////////////////////////////
                   

                    habilitar_controles("Nuevo");

                }

                if (field == 'Eliminar') {
                    bootbox.confirm({
                        title: "Eliminar",
                        message: "¿Esta seguro de Eliminar?",
                        buttons: {
                            cancel: {
                                label: '<i class="fa fa-times"></i> Cancel'
                            },
                            confirm: {
                                label: '<i class="fa fa-check"></i> Confirm'
                            }
                        },
                        callback: function (result) {
                            if (result) {
                                Eliminar("Producto", row.Producto_ID);//////////////////
                            }
                            cargar_tabla_Prod();
                        }
                    });

                }
            },

        });

    } catch (e) {
        asignar_modal("Tabla Productos", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }

}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_tabla_Categ() {
    var registros = "{}";

    //var Obj_Param = Obtener_Parametros();

    $.ajax({
        url: 'Controllers/Ctrl_Productos.asmx/Consultar_Categorias',
        //data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                registros = res.Registros;
                llenar_grid_categ(registros);
            }
        }
    });
}
/// </summary>

/// </summary>
function llenar_grid_categ(registros) {
    try {
        //CATEGORIAS TABLE////////////////////////////////////////////////////////

        $('#Tbl_RegistrosCateg').bootstrapTable('destroy');
        $('#Tbl_RegistrosCateg').bootstrapTable({
            data: JSON.parse(registros),
            method: 'POST',
            height: 400,
            striped: true,
            pagination: true,
            pageSize: 50,
            pageList: [10, 25, 50, 100, 200],
            search: true,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [
                { field: 'Categoria', title: 'Categoria', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                {
                    field: 'Modificar', title: 'Modificar', align: 'center', formatter: function (index, value, row) {
                        return '<button type="button" class="btn-warning btn-lg" title="Modificar"><i class="glyphicon glyphicon-edit"></i></button>'
                        //return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="glyphicon glyphicon-remove"></i></a></div>';
                    }
                },
                {
                    field: 'Eliminar', title: 'Eliminar', align: 'center', formatter: function (index, value, row) {
                        //return '<button type="button" class="btn-primary btn-sm" title="Modificar"><i class="glyphicon glyphicon-edit"></i></button>'
                        return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"> <button type="button" class="btn-warning btn-lg" title="Eliminar"><i class="glyphicon glyphicon-remove"></i></button></a></div>';
                    }
                },
                { title: "Categoria_ID", visible: false },


            ],
            onClickCell: function (field, value, row, $element) {
                if (field == 'Modificar') {
                    $('#txt_categ_id').val(row.Categoria_ID);
                    $('#txt_descCateg').val(row.Categoria);


                    habilitar_controles("Nuevo");

                }

                if (field == 'Eliminar') {
                    bootbox.confirm({
                        title: "Eliminar",
                        message: "¿Esta seguro de Eliminar?",
                        buttons: {
                            cancel: {
                                label: '<i class="fa fa-times"></i> Cancel'
                            },
                            confirm: {
                                label: '<i class="fa fa-check"></i> Confirm'
                            }
                        },
                        callback: function (result) {
                            if (result) {
                                Eliminar("Categoria", row.Categoria_ID);//////////////////
                            }
                            cargar_tabla_Categ();
                        }
                    });

                }
            },

        });
    } catch (e) {
        //asignar_modal("", e);
        //jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }

}
/*====================================== LLENAR COMBOS =====================================*/
/// <summary>
/// FUNCION PARA TRAER LA INFORMACIÓN AL COMBO
/// </summary>
function Cargar_Cmb() {
    try {
        $('#cmb_Categ').select2({
            theme: "classic",
            language: "es",
            width: 'resolve',  //esto ayuda junto con el style en html a redimensionar el combo
            placeholder: 'Seleccione la Categoria',
            allowClear: true,
            minimumInputLength: 0,
            tags: false,
            tokenSeparators: [','],
            ajax: {
                url: 'Controllers/Ctrl_Productos.asmx/Cargar_Cmb',
                cache: "true",
                dataType: 'json',
                cache: "true",
                type: "POST",
                delay: 250,
                cache: true,
                params: {
                    contentType: 'application/json; charset=utf-8'
                },
                quietMillis: 100,
                results: function (data) {
                    return { results: data };
                },
                data: function (params) {
                    return {
                        q: params.term,
                        page: params.page
                    };
                },
                processResults: function (data, page) {
                    return {
                        results: data
                    };
                },
            },
            templateResult: formato_conceptos
        });

        $('#cmb_Categ').on("select2:select", function (evt) {
            $('#txt_categ').val(evt.params.data.text);
            $('#txt_categ_id').val(evt.params.data.id);
            //var modal = $('#Modal_Cajas');
            //modal.modal({ backdrop: 'static', keyboard: false });
        });

        $("#cmb_Categ").on("select2:unselecting instead", function (e) {
            $('#txt_categ').val('');
            $('#txt_categ_id').val('');
        });
    } catch (e) {
        mostrar_mensaje('Información técnica', e);
    }
}
/*====================================== IMAGEN =====================================*/
/// <summary>
/// FUNCION PARA CARGAR LA IMAGEN
/// </summary>
$("#img_file").change(function () {
    readURL(this);
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#img').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

/*====================================== REGISTRO, MODIFICAR Y ELIMINAR =====================================*/

/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Ope_Alta() {
    var Obj_usuario = new Object();

    try {
        //PRODUCTOS
        Obj_usuario.Producto = $('#txt_descProd').val();
        Obj_usuario.Categoria_ID = $('#txt_categ_id').val();
        Obj_usuario.Precio = $('#txt_precio').val();

        //CATEGORIAS
        Obj_usuario.Categoria = $('#txt_descCateg').val();
        //Obj_usuario.Imagen = $('#img_file').get(0).files;


        $.ajax({
            url: 'Controllers/Ctrl_Productos.asmx/Ope_Alta',
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
        //PRODUCTOS
        Obj_usuario.Producto_ID = $('#txt_id').val();
        Obj_usuario.Producto = $('#txt_descProd').val();
        Obj_usuario.Categoria_ID = $('#txt_categ_id').val();
        Obj_usuario.Precio = $('#txt_precio').val();

        //CATEGORIAS
        Obj_usuario.Categoria = $('#txt_descCateg').val();

        $.ajax({
            url: 'Controllers/Ctrl_Productos.asmx/Ope_Modificar',
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

/// <summary>
/// Función que ejecuta la eliminacion de los registros
/// </summary>
function Eliminar(Tipo, ID) {
    var Obj_object = new Object();
    try {
        if (Tipo == "Producto")
            Obj_object.Producto_ID = ID;
        else
            Obj_object.Categoria_ID = ID;


        $.ajax({
            url: 'Controllers/Ctrl_Productos.asmx/Eliminar',
            data: "{'Datos':'" + JSON.stringify(Obj_object) + "'}",
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
        cargar_tabla_Prod();
        habilitar_controles('IrProd');
        limpiar_controles();
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
        if ($('#txt_showing').val() == "c") {

            if ($('#txt_descCateg').val() == '' || $('#txt_descCateg').val() == undefined || $('#txt_descCateg').val() == null) {
                output.Estatus = false;
                output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>DESCRIPCION</strong>.</span><br />';
            }
        }
        else {
            if ($('#cmb_Categ :selected').val() == '' || $('#cmb_Categ :selected').val() == undefined || $('#cmb_Categ :selected').val() == null) {
                output.Estatus = false;
                output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CATEGORIA</strong>.</span><br />';
            }
            if ($('#txt_descProd').val() == '' || $('#txt_descProd').val() == undefined || $('#txt_descProd').val() == null) {
                output.Estatus = false;
                output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>DESCRIPCION</strong>.</span><br />';
            }
            if ($('#txt_precio').val() == '' || $('#txt_precio').val() == undefined || $('#txt_precio').val() == null) {
                output.Estatus = false;
                output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>PRECIO</strong>.</span><br />';
            }
        }
        //if ($('#txt_password').val() == '' || $('#txt_password').val() == undefined || $('#txt_password').val() == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CONTRASEÑA</strong>.</span><br />';
        //}
        //if ($('#txt_email').val() == '' || $('#txt_email').val() == undefined || $('#txt_email').val() == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CORREO ELECTRONICO</strong>.</span><br />';
        //} else {
        //    oTable.column(1)
        //           .data()
        //           .each(function (value, index) {
        //               if (value !== $Correo) {
        //                   if ($('#txt_email').val() == value) {
        //                       output.Estatus = false;
        //                       output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CORREO ELECTRONICO (asignado)</strong>.</span><br />';
        //                   }
        //               }
        //           });
        //}


        //if ($('#cmb_estatus :selected').val() == '' || $('#cmb_estatus :selected').val() == undefined || $('#cmb_estatus :selected').val() == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>ESTATUS</strong>.</span><br />';
        //}

        //var selValue = $('input[name=Tipo]:checked').val();
        //if (selValue == '' || selValue == undefined || selValue == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>TIPO DE USUARIO</strong>.</span><br />';
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
        case "IrCateg":
            $('#li-nuevo').css({ display: 'inline-block' });
            $('#li-guardar').css({ display: 'none' });
            $('#li-cancelar').css({ display: 'none' });
            $('#Categorias').css({ display: 'Block' });

            $('#DatosCateg').css({ display: 'Block' });//tablas
            $('#DatosProd').css({ display: 'none' });//tablas
            $('#li-Categ').css({ display: 'none' });//botones
            $('#li-Prod').css({ display: 'inline-block' });//botones

            //PRODUCTOS
            $('#txt_id').val("");
            $('#txt_descProd').val("");
            $('#txt_categ_id').val("");
            $('#txt_precio').val("");

            $('#ShowProd').css({ display: 'none' });//inputs
            $('#ShowCateg').css({ display: 'none' });//inputs

            cargar_tabla_Categ();

            $('#txt_showing').val("c");

            break;
        case "IrProd":
            $('#li-nuevo').css({ display: 'inline-block' });//BOTONES
            $('#li-guardar').css({ display: 'none' });
            $('#li-cancelar').css({ display: 'none' });
            $('#Productos').css({ display: 'Block' });

            $('#DatosCateg').css({ display: 'none' });//tablas
            $('#DatosProd').css({ display: 'Block' });//tablas
            $('#li-Categ').css({ display: 'inline-block' });//botones
            $('#li-Prod').css({ display: 'none' });//botones


            //CATEGORIAS
            $('#txt_descCateg').val("");

            $('#ShowProd').css({ display: 'none' });//inputs
            $('#ShowCateg').css({ display: 'none' });//inputs

            cargar_tabla_Prod();

            $('#txt_showing').val("p");

            break;

        case "Nuevo":
            $('#li-nuevo').css({ display: 'none' });
            $('#li-guardar').css({ display: 'inline-block' });
            $('#li-cancelar').css({ display: 'inline-block' });

            var res = $('#txt_showing').val();
            if (res == "c") {
                $('#DatosCateg').css({ display: 'none' });

                $('#ShowProd').css({ display: 'none' });
                $('#ShowCateg').css({ display: 'inline-block' });
            } else {
                $('#DatosProd').css({ display: 'none' });

                $('#ShowProd').css({ display: 'inline-block' });
                $('#ShowCateg').css({ display: 'none' });
            }

            break;
    }
}
function formato_conceptos(row) {
    var $row = $('<span style="font-family:Century Gothic;font-size:12px;"><i class="fa fa-tag" style="color:#000;"></i>&nbsp;' + row.text + '</span>');

    var _salida = '<span>' +
        '<i class="fa fa-tag" style="color:#0060aa;"></i>&nbsp;' + row.text +
        '</span>';

    // _salida += '<span>' +
    //'<i class="glyphicon glyphicon-option-vertical" style="color:#0060aa;"></i>&nbsp;' + row.tag +
    //'</span>';

    return $(_salida);
};
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
