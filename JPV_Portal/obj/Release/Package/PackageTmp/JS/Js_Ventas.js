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
        estado_inicial();
        habilitar_controles('Inicio');
        eventos();
        cargar_tabla();
        Folio();
        Cargar_Cmb_Producto();
        Cargar_Cmb_Categoria();
        Calcular_Fecha();

        $('#cmb_producto').select2("val", "PRODUCTOS");

        $('#cmb_categoria').select2("val", "CATEGORIAS");


        $('#txt_fecha').datepicker({
            uiLibrary: 'bootstrap4'
        });

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION QUE INICIALIZA LOS MANEJADORES DE EVENTOS.
/// </summary>
function eventos() {
    //***************************BOTON CANTIDAD******************************************************************//
    $('#txt_cantidad').on('click', function (e) {
        $('#Cantidad').css({ display: 'Block' });
        $('#Precio').css({ display: 'none' });
    });

    $('#btn_ce').on('click', function (e) {
        $("#txt_importe").val('');
        $("#txt_cantidad").val('');

    });
    $('#btn_1').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 1;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_2').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 2;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_3').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 3;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_4').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 4;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_5').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 5;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_6').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 6;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_7').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 7;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_8').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 8;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_9').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 9;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_0').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + 0;
        $("#txt_cantidad").val(canti);
        Importe();
    });
    $('#btn_punto').on('click', function (e) {
        var cantidad = $("#txt_cantidad").val();
        var canti = cantidad + '.';
        $("#txt_cantidad").val(canti);
        Importe();
    });

    //****************************BOTONES PAGAR*********************************************//
    $('#txt_precio').on('click', function (e) {
        $('#Cantidad').css({ display: 'none' });
        $('#Precio').css({ display: 'Block' });
    });

    $('#btn_ce_p').on('click', function (e) {
        $("#txt_importe").val('');
        $("#txt_precio").val('');

    });
    $('#btn_1_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 1;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_2_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 2;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_3_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 3;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_4_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 4;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_5_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 5;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_6_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 6;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_7_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 7;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_8_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 8;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_9_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 9;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_0_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + 0;
        $("#txt_precio").val(canti);
        Importe();
    });
    $('#btn_punto_p').on('click', function (e) {
        var cantidad = $("#txt_precio").val();
        var canti = cantidad + '.';
        $("#txt_precio").val(canti);
        Importe();
    });

    $('#por').on('click', function (e) {
        $('#Cantidad').css({ display: 'none' });
        $('#Precio').css({ display: 'Block' });
    });
    $('#por_').on('click', function (e) {
        $('#Cantidad').css({ display: 'none' });
        $('#Precio').css({ display: 'Block' });
    });

    $('#btn_agregar').on('click', function (e) {
        e.preventDefault();
        var output = validar_datos();
        if (output.Estatus) {
            Importe();
            Cargar_Informacion();
            Total();
            limpiar_agregar();
            $('#Cantidad').css({ display: 'Block' });
            $('#Precio').css({ display: 'none' });
        }
        else {
            $('#btn_agregar').popModal({
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



    $('#Btn_Limpiar_Todo').on('click', function (e) {
        $("#cmb_categoria").select2("val", "CATEGORIA");
        $("#cmb_producto").select2("val", "PRODUCTOS");
        cargar_tabla();
        limpiar_controles();
        $("#txt_cantidad").val("1");
        Calcular_Fecha();
        Folio();

    });

    $('#btn_imprimir').on('click', function (e) {
        e.preventDefault();
        Calcular_Fecha();
        Folio();
        Imprimir();
    });

    $('#txt_cantidad').on('keyup', function (e) {
        Importe();
    });
    $('#txt_precio').on('keyup', function (e) {
        Importe();
    });

    $('.btn-number').click(function (e) {
        e.preventDefault();

        fieldName = $(this).attr('data-field');
        type = $(this).attr('data-type');
        var input = $("input[name='" + fieldName + "']");
        var currentVal = parseInt(input.val());
        if (!isNaN(currentVal)) {
            if (type == 'minus') {

                if (currentVal > input.attr('min')) {
                    input.val(currentVal - 1).change();
                }
                if (parseInt(input.val()) == input.attr('min')) {
                    //$(this).attr('disabled', true);
                }

            } else if (type == 'plus') {

                if (currentVal < input.attr('max')) {
                    input.val(currentVal + 1).change();
                }
                if (parseInt(input.val()) == input.attr('max')) {
                    //$(this).attr('disabled', true);
                }

            }
        } else {
            input.val(0);
        }
    });
}

/*====================================== OPERACIONES =====================================*/
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_tabla() {
    try {
        var registros = "{}";

        $('#Tbl_Registros').bootstrapTable('destroy');
        $('#Tbl_Registros').bootstrapTable({
            data: JSON.parse(registros),
            method: 'POST',
            height: 250,
            striped: true,
            pagination: true,
            pageSize: 50,
            pageList: [3, 5, 10],
            search: false,
            showColumns: false,
            showRefresh: false,
            minimumCountColumns: 2,
            clickToSelect: true,
            columns: [
                { field: 'Cantidad', title: 'Cantidad', align: 'right', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Descripcion', title: 'Descripcion', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
                { field: 'Precio', title: 'Precio', align: 'right', valign: 'center', sortable: true, clickToSelect: false },

                {
                    field: 'Quitar', title: 'Quitar', align: 'center', formatter: function (index, value, row) {
                        return '<div><a class="remove ml10" id="' + index + '" href="javascript:void(0)"><i class="	fa fa-minus-square"></i></a></div>';
                    }
                },
                { title: "ID", visible: false },
                { title: "Producto_ID", visible: false },

                { field: 'Importe', title: 'Importe', visible: false },
            ],
            onClickCell: function (field, value, row, $element) {

                if (field == 'Quitar') {
                    Quitar(row.ID);
                    Total();
                }
            }
        });

    } catch (e) {
        mostrar_mensaje('Informe Técnico', e);
    }

}
/// <summary>
/// FUNCION PARA ELEGIR EL FOLIO
/// </summary>
function Folio() {
    var registros = "{}";
    try {
        $.ajax({
            url: 'Controllers/Ctrl_Ventas.asmx/Folio',
            method: 'POST',
            cache: false,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                if (data.d != undefined && data.d != null) {
                    var res = eval("(" + data.d + ")");
                    registros = res.Registros;
                    var row = JSON.parse(registros);
                    if (registros == null || registros == "{}")
                        var Folio = 1;
                    else
                        var Folio = parseInt(row[0].Folio) + 1;

                    $("#txt_folio").val(Folio);
                }
            }
        });
    } catch (e) {
        mostrar_mensaje('Informe Técnico', e);
    }
}
/// <summary>
/// FUNCION PARA CARGAR LA INFORMACION DEL PRODUCTO
/// </summary>
function Cargar_Informacion() {
    try {
        var benef = $('#Tbl_Registros').bootstrapTable('getData');
        var no_ben = benef.length + 1;

        $('#Tbl_Registros').bootstrapTable('insertRow', {
            index: no_ben,
            row: {
                ID: no_ben,
                Descripcion: $('#txt_producto').val(), //
                Cantidad: $('#txt_cantidad').val(),
                Precio: $('#txt_precio').val(),
                Importe: $('#txt_importe').val().replace(/[, $]+/g, ''),
                Producto_ID: $('#txt_producto_id').val(),

                Operacion: no_ben
            }
        });
    } catch (e) {
        mostrar_mensaje('Informe Técnico', e);
    }

}
/// <summary>
/// Función que ejecuta el alta de los registros
/// </summary>
function Imprimir(Respuesta) {
    var Obj_usuario = new Object();

    var Obj_Param_Lista = new Object();
    var Obj_Param_Cajas = new Object();

    try {

        var now = new Date();
        var diario = now.getDay();
        if (diario == 1)
            diario = "Lunes";
        if (diario == 2)
            diario = "Martes";
        if (diario == 3)
            diario = "Miercoles";
        if (diario == 4)
            diario = "Jueves";
        if (diario == 5)
            diario = "Viernes";
        if (diario == 6)
            diario = "Sabado";
        if (diario == 0)
            diario = "Domingo";

        var total = $('#txt_total').val().replace(/[, $]+/g, '');
        var iva = total * 0.16;
        var subtotal = total * 1 - iva * 1;
        Obj_usuario.Dia = diario;
        Obj_usuario.Folio = $('#txt_folio').val();
        Obj_usuario.Total = $('#txt_total').val().replace(/[, $]+/g, '');
        Obj_usuario.Subtotal = subtotal;
        Obj_usuario.IVA = iva;
        Obj_usuario.Fecha = $('#txt_fecha').val();

        registros = $('#Tbl_Registros').bootstrapTable('getData');
        param = new Array();

        if (registros.length > 0) {
            for (var i = 0; i < registros.length; i++) {
                Obj_Param_Lista = new Object();
                Obj_Param_Lista.Cantidad = registros[i].Cantidad;
                Obj_Param_Lista.Precio = registros[i].Precio;
                Obj_Param_Lista.Importe = registros[i].Importe;
                Obj_Param_Lista.Descripcion = registros[i].Descripcion;
                Obj_Param_Lista.Producto_ID = registros[i].Producto_ID;
                param[i] = Obj_Param_Lista;
            }

            $.ajax({
                url: 'Controllers/Ctrl_Ventas.asmx/Imprimir',
                data: "{'Datos':'" + JSON.stringify(Obj_usuario) + "', 'Items_Lista':'" + JSON.stringify(param) + "'}",
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
        }
        else {
            asignar_modal("Advertencia", 'No se ha cargado ningun material a la lista');
            jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });

        }
        $('#cmb_categoria').select2("val", "CATEGORIA");
        Calcular_Fecha();
        Folio();

    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION PARA TRAER LA INFORMACIÓN AL COMBO
/// </summary>
function Cargar_Cmb_Categoria() {
    try {
        $('#cmb_categoria').select2({
            theme: "classic",
            language: "es",
            placeholder: 'CATEGORIAS',
            allowClear: true,
            minimumInputLength: 0,
            tags: false,
            tokenSeparators: [','],
            ajax: {
                url: 'Controllers/Ctrl_Ventas.asmx/Cargar_Cmb_Categoria',
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

        $('#cmb_categoria').on("select2:select", function (evt) {
            $('#txt_categoria').val(evt.params.data.text);
            $('#txt_categoria_id').val(evt.params.data.id);

        });

        $("#cmb_categoria").on("select2:unselecting instead", function (e) {
            $('#txt_categoria').val('');
            $('#txt_categoria_id').val('');
        });
    } catch (e) {
        asignar_modal("", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <summary>
/// FUNCION PARA TRAER LA INFORMACIÓN AL COMBO
/// </summary>
function Cargar_Cmb_Producto() {
    try {

        var cat_id = $('#cmb_categoria').val();

        $('#cmb_producto').select2({
            theme: "classic",
            language: "es",
            placeholder: 'PRODUCTO',
            allowClear: true,
            minimumInputLength: 0,
            tags: false,
            tokenSeparators: [','],
            ajax: {
                url: 'Controllers/Ctrl_Ventas.asmx/Cargar_Cmb_Producto',
                //data: "{'Cat_ID':'" + cat_id + "'}",
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
                        page: cat_id
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

        $('#cmb_producto').on("select2:select", function (evt) {
            $('#txt_producto').val(evt.params.data.text);
            $('#txt_producto_id').val(evt.params.data.id);
            $('#txt_precio').val(evt.params.data.tag2);

            Importe();

        });

        $("#cmb_producto").on("select2:unselecting instead", function (e) {
            $('#txt_producto').val('');
            $('#txt_producto_id').val('');
            $('#txt_precio').val('');

        });
    } catch (e) {
        mostrar_mensaje('Información técnica', e);
    }
}
/// </summary>
/// <usuario_creo></usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
function Importe() {
    var costo = 0;
    var cantidad = $("#txt_cantidad").val();

    if ($("#txt_precio").val() == null || $("#txt_precio").val() == "") {
        costo = 0;
    } else {
        costo = $("#txt_precio").val();
    }
    var importe = cantidad * costo;
    importe = importe.toFixed(2)
    $("#txt_importe").val(importe);
    $("#txt_importe").formatCurrency();
}
/// </summary>
/// <usuario_creo></usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
function Total() {
    var importe = 0;

    //registros = $('#Tbl_Registros').DataTable().rows().data();
    registros = $('#Tbl_Registros').bootstrapTable('getData');

    if (registros.length > 0)
        for (var i = 0; i < registros.length; i++) {
            importe = importe * 1 + registros[i].Importe * 1;
        }

    importe = importe.toFixed(2).replace(/[, $]+/g, '');
    $("#txt_total").val(importe).formatCurrency();
}
/*====================================== GENERALES =====================================*/
/// <summary>
/// FUNCION PARA ELIMINAR LA INFORMACION DEL REGISTRO
/// </summary>
function Quitar(ID) {

    try {

        $('#Tbl_Registros').bootstrapTable('remove', { field: 'ID', values: [ID] });

    } catch (e) {
        mostrar_mensaje('Informe Tecnico', e);
    }

}
/// <summary>
/// FUNCION PARA ESTABLECER LA PAGINA CON LA CONFIGURACION INICIAL
/// </summary>
function estado_inicial() {
    try {
        habilitar_controles('Inicio');
        cargar_tabla();
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
        //if ($('#txt_cliente').val() == '' || $('#txt_cliente').val() == undefined || $('#txt_cliente').val() == null) {
        //    output.Estatus = false;
        //    output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CLIENTE</strong>.</span><br />';
        //}
        if ($('#txt_producto').val() == '' || $('#txt_producto').val() == undefined || $('#txt_producto').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>PRODUCTO</strong>.</span><br />';
        }
        if ($('#txt_cantidad').val() == '' || $('#txt_cantidad').val() == undefined || $('#txt_cantidad').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>CANTIDAD</strong>.</span><br />';
        }
        if ($('#txt_precio').val() == '' || $('#txt_precio').val() == undefined || $('#txt_precio').val() == null) {
            output.Estatus = false;
            output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>PRECIO</strong>.</span><br />';
        }

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    } finally {
        return output;
    }
}
/// <usuario_creo></usuario_creo>
/// <fecha_creo>05-Mayo-2016</fecha_creo>
function Calcular_Fecha() {
    //Calculamos la Fecha
    function addzero(i) {
        if (i < 10)
            i = "0" + i;
        return i;
    }

    var now = new Date();

    var mes = addzero((now.getMonth() + 1).toString());
    var dia = addzero(now.getDate().toString());
    var horas = now.getHours().toString();
    var minutos = now.getMinutes().toString();
    var fecha = dia + "/" + mes + "/" + now.getFullYear().toString() + ' ' + horas + ':' + minutos;

    $('#txt_fecha').val(fecha);

}
/// <summary>
/// FUNCION QUE HABILITA LOS CONTROLES DE LA PAGINA DE ACUERDO A LA OPERACION A REALIZAR.
/// </summary>
function habilitar_controles(opcion) {
    switch (opcion) {
        case "Inicio":
            $('#cont_alta').css({ display: 'Block' });
            $('#Reg_Datos').css({ display: 'Block' });

            $('#Cantidad').css({ display: 'Block' });
            $('#Precio').css({ display: 'none' });

            break;
    }
}
/// <summary>
/// FUNCION PARA LIMPIAR LOS CONTROLES
/// </summary>
function limpiar_controles() {
    $('input[type=text]').each(function () { $(this).val(''); });
    $('input[type=password]').each(function () { $(this).val(''); });
    $('input[type=hidden]').each(function () { $(this).val(''); });
    $('select').each(function () { $(this).val(''); });
    Folio();
    Calcular_Fecha();
}
/// <summary>
/// formato_conceptos
/// </summary>
function limpiar_agregar() {
    $('#txt_cantidad').val('');
    $('#txt_precio').val('');
    $('#cmb_producto').select2("val", "PRODUCTO");
    $('#txt_producto_id').val('');
    $('#txt_producto').val('');
    $('#txt_importe').val('');
}
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function formato_conceptos(row) {
    var $row = $('<span style="font-family:Century Gothic; font-size: 165%;"><i class="fa fa-tag" style="color:#fff;"></i>&nbsp;' + row.text + '</span>');

    var _salida = '<span>' +
        '<i class="fa fa-tag" style="color:#red;"></i>&nbsp;' + row.text +
        '</span>';

    return $(_salida);
};
/// <summary>
/// CREAR MODAL MENSAJE
/// </summary>
function asignar_modal(titulo, mensaje) {
    $('#title').text('');
    $('#Ml_boby').text('');
    $('#title').append(titulo + '<span class="glyphicon glyphicon-option-vertical"></span> ');
    $('#Ml_boby').append(mensaje);
}
