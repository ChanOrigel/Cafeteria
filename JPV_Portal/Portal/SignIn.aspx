<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="JPV_Portal.Portal.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <title>Punto de Venta | Inicia sesión</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=no" />
    <meta name="description" content="IMASEN" />
    <meta http-equiv="Content-Language" content="es" />
    <meta name="author" content="" />
    <!--styles -->
    <link href="../Recursos/bootstrap-4.0.0-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Recursos/Css/JPV_Login.css" rel="stylesheet" />
    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
      <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->
    <!-- favicon -->
    <%--<link rel="shortcut icon" href="../Recursos/Image/Logos/Imasen.ico" />--%>
</head>
<body>
    <div class="jumbotron"></div>
    <div class="container">
        <div id="login-wraper">
            <form class="form login-form">
                <div class="login-header text-center">
                    <p>Ingresa tus datos para acceder al Portal</p>
                </div>
                <div class="body">
                    <input type="text" placeholder="Usuario" class="form-control form-control-sm" name="username" id="Txt_User" autocomplete="off" />
                    <small>
                        <label style="color: red" class="label-error" id="lbl_error_user"></label>
                    </small>
                    <input type="password" placeholder="Contraseña" class="form-control form-control-sm" name="password" id="Txt_Password_User" autocomplete="off" />
                    <small>
                        <label style="color: red" class="label-error" id="lbl_error_password"></label>
                    </small>
                    <!-- Errors container -->
                    <div style="color: red" class="errors-container text-center"></div>
                    <button id="Btn_Entrar_User" type="submit" class="btn btn-success btn-block">Iniciar sesión</button>
                </div>
                <div class="login-footer text-center">
                    <a id="Btn_Recuperar_User" class="btn-link" title="Recuperar Contraseña" style="cursor: pointer;">¿Olvidaste tu Contraseña?</a>
                </div>
            </form>
        </div>
    </div>
    <footer class="footer"></footer>

    <!--  Modales ================================================== -->
    <div id="Ventana_Espera" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Procesando</h4>
                </div>
                <div class="modal-body">
                    <div class="progress">
                        <div class="progress-bar progress-bar-striped active" role="progressbar"
                            aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" id="modal_mensaje" name="modal_mensaje">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="title" class="modal-title"></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <h5 id="Mensaje"></h5>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-info" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modal_recuperar_user" name="Recuperar_Contrasenia">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel">
                        <i class="fa fa-pencil-square-o" style="font-size: 25px;"></i>&nbsp;&nbsp;Recuperar Contraseña
                    </h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times"></i></button>
                </div>
                <div class="modal-body">
                    <table width="100%">
                        <tr>
                            <td></td>
                            <td>
                                <input type="text" id="txt_no_emp_pass" class="form-control" placeholder="Correo electronico" /></td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="submit" class="btn btn-success" data-dismiss="modal" id="btn_enviar_user"><i class="fa fa-envelope"></i>&nbsp;Enviar</button>
                </div>
            </div>
        </div>
    </div>
    <!--  javascript ================================================== -->
    <script src="../Recursos/Jquery/jquery-3.3.1.min.js"></script>
    <script src="../Recursos/Jquery/jquery-migrate-3.0.0.min.js"></script>
    <script src="../Recursos/bootstrap-4.0.0-dist/js/bootstrap.min.js"></script>
    <script src="../Recursos/Javascript/backstretch.min.js"></script>
    <script src="../Recursos/Javascript/JPV-login.js"></script>
    <script src="../JS/SingIn.js?v=1.0.1"></script>
</body>
</html>
