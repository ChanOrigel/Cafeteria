/*====================================== VARIABLES =====================================*/


/*====================================== INICIO-CARGA =====================================*/
jQuery(document).on('ready', function () {
    LimpiarControles();
    eventos();
});


/// <summary>
/// FUNCION QUE INICIALIZA LOS MANEJADORES DE EVENTOS.
/// </summary>
function eventos() {
    $('#btn_enviar_user').bind('click', function (e) {
        e.preventDefault();
        recuperar_contrasenia(true);
    });

    $('#Btn_Recuperar_User').on('click', function (e) {
        e.preventDefault();
        jQuery('#modal_recuperar_user').modal('show', { backdrop: 'static' });
    });

    $('#Btn_Entrar_User').on('click', function (e) {
        e.preventDefault();
        Validar_Usuario();
    });

    $(document).on("keypress", "#Txt_Password", function (e) {
        if (e.which == 13) {
            Validar_Autenticacion();
        }
    });
}

/// <summary>
/// FUNCION PARA EL LOGIN
/// </summary>
    function Validar_Usuario() {
        var valido = true;
        LimpiarControles();
        try {
            if ($('#Txt_User').val() == undefined || $('#Txt_User').val() == "" || $('#Txt_User').val() == null) {
                valido = false;
                $('#lbl_error_user').text('Favor de proporcionar su Usuario');
            }

            if ($('#Txt_Password_User').val() == undefined || $('#Txt_Password_User').val() == "" || $('#Txt_Password_User').val() == null) {
                valido = false;
                $('#lbl_error_password_user').text('Favor de proporcionar la contraseña');
            }

            if (valido) {
                var Parametros = new Array();
                Parametros = new Object();
                Parametros.Usuario = $('#Txt_User').val();

                Parametros.Password = $('#Txt_Password_User').val();

                $.ajax({
                    url: "WebSessions.aspx?Accion=Autentificar&Param=" + JSON.stringify(Parametros),
                    method: 'POST',
                    cache: false,
                    async: true,
                    contentType: 'application/json; charset=UTF-8',
                    dataType: 'json',
                    success: function (resp) {
                        if (resp.Estatus) {
                            window.location.href = 'Frm_Ventas.aspx';
                        }
                        else {
                            $('.errors-container').text(resp.Mensaje);
                        }
                    }
                });
            } else {
                $("label").addClass("font-label-error");
            }
        } catch (e) {
            asignar_modal("Informe Técnico", e);
        }
    }

    /// <summary>
    /// FUNCION PARA LIMPIAR LOS CONTROLES
    /// </summary>
    function LimpiarControles() {
        $('#lbl_error_No_proveedor').text('');
        $('#lbl_error_password_Proveedor').text('');
        $('#lbl_error_user').text('');
        $('#lbl_error_password_user').text('');
        $('.errors-container').text('');
    }
    /// <summary>
    /// Función que se utilizara para recuperar la contrasenia.
    /// </summary>
    function recuperar_contrasenia(Validar) {
        try {

            if (Validar) {
                if ($("#txt_no_emp_pass").val() != null && $("#txt_no_emp_pass").val() != "" && $("#txt_no_emp_pass").val() != undefined) {
                    var Parametros = new Array();
                    Parametros = new Object();
                    Parametros.Email = $("#txt_no_emp_pass").val();
                    Abrir_Ventana_Espera();
                    $.ajax({
                        url: "WebSessions.aspx?Accion=RecuperarUsuario&Param=" + JSON.stringify(Parametros),
                        method: 'POST',
                        cache: false,
                        async: false,
                        contentType: 'application/json; charset=UTF-8',
                        dataType: 'json',
                        success: function (resp) {
                            Cerrar_Ventana_Espera();
                            $("#txt_no_emp_pass").val('');
                            asignar_modal("Recuperación de contraseña", resp.Mensaje);
                            jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                            Cerrar_Ventana_Espera();
                        }
                    });
                }
            } else {
                if ($("#txt_no_prov_pass").val() != null && $("#txt_no_prov_pass").val() != "" && $("#txt_no_prov_pass").val() != undefined) {
                    var Parametros = new Array();
                    Parametros = new Object();
                    Parametros.No_Proveedor = $("#txt_no_prov_pass").val();

                    $.ajax({
                        url: "WebSessions.aspx?Accion=RecuperarProveedor&Param=" + JSON.stringify(Parametros),
                        method: 'POST',
                        cache: false,
                        async: false,
                        contentType: 'application/json; charset=UTF-8',
                        dataType: 'json',
                        success: function (resp) {
                            $("#txt_no_prov_pass").val('');
                            asignar_modal("Recuperación de contraseña", resp.Mensaje);
                            jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
                            Cerrar_Ventana_Espera();
                        }
                    });
                }
            }

        } catch (e) {
            asignar_modal("Informe Técnico", e);
            jQuery('#modal_mensaje').modal({ backdrop: 'static', keyboard: false });
            Cerrar_Ventana_Espera();
        }
    }

    /// <summary>
    /// CREAR MODAL MENSAJE
    /// </summary>
    function asignar_modal(titulo, mensaje) {
        $('#title').text('');
        $('#Mensaje').text('');
        $('#title').text(titulo);
        $('#Mensaje').text(mensaje);
    }


    function Abrir_Ventana_Espera() {
        var pleaseWaitDiv = $('#Ventana_Espera');
        pleaseWaitDiv.modal();
    }
    function Cerrar_Ventana_Espera() {
        var pleaseWaitDiv = $('#Ventana_Espera');
        pleaseWaitDiv.modal('hide');
    }