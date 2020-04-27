/*====================================== INICIO-CARGA =====================================*/
$(document).on('ready', function () {
    
    LogOut();
    //document.getElementById("Cerrar-Session").addEventListener("click", ResetSession);
    $('#btn_cerrar').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            url: '../Portal/WebSessions.aspx?Accion=Cerrar',
            type: 'POST',
            cache: false,
            async: true,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                window.location.href = "../Portal/SignIn.aspx";
            }
        });
    });
});



/// <summary>
/// CREAR MODAL MENSAJE
/// </summary>
function LogOut() {
    var tags = '';
    tags += '<div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" id="LogOut" name="LogOut">';
    tags += '<div class="modal-dialog modal-sm">';
    tags += '<div class="modal-content">';
    tags += '<div class="modal-header">';
    tags += '<h4 id="title_LogOut" class="modal-title" id="myModalLabel"><span class="glyphicon glyphicon-option-vertical"></span> Advertencia</h4>';
    tags += '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>';
    tags += '</div>';
    tags += '<div id="Ml_boby_LogOut" class="modal-body">';
    tags += 'Tu sesión ha expirado';
    tags += '</div>';
    tags += '<div class="modal-footer">';
    tags += '<button id="Cerrar-Session" type="button" class="btn btn-info" data-dismiss="modal">Cerrar</button>';
    tags += '</div>';
    tags += '</div>';
    tags += '</div>';
    tags += '</div>';
    $(tags).appendTo('body');
}