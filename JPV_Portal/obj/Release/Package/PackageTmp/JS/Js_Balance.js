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

        var $table = $('#Tbl_Registros');
        $(function () {
            $table.on('click-row.bs.table', function (e, row, $element) {
                $('.success').removeClass('success');
                $($element).addClass('success');
            });
        });
        //$('#Txt_Fecha_Inicial').datepicker({
        //    uiLibrary: 'bootstrap4'
        //});
        //$('#Txt_Fecha_Final').datepicker({
        //    uiLibrary: 'bootstrap4'
        //});

        Iniciar_Formulario();
        Calcular_Fecha();
        cargar_tabla();

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}

/// <summary>
/// FUNCION QUE INICIALIZA LOS MANEJADORES DE EVENTOS.
/// </summary>
function eventos() {
    $('#btn_actualizar').on('click', function (e) {
        e.preventDefault();
        var modal = $('#Modal_Actualizar');
        modal.modal({ backdrop: 'static', keyboard: false });
    });
    $('#btn_cortes').on('click', function (e) {
        e.preventDefault();
        var modal = $('#Modal_Corte');
        modal.modal({ backdrop: 'static', keyboard: false });
    });
    $('#btn_reportes').on('click', function (e) {
        e.preventDefault();
        var modal = $('#Modal_Reporte');
        modal.modal({ backdrop: 'static', keyboard: false });
    });

    //setInterval('cargar_tabla()', 9000);



    $('#Txt_Fecha_Inicial').datepicker({
        format: {
            /*
             * Say our UI should display a week ahead,
             * but textbox should store the actual date.
             * This is useful if we need UI to select local dates,
             * but store in UTC
             */
            toDisplay: function (date, format, language) {
                var d = new Date(date);
                d.setDate(d.getDate());
                return d.toISOString();
            },
            toValue: function (date, format, language) {
                var d = new Date(date);
                d.setDate(d.getDate());
                return new Date(d);
            }
        }
    });
    $('#Txt_Fecha_Final').datepicker({
        format: {
            /*
             * Say our UI should display a week ahead,
             * but textbox should store the actual date.
             * This is useful if we need UI to select local dates,
             * but store in UTC
             */
            toDisplay: function (date, format, language) {
                var d = new Date(date);
                d.setDate(d.getDate());
                return d.toISOString();
            },
            toValue: function (date, format, language) {
                var d = new Date(date);
                d.setDate(d.getDate());
                return new Date(d);
            }
        }
    });


}

/*====================================== OPERACIONES =====================================*/
/// <usuario_creo>Leslie González Vázquez</usuario_creo>
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

    $('#Txt_Fecha').val(fecha);


    var fecha_inicial = mes + "/" + "01" + "/" + now.getFullYear().toString();

    $('#Txt_Fecha_Inicial').val(fecha_inicial);


    var dia_final = addzero((now.getDate() + 1).toString());
    var fecha_final = mes + "/" + dia_final + "/" + now.getFullYear().toString();

    $('#Txt_Fecha_Final').val(fecha_final);

}
/// <summary>
/// Función que obtener los parametros
/// </summary>
/// <usuario_creo>MARÍA CHANTAL ORIGEL SEGURA</usuario_creo>
/// <fecha_creo>24 OCTUBRE 2017</fecha_creo>
function Obtener_Parametros() {
    var Obj_Param = null;
    try {
        Obj_Param = new Object();

        Obj_Param.Fecha_Inicio = $('#Txt_Fecha_Inicial').val();
        Obj_Param.Fecha_Fin = $('#Txt_Fecha_Final').val();

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
    return Obj_Param;
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function cargar_tabla() {
    var registros = "{}";

    $.ajax({
        url: 'Controllers/Ctrl_Balance.asmx/Consultar_Ventas',
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                row = JSON.parse(res.Registros);
                if (row != null) {
                    if (row.length > 0) {
                        llenar_grid();
                        var venta_total = 0;
                        for (var i = 0; i < row.length; i++) {
                            venta_total += row[i].Total;
                            var benef = $('#Tbl_Registros').bootstrapTable('getData');
                            var no_ben = benef.length + 1;
                            $('#Tbl_Registros').bootstrapTable('insertRow', {
                                index: no_ben,
                                row: {
                                    Folio: row[i].Folio,
                                    Total: row[i].Total,
                                    Usuario_Creo: row[i].Usuario_Creo,
                                    Operacion: no_ben,
                                    ID: no_ben,
                                }
                            });
                        }
                        $("#Txt_Ventas").val(venta_total).formatCurrency();

                    } else {
                        llenar_grid();
                    }
                }
            }
        }
    });
}
/// </summary>
/// <usuario_creo>Chantal Origel</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
function llenar_grid() {

    var registros = "{}";

    $('#Tbl_Registros').bootstrapTable('destroy');
    $('#Tbl_Registros').bootstrapTable({
        data: JSON.parse(registros),
        method: 'POST',
        height: 350,
        striped: true,
        pagination: true,
        pageSize: 5,
        pageList: [5],
        search: false,
        showColumns: false,
        showRefresh: false,
        minimumCountColumns: 2,
        clickToSelect: true,
        columns: [
            { field: 'Folio', title: 'Folio', align: 'left', valign: 'left', sortable: true, clickToSelect: false },
            {
                field: 'Total', title: 'Total de la venta', align: 'left', valign: 'center', sortable: true, clickToSelect: false, formatter: function (value, row, index) { return accounting.formatMoney(value); }
            },
            { field: 'Usuario_Creo', title: 'Usuario en Turno', align: 'left', valign: 'center', sortable: true, clickToSelect: false },
            { field: 'ID', title: 'ID', visible: false },

        ],
        onClickCell: function (field, value, row, $element) {
            //if (field == 'Abonar') {
            //    var modal = $('#Modal');
            //    modal.modal({ backdrop: 'static', keyboard: false });
            //}
        }
    });
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
function Iniciar_Formulario() {
    var registros = "{}";
    $.ajax({
        url: 'Controllers/Ctrl_Balance.asmx/Iniciar_Formulario',
        method: 'POST',
        cache: false,
        async: true,
        responsive: true,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (data.d != undefined && data.d != null) {
                var res = eval("(" + data.d + ")");
                row = JSON.parse(res.Registros);
                if (row != null) {
                    if (row.length > 0) {
                        $("#Txt_Balance_Id").val(row[0].Balance_ID)
                        if (row[0].Inicio_Actualizado == null || row[0].Inicio_Actualizado == "")
                            $("#Txt_Caja_Inicio").val(row[0].Fin_Caja).formatCurrency();
                        else
                            $("#Txt_Caja_Inicio").val(row[0].Inicio_Actualizado).formatCurrency();

                    }
                }
            }
        }
    });
}
/// <summary>
/// FUNCION PARA CARGAR LOS REGISTROS
/// </summary>
//function Consultar_Ventas() {

//    $.ajax({
//        url: 'Controllers/Ctrl_Balance.asmx/Consultar_Ventas',
//        method: 'POST',
//        cache: false,
//        async: true,
//        responsive: true,
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json',
//        success: function (data) {
//            if (data.d != undefined && data.d != null) {
//                var res = eval("(" + data.d + ")");
//                row = JSON.parse(res.Registros);
//                if (row != null) {
//                    if (row.length > 0) {
//                        $("#Txt_Ventas").val(row[0].Total).formatCurrency();
//                    }
//                }
//            }
//        }
//    });
//}
/// <summary>
/// FUNCION PARA VALIDAR LOS DATOS REQUERIDOS
/// </summary>
function validar_datos(tipo) {
    var output = new Object();
    output.Estatus = true;
    output.Mensaje = '';

    try {
        if (tipo == "actualizar") {
            if ($('#Txt_Nuevo_Inicio').val() == '' || $('#Txt_Nuevo_Inicio').val() == undefined || $('#Txt_Nuevo_Inicio').val() == null) {
                output.Estatus = false;
                output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>Nuevo inicio de caja</strong>.</span><br />';

                $('#Txt_Nuevo_Inicio').popModal({
                    html: "<h6> Datos requeridos </h6> <hr /> " + output.Mensaje + "<div class='popModal_footer'><button type='button' class='btn btn-primary btn-block' data-popmodal-but='ok'>ok</button></div>",
                    placement: 'bottomLeft',
                    showCloseBut: true,
                    onDocumentClickClose: false,
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
            else {
                Actualizar();
            }
        }
        else if (tipo == "corte") {
            if ($('#Txt_Caja_Fin').val() == '' || $('#Txt_Caja_Fin').val() == undefined || $('#Txt_Caja_Fin').val() == null) {
                output.Estatus = false;
                output.Mensaje += '<span class="glyphicon glyphicon-triangle-right"><strong>Monto final de caja</strong>.</span><br />';

                $('#Txt_Caja_Fin').popModal({
                    html: "<h6> Datos requeridos </h6> <hr /> " + output.Mensaje + "<div class='popModal_footer'><button type='button' class='btn btn-primary btn-block' data-popmodal-but='ok'>ok</button></div>",
                    placement: 'bottomLeft',
                    showCloseBut: true,
                    onDocumentClickClose: false,
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
            else {
                Enviar_Corte();
            }
        }

        //limpiar_controles();
    }
    catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <usuario_creo>Chantal Origel</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Actualizar() {
    Obj_Param = new Object();

    try {
        Obj_Param.Inicio_Actualizado = $("#Txt_Nuevo_Inicio").val();
        Obj_Param.Inicio_Caja = $("#Txt_Caja_Inicio").val();
        Obj_Param.Balance_ID = $("#Txt_Balance_Id").val();

        $.ajax({
            url: 'Controllers/Ctrl_Balance.asmx/Actualizar',
            data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
            type: 'POST',
            async: false,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {
                    location.reload();
                }
                else {
                    asignar_modal("Informe Técnico", e);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
        //cargar_tabla();
        //Iniciar_Formulario();

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <usuario_creo>Chantal Origel</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Enviar_Corte() {
    Obj_Param = new Object();

    try {
        Obj_Param.Fin_Caja = $("#Txt_Caja_Fin").val().replace(/[, $]+/g, '');
        Obj_Param.Inicio_Caja = $("#Txt_Caja_Inicio").val().replace(/[, $]+/g, '');
        Obj_Param.Ventas = $("#Txt_Ventas").val().replace(/[, $]+/g, '');

        $.ajax({
            url: 'Controllers/Ctrl_Balance.asmx/Enviar_Corte',
            data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
            type: 'POST',
            async: false,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {
                    location.reload();
                }
                else {
                    asignar_modal("Informe Técnico", e);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
        //cargar_tabla_cajas();

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/// <usuario_creo>Chantal Origel</usuario_creo>
/// <fecha_creo>21 Junio 2016</fecha_creo>
/// <summary>
function Enviar_Reporte() {
    Obj_Param = new Object();

    try {
        Obj_Param.Fecha_Fin = $("#Txt_Fecha_Final").val();
        Obj_Param.Fecha_Inicio = $("#Txt_Fecha_Inicial").val();

        $.ajax({
            url: 'Controllers/Ctrl_Balance.asmx/Enviar_Reporte',
            data: "{'Parametros':'" + JSON.stringify(Obj_Param) + "'}",
            type: 'POST',
            async: false,
            cache: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (Resultado) {
                var res = eval("(" + Resultado.d + ")");
                if (res.Estatus) {
                    location.reload();
                }
                else {
                    asignar_modal("Informe Técnico", e);
                    jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                }
            }
        });
        //cargar_tabla_cajas();

    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
    }
}
/*====================================== GENERALES =====================================*/
/// <summary>
/// FUNCION PARA ESTABLECER LA PAGINA CON LA CONFIGURACION INICIAL
/// </summary>
function estado_inicial() {
    try {
        cargar_tabla();
        habilitar_controles('Inicio');
        limpiar_controles();
    } catch (e) {
        asignar_modal("Informe Técnico", e);
        jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
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
